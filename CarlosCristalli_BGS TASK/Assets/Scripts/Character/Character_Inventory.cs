using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BGS_TEST
{
    public class Character_Inventory : MonoBehaviour
    {
        public static Action<CharacterCustomizationPiece> ReturningPiece; // Action event for returning a customization piece
        public static Action<CharacterCustomizationPiece> BuyingPiece; // Action event for returning a customization piece

        [SerializeField] private int money;// Player's current money
        [SerializeField] private int moneyCounter;// Total cost of items in the shopping list
        private List<CharacterCustomizationPiece> customizationPieces = new List<CharacterCustomizationPiece>();// List of owned customization pieces
        private List<CharacterCustomizationPiece> shoppingList = new List<CharacterCustomizationPiece>();// List of pieces the player intends to buy

        /// <summary>
        /// Gets the player's current money.
        /// </summary>
        public int Money
        {
            get { return money; }
        }

        /// <summary>
        /// Gets the total cost of items in the shopping list.
        /// </summary>
        public int ShoppingListPrice
        {
            get { return moneyCounter; }
        }

        /// <summary>
        /// Gets the list of items in the shopping list.
        /// </summary>
        public List<CharacterCustomizationPiece> ShoppingList
        {
            get { return shoppingList; }
        }

        /// <summary>
        /// Gets the list of owned customization pieces.
        /// </summary>
        public List<CharacterCustomizationPiece> InventoryList
        {
            get { return customizationPieces; }
        }

        /// <summary>
        /// Adds a customization piece to the shopping list and updates the UI.
        /// </summary>
        /// <param name="piece">The customization piece to add.</param>
        public void AddPiece(CharacterCustomizationPiece piece)
        {
            shoppingList.Add(piece);
            moneyCounter += piece.Price;

            // Update UI display counter
            UI_Manager.Instance.UpdateDisplayCounter(shoppingList.Count, moneyCounter > money);
        }

        /// <summary>
        /// Removes a customization piece from the shopping list and updates the UI.
        /// </summary>
        /// <param name="piece">The customization piece to remove.</param>
        public void RemovePiece(CharacterCustomizationPiece piece) 
        {
            shoppingList.Remove(piece);
            moneyCounter -= piece.Price;

            // Update UI display counter
            UI_Manager.Instance.UpdateDisplayCounter(shoppingList.Count, moneyCounter > money);
        }

        /// <summary>
        /// Gets a customization piece from the shopping list by its type.
        /// </summary>
        /// <param name="type">The type of customization piece to find.</param>
        /// <returns>The found customization piece, or null if not found.</returns>
        public CharacterCustomizationPiece GetPieceByType(CharacterCustomizationPiece.Type type)
        {
            return shoppingList.FirstOrDefault(piece => piece.PieceType == type);
        }

        /// <summary>
        /// Purchases all items in the shopping list, adds them to the inventory, and updates the UI.
        /// </summary>
        public void BuyShoppingList()
        {
            money -= moneyCounter;

            foreach (var piece in shoppingList)
            {
                customizationPieces.Add(piece);
                BuyingPiece?.Invoke(piece);
            }

            shoppingList.Clear();
            moneyCounter = 0;

            // Update UI display counter
            UI_Manager.Instance.UpdateDisplayCounter(shoppingList.Count, moneyCounter > money);
        }
    }
}
