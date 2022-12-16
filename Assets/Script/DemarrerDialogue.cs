using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemarrerDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DialogueTrigger dialogueTrigger = this.GetComponent<DialogueTrigger>();
        dialogueTrigger.TriggerDialogue();
        var Joueurs = GameObject.FindGameObjectsWithTag("Player");
        if (GameObject.Find("Canvas").gameObject.activeSelf)
        {
            foreach (var player in Joueurs)
            {
                player.GetComponent<PlayerController>().moving = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            /* DialogueTrigger dialogueTrigger = this.GetComponent<DialogueTrigger>();
             dialogueTrigger.TriggerDialogue();*/
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
