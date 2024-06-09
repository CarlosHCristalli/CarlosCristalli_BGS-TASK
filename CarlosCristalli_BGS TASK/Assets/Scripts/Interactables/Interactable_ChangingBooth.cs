using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS_TEST
{
    public class Interactable_ChangingBooth : Interactable
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Transform characterParentForAnimation;

        [SerializeField] private Character_VisualManager display;
        [SerializeField] private CharacterCustomizationPiece selectedCustomizationPiece;

        private bool isBoothEmpty = false;

        private Character_VisualManager character;
        private Character_InputManager characterInput;
        private Transform characterDefaultParent;

        private void OnEnable()
        {
            if (display == null)
            {
                Debug.LogError($"display reference is missing in Interactable_ChangingBooth: {gameObject.name}");
                return;
            }

            display.HidePartType(CharacterCustomizationPiece.Type.Body, false);
            display.HidePartType(CharacterCustomizationPiece.Type.Hair, false);
            display.HidePartType(CharacterCustomizationPiece.Type.Hat, false);

            display.SetCurrentCustomizationPiece(selectedCustomizationPiece);

            Character_Inventory.ReturningPiece += ReciveReturn;
        }

        private void OnDisable()
        {
            Character_Inventory.ReturningPiece -= ReciveReturn;
        }

        // Overrides the Interact method from the base class to handle interactions with the changing booth
        public override void Interact(Character_InteractionManager _character)
        {
            base.Interact(_character);

            if(_character != null)
                character = _character.GetComponentInChildren<Character_VisualManager>();

            characterDefaultParent = character.transform.parent;
            character.transform.parent = characterParentForAnimation;

            // Disable character movement
            characterInput = _character.GetComponent<Character_InputManager>();
            if (characterInput != null)
                characterInput.enabled = false;

            animator.SetTrigger("Enter");
        }

        /// <summary>
        /// Equips or removes the selected customization piece based on the current state of the changing booth.
        /// </summary>
        public void EquipCustomizationPiece()
        {
            if (character == null || display == null)
            {
                Debug.LogError($"Character or display references are missing in Interactable_ChangingBooth. : {gameObject.name}");
                return;
            }

            if (!isBoothEmpty)
            {
                character.SetCurrentCustomizationPiece(selectedCustomizationPiece, true);
                display.HidePartType(selectedCustomizationPiece.PieceType, true);
                isBoothEmpty = true;
                tooltipDisplay = "Press E to Return";
            }
            else
            {
                character.RemovePieceFromInventory(selectedCustomizationPiece);
                ReciveReturn(selectedCustomizationPiece);
            }
        }

        /// <summary>
        /// Called when the changing animation 1 cycle is finished, to equip or remove the customization piece.
        /// </summary>
        public void FinishedChanging()
        {
            // Equip or remove the selected customization piece
            EquipCustomizationPiece();
        }

        /// <summary>
        /// Called when the changing animation 2 cycle is finished, to reset the character's parent and re-enable input.
        /// </summary>
        public void AnimationOver()
        {
            if (character != null)
            {
                character.transform.parent = characterDefaultParent;

                if (characterInput != null)
                    characterInput.enabled = true;
            }
        }

        /// <summary>
        /// Receives the returned customization piece and updates the booth state.
        /// </summary>
        /// <param name="piece">The returned customization piece.</param>
        void ReciveReturn(CharacterCustomizationPiece piece)
        {
            if(selectedCustomizationPiece == piece)
            {
                character.SetCurrentCustomizationPieceFromInventory(selectedCustomizationPiece.PieceType);
                display.HidePartType(selectedCustomizationPiece.PieceType, false);
                isBoothEmpty = false;
                tooltipDisplay = "Press E to Try";
            }
        }
    }
}