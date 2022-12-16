using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class DontDestroyAudio : MonoBehaviour
{
    public AudioClip LevelSong;
    public AudioClip MenuSong;
    private AudioClip ActualMusic;
    public bool ActiverSon;

    public Sprite SonActive;
    public Sprite SonDesactive;
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.transform.gameObject);
        ActualMusic = MenuSong;
         var Musiques = GameObject.FindGameObjectsWithTag("Musique");
         if (Musiques.Length != 1)
         {
             Destroy(this.gameObject);
         }
        ActiverSon = true;
    }
    public void ChangeMusic(string nameSong)
    {

        if (nameSong == "Level")
        {
            if(ActualMusic != LevelSong)
            {
                ActualMusic = LevelSong;
                this.GetComponent<AudioSource>().clip = ActualMusic;
                this.GetComponent<AudioSource>().Play();
            }
        }
        else if (nameSong == "Menu")
        {
            if (ActualMusic != MenuSong)
            {
                ActualMusic = MenuSong;
                this.GetComponent<AudioSource>().clip = ActualMusic;
                this.GetComponent<AudioSource>().Play();
            }
        }

    }

    public void ActiveSong()
    {
        ActiverSon = !ActiverSon;
        if (!ActiverSon)
        {
            this.GetComponent<AudioSource>().Stop();
          //  this.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Son").GetComponent<Image>().sprite = SonDesactive;
        }
        else
        {
            this.GetComponent<AudioSource>().Play();
           // this.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Son").GetComponent<Image>().sprite = SonActive;
        }
    }

}
