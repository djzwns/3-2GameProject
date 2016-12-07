using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    Player player;

    public Player getplayer
    {
        get
        {
            if(player != null)
                return player;
            return null;
        }
    }

    public float fCloudSpeed = 1f;

    public bool isPause { get; private set; }

    void Awake()
    {
        player = ScriptableObject.CreateInstance<Player>();//new Player();
        BGMManager.Instance.BGMChange(SceneManager.GetActiveScene().buildIndex);   // 1번은 메인화면 인덱스 번호

        isPause = false;
    }

    void Update()
    {
        Borodar.FarlandSkies.LowPoly.SkyboxController.Instance.CloudsRotation = Time.time * fCloudSpeed;

        if (Input.GetKeyDown(KeyCode.Escape))
            isPause = !isPause;


        // 게임 일시정지.
        if (isPause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void Play()
    {
        isPause = false;
    }
}
