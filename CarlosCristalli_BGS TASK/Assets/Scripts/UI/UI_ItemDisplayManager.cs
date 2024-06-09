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
        [Header("display")]
        [SerializeField] private Image nameDisplayBackground;
        [SerializeField] private TMP_Text nameDisplay;
        [SerializeField] private Image iconDisplay;
        [SerializeField] private Image iconShadowDisplay;
        [SerializeField] private TMP_Text priceDisplay;
        [SerializeField] private GameObject equippedLogo;
        [SerializeField] private Color equippedColor;

        [Space]
        [Header("Piece")]
        public CharacterCustomizationPiece pieceToDisplay;
        private Action callBack;

        /// <summary>
        /// Sets up the item display with the given parameters.
        /// </summary>
        /// <param name="callback">Action to perform on item selection.</param>
        /// <param name="piece">Customization piece to display.</param>
        public void Setup(Action callback, CharacterCustomizationPiece piece, bool equipped)
        {
            pieceToDisplay = piece;
            this.callBack = callback;

            // Update UI elements with piece details
            equippedLogo.SetActive(equipped);
            nameDisplayBackground.color = (equipped)? equippedColor : Color.white;
            nameDisplay.text = piece.name;
            priceDisplay.text = $"{piece.Price}$";
            iconDisplay.sprite = piece.Icon;
            iconShadowDisplay.sprite = piece.Icon;
        }

        /// <summary>
        /// Invokes the callback action when the item is selected.
        /// </summary>
        public void SelectItem()
        {
            callBack?.Invoke();
        }
    }
}
