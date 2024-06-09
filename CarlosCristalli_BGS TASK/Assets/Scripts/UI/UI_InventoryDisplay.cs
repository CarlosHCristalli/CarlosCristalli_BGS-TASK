using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace BGS_TEST
{
    public class UI_InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private UI_ItemDisplayManager itemDisplayTemplate;
        [Space]
        [SerializeField] private Transform scrollViewDisplayParent;
        [SerializeField] private TMP_Text titleDisplay;
        [SerializeField] private TMP_Text moneyDisplay;
        [SerializeField] private TMP_Text ShoppingListPriceDisplay;
        [SerializeField] private GameObject ShoppingListPriceDisplayParent;

        /// <summary>
        /// Sets up the inventory display with the given parameters.
        /// </summary>
        /// <param name="title">title of the inventory display.</param>
        /// <param name="callback">Action to perform on selecting an item.</param>
        /// <param name="toBeDisplayed">List of items to display.</param>
        /// <param name="money">Current money amount.</param>
        /// <param name="shoppingListPrice">Total price of items in the shopping list.</param>
        public void Setup(string title, Action<CharacterCustomizationPiece> callback,List<CharacterCustomizationPiece> toBeDisplayed, int money, int shoppingListPrice)
        {
            titleDisplay.text = title;
            moneyDisplay.text = $"{money}$";
            ShoppingListPriceDisplay.text = $"-{shoppingListPrice}$";
            ShoppingListPriceDisplayParent.SetActive(shoppingListPrice > 0);

            ClearPreviousItemsDisplayed();

            foreach (var piece in toBeDisplayed)
            {
                UI_ItemDisplayManager temp = Instantiate(itemDisplayTemplate, scrollViewDisplayParent);
                temp.Setup(()=> callback(piece), piece);
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
    }
}