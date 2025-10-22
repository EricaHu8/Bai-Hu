using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafConversations : MonoBehaviour
{
    [SerializeField] private ConversationObject[] possibleConvo;

    [SerializeField] private bool containsShard;
    public GameObject mirror;
    public GameObject shard;

    public void SelectConversation()
    {
        if (PlayerControl.instance.isHuman)
        {
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[0];
            if(shard  != null )
                shard.GetComponent<ItemControl>().OnButtonClick();
        }
        else
        {
            gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[1];
            if(shard != null )  
                shard.GetComponent<ItemControl>().OnButtonClick();
        }

        if (containsShard)
        {
            if(ItemControl.shardCount >= 3)
            {
                shard.GetComponent<ItemControl>().UsedItem("Shard");
                mirror.GetComponent<ItemControl>().OnButtonClick();
                if (PlayerControl.instance.isHuman)
                {
                    gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[2];
                }
                else
                {
                    gameObject.GetComponent<DialogueActivator>().conversation = possibleConvo[3];
                }
            }
        }
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
}
