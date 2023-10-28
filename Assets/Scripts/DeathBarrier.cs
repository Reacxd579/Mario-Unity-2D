using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
        // Resta una vida cuando el jugador toca la barrera de muerte
        GameManager.Instance.RemoveLife();
        GameManager.Instance.SavePlayerData();
        }
        else
        {
            Destroy(other.gameObject);
        }
    }

}
