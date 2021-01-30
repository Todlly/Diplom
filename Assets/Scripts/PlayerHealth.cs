using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int CurrentHealth;
    private int MaxHealth = 20;
    private Animator Animator;
    private NavMeshAgent Navigator;

    [SerializeField]
    private Slider HPBar;

    public bool IsAlive { get; private set; } = true;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Navigator = GetComponent<NavMeshAgent>();
        HPBar = GameObject.FindGameObjectWithTag("HP Bar").GetComponent<Slider>();
        HPBar.minValue = 0;
        HPBar.maxValue = MaxHealth;
        CurrentHealth = MaxHealth;
        HPBar.value = CurrentHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Die()
    {

        Debug.Log("You died");
    }


    public void GetDamage(int amount)
    {
        if (!IsAlive)
            return;

        CurrentHealth -= amount;
        HPBar.value = CurrentHealth;

        if (CurrentHealth <= 0)
        {
            Navigator.enabled = false;
            GetComponent<PlayerMoving>().enabled = false;
            IsAlive = false;
            Animator.SetBool("IsAlive", false);
            GetComponent<PlayerMoving>().enabled = false;
            Animator.SetTrigger("Die");
        }
        else
        {
            Animator.SetTrigger("GetHit");
        }
    }
}
