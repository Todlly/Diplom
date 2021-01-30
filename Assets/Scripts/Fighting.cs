using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Fighting : MonoBehaviour
{
    public Sprite Dummy { get; set; }

    public float AttackRange { get; set; } = 6f;
    private float AttackAngle { get; set; }
    private int AttackDamage { get; set; } = 3;
    private float AutoattackTime { get; set; } = 1.5f;
    public bool IsAttacking { get; set; } = false;

    private Animator Animator { get; set; }
    private NavMeshAgent Navigator { get; set; }

    private Image TargetFrame { get; set; }
    public Enemy SelectedEnemy { get; set; }
    private Rigidbody Player { get; set; }

    private Text EnemyLabel { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        Navigator = GetComponent<NavMeshAgent>();

        Dummy = Resources.Load<Sprite>("Icons/Dummy");
        TargetFrame = GameObject.Find("TargetFrame").GetComponent<Image>();
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
        {
            TargetFrame.enabled = true;
            TargetFrame.sprite = Dummy;
            EnemyLabel.text = SelectedEnemy.name + ", IsAttacking = " + Vector3.Distance(Player.position, SelectedEnemy.transform.position);
        }
        else
        {
            TargetFrame.sprite = null;
            TargetFrame.enabled = false;
            EnemyLabel.text = "No enemy selected" + ", IsAttacking = " + IsAttacking;
        }
    }

    private void TurnToEnemy()
    {
        Quaternion lookTo = Quaternion.LookRotation(SelectedEnemy.transform.position - transform.position);
        transform.eulerAngles = new Vector3(0, lookTo.eulerAngles.y, 0);
    }

    private void FixedUpdate()
    {
        ApproachForAttack();
    }

    public void Attack()
    {
        SelectedEnemy.gameObject.SendMessage("GetDamage", AttackDamage);
    }

    private void ApproachForAttack()
    {
        if (SelectedEnemy != null)
        {
            float distance = Vector3.Distance(transform.position, SelectedEnemy.transform.position);
            EnemyLabel.text = "Distance " + distance;
            if (distance > AttackRange)
            {
                Animator.SetBool("IsAttacking", false);
                Navigator.SetDestination(SelectedEnemy.transform.position);
            }
            else
            {

                IsAttacking = true;
                Animator.SetBool("IsAttacking", true);
            }
        }
        else
        {
            Animator.SetBool("IsAttacking", false);
        }

    }


}