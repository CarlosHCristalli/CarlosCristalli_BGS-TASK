using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BGS_TEST
{
    public class Character_VisualManager : MonoBehaviour
    {
        [SerializeField] private Character_Inventory inventory;

        [Header("Body")]
        [SerializeField] private Animator mainAnimator;
        [SerializeField] private Animator clothesAnimator;
        [SerializeField] private AnimatorOverrideController currentClothesAnimator;
        [SerializeField] private CharacterCustomizationPiece currentClothingPiece;

        [Header("Hair")]
        [SerializeField] private Animator hairAnimator;
        [SerializeField] private AnimatorOverrideController currentHairAnimator;
        [SerializeField] private CharacterCustomizationPiece currentHairPiece;

        [Header("Hat")]
        [SerializeField] private Animator hatAnimator;
        [SerializeField] private AnimatorOverrideController currentHatAnimator;
        [SerializeField] private CharacterCustomizationPiece currentHatPiece;

        /// <summary>
        /// Updates all animators with the given isSprinting state and movement angle.
        /// </summary>
        /// <param name="isSprinting">Whether the character is sprinting or not.</param>
        /// <param name="movementAngle">The angle of movement.</param>
        public void UpdateAnimators(bool isSprinting, float movementAngle)
        {
            // List of animators to update
            Animator[] animators = { mainAnimator, clothesAnimator, hairAnimator, hatAnimator };

            // Update each animator
            foreach (Animator animator in animators)
            {
                animator.SetBool("IsSprinting", isSprinting);
                animator.SetFloat("MovementAngle", movementAngle);
            }
        }

        /// <summary>
        /// Sets the current customization piece (clothing, hair, or hat) and optionally adds it to the inventory.
        /// </summary>
        /// <param name="piece">The customization piece to set.</param>
        /// <param name="addToInventory">Whether to add the piece to the inventory.</param>
        public void SetCurrentCustomizationPiece(CharacterCustomizationPiece piece, bool addToInventory = false)
        {
            //Activate the current part of the specified type before changing it
            HidePartType(piece.PieceType, false);

            // Set the appropriate animator and piece based on the type of customization piece
            switch (piece.PieceType)
            {
                case CharacterCustomizationPiece.Type.Body:
                    SetAnimator(ref currentClothingPiece, ref currentClothesAnimator, clothesAnimator, piece);
                    break;

                case CharacterCustomizationPiece.Type.Hair:
                    SetAnimator(ref currentHairPiece, ref currentHairAnimator, hairAnimator, piece);
                    break;

                case CharacterCustomizationPiece.Type.Hat:
                    SetAnimator(ref currentHatPiece, ref currentHatAnimator, hatAnimator, piece);
                    break;
            }

            // Add the piece to the inventory if specified
            if (addToInventory)
            {
                inventory.AddPiece(piece);
            }
        }

        /// <summary>
        /// Helper method to set the animator and current piece for a given type.
        /// </summary>
        /// <param name="currentPiece">Reference to the current piece field.</param>
        /// <param name="currentAnimator">Reference to the current animator override controller field.</param>
        /// <param name="animator">The animator to set the runtime controller for.</param>
        /// <param name="piece">The new customization piece to set.</param>
        private void SetAnimator(ref CharacterCustomizationPiece currentPiece, ref AnimatorOverrideController currentAnimator, Animator animator, CharacterCustomizationPiece piece)
        {
            currentPiece = piece;
            currentAnimator = piece.AnimatorOverride;
            animator.runtimeAnimatorController = currentAnimator;
        }

        /// <summary>
        /// Removes a customization piece from the inventory.
        /// </summary>
        /// <param name="piece">The piece to remove from the inventory.</param>
        public void RemovePieceFromInventory(CharacterCustomizationPiece piece)
        {
            inventory.RemovePiece(piece);
        }

        /// <summary>
        /// Sets the current customization piece from the inventory based on the type.
        /// </summary>
        /// <param name="type">The type of customization piece to set.</param>
        public void SetCurrentCustomizationPieceFromInventory(CharacterCustomizationPiece.Type type)
        {
            // Find the first piece in the inventory that matches the specified type
            var selected = inventory.GetPieceByType(type);

            if (selected != null)
            {
                // Set the found piece as the current piece
                SetCurrentCustomizationPiece(selected);
            }
            else
            {
                // Hide the part if no matching piece is found
                HidePartType(type, true);
            }
        }

        /// <summary>
        /// Hides or shows a part type based on the value parameter.
        /// </summary>
        /// <param name="type">The type of customization piece to hide or show.</param>
        /// <param name="value">Whether to hide or show the part.</param>
        public void HidePartType(CharacterCustomizationPiece.Type type,bool value)
        {
            switch (type)
            {
                case CharacterCustomizationPiece.Type.Body:
                    clothesAnimator.gameObject.SetActive(!value);
                    break;

                case CharacterCustomizationPiece.Type.Hair:
                    hairAnimator.gameObject.SetActive(!value);
                    break;

                case CharacterCustomizationPiece.Type.Hat:
                    hatAnimator.gameObject.SetActive(!value);
                    break;

            }
        }
    }
}