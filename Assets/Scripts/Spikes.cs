using UnityEngine;
using UnityEngine.Tilemaps;

public class Spikes : MonoBehaviour
{

    [SerializeField] private int damage; // Variable to store the amount of damage given.

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.CompareTag("Player"))
        {
            col.collider.GetComponent<PlayerHealth>().takeDamage(damage);
        }
    }
}