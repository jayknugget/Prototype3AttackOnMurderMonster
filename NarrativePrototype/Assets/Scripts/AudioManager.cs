using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip door;
    public static AudioClip collect;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        door = Resources.Load<AudioClip>("door");
        collect = Resources.Load<AudioClip>("collect");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void playSound(string sound)
    {
        door = Resources.Load<AudioClip>("door");
        collect = Resources.Load<AudioClip>("collect");

        if (sound == "door")
        {
            audioSrc.PlayOneShot(door);
        }
        if (sound == "collect")
        {
            audioSrc.PlayOneShot(collect);
        }
    }
}
