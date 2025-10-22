using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionConversations : MonoBehaviour
{
    [SerializeField] private DialogueObject[] possibleDialogue;
    [SerializeField] private ConversationObject[] possibleConversation;

    public GameObject jiaoren;
    public GameObject dragon;

    public GameObject barrier;

    public void SelectConversation()
    {
        bool firstInteract = jiaoren.GetComponent<DialogueActivator>().firstInteract;
        bool jiaorenComplete = jiaoren.GetComponent<JiaorenConversations>().questComplete;
        bool dragonComplete = dragon.GetComponent<DragonConversations>().questComplete;

        if (firstInteract) //player has yet to talk to jiaoren and trigger their quest
        {
            possibleConversation[0].conversation[0].responses[0].dialogueObject = possibleDialogue[0];
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConversation[0];
        }
        else if (!jiaorenComplete) //checks if you have yet to complete the quest from the jiaoren
        {
            possibleConversation[0].conversation[0].responses[0].dialogueObject = possibleDialogue[1];
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConversation[0];
        }
        else if (!dragonComplete) //got the pearl but haven't given it to the dragon yet
        {
            possibleConversation[0].conversation[0].responses[0].dialogueObject = possibleDialogue[2];
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConversation[0];
        }
        else //when both quests are complete
        {
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConversation[1];
            Destroy(barrier);
        }
        

    }
}
