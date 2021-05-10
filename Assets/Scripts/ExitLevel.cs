using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundtrackPlayer;

public class ExitLevel : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> AliveEnemies;

    void Start()
    {
        AliveEnemies.AddRange(FindObjectsOfType<Enemy>());
        AudioSource soundtrack = SoundtrackPlayerInstance.GetComponent<AudioSource>();
        soundtrack.Stop();
        soundtrack.clip = SoundtrackPlayerInstance.tracks[0];
        soundtrack.Play();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is trying to escape");
            if(AliveEnemies.FindAll(enemy => enemy != null).Count == 0)
            {
                Debug.LogError("Win");
            }
            else
            {
                Debug.Log("Not all enemies killed");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
