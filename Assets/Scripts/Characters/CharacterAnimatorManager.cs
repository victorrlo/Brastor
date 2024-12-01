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
    public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue)
    {
        _character._animator.SetFloat("Horizontal", horizontalValue);
        _character._animator.SetFloat("Horizontal", verticalValue);
    }
}
