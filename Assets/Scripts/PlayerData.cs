using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int maxHealth;
    public int health;
    public float speed;
    public int damage;
    public float hitCooldown;
}
