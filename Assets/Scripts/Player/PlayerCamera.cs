using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Brastor
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera _instance;
        public Camera _camera;
        public PlayerManager _player;
        [SerializeField] Transform _cameraPivotTransform;

        [Header("Camera Settings")]
        private float cameraSmoothSpeed = 1; //the bigger, the longer for the camera to reach its position during movement
        [SerializeField] float _leftAndRightRotationSpeed = 220;
        [SerializeField] float _upAndDownRotationSpeed = 220;
        [SerializeField] float _minimumPivot = -30; //lowest point player can look down
        [SerializeField] float _maximumPivot = 60; //highest point player can look up
        [SerializeField] float _cameraCollisionRadius = 0.2f;



        [Header("Camera Values")]
        private Vector3 _cameraVelocity;
        private Vector3 _cameraObjectPosition; // used for camera colisions (moves camera to this position when colliding)
        [SerializeField] float _leftAndRightLookAngle;
        [SerializeField] float _upAndDownLookAngle;
        private float _cameraZPosition;
        private float _targetCameraZPosition;
        [SerializeField] LayerMask _collideWithLayers;

        private void Awake()
        {
            _player = GetComponent<PlayerManager>();
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            _cameraZPosition = _camera.transform.localPosition.z;
        }

        public void HandleAllCameraActions()
        {
            if (_player != null)
            {
                HandleFollowTarget();

                //rotate around the player
                HandleRotation();

                //collide with objects
                HandleColisions();
            }

        }

        private void HandleFollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, _player.transform.position, ref _cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPosition;
        }

        private void HandleRotation()
        {
            //if locked on, force rotation towards target
            //else rotate normally

            //normal rotations
            //rotate left and right based on horizontal movement on the right joystick
            _leftAndRightLookAngle += (InputManager._instance._cameraHorizontalInput * _leftAndRightRotationSpeed) * Time.deltaTime;
            //rotate up and down based on vertical movement on the right joystick
            _upAndDownLookAngle -= (InputManager._instance._cameraVerticalInput *_upAndDownRotationSpeed) * Time.deltaTime;
            //clamp up the up and down look angle between a min and max value
            _upAndDownLookAngle = Mathf.Clamp(_upAndDownLookAngle, _minimumPivot, _maximumPivot);


            Vector3 cameraRotation = Vector3.zero;
            Quaternion targetRotation;

            //rotates this gameobject left and right
            cameraRotation.y = _leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;

            //rotates this gameobject up and down
            cameraRotation.x = _upAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            _cameraPivotTransform.localRotation = targetRotation;

        }

        private void HandleColisions()
        {
            _targetCameraZPosition = _cameraZPosition;
            RaycastHit hit;
            // DIRECTION FOR COLLISION CHECK
            Vector3 direction = _camera.transform.position - _cameraPivotTransform.position;
            direction.Normalize();

            // CHECK IF THERE'S AN OBJECT IN FRONT OF DESIRED DIRECTION
            if (Physics.SphereCast(_cameraPivotTransform.position, _cameraCollisionRadius, direction, out hit, Mathf.Abs(_targetCameraZPosition), _collideWithLayers))
            {
                //  IF THERE IS, WE GET OUR DISTANCE FROM IT
                float distanceFromHitObject = Vector3.Distance(_cameraPivotTransform.position, hit.point);

                // THEN EQUATE OUR TARGET Z POSITION TO THE FOLLOWING
                _targetCameraZPosition = -(distanceFromHitObject - _cameraCollisionRadius);
            }

            // IF OUR TARGET POSITION IS LESS THAN THE RADIUS WE DEFINED, WE SUBTRACT OUR COLLISION RADIUS (MAKING IT SNAP BACK)
            if (Mathf.Abs(_targetCameraZPosition) < _cameraCollisionRadius)
            {
                _targetCameraZPosition = -_cameraCollisionRadius;
            }
             
            // WE THEN APPLY OUR FINAL POSITION USING A LERP OVER A TIME OF 0.2F
            _cameraObjectPosition.z = Mathf.Lerp(_camera.transform.localPosition.z, _targetCameraZPosition, 0.2f);
            _camera.transform.localPosition = _cameraObjectPosition;
        }
    }
}

