using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("Contadores de Pontuação (TextMeshPro)")]
    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;

    [Header("Painel de Vitória")]
    public GameObject winnerPanel;
    public TextMeshProUGUI winnerText;

    [Header("Regras de Vitória")]
    public int targetScore = 6;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (winnerPanel != null)
        {
            winnerPanel.SetActive(false);
        }
    }

    private void OnEnable()
    {
        // Escuta o evento estático do PlayerOM
        PlayerOM.OnStarCollected += HandleStarCollected;
    }

    private void OnDisable()
    {
        // Cancela a inscrição ao desativar a UI
        PlayerOM.OnStarCollected -= HandleStarCollected;
    }

    private void Start()
    {
        Time.timeScale = 1f;

        // Reseta os dados na estrutura estática ao iniciar
        PlayerOM.ResetScores();

        UpdateUI();
    }

    private void HandleStarCollected(int playerID, int currentStars)
    {
        UpdateUI();
        CheckWinner(playerID, currentStars);
    }

    private void UpdateUI()
    {
        if (p1ScoreText != null)
            p1ScoreText.text = $"P1: {PlayerOM.GetStars(1)} / {targetScore}";

        if (p2ScoreText != null)
            p2ScoreText.text = $"P2: {PlayerOM.GetStars(2)} / {targetScore}";
    }

    private void CheckWinner(int playerID, int score)
    {
        if (targetScore <= 0) return;

        if (score >= targetScore)
        {
            ShowWinner($"JOGADOR {playerID} VENCEU!");
        }
    }

    public void ShowWinner(string message)
    {
        if (winnerPanel != null) winnerPanel.SetActive(true);
        if (winnerText != null) winnerText.text = message;

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadGameplayWithUI();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}