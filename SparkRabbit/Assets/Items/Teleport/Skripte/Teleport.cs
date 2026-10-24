using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Transform[] teleport;
    void Start()
    {
        teleport = GetComponentsInChildren<Transform>();
    }
    public void ChildTrigger(Transform childTransform, Collider2D Player, Vector3 pozicijaTeleporta)
    {
        foreach (Transform t in teleport)
        {
            if (!t.Equals(childTransform))
            {
                Player.transform.position = t.position+pozicijaTeleporta;
            }
        }
    }
}
