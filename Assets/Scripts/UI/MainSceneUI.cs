using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneUI : MonoBehaviour {
    void Awake()
    {
        BGMManager.Instance.BGMChange(1);   // 1번은 메인화면 인덱스 번호
    }

    public void GameStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
