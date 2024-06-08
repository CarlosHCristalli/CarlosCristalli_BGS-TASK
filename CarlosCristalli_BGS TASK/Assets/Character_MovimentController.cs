using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS_TEST
{
    public class Character_MovimentController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Animator animator;
        [SerializeField] private Character_InputManager inputManager;

        [SerializeField] private float movementSpeed;
        [SerializeField] private float sprintSpeedMultiplier;

        private float movAngle = 0;

        // Constants for direction angles
        private const float UpAngle = 0;
        private const float DownAngle = 180;
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
            animator.SetBool("IsSprinting", inputManager.IsSprinting);
            animator.SetFloat("MovementAngle", movAngle);           
        }

        private void FixedUpdate()
        {
            // Determine current movement speed
            float currentMovimentSpeed = (inputManager.IsSprinting) ? sprintSpeedMultiplier * movementSpeed : movementSpeed;

            // Apply movement to the Rigidbody
            rb.velocity = inputManager.MoveDirection * currentMovimentSpeed;
        }
    }
}