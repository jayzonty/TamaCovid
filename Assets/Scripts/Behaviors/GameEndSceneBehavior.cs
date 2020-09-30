using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TamaCovid
{
    /// <summary>
    /// Monobehavior containing functionality related
    /// to the game end scene.
    /// </summary>
    public class GameEndSceneBehavior : MonoBehaviour
    {
        /// <summary>
        /// List of possible ending texts.
        /// </summary>
        [TextArea]
        public List<string> endingTexts;

        /// <summary>
        /// Text component that will display the
        /// ending text.
        /// </summary>
        public Text gameEndText;

        /// <summary>
        /// Reference to the GameStateBehavior script.
        /// </summary>
        private GameStateBehavior gameStateBehavior;

        /// <summary>
        /// Unity callback function that is called
        /// when the game object is created.
        /// </summary>
        private void Awake()
        {
            gameStateBehavior = GameObject.FindObjectOfType<GameStateBehavior>();
        }

        /// <summary>
        /// Unity callback function that is called
        /// before the first update call.
        /// </summary>
        private void Start()
        {
            if (gameEndText != null)
            {
                string text = endingTexts[gameStateBehavior.EndingNumber];

                text += "\n\nCOVID-19 status: ";

                if (gameStateBehavior.Data.HasCovid)
                {
                    text += "Positive\n";
                    text += "Number of people infected: " + gameStateBehavior.Data.GetStatValue(Constants.NUM_INFECTED_STAT_NAME) + "\n";
                    text += "Action that caused you to contract COVID-19: " + gameStateBehavior.actionThatCauseCOVID.displayName;
                }
                else
                {
                    text += "Negative";
                }

                text += "\n\nPress space to continue...";

                gameEndText.text = text;
            }
        }

        /// <summary>
        /// Unity callback function that is called
        /// per frame.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
    }
}
