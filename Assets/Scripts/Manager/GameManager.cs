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
    public GUIStyle button;
    float buttonWidth;
    float buttonHeight;

    void Start()
    {
        player = ScriptableObject.CreateInstance<Player>();//new Player();
        tm = ToyManager.Instance;
        sm = StageManager.Instance;

        buttonWidth = Screen.width * 0.172f;
        buttonHeight = Screen.height * 0.083f;
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
                if (GUI.Button(new Rect(Screen.width * 0.5f - buttonWidth * 1.3f, Screen.height * 0.5f + buttonHeight + 33f, buttonWidth, buttonHeight), "메인메뉴", button))
                {
                    sm.GotoMain();
                }
                if (GUI.Button(new Rect(Screen.width * 0.5f + buttonWidth * 0.2f, Screen.height * 0.5f + buttonHeight + 33f, buttonWidth, buttonHeight), "계속하기", button))
                {
                    isPause = false;
                }
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
