using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragonConversations : MonoBehaviour
{
    [SerializeField] private ConversationObject[] possibleConvo;

    public TextMeshProUGUI questbox;
    public GameObject pearl;
    public GameObject speechBubble;
    public GameObject barrier;
    public GameObject[] areaActivators;
    public GameObject jiaorenQuestActivator;

    public Sprite happyDragon;

    public bool questComplete { get; private set; } = false;

    public void SelectConversation()
    {
        bool firstTime = gameObject.GetComponent<DialogueActivator>().firstInteract;
        bool hasItem = pearl.GetComponent<ItemControl>().hasPearl;

        switch(firstTime)
        {
            case true:
                //First time talking to the dragon (obtain pearl quest)
                gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[0];
                Destroy(barrier);
                jiaorenQuestActivator.SetActive(true);

                for(int i = 0; i < areaActivators.Length; i++)
                {
                    if(areaActivators[i] != null)
                    {
                        Destroy(areaActivators[i]);
                    }
                }

                questbox.text = "Find a pearl for the jiaolong";
                break;

            case false:
                if (hasItem)
                {
                    //when you have the pearl (quest complete)
                    gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[1];
                    gameObject.GetComponent<SpriteRenderer>().sprite = happyDragon;

                    //Changes in game sprites to reflect freedom 
                    questbox.text = "";
                    pearl.GetComponent<ItemControl>().UsedItem("Pearl");
                    Destroy(speechBubble);
                    gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    PlayerControl.instance.numTails = 6;

                    questComplete = true;
                }
                else
                {
                    //when there is no pearl, second+ time talking to them
                    gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[2];
                }
                break;
        }
    }
}
