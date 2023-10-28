// Importa las bibliotecas necesarias.
using UnityEditor;
using UnityEngine;

// Asegura que este componente tenga un Rigidbody2D adjunto.
[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour
{
    // La velocidad de movimiento de la entidad.
    public float speed = 1f;

    // La dirección inicial de movimiento de la entidad.
    public Vector2 direction = Vector2.left;

    // Rigidbody2D asociado a la entidad.
    private new Rigidbody2D rigidbody;

    // Almacena la velocidad actual de la entidad.
    private Vector2 velocity;

    // Se llama al inicio antes de Start(). Inicializa el Rigidbody2D y desactiva este script.
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    // Se llama cuando la entidad se hace visible en la cámara.
    private void OnBecameVisible()
    {
        // Habilita o deshabilita el script según si se encuentra en el editor de Unity en pausa o no.
        #if UNITY_EDITOR
        enabled = !EditorApplication.isPaused;
        #else
        enabled = true;
        #endif
    }

    // Se llama cuando la entidad se vuelve invisible en la cámara.
    private void OnBecameInvisible()
    {
        // Deshabilita el script.
        enabled = false;
    }

    // Se llama cuando se habilita este componente.
    private void OnEnable()
    {
        // Despierta el Rigidbody2D, lo que lo activa para física.
        rigidbody.WakeUp();
    }

    // Se llama cuando se deshabilita este componente.
    private void OnDisable()
    {
        // Detiene la velocidad del Rigidbody2D y lo "duerme".
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }

    // Se llama en intervalos de tiempo fijos para manejar la lógica de movimiento de la entidad.
    private void FixedUpdate()
    {
        // Calcula la velocidad en función de la dirección y la velocidad establecida.
        velocity.x = direction.x * speed;

        // Aplica la gravedad a la velocidad en el eje Y.
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        // Mueve la posición del Rigidbody2D basado en la velocidad.
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);

        // Realiza un Raycast en la dirección de movimiento para cambiar la dirección cuando se detecta una colisión.
        if (rigidbody.Raycast(direction))
        {
            direction = -direction;
        }

        // Asegura que la entidad no pueda moverse hacia abajo si está en el suelo.
        if (rigidbody.Raycast(Vector2.down))
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }

        // Cambia la orientación de la entidad según la dirección de movimiento.
        if (direction.x > 0f)
        {
            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if (direction.x < 0f)
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }
}
