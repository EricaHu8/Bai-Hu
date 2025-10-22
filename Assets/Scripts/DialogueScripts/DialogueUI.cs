using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI instance;
    [SerializeField] private GameObject fox;

    public bool IsOpen { get; private set; }

    //Objects to set active (or not set active)
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject spritePanel;

    //All the dialogue text boxes
    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] private TextMeshProUGUI nameBox;
    [SerializeField] private TextMeshProUGUI titleBox;

    [SerializeField] private GameObject continueBox;
    [SerializeField] private GameObject pickResponseBox;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start() { 
        CloseDialogueBox();
    }

    //This is just to keep more coroutines private
    public void CallConversationCoroutine(ConversationObject conversation)
    {
        StartCoroutine(StartConversation(conversation));
    }

    public IEnumerator StartConversation(ConversationObject conversation)
    {
        IsOpen = true;
        fox.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        dialoguePanel.SetActive(true);
        spritePanel.SetActive(true);
        foreach (DialogueObject dialogue in conversation.Conversation)
        {
            yield return StartCoroutine(ShowDialogue(dialogue));
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        CloseDialogueBox();
    }
    private IEnumerator ShowDialogue(DialogueObject dialogueObject)
    {
        ChangeSprite(dialogueObject);
        yield return StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public IEnumerator ShowDialogue(DialogueObject dialogueObject, bool isResponse)
    {
        ChangeSprite(dialogueObject);
        yield return StartCoroutine(StepThroughDialogue(dialogueObject));
        ResponseHandler.instance.responseChosen = isResponse;
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        ResponseHandler.instance.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        string charName = dialogueObject.CharName;
        string charTitle = dialogueObject.CharTitle;
        continueBox.SetActive(true);
        pickResponseBox.SetActive(false);


        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return RunTypingEffect(dialogue, charName, charTitle);

            dialogueBox.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses)
            {
                break;
            }

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); //Waits until user presses key to go to next dialogue
        }

        if (dialogueObject.HasResponses)
        {
            continueBox.SetActive(false);
            pickResponseBox.SetActive(true);

            //Making Bai Hu's name, title, and sprite show up for responses
            TypewriterEffect.instance.SetText("Bai Hu", nameBox);
            TypewriterEffect.instance.SetText("White Fox", titleBox);

            DialogueObject responseChar = dialogueObject.responses[0].dialogueObject;

            if (responseChar.CharName != "Bai Hu")
            {
                if (PlayerControl.instance.isHuman)
                {
                    spritePanel.GetComponent<Image>().sprite = Resources.Load<Sprite>("baihuhuman-ink");
                }
                else
                {
                    switch (fox.GetComponent<PlayerControl>().numTails)
                    {
                        case 1:
                            spritePanel.GetComponent<Image>().sprite = Resources.Load<Sprite>("baihu1-ink");
                            break;
                        case 2:
                            spritePanel.GetComponent<Image>().sprite = Resources.Load<Sprite>("baihu2-ink");
                            break;
                        case 4:
                            spritePanel.GetComponent<Image>().sprite = Resources.Load<Sprite>("baihu4-ink");
                            break;
                        case 6:
                            spritePanel.GetComponent<Image>().sprite = Resources.Load<Sprite>("baihu6-ink");
                            break;
                        case 9:
                            spritePanel.GetComponent<Image>().sprite = Resources.Load<Sprite>("baihu9-ink");
                            break;
                    }
                }
            }
            else
            {
                spritePanel.GetComponent<Image>().sprite = dialogueObject.responses[0].dialogueObject.CharSprite;
            }

            ResponseHandler.instance.ShowReponses(dialogueObject.Responses);
            yield return new WaitUntil(() => ResponseHandler.instance.responseChosen);
        }
    }

    private IEnumerator RunTypingEffect(string toType, string charName, string charTitle)
    {
        TypewriterEffect.instance.Run(toType, dialogueBox, charName, nameBox, charTitle, titleBox);

        while (TypewriterEffect.instance.isRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                TypewriterEffect.instance.Stop();
            }
        }
    }

    private void ChangeSprite(DialogueObject dialogueObject)
    {
        Image image = spritePanel.GetComponent<Image>();
        if (dialogueObject.CharSprite != null)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            image.sprite = dialogueObject.CharSprite;
        }
    }

    private void CloseDialogueBox()
    {
        IsOpen = false;
        fox.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        fox.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        dialoguePanel.SetActive(false);
        spritePanel.SetActive(false);
        TypewriterEffect.instance.Run(string.Empty, dialogueBox, string.Empty, nameBox, string.Empty, titleBox); //Making all textboxes empty
        spritePanel.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
    }
}
