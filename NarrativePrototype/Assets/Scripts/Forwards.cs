using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Forwards : MonoBehaviour
{
    private Transform playerPos;
    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<NavMeshAgent>().speed = 3f;
        //GetComponentInChildren<Animator>().SetTrigger("Attack");
    }

    private void Update()
    {
        GetComponent<NavMeshAgent>().destination = playerPos.position;
    }

    private void OnTriggerStay(Collider c)
    {
        if (c.transform.tag == "Player")
        {
            SceneManager.LoadScene(1);
        }
    }
}
