using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoiceManager : MonoBehaviour
{
    [SerializeField] public AudioClip[] clip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float secondsUntilSceneChange;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    private bool sceneChangeInitated;

    private void Start()
    {
        sceneChangeInitated = false;
    }

    public void PlayVoiceLine(int number)
    {
        audioSource.clip = clip[number];
        audioSource.Play();
    }

    public void PlayVoiceLineAndChangeScene(int number)
    {
        sceneChangeInitated = true;
        audioSource.clip = clip[number];
        audioSource.Play();
    }

    private void Update()
    {
        if(sceneChangeInitated && !audioSource.isPlaying)
        {
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(secondsUntilSceneChange);
        Initiate.Fade(sceneToLoad, fadeColor, fadeSpeed);
        yield return null;
    }


}
