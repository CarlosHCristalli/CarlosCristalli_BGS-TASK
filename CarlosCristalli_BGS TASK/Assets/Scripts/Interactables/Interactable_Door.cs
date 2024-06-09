using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS_TEST
{
    public class Interactable_Door : Interactable
    {
        public static Action<bool> isOutside;

        [SerializeField] private GameObject currentAmbiant;
        [SerializeField] private GameObject nextAmbiant;
        [SerializeField] private Interactable_Door doorOnTheOtherSide;

        [SerializeField] private bool isOutdoor;

        // Override the Interact method from the base class to handle door interactions
        public override void Interact(Character_InteractionManager character)
        {
            base.Interact(character);

            currentAmbiant.SetActive(false);
            nextAmbiant.SetActive(true);

            Vector3 targetPosition = doorOnTheOtherSide.transform.position;
            targetPosition.z = character.transform.position.z;

            character.transform.position = targetPosition;

            isOutside?.Invoke(!isOutdoor);
        }
    }
}