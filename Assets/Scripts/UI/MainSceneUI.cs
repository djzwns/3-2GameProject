using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneUI : MonoBehaviour {

    public void GameStart()
    {
        SceneManager.LoadScene("Game");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
