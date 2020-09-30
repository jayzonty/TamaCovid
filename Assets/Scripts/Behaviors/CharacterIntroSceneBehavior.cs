using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TamaCovid
{
    public class CharacterIntroSceneBehavior : MonoBehaviour
    {
        public List<SO_CharacterPreset> characterPresets;

        public Text descriptionText;

        private GameStateBehavior gameStateBehavior;

        private void Awake()
        {
            gameStateBehavior = GameObject.FindObjectOfType<GameStateBehavior>();
        }

        private void Start()
        {
            gameStateBehavior.ResetStatsAndFlags();

            // Randomly choose a character preset to start with
            int randomIndex = Random.Range(0, characterPresets.Count);
            SO_CharacterPreset characterPreset = characterPresets[randomIndex];

            gameStateBehavior.InitializeGameStateFromPreset(characterPreset);

            if (descriptionText != null)
            {
                descriptionText.text = characterPreset.description;
                descriptionText.text += "\n\nPress space to continue...";
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("MainGameScene");
            }
        }
    }
}
