using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerManager : CharacterManager
{

    [SerializeField] private GameObject _playerPrefab;

    PlayerLocomotionManager _playerLocomotionManager;

    protected override void Awake()
    {
        base.Awake();

        _playerLocomotionManager = _playerPrefab.GetComponent<PlayerLocomotionManager>();
    }

    protected override void Update()
    {
        base.Update();

        _playerLocomotionManager.HandleAllMovement();
    }
}
