using UnityEngine;
using UnityEngine.UI;

namespace TamaCovid
{
    /// <summary>
    /// Class describing the behavior of
    /// the generic text box.
    /// </summary>
    public class TextBoxBehavior : MonoBehaviour
    {
        /// <summary>
        /// Text component that displays the text string.
        /// </summary>
        public Text textComponent;

        /// <summary>
        /// Sets the text being displayed by the text box.
        /// </summary>
        /// <param name="text"></param>
        public void SetText(string text)
        {
            if (text != null)
            {
                textComponent.text = text;
            }
        }

    }
}
