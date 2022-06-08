using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    //Script for the Main Menu

    //Player name input
    private string input;

    //clickable start game button
    //currently just loads a random scene
    //loads
    public void StartGame()
    {

        Loader.Load("LoserHouseScene");
    }

}
