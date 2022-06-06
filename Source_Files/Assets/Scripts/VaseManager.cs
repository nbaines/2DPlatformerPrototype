using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseManager : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioS;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
    }
    public void Break()
    {
        animator.SetBool("isBroken", true);
        audioS.Play();
        StartCoroutine(disableVase());
    }

    IEnumerator disableVase() {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }
}
