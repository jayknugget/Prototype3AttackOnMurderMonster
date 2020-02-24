using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{
    private int keys = 0;
    private bool firstDoor = true;
    public GameObject monsterSit;

    private List<Transform> openDoors = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 2f))
            {
                if (hit.transform.CompareTag("Key"))
                {
                    keys++;
                    // play sound
                    //AudioManager.playSound("collect");
                    hit.transform.gameObject.SetActive(false);
                }
                else if (hit.transform.CompareTag("Door"))
                {
                    bool opened = false;
                    for (int i = 0; i < openDoors.Count; i++)
                    {
                        if (hit.transform == openDoors[i])
                        {
                            if (hit.transform.parent.parent.GetComponent<Animator>().GetBool("open"))
                            {
                                hit.transform.parent.parent.GetComponent<Animator>().SetBool("open", false);
                            }
                            else
                            {
                                hit.transform.parent.parent.GetComponent<Animator>().SetBool("open", true);
                            }
                            opened = true;
                            break;
                         }
                    }
                    if (!opened)
                    {
                        if (keys >= 1)
                        {
                            if (firstDoor)
                            {
                                firstDoor = false;
                                monsterSit.SetActive(false);
                            }
                            keys--;
                            hit.transform.parent.parent.GetComponent<Animator>().SetBool("open", true);
                            openDoors.Add(hit.transform);
                            //AudioManager.playSound("door");
                        }
                    }
                }
            }
        }
    }
}