using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS_TEST
{
    public class Interactable_ChangingBooth : Interactable
    {
        [SerializeField] private Character_VisualManager Display;
        [SerializeField] private CharacterCustomizationPiece SelectedCustomizationPiece;

        bool isBoothEmpty = false;

        private Character_VisualManager character;

        private void OnEnable()
        {
            if (Display == null)
            {
                Debug.LogError($"Display reference is missing in Interactable_ChangingBooth: {gameObject.name}");
                return;
            }

            Display.HidePartType(CharacterCustomizationPiece.Type.Body, false);
            Display.HidePartType(CharacterCustomizationPiece.Type.Hair, false);
            Display.HidePartType(CharacterCustomizationPiece.Type.Hat, false);

            Display.SetCurrentCustomizationPiece(SelectedCustomizationPiece);
        }

        // Overrides the Interact method from the base class to handle interactions with the changing booth
        public override void Interact(Character_InteractionManager _character)
        {
            base.Interact(_character);

            if(_character != null)
                character = _character.GetComponentInChildren<Character_VisualManager>();

            // Equip or remove the selected customization piece
            EquipCustomizationPiece();
        }

        /// <summary>
        /// Equips or removes the selected customization piece based on the current state of the changing booth.
        /// </summary>
        public void EquipCustomizationPiece()
        {
            if (character == null || Display == null)
            {
                Debug.LogError($"Character or Display references are missing in Interactable_ChangingBooth. : {gameObject.name}");
                return;
            }

            if (!isBoothEmpty)
            {
                character.SetCurrentCustomizationPiece(SelectedCustomizationPiece, true);
                Display.HidePartType(SelectedCustomizationPiece.PieceType, true);
                isBoothEmpty = true;
            }
            else
            {
                character.RemovePieceFromInventory(SelectedCustomizationPiece);
                character.SetCurrentCustomizationPieceFromInventory(SelectedCustomizationPiece.PieceType);
                Display.HidePartType(SelectedCustomizationPiece.PieceType, false);
                isBoothEmpty = false;
            }
        }
    }
}