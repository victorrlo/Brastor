using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private GameObject _playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(_playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
