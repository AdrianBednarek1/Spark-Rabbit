using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNanosenjeStete : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool napadam = false;
    [SerializeField]
    Transform pozicjiaUdarca;
    [SerializeField]
    private float range;
    [SerializeField]
    private LayerMask LayerPlayera;
    private void Napad()
    {
        Collider2D[] udarac = Physics2D.OverlapCircleAll(pozicjiaUdarca.position, range, LayerPlayera);
        foreach (Collider2D player in udarac)
        {
            if (player.CompareTag("Player"))
            {
                player.GetComponent<PlayerKontroler>().ozljedzen();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pozicjiaUdarca.position, range);
    }
}
