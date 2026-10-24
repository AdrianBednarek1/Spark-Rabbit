using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    private float Yvisina;

    Slider HPslider;
    private float hp = 1;
    public bool MozeAnimiratiStetu = false;
    

    void Start()
    {
        KreairajSlider();
    }
    private void Update()
    {
        PozicionirajHPslider();
    }
    public bool ImaLiHP()
    {
        if (hp < 0)
        {
            return false;
        }
        return true;
    }
    public void SmanjiHP(float damage)
    {
        MozeAnimiratiStetu = true;
        hp -= damage;
        HPslider.value = hp;
    }
    private void KreairajSlider()
    {
        if (slider != null && canvas!=null)
        {
            HPslider = Instantiate(slider);
            HPslider.transform.SetParent(canvas.transform);
            HPslider.gameObject.SetActive(true);
        }

    }
    private void PozicionirajHPslider()
    {
        if (HPslider != null)
        {
            HPslider.transform.position = new Vector3(transform.position.x, transform.position.y + Yvisina, transform.position.z);
        }
    }
    public void UnistiHPSlider()
    {
        Destroy(HPslider.gameObject);
    }
}
