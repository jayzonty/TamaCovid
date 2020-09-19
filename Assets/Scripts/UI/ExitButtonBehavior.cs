using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace TamaCovid
{
    /// <summary>
    /// Class that handles the behavior of the
    /// exit button in the title screen.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class ExitButtonBehavior : MonoBehaviour
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
            button.onClick.AddListener(ExitGame);
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the script is disabled.
        /// </summary>
        private void OnDisable()
        {
            button.onClick.RemoveListener(ExitGame);
        }

        /// <summary>
        /// Exit the game.
        /// </summary>
        private void ExitGame()
        {
            // Quitting from the editor is different
            // from quitting the application (when deployed)
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            // Exit the game.
            Application.Quit();
#endif
        }
    }
}
