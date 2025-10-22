using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageLionConversations : MonoBehaviour
{
    public static VillageLionConversations instance;
    [SerializeField] private ConversationObject[] possibleConversation;

    public bool finalCutsceneComplete = false;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void SelectConversation()
    {
        if (!finalCutsceneComplete)
        {
            if (PlayerControl.instance.isHuman)
            {
                gameObject.GetComponent<DialogueActivator>().conversation = possibleConversation[0];
            }
            else
            {
                gameObject.GetComponent<DialogueActivator>().conversation = possibleConversation[1];
            }
        }
        else
        {
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConversation[2];
        }
    }
}
