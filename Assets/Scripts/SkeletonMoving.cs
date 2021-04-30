using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMoving : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator Animator;
    private NavMeshAgent Navigator;
    private PlayerHealth Player;
    private Enemy SelfEnemyScript;

    [SerializeField]
    private float AttackRange = 3;

    [SerializeField]
    private float NoticeRange = 20;

    [SerializeField]
    private int Damage = 4;

    private bool IsAttacking = false;

    void Start()
    {
        Animator = GetComponent<Animator>();
        Navigator = GetComponent<NavMeshAgent>();
        Navigator.stoppingDistance = AttackRange;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        SelfEnemyScript = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CauseDamage()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        float angle = Vector3.Angle(transform.forward, Player.transform.position - transform.position);
        if (distance <= AttackRange && angle < 25)
        {
            Player.GetDamage((int)Damage, transform.forward);
        }
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
            Navigator.ResetPath();
        }
    }


    void OnDrawGizmos()
    {
       // Gizmos.DrawSphere(transform.position, NoticeRange);
    }
}
