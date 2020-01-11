using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Ending : MonoBehaviour
{
    [SerializeField] private Buttons pause;
    [SerializeField] private AudioSource[] source;
    [SerializeField] private VideoPlayer vp;
    [SerializeField] private GameObject[] feather;
    private bool levelended;

    private void Start()
    {
        levelended = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pause.enabled = false;
            foreach(AudioSource x in source)
            {
                x.Stop();
            }
            foreach (GameObject x in feather)
            {
                Destroy(x);
            }
            vp.Play();
            levelended = true;
        }
    }

    private void Update()
    {
        if(!vp.isPlaying && levelended)
        {
            Initiate.Fade("Credits", Color.black, 1f);
        }
    }
}
