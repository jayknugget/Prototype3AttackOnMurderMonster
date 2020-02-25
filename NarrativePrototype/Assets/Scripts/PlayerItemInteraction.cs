using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerItemInteraction : MonoBehaviour
{
    public GameObject monsterMove;
    public GameObject monsterSit;
    public Transform keyText;
    public GameObject lose;

    public int keys = 0;
    private bool firstDoor = true;
    public List<Animator> openDoors;
    private List<Transform> unlockedDoors = new List<Transform>();

    private bool musicbox = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < openDoors.Count; i++)
        {
            openDoors[i].SetBool("open", true);
            unlockedDoors.Add(openDoors[i].transform.GetChild(1).GetChild(3));
        }
        TextUpdate();
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
                    TextUpdate();
                    // play sound
                    AudioManager.playSound("collect");
                    hit.transform.gameObject.SetActive(false);
                }
                else if (hit.transform.CompareTag("HolyGrail"))
                {
                    musicbox = true;
                    hit.transform.gameObject.SetActive(false);
                }
                else if (hit.transform.CompareTag("Note"))
                {

                }
                else if (hit.transform.CompareTag("LastDoor"))
                {
                    hit.transform.parent.parent.GetComponent<Animator>().SetBool("open", true);
                    if (musicbox)
                    {
                        
                    }
                    else
                    {
                        lose.SetActive(true);
                    }
                }
                else if (hit.transform.CompareTag("Door"))
                {
                    bool opened = false;
                    for (int i = 0; i < unlockedDoors.Count; i++)
                    {
                        if (hit.transform == unlockedDoors[i])
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
                        if (firstDoor)
                        {
                            firstDoor = false;
                            monsterSit.SetActive(false);
                            monsterMove.SetActive(true);
                            hit.transform.parent.parent.GetComponent<Animator>().SetBool("open", true);
                            unlockedDoors.Add(hit.transform);
                        }
                        else if (keys >= 1)
                        {
                            keys--;
                            hit.transform.parent.parent.GetComponent<Animator>().SetBool("open", true);
                            unlockedDoors.Add(hit.transform);
                            AudioManager.playSound("door");
                            TextUpdate();
                        }
                    }
                }
            }
        }
    }

    private void TextUpdate()
    {
        if (unlockedDoors.Count <= 8)
        {
            if (keys == 0)
            {
                keyText.GetChild(0).gameObject.SetActive(false);
                keyText.GetChild(1).gameObject.SetActive(false);
                keyText.GetChild(2).gameObject.SetActive(false);
                keyText.GetChild(3).gameObject.SetActive(false);
                keyText.GetChild(4).gameObject.SetActive(true);
                keyText.GetChild(4).GetComponent<TextMeshProUGUI>().text = "He follows the light\n Look to the walls for answers\nLMB: Collect | RMB: Flashlight";
            }
            else if (keys == 1)
            {
                keyText.GetChild(0).gameObject.SetActive(true);
                keyText.GetChild(1).gameObject.SetActive(true);
                keyText.GetChild(2).gameObject.SetActive(false);
                keyText.GetChild(3).gameObject.SetActive(false);
                keyText.GetChild(0).GetComponent<TextMeshProUGUI>().text = "my dear - \noh, my love";
                keyText.GetChild(1).GetComponent<TextMeshProUGUI>().text = "you forgot to lock the door.";
            }
            else if (keys == 2)
            {
                keyText.GetChild(0).gameObject.SetActive(true);
                keyText.GetChild(1).gameObject.SetActive(false);
                keyText.GetChild(2).gameObject.SetActive(false);
                keyText.GetChild(0).GetComponent<TextMeshProUGUI>().text = "you thought\nyou could find us";
            }
            else if (keys == 3)
            {
                keyText.GetChild(0).gameObject.SetActive(false);
                keyText.GetChild(1).gameObject.SetActive(false);
                keyText.GetChild(2).gameObject.SetActive(true);
                keyText.GetChild(2).GetComponent<TextMeshProUGUI>().text = "     we locked the doors\n     but it was already inside";
            }
            else if (keys == 4)
            {
                keyText.GetChild(0).gameObject.SetActive(true);
                keyText.GetChild(1).gameObject.SetActive(true);
                keyText.GetChild(2).gameObject.SetActive(true);
                keyText.GetChild(3).gameObject.SetActive(true);
                keyText.GetChild(0).GetComponent<TextMeshProUGUI>().text = "one key in\nyour left\neye";
                keyText.GetChild(1).GetComponent<TextMeshProUGUI>().text = "one key in your right eye";
                keyText.GetChild(2).GetComponent<TextMeshProUGUI>().text = "one key in your tongue\nsings a song for me";
                keyText.GetChild(3).GetComponent<TextMeshProUGUI>().text = "no key for me\n     no key for me?";
            }
        }
    }
}