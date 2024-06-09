using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BGS_TEST
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private RectTransform tooltip;
        [SerializeField] private TMP_Text tooltipTextDisplay;
        [SerializeField] private Transform target;

        private void LateUpdate()
        {
            // Update tooltip position to match the target's position
            if (target != null)
            {
                tooltip.position = target.position;
            }
        }

        /// <summary>
        /// Activates the tooltip and sets the displayed text.
        /// </summary>
        /// <param name="text">The text to display in the tooltip.</param>
        public void ActivateTooltip(string text)
        {
            gameObject.SetActive(true);
            tooltipTextDisplay.text = text;
        }

        /// <summary>
        /// Deactivates the tooltip.
        /// </summary>
        public void DeactivateTooltip()
        {
            gameObject.SetActive(false);
        }
    }
}