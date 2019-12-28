using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingAnimals : MonoBehaviour
{
    private Rigidbody rb;
    private bool isGrounded;
    private bool jumped;
    private Transform player;

    [SerializeField]private float jumpCD;
    [SerializeField]private Vector3 jumpForce;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody>();
        jumped = false;
        isGrounded = false;
        transform.LookAt(player);        
        transform.Rotate(0, 180, 0);
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = 0;
        rotationVector.x = 0;
        transform.rotation = Quaternion.Euler(rotationVector);

    }

    private void FixedUpdate()
    {
        
        if(isGrounded)
        {
            StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        jumped = true;
        rb.AddForce(jumpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(jumpCD);
        jumped = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
