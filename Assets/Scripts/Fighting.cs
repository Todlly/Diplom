using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Fighting : MonoBehaviour
{
    public Sprite Dummy { get; set; }

    public float AttackRange { get; set; } = 6f;
    private int AttackDamage { get; set; } = 3;

    private Animator Animator { get; set; }
    private NavMeshAgent Navigator { get; set; }
    public Enemy TargetEnemy { get; set; }
    private PlayerMoving MovingScript;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Navigator = GetComponent<NavMeshAgent>();
        MovingScript = GetComponent<PlayerMoving>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void TurnToEnemy()
    {
        Quaternion lookTo = Quaternion.LookRotation(TargetEnemy.transform.position - transform.position);
        transform.eulerAngles = new Vector3(0, lookTo.eulerAngles.y, 0);
    }

    private void FixedUpdate()
    {
        ApproachForAttack();
    }

    public void Attack()
    {
        TargetEnemy.gameObject.SendMessage("GetDamage", AttackDamage);
    }

    private void ApproachForAttack()
    {
        if (TargetEnemy != null && MovingScript.isHeadingToTarget)
        {
            float distanceToTarget = Vector3.Distance(transform.position, TargetEnemy.transform.position);

            if (distanceToTarget > AttackRange)
            {
                Animator.SetBool("IsAttacking", false);
                Navigator.SetDestination(TargetEnemy.transform.position);
            }
            else
            {
                Animator.SetBool("IsAttacking", true);
            }
        }
        else
        {
            Animator.SetBool("IsAttacking", false);
        }

    }


}