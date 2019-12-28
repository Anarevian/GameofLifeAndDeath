using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    [SerializeField] private GameObject objToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            objToActivate.SetActive(true);
        }
    }
}
