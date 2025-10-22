using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResponseHandler : MonoBehaviour
{
    public static ResponseHandler instance;

    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    [SerializeField] public List<GameObject> tempResponseButtons = new List<GameObject>();
    private ResponseEvent[] responseEvents;

    public int responseIndex;
    public bool responseChosen = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }

    public void ShowReponses(Response[] responses)
    {
        responseChosen = false;
        float responseBoxHeight = 0f;

        for (int i = 0; i < responses.Length; i++)
        {
            Response response = responses[i];
            responseIndex = i;

            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TextMeshProUGUI>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response, responseIndex));

            tempResponseButtons.Add(responseButton);

            //To start the selection indicator on the first response
            KeyEnter.instance.SelectIndicator(tempResponseButtons, 0);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseIndex = 0;

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response, int responseIndex)
    {
        responseBox.gameObject.SetActive(false);
        
        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }

        tempResponseButtons = new List<GameObject>();

        if (responseEvents != null && responseIndex <= responseEvents.Length)
        {
            responseEvents[responseIndex].OnPickedResponse?.Invoke();
        } 

        responseEvents = null;

        StartCoroutine(DialogueUI.instance.ShowDialogue(response.DialogueObject, true));
    }
}
