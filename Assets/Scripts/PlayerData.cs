using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int MaxHealth;
    public int Health;
    public float speed;
    public int Damage;
    public float HitCooldown;
    public int Score;
}
