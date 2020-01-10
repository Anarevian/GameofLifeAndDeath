using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    [SerializeField] private VideoPlayer video;
    [SerializeField] private GameObject[] objectsToDeactivate;

    [SerializeField] private string sceneName;
    [SerializeField] private Color fadecolor;
    [SerializeField] private float fadespeed;

    private bool isPlaying;


    private void Start()
    {
        isPlaying = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if(video.isPlaying && isPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Level1");
        }
        else if(!video.isPlaying && isPlaying)
        {
            Initiate.Fade(sceneName, fadecolor, fadespeed);
        }
    }

    public void Play()
    {
        foreach(GameObject x in objectsToDeactivate)
        {
            x.SetActive(false);
        }
        video.Play();
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.7f);
        isPlaying = true;
    }
}
