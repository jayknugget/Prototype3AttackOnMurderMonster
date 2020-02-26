using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Camera cam;
    public Light flash;
    public static bool flashlightOn;

    public GameObject darkTextObject;

    // Start is called before the first frame update
    void Start()
    {
        flashlightOn = false;
        flash.intensity = 0;
        darkTextObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = cam.transform.rotation;

        if (Input.GetMouseButtonDown(1))
        {
            FlashlightActivate();
            GetComponent<AudioSource>().Play();
        }
    }

    void FlashlightActivate()
    {
        if (flashlightOn)
        {
            flashlightOn = false;
            darkTextObject.SetActive(true);
        }
        else
        {
            darkTextObject.SetActive(false);
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

    public static bool IsFlashlightOn()
    {
        return flashlightOn;
    }
}
