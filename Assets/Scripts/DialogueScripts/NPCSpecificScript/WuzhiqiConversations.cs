using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WuzhiqiConversations : MonoBehaviour
{
    public static WuzhiqiConversations instance;
    
    [SerializeField] private ConversationObject[] possibleConvo;
    public TextMeshProUGUI questbox;
    public GameObject speechBubble;

    public TextMeshProUGUI CYYquestbox;
    public GameObject leafPiles;
    public GameObject chenYeye;
    public GameObject bullies;
    public GameObject peiLiang;
    public bool questComplete = false;

    private bool firstTalkPostCYY = true;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SelectConversation()
    {
        bool firstTime = gameObject.GetComponent<DialogueActivator>().firstInteract;
        if(firstTime)
        {
            if (PlayerControl.instance.isHuman)
            {
                gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[4];
            }
            else
            {
                gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[0];
            }
            questbox.gameObject.SetActive(true);
            questbox.text = "Find a way to pull the statue out";
        }
        else
        {
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[1]; //second time

            if (chenYeye.GetComponent<ChenYeyeConversations>().questTriggered && firstTalkPostCYY)
            {
                if (PlayerControl.instance.isHuman)
                {
                    gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[2];
                }
                else
                {
                    gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[5];
                }
                leafPiles.gameObject.SetActive(true);
                CYYquestbox.text = "Find 3 mirror shards in the leaf pile";
                firstTalkPostCYY = false;
            }

            //Confront Wuzhiqi
            if(chenYeye.GetComponent<ChenYeyeConversations>().questComplete && bullies.GetComponent<BulliesConversations>().questComplete && peiLiang.GetComponent<PeiLiangConversations>().questComplete)
            {
                //This needs to trigger a coroutine think to make a sequence of events
                questbox.gameObject.SetActive(false);
                Destroy(speechBubble);
                gameObject.GetComponent<CircleCollider2D>().enabled = false;

                questComplete = true;
                gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[3];
            }
        }
    }
}
