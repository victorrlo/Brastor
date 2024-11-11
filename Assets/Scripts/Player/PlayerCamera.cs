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
        [SerializeField] float leftAndRightRotationSpeed = 220;
        [SerializeField] float upAndDownRotationSpeed = 220;
        [SerializeField] float minimumPivot = -30; //lowest point player can look down
        [SerializeField] float maximumPivot = 60; //highest point player can look up



        [Header("Camera Values")]
        private Vector3 cameraVelocity;
        [SerializeField] float leftAndRightLookAngle;
        [SerializeField] float upAndDownLookAngle;

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
        }

        public void HandleAllCameraActions()
        {
            if (_player != null)
            {
                HandleFollowTarget();

                //rotate around the player
                HandleRotation();
                
                //collide with objects

            }

        }

        private void HandleFollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, _player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPosition;
        }

        private void HandleRotation()
        {
            //if locked on, force rotation towards target
            //else rotate normally

            //normal rotations
            //rotate left and right based on horizontal movement on the right joystick
            leftAndRightLookAngle += (InputManager._instance._cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
            //rotate up and down based on vertical movement on the right joystick
            upAndDownLookAngle -= (InputManager._instance._cameraVerticalInput *upAndDownRotationSpeed) * Time.deltaTime;
            //clamp up the up and down look angle between a min and max value
            upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);


            Vector3 cameraRotation = Vector3.zero;
            Quaternion targetRotation;

            //rotates this gameobject left and right
            cameraRotation.y = leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;

            //rotates this gameobject up and down
            cameraRotation.x = upAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            _cameraPivotTransform.localRotation = targetRotation;

        }
    }
}

