using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.GetKeyDown(KeyCode.Escape)))
        {
            Application.Quit();
        }
    }
}
