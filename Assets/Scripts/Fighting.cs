using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Fighting : MonoBehaviour
{
    private float AttackRange { get; set; } = 5f;
    private float AttackAngle { get; set; }
    private int AttackDamage { get; set; } = 1;
    private float AutoattackTime { get; set; } = 1.5f;
    public bool IsAttacking { get; set; } = false;

    private Animator Animator { get; set; }
    private NavMeshAgent Navigator { get; set; }

    public Enemy SelectedEnemy { get; set; }
    private Rigidbody Player { get; set; }

    private Text EnemyLabel { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        Navigator = GetComponent<NavMeshAgent>();

        EnemyLabel = GameObject.Find("SelectedEnemyLabel").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, 1 << 7))
            {
                SelectedEnemy = hit.collider.gameObject.GetComponent<Enemy>();
            }
            else
            {
                SelectedEnemy = null;
            }

            //    Animator.SetTrigger("Attack1");
        }

        if (SelectedEnemy != null)
            EnemyLabel.text = SelectedEnemy.name + ", IsAttacking = " + IsAttacking + Vector3.Distance(Player.position, SelectedEnemy.transform.position);
        else
            EnemyLabel.text = "No enemy selected" + ", IsAttacking = " + IsAttacking;
    }

    private void FixedUpdate()
    {
        if (IsAttacking)
            Player.transform.rotation = Quaternion.LookRotation(SelectedEnemy.transform.position - Player.position);
        ApproachForAttack();
    }

    public void Attack()
    {
        SelectedEnemy.gameObject.SendMessage("GetDamage", AttackDamage);
    }

    IEnumerator Attacking(Enemy enemy)
    {
        Animator.SetBool("Attack1", true);

        while (IsAttacking)
        {
            yield return null;
        }

        Animator.SetBool("Attack1", false);
    }

    private void ApproachForAttack()
    {
        if (SelectedEnemy != null)
        {
            float distance = Vector3.Distance(Player.position, SelectedEnemy.transform.position);
            if (distance > AttackRange)
            {
                IsAttacking = false;
                Navigator.stoppingDistance = AttackRange;
                Navigator.SetDestination(SelectedEnemy.transform.position);
                Player.rotation = Quaternion.LookRotation(Navigator.destination - Player.position);
                Animator.SetBool("IsWalking", true);
            }
            else
            {
                //   Animator.SetBool("IsWalking", false);

                //   if (!IsAttacking)
                //       StartCoroutine(Attacking(SelectedEnemy));
                // Navigator.ResetPath();
                Debug.Log("Approached");
                IsAttacking = true;
                StartCoroutine(Attacking(SelectedEnemy));
            }
        }
        else
        {
            IsAttacking = false;
        }

    }


}