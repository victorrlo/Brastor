using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerManager : CharacterManager
{

    [SerializeField] private GameObject _playerPrefab
        ;
    PlayerLocomotionManager _playerLocomotionManager;

    protected override void Awake()
    {
        base.Awake();

        _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
    }

    protected override void Update()
    {
        _playerLocomotionManager.HandleAllMovement();
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(_playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
