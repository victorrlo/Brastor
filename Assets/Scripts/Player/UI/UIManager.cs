using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brastor { 
    public class UIManager : MonoBehaviour
    {

        public static UIManager _instance;
        private void Awake()
        {
            if (_instance == null) _instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
