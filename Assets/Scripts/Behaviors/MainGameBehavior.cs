using System.Collections.Generic;

using UnityEngine;

namespace TamaCovid
{
    /// <summary>
    /// MonoBehavior class for handling the behavior
    /// of the main game loop.
    /// </summary>
    public class MainGameBehavior : MonoBehaviour
    {
        // TODO: Separate functionality of each state into separate classes,
        // something like a "Sequence" class.

        /// <summary>
        /// Reference to the text box.
        /// </summary>
        public TextBoxBehavior textBox;

        /// <summary>
        /// Start of day events
        /// </summary>
        public List<SO_Event> startOfDayEvents;

        /// <summary>
        /// Start of day dialogues
        /// </summary>
        public List<Dialogue> startOfDayDialogues;

        /// <summary>
        /// States within the gameplay loop
        /// </summary>
        public enum State
        {
            Setup,
            StartOfDayEvents,
            StartOfDayDialogues,
            ActivitiesSelection,
            PerformAction,
            GameEnd
        }

        /// <summary>
        /// Current state in the gameplay loop.
        /// </summary>
        public State CurrentState
        {
            get;
            private set;
        }

        /// <summary>
        /// Reference to the dialogue system.
        /// </summary>
        private DialogueSystemBehavior dialogueSystem;

        /// <summary>
        /// Reference to the parser behavior script
        /// </summary>
        private ParserBehavior parserBehavior;

        /// <summary>
        /// Reference to the game state data.
        /// </summary>
        private GameState gameState;

        /// <summary>
        /// Cached list of dialogues to play.
        /// (Used during CurrentState == State.StartOfDayDialogues)
        /// </summary>
        private List<Dialogue> dialoguesToPlay = new List<Dialogue>();

        /// <summary>
        /// Perform the provided action.
        /// </summary>
        /// <param name="action">Data related to the action being performed.</param>
        public void DoAction(SO_Action action)
        {
            if (CurrentState == State.ActivitiesSelection)
            {
                if (dialogueSystem.PlayFirstPossibleDialogue(action.dialogues) != null)
                {
                    CurrentState = State.PerformAction;
                }
            }
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the game object is created.
        /// </summary>
        private void Awake()
        {
            dialogueSystem = GameObject.FindObjectOfType<DialogueSystemBehavior>();

            parserBehavior = GameObject.FindObjectOfType<ParserBehavior>();

            GameStateBehavior gameStateBehavior = GameObject.FindObjectOfType<GameStateBehavior>();
            if (gameStateBehavior != null)
            {
                gameState = gameStateBehavior.Data;
            }
        }

        /// <summary>
        /// Unity callback function that is called
        /// before the first update call.
        /// </summary>
        private void Start()
        {
            CurrentState = State.Setup;
        }

        /// <summary>
        /// Unity callback function that is called
        /// per frame.
        /// </summary>
        private void Update()
        {
            switch (CurrentState)
            {
                case State.Setup:
                    // Setup stat values of player. In the future, set this
                    // based on some sort of preset.
                    gameState.SetStatValue(Constants.DAY_STAT_NAME, 1);

                    gameState.SetStatValue(Constants.MONEY_STAT_NAME, 500);

                    gameState.HasDepression = Random.value <= 0.25f;

                    CurrentState = State.StartOfDayEvents;

                    break;

                case State.StartOfDayEvents:
                    foreach (SO_Event startOfDayEvent in startOfDayEvents)
                    {
                        foreach (SO_Event.EventBranch eventBranch in startOfDayEvent.eventBranches)
                        {
                            if (parserBehavior.ParseConditions(eventBranch.triggerCondition))
                            {
                                parserBehavior.ParseCommands(eventBranch.eventAction);
                                break;
                            }
                        }
                    }
                    foreach (Dialogue startOfDayDialogue in startOfDayDialogues)
                    {
                        if (parserBehavior.ParseConditions(startOfDayDialogue.conditionsString))
                        {
                            dialoguesToPlay.Add(startOfDayDialogue);
                        }
                    }

                    CurrentState = State.StartOfDayDialogues;

                    break;

                case State.StartOfDayDialogues:
                    if (dialogueSystem.IsDialogueFinished)
                    {
                        if (dialoguesToPlay.Count > 0)
                        {
                            Dialogue chosenDialogue = dialogueSystem.PlayFirstPossibleDialogue(dialoguesToPlay);
                            if (chosenDialogue != null)
                            {
                                dialoguesToPlay.Remove(chosenDialogue);
                            }
                            else
                            {
                                CurrentState = State.ActivitiesSelection;
                            }
                        }
                        else
                        {
                            CurrentState = State.ActivitiesSelection;
                        }
                    }

                    break;

                case State.ActivitiesSelection:
                    textBox.SetText("Choose activities for the day.", 0);
                    break;

                case State.PerformAction:
                    if (dialogueSystem.IsDialogueFinished)
                    {
                        dialogueSystem.PlayDialogue(null);

                        int day = gameState.GetStatValue(Constants.DAY_STAT_NAME);
                        if (day > 7)
                        {
                            // Once the player ends day 7, the game ends as well.
                            CurrentState = State.GameEnd;
                        }
                        else
                        {
                            // Proceed as normal.
                            CurrentState = State.ActivitiesSelection;
                        }
                    }

                    break;

                case State.GameEnd:
                    break;

                default: // Should not happen
                    Debug.LogError("Reach an invalid state!");
                    break;
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
    }
}
