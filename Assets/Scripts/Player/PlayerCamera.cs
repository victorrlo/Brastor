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

        [Header("Camera Settings")]
        private Vector3 cameraVelocity;
        private float cameraSmoothSpeed = 1; //the bigger, the longer for the camera to reach its position during movement

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
                FollowTarget();
                //rotate around the player
                //collide with objects

            }

        }

        private void FollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, _player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPosition;
        }
    }
}

