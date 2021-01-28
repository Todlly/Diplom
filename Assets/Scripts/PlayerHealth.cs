using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int CurrentHealth;
    private int MaxHealth = 20;

[SerializeField]
    private Slider HPBar;

    // Start is called before the first frame update
    void Start()
    {
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

    public void GetDamage(int amount)
    {
        CurrentHealth -= amount;
        HPBar.value = (float)CurrentHealth / (float)MaxHealth;
    }
}
