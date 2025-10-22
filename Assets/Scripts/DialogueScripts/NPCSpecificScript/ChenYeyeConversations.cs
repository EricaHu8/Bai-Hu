using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChenYeyeConversations : MonoBehaviour
{
    [SerializeField] private ConversationObject[] possibleConvo;

    public TextMeshProUGUI questbox;
    public TextMeshProUGUI mainQuest;
    public GameObject speechBubble;
    public GameObject mirror;
    public GameObject peiLiang;
    public GameObject bullies;
    public GameObject barrier;

    public bool questComplete = false;
    public bool questTriggered = false;

    public GameObject CYYOriginal;
    public GameObject CYYConfrontation;

    public void SelectConversation()
    {
        if (!PlayerControl.instance.isHuman)
        {
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[3];
            gameObject.GetComponent<DialogueActivator>().setInteractToZero();
            return;
        }

        bool firstTime = gameObject.GetComponent<DialogueActivator>().firstInteract;

        if (firstTime)
        {
            questTriggered = true;
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[0];
            questbox.gameObject.SetActive(true);
            questbox.text = "Confront the mirror thief";
        }
        else
        {
            if (mirror.GetComponent<ItemControl>().hasMirror)
            {
                mirror.GetComponent<ItemControl>().UsedItem("Mirror");
                gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[1];
                questComplete = true;
                Destroy(speechBubble);
                questbox.gameObject.SetActive(false);
                gameObject.GetComponent<CircleCollider2D>().enabled = false;

                CYYOriginal.SetActive(false);
                CYYConfrontation.SetActive(true);
            }
            else
            {
                gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[2];
            }
        }

        if (questComplete && bullies.GetComponent<BulliesConversations>().questComplete && peiLiang.GetComponent<PeiLiangConversations>().questComplete)
        {
            mainQuest.text = "Confront Wuzhiqi";
            barrier.SetActive(true);
        }

    }
}
