using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Brastor
{
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public PlayerAnimatorManager _playerAnimatorManager;
        [HideInInspector] public PlayerLocomotionManager _playerLocomotionManager;

        protected override void Awake()
        {
            base.Awake();

            //do more stuff, only for the player
            _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            InputManager._instance._player = this;
        }

        private void Start()
        {
            PlayerCamera._instance._player = this; //assign camera to player
        }

        protected override void Update()
        {
            base.Update();
            PlayerCamera._instance._player = this;

            _playerLocomotionManager.HandleAllMovement();
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            PlayerCamera._instance.HandleAllCameraActions();
        }
    }
}
