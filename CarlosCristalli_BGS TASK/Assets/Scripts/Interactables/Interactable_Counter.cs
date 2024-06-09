using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS_TEST
{
    public class Interactable_Counter : Interactable
    {
        private Character_Inventory characterInventory;

        [SerializeField] private AudioClip purchase;
        [SerializeField] private AudioClip callAttention;

        public override void Interact(Character_InteractionManager _character)
        {
            characterInventory = _character.GetComponent<Character_Inventory>();

            if (characterInventory == null)
            {
                Debug.LogError("Character_Inventory component is missing from the character.");
                return;
            }

            UI_Manager.Instance.OpenCounterOptions();

            soundEffectPlayer.clip = callAttention;
            soundEffectPlayer.Play();
        }

        /// <summary>
        /// Handles the buy action by displaying a confirmation message or an error message.
        /// </summary>
        public void Buy()
        {
            if (characterInventory != null)
            {
                if (characterInventory.ShoppingList.Count > 0 && characterInventory.Money >= characterInventory.ShoppingListPrice)
                {
                    UI_Manager.Instance.ShowConfirmationMessage(ConfirmPurchase, GetConfirmationMessage());
                }
                else
                {
                    string message = characterInventory.ShoppingList.Count == 0
                        ? "No items on Shopping List."
                        : "You can't afford this!";
                    UI_Manager.Instance.ShowMessage(message);
                }
            }
        }

        /// <summary>
        /// Opens the inventory display for selling items.
        /// </summary>
        public void Sell()
        {
            UI_Manager.Instance.OpenInventoryDisplay("Inventory", characterInventory.SellPiece, characterInventory.InventoryList, characterInventory.Money, -1);
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
                soundEffectPlayer.clip = purchase;
                soundEffectPlayer.Play();
            }
        }
    }
}