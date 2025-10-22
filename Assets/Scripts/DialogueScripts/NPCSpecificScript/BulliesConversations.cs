using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulliesConversations : MonoBehaviour
{
    [SerializeField] private ConversationObject[] possibleConvo;

    public TextMeshProUGUI questbox;
    public TextMeshProUGUI mainQuest;
    public GameObject speechBubble;
    public GameObject XQOriginal;
    public GameObject XQConfrontation;
    public GameObject BulliesOriginal;
    public GameObject BulliesConfrontation;

    public GameObject chenYeye;
    public GameObject peiLiang;
    public GameObject barrier;


    public bool questComplete = false;

    public void SelectConversation()
    {
        //Checks if human
        if (!PlayerControl.instance.isHuman)
        {
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[2];
            return;
        }

        //Checks for other dialogue if not human
        if (XQOriginal.GetComponent<XiaoQiuConversations>().questComplete) //If finished comforting XQ
        {
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[0];
            questComplete = true;
            Destroy(speechBubble);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            questbox.gameObject.SetActive(false);

            XQOriginal.SetActive(false);
            BulliesOriginal.SetActive(false);
            XQConfrontation.SetActive(true);
            BulliesConfrontation.SetActive(true);
        }
        else
        {
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[1];
        }

        if (chenYeye.GetComponent<ChenYeyeConversations>().questComplete && questComplete && peiLiang.GetComponent<PeiLiangConversations>().questComplete)
        {
            mainQuest.text = "Confront Wuzhiqi";
            barrier.SetActive(true);
        }
    }
}
