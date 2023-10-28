using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SideScrolling : MonoBehaviour
{
    private new Camera camera;
    private Transform player;

    public float height = 6.5f;
    public float undergroundHeight = -9.5f;
    public float undergroundThreshold = 0f;
    public float followSpeed = 5f; // Velocidad de seguimiento de la cámara

    // Nuevas variables para el seguimiento vertical
    public float verticalFollowSpeed = 3f;
    public float verticalOffset = 3f;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        // Obtén la posición actual de la cámara
        Vector3 cameraPosition = transform.position;

        // Interpola suavemente la posición en X hacia la posición del jugador
        cameraPosition.x = Mathf.Lerp(cameraPosition.x, player.position.x, followSpeed * Time.deltaTime);

        // Interpola suavemente la posición en Y hacia la posición del jugador más el desplazamiento vertical
        float targetY = player.position.y + verticalOffset;
        cameraPosition.y = Mathf.Lerp(cameraPosition.y, targetY, verticalFollowSpeed * Time.deltaTime);

        // Establece la posición de la cámara
        transform.position = cameraPosition;
    }

    public void SetUnderground(bool underground)
    {
        // Configura la altura de la cámara según si el jugador está bajo tierra o no
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergroundHeight : height;
        transform.position = cameraPosition;
    }
}
