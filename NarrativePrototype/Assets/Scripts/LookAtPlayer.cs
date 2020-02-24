using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform PlayerTransform;

    void Update()
    {
        transform.LookAt(PlayerTransform);
    }
}
