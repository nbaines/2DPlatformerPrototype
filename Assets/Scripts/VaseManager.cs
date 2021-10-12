using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseManager : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Break()
    {
        animator.SetBool("isBroken", true);
        StartCoroutine(disableVase());
    }

    IEnumerator disableVase() {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }
}
