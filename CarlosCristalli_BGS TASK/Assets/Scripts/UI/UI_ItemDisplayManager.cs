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
        [SerializeField] private TMP_Text nameDisplay;
        [SerializeField] private Image iconDisplay;
        [SerializeField] private Image iconShadowDisplay;
        [SerializeField] private TMP_Text priceDisplay;

        [Space]
        [Header("Piece")]
        public CharacterCustomizationPiece pieceToDisplay;
        private Action callBack;

        /// <summary>
        /// Sets up the item display with the given parameters.
        /// </summary>
        /// <param name="callBack">Action to perform on item selection.</param>
        /// <param name="piece">Customization piece to display.</param>
        public void Setup(Action callBack, CharacterCustomizationPiece piece)
        {
            pieceToDisplay = piece;
            this.callBack = callBack;

            // Update UI elements with piece details
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
            callBack?.Invoke(); // Use null-conditional operator to ensure callback is not null
        }
    }
}
