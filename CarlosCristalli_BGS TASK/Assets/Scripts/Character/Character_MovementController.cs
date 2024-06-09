using UnityEngine;

namespace BGS_TEST
{
    public class Character_MovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private AudioSource audioSource;
        [Header("Reference")]
        [SerializeField] Character_VisualManager visualManager;
        [SerializeField] private Character_InputManager inputManager;

        [SerializeField] private float movementSpeed;
        [SerializeField] private float sprintSpeedMultiplier;
        [SerializeField] private Vector2 collisionOffset;
        [SerializeField] private LayerMask obstacleLayer; // Layer mask to specify which layers are considered obstacles

        private float movAngle = 0;

        // Constants for direction angles
        private const float UpAngle = 180;
        private const float DownAngle = 0;
        private const float LeftAngle = 270;
        private const float RightAngle = 90;
        private const float IdleAngle = -1;

        // Update is called once per frame
        void Update()
        {
            // Determine movement angle based on input direction
            if (inputManager.MoveDirection == Vector2.zero)
            {
                movAngle = IdleAngle;
            }
            else if (inputManager.MoveDirection == Vector2.up)
            {
                movAngle = UpAngle;
            }
            else if(inputManager.MoveDirection == Vector2.down)
            {
                movAngle = DownAngle;
            }
            else if (inputManager.MoveDirection == Vector2.left)
            {
                movAngle = LeftAngle;
            }
            else if (inputManager.MoveDirection == Vector2.right)
            {
                movAngle = RightAngle;
            }

            // Update animator parameters
            visualManager.UpdateAnimators(inputManager.IsSprinting, movAngle);

            //Handle Sound
            if (movAngle != IdleAngle && !audioSource.isPlaying)
            {
                audioSource.Play();
                //Change the pitch to differentiate from walking to Sprinting
                audioSource.pitch = (inputManager.IsSprinting) ? 0.8f * sprintSpeedMultiplier : 0.8f;
            }
        }

        private void FixedUpdate()
        {
            // Determine current movement speed
            float currentMovementSpeed = (inputManager.IsSprinting) ? sprintSpeedMultiplier * movementSpeed : movementSpeed;

            // Check for obstacles in the direction of movement
            Vector2 direction = inputManager.MoveDirection.normalized;

            if (CanMove(direction))
            {
                // Apply movement to the Rigidbody
                rb.velocity = direction * currentMovementSpeed;
            }
            else
            {
                // Stop movement if an obstacle is detected in the direction of movement
                rb.velocity = Vector2.zero;
            }
        }

        /// <summary>
        /// Checks if the character can move in the specified direction without colliding with obstacles.
        /// </summary>
        /// <param name="direction">The direction in which the character intends to move.</param>
        /// <returns>True if the character can move in the specified direction, otherwise false.</returns>
        private bool CanMove(Vector2 direction)
        {
            Physics2D.queriesHitTriggers = false;
            // Perform a raycast to detect obstacles in the direction of movement
            RaycastHit2D hit = Physics2D.Raycast(rb.position + collisionOffset, direction, 0.03f, obstacleLayer);

            return hit.collider == null;
        }

        public void StopMoving()
        {
            rb.velocity = Vector2.zero;
        }
    }
}