using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    CinemachineVirtualCamera TrenutnaKamera;

    [SerializeField]
    PlayerHP hpManager;
    void Start()
    {
        TrenutnaKamera = GetComponent<CinemachineVirtualCamera>();      
    }
    void Update()
    { 
        if (!hpManager.AkoImaHP())
        {
            TrenutnaKamera.enabled = false;
        }
        else
        {
            TrenutnaKamera.enabled = true;
        }      
    }
}
