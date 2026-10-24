using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSound : MonoBehaviour
{
    [SerializeField]
    AudioClip attack1;

    [SerializeField]
    AudioClip attack2;

    [SerializeField]
    AudioClip Ouch;
    private AudioSource source;
    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.volume = 1f;
    }

    // Update is called once per frame
    private void OuchSound()
    {
        source.PlayOneShot(Ouch);
    }
    private void Attack1Sound()
    {
        source.PlayOneShot(attack1);
    }
    private void Attack2Sound()
    {
        source.PlayOneShot(attack2);
    }
}
