using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : MonoBehaviour
{
<<<<<<< HEAD
    public float MeleeAttackRange;
    public float RangeAttackRange;
    public float MeleeAttackDamage;
    public float RangeAttackDamage;
    public float NoticeRange;

    private NavMeshAgent Navigator;
    private Animator Animator;

    private PlayerHealth Player;

    // Start is called before the first frame update
    void Start()
    {
        Navigator = GetComponent<NavMeshAgent>();
        Navigator.updateRotation = false;
        Navigator.stoppingDistance = RangeAttackRange;
        Animator = GetComponent<Animator>();

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
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

            if (distanceToPlayer > RangeAttackRange)
            {
                transform.rotation = Quaternion.LookRotation(Player.transform.position - transform.position);
                Navigator.SetDestination(Player.transform.position);
                Animator.SetBool("IsRangeAttacking", false);
                Animator.SetBool("IsMeeleAttacking", false);
            }
            else if (distanceToPlayer > MeleeAttackRange)
            {
                Animator.SetBool("IsRangeAttacking", true);
                Animator.SetBool("IsMeeleAttacking", false);
            }
            else
            {
                Animator.SetBool("IsRangeAttacking", false);
                Animator.SetBool("IsMeeleAttacking", true);
            }
        }
    }

    private void MeleeAttack()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        float angle = Vector3.Angle(transform.forward, Player.transform.position - transform.position);
        if (distance <= MeleeAttackRange && angle < 25)
        {
            Player.GetDamage((int)MeleeAttackDamage, transform.forward);
        }
    }

=======
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
>>>>>>> eda3efce6a6a8884c0263bd60f6f4a8829e9647a
}
