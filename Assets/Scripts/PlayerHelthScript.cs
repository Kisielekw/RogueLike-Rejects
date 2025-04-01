using UnityEngine;
using UnityEngine.UI;

public class PlayerHelthScript : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private GameObject _GameUI;
    [SerializeField] private GameObject _GameOverUI;

    public PlayerData playerData;

    public void TakeDamage(int damage)
    {
        playerData.Health -= damage;
        if (playerData.Health <= 0)
        {
            playerData.Health = 0;
            _GameUI.SetActive(false);
            _GameOverUI.SetActive(true);
        }
    }

    void Update()
    {
        _healthSlider.value = playerData.Health;
    }
}
