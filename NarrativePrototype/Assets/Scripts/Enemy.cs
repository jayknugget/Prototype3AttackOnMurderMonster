using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameObject[] waypoints;
    private NavMeshAgent myAgent;
    private int currentWaypoint;
    private int playerhealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.destination = waypoints[currentWaypoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(myAgent.destination, transform.position) <= 1)
        {
            currentWaypoint++;
            if(currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
            myAgent.destination = waypoints[currentWaypoint].transform.position;

        }

    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Vector3.Distance(transform.position, other.transform.position) <= 2)
            {
                myAgent.destination = transform.position;
                GetComponentInChildren<Animator>().SetTrigger("Attack");
                playerhealth = playerhealth - 5;
                if(playerhealth <= 0)
                {
                    SceneManager.LoadScene(0);

                }
 
            }
            else
            {
                myAgent.destination = other.transform.position;
            }
        }
    }
}
