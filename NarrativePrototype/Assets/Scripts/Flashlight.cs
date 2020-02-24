using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Camera cam;
    public Light flash;
    private bool flashlightOn;

    // Start is called before the first frame update
    void Start()
    {
        flashlightOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = cam.transform.rotation;

        if (Input.GetMouseButtonDown(1))
        {
            FlashlightActivate();
        }
    }

    void FlashlightActivate()
    {
        if (flashlightOn)
        {
            flashlightOn = false;
        }
        else
        {
            flashlightOn = true;
        }

        if (flashlightOn)
        {
            flash.intensity = 1;
        }
        else
        {
            flash.intensity = 0;
        }
    }
}
