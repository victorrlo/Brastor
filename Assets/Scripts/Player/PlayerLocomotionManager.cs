using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Brastor
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager _player;

        public float _verticalMovement;
        public float _horizontalMovement;
        public float moveAmount;

        private Vector3 _moveDirection;
        private Vector3 _targetRotationDirection;
        [SerializeField] float _walkingSpeed = 2;
        [SerializeField] float _runningSpeed = 5;
        [SerializeField] float _rotationSpeed = 15;

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
    }
}