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
                    break;

                case State.WakeUpSequence:
                    break;

                case State.ActivitiesSelection:
                    break;

                case State.PerformActivities:
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
    }
}
