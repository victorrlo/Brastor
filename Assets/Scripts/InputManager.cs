using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Brastor { 
    public class InputManager : MonoBehaviour
    {
        public static InputManager _instance;
        public PlayerManager _player;
        PlayerControls _playerControls;

        [Header("Camera Movement Input")]
        [SerializeField] private Vector2 _cameraInput;
        public float _cameraVerticalInput;
        public float _cameraHorizontalInput;

        [Header("Player Movement Input")]
        [SerializeField] private Vector2 _movementInput;
        public float _verticalInput;
        public float _horizontalInput;
        public float _moveAmount;

        [Header("PLAYER ACTION INPUT")]
        [SerializeField] bool _dodgeInput = false;

        

        private void Awake()
        {
            if (_instance == null) _instance = this;
            else Destroy(gameObject);

            _playerControls = new PlayerControls();
            _player = GetComponent<PlayerManager>();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            //when the scene changes, run this logic
            SceneManager.activeSceneChanged += OnSceneChange;

            _instance.enabled = false;
        }

        private void Update()
        {
            HandleAllInput();
        }

        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            //if we are loading into our world scene, enable our player controls
            if(newScene.buildIndex == SaveManager._instance.GetWorldSceneIndex())
            {
                _instance.enabled = true;
            }
            //otherwise we must be at the menu scene, disable our player controls
            //this is so our player cant move around if we enter things like a character creation menu etc
            else
            {
                _instance.enabled = false;
            }
        }

        // DISABLE INPUT ADJUSTS IF FOCUS IS NOT ON THE GAME ANYMORE
        private void OnApplicationFocus(bool hasFocus)
        {
            if (_playerControls == null)
            {
                Debug.LogError("Player Controls has not been initialized!");
                return;
            }
            if (hasFocus)
            {
                EnableInput();
            }
            else if (!hasFocus)
            {
                DisableInput();
            }
        }

        // TO BE ABLE TO HANDLE INPUT ENABLING AND DISABLING IN OTHER PARTS OF THE CODE (FOR EXAMPLE DURING MENU OPENING)
        private void EnableInput()
        {
            if (_playerControls != null)
            {
                _playerControls.Enable();
            }
        }

        private void DisableInput()
        {
            if (_playerControls != null)
            {
                _playerControls.Disable();
            }
        }


        private void OnEnable()
        {
            if (_playerControls == null) _playerControls = new PlayerControls();

            _playerControls.PlayerMovement.Movement.performed += i => _movementInput = i.ReadValue<Vector2>();
            _playerControls.PlayerCamera.Movement.performed += i => _cameraInput = i.ReadValue<Vector2>();
            _playerControls.PlayerAction.Dodge.performed += i => _dodgeInput = true;

            _playerControls.Enable();
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        private void HandleAllInput()
        {
            HandleCameraMovementInput();
            HandlePlayerMovementInput();
            HandleDodgeInput();
        }

        // MOVEMENT

        private void HandlePlayerMovementInput()
        {
            _verticalInput = _movementInput.y;
            _horizontalInput = _movementInput.x;

            _moveAmount = Mathf.Clamp01(Mathf.Abs(_verticalInput) + Mathf.Abs(_horizontalInput));


            //clamp the values, so it's either 0, 0.5 or 1
            if (_moveAmount <= 0.5 && _moveAmount < 0)
            {
                _moveAmount = 0.5f; //slow walking
            }
            else if (_moveAmount > 0.5 && _moveAmount <= 1)
            {
                _moveAmount = 1; //running
            }
            //in the future there will be sprinting too

            // WHY DO WE PASS 0 ON THE HORIZONTAL? BECAUSE WE ONLY WANT NON-STRAFING MOVEMENT
            // WE USE THE HORIZONTAL WHEN WE ARE STRAFING OR LOCKED ON

            if (_player == null)
            {
                return;
            }

            // IF NOT LOCKED ON, USE ONLY MOVE AMOUNT
            _player._playerAnimatorManager.UpdateAnimatorMovementParameters(0, _moveAmount);

            // IF WE ARE LOCKED ON, PASS THE HORIZONTAL VALUE
        }

        private void HandleCameraMovementInput()
        {
            _cameraVerticalInput = _cameraInput.y;
            _cameraHorizontalInput = _cameraInput.x;
        }

        // ACTION

        private void HandleDodgeInput()
        {
            if (_dodgeInput == true)
            {
                _dodgeInput = false;
                // FOR THE FUTURE: RETURN IF MENU OR UI WINDOW IS OPEN, TO DO NOTHING WHILE IN THESE STATES

                // PERFORM DODGE
                _player._playerLocomotionManager.AttemptToPerformDodge();
            }
        }
    }
}