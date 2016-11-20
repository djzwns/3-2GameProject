using UnityEngine;
using System.Collections;

public class ActiveConversation : MonoBehaviour {
    public float fTextPassTime;
    public Conversation conversation;
    
    bool bUsed = false;

    void OnTriggerEnter(Collider coll)
    {
        if (!bUsed && conversation != null && coll.tag == "Player")
        {
            ConversationManager.Instance.StartConversation(conversation, fTextPassTime);
            bUsed = true;
        }
    }
}
