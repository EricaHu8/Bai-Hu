using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ConversationObject")]
public class ConversationObject : ScriptableObject
{
    [SerializeField] public DialogueObject[] conversation;

    public DialogueObject[] Conversation => conversation;
}
