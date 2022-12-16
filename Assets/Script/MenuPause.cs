using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    private static GameObject Menu;

    void Start()
    {
        Menu = GameObject.Find("Menu pause");
    }

}