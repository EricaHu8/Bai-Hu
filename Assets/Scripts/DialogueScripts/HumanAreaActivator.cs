using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HumanAreaActivator : MonoBehaviour
{
    public ConversationObject conversation;
    public bool updateQuestBox;
    public TextMeshProUGUI questBox;
    public string quest;

    public GameObject areaActivator;
    public GameObject peiLiang;
    private static int PLnext = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerControl.instance.isHuman)
        {
            StartCoroutine(StartConversation());

            if (TryGetComponent(out DialogueResponseEvents responseEvents))
            {
                DialogueUI.instance.AddResponseEvents(responseEvents.Events);
            }

            if(gameObject.tag == "XiaoQiu")
            {
                Destroy(areaActivator);
            }

            if (peiLiang != null)
            {
                if(areaActivator != null)
                {
                    areaActivator.SetActive(true);
                }
                PLnext++;
                switch (PLnext)
                {
                    case 1:
                        peiLiang.transform.position = new Vector3(6.83f, 19.95f, 1.0f);
                        break;
                    case 2:
                        peiLiang.transform.position = new Vector3(-6.4f, 14.11f, 1.0f);
                        break;
                }
            }
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
            questBox.gameObject.SetActive(true);
            questBox.text = quest;
        }
        DialogueUI.instance.CallConversationCoroutine(conversation);
        Destroy(gameObject);
    }
}
