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

    float lifeIconSize;

    float UIMarginW;
    float UIMarginH;

    float toybarWidth;
    float toybarHeight;

    void Start()
    {
        tm = ToyManager.Instance;
        sm = StageManager.Instance;
        gm = GameManager.Instance;

        player = gm.getplayer;
        
        buttonWidth = Screen.width * 0.172f;
        buttonHeight = Screen.height * 0.083f;

        lifeIconSize = Screen.width * 0.08f;

        UIMarginW = Screen.width * 0.016f;
        UIMarginH = Screen.height * 0.016f;

        toybarWidth = Screen.width * 0.25f;
        toybarHeight = Screen.height * 0.2f;

        toyBar.fontSize = (int)(Screen.width * 0.034f);
        toyBar.contentOffset = new Vector2(-UIMarginW, 0);
    }

    void OnGUI()
    {
        if (!sm.bEnd)
        {
            // 플레이어 체력 표시
            for (int i = 0; i < player.MaxLife; ++i)
            {
                if (i < player.CurrentLife)
                    GUI.DrawTexture(new Rect(UIMarginW + (lifeIconSize * i), UIMarginH, lifeIconSize, lifeIconSize), lifeIcon[0]);
                else
                    GUI.DrawTexture(new Rect(UIMarginW + (lifeIconSize * i), UIMarginH, lifeIconSize, lifeIconSize), lifeIcon[1]);
            }

            // 장난감 조각 상황 표시
            GUI.BeginGroup(new Rect(Screen.width - toybarWidth - UIMarginW, UIMarginH, toybarWidth, toybarHeight));
            GUI.Label(new Rect(0, 0, toybarWidth, toybarHeight), tm.iPiecesOfToy - tm.iPiecesCount + " / " + tm.iPiecesOfToy, toyBar);
            GUI.EndGroup();

            // 일시정지시 UI
            if (gm.isPause)
            {
                GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height), style);
                if (GUI.Button(new Rect(Screen.width * 0.5f - buttonWidth * 1.3f, Screen.height * 0.5f + buttonHeight * 1.5f, buttonWidth, buttonHeight), "메인메뉴", button))
                {
                    sm.GotoMain();
                }
                if (GUI.Button(new Rect(Screen.width * 0.5f + buttonWidth * 0.2f, Screen.height * 0.5f + buttonHeight * 1.5f, buttonWidth, buttonHeight), "계속하기", button))
                {
                    gm.Play();
                }
                GUI.EndGroup();
            }
        }
    }
}
