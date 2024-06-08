using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS_TEST
{
    public class Character_InteractionManager : MonoBehaviour
    {
        [SerializeField] Tooltip tooltip;

        Interactable currentInteractable;

        private void OnEnable()
        {
            Character_InputManager.OnInteract += Interact;
        }

        private void OnDisable()
        {
            Character_InputManager.OnInteract -= Interact;
        }

        /// <summary>
        /// Sets the current interactable object.
        /// </summary>
        /// <param name="interactable">The interactable object to set.</param>
        public void SetInteractable(Interactable interactable)
        {
            currentInteractable = interactable;
        }

        /// <summary>
        /// Handles player interaction with the current interactable object.
        /// </summary>
        public void Interact()
        {

            if (currentInteractable != null)
            {
                currentInteractable.Interact(this);
                tooltip.DeactivateTooltip();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Check if the collider belongs to an interactable object
            if (collision.CompareTag("Interactable"))
            {
                // Get the Interactable component attached to the collider
                Interactable interactable = collision.GetComponent<Interactable>();

                // Check if the interactable component is valid
                if (interactable != null)
                {
                    // Set the current interactable object and activate its tooltip
                    currentInteractable = interactable;
                    tooltip.ActivateTooltip(currentInteractable.tooltipDisplay);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // Check if the collider belongs to an interactable object
            if (collision.CompareTag("Interactable"))
            {
                // Get the Interactable component attached to the collider
                Interactable interactable = collision.GetComponent<Interactable>();

                // Check if the interactable component is valid
                if (interactable != null)
                {
                    // Clear the current interactable object and deactivate its tooltip
                    currentInteractable = null;
                    tooltip.DeactivateTooltip();
                }
            }
        }
    }
}