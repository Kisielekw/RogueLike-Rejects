using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    void Update()
    {
        var roomData = GetComponentInParent<RoomInfo>();
        if(roomData.IsCleared())
            GetComponent<SpriteRenderer>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var roomData = GetComponentInParent<RoomInfo>();
            if (roomData.IsCleared())
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
