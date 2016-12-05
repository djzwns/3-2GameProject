using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BGMManager : Singleton<BGMManager> {
    int iCurrentBgm;

    public AudioClip[] bgm;
    AudioSource source;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        iCurrentBgm = SceneManager.GetActiveScene().buildIndex;

        source = GetComponent<AudioSource>();
        source.clip = bgm[iCurrentBgm];
        source.Play();
    }

    public void BGMChange(int num)
    {
        iCurrentBgm = num;

        
    }
}
