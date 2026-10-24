using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGlazba : MonoBehaviour
{
    public static AudioSource glazba;
    private void Start()
    {
        glazba = GetComponentInChildren<AudioSource>();
    }
}
