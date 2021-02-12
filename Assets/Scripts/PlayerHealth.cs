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
    private float RegenerationAmount = 1f; // Regen amount per second
    private float RegenerationDelay = 2;
    private float RegenerationTimer = 0f;

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
        StartCoroutine(Regenerate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    IEnumerator Regenerate()
    {
        while (true)
        {
            if (CurrentHealth < MaxHealth)
            {
                ChangeHealth((int)RegenerationAmount);
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void Die()
    {
        gameObject.SendMessage("ClearSelection");
        Debug.Log("You died");
    }


    public void ChangeHealth(int amount)
    {
        if (!IsAlive)
            return;

        CurrentHealth += amount;
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
        else if(amount < 0)
        {
            Animator.SetTrigger("GetHit");
        }
    }
}
