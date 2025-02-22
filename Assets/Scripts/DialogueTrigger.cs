using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
 
[System.Serializable]


public class DialogueCharacter
{
    public string name;
}

 
[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}
 
[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}
 
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool waiting = false;
    public bool KillOnCompletion;
 
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue, gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Spacebar pressed!");
            waiting = true;
            TriggerDialogue();
        }
    }

    IEnumerator WaitForConfirm()
    {
        yield return null;
    }
}