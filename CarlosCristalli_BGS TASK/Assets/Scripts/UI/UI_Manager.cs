using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace BGS_TEST
{
    public class UI_Manager : MonoBehaviour
    {
        public static UI_Manager Instance;

        [Header("Inventory Add display")]
        [SerializeField] private Image extraCounterDisplay;
        [SerializeField] private TMP_Text extraCounterDisplayText;

        [Header("Confirmation Message")]
        [SerializeField] private GameObject confirmationMessageDisplay;
        [SerializeField] private TMP_Text confirmationMessageDisplayText;
        [SerializeField] private Button confirmationMessageDisplayConfirmationButton;

        [Header("Message")]
        [SerializeField] private GameObject messageDisplay;
        [SerializeField] private TMP_Text messageDisplayText;

        [Header("Inventory display")]
        [SerializeField] private UI_InventoryDisplay inventoryDisplay;
        [SerializeField] private Character_VisualManager characterVisual;
        [SerializeField] private Character_Inventory characterInventory;

        [Header("Inventory Buttons")]
        [SerializeField] private GameObject characterInventoryButton;
        [SerializeField] private GameObject ShoppingListInventoryButton;

        private void OnEnable()
        {
            Interactable_Door.isOutside += HandlleChangeOfScenary;
        }

        private void OnDisable()
        {
            Interactable_Door.isOutside -= HandlleChangeOfScenary;
        }

        private void Awake()
        {
            // Singleton pattern to ensure only one instance of UI_Manager exists
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// Updates the display counter with the number of items and their budget status.
        /// </summary>
        /// <param name="count">Number of items to display.</param>
        /// <param name="overbudget">Indicates if the budget is exceeded.</param>
        public void UpdateDisplayCounter(int count,bool overbudget)
        {
            extraCounterDisplay.gameObject.SetActive(count > 0);

            extraCounterDisplayText.text = count.ToString();

            extraCounterDisplay.color = overbudget ? Color.red : Color.white;
        }

        /// <summary>
        /// Shows a confirmation message with a callback action.
        /// </summary>
        /// <param name="callback">Action to perform on confirmation.</param>
        /// <param name="text">Message text to display.</param>
        public void ShowConfirmationMessage(Action callback, string text)
        {
            confirmationMessageDisplay.SetActive(true);
            confirmationMessageDisplayText.text = text;

            confirmationMessageDisplayConfirmationButton.onClick.RemoveAllListeners();
            confirmationMessageDisplayConfirmationButton.onClick.AddListener(() =>
            {
                callback();
                confirmationMessageDisplay.SetActive(false);
            });
        }

        /// <summary>
        /// Shows a simple message on the screen.
        /// </summary>
        /// <param name="text">Message text to display.</param>
        public void ShowMessage(string text)
        {
            messageDisplay.SetActive(true);
            messageDisplayText.text = text;
        }

        /// <summary>
        /// Opens the inventory display with the given parameters.
        /// </summary>
        /// <param name="title">Title of the inventory display.</param>
        /// <param name="callback">Action to perform on selecting an item.</param>
        /// <param name="toBeDisplayed">List of items to display.</param>
        /// <param name="money">Current money amount.</param>
        /// <param name="shoppingListPrice">Total price of items in the shopping list.</param>
        public void OpenInventoryDisplay(string Title, Action<CharacterCustomizationPiece> callback, List<CharacterCustomizationPiece> toBeDisplayed, int money, int ShoppingListPrice)
        {
            inventoryDisplay.gameObject.SetActive(true);
            inventoryDisplay.Setup(Title, callback, toBeDisplayed, money, ShoppingListPrice);
        }

        public void OpenShoppingList(Character_Inventory inventory)
        {
            OpenInventoryDisplay("Shopping List", AskToRemovePiece, inventory.ShoppingList, inventory.Money, inventory.ShoppingListPrice);
        }

        public void OpenInventory(Character_Inventory inventory)
        {
            OpenInventoryDisplay("Inventory", AskToWearPiece, inventory.InventoryList, inventory.Money, -1);
        }

        private void AskToWearPiece(CharacterCustomizationPiece piece)
        {
            ShowConfirmationMessage(() => { characterVisual.SetCurrentCustomizationPiece(piece); }, $"Change to {piece.name}?");
        }

        private void AskToRemovePiece(CharacterCustomizationPiece piece)
        {
            ShowConfirmationMessage(() =>
            {
                characterInventory.RemovePiece(piece);
                OpenShoppingList(characterInventory);
                Character_Inventory.ReturningPiece?.Invoke(piece);
            }, $"Are you sure you want to return {piece.name}?");
        }

        private void HandlleChangeOfScenary(bool isOutside)
        {
            ShoppingListInventoryButton.SetActive(!isOutside);
        }
    }
}