using System.Collections.Generic;

using UnityEngine;

namespace TamaCovid
{
    /// <summary>
    /// Scriptable object that contains data about
    /// in-game actions.
    /// </summary>
    [CreateAssetMenu(fileName = "New Action", menuName = "TamaCovid/Action")]
    public class SO_Action : ScriptableObject
    {
        /// <summary>
        /// Category of the action
        /// </summary>
        public string category;

        /// <summary>
        /// Display name of the action.
        /// </summary>
        public string displayName;

        /// <summary>
        /// Description of the action
        /// </summary>
        [TextArea]
        public string description;

        /// <summary>
        /// Risk factor of contracting/spreading COVID when performing this action.
        /// </summary>
        public float covidRiskFactor = 0.0f;

        /// <summary>
        /// Number of other people involved in this action.
        /// </summary>
        public int numPeopleInvolved = 0;

        /// <summary>
        /// String containing the conditions for this
        /// action to be enabled.
        /// </summary>
        public string enabledConditionString;

        /// <summary>
        /// List of dialogue lines for this particular action
        /// </summary>
        public List<Dialogue> dialogues;
    }
}
