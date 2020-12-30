using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreating : MonoBehaviour
{
    [SerializeField]
    private GameObject Room { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;

        if (!player.CompareTag("Player"))
            return;


    }
}
