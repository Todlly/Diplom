//using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour//, IHavingHealth
{
    public enum EnemyType
    {
        Dummy, Skeleton
    }
    [SerializeField]
    public int MaxHealth;

    [SerializeField]
    public int CurrentHealth;
    private Animator Animator;
    private Slider HealthBar;
    private PlayerHealth Player;
    private Interactable SelfInteractable;

    public EnemyType Type;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        Animator = GetComponent<Animator>();
        HealthBar = GetComponentInChildren<Slider>();

        MaxHealth = 5;
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
        Destroy(this.gameObject);
        Player.gameObject.SendMessage("ClearSelection");
    }

    private void TurnToPlayer()
    {
        transform.eulerAngles = new Vector3(0, Quaternion.LookRotation(Player.transform.position - transform.position).eulerAngles.y, 0);
    }

    

    private void Hello()
    {
        Debug.Log("HEllo");
    }

    private void GetDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
}
