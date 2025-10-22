using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;
    [SerializeField] public DialogueObject dialogueObject;

    //Pointers to string/DialogueObject
    public string ResponseText => responseText;
    public DialogueObject DialogueObject => dialogueObject;
}
