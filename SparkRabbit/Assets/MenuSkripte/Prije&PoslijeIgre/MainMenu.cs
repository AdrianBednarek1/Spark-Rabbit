using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tranzicija;
    private void Start()
    {
        tranzicija.SetActive(true);
    }
    public void PlayGame()
    {
        Manager.bodovi = 0;
        StartCoroutine(UcitajLevel(SceneManager.GetActiveScene().buildIndex + 1));        
    }
    IEnumerator UcitajLevel(int indeks)
    {
        tranzicija.GetComponent<Tranzicija>().ZapocniDruguTranziciju();
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(indeks);
    }
    IEnumerator IzadziIzIgre()
    {
        tranzicija.GetComponent<Tranzicija>().ZapocniDruguTranziciju();
        yield return new WaitForSeconds(1.3f);
        Application.Quit();
    }
    public void QuitGame()
    {
        StartCoroutine(IzadziIzIgre());
    }
}
