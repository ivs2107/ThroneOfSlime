
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float horizontalInput;
    private float verticalInput;

    public int nombrePlayers;
    public GameObject MurSwitch;
    private bool Switch = false;

    public Animator animator;
    public Animator animatorDialogue;
    public GameObject Menu;

    public bool first = true;
    
    public int GetNombrePlayers()
    {
        return nombrePlayers;
    }
    public void SetNombrePlayers(int NombrePlayers)
    {
       nombrePlayers = NombrePlayers;
    }

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Musique").GetComponent<AudioSource>().isPlaying)
        {
            GameObject.FindGameObjectWithTag("Musique").GetComponent<DontDestroyAudio>().ChangeMusic("Level");
        }
        //this.GetComponent<AudioSource>().Play();
    }

    public void LoadLevelSelector()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //bool state = false;

    public void AffichePause()
    {

        /*   //state= !state;
           Menu.gameObject.SetActive(true);*/
        // Menu.gameObject.active = !Menu.gameObject.active;

       /* if (Menu.gameObject.activeSelf)
        {
            
        }
        else
        {
           
        }*/

        var Joueurs = GameObject.FindGameObjectsWithTag("Player");
        if (Menu.gameObject.activeSelf)
        {
            Menu.gameObject.SetActive(false);
            foreach (var player in Joueurs)
            {
                player.GetComponent<PlayerController>().moving = false;
            }
            
        }
        //var Joueurs = GameObject.FindGameObjectsWithTag("Player");
        else
        {
            Menu.gameObject.SetActive(true);
            foreach (var player in Joueurs)
            {
                player.GetComponent<PlayerController>().moving = true;
            }
          
        }
    }
    public void UpdateWall()
    {
        if (MurSwitch != null)
        {
            
          /*  if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
            {*/
                if (Switch)
                {
                    MurSwitch.SetActive(true);
                }
                else
                {
                    MurSwitch.SetActive(false);
                }
                Switch = !Switch;
          //  }
        }
    }

    public float ChangeScene()
    {

        // animator.SetBool("isSwitchingScene", true);
        animator.Play("Slime_Open", 0);
        var time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        return 0.65f;
    }

    public IEnumerator WaitChange()
    {
        
        yield return new WaitForSeconds(10);

    }
}
