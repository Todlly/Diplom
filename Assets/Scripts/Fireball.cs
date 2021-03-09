using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int Damage = 4;
    public float speed = 8;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        FlyingForward();
    }

    private void FlyingForward()
    {
        transform.position += transform.forward.normalized * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
            player.GetDamage(Damage, transform.forward);
        }
        Destroy(gameObject, 0);
    }
}
