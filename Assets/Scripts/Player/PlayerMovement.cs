using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

namespace Marsminerwa
{

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Shooter))]
    public class PlayerMovement : MonoBehaviour, DefaultInputActions.IPlayerActions
    {
        private static PlayerMovement current;
        public static GameObject PlayerObject => current ? current.gameObject : null;
        public static Vector2 PlayerPosition => current ? current.transform.position : Vector2.zero;
        
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

        [SerializeField]
        private LayerMask interactionMask;

        [SerializeField]
        private GameObject deathScreen;
        
        private Rigidbody2D rb;
        private Shooter shooter;
        
        private bool isShooting;
        private Vector2 inputMovement;
        private Vector2 lookingDirection;
        private float trueLookDistance;
        private DefaultInputActions inputActions;
        private Animator animator;
        private Health health;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            shooter = GetComponent<Shooter>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
            current = this;
        }

        private void OnEnable()
        {
            if (inputActions is null)
            {
                inputActions = new DefaultInputActions();
                inputActions.Player.SetCallbacks(this);
            }

            inputActions.Enable();
            health.OnDeath += OnDeath;
            health.OnHealthChange += OnHealthChange;
        }

        private void OnDisable()
        {
            inputActions.Disable();
            health.OnDeath -= OnDeath;
            health.OnHealthChange -= OnHealthChange;
        }

        private void OnDeath(Health _, int __)
        {
            GameManager.PlayPlayerDeathSound();
            
            deathScreen.SetActive(true);
            Time.timeScale = 0.25f;
            // enabled = false;
        }

        private void OnHealthChange(Health source, int previous)
        {
            if (source.CurrentHealth < previous)
            {
                animator.SetTrigger(AnimatorProps.Damage);
            }
        }

        private void Update()
        {
            ProcessLooking();
            ProcessShooting();
            ProcessInteractions();
            ProcessAnimations();
        }

        private void ProcessAnimations()
        {
            animator.SetFloat(AnimatorProps.Velocity, rb.velocity.magnitude);
        }

        private void ProcessShooting()
        {
            if (isShooting) Shoot();
        }

        private void ProcessLooking()
        {
            if (lookingDirection != Vector2.zero)
            {
                Vector3 toolPosition = lookingDirection * playerToolOffset;
                toolPosition.z = lookingDirection.y < 0 ? -0.1f : 0.1f;
                playerTool.localPosition = toolPosition;
                float cameraOffset = Mathf.Min(trueLookDistance, cameraTargetOffset);
                cameraTarget.localPosition = lookingDirection * cameraOffset;
                playerTool.up = lookingDirection;

            }
        }

        private void ProcessInteractions()
        {
            if (inputMovement != Vector2.zero)
            {
                ContactFilter2D filter2D = new ContactFilter2D();
                filter2D.layerMask = interactionMask;
                filter2D.useTriggers = false;
                List<RaycastHit2D> hits = new();
                int hitCount = Physics2D.Raycast(transform.position, inputMovement, filter2D, hits, 0.6f);
                for (int i = 0; i < hitCount ; i++)
                {
                    RaycastHit2D hit = hits[i];
                    Interactable interacable = hit.collider.GetComponent<Interactable>();
                    if (interacable != null)
                    {
                        Direction dir = DirectionExtension.FromVector(inputMovement);
                        interacable.Interact(dir);
                        break;
                    }
                }
            }
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

        public void OnExit(InputAction.CallbackContext context)
        {
            SceneManager.LoadScene("Scenes/Menu");
            GamePrefs.SetScore(Score.score);
        }
    }
}