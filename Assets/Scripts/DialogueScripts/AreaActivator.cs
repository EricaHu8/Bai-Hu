using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AreaActivator : MonoBehaviour
{
    public ConversationObject conversation;
    public bool updateQuestBox;
    public TextMeshProUGUI questBox;
    public string quest;

    public GameObject areaActivator;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(StartConversation());

        if (gameObject.tag == "XiaoQiu")
        {
            Destroy(areaActivator);
        }
    }

    private IEnumerator StartConversation()
    {
        if (gameObject.tag == "Start")
        {
            yield return new WaitForSeconds(1);
        }
        else if (updateQuestBox)
        {
            questBox.text = quest;
        }
        DialogueUI.instance.CallConversationCoroutine(conversation);
        Destroy(gameObject);
    }
}
