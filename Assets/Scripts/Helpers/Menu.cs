using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void Update()
    {
        if (Input.GetKeyDown("r")) StartGame();
    }

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

    public void MenuGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SettingGame()
    {
        SceneManager.LoadScene("Setting");
    }

    public void Vaisseaux()
    {
        SceneManager.LoadScene("Vaisseaux");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
