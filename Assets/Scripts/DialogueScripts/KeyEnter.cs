using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyEnter : MonoBehaviour
{
    public static KeyEnter instance;
    bool canPress;
    float cooldownTime = 0.5f;
    float timeSinceLastInput = 0f;

    private void Start()
    { 
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (DialogueUI.instance.IsOpen)
        {
            ResponseHandler instance = ResponseHandler.instance;
            List<GameObject> responses = instance.tempResponseButtons;

            if(responses.Count != 0)
            {
                timeSinceLastInput += Time.deltaTime;

                if(!canPress && timeSinceLastInput >= cooldownTime)
                {
                    canPress = true;
                }

                if (Input.GetKeyDown(KeyCode.Space) && canPress)
                {
                    responses[instance.responseIndex].GetComponent<Button>().onClick.Invoke();
                    canPress = false;
                    timeSinceLastInput = 0f;
                }
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (responses.Count - 1 > instance.responseIndex)
                {
                    instance.responseIndex++;
                    SelectIndicator(responses, instance.responseIndex);
                }
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (instance.responseIndex > 0)
                {
                    instance.responseIndex--;
                    SelectIndicator(responses, instance.responseIndex);
                }
            }
        }
    }

    public void SelectIndicator(List<GameObject> responses, int index)
    {
        for(int i = 0; i < responses.Count; i++)
        {
            responses[i].GetComponent<TextMeshProUGUI>().text = responses[i].GetComponent<TextMeshProUGUI>().text.Replace("> ", "");

            if (i == index)
            {
                responses[i].GetComponent<TextMeshProUGUI>().text = "> " + responses[i].GetComponent<TextMeshProUGUI>().text;
            }
        }
    }
}
