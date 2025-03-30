using UnityEngine;

public class ProjectileControle : MonoBehaviour
{
    [SerializeField] private int _damage;

    [SerializeField] private EclipseData _eclipseData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHelthScript>().TakeDamage(_eclipseData.IsEclipseActive ? _damage * 2 : _damage);
        }
        Destroy(gameObject);
    }
}
