using UnityEngine;
using System.Collections;

public class GameSceneUI : MonoBehaviour
{
    StageManager sm;
    ToyManager tm;
    GameManager gm;
    Player player;

    public Texture[] lifeIcon;
    public GUIStyle toyBar;
    public GUIStyle style;
    public GUIStyle button;

    float buttonWidth;
    float buttonHeight;

    void Start()
    {
        tm = ToyManager.Instance;
        sm = StageManager.Instance;
        gm = GameManager.Instance;

        player = gm.getplayer;
        
        buttonWidth = Screen.width * 0.172f;
        buttonHeight = Screen.height * 0.083f;
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

            float fWidth = Screen.width * 0.25f;
            float fHeight = Screen.height * 0.2f;
            GUI.BeginGroup(new Rect(Screen.width - fWidth - 10f, 10f, fWidth, fHeight));
            GUI.Label(new Rect(0, 0, fWidth, fHeight), tm.iPiecesOfToy - tm.iPiecesCount + " / " + tm.iPiecesOfToy, toyBar);
            GUI.EndGroup();

            if (gm.isPause)
            {
                GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height), style);
                if (GUI.Button(new Rect(Screen.width * 0.5f - buttonWidth * 1.3f, Screen.height * 0.5f + buttonHeight + 33f, buttonWidth, buttonHeight), "메인메뉴", button))
                {
                    sm.GotoMain();
                }
                if (GUI.Button(new Rect(Screen.width * 0.5f + buttonWidth * 0.2f, Screen.height * 0.5f + buttonHeight + 33f, buttonWidth, buttonHeight), "계속하기", button))
                {
                    gm.Play();
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
