using System.Collections.Generic;

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
        /// List of text for each layer of the text box.
        /// </summary>
        private List<string> textLayers;

        /// <summary>
        /// Sets the text being displayed by the text box.
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        public void SetText(string text)
        {
            SetText(text, textLayers.Count - 1);
        }

        /// <summary>
        /// Sets the text being displayed by the text box.
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <param name="layer">Layer in which the text will be displayed</param>
        /// <remarks>Text set in the highest layer will always be displayed.</remarks>
        public void SetText(string text, int layer)
        {
            if (layer >= textLayers.Count)
            {
                int numLayersToAdd = layer - textLayers.Count + 1;
                for (int i = 0; i < numLayersToAdd; ++i)
                {
                    textLayers.Add("");
                }
            }

            textLayers[layer] = text;
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the game object is created.
        /// </summary>
        private void Awake()
        {
            textLayers = new List<string>();
            textLayers.Add("");
        }

        /// <summary>
        /// Unity callback function that is called
        /// per frame.
        /// </summary>
        private void Update()
        {
            if (textComponent != null)
            {
                string textToDisplay = "";
                for (int i = textLayers.Count - 1; i >= 0; --i)
                {
                    if (!string.IsNullOrEmpty(textLayers[i]))
                    {
                        textToDisplay = textLayers[i];
                        break;
                    }
                }

                textComponent.text = textToDisplay;
            }
        }
    }
}
