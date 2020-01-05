using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private float interactionDist;
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private MultiDimensonalArray[] deadAnimalToAnimal;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private VoiceManager vm;
    [SerializeField] private int startValue;
    [SerializeField] private int endValue;


    private LayerMask mask;
    private void Start()
    {
        mask = LayerMask.GetMask("Interactable");
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * interactionDist, Color.green); 
        if(Physics.Raycast(transform.position, transform.forward, out hit, interactionDist, mask))
        {
            if(Input.GetKeyDown(interactKey))
            {
                foreach (MultiDimensonalArray x in deadAnimalToAnimal)
                {
                    GameObject deadAnimal = x.Object[0];

                    if (deadAnimal == hit.transform.gameObject)
                    {
                        GameObject.Instantiate(x.Object[1], x.Object[2].transform.position, deadAnimal.transform.rotation, deadAnimal.GetComponentInParent<AudioSource>().transform);
                        GameObject.Destroy(deadAnimal);
                        if(playerController.MaxJumps + 1 < endValue)
                        {
                            vm.PlayVoiceLine(playerController.MaxJumps - startValue);
                            Debug.Log("No Scene Change " + playerController.MaxJumps);
                        }
                        else if(playerController.MaxJumps + 1 == endValue)
                        {
                            vm.PlayVoiceLineAndChangeScene(playerController.MaxJumps - startValue);
                            Debug.Log("Scene Change");
                        }
                        playerController.MaxJumps++;
                        break;
                    }
                }
            }
        }
    }
}
