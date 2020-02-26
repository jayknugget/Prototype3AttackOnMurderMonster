using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerItemInteraction : MonoBehaviour
{
    public GameObject monsterMove;
    public GameObject monsterSit;
    public GameObject monsterLose;

    private int keys = 0;
    public Transform keyText;
    private bool firstDoor = true;
    private bool secondDoor = true;
    private bool lastDoor = false;
    private Transform endDoor;
    public List<Animator> openDoors;
    private List<Transform> unlockedDoors = new List<Transform>();

    public GameObject noteUI;
    private bool pause = false;

    public GameObject endUI;

    public GameObject musicKey;

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
        if (pause)
        {
            Time.timeScale = 0;
            if (Input.GetMouseButtonDown(0) && noteUI.activeInHierarchy)
            {
                Resume();
            }
        }
        else
        {
            Time.timeScale = 1;

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 4f))
                {
                    if (hit.transform.CompareTag("Key"))
                    {
                        keys++;
                        TextUpdate();
                        if (hit.transform.GetComponent<DataStorage>().keyDoor.tag == "LastDoor")
                        {
                            lastDoor = true;
                            endDoor = hit.transform;
                        }
                        else
                        {
                            unlockedDoors.Add(hit.transform.GetComponent<DataStorage>().keyDoor);
                        }
                        hit.transform.GetComponent<DataStorage>().keyNumber.SetActive(true);
                        // play sound
                        //AudioManager.playSound("collect");
                        hit.transform.gameObject.SetActive(false);
                    }
                    else if (hit.transform.CompareTag("MusicBox"))
                    {
                        musicbox = true;
                        hit.transform.gameObject.SetActive(false);
                    }
                    else if (hit.transform.CompareTag("Note"))
                    {
                        noteUI.SetActive(true);
                        hit.transform.GetComponent<DataStorage>().noteText.SetActive(true);
                        pause = true;
                    }
                    else if (hit.transform.CompareTag("LastDoor"))
                    {
                        if (lastDoor)
                        {
                            endUI.SetActive(true);
                            pause = true;
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

                                if (secondDoor) // first unlocked door that isn't the starting room
                                {
                                    musicKey.SetActive(true); // activate the music key where the monster was sitting
                                    monsterMove.SetActive(true); // activate the actual monster enemy
                                    unlockedDoors[2].parent.parent.GetComponent<Animator>().SetBool("open", false); // close the starting room door
                                    secondDoor = false;
                                }

                                opened = true;
                                AudioManager.playSound("door");
                                break;
                            }
                        }
                        if (!opened)
                        {
                            if (firstDoor)
                            {
                                firstDoor = false;
                                monsterSit.SetActive(false);
                                hit.transform.parent.parent.GetComponent<Animator>().SetBool("open", true);
                                unlockedDoors.Add(hit.transform);
                                AudioManager.playSound("door");
                            }
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

    public void EndGame()
    {
        monsterMove.SetActive(false);
        endDoor.parent.parent.GetComponent<Animator>().SetBool("open", true);
        if (musicbox)
        {

        }
        else
        {
            monsterLose.SetActive(true);
        }
    }

    public void Resume()
    {
        for (int i = 1; i < noteUI.transform.childCount; i++)
        {
            noteUI.transform.GetChild(i).gameObject.SetActive(false);
        }
        noteUI.SetActive(false);
        endUI.SetActive(false);
        pause = false;
    }
}