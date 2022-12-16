using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            GameObject.FindGameObjectWithTag("Musique").GetComponent<DontDestroyAudio>().ChangeMusic("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
