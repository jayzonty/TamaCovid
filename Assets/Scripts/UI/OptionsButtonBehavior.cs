using UnityEngine;
using UnityEngine.UI;

namespace TamaCovid
{
    /// <summary>
    /// Class that handles the behavior of the
    /// options button in the title screen.
    /// </summary>
    public class OptionsButtonBehavior : MonoBehaviour
    {
        /// <summary>
        /// Button component attached to this game object.
        /// </summary>
        private Button button;

        /// <summary>
        /// Unity callback function that is called
        /// when the game object is created.
        /// </summary>
        private void Awake()
        {
            button = GetComponent<Button>();
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the script is enabled.
        /// </summary>
        private void OnEnable()
        {
            button.onClick.AddListener(OpenOptionsScreen);
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the script is disabled.
        /// </summary>
        private void OnDisable()
        {
            button.onClick.RemoveListener(OpenOptionsScreen);
        }

        /// <summary>
        /// Exit the game.
        /// </summary>
        private void OpenOptionsScreen()
        {
            // TODO: Implement opening of options screen.
        }
    }
}
