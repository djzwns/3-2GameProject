using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {
    private int iPiecesOfToy;
    private int iToyCount;

    public Texture[] lifeIcon;

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

    void Start()
    {
        iPiecesOfToy = FindObjectsOfType(typeof(ToyPiece)).Length;
        iToyCount = iPiecesOfToy;
        player = ScriptableObject.CreateInstance<Player>();//new Player();
    }

    void Update()
    {
        Borodar.FarlandSkies.LowPoly.SkyboxController.Instance.CloudsRotation = Time.time * fCloudSpeed; 
    }

    public void CollectToy()
    {
        if (iToyCount > 0)
            --iToyCount;
    }

    void OnGUI()
    {
        // 플레이어 체력 표시
        for (int i = 0; i < player.MaxLife; ++i)
        {
            if(i < player.CurrentLife)
                GUI.DrawTexture(new Rect(30 + (50 * i), 30, 50, 50), lifeIcon[0]);
            else
                GUI.DrawTexture(new Rect(30 + (50 * i), 30, 50, 50), lifeIcon[1]);
        }

        // 장난감 조각 현황?
        GUI.Box(new Rect(Screen.width-150, 30, 120, 30), iPiecesOfToy - iToyCount + "/" + iPiecesOfToy);

        // clear 시
        if (iToyCount == 0)
        {
            GUI.Box(new Rect(Screen.width * 0.5f - 60, Screen.height * 0.5f, 120, 30), "Clear");
        }
        else if (player.CurrentLife <= 0)
        {
            GUI.Box(new Rect(Screen.width * 0.5f - 60, Screen.height * 0.5f, 120, 30), "Failed");
        }
    }
}
