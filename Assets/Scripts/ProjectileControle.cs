using UnityEngine;

public class ProjectileControle : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHelthScript>().TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
}
