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
        /// Perform the provided action.
        /// </summary>
        /// <param name="action">Data related to the action being performed.</param>
        public void DoAction(SO_Action action)
        {
            if (CurrentState == State.ActivitiesSelection)
            {
                dialogueSystem.ShowFirstPossibleDialogue(action.dialogues);

                CurrentState = State.PerformActivities;
            }
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the game object is created.
        /// </summary>
        private void Awake()
        {
            dialogueSystem = GameObject.FindObjectOfType<DialogueSystemBehavior>();
        }

        /// <summary>
        /// Unity callback function that is called
        /// before the first update call.
        /// </summary>
        private void Start()
        {
            //dialogueSystem.ShowDialogue(testIntroDialogue);

            CurrentState = State.Intro;
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
                    CurrentState = State.ActivitiesSelection;
                    break;

                case State.ActivitiesSelection:
                    textBox.SetText("Choose activities for the day.", 0);
                    break;

                case State.PerformActivities:
                    if (dialogueSystem.IsDialogueFinished)
                    {
                        dialogueSystem.ShowDialogue(null);
                        CurrentState = State.ActivitiesSelection;
                    }

                    break;

                case State.PrepareToSleep:
                    break;

                case State.Sleep:
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
