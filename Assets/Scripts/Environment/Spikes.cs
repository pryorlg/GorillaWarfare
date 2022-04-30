using UnityEngine;
using UnityEngine.Tilemaps;

public class Spikes : MonoBehaviour
{
    private TilemapCollider2D spikeCollider;

    private PlayerHealth playerHealth;

    [SerializeField] private int damage; // Variable to store the amount of damage given.

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        spikeCollider = GetComponent<TilemapCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.CompareTag("Player"))
        {
            col.collider.GetComponent<PlayerHealth>().takeDamage(damage);
            col.collider.gameObject.SetActive(false);
        }
    }
}
