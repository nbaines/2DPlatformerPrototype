using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public AudioSource audioS;       //assigned in editor
    public AudioClip[] musicFiles;  //audio files assigned in editor.
                                    //element 0 is swamp music, 1 is town, 2 will be church.
    public string sceneName;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Called by OnEnable and OnDisable, which are unity built ins that occur after awake and on scene being unloaded, respectively.
    //this function will be called after awake when the scene is fully enabled, and will know what scene it is in thanks to the sceneLoaded built in.
    //This function calls the function to play the music, telling it what scene we're in, as well as initiating the fadein for the new music.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        PlayBackgroundMusic(scene.name);
        StartCoroutine(Fade(1.5f, 0.75f));
    }
    public void PlayBackgroundMusic(string sceneName = "Town Area")
    {
        if (sceneName == "Swamp Level")
            audioS.clip = musicFiles[0];
        else if (sceneName == "Town Area" || sceneName == "MainMenu")   //edit this if else chain when the church is implemented.
            audioS.clip = musicFiles[1];
        audioS.Play();
    }

    //fadein and fadeout make the audio less jarring
    public IEnumerator Fade(float duration = 3.0f, float targetVol = 0.0f)
    {
        float currentTime = 0.0f;
        float startVol = audioS.volume;
        while(currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioS.volume = Mathf.Lerp(startVol, targetVol, currentTime / duration);
            yield return null;
        }
        yield break;
    }


    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
