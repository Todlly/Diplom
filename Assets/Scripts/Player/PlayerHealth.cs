using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static Menu;

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

    private AudioSource AudioPlayer;
    public AudioClip Block;
    public AudioClip Hit;
    public AudioClip Death;

    private Fighting FightingScript;

    [SerializeField]
    private Slider HPBar;

    public bool IsAlive { get; private set; } = true;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Navigator = GetComponent<NavMeshAgent>();
        FightingScript = GetComponent<Fighting>();
        HPBar = GameObject.FindGameObjectWithTag("HP Bar").GetComponent<Slider>();
        HPBar.minValue = 0;
        HPBar.maxValue = MaxHealth;
        CurrentHealth = MaxHealth;
        HPBar.value = CurrentHealth;
        StartCoroutine(Regenerate());

        AudioPlayer = GetComponent<AudioSource>();
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
        FindObjectOfType<Timer>().Stop();
        gameObject.SendMessage("ClearSelection");
        GameObject.Find("End Menu").GetComponent<EndMenu>().Defeat();
    }

    public void GetDamage(int amount, Vector3 dealersForward) // Функция получения урона героем
    {
        if (!Animator.GetBool("IsBlocking")) // Если нет блока, просто получить урон
        {
            AudioPlayer.PlayOneShot(Hit, AudioPlayer.volume); // Проиграть звук попадания
            ChangeHealth(-amount); // Уменьшить здоровье
            return; // Выйти из функции
        }

        if (Vector3.Angle(transform.forward, dealersForward) >= 135) // Если щит поднят, и атакующий спереди от игрока
        {
            AudioPlayer.PlayOneShot(Block, AudioPlayer.volume); // Проиграть звук попадания по щиту
        }
        else // Если атакующий не спереди, то всё равно получить урон
        {
            AudioPlayer.PlayOneShot(Hit, AudioPlayer.volume); // Проиграть звук попадания
            ChangeHealth(-amount); // Уменьшить здоровье
        }
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
            AudioPlayer.PlayOneShot(Death, AudioPlayer.volume);
            Animator.SetTrigger("Die");
        }
        else if (amount < 0)
        {
            Animator.SetTrigger("GetHit");
        }
    }
}
