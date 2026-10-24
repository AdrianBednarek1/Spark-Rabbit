using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    private int hp = 5;
    private int zivoti = 3;

    [SerializeField]
    GameObject HPsrce;

    [SerializeField]
    GameObject Zivot;

    private PlayerSound sound;
    private void Start()
    {
        sound = GetComponent<PlayerSound>();
        ZivotiOdProslogLevela();
        OsvjeziPrikazZivota();
        OsvjeziPrikazHPa();
    }
    private void smanjiHP()
    {
        hp -= 1;
        OsvjeziPrikazHPa();
    }
    public void smanjiHPiliZivot()
    {     
        smanjiHP();  
        
        if(!AkoImaHP())
        {
            smanjiZivote();
        }
    }
    public int StanjeHPa()
    {
        return hp;
    }
    public bool AkoImaHP()
    {
        if (hp <= 5 && hp >= 0)
        {
            return true;
        }
        return false;
    }
    private void smanjiZivote()
    {
        zivoti -= 1;
        OsvjeziPrikazZivota();
    }
    public bool NemaZivote()
    {
        if (zivoti < 0)
        {
            return true;
        }
        return false;
    }
    public void ResetHP()
    {
        hp = 5;
        OsvjeziPrikazHPa();
    }
    public void DobivaZivot()
    {
        zivoti += 1;
        sound.PustiZivotZvuk();
        OsvjeziPrikazZivota();
    }
    public int stanjeZivota()
    {
        return zivoti;
    }
    private void OsvjeziPrikazHPa()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < hp)
            {
                HPsrce.GetComponentsInChildren<SpriteRenderer>()[i].enabled = true;
            }
            else
            {
                HPsrce.GetComponentsInChildren<SpriteRenderer>()[i].enabled = false;
            }
        }
    }
    private void OsvjeziPrikazZivota()
    {
        if (!NemaZivote())
        {
            Zivot.GetComponent<TextMeshProUGUI>().text = zivoti.ToString() + "X";
        }
    }

    private void ZivotiOdProslogLevela()
    {
        if (Manager.ekstraZivoti != 0)
        {
            zivoti = Manager.ekstraZivoti;
            Manager.ekstraZivoti = 0;
        }
    }
}
