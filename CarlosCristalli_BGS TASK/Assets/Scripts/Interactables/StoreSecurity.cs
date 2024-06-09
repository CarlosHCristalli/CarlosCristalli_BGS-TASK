using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS_TEST
{
    public class StoreSecurity : MonoBehaviour
    {
        [SerializeField] private Character_Inventory inventory;
        [SerializeField] private GameObject security;

        private void OnEnable()
        {
            // Activate security if there are items in the shopping list
            security.SetActive(inventory.ShoppingList.Count > 0);

            // Show a warning message if the shopping list is not empty
            if (inventory.ShoppingList.Count > 0)
            {
                UI_Manager.Instance.ShowMessage("You are trying to leave without paying!! \n Step back! \n Or we will open fire!");
            }
        }
    }
}