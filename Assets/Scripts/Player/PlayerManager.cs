using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Brastor
{
    public class PlayerManager : CharacterManager
    {

        PlayerLocomotionManager _playerLocomotionManager;

        protected override void Awake()
        {
            base.Awake();

            //do more stuff, only for the player
            _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
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
