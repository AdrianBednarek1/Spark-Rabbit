using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodoviZvuk : MonoBehaviour
{
    private AudioSource source;
    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.volume = .8f;
    }
    public void OnChildTrigger(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
    
}
