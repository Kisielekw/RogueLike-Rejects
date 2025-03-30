using UnityEngine;
using UnityEngine.UI;

public class PlayerHelthScript : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;

    public PlayerData playerData;

    void Start()
    {
        _healthSlider.maxValue = playerData.maxHealth;
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

    void Update()
    {
        _healthSlider.value = playerData.health;
    }
}
