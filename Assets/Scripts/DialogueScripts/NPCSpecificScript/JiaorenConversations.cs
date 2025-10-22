using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JiaorenConversations : MonoBehaviour
{
    [SerializeField] private ConversationObject[] possibleConvo;

    public TextMeshProUGUI questbox;
    public GameObject silkworm;
    public GameObject pearl;
    public GameObject speechBubble;

    public bool questComplete { get; private set; } = false;   

    public void SelectConversation()
    {
        bool firstTime = gameObject.GetComponent<DialogueActivator>().firstInteract;
        bool hasItem = ItemControl.treeCount >= 3;

        switch (firstTime)
        {
            case true:
                //First time talking to the jiaoren (updates crying quest)
                gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[0];
                questbox.text = "Get 3 silkworm cocoons for the jiaoren";
                break;

            case false:
                if (hasItem)
                {
                    //when you have the silkworm (quest complete)
                    gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[1];
                    pearl.GetComponent<ItemControl>().OnButtonClick();

                    //Changes in game to reflect this 
                    questbox.text = "";
                    silkworm.GetComponent<ItemControl>().UsedItem("SilkObject");
                    gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    Destroy(speechBubble);
                    PlayerControl.instance.numTails = 4;

                    questComplete = true;
                }
                else
                {
                    //when there is no silkworms, second+ time talking to them
                    gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[2];
                }
                break;
        }
    }
}
