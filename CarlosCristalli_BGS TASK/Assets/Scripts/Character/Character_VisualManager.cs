using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS_TEST
{
    public class Character_VisualManager : MonoBehaviour
    {

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

        // Update all animators with the given isSprinting state and movement angle
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

        // Set the current clothing, hair, or hat piece
        public void SetCurrentCustomizationPiece(CharacterCustomizationPiece piece)
        {
            switch (piece.TypeOfPiece)
            {
                case CharacterCustomizationPiece.Type.Body:
                    currentClothingPiece = piece;
                    currentClothesAnimator = piece.AnimatorOverride;
                    clothesAnimator.runtimeAnimatorController = currentClothesAnimator;
                    break;

                case CharacterCustomizationPiece.Type.Hair:
                    currentHairPiece = piece;
                    currentHairAnimator = piece.AnimatorOverride;
                    hairAnimator.runtimeAnimatorController = currentClothesAnimator;
                    break;

                case CharacterCustomizationPiece.Type.Hat:
                    currentHatPiece = piece;
                    currentHatAnimator = piece.AnimatorOverride;
                    hatAnimator.runtimeAnimatorController = currentHatAnimator;
                    break;
            }

        }
    }
}