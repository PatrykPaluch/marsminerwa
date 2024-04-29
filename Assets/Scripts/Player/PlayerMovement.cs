using UnityEngine;
using UnityEngine.InputSystem;

namespace Marsminerwa
{

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Shooter))]
    public class PlayerMovement : MonoBehaviour, DefaultInputActions.IPlayerActions
    {
        [SerializeField]
        private float speed = 5f;

        [SerializeField]
        private Transform playerTool;
        
        [SerializeField]
        private Transform cameraTarget;

        [SerializeField]
        private float playerToolOffset = 0.5f;
        [SerializeField]
        private float cameraTargetOffset = 5f;

        private Rigidbody2D rb;
        private Shooter shooter;
        
        private bool isShooting;
        private Vector2 inputMovement;
        private Vector2 lookingDirection;
        private float trueLookDistance;
        private DefaultInputActions inputActions;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            shooter = GetComponent<Shooter>();
        }

        private void OnEnable()
        {
            if (inputActions is null)
            {
                inputActions = new DefaultInputActions();
                inputActions.Player.SetCallbacks(this);
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        private void Update()
        {
            if (lookingDirection != Vector2.zero)
            {
                playerTool.localPosition = lookingDirection * playerToolOffset;
                float cameraOffset = Mathf.Min(trueLookDistance, cameraTargetOffset);
                cameraTarget.localPosition = lookingDirection * cameraOffset;
            }
            if (isShooting) Shoot();
        }

        void FixedUpdate()
        {
            rb.velocity = inputMovement * speed;
        }

        private void Shoot()
        {
            shooter.Shoot(transform.position, lookingDirection);
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            inputMovement = context.ReadValue<Vector2>();
        }

        public void OnLooking(InputAction.CallbackContext context)
        {
            Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            Vector2 playerPosition = transform.position; 
            Vector2 difference = (mousePosition - playerPosition);
            trueLookDistance = difference.magnitude;
            lookingDirection = difference.normalized;
        }
        
        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed) isShooting = true;
            if (context.canceled) isShooting = false;
        }
    }
}