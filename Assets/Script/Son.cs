using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Son : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite SonActive;
    public Sprite SonDesactive;

    private void Start()
    {
        bool Active = GameObject.FindGameObjectWithTag("Musique").GetComponent<DontDestroyAudio>().ActiverSon;
        if (!Active)
        {
            GameObject.FindGameObjectWithTag("Son").GetComponent<Image>().sprite = SonDesactive;

        }
        else
        {
            GameObject.FindGameObjectWithTag("Son").GetComponent<Image>().sprite = SonActive;
        }
    }
    public void ActiveSong()
    {

        bool Active = GameObject.FindGameObjectWithTag("Musique").GetComponent<DontDestroyAudio>().ActiverSon;
        Active = !Active;
        GameObject.FindGameObjectWithTag("Musique").GetComponent<DontDestroyAudio>().ActiverSon = Active;
        if (!Active)
        {
            GameObject.FindGameObjectWithTag("Musique").GetComponent<AudioSource>().Stop();
            //  this.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Son").GetComponent<Image>().sprite = SonDesactive;
            
        }
        else
        {
            GameObject.FindGameObjectWithTag("Musique").GetComponent<AudioSource>().Play();
            // this.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Son").GetComponent<Image>().sprite = SonActive;
        }
    }
}
