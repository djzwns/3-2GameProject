using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager> {
    int iNextSceneIndex;
    Image image;
    public bool bEnd { get; private set; }

    BGMManager bgm;

    void Start()
    {
        iNextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        image = GameObject.Find("blind").GetComponent<Image>();
        bgm = BGMManager.Instance;
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

        yield return new WaitForSecondsRealtime(0.5f);
        bgm.NextBGM();
        SceneManager.LoadScene(iNextSceneIndex);
        bEnd = false;
    }

    IEnumerator FailGame()
    {
        bEnd = true;
        yield return FadeOut(0.5f);

        yield return new WaitForSecondsRealtime(0.5f);
        bgm.BGMChange(iNextSceneIndex - 1);
        SceneManager.LoadScene(iNextSceneIndex - 1);
        bEnd = false;
    }

    // 메인으로 돌아가기
    public void GotoMain()
    {
        bgm.BGMChange(1);   // 1번은 메인화면 인덱스 번호
        SceneManager.LoadScene("Main");
    }
}
