using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace BGS_TEST
{
    public class SoundEffects_Manager : MonoBehaviour
    {
        public static SoundEffects_Manager Instance;

        [SerializeField] private AudioMixer masterMixer;

        [SerializeField] private AudioSource selling;
        [SerializeField] private AudioClip sellingSound;

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
        /// Sets the master volume level.
        /// </summary>
        /// <param name="level">Slider controlling the volume level.</param>
        public void SetMasterLevel(Slider level)
        {
            masterMixer.SetFloat("Volume_Master", level.value);
        }

        /// <summary>
        /// Sets the sound effects volume level.
        /// </summary>
        /// <param name="level">Slider controlling the volume level.</param>
        public void SetSoundEffectsLevel(Slider level)
        {
            masterMixer.SetFloat("Volume_SoundEffects", level.value);
        }

        /// <summary>
        /// Sets the music volume level.
        /// </summary>
        /// <param name="level">Slider controlling the volume level.</param>
        public void SetMusicLevel(Slider level)
        {
            masterMixer.SetFloat("Volume_Music", level.value);
        }

        public void PlaySellingSound()
        {
            selling.clip = sellingSound;
            selling.Play();
        }
    }
}