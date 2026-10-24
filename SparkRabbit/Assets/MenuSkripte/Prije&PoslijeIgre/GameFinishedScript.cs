using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameFinishedScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI score;
    void Start()
    {
        score.text = "Score : " + Manager.bodovi;
    }
    public static void ResetirajRezultate()
    {
        Manager.bodovi = 0;
    }
}
