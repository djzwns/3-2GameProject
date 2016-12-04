using UnityEngine;
public class GameManager : Singleton<GameManager> {
    public Texture[] lifeIcon;

    Player player;
    ToyManager tm;
    StageManager sm;

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

    bool isPause = false;
    public GUIStyle style;

    void Start()
    {
        player = ScriptableObject.CreateInstance<Player>();//new Player();
        tm = ToyManager.Instance;
        sm = StageManager.Instance;
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

    void OnGUI()
    {
        if (!sm.bEnd)
        {
            // 플레이어 체력 표시
            for (int i = 0; i < player.MaxLife; ++i)
            {
                if (i < player.CurrentLife)
                    GUI.DrawTexture(new Rect(30 + (50 * i), 30, 50, 50), lifeIcon[0]);
                else
                    GUI.DrawTexture(new Rect(30 + (50 * i), 30, 50, 50), lifeIcon[1]);
            }

            if (isPause)
            {
                GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height), style);
                GUI.EndGroup();
            }
        }
        if (tm.iPiecesCount > 0 && player.CurrentLife <= 0)
        {
            PlayerAnimManager.Instance.Die();
            PlayerController.Instance.Die();

            sm.Fail();
        }
    }
}
