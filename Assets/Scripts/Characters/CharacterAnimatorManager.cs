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

    public virtual void PlayTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true)
    {
        _character._animator.applyRootMotion = applyRootMotion;
        _character._animator.CrossFade(targetAnimation, 0.2f);
        // USED TO STOP CHARACTER FROM ATTEMPTING NEW ACTIONS
        // EXAMPLE: GET DAMAGED, BEGIN PERFORMING A DAMAGE ANIMATION
        // FLAG isPerformingAction TURNS TRUE IF CHARACTER IS STUNNED
        // THEN CAN BE CHECKED BEFORE ATTEMPTING NEW ACTIONS
        _character.isPerformingAction = isPerformingAction;
    }
}
