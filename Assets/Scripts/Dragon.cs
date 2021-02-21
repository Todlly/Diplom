using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : MonoBehaviour
{
    private PlayerHealth Player;
    private NavMeshAgent Navigator;
    private Animator Animator;
    [SerializeField]
    private GameObject FireballSpawner;
    [SerializeField]
    private GameObject FireballFrefab;

    private float NoticeRange = 20;
    private float AttackRange = 15;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        Navigator = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();

        Navigator.stoppingDistance = AttackRange;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <= NoticeRange)
        {
            if (Vector3.Distance(transform.position, Player.transform.position) > AttackRange)
            {
                Navigator.SetDestination(Player.transform.position);
                Navigator.stoppingDistance = AttackRange;
                Animator.SetBool("IsRangeAttacking", false);
            }
            else
            {
                float dot = Vector3.Dot((transform.position - Player.transform.position).normalized, transform.forward);
                if (dot > 0.9)
                    Animator.SetBool("IsRangeAttacking", true);
            }
        }
        else
        {
            Animator.SetBool("IsRangeAttacking", false);
            Navigator.stoppingDistance = 0;
            Navigator.ResetPath();
        }
    }

    private void ShootFireball()
    {
        GameObject.Instantiate(FireballFrefab, FireballSpawner.transform.position, FireballSpawner.transform.rotation);
    }

    void Update()
    {

    }
}
