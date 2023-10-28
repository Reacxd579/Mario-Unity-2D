using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PointsController pointsController;
    private LivesController livesController;
    public static GameManager Instance { get; private set; }

    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives = 3;

    public int coins { get; private set; }
    private static int score = 0;

    public static int Score
    {
        get { return score; }
        set { score = value; }
    }

    private void Awake()
    {
        pointsController = FindObjectOfType<PointsController>();
        livesController = FindObjectOfType<LivesController>();
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("Lives", lives);
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("Score", Score);
        PlayerPrefs.SetInt("World", world);
        PlayerPrefs.SetInt("Stage", stage);
        PlayerPrefs.Save();
    }

    public void LoadPlayerData()
    {
        world = PlayerPrefs.GetInt("World", 1);
        stage = PlayerPrefs.GetInt("Stage", 1);
        Score = PlayerPrefs.GetInt("Score", 0);
        lives = PlayerPrefs.GetInt("Lives", 3);
        coins = PlayerPrefs.GetInt("Coins", 0);
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void GameOver()
    {
        Debug.Log("Se te reiniciaron las vidas");
        lives = 3;  // Reiniciar vidas a 3
        SceneManager.LoadScene("MAPA");
        Debug.Log("Vidas actuales: " + lives);
        Debug.Log("Monedas actuales: " + coins);
        Debug.Log("Mundo actual: " + world);
        Debug.Log("Nivel actual: " + stage);
        score = 0;
        Score = 0;
    }


    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;
        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void RemoveLife()
    {
        lives--;
        SceneManager.LoadScene("MAPA");
        Debug.Log("Lives: " + lives);
        if (lives == 0)
        {
            GameOver();
        }
    }

    public void AddCoin()
    {
        coins++;

        if (coins >= 100)
        {
            Debug.Log("PUNTAJE" + Score);
            Debug.Log("Ganaste una vida por: " + coins + " monedas");
            coins -= 100;
            AddLife();
        }

        CoinsController coinsController = FindObjectOfType<CoinsController>();
        if (coinsController != null)
        {
            coinsController.UpdateCoinsText();
        }
    }
    public void AddLife()
    {
        lives++;
        Debug.Log("Lives: " + lives);
        if (pointsController != null)
        {
            pointsController.UpdatePointsText();
        }
        if (livesController != null)
        {
            livesController.UpdateLivesText();
        }
        // Después de agregar una vida, actualiza el TextMeshProUGUI.
        CoinsController coinsController = FindObjectOfType<CoinsController>();
        if (coinsController != null)
        {
            coinsController.UpdateCoinsText();
        }
    }
}
