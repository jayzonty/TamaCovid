using System.Collections.Generic;

using UnityEngine;

namespace TamaCovid
{
    /// <summary>
    /// Scriptable object containing data
    /// about an in-game event.
    /// </summary>
    [CreateAssetMenu(fileName = "New Event", menuName = "TamaCovid/Event")]
    public class SO_Event : ScriptableObject
    {
        /// <summary>
        /// Struct that contains the data for one branch
        /// of an event.
        /// </summary>
        [System.Serializable]
        public struct EventBranch
        {
            /// <summary>
            /// String representing the condition
            /// for triggering this event branch.
            /// </summary>
            public string triggerCondition;

            /// <summary>
            /// String describing what happens when this
            /// event branch is triggered.
            /// </summary>
            public string eventAction;
        }

        /// <summary>
        /// List of event branches.
        /// </summary>
        public List<EventBranch> eventBranches;
    }
}
