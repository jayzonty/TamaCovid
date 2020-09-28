using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TamaCovid
{
    /// <summary>
    /// Monobehavior that contains functions related
    /// to parsing commands/expressions.
    /// </summary>
    public class ParserBehavior : MonoBehaviour
    {
        private GameState gameState;

        /// <summary>
        /// Parse the commands laid out in the provided string.
        /// </summary>
        /// <remarks>
        /// For now, commands affect just the game state, but should
        /// be changed in the future if we want it to be more flexible.
        /// </remarks>
        /// <param name="commandsString"></param>
        public void ParseCommands(string commandsString)
        {
            if (string.IsNullOrEmpty(commandsString))
            {
                return;
            }

            // TODO: Might want to consider doing regex instead.
            // TODO: Implement the commands
            // For now, split based on semi-colon.
            // StringSplitOptions.RemoveEmptyEntries is to account for multiple white spaces between commands.
            string[] commands = commandsString.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (string command in commands)
            {
                string commandStr = command.Trim();
                if (!string.IsNullOrEmpty(commandStr))
                {
                    if (command.StartsWith("@"))
                    {
                        // Special commands to be added in the future
                    }
                    else
                    {
                        // We're expecting a = b, a + b, or a - b
                        char op = '=';
                        if (commandStr.Contains("+"))
                        {
                            op = '+';
                        }
                        else if (commandStr.Contains("-"))
                        {
                            op = '-';
                        }

                        string[] tokens = commandStr.Split(op);
                        if (tokens.Length == 2) // If it's one of the 3 formats above, we expect to get both a and b
                        {
                            string a = tokens[0].Trim();
                            string b = tokens[1].Trim();

                            // First try if a is a stat name. If it is, apply the
                            // operation accordingly.
                            int statVal;
                            if (gameState.TryGetStatValue(a, out statVal))
                            {
                                int intVal = 0;
                                int.TryParse(b, out intVal);

                                if (op == '=') { statVal = intVal; }
                                else if (op == '+') { statVal += intVal; }
                                else if (op == '-') { statVal -= intVal; }

                                // HACK: Quick hack for now to make sure the
                                // time does not exceed 24 hours (1440 minutes).
                                if (a == Constants.TIME_STAT_NAME)
                                {
                                    int day = gameState.GetStatValue(Constants.DAY_STAT_NAME);
                                    while (statVal >= 1440)
                                    {
                                        statVal -= 1440;
                                        ++day;
                                    }
                                    gameState.SetStatValue(Constants.DAY_STAT_NAME, day);
                                }

                                gameState.SetStatValue(a, statVal);
                            }
                            // If it's not a stat name, assume it's a flag, and
                            // set the value accordingly.
                            else
                            {
                                bool flagVal = false;
                                if (bool.TryParse(b, out flagVal))
                                {
                                    gameState.SetFlagValue(a, flagVal);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Parse and evaluate the conditions laid out in string form.
        /// </summary>
        /// <param name="conditionsString">String containing the conditions</param>
        /// <returns></returns>
        public bool ParseConditions(string conditionsString)
        {
            // TODO: Refactor. This function is getting long and not flexible

            if (string.IsNullOrEmpty(conditionsString))
            {
                return true;
            }

            conditionsString = conditionsString.Trim();

            if (conditionsString.Contains(" "))
            {
                string[] expressions = conditionsString.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (string expression in expressions)
                {
                    if (!ParseConditions(expression))
                    {
                        return false;
                    }
                }

                return true;
            }
            else if (conditionsString.Contains("|"))
            {
                string[] expressions = conditionsString.Split(new char[] { '|' }, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (string expression in expressions)
                {
                    if (ParseConditions(expression))
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                if (conditionsString.StartsWith("@"))
                {
                    // Special commands (@command(...))

                    int openParenthesisIndex = conditionsString.IndexOf('(');
                    int closedParenthesisIndex = conditionsString.IndexOf(')');

                    int commandNameLength = openParenthesisIndex - 1;
                    string commandName = conditionsString.Substring(1, commandNameLength);

                    int argumentsStringLength = closedParenthesisIndex - openParenthesisIndex - 1;
                    string argumentsString = conditionsString.Substring(openParenthesisIndex + 1, argumentsStringLength);

                    if (commandName == "rollSuccess")
                    {
                        return RollSuccessCommand(float.Parse(argumentsString));
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (conditionsString.Contains("=") || conditionsString.Contains(">") || conditionsString.Contains("<"))
                {
                    int aLength = 0, bStart = 0;
                    bool lessThan = false, greaterThan = false, equal = false;
                    if (conditionsString.Contains("==") || conditionsString.Contains("!="))
                    {
                        aLength = conditionsString.IndexOf('=');
                        bStart = aLength + 2;
                        equal = conditionsString.Contains("==");
                    }
                    else if (conditionsString.Contains("<")) // a < b, a <= b
                    {
                        lessThan = true;
                        aLength = conditionsString.IndexOf('<');
                        bStart = aLength + 1;
                        if (conditionsString.Contains("="))
                        {
                            equal = true;
                            ++bStart;
                        }
                    }
                    else // a > b, a >= b
                    {
                        greaterThan = true;
                        aLength = conditionsString.IndexOf('>');
                        bStart = aLength + 1;
                        if (conditionsString.Contains("="))
                        {
                            equal = true;
                            ++bStart;
                        }
                    }

                    string statName = conditionsString.Substring(0, aLength);
                    int statValue;
                    if (gameState.TryGetStatValue(statName, out statValue))
                    {
                        int val;
                        if (int.TryParse(conditionsString.Substring(bStart), out val))
                        {
                            if (lessThan)
                            {
                                if (equal) { return statValue <= val; }
                                else { return statValue < val; }
                            }
                            else if (greaterThan)
                            {
                                if (equal) { return statValue >= val; }
                                else { return statValue > val; }
                            }
                            else if (equal)
                            {
                                return statValue == val;
                            }
                            else
                            {
                                return statValue != val;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    bool negate = false;
                    if (conditionsString.StartsWith("!"))
                    {
                        negate = true;
                        conditionsString = conditionsString.Substring(1);
                    }

                    bool flagValue = gameState.GetFlagValue(conditionsString);
                    return flagValue != negate;
                }
            }
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the game object is created.
        /// </summary>
        private void Awake()
        {
            GameStateBehavior gameStateBehavior = GameObject.FindObjectOfType<GameStateBehavior>();
            if (gameStateBehavior != null)
            {
                gameState = gameStateBehavior.Data;
            }
        }

        /// <summary>
        /// Command for rolling either a true or false given
        /// the odds of getting a true.
        /// </summary>
        /// <param name="odds">Odds of getting a true (0.0 ~ 1.0)</param>
        /// <returns></returns>
        private bool RollSuccessCommand(float odds)
        {
            float roll = Random.Range(0.0f, 1.0f);
            return roll <= odds;
        }
    }
}
