using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           Destroy(gameObject); 
        }
    }
}
