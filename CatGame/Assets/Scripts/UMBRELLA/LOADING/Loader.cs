using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    //this script controls loading between scenes

    //list of Scenes
    //Scene names must be exact
    public enum Scene
    {
        MisterLimeScene,
        OfficeScene,

        LoadingScene
    }

    private static Action onLoaderCallback;

    //Load requires string (thanks Fae)
    //loads loading screen and current new Scene
    public static void Load(String scene)
    {
        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(scene.ToString());
        };

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }

    }
}