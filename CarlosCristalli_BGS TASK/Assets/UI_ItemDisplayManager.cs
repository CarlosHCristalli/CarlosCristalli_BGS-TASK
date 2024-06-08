using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BGS_TEST
{
    public class UI_ItemDisplayManager : MonoBehaviour
    {
        [Header("Display")]
        [SerializeField] private TMP_Text nameDisplay;
        [SerializeField] private Image IconDisplay;
        [SerializeField] private Image IconShadowDisplay;

        [Space]
        [Header("Piece")]
        public CharacterCustomizationPiece pieceToDisplay;
        private Action callBack;

        public void Setup(Action callBack, CharacterCustomizationPiece piece)
        {
            pieceToDisplay = piece;
            this.callBack = callBack;

            nameDisplay.text = piece.name;
            IconDisplay.sprite = piece.Icon;
            IconShadowDisplay.sprite = piece.Icon;
        }

        public void SelectItem()
        {
            callBack();
        }
    }
}
