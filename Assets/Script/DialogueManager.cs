using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //public Text nameText;
    public Text dialogueText;

    public Animator animator;


    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

       /// nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
      //  dialogueText.text = sentence;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (Char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            // yield a floating point number, which will pause the routine for a desired amount of time
            yield return null;
        }
    }

    private void EndDialogue()
    {
        Debug.Log("End");
        animator.SetBool("IsOpen", false);
        var Joueurs = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in Joueurs)
        {
            player.GetComponent<PlayerController>().moving = false;
        }
    }
}