using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStatueConversations : MonoBehaviour
{
    [SerializeField] private ConversationObject[] possibleConvo;

    public void SelectConversation()
    {
        if (PlayerControl.instance.isHuman)
        {
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[0];
        }
        else
        {
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[1];
        }
    }
}
