using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoMenu : MonoBehaviour
{
    [SerializeField] private Animation anim;

    private void Update()
    {
        if(!anim.isPlaying)
        {
            SceneManager.LoadScene(0);
        }
    }
}
