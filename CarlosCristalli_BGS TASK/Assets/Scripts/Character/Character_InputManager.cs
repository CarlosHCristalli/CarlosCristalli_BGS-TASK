using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BGS_TEST
{
    public class Character_InputManager : MonoBehaviour
    {
        public delegate void InteractionEvent();
        public static event InteractionEvent OnInteract;

        // PlayerInputActions instance to handle input actions
        [SerializeField] private PlayerInputActions playerInputActions;

        // Property to store and provide the movement direction
        public Vector2 MoveDirection{ get; private set; }

        // Property to indicate whether the player is sprinting
        public bool IsSprinting { get; private set; }

        // Input actions for move, interact, and sprint
        private InputAction move;
        private InputAction interact;
        private InputAction sprint;

        // Initialize the PlayerInputActions instance
        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
        }

        // Enable input actions and subscribe to events
        private void OnEnable()
        {
            // Retrieve input actions from the PlayerInputActions instance
            move = playerInputActions.Player.Move;
            interact = playerInputActions.Player.Interact;
            sprint = playerInputActions.Player.Sprint;

            // Enable input actions
            move.Enable();
            interact.Enable();
            sprint.Enable();

            // Subscribe to performed and canceled events for input actions
            interact.performed += OnInteractInput;
            sprint.performed += OnSprintInput;
            sprint.canceled += OnSprintInput;
        }

        // Disable input actions and unsubscribe from events
        private void OnDisable()
        {
            // Disable input actions
            move.Disable();
            interact.Disable();
            sprint.Disable();

            // Unsubscribe from events to avoid memory leaks
            interact.performed -= OnInteractInput;
            sprint.performed -= OnSprintInput;
            sprint.canceled -= OnSprintInput;
        }

        void Start()
        {
            // Initialize MoveDirection to zero
            MoveDirection = Vector2.zero;
        }

        void Update()
        {
            // Update MoveDirection based on input value
            MoveDirection = move.ReadValue<Vector2>();
        }

        // Handle the Interact action event
        private void OnInteractInput( InputAction.CallbackContext context)
        {
            //Add interact logic
            Debug.Log("OnInteractInput");
            OnInteract?.Invoke();
        }

        // Handle the Sprint action event
        private void OnSprintInput(InputAction.CallbackContext context)
        {
            // Toggle sprint state
            IsSprinting = !IsSprinting;
        }
    }
}