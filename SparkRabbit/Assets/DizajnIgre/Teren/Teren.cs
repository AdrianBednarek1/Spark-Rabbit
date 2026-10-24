using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Teren : MonoBehaviour
{
    public GameObject Nebo;
    
    private SpriteRenderer[] spriteNebo;
    private SpriteShapeRenderer[] spriteRenderer;
    private bool stop = false;
    private int indeksBoje;
    private float t = 0f;

    [SerializeField][Range(0f, 1f)] 
    float lerpTime;
    
    [SerializeField][Range(0f, 1f)] 
    float lerpCOlor;
    
    [SerializeField] 
    Color[] boje;
   
    [SerializeField] 
    Color pocetnaBoja;

    [SerializeField]
    PlayerHP hpManager;
    void Start()
    {
        spriteRenderer = GetComponentsInChildren<SpriteShapeRenderer>();
        spriteNebo = Nebo.GetComponentsInChildren<SpriteRenderer>();
    }
    void Update()
    {
        if (hpManager.StanjeHPa() < 1 || UpravljanjePjesmom.BossGlazba)
        {
            stop = false;
            foreach (SpriteRenderer sr in spriteNebo)
            {
                sr.color = MijenjajBoje(sr.color);
            }
            foreach (SpriteShapeRenderer sr in spriteRenderer)
            {
                sr.color = MijenjajBoje(sr.color);           
            }
        }
        else if (!stop)
        {
            stop = true;
            foreach (SpriteRenderer sr in spriteNebo)
            {
                sr.color = Color.white;
            }
            foreach (SpriteShapeRenderer sr in spriteRenderer)
            {
                sr.color = pocetnaBoja;
            }
        }
    }
    private Color MijenjajBoje(Color color)
    {     
        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if (t > .9f)
        {
            t = 0f;
            indeksBoje++;
            indeksBoje = (indeksBoje >= boje.Length) ? 0 : indeksBoje;
        }
        return Color.Lerp(color, boje[indeksBoje], t * lerpCOlor);
    }
}
