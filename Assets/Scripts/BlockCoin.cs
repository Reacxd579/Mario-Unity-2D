using System.Collections;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{
    private PointsController pointsController;
    private void Awake()
    {
        pointsController = FindObjectOfType<PointsController>();
    }
    private void Start()
    {
        if (pointsController != null)
        {
            GameManager.Score += 100;
            pointsController.UpdatePointsText();
            GameManager.Instance.AddCoin();
            Debug.Log("Monedas actuales: " + GameManager.Instance.coins); // Imprimir la cantidad actual de monedas
            GameManager.Instance.SavePlayerData(); // Guardar la cantidad de monedas
            StartCoroutine(Animate());
        }
        else
        {
            Debug.LogError("PointsController not found in the scene!");
        }
    }

private IEnumerator Animate()
{
    Vector3 restingPosition = transform.localPosition;
    Vector3 animatedPosition = restingPosition + Vector3.up * 2f;

    yield return Move(restingPosition, animatedPosition);
    yield return Move(animatedPosition, restingPosition);

    Destroy(gameObject);
}

private IEnumerator Move(Vector3 from, Vector3 to)
{
    float elapsed = 0f;
    float duration = 0.25f;

    while (elapsed < duration)
    {
        float t = elapsed / duration;

        transform.localPosition = Vector3.Lerp(from, to, t);
        elapsed += Time.deltaTime;

        yield return null;
    }

    transform.localPosition = to;
}

}
