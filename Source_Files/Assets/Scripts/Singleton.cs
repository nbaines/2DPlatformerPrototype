using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    [SerializeField] string tagToSingle;    //set this in the inspector, make it whatever the tag of the object it is attached to is, and it will enforce only one of an object of that tag.
   
    private void Awake()
    {
        GameObject[] pers = GameObject.FindGameObjectsWithTag(tagToSingle);
        if (pers.Length > 1)
            Destroy(this.gameObject);   //this block checks for how many objects of the type "persistane there are, and then destroys this if we try to create a second
        
        DontDestroyOnLoad(this.transform.gameObject);   //this script can be attached to any object we need to keep between scenes.
    }
}
