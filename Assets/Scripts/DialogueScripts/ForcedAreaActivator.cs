using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ForcedAreaActivator : MonoBehaviour
{
    //conversation that activates when you run into the barrier
    public ConversationObject conversation;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(StartConversation());
    }

    private IEnumerator StartConversation()
    {
        DialogueUI.instance.CallConversationCoroutine(conversation);
        yield return null;
    }
}
