using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerMaxLives, playerCurrLives; // Variables to store the maximum and current player health.

    void Start()
    {
        // Set the current player lives to the maximum player lives.
        playerCurrLives = playerMaxLives;
    }

    void Update()
    {
        if(playerCurrLives <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
