using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPlayerPrefs : MonoBehaviour
{
    //this is the master list of Cat Game PlayerPrefs

    //tracks Player's name
    //used in save profiles and certain texts
    //PlayerPrefs.SetString("PlayerName", "Cat");
    //PlayerPrefs.GetString("PlayerName");

    //tracks lives
    //losing HP will result in the loss of a life
    //lose 9 lives and the game is over (this game should be easy enough that is easy to do)
    ////PlayerPrefs.SetInt("Lives", 9);
    //PlayerPrefs.GetInt("Lives");
    int lives;

    //tracks the maximum HP
    //performing certain tasks increases max HP?
    int maxHP;

    //tracks
    int time;

}
