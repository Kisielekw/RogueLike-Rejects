using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int _health = 1;

    public void Damage(int damage)
    {
        _health -= damage;

        if(_health <= 0)
            Destroy(gameObject);
    }
}
