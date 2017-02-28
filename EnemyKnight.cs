using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class EnemyKnight : MonoBehaviour {

    CharacterController cc;
    Animator anim;

    public float moveSpeed = 3f;
    public float moveTargetAccuracy = 0.25f;

    public float detectionWidth = 1f;
    public float detectionRange = 10f;
    public float detectionCheckFrequency = 0.5f;
    public float attackRange = 1f;

    private Transform player;
    private bool playerDetected;
    private float timeUntilCheck;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeUntilCheck = detectionCheckFrequency;
    }
	
	// Update is called once per frame
	void Update () {
        timeUntilCheck -= Time.deltaTime;        	
        if (timeUntilCheck <= 0)
        {
            playerDetected = CheckForPlayerDetection();
        }

        if (playerDetected)
        {
            ChasePlayer();
        } else
        {
            Patrol();
        }

    }

    private bool CheckForPlayerDetection()
    {
        //TODO need a smarter detection algorithm which checks line of sight is not blocked (send 2 raycasts to corners of the player xform?)
        //TODO need to make sure the enemy is facing towards the player
        Ray ray = new Ray(transform.position, player.position - transform.position);
        RaycastHit[] hits = Physics.SphereCastAll(ray, detectionWidth, detectionRange, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform == player)
            {
                return true;
            }
        }
        return false;
    }

    private void ChasePlayer()
    {
        var offset = player.position - transform.position;

        if (offset.magnitude > attackRange)
        {
            anim.SetBool("Run", true);
            transform.LookAt(player.position);            
            offset = offset.normalized * moveSpeed;
            cc.Move(offset * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetTrigger("Attack01");
        }
    }

    private void Patrol()
    {
        anim.SetBool("Run", false);
    }
}
