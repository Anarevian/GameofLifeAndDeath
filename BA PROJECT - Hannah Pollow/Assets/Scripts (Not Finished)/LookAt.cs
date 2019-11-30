using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform lookingPl;

    void Update()
    {
        transform.LookAt(lookingPl);
    }
}
