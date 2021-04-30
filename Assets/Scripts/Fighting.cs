using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Fighting : MonoBehaviour
{
    public Sprite Dummy { get; set; }

    public float AttackRange = 6f;
    private int AttackDamage = 3;

    private Animator Animator;
    private NavMeshAgent Navigator;
    public Enemy TargetEnemy;
    private PlayerMoving MovingScript;

    private AudioSource AudioPlayer;
    public AudioClip EquipSound;
    private AudioSource EquipSoundPlayer;
    public AudioClip attack;


    private Image ShieldIcon;
    private Color[] ShieldIconColors = new Color[] { new Color(.3f, .3f, .3f), new Color(1.0f, 1.0f, 1.0f) };

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Navigator = GetComponent<NavMeshAgent>();
        MovingScript = GetComponent<PlayerMoving>();
        ShieldIcon = GameObject.Find("ShieldIcon").GetComponent<Image>();
        ShieldIcon.color = ShieldIconColors[0];

        AudioPlayer = GetComponent<AudioSource>();
        EquipSoundPlayer = gameObject.AddComponent<AudioSource>();
        EquipSoundPlayer.clip = EquipSound;
        EquipSoundPlayer.volume = 0.4f;
    }


    // Update is called once per frame
    void Update()
    {
        Blocking();
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
        AudioPlayer.PlayOneShot(attack, AudioPlayer.volume);
        TargetEnemy.gameObject.SendMessage("GetDamage", AttackDamage);
    }

    private void Blocking()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            EquipSoundPlayer.Play();
            if (!Animator.GetBool("IsBlocking"))
            {
                Animator.SetBool("IsBlocking", true);
                Navigator.speed = 0;
                ShieldIcon.color = ShieldIconColors[1];
                Debug.Log(ShieldIcon.color);
            }
            else
            {
                Animator.SetBool("IsBlocking", false);
                Navigator.speed = MovingScript.PlayerMovementSpeed;
                ShieldIcon.color = ShieldIconColors[0];
                Debug.Log(ShieldIcon.color);
            }
        }
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