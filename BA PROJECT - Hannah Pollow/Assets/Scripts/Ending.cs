using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Ending : MonoBehaviour
{
    [SerializeField] private VideoPlayer vp;
    private bool levelended;

    private void Start()
    {
        levelended = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            vp.Play();
            levelended = true;
        }
    }

    private void Update()
    {
        if(!vp.isPlaying && levelended)
        {
            Initiate.Fade("Credits", Color.black, 0.5f);
        }
    }
}
