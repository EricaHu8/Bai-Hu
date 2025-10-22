using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class XiaoQiuConversations : MonoBehaviour
{
    [SerializeField] private ConversationObject[] possibleConvo;
    
    public TextMeshProUGUI questbox;
    public GameObject speechBubble;
    public Sprite happyXQ;
    public bool questComplete { get; private set; } = false; //This is specifically if you comforted her properly

    public void SelectConversation()
    {
        if (PlayerControl.instance.isHuman)
        {
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[0];
        }
        else
        {
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[1];
            gameObject.GetComponent<SpriteRenderer>().sprite = happyXQ;
            PlayerControl.instance.isHuman = true;
            questComplete = true;
            Destroy(speechBubble);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            questbox.text = "Talk to Xiao Ming";
        }
    }
}
