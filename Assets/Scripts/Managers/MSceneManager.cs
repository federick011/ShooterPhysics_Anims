using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class MSceneManager : SubManagersBase
{
    public enum ScenesToLoad 
    {
        None,
        SceneLoader,
        SceneGameplay
    }

    public ScenesToLoad currentScene = ScenesToLoad.None;

    Dictionary<ScenesToLoad, string> scenesToLoad = new Dictionary<ScenesToLoad, string>() 
    {
        { ScenesToLoad.None, "" },
        { ScenesToLoad.SceneLoader, "SceneLoader" },
        { ScenesToLoad.SceneGameplay, "SceneGameplay" },
    };

    public override void Initialize(AppManager appManager)
    {
        base.Initialize(appManager);
    }

    public void LoadBasicScene()
    {
        LoadNewScene(ScenesToLoad.SceneGameplay);
    }

    public void LoadNewScene(ScenesToLoad sceneToLoad, bool additive = true) 
    {
        if (!scenesToLoad.ContainsKey(sceneToLoad)) return;

        currentScene = sceneToLoad;

        SceneManager.LoadScene(scenesToLoad[sceneToLoad], ((additive) ? LoadSceneMode.Additive: LoadSceneMode.Single));
    }

    public void UnloadScene()
    {
        SceneManager.UnloadSceneAsync(scenesToLoad[currentScene]);
    }
}
