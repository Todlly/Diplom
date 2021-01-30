using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMoving : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator Animator;
    private NavMeshAgent Navigator;
    private GameObject Player;

    [SerializeField]
    private float AttackRange = 3;

    [SerializeField]
    private float NoticeRange = 20;
    void Start()
    {
        Animator = GetComponent<Animator>();
        Navigator = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void FixedUpdate()
    {
        Animator.SetFloat("Speed", Navigator.velocity.magnitude / Navigator.speed);
        float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
        if (distanceToPlayer <= NoticeRange)
        {
            if (distanceToPlayer > AttackRange)
            {
                Navigator.SetDestination(Player.transform.position);
                Navigator.stoppingDistance = AttackRange;
                Animator.SetBool("IsAttacking", false);
            }
            else
            {
                Animator.SetBool("IsAttacking", true);
            }
        }
        else
        {
            Animator.SetBool("IsAttacking", false);
            Navigator.stoppingDistance = 0;
            Navigator.ResetPath();
        }
    }

    
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, NoticeRange);
    }
}
