using UnityEngine;

public class PlayerHelthScript : MonoBehaviour
{
    public PlayerData playerData;

    void Start()
    {
        playerData.health = playerData.maxHealth;
    }

    public void TakeDamage(int damage)
    {
        playerData.health -= damage;
        if (playerData.health <= 0)
        {
            playerData.health = 0;
            Debug.Log("Player died");
        }
    }
}
