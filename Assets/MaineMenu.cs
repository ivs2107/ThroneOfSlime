using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MaineMenu : MonoBehaviour
{
    public string levelName;
    private Button button;
    public Ease moveEase;

    int firstRun = 0; // variable inside the class, but not inside a function.

       


        private void Start()
        {
        var buttons = GameObject.FindGameObjectsWithTag("Button_Level");
        foreach (var button in buttons)
        {
            if (int.Parse(button.name.Split('_')[1]) > int.Parse(PlayerPrefs.GetString("Level").Split()[1]))
            {
                button.GetComponent<Image>().color = Color.grey;
            }
        }

        firstRun = PlayerPrefs.GetInt("savedFirstRun");


        if (firstRun == 0) // remember "==" for comparing, not "=" which assigns value
        {
            firstRun = 1;
        }
        else
        {
            PlayerPrefs.SetString("Level", "Level 1");
        }
    }

    public void SelectLevel()
    {
        //SceneManager.GetSceneByName(levelName);
        SceneManager.LoadScene(levelName);
    }

    public void ChangeBackgroundAvant()
    {
        GameObject Camera = GameObject.FindGameObjectWithTag("MainCamera");
       Camera.transform.DOMoveX(Camera.transform.position.x + 18, 1).SetEase(moveEase);
    }

    public void ChangeBackgroundArriere()
    {
        GameObject Camera = GameObject.FindGameObjectWithTag("MainCamera");
        Camera.transform.DOMoveX(Camera.transform.position.x - 18, 1).SetEase(moveEase);
    }

    public void DisabledLevel()
    {
        button = GetComponent<Button>();
        if (int.Parse(button.name.Split('_')[1]) > int.Parse(PlayerPrefs.GetString("Level").Split()[1]))
        {
            button.enabled = false;
            Debug.Log(button.name.Split('_')[1]);
        }
        else
        {
            SceneManager.LoadScene(levelName);
        }
        Debug.Log("level Saved : " + PlayerPrefs.GetString("Level").Split()[1]);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelSelector");
        //PlayerPrefs.SetString("Level", "Level 1");
        if (PlayerPrefs.GetString("Level") == "" || PlayerPrefs.GetString("Level") is null)
        {
            PlayerPrefs.SetString("Level", "Level 1");
        }
        Debug.Log("test level : " + PlayerPrefs.GetString("Level"));
       // SceneManager.LoadScene("LevelSelector");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    /*public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }*/

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
