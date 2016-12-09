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

    ToyManager tm;
    StageManager sm;
    Borodar.FarlandSkies.LowPoly.SkyboxController skyboxCtrl;

    void Awake()
    {
        player = ScriptableObject.CreateInstance<Player>();//new Player();

        // 씬이 시작될 때 마다 bgm 변경
        BGMManager.Instance.BGMChange(SceneManager.GetActiveScene().buildIndex);

        isPause = false;

        tm = ToyManager.Instance;
        sm = StageManager.Instance;
        skyboxCtrl = Borodar.FarlandSkies.LowPoly.SkyboxController.Instance;
    }

    void Update()
    {
        skyboxCtrl.CloudsRotation = Time.time * fCloudSpeed;

        if (Input.GetKeyDown(KeyCode.Escape))
            isPause = !isPause;


        // 게임 일시정지.
        if (isPause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        if (tm.iPiecesCount > 0 && player.CurrentLife <= 0)
        {
            PlayerAnimManager.Instance.Die();
            PlayerController.Instance.Die();

            sm.Fail();
        }
    }

    public void Play()
    {
        isPause = false;
    }
}
