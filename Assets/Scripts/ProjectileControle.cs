using UnityEngine;

public class ProjectileControle : MonoBehaviour
{
    [SerializeField] private int _damage;

    [SerializeField] private EclipseData _eclipseData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<PlayerHelthScript>();
            playerHealth.TakeDamage(_eclipseData.IsEclipseActive ? _damage * 2 : _damage);
            Destroy(gameObject);
        }
    }
}
