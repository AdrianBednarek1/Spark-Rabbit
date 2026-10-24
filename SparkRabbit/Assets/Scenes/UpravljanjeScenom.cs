using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpravljanjeScenom : MonoBehaviour
{
    [SerializeField]
    Tranzicija tranzicijaScene;

    private void Start()
    {
       tranzicijaScene.PrikaziTranziciju();
    }
    IEnumerator UcitavanjeLevela(int indeks)
    {
        tranzicijaScene.ZapocniDruguTranziciju();
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(indeks);
    }
    public void SljedeciLevel()
    {
        StartCoroutine(UcitavanjeLevela(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void NaMenu()
    {
        StartCoroutine(UcitavanjeLevela(0));
    }
    public void NaPocetniLevel()
    {
        StartCoroutine(UcitavanjeLevela(1));
    }
    public void RestartLevel()
    {
        StartCoroutine(UcitavanjeLevela(SceneManager.GetActiveScene().buildIndex));
    }
}
