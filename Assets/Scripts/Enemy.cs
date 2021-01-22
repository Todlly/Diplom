using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour//, IHavingHealth
{
    private int MaxHealth { get; set; }
    private int CurrentHealth { get; set; }
    private Animator Animator { get; set; }
    private Slider HealthBar { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        HealthBar = GetComponentInChildren<Slider>();

        MaxHealth = 20;
        HealthBar.minValue = 0;
        HealthBar.maxValue = MaxHealth;

        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.value = CurrentHealth;
    }

    private void Die()
    {

    }

    private void GetDamage(int damageAmount)
    {
        Debug.Log("Enemy got hit for " + damageAmount + " damage.");
        Animator.SetTrigger("GetHit");
        CurrentHealth -= damageAmount;
        if(CurrentHealth <= 0)
        {
            Die();
        }
    }
}
