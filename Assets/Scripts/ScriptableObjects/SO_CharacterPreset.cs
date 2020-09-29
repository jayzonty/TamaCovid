using UnityEngine;

namespace TamaCovid
{
    /// <summary>
    /// Scriptable object containing data about a
    /// character preset.
    /// </summary>
    [CreateAssetMenu(fileName = "New Preset", menuName = "TamaCovid/Character Preset")]
    public class SO_CharacterPreset : ScriptableObject
    {
        /// <summary>
        /// Name of the preset.
        /// </summary>
        public string presetName = "Character Preset";

        /// <summary>
        /// Description for the preset.
        /// </summary>
        [TextArea]
        public string description = "Description";

        /// <summary>
        /// Starting money
        /// </summary>
        public int startingMoney = 0;

        /// <summary>
        /// Starting job
        /// </summary>
        public int startingJob = 0;

        /// <summary>
        /// Does the character possess a car?
        /// </summary>
        public bool hasCar = false;

        /// <summary>
        /// Is the character immunocompromised?
        /// </summary>
        public bool isImmunocompromised = false;

        /// <summary>
        /// Does the character have depression?
        /// </summary>
        public bool hasDepression = false;
    }
}
