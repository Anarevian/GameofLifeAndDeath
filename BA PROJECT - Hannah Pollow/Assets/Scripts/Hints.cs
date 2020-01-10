using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour
{
    [SerializeField] private GameObject hintToDeactivate;
    [SerializeField] private GameObject hintToActivate;

    [SerializeField] private GameObject bunny;

    private void Start()
    {
        bunny = GameObject.FindGameObjectWithTag("Bunny");
    }

    private void Update()
    {
        if(bunny == null)
        {
            hintToActivate.SetActive(true);
            hintToDeactivate.SetActive(false);
        }
    }
}
