using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneUI : MonoBehaviour {

    public void GameStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
