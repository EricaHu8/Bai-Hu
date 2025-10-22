using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionForcedAreaActivator : MonoBehaviour
{
    //conversation that activates when you run into the barrier
    public ConversationObject[] possibleConvos;
    private ConversationObject conversation;

    public GameObject jiaoren;
    public GameObject dragon;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SelectConversation();
        StartCoroutine(StartConversation());
    }

    public void SelectConversation()
    {
        bool jiaorenComplete = jiaoren.GetComponent<JiaorenConversations>().questComplete;
        bool dragonComplete = dragon.GetComponent<DragonConversations>().questComplete;

        if(jiaorenComplete && dragonComplete)
        {
            conversation = possibleConvos[1];
        }
        else
        {
            conversation = possibleConvos[0];
        }
    }

    private IEnumerator StartConversation()
    {
        DialogueUI.instance.CallConversationCoroutine(conversation);
        yield return null;
    }
}
