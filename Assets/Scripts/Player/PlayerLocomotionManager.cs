using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Brastor
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager _player;


        [HideInInspector] public float _verticalMovement;
        [HideInInspector] public float _horizontalMovement;
        [HideInInspector] public float moveAmount;

        [Header("Movement Settings")]
        private Vector3 _moveDirection;
        private Vector3 _targetRotationDirection;
        [SerializeField] float _walkingSpeed = 2;
        [SerializeField] float _runningSpeed = 5;
        [SerializeField] float _rotationSpeed = 15;

        [Header("Dodge")]
        private Vector3 rollDirection;

        protected override void Awake()
        {
            base.Awake();
            _player = GetComponent<PlayerManager>();
        }

        public void HandleAllMovement()
        {
            //grounded movement
            HandleGroundedMovement();
            //rotation movement
            HandleRotation();
            //aerial movement
            //jumping movement
            //falling
        }


        private void GetVerticalAndHorizontalInputs()
        {
            _verticalMovement = InputManager._instance._verticalInput;
            _horizontalMovement = InputManager._instance._horizontalInput;
        }

        private void HandleGroundedMovement()
        {
            GetVerticalAndHorizontalInputs();

            _moveDirection =  PlayerCamera._instance._camera.transform.forward * _verticalMovement;
            _moveDirection = _moveDirection + PlayerCamera._instance._camera.transform.right * _horizontalMovement;
            _moveDirection.Normalize();
            _moveDirection.y = 0;

            if (InputManager._instance._moveAmount > 0.5f)
            {
                // running speed
                _player._characterController.Move(_moveDirection * _runningSpeed * Time.deltaTime);
            }
            else if (InputManager._instance._moveAmount <= 0.5f)
            {
                // walking speed
                _player._characterController.Move(_moveDirection * _walkingSpeed * Time.deltaTime);
            }
        }

        private void HandleRotation()
        {
            _targetRotationDirection = Vector3.zero;
            _targetRotationDirection = PlayerCamera._instance._camera.transform.forward * _verticalMovement;
            _targetRotationDirection = _targetRotationDirection + PlayerCamera._instance._camera.transform.right * _horizontalMovement;
            _targetRotationDirection.Normalize();
            _targetRotationDirection.y = 0;

            if (_targetRotationDirection == Vector3.zero)
            {
                _targetRotationDirection = transform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(_targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }

        public void AttemptToPerformDodge()
        {
            if(_player.isPerformingAction)
            {
                return;
            }
            // WHILE MOVING, PERFORM A ROLL
            if(moveAmount > 0)
            {
                rollDirection = PlayerCamera._instance._camera.transform.forward * _verticalMovement;
                rollDirection = PlayerCamera._instance._camera.transform.right * _horizontalMovement;

                rollDirection.y = 0;
                rollDirection.Normalize();

                Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
                _player.transform.rotation = playerRotation;

                // PERFORM A ROLL ANIMATION
                _player._playerAnimatorManager.PlayTargetActionAnimation("Roll_Forward_01", true, true);
                Debug.LogWarning("DO A BARREL ROLL!");
            }
            // IF NOT MOVING, PERFORM A BACKSTEP
            else
            {
                // PERFORM A BACKSTEP ANIMATION
            }
            
        }
    }
}