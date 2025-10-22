using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PeiLiangConversations : MonoBehaviour
{
    [SerializeField] private ConversationObject[] possibleConvo;
    public TextMeshProUGUI mainQuest;
    public TextMeshProUGUI questbox;
    public GameObject speechBubble;
    public GameObject chenYeye;
    public GameObject bullies;
    public GameObject barrier;
    public int rude = 0;
    public int teaching = 0;
    public int shy = 0;

    public bool questComplete = false;
    public Sprite fallen;

    public GameObject PLConfrontation;
    public GameObject PLOriginal;

    /*
    Start location: -6.96, 7.29, 1
    Woodcutter's location: 6.83, 19.95, 1
    Ginkgo Tree location: -6.4, 14.11, 1
     */

    public void SelectConversation() 
    {
        if (!PlayerControl.instance.isHuman)
        {
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[4];
            return;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = fallen;
            questbox.text = "Talk to Pei Liang again";
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[5];
        }

        if (rude + teaching + shy == 3) //this is to set the ending for him
        {
            if(rude >= 2)
            {
                gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[0];
            }
            else if(teaching >= 2)
            {
                gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[1];
            }
            else if(shy >= 2)
            {
                gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[2];
            }
            else
            {
                gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[3];
            }
            Destroy(speechBubble);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            questbox.gameObject.SetActive(false);
            questComplete = true;
            PLConfrontation.SetActive(true);
            PLOriginal.SetActive(false);
        }

        if (chenYeye.GetComponent<ChenYeyeConversations>().questComplete && bullies.GetComponent<BulliesConversations>().questComplete && questComplete)
        {
            mainQuest.text = "Confront Wuzhiqi";
            barrier.SetActive(true);
        }
    }

    public void RouteChoice(string route)
    {
        switch (route)
        {
            case "Rude":
                rude++;
                break;
            case "Teach":
                teaching++;
                break;
            case "Shy":
                shy++;
                break;
        }
    }
}
