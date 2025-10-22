using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanForcedAreaActivator : MonoBehaviour
{
    public ConversationObject conversation;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(StartConversation());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(PlayerControl.instance.isHuman)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private IEnumerator StartConversation()
    {
        DialogueUI.instance.CallConversationCoroutine(conversation);
        yield return null;
    }
}
