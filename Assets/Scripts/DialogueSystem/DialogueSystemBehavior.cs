﻿using System.Collections.Generic;

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
        /// Was the textbox clicked?
        /// </summary>
        private bool textBoxClicked = false;

        /// <summary>
        /// Current dialogue being stored.
        /// </summary>
        public Dialogue CurrentDialogue
        {
            get;
            private set;
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
            get;
            private set;
        } = true;

        /// <summary>
        /// Play the provided dialogue.
        /// </summary>
        /// <param name="dialogue">The dialogue to play.</param>
        /// <returns>True if the dialogue was played successfully. False otherwise.</returns>
        public bool PlayDialogue(Dialogue dialogue)
        {
            if (dialogue == null)
            {
                CurrentDialogue = null;
                IsDialogueFinished = true;

                if (textBox != null)
                {
                    textBox.SetText("", 2);
                }

                return false;
            }

            if ((dialogue.lines.Count == 0) || !parserBehavior.ParseConditions(dialogue.dialogueCondition))
            {
                IsDialogueFinished = true;
                return false;
            }

            CurrentDialogue = dialogue;
            CurrentDialogueLineIndex = -1;
            if (HasNextDialogueLine())
            {
                ShowNextDialogueLine();
                IsDialogueFinished = false;

                return true;
            }
            else
            {
                IsDialogueFinished = true;

                return false;
            }
        }

        /// <summary>
        /// Play the first dialogue whose conditions are
        /// satisfied.
        /// </summary>
        /// <param name="dialogueList">List of dialogues</param>
        /// <returns>The dialogue played. If there were no dialogues whose conditions are satisfied, returns null.</returns>
        public Dialogue PlayFirstPossibleDialogue(List<Dialogue> dialogueList)
        {
            foreach (Dialogue dialogue in dialogueList)
            {
                if (PlayDialogue(dialogue))
                {
                    return dialogue;
                }
            }

            return null;
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
        /// when the script is enabled.
        /// </summary>
        private void OnEnable()
        {
            if (textBox != null)
            {
                textBox.onClicked.AddListener(HandleTextBoxClicked);
            }
        }

        /// <summary>
        /// Unity callback function that is called
        /// when the script is disabled.
        /// </summary>
        private void OnDisable()
        {
            if (textBox != null)
            {
                textBox.onClicked.RemoveListener(HandleTextBoxClicked);
            }
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
                        IsDialogueFinished = true;
                        textBox.SetText(null, 2);
                        textBox.SetProceedIndicatorVisible(false);
                    }
                }
            }

            textBoxClicked = false;
        }

        /// <summary>
        /// Did the player press the next dialogue button during the current frame?
        /// </summary>
        /// <returns>True if the player pressed the next dialogue button during the current frame.</returns>
        private bool IsProceedDialogButtonPressed()
        {
            return Input.GetKeyDown(KeyCode.Space) || textBoxClicked;
        }

        /// <summary>
        /// Show the next dialogue line
        /// </summary>
        private void ShowNextDialogueLine()
        {
            if (CurrentDialogue == null)
            {
                return;
            }

            int nextLineIndex = GetNextDialogueLineIndex(CurrentDialogueLineIndex);
            if (nextLineIndex != -1)
            {
                Dialogue.Line line = CurrentDialogue.lines[nextLineIndex];

                if (textBox != null)
                {
                    textBox.SetProceedIndicatorVisible(true);
                    textBox.SetText(line.message, 2);
                }

                parserBehavior.ParseCommands(line.commands);

                CurrentDialogueLineIndex = nextLineIndex;
            }
        }

        /// <summary>
        /// Gets the index of the next valid dialogue line for the current dialogue.
        /// </summary>
        /// <param name="startingIndex">Index where the search will start.</param>
        /// <returns>The index of the next valid dialogue line. Returns -1 if a valid line was not found.</returns>
        private int GetNextDialogueLineIndex(int startingIndex)
        {
            if (CurrentDialogue != null)
            {
                for (int i = startingIndex + 1; i < CurrentDialogue.lines.Count; ++i)
                {
                    if (parserBehavior.ParseConditions(CurrentDialogue.lines[i].messageCondition))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Determines whether there is a next dialogue line
        /// that can be displayed.
        /// </summary>
        /// <returns>True if there is a next dialogue line. False otherwise.</returns>
        private bool HasNextDialogueLine()
        {
            return GetNextDialogueLineIndex(CurrentDialogueLineIndex) != -1;
        }

        /// <summary>
        /// Handler function for when the text box was clicked.
        /// </summary>
        private void HandleTextBoxClicked()
        {
            textBoxClicked = true;
        }
    }
}
