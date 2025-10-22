using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    public ConversationObject conversation;
    [SerializeField] private bool isMultiDialogue;
    [SerializeField] private GameObject talkIndicator;

    //Final Cutscene use only
    [SerializeField] private ConversationObject[] finalCutscene;
    [SerializeField] private GameObject[] originalSprites;
    [SerializeField] private GameObject[] finalCutsceneSprites;
    [SerializeField] private bool hasWZQ;
    public GameObject[] barriers;
    public GameObject lionF;

    private int numInteract = 0;
    public bool firstInteract {  get; private set; }

    private void Start()
    {
        firstInteract = true;
        talkIndicator.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.TryGetComponent(out PlayerControl player))
        {
            player.Interactable = this;
            talkIndicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerControl player))
        {
            if(player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }
            talkIndicator.SetActive(false);
        }
    }

    public void Interact(PlayerControl player)
    {
        numInteract++;
        if(numInteract > 1)
        {
            firstInteract = false;
        }

        //This is so inefficient I'm sorry
        //Checks if it is multidialogue, then calls the select conversation for that specific NPC
        if (isMultiDialogue)
        {
            switch (gameObject.tag)
            {
                case "Tiger":
                    gameObject.GetComponent<TigerConversations>().SelectConversation();
                    break;
                case "Dragon":
                    gameObject.GetComponent<DragonConversations>().SelectConversation();
                    break;
                case "Jiaoren":
                    gameObject.GetComponent<JiaorenConversations>().SelectConversation();
                    break;
                case "Lion":
                    gameObject.GetComponent<LionConversations>().SelectConversation();
                    break;
                case "VillageLion":
                    gameObject.GetComponent<VillageLionConversations>().SelectConversation();
                    break;
                case "Wuzhiqi":
                    gameObject.GetComponent<WuzhiqiConversations>().SelectConversation();
                    break;
                case "PeiLiang":
                    gameObject.GetComponent<PeiLiangConversations>().SelectConversation();
                    break;
                case "XiaoMing":
                    gameObject.GetComponent<BulliesConversations>().SelectConversation();
                    break;
                case "XiaoQiu":
                    gameObject.GetComponent<XiaoQiuConversations>().SelectConversation();
                    break;
                case "ChenYeye":
                    gameObject.GetComponent<ChenYeyeConversations>().SelectConversation();
                    break;
                case "Leaf":
                    gameObject.GetComponent<LeafConversations>().SelectConversation();
                    break;
                case "Water":
                    gameObject.GetComponent<WaterStatueConversations>().SelectConversation();
                    break;
            }
        }

        if(TryGetComponent(out DialogueResponseEvents responseEvents))
        {
            DialogueUI.instance.AddResponseEvents(responseEvents.Events);
        }

        if (hasWZQ)
        {
            if (WuzhiqiConversations.instance.questComplete)
            {
                StartCoroutine(FinalCutscene());
                return;
            }
        }

        DialogueUI.instance.CallConversationCoroutine(conversation);
    }

    public void setInteractToZero()
    {
        numInteract = 0;
    }

    private IEnumerator FinalCutscene()
    {
        //Confrontation
        yield return DialogueUI.instance.StartConversation(finalCutscene[0]);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        //Fade into HeaveHo conversation
        yield return FadingScript.instance.FadeOutAndInHelper(new Vector3(1.15f, -11.96f, 8f));
        revealLayout(1);
        yield return DialogueUI.instance.StartConversation(finalCutscene[1]);

        //Fade into Drying Guardiaon lions
        yield return FadingScript.instance.FadeOutAndInHelper(new Vector3(-7.24f, 28.96f, 8f));
        revealLayout(2);
        yield return DialogueUI.instance.StartConversation(finalCutscene[2]);

        //Fade into everyone leaves are you talk to Guardian lions
        PlayerControl.instance.isHuman = false;
        PlayerControl.instance.shadows[0].SetActive(true);
        PlayerControl.instance.shadows[1].SetActive(false);
        lionF.SetActive(true);
        revealLayout(3);
        yield return FadingScript.instance.FadeOutAndInHelper(new Vector3(-4.46f, 29.28f, 8));
        yield return DialogueUI.instance.StartConversation(finalCutscene[3]);

        for(int i = 0; i < barriers.Length; i++)
        {
            Destroy(barriers[i]);
        }
        WuzhiqiConversations.instance.questComplete = false;
        VillageLionConversations.instance.finalCutsceneComplete = true;
        PlayerControl.instance.numTails = 9;
    }

    private void revealLayout(int index)
    {
        for(int i = 0; i < finalCutsceneSprites.Length; i++)
        {
            finalCutsceneSprites[i].gameObject.SetActive(false);
            if(index == i)
            {
                finalCutsceneSprites[i].gameObject.SetActive(true);
                
                if(index == 3)
                {
                    for (int j = 0; j < finalCutsceneSprites[i].transform.childCount; j++)
                    {
                        finalCutsceneSprites[i].transform.GetChild(j).gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
