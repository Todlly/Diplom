using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AI;
using static Menu;

public class Dragon : MonoBehaviour
{
    public float MeleeAttackRange;
    public float RangeAttackRange;
    public float MeleeAttackDamage;
    public float RangeAttackDamage;
    public float NoticeRange;

    public AudioClip FireballShot, Woosh, Bite;
    private AudioSource AudioPlayer;

    [SerializeField]
    private GameObject Fireball;
    [SerializeField]
    private GameObject FireballSpawner;

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
        AudioPlayer = GetComponent<AudioSource>();

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    public void PlayFireball()
    {
        AudioPlayer.PlayOneShot(FireballShot, AudioPlayer.volume);
    }

    public void ShootFireball()
    {
        Instantiate(Fireball, FireballSpawner.transform.position, FireballSpawner.transform.rotation);
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

    private void PlayWoosh()
    {
        AudioPlayer.PlayOneShot(Woosh, AudioPlayer.volume);
    }

    private void PlayBite()
    {
        AudioPlayer.PlayOneShot(Bite, AudioPlayer.volume);
    }
}
