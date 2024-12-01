using Brastor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    CharacterManager _character;

    protected virtual void Awake()
    {
         _character = GetComponent<CharacterManager>();
    }
    public void UpdateAnimatorMovementParameters(float horizontalMovement, float verticalMovement)
    {
        _character._animator.SetFloat("Horizontal", horizontalMovement, 0.1f, Time.deltaTime);
        _character._animator.SetFloat("Horizontal", verticalMovement, 0.1f, Time.deltaTime);
    }
}
