using UnityEngine;
using UnityEditor;

/*
    MenuItem 을 이용해서 유니티 메뉴에 추가.
    Conversation 애셋을 만든다.
    CustomAssetUtility : 에디터 스크립트, 위키 유니티 참고해서 만들었음.
*/
public class ConversationCreator : MonoBehaviour {

    [MenuItem("Assets/Create/Conversation")]
    public static void CreateAsset()
    {
        CustomAssetUtility.CreateAsset<Conversation>();
    }
}
