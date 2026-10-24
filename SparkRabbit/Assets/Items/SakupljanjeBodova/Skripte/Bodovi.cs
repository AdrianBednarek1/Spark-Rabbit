using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bodovi : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject,1.2f);
        transform.position += new Vector3(0, 0.5f, 0);
    }
}
