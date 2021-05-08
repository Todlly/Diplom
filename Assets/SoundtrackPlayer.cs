using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackPlayer : MonoBehaviour
{
    public AudioClip[] tracks;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
        source.clip = tracks[0];
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
