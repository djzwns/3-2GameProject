using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScene : MonoBehaviour {

    public void NextScene()
    {
        SceneManager.LoadScene("Main");
    }
}
