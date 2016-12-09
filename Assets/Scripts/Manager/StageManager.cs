using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager> {
    int iNextSceneIndex;
    Image image;
    public bool bEnd { get; private set; }
    

    void Start()
    {
        iNextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        image = GameObject.Find("blind").GetComponent<Image>();
    }

    public void Clear()
    {
        StartCoroutine(ClearGame());
    }

    public void Fail()
    {
        StartCoroutine(FailGame());
    }

    // 화면 어둡게
    public IEnumerator FadeOut(float speed)
    {
        while (image.color.a < 1f)
        {
            image.color = new Color(0, 0, 0, image.color.a + 0.04f);
            yield return new WaitForSecondsRealtime(speed);
        }
    }

    // 화면 밝게
    public IEnumerator FadeIn(float speed)
    {
        while (image.color.a > 0f)
        {
            image.color = new Color(0, 0, 0, image.color.a - 0.08f);
            yield return new WaitForSecondsRealtime(speed);
        }
    }

    IEnumerator ClearGame()
    {
        bEnd = true;
        yield return FadeOut(0.5f);

        yield return new WaitForSecondsRealtime(0.6f);
        SceneManager.LoadScene(iNextSceneIndex);
        bEnd = false;
    }

    IEnumerator FailGame()
    {
        bEnd = true;

        yield return new WaitForSecondsRealtime(1f);
        yield return FadeOut(0.5f);

        yield return new WaitForSecondsRealtime(0.6f);
        SceneManager.LoadScene(iNextSceneIndex - 1);
        bEnd = false;
    }

    // 메인으로 돌아가기
    public void GotoMain()
    {
        SceneManager.LoadScene("Main");
    }
}
