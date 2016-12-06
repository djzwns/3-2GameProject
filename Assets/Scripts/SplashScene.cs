using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScene : MonoBehaviour {

    public void NextScene()
    {
        BGMManager.Instance.NextBGM();
        SceneManager.LoadScene("Main");
    }
}
