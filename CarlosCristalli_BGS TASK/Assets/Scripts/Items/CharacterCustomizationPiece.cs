using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS_TEST
{
    [CreateAssetMenu(fileName = "New Piece", menuName = "ScriptableObjects/CharacterCustomizationPiece", order = 1)]
    public class CharacterCustomizationPiece : ScriptableObject
    {
        public enum Type { Body, Hair, Hat}

        public Type TypeOfPiece;
        public string DisplayName;
        public Sprite Icon;
        public AnimatorOverrideController AnimatorOverride;
    }
}