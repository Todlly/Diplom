using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Enemy : MonoBehaviour//, IHavingHealth
{
    public enum EnemyType
    {
        Dummy, Skeleton, Dragon
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

    void Start()
    {
        Animator = GetComponent<Animator>();
        HealthBar = GetComponentInChildren<Slider>();

        MaxHealth = 5;
        HealthBar.minValue = 0;
        HealthBar.maxValue = MaxHealth;

        CurrentHealth = MaxHealth;
    }

    void Update()
    {
        if(Player == null)
            Player = GameObject.Find("Player").GetComponent<PlayerHealth>();

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

    

    private void GetDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
}
