using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private LivesController livesController;
    private PointsController pointsController;
    public enum Type
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        Starpower,
    }

    public Type type;
    private void Awake()
    {
        livesController = FindObjectOfType<LivesController>();
        pointsController = FindObjectOfType<PointsController>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {

        switch (type)
        {
            case Type.Coin:
                GameManager.Instance.AddCoin();
                GameManager.Score += 100;
                pointsController.UpdatePointsText();
                Debug.Log("Monedas actuales: " + GameManager.Instance.coins); // Imprimir la cantidad actual de monedas
                GameManager.Instance.SavePlayerData();
                break;

            case Type.ExtraLife:
                GameManager.Instance.AddLife();
                livesController.UpdateLivesText();
                GameManager.Score += 1000;
                pointsController.UpdatePointsText();
                Debug.Log("Vida Extra!");
                GameManager.Instance.SavePlayerData();
                break;

            case Type.MagicMushroom:
                GameManager.Score += 600;
                pointsController.UpdatePointsText();
                GameManager.Instance.SavePlayerData();
                player.GetComponent<Player>().Grow();
                break;

            case Type.Starpower:
                GameManager.Score += 500;
                pointsController.UpdatePointsText();
                GameManager.Instance.SavePlayerData();
                player.GetComponent<Player>().Starpower();
                break;
        }

        Destroy(gameObject);
    }

}
