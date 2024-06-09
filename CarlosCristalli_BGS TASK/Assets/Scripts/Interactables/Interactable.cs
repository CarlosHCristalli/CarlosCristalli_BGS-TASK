using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS_TEST
{
    public class Interactable : MonoBehaviour
    {
        // Tooltip text to display when the player gets close to the interactable object
        public string tooltipDisplay;

        //Sound effects source
        [SerializeField] protected AudioSource soundEffectPlayer;

        /// <summary>
        /// Virtual method to handle interaction logic. Can be overridden by derived classes.
        /// </summary>
        /// <param name="character">The character interacting with the object.</param>
        public virtual void Interact(Character_InteractionManager character)
        {

        }
    }
}