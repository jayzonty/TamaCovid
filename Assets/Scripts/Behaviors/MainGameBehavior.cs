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
        /// Game state data.
        /// </summary>
        public GameState GameState
        {
            get;
            private set;
        }

        /// <summary>
        /// Test dialogue. (TEMPORARY)
        /// </summary>
        public Dialogue testIntroDialogue;

        /// <summary>
        /// States within the gameplay loop
        /// </summary>
        public enum State
        {
            Intro,
            PreWakeUpSequence,
            WakeUpSequence,
            ActivitiesSelection,
            PerformActivities,
            PrepareToSleep,
            Sleep,
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
        /// Unity callback function that is called
        /// when the game object is created.
        /// </summary>
        private void Awake()
        {
            GameState = new GameState();

            GameState.AddStat(Constants.MONEY_STAT_NAME);
            GameState.AddStat(Constants.ENERGY_STAT_NAME);
            GameState.AddStat(Constants.FOOD_STAT_NAME);
            GameState.AddStat(Constants.HUNGER_STAT_NAME);
            GameState.AddStat(Constants.ANXIETY_STAT_NAME);

            // TODO: Move the randomization somewhere else.
            // For testing purposes.
            GameState.SetFlagValue("hasCovid", Random.value <= 0.5f);

            dialogueSystem = GameObject.FindObjectOfType<DialogueSystemBehavior>();
        }

        /// <summary>
        /// Unity callback function that is called
        /// before the first update call.
        /// </summary>
        private void Start()
        {
            dialogueSystem.ShowDialogue(testIntroDialogue);
        }

        /// <summary>
        /// Unity callback function that is called
        /// per frame.
        /// </summary>
        private void Update()
        {
            switch (CurrentState)
            {
                case State.Intro:

                    if (dialogueSystem.IsDialogueFinished)
                    {
                        dialogueSystem.ShowDialogue(null);

                        // For now, just jump to wake up sequence, and set the text about waking up
                        textBox.SetText("You woke up at 8AM.");
                        CurrentState = State.WakeUpSequence;
                    }

                    break;

                case State.WakeUpSequence:

                    // Temporary.
                    // If player presses the next dialogue button,
                    // [roceed to activities selection.
                    if (IsProceedDialogButtonPressed())
                    {
                        textBox.SetText("Choose activities for the day.");
                        CurrentState = State.ActivitiesSelection;
                    }

                    break;

                case State.ActivitiesSelection:

                    // Temporary
                    // If player presses the next dialogue button,
                    // proceed to perform activities.
                    if (IsProceedDialogButtonPressed())
                    {
                        textBox.SetText("Perform activities.");
                        CurrentState = State.PerformActivities;
                    }

                    break;

                case State.PerformActivities:

                    // Temporary
                    // If player presses the next dialogue button,
                    // proceed to sleep preparation.
                    if (IsProceedDialogButtonPressed())
                    {
                        textBox.SetText("Prepare to sleep.");
                        CurrentState = State.PrepareToSleep;
                    }

                    break;

                case State.PrepareToSleep:

                    // Temporary
                    // If player presses the next dialogue button,
                    // sleep.
                    if (IsProceedDialogButtonPressed())
                    {
                        textBox.SetText("Sleeping... zzzzz");
                        CurrentState = State.Sleep;
                    }

                    break;

                case State.Sleep:

                    // Temporary
                    // If player presses the next dialogue button,
                    // wake up
                    if (IsProceedDialogButtonPressed())
                    {
                        textBox.SetText("You woke up at 8AM.");
                        CurrentState = State.WakeUpSequence;
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
