using System.Collections.Generic;

using UnityEngine;

namespace TamaCovid
{
    /// <summary>
    /// Class that handles the behavior of the
    /// DialogueSystem.
    /// </summary>
    public class DialogueSystemBehavior : MonoBehaviour
    {
        /// <summary>
        /// Text box
        /// </summary>
        public TextBoxBehavior textBox;

        /// <summary>
        /// Reference to the parser behavior script.
        /// </summary>
        private ParserBehavior parserBehavior;

        /// <summary>
        /// Current dialogue being shown
        /// </summary>
        public Dialogue CurrentDialogue
        {
            get;
            set;
        }

        /// <summary>
        /// Which line inside the current dialogue
        /// are we displaying/processing right now.
        /// </summary>
        public int CurrentDialogueLineIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// Is the current dialogue finished.
        /// </summary>
        public bool IsDialogueFinished
        {
            get
            {
                return (CurrentDialogue == null)
                    || (CurrentDialogueLineIndex >= CurrentDialogue.lines.Count);
            }
        }

        /// <summary>
        /// Show the provided dialogue.
        /// </summary>
        /// <param name="dialogue"></param>
        public void ShowDialogue(Dialogue dialogue)
        {
            CurrentDialogue = dialogue;

            if ((dialogue == null) || dialogue.lines.Count == 0)
            {
                if (textBox != null)
                {
                    textBox.SetText("");
                }

                return;
            }

            CurrentDialogueLineIndex = -1;
            ShowNextDialogueLine();
        }

        /// <summary>
        /// Show the first dialogue whose conditions are
        /// satisfied.
        /// </summary>
        /// <param name="dialogueList">List of dialogues</param>
        /// <returns>True if there was a dialogue whose conditions are satisfied. False otherwise.</returns>
        public bool ShowFirstPossibleDialogue(List<Dialogue> dialogueList)
        {
            foreach (Dialogue dialogue in dialogueList)
            {
                if (parserBehavior.ParseConditions(dialogue.conditionsString))
                {
                    ShowDialogue(dialogue);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the game object is created.
        /// </summary>
        private void Awake()
        {
            parserBehavior = GameObject.FindObjectOfType<ParserBehavior>();
        }

        /// <summary>
        /// Unity callback function that is called
        /// before the first update call.
        /// </summary>
        private void Start()
        {
        }

        /// <summary>
        /// Unity callback function that is called
        /// every frame.
        /// </summary>
        private void Update()
        {
            if (CurrentDialogue != null)
            {
                if (IsProceedDialogButtonPressed())
                {
                    if (HasNextDialogueLine())
                    {
                        ShowNextDialogueLine();
                    }
                    else
                    {
                        textBox.SetText(null, 2);
                    }
                }
            }
        }

        /// <summary>
        /// Did the player press the next dialogue button during the current frame?
        /// </summary>
        /// <returns>True if the player pressed the next dialogue button during the current frame.</returns>
        private bool IsProceedDialogButtonPressed()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        /// <summary>
        /// Show the next dialogue line
        /// </summary>
        private void ShowNextDialogueLine()
        {
            while (++CurrentDialogueLineIndex < CurrentDialogue.lines.Count)
            {
                Dialogue.Line line = CurrentDialogue.lines[CurrentDialogueLineIndex];
                if (parserBehavior.ParseConditions(line.conditionsString))
                {
                    textBox.SetText(line.message, 2);
                    parserBehavior.ParseCommands(line.commandsString);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether there is a next dialogue line
        /// that can be displayed.
        /// </summary>
        /// <returns>True if there is a next dialogue line. False otherwise.</returns>
        private bool HasNextDialogueLine()
        {
            for (int i = CurrentDialogueLineIndex; i < CurrentDialogue.lines.Count; ++i)
            {
                Dialogue.Line line = CurrentDialogue.lines[CurrentDialogueLineIndex];
                if (parserBehavior.ParseConditions(line.conditionsString))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
