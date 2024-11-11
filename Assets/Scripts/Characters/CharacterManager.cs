using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brastor { 
    public class CharacterManager : MonoBehaviour
    {
        public CharacterController _characterController;
        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);

            _characterController = GetComponent<CharacterController>();
        }

        protected virtual void Update()
        {

        }

        protected virtual void LateUpdate()
        {
            
        }
    }

}