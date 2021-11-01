using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    //TODO: figure out how to tell which scene we need to move to dynamically, implement a loading screen too probably.
    public void LoadNextLevel(string toLoad = "Town Area")
    {
        StartCoroutine(ChangeScene(toLoad));
    }

    IEnumerator ChangeScene(string toLoad)  //pass this function the name of which scene to load and it will transition us to it.
    {
        yield return new WaitForSeconds(3); //3 second delay before the load begins
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(toLoad);
        while (!asyncLoad.isDone)
            yield return null;
    }
}
