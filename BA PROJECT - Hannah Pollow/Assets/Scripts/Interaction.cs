using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private float interactionDist;
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private MultiDimensonalArray[] deadAnimalToAnimal;

    private LayerMask mask;
    private void Start()
    {
        mask = LayerMask.GetMask("Interactable");
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * interactionDist);
        if(Physics.Raycast(transform.position, transform.forward, out hit, interactionDist, mask))
        {
            if(Input.GetKeyDown(interactKey))
            {
                foreach (MultiDimensonalArray x in deadAnimalToAnimal)
                {
                    GameObject deadAnimal = x.Object[0];

                    if (deadAnimal == hit.transform.gameObject)
                    {
                        GameObject.Instantiate(x.Object[1], deadAnimal.transform.position, deadAnimal.transform.rotation, deadAnimal.GetComponentInParent<AudioSource>().transform);
                        GameObject.Destroy(deadAnimal);
                    }
                }
            }
        }
    }
}
