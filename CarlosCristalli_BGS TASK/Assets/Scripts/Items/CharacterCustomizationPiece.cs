using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS_TEST
{
    [CreateAssetMenu(fileName = "New Piece", menuName = "ScriptableObjects/CharacterCustomizationPiece", order = 1)]
    public class CharacterCustomizationPiece : ScriptableObject
    {
        /// <summary>
        /// Enum representing the type of customization piece (Body, Hair, Hat).
        /// </summary>
        public enum Type { Body, Hair, Hat}

        public Type PieceType;
        public string DisplayName;
        public Sprite Icon;
        public AnimatorOverrideController AnimatorOverride;
    }
}