using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    public Texture[] lifeIcon;

    Player player;
    ToyManager tm;

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
        player = ScriptableObject.CreateInstance<Player>();//new Player();
        tm = ToyManager.Instance;
    }

    void Update()
    {
        Borodar.FarlandSkies.LowPoly.SkyboxController.Instance.CloudsRotation = Time.time * fCloudSpeed; 
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
        if (tm.iPiecesCount > 0 && player.CurrentLife <= 0)
        {
            PlayerAnimManager.Instance.Die();
            PlayerController.Instance.Die();
            if(GUI.Button(new Rect(Screen.width * 0.5f - 60, Screen.height * 0.5f, 120, 30), "Failed"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
