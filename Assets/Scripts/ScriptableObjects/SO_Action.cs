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
        /// Description of the action
        /// </summary>
        public string description;

        // TODO: Add data regarding conditions for
        // allowing the action to be performed.

        // TODO: Add data for dialogue lines and triggers
        // to change the game state, or to set/unset flags, etc.
    }
}
