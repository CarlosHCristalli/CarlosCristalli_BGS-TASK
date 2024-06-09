using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace BGS_TEST
{
    public class UI_InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private Character_VisualManager character;
        [SerializeField] private UI_ItemDisplayManager itemDisplayTemplate;
        [Space]
        [SerializeField] private Transform scrollViewDisplayParent;
        [SerializeField] private TMP_Text titleDisplay;
        [SerializeField] private TMP_Text moneyDisplay;
        [SerializeField] private TMP_Text shoppingListPriceDisplay;
        [SerializeField] private GameObject shoppingListPriceDisplayParent;

        /// <summary>
        /// Sets up the inventory display with the given parameters.
        /// </summary>
        /// <param name="title">title of the inventory display.</param>
        /// <param name="callback">Action to perform on selecting an item.</param>
        /// <param name="toBeDisplayed">List of items to display.</param>
        /// <param name="money">Current money amount.</param>
        /// <param name="shoppingListPrice">Total price of items in the shopping list.</param>
        public void Setup(string title, Action<CharacterCustomizationPiece, bool> callback,List<CharacterCustomizationPiece> toBeDisplayed, int money, int shoppingListPrice)
        {
            titleDisplay.text = title;
            moneyDisplay.text = $"{money}$";
            shoppingListPriceDisplay.text = $"-{shoppingListPrice}$";
            shoppingListPriceDisplayParent.SetActive(shoppingListPrice > 0);

            ClearPreviousItemsDisplayed();

            List<CharacterCustomizationPiece> equippedFound = CheckForEquippedItems(toBeDisplayed);

            // Display equipped items
            foreach (var piece in equippedFound)
            {
                UI_ItemDisplayManager temp = Instantiate(itemDisplayTemplate, scrollViewDisplayParent);
                temp.Setup(() => callback(piece, true), piece, true);
            }

            // Display the remaining items
            foreach (var piece in toBeDisplayed)
            {
                if (!equippedFound.Contains(piece))
                {
                    UI_ItemDisplayManager temp = Instantiate(itemDisplayTemplate, scrollViewDisplayParent);
                    temp.Setup(()=> callback(piece, false), piece, false);
                }
            }
        }

        /// <summary>
        /// Clears the previously displayed items from the scroll view.
        /// </summary>
        private void ClearPreviousItemsDisplayed()
        {
            foreach (Transform child in scrollViewDisplayParent)
            {
                if (child.GetComponent<UI_ItemDisplayManager>() != null)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        private List<CharacterCustomizationPiece> CheckForEquippedItems(List<CharacterCustomizationPiece> toBeDisplayed)
        {
            List<CharacterCustomizationPiece> equippedItemsFound = new List<CharacterCustomizationPiece>();

            foreach (var piece in toBeDisplayed)
            {
                if(piece == character.EquippedClothingPiece && !equippedItemsFound.Contains(piece))
                {
                    equippedItemsFound.Add(piece);
                }
                if (piece == character.EquippedHairPiece && !equippedItemsFound.Contains(piece))
                {
                    equippedItemsFound.Add(piece);
                }
                if (piece == character.EquippedHatPiece && !equippedItemsFound.Contains(piece))
                {
                    equippedItemsFound.Add(piece);
                }
            }

            return equippedItemsFound;
        }
    }
}