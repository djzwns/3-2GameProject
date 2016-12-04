using UnityEngine;
using System.Collections;

public class ConversationManager : Singleton<ConversationManager> {

    public GUIStyle style;

    bool bTalking = false;
    bool bNextText = false;

    ConversationBase currentConversation;
    string oldFuncName;
    public string leftName;

    int iTextboxWidth = Screen.width;
    int iTextboxHeight = (int)(Screen.height * 0.25f);
    int iNameTextLength;
    int iLineNumber = 0;

    PlayerController player;

    void Awake()
    {
        player = PlayerController.Instance;
    }

    void Update()
    {
        bNextText = Input.GetButtonUp("Jump");

        if (bNextText)
        {
            ++iLineNumber;
        }
    }

    // 외부에서 대화 시작할 때 사용하는 함수, 코루틴 실행시킴
    public void StartConversation(Conversation conversation, float textPassTime = 0f)
    {
        if (textPassTime == 0)
        {
            StartCoroutine(DisplayConversation(conversation));
        }
        else
            StartCoroutine(DisplayConversation(conversation, textPassTime));
    }

    // 시간따라 대화가 넘어가는 대화창
    IEnumerator DisplayConversation(Conversation conversation, float textPassTime)
    {
        player.bTalking = bTalking = true;
        foreach (var conversationLine in conversation.conversationLines)
        {
            currentConversation = conversationLine;
            iNameTextLength = currentConversation.SpeakingCharacterName.Length * 7;
            
            Disaster(currentConversation.FunctionName);

            yield return new WaitForSeconds(textPassTime);
        }
        player.bTalking = bTalking = false;
    }

    // 버튼 입력식
    IEnumerator DisplayConversation(Conversation conversation)
    {
        player.bTalking = bTalking = true;
        iLineNumber = 0;
        while (iLineNumber < conversation.conversationLines.Length && !Input.GetKeyDown(KeyCode.Escape))
        {
            currentConversation = conversation.conversationLines[iLineNumber];
            iNameTextLength = currentConversation.SpeakingCharacterName.Length * 7;

            Disaster(currentConversation.FunctionName);

            yield return new WaitForFixedUpdate();
        }

        player.bTalking = bTalking = false;
    }

    // 악몽 환경변화 이벤트
    void Disaster(string disaster)
    {
        if (oldFuncName != disaster)
        {
            switch (disaster)
            {
                case "Quake":
                    NightmareManager.Instance.Quake(); break;

                case "HellFire":
                    NightmareManager.Instance.HellFire(); break;

                case "Water":
                    NightmareManager.Instance.Swell(); break;

                default: break;
            }
            oldFuncName = disaster;
        }
    }

    void OnGUI()
    {
        if (bTalking)
        {
            float textureWidth = currentConversation.DisplayTexture.width;
            float textureHeight = currentConversation.DisplayTexture.height;

            // 이미지 띄우기
            if (currentConversation.SpeakingCharacterName != leftName)
                GUI.Label(new Rect(iTextboxWidth - textureWidth, Screen.height - (iTextboxHeight + textureHeight), textureWidth, textureHeight), currentConversation.DisplayTexture);
            else
                GUI.Label(new Rect(0, Screen.height - (iTextboxHeight + textureHeight), textureWidth, textureHeight), currentConversation.DisplayTexture);

            // 대화창 그룹
            GUI.BeginGroup(new Rect(0, Screen.height - iTextboxHeight, iTextboxWidth, iTextboxHeight), style);

            // 대화창 뒷배경
            GUI.Box(new Rect(0, 0, iTextboxWidth, iTextboxHeight), "");

            // 이름
            GUI.Label(new Rect(30, 20, iNameTextLength * 7f, 20), currentConversation.SpeakingCharacterName);

            // 대화
            GUI.Label(new Rect(50, 40, iTextboxWidth, 20), currentConversation.ConversationText);

            GUI.EndGroup();
        }
        else
        {
            StopCoroutine("DisplayConversation");
        }
    }
}
