using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.LoadScene("GetStarted_Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saiu do jogo");
    }
}