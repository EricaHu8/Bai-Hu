using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueObject")]
public class DialogueObject : ScriptableObject
{
    /*
    NOTE FOR CREATING DIALOGUE OBJECTS IN UNITY
    If you want to add a response, you need to add a blank item to the list (or else the responses will just pop up over the last item in the list)
     */
    
    [SerializeField][TextArea] private string[] dialogue;
    [SerializeField] private string charName;
    [SerializeField] private string charTitle;
    [SerializeField] private Sprite charSprite;

    [SerializeField] public Response[] responses;

    //Getters
    public string[] Dialogue => dialogue; 
    public string CharName => charName;
    public string CharTitle => charTitle;
    public Sprite CharSprite => charSprite;
    public Response[] Responses => responses;
    public bool HasResponses => Responses != null && Responses.Length > 0;
}
