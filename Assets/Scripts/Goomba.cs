using UnityEngine;

public class Goomba : MonoBehaviour
{

    private PointsController pointsController;
    public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player.starpower)
            {
                Hit();
                GameManager.Score += 400;
                pointsController.UpdatePointsText();
                GameManager.Instance.SavePlayerData();

            }
            else if (collision.transform.DotTest(transform, Vector2.down))
            {
                Flatten();
            }
            else
            {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            GameManager.Score += 200;
            pointsController.UpdatePointsText();
            GameManager.Instance.SavePlayerData();
            Hit();
        }
    }
    private void Awake()
    {
        pointsController = FindObjectOfType<PointsController>();
    }
    private void Flatten()
    {
        GameManager.Score += 200;
        pointsController.UpdatePointsText();
        GameManager.Instance.SavePlayerData();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);

    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

}
