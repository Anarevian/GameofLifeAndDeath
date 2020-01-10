using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ActivateShellfish : MonoBehaviour
{
    private List<GameObject> shellfishes;

    private void OnEnable()
    {
        shellfishes = new List<GameObject>();
        foreach (MeshRenderer m in GetComponentInParent<AudioSource>().GetComponentInParent<VideoPlayer>().GetComponentsInChildren<MeshRenderer>(true))
        {
            if(m.gameObject.tag == "Krebstier")
            {
                shellfishes.Add(m.gameObject);
            }
        }

        foreach(GameObject x in shellfishes)
        {
            x.SetActive(true);
        }
    }

}
