using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brastor { 
    public class TitleScreenManager : MonoBehaviour
    {
        [SerializeField] private GameObject _playerObject;
        private static GameObject _instance;

        public void StartNewGame()
        {
            _instance = Instantiate(_playerObject);
            DontDestroyOnLoad( _instance );

            StartCoroutine(SaveManager._instance.LoadNewGame());
        }
    }
}