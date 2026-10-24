using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSaved : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1.5f);
        transform.position += new Vector3(0, 2.5f, 0);
    }
}
