using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    public Transform[] backgroundSprites; //array of bg sprites
    private float[] bgParallaxMult; //movement multipliers
    public float parallaxSmoothness = 1f; //smoothness of parallax movement

    private Transform camera;
    private Vector3 prevCamPos;

    void Awake()
    {
        camera = Camera.main.transform; //reference camera's transformation/movement
    }

    void Start()
    {
        prevCamPos = camera.position;

        int numBackgrounds = backgroundSprites.Length;
        bgParallaxMult = new float[numBackgrounds]; //different movement multiplier for each bg sprite

        //Set move multipliers based on each bg sprite's z-position
        for (int currBackground = 0; currBackground < backgroundSprites.Length; currBackground++)
        {
            bgParallaxMult[currBackground] = backgroundSprites[currBackground].position.z;
        }
    }

    void Update()
    {
        for (int currBackground = 0; currBackground < backgroundSprites.Length; currBackground++)
        {
            //Handle x-axis
            float parallaxOffsetX = (camera.position.x - prevCamPos.x) * bgParallaxMult[currBackground]; //determine x-axis offset based on camera movement
            float parallaxNewPosX = backgroundSprites[currBackground].position.x + parallaxOffsetX; //calculate new x-position of given bg sprite

            //Handle y-axis
            float parallaxOffsetY = (camera.position.y - prevCamPos.y) * bgParallaxMult[currBackground] / 2; //determine y-axis offset based on camera movement (note: has purposefully slower movement!)
            float parallaxNewPosY = backgroundSprites[currBackground].position.y + parallaxOffsetY; //calculate new y-position of given bg sprite

            //Set new position of bg sprite
            Vector3 parallaxNewPos = new Vector3(parallaxNewPosX, parallaxNewPosY, backgroundSprites[currBackground].position.z);

            //Interpolate movement between old/new bg sprite positions
            backgroundSprites[currBackground].position = Vector3.Lerp(backgroundSprites[currBackground].position, parallaxNewPos, parallaxSmoothness * Time.deltaTime);
        }

        //Update camera position
        prevCamPos = camera.position;
    }
}