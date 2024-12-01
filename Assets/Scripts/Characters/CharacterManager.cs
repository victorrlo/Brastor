using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brastor { 
    public class CharacterManager : MonoBehaviour
    {
        public CharacterController _characterController;
        [HideInInspector] public Animator _animator;
        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);
            _animator = GetComponent<Animator>();
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