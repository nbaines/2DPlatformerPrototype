using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public MusicHandler music;  //assigned in editor

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            LoadNextLevel();
    }
    //TODO: figure out how to tell which scene we need to move to dynamically, implement a loading screen too probably.
    //currently the dynamic loading is going to be left undone, we're just going to go main menu -> swamp -> town -> church
    //leaving the framework for loading dynamically and retreading levels if anyone ever comes back to this.
    public void LoadNextLevel(string toLoad = "Town Area")
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MainMenu")
            toLoad = "Swamp Level";
        else if (sceneName == "Swamp Level")
            toLoad = "Town Area";
        else if (sceneName == "Town Area")
            toLoad = "Church Level";
        StartCoroutine(ChangeScene(toLoad));
    }

    IEnumerator ChangeScene(string toLoad)  //pass this function the name of which scene to load and it will transition us to it.
    {
        float loadWait = 1.5f;
        StartCoroutine(music.Fade(loadWait, 0.0f));
        yield return new WaitForSeconds(loadWait); //delay load start to fade music
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(toLoad);
        while (!asyncLoad.isDone)
            yield return null;

    }
}
