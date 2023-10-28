// Importa las bibliotecas necesarias para el script.
using System.Collections;
using UnityEngine;

// Declara una clase llamada BlockHit que hereda de MonoBehaviour.
public class BlockHit : MonoBehaviour
{
    // Declara variables públicas.
    public GameObject item;         // Referencia a un objeto que se instancia cuando se destruye el bloque.
    public Sprite emptyBlock;       // Sprite que se muestra cuando el bloque se destruye completamente.
    public int maxHits = -1;        // Número máximo de golpes que el bloque puede recibir antes de ser destruido.
    private bool animating;         // Indica si se está reproduciendo una animación en el bloque.

    // Método que se llama cuando ocurre una colisión 2D con el bloque.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si no se está reproduciendo una animación y si el bloque no ha alcanzado su límite de golpes y si la colisión es con un objeto con la etiqueta "Player".
        if (!animating && maxHits != 0 && collision.gameObject.CompareTag("Player"))
        {
            // Verifica si la colisión se produce desde arriba.
            if (collision.transform.DotTest(transform, Vector2.up))
            {
                // Ejecuta la función Hit para manejar el golpe al bloque.
                Hit();
            }
        }
    }

    // Función para manejar un golpe al bloque.
    private void Hit()
    {
        // Obtiene el componente SpriteRenderer adjunto al bloque.
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Muestra el sprite si estaba oculto.
        spriteRenderer.enabled = true;

        // Reduce el contador de golpes restantes en el bloque.
        maxHits--;

        // Si se alcanza el límite de golpes, cambia el sprite del bloque al sprite vacío.
        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }

        // Si se proporciona un objeto para instanciar, lo crea en la posición del bloque.
        if (item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }

        // Inicia la animación del bloque.
        StartCoroutine(Animate());
    }

    // Coroutine para ejecutar una animación.
    private IEnumerator Animate()
    {
        animating = true;

        // Define la posición de reposo y la posición animada para el bloque.
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;

        // Realiza una animación de movimiento entre las posiciones.
        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        animating = false;
    }

    // Coroutine para realizar un movimiento suave entre dos posiciones.
    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            // Interpola suavemente entre las posiciones "from" y "to".
            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        // Asegura que la posición final sea "to".
        transform.localPosition = to;
    }
}
