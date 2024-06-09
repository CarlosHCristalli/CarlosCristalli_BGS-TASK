using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS_TEST
{
    public class Interactable_Counter : Interactable
    {
        Character_Inventory characterInventory;

        public override void Interact(Character_InteractionManager _character)
        {
            characterInventory = _character.GetComponent<Character_Inventory>();

            if (characterInventory != null)
            {
                if (characterInventory.ShoppingList.Count > 0)
                {
                    UI_Manager.Instance.ShowConfirmationMessage(ConfirmPurchase, GetConfirmationMessage());
                }
                else
                {
                    UI_Manager.Instance.ShowMessage("No items on Shopping List");
                }
            }
        }

        /// <summary>
        /// Generates the confirmation message for the purchase.
        /// </summary>
        /// <returns>A string containing the confirmation message.</returns>
        private string GetConfirmationMessage()
        {
            return $"Would you like to buy the items on your shopping list?\nThe amount totals to: {characterInventory.ShoppingListPrice}$";
        }

        /// <summary>
        /// Confirms the purchase by buying the items on the shopping list.
        /// </summary>
        public void ConfirmPurchase()
        {
            if (characterInventory != null)
            {
                characterInventory.BuyShoppingList();
            }
        }
    }
}