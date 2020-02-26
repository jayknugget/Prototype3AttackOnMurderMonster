using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Forwards : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = Vector3.right * 260f * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.transform.tag == "Player")
        {
            SceneManager.LoadScene(0);
        }
    }
}
