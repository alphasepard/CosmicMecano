using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Credit()
    {
        SceneManager.LoadScene("Credit");
    }

    public void GameWin()
    {
        SceneManager.LoadScene("GameWin");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
