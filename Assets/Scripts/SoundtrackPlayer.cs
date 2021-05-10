using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundtrackPlayer : MonoBehaviour
{
    public AudioClip[] tracks;
    private AudioSource source;
    // Start is called before the first frame update
    public static SoundtrackPlayer SoundtrackPlayerInstance;

    private void Awake()
    {
        if (SoundtrackPlayerInstance == null)
            SoundtrackPlayerInstance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
      //  source.clip = tracks[0];
     //   source.Play();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
