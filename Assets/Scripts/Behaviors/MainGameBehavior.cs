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

        public TextBoxBehavior textBox;

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
        /// Unity callback function that is called
        /// per frame.
        /// </summary>
        private void Update()
        {
            switch (CurrentState)
            {
                case State.Intro:

                    // For now, just jump to wake up sequence, and set the text about waking up
                    textBox.SetText("You woke up at 8AM.");
                    CurrentState = State.WakeUpSequence;

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
