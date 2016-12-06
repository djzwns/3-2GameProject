using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BGMManager : Singleton<BGMManager> {
    int bgmCount = 0;

    bool bCanChange = true;

    public AudioClip[] bgm;
    AudioSource source;
    float fliptime = 0.1f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        bgmCount = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(bgmCount);
        source = GetComponent<AudioSource>();
        source.clip = bgm[bgmCount];
        source.Play();
    }

    // 씬 인덱스를 이용한 bgm 변경
    public void BGMChange(int num)
    {
        if (bCanChange)
        {
            bgmCount = num;
            Debug.Log(bgmCount);
            StartCoroutine(ChangeBGM(num));
        }      
    }

    // 오디오 클립을 이용한 bgm 변경
    public void BGMChange(AudioClip clip)
    {
        if (bCanChange)
        {
            StartCoroutine(ChangeBGM(clip));
        }
    }

    // 원래 bgm 으로 되돌리기
    public void PrevBGM()
    {
        StartCoroutine(ChangeBGM(bgmCount));
    }

    public void NextBGM()
    {
        if (bCanChange)
        {
            ++bgmCount;
            if (bgmCount >= bgm.Length)
                bgmCount = 0;

            Debug.Log(bgmCount);
            if (bCanChange)
                StartCoroutine(ChangeBGM(bgmCount));
        }
    }

    public void Mute()
    {
        StartCoroutine(FadeOut(fliptime));
    }

    // 음소거 후 일시 정지
    IEnumerator FadeOut(float time)
    {
        while (source.volume > 0)
        {
            source.volume -= 0.1f;

            yield return new WaitForSecondsRealtime(time);
        }
        source.Pause();
    }

    // 재생 시키면서 볼륨을 올린다.
    IEnumerator FadeIn(float time)
    {
        source.Play();
        while (source.volume < 0.5f)
        {
            source.volume += 0.1f;

            yield return new WaitForSecondsRealtime(time);
        }
    }

    IEnumerator ChangeBGM(AudioClip clip)
    {
        bCanChange = false;
        yield return FadeOut(fliptime);

        source.clip = clip;

        yield return FadeIn(fliptime);
        bCanChange = true;
    }

    IEnumerator ChangeBGM(int num)
    {
        bCanChange = false;
        yield return FadeOut(fliptime);

        source.clip = bgm[num];

        yield return FadeIn(fliptime);
        bCanChange = true;
    }
}
