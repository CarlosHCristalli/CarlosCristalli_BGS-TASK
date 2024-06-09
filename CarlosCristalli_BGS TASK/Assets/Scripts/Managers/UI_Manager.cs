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

        [Header("Counter")]
        [SerializeField] private GameObject CounterOptions;

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
            Interactable_Door.isOutside += HandleChangeOfScenery;
        }

        private void OnDisable()
        {
            Interactable_Door.isOutside -= HandleChangeOfScenery;
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
                Destroy(gameObject);
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
        public void OpenInventoryDisplay(string Title, Action<CharacterCustomizationPiece,bool> callback, List<CharacterCustomizationPiece> toBeDisplayed, int money, int ShoppingListPrice)
        {
            inventoryDisplay.gameObject.SetActive(true);
            inventoryDisplay.Setup(Title, callback, toBeDisplayed, money, ShoppingListPrice);
        }

        public void OpenShoppingList(Character_Inventory inventory)
        {
            OpenInventoryDisplay("Shopping Cart", AskToRemovePiece, inventory.ShoppingList, inventory.Money, inventory.ShoppingListPrice);
        }

        public void OpenInventory(Character_Inventory inventory)
        {
            OpenInventoryDisplay("Inventory", AskToWearPiece, inventory.InventoryList, inventory.Money, -1);
        }

        public void OpenCounterOptions()
        {
            CounterOptions.SetActive(true);
        }

        private void AskToWearPiece(CharacterCustomizationPiece piece, bool isEquipped)
        {
            if (isEquipped)
            {
                ShowConfirmationMessage(() =>
                {
                    characterVisual.HidePartType(piece.PieceType, true);
                    OpenInventory(characterInventory);
                }, $"Change out of {piece.name}?");
            }
            else
            {
                ShowConfirmationMessage(() =>
                {
                    characterVisual.HidePartType(piece.PieceType, false);
                    characterVisual.SetCurrentCustomizationPiece(piece);
                    OpenInventory(characterInventory);
                }, $"Change to {piece.name}?");
            }
        }

        private void AskToRemovePiece(CharacterCustomizationPiece piece, bool isEquipped)
        {
            ShowConfirmationMessage(() =>
            {
                characterInventory.RemovePiece(piece);
                OpenShoppingList(characterInventory);
                Character_Inventory.ReturningPiece?.Invoke(piece);
            }, $"Are you sure you want to return {piece.name}?" + (isEquipped ? "\nIt is currently equipped" : string.Empty));
        }

        private void HandleChangeOfScenery(bool isOutside)
        {
            ShoppingListInventoryButton.SetActive(!isOutside);
        }
    }
}