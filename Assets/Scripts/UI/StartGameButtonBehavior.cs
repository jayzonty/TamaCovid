using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TamaCovid
{
    /// <summary>
    /// Class that handles the behavior of the
    /// start button in the title screen.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class StartGameButtonBehavior : MonoBehaviour
    {
        /// <summary>
        /// Button component attached to the game object.
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
            button.onClick.AddListener(StartGame);
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the script is disabled.
        /// </summary>
        private void OnDisable()
        {
            button.onClick.RemoveListener(StartGame);
        }

        /// <summary>
        /// Start the game.
        /// </summary>
        private void StartGame()
        {
            // For now, just switch to the main game scene.
            // Might also want to have a constant variable
            // somewhere for the scene name of the main game.
            SceneManager.LoadScene("MainGameScene");
        }
    }
}
