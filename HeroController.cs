using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class HeroController : MonoBehaviour {

    public float moveSpeed = 4f;
    public float moveTargetAccuracy = 0.25f; //How close the hero must get to a target before they stop moving

    public float lookSpeed = 0.5f; //Time taken for the hero to orient themselves towards a new target

    private CharacterController cc;
    private Animator anim;

    private bool hasTarget;
    private Vector3 target;

    void Start () {        
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }
	
	void Update () { 
        if (hasTarget)
        {
            MoveToTarget();
        }                 
    }

    public void MoveToTarget(Vector3 target)
    {
        ClearAnimationState();
        anim.SetBool("Run", true);

        hasTarget = true;
        this.target = target;
        LookAt(target, lookSpeed); 
    }

    public void CancelMoveToTarget()
    {
        hasTarget = false;
        ClearAnimationState();
    }

    public void LookAt(Vector3 target, float lookSpeed)
    {
        transform.DOLookAt(target, lookSpeed);
    }

    public void StartSwipeAttack()
    {
        ClearAnimationState();
        anim.SetTrigger("Attack02");
    }

    private void MoveToTarget()
    {
        Vector3 targetDirection = target - transform.position;
        //If we are further away from the target than moveTargetAccuracy then keep on moving towards it.
        if (targetDirection.magnitude > moveTargetAccuracy)
        {
            cc.Move(targetDirection.normalized * moveSpeed * Time.deltaTime);
        }
        else
        {
            CancelMoveToTarget();
        }
    }

    public void ClearAnimationState()
    {
        anim.SetBool("Run", false);
        anim.ResetTrigger("Attack02");
    }
}
