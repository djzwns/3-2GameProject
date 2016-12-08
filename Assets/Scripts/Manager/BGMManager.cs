using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BGMManager : Singleton<BGMManager> {
    int bgmCount = 0;

    public AudioClip[] bgm;
    AudioSource source;
    float fliptime = 0.1f;
    float pauseTime = 0f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        bgmCount = SceneManager.GetActiveScene().buildIndex;

        source = GetComponent<AudioSource>();
        source.clip = bgm[bgmCount];
        source.Play();
    }

    // 씬 인덱스를 이용한 bgm 변경
    public void BGMChange(int num)
    {
        StopAllCoroutines();
        bgmCount = num;
        StartCoroutine(ChangeBGM(num));
    }

    // 오디오 클립을 이용한 bgm 변경
    public void BGMChange(AudioClip clip)
    {
        // 바꾸려는 음악이 같은 음악이 아니면 변경
        if (clip != source.clip)
        {
            StopAllCoroutines();
            StartCoroutine(ChangeBGM(clip));
        }
    }

    // 원래 bgm 으로 되돌리기
    public void PrevBGM()
    {
        // 되돌리는데 원래 음악이 아니면 변경
        if (bgm[bgmCount] != source.clip)
        {
            StopAllCoroutines();
            source.SetScheduledStartTime(pauseTime);
            StartCoroutine(ChangeBGM(bgmCount));
        }
    }

    public void NextBGM()
    {
            ++bgmCount;
            if (bgmCount >= bgm.Length)
                bgmCount = 0;
            
            StartCoroutine(ChangeBGM(bgmCount));
    }

    public void Play()
    {
        StartCoroutine(FadeIn(fliptime));
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
        pauseTime = source.time;
    }

    // 재생 시키면서 볼륨을 올린다.
    IEnumerator FadeIn(float time)
    {
        source.Play();
        while (source.volume < 1f)
        {
            source.volume += 0.1f;

            yield return new WaitForSecondsRealtime(time);
        }
    }

    IEnumerator ChangeBGM(AudioClip clip)
    {
        yield return FadeOut(fliptime);

        source.clip = clip;

        yield return FadeIn(fliptime);
    }

    IEnumerator ChangeBGM(int num)
    {
        yield return FadeOut(fliptime);

        source.clip = bgm[num];

        yield return FadeIn(fliptime);
    }
}
