using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TigerConversations : MonoBehaviour
{
    [SerializeField] private ConversationObject[] possibleConvo;
    private bool questComplete = false;
    public ConversationObject selectedConvo { get; private set; }
    public GameObject stick;
    public GameObject escapedRocks;
    public Sprite freeTiger;
    public GameObject speechBubble;

    public GameObject barrier;

    public bool stickUsed;

    private void Start()
    {
        escapedRocks.SetActive(false);
    }

    public void SelectConversation()
    {
        bool firstTime = gameObject.GetComponent<DialogueActivator>().firstInteract;
        bool hasItem = stick.GetComponent<ItemControl>().hasStick;
        switch (firstTime)
        {
            case true:
                gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[0];
                break;
            case false:
                if (questComplete)
                {
                    gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[3];
                    Destroy(gameObject);
                    Destroy(speechBubble);
                    Destroy(barrier);
                    PlayerControl.instance.numTails = 2;
                }
                else if (hasItem)
                {
                    //when you have the stick
                    gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[1];

                    //Changes in game sprites to reflect freedom 
                    gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.5f, 1);
                    gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.5f, -1);
                    questComplete = true;

                }
                else
                { 
                    //when there is no stick 
                    gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[2];
                }                
                break;
        }
            
    }

}
