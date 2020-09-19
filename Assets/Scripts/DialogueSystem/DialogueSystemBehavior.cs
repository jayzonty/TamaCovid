using UnityEngine;

namespace TamaCovid
{
    /// <summary>
    /// Class that handles the behavior of the
    /// DialogueSystem.
    /// </summary>
    public class DialogueSystemBehavior : MonoBehaviour
    {
        /// <summary>
        /// Text box
        /// </summary>
        public TextBoxBehavior textBox;

        /// <summary>
        /// Data regarding the game state.
        /// </summary>
        private GameState gameState;

        /// <summary>
        /// Current dialogue being shown
        /// </summary>
        public Dialogue CurrentDialogue
        {
            get;
            set;
        }

        /// <summary>
        /// Which line inside the current dialogue
        /// are we displaying/processing right now.
        /// </summary>
        public int CurrentDialogueLineIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// Is the current dialogue finished.
        /// </summary>
        public bool IsDialogueFinished
        {
            get
            {
                return (CurrentDialogue == null)
                    || (CurrentDialogueLineIndex >= CurrentDialogue.lines.Count);
            }
        }

        /// <summary>
        /// Show the provided dialogue.
        /// </summary>
        /// <param name="dialogue"></param>
        public void ShowDialogue(Dialogue dialogue)
        {
            CurrentDialogue = dialogue;

            if ((dialogue == null) || dialogue.lines.Count == 0)
            {
                if (textBox != null)
                {
                    textBox.SetText("");
                }

                return;
            }

            CurrentDialogueLineIndex = -1;
            ShowNextDialogueLine();
        }

        /// <summary>
        /// Unity callback function that is called
        /// before the first update call.
        /// </summary>
        private void Start()
        {
            // TODO: Temporary
            MainGameBehavior mainGameBehavior = GameObject.FindObjectOfType<MainGameBehavior>();
            if (mainGameBehavior != null)
            {
                gameState = mainGameBehavior.GameState;
            }
        }

        /// <summary>
        /// Unity callback function that is called
        /// every frame.
        /// </summary>
        private void Update()
        {
            if (CurrentDialogue != null)
            {
                if (IsProceedDialogButtonPressed())
                {
                    if (HasNextDialogueLine())
                    {
                        ShowNextDialogueLine();
                    }
                    else
                    {
                        textBox.SetText("");
                    }
                }
            }
        }

        /// <summary>
        /// Did the player press the next dialogue button during the current frame?
        /// </summary>
        /// <returns>True if the player pressed the next dialogue button during the current frame.</returns>
        private bool IsProceedDialogButtonPressed()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        /// <summary>
        /// Show the next dialogue line
        /// </summary>
        private void ShowNextDialogueLine()
        {
            while (++CurrentDialogueLineIndex < CurrentDialogue.lines.Count)
            {
                Dialogue.Line line = CurrentDialogue.lines[CurrentDialogueLineIndex];
                if (ParseConditions(line.conditionsString))
                {
                    textBox.SetText(line.message);
                    Debug.Log(line.message);
                    ParseCommands(line.commandsString);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether there is a next dialogue line
        /// that can be displayed.
        /// </summary>
        /// <returns>True if there is a next dialogue line. False otherwise.</returns>
        private bool HasNextDialogueLine()
        {
            for (int i = CurrentDialogueLineIndex; i < CurrentDialogue.lines.Count; ++i)
            {
                Dialogue.Line line = CurrentDialogue.lines[CurrentDialogueLineIndex];
                if (ParseConditions(line.conditionsString))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Parse the commands laid out in the provided string
        /// </summary>
        /// <param name="commandsString"></param>
        private void ParseCommands(string commandsString)
        {
            if (string.IsNullOrEmpty(commandsString))
            {
                return;
            }

            // TODO: Might want to consider doing regex instead.
            // TODO: Implement the commands
            // For now, split based on white space.
            // StringSplitOptions.RemoveEmptyEntries is to account for multiple white spaces between commands.
            string[] commands = commandsString.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (string command in commands)
            {
                if (command.StartsWith("@stat"))
                {
                    // Modify stat command (examples: @stat:money+5, @stat:hunger-3, @stat:anxiety=10)
                }
                else if (command.StartsWith("@setFlag"))
                {
                    // Set flag command
                }
                else if (command.StartsWith("@unsetFlag"))
                {
                    // Unset flag command
                }
            }
        }

        /// <summary>
        /// Parse and evaluate the conditions laid out in string form.
        /// </summary>
        /// <param name="conditionsString">String containing the conditions</param>
        /// <returns></returns>
        private bool ParseConditions(string conditionsString)
        {
            // TODO: Implement

            return true;
        }
    }
}
