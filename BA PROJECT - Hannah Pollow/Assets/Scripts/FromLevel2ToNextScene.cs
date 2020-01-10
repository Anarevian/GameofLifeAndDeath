using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FromLevel2ToNextScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float timeUntilSceneLoad;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(timeUntilSceneLoad);
        Initiate.Fade(sceneName, fadeColor, fadeSpeed);
    }
}
