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

        private bool isBoothEmpty = true;
        private bool paidForIt = true;

        private Character_VisualManager character;
        private Character_InputManager characterInput;
        private Character_MovementController characterMovement;
        private Transform characterDefaultParent;
        private Vector3 characterDefaultLocalPosition;

        private void OnEnable()
        {
            if (display == null)
            {
                Debug.LogError($"display reference is missing in Interactable_ChangingBooth: {gameObject.name}");
                return;
            }

            if (isBoothEmpty && paidForIt)
            {
                InitializeDisplay();
            }

            Character_Inventory.ReturningPiece += ReceiveReturn;
            Character_Inventory.BuyingPiece += WasPaidFor;
        }

        private void OnDisable()
        {
            Character_Inventory.ReturningPiece -= ReceiveReturn;
            Character_Inventory.BuyingPiece -= WasPaidFor;
        }

        /// <summary>
        /// Initializes the display by setting the default state of the booth.
        /// </summary>
        private void InitializeDisplay()
        {
            display.HidePartType(CharacterCustomizationPiece.Type.Body, false);
            display.HidePartType(CharacterCustomizationPiece.Type.Hair, false);
            display.HidePartType(CharacterCustomizationPiece.Type.Hat, false);

            display.SetCurrentCustomizationPiece(selectedCustomizationPiece);
            isBoothEmpty = false;
        }

        // Overrides the Interact method from the base class to handle interactions with the changing booth
        public override void Interact(Character_InteractionManager _character)
        {
            base.Interact(_character);

            if (isBoothEmpty)
            {
                if (!_character.GetComponent<Character_Inventory>().ShoppingList.Contains(selectedCustomizationPiece))
                    return;
            }

            if (_character != null)
                character = _character.GetComponentInChildren<Character_VisualManager>();

            characterDefaultParent = character.transform.parent;
            characterDefaultLocalPosition = character.transform.localPosition;
            character.transform.parent = characterParentForAnimation;

            DisableCharacterMovement(_character);

            animator.SetTrigger("Enter");
            soundEffectPlayer.Play();
        }

        /// <summary>
        /// Disables the character's movement and input controls.
        /// </summary>
        /// <param name="_character">The character to disable.</param>
        private void DisableCharacterMovement(Character_InteractionManager _character)
        {
            characterInput = _character.GetComponent<Character_InputManager>();
            characterMovement = _character.GetComponent<Character_MovementController>();

            if (characterInput != null)
            {
                characterInput.ResetMoveDirection();
                characterInput.enabled = false;
                characterMovement.StopMoving();
                characterMovement.enabled = false;
            }
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
                paidForIt = false;
                tooltipDisplay = "Press E to Return";
            }
            else
            {
                character.RemovePieceFromInventory(selectedCustomizationPiece);
                ReceiveReturn(selectedCustomizationPiece);
            }
        }

        /// <summary>
        /// Called when the changing animation 1 cycle is finished, to equip or remove the customization piece.
        /// </summary>
        public void FinishedChanging()
        {
            // Equip or remove the selected customization piece
            EquipCustomizationPiece();
            soundEffectPlayer.Play();
        }

        /// <summary>
        /// Called when the changing animation 2 cycle is finished, to reset the character's parent and re-enable input.
        /// </summary>
        public void AnimationOver()
        {
            if (character != null)
            {
                character.transform.parent = characterDefaultParent;
                character.transform.localPosition = characterDefaultLocalPosition;

                if (characterInput != null)
                {
                    characterInput.enabled = true;
                    characterMovement.enabled = true;
                }
            }
        }

        /// <summary>
        /// Receives the returned customization piece and updates the booth state.
        /// </summary>
        /// <param name="piece">The returned customization piece.</param>
        void ReceiveReturn(CharacterCustomizationPiece piece)
        {
            if(selectedCustomizationPiece == piece)
            {
                character.SetCurrentCustomizationPieceFromInventory(selectedCustomizationPiece.PieceType);
                display.HidePartType(selectedCustomizationPiece.PieceType, false);
                isBoothEmpty = false;
                paidForIt = true;
                tooltipDisplay = "Press E to Try";
            }
        }

        void WasPaidFor(CharacterCustomizationPiece piece)
        {
            if (selectedCustomizationPiece == piece)
            {
                paidForIt = true;
            }

            tooltipDisplay = "Out of stock";
        }
    }
}