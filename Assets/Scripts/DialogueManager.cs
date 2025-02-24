using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
 
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
 
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;
 
    private Queue<DialogueLine> lines;
    
    public bool isDialogueActive = false;
 
    public float typingSpeed = 1f;
 
    //public Animator animator;
    private GameObject Speaker;
 
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
 
        lines = new Queue<DialogueLine>();
    }
    public void StartDialogue(Dialogue dialogue, GameObject passed_speaker)
    {
        Speaker = passed_speaker;
        isDialogueActive = true;
 
        //animator.Play("show");
 
        lines.Clear();
 
        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }
 
        DisplayNextDialogueLine();
    }

    /*
    public IEnumerator StartBattlePopup(Sprite faceball , string comboName , string description)
    {
        isDialogueActive = true;
        lines.Clear();

        characterName.text = comboName;
        dialogueArea.text = description;
        yield return new WaitForSeconds(1.5f);
        yield return null;
    }
    */

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
 
        DialogueLine currentLine = lines.Dequeue();
 
        characterName.text = currentLine.character.name;
 
        StopAllCoroutines();
 
        StartCoroutine(TypeSentence(currentLine));
    }
 
    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        StartCoroutine(DMNext());
    }
 
    public void EndDialogue()
    {
        isDialogueActive = false;
        //animator.Play("hide");
        if (Speaker.GetComponent<DialogueTrigger>().KillOnCompletion == true)
        {
            Speaker.SetActive(false);
        }
        GameObject obj = GameObject.Find("Scene Controller");
        SceneController command = obj.GetComponent<SceneController>();
        command.LoadSceneUnderstood();
    }

    IEnumerator DMNext()
    {
        while(isDialogueActive == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                DisplayNextDialogueLine();
                yield break;
            }
            yield return null;
        }
        yield return null;
    }
}