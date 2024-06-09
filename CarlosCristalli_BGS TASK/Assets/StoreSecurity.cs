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
            security.SetActive(inventory.ShoppingList.Count > 0);

            if(inventory.ShoppingList.Count > 0)
            {
                UI_Manager.Instance.ShowMessage("You are tring to leave without paing!! \n Step back! \n Or we will open fire!");
            }
        }
    }
}