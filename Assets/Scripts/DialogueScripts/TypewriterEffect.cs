using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public static TypewriterEffect instance;
    
    [SerializeField] private float typingSpeed = 50f;
    public bool isRunning { get; private set; }
    private Coroutine typingCoroutine;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void Run(string toType, TextMeshProUGUI dialogueBox, string charName, TextMeshProUGUI nameBox, string charTitle, TextMeshProUGUI titleBox)
    {
        typingCoroutine = StartCoroutine(TypeText(toType, dialogueBox, charName, nameBox, charTitle, titleBox));

    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        isRunning = false;
    }

    //Just to set the text (no typing)
    public void SetText (string toSet, TextMeshProUGUI textbox)
    {
        textbox.text = toSet;
    }

    //To type out the text
    private IEnumerator TypeText(string toType, TextMeshProUGUI dialogueBox, string charName, TextMeshProUGUI nameBox, string charTitle, TextMeshProUGUI titleBox)
    {
        isRunning = true;
        float t = 0;
        int index = 0;

        //Setting the non-typed parts of the dialogue
        SetText(charName, nameBox);
        SetText(charTitle, titleBox);

        while (index < toType.Length)
        {
            t += Time.deltaTime * typingSpeed;
            index = Mathf.FloorToInt(t);
            index = Mathf.Clamp(index, 0, toType.Length);

            dialogueBox.text = toType.Substring(0, index);

            yield return null;
        }

        isRunning = false;
    }
}
