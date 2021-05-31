using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SoundtrackPlayer : MonoBehaviour
{
    private Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
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

        clips.Add("Main theme", Resources.Load<AudioClip>("Sounds/Soundtracks/MainTheme"));
        clips.Add("Menu theme", Resources.Load<AudioClip>("Sounds/Soundtracks/MenuTheme"));
    }

    public void ChangeClip(string clipName)
    {
        if (source.isPlaying)
            source.Stop();
        source.clip = clips[clipName];
        source.Play();
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
