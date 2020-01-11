using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioSource[] source;

    [SerializeField]private GameObject player;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menü");
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if(pauseMenu.activeSelf)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                foreach(AudioSource x in source)
                {
                    x.Pause();
                }
                Time.timeScale = 0.0f;

                player.GetComponent<PlayerController>().enabled = false;
                player.GetComponentInChildren<Interaction>().enabled = false;
                player.GetComponentInChildren<CameraController>().enabled = false;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                foreach (AudioSource x in source)
                {
                    x.UnPause();
                }
                Time.timeScale = 1.0f;

                player.GetComponent<PlayerController>().enabled = true;
                player.GetComponentInChildren<Interaction>().enabled = true;
                player.GetComponentInChildren<CameraController>().enabled = true;
            }
        }
    }

}
