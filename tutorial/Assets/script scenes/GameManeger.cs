using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public enum GameState
{
    Iniciando,
    Gameplay,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;
    public PlayerInput playerInput;

    [Header("Configuração de Cenas")]
    public string gameplaySceneName = "GetStarted_Scene";
    public string uiSceneName = "GUI"; 

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Inicia o processo de carregamento do Gameplay com a UI
        LoadGameplayWithUI();
    }

    public void SetState(GameState newState)
    {
        currentState = newState;
        Debug.Log("Estado atual: " + currentState);
    }

    /// <summary>
    /// Carrega a cena de jogo principal e adiciona a UI por cima.
    /// </summary>
    public void LoadGameplayWithUI()
    {
        StartCoroutine(LoadGameplayWithUIRoutine());
    }

    private IEnumerator LoadGameplayWithUIRoutine()
    {
        SetState(GameState.Gameplay);

        // Se a cena de Gameplay já estiver aberta (por exemplo, ao testar direto pelo Editor), pula o carregamento Single
        if (SceneManager.GetActiveScene().name != gameplaySceneName)
        {
            AsyncOperation asyncGameplay = SceneManager.LoadSceneAsync(gameplaySceneName, LoadSceneMode.Single);
            while (!asyncGameplay.isDone)
            {
                yield return null;
            }
        }

        // Carrega a cena de UI por cima (Additive) se ainda não estiver aberta
        yield return StartCoroutine(LoadUISceneAdditiveRoutine());
    }

    private IEnumerator LoadUISceneAdditiveRoutine()
    {
        if (!SceneManager.GetSceneByName(uiSceneName).isLoaded)
        {
            AsyncOperation asyncUI = SceneManager.LoadSceneAsync(uiSceneName, LoadSceneMode.Additive);
            while (!asyncUI.isDone)
            {
                yield return null;
            }
        }
    }

    public void AssignInputToPlayer()
    {
        if (playerInput != null)
        {
            playerInput.ActivateInput();
            Debug.Log("Input ativado");
        }
    }
}