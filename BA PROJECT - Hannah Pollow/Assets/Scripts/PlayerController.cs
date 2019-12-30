using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityMult;
    [SerializeField] private float dragTime;
    [SerializeField] private float drag;
    [SerializeField] private float jumpCD;

    [SerializeField] private string horizAxisName;
    [SerializeField] private string vertAxisName;
    [SerializeField] private KeyCode jumpAxisName;
    [SerializeField] private string sprintAxisName;
    [SerializeField] private string flyDownAxisName;

    [SerializeField] private AnimationCurve jumpFallof;
    [SerializeField] private float timeBetweenJump;

    [SerializeField] private Animator anim;
    [SerializeField] private GameObject[] feathers;
    public int MaxJumps;
    

    public int jumpCount = 0;
    public bool isjumping;
    public bool isGrounded;

    private Rigidbody rb;

    public void Start()
    {
        isGrounded = true;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void Update()
    {
        Move();
        UpdateOverlay();
    }

    public void Move()
    {

        Vector3 movVec = Vector3.ClampMagnitude(new Vector3(Input.GetAxis(horizAxisName), 0, Input.GetAxis(vertAxisName)), 1);

        switch (Input.GetAxis(sprintAxisName))
        {
            case 1:
                movVec *= runSpeed * Time.deltaTime;
                anim.SetBool("isRunning", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("IsIdleing", false);
                break;
            default:
                movVec *= walkSpeed * Time.deltaTime;
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", true);
                anim.SetBool("IsIdleing", false);
                break;
        }

        if (Input.GetAxis(horizAxisName) == 0 && Input.GetAxis(vertAxisName) == 0)
        {
            anim.SetBool("IsIdleing", true);
        }

        if (!isGrounded)
        {
            movVec += Vector3.down * gravityMult * Time.deltaTime;
        }

        gameObject.transform.Translate(movVec);
        if (Input.GetKeyDown(jumpAxisName) && jumpCount < MaxJumps && !isjumping)
        {
            jumpCount++;
            anim.SetBool("isFlying", true);
            anim.SetBool("IsIdleing", false);
            Jump();     
        }
        else if (isGrounded && !isjumping)
        {
            jumpCount = 0;
        }


    }

    public void Jump()
    {
        StartCoroutine(jumpEvaluation());
    }

    public void UpdateOverlay()
    {
        foreach(GameObject x in feathers)
        {
            x.SetActive(false);
        }
        switch(MaxJumps - jumpCount)
        {
            case 1:
                feathers[0].SetActive(true);
                break;
            case 2:
                feathers[0].SetActive(true);
                feathers[1].SetActive(true);
                break;
            case 3:
                feathers[0].SetActive(true);
                feathers[1].SetActive(true);
                feathers[2].SetActive(true);
                break;
            case 4:
                feathers[0].SetActive(true);
                feathers[1].SetActive(true);
                feathers[2].SetActive(true);
                feathers[3].SetActive(true);
                break;
            case 5:
                feathers[0].SetActive(true);
                feathers[1].SetActive(true);
                feathers[2].SetActive(true);
                feathers[3].SetActive(true);
                feathers[4].SetActive(true);
                break;
        }
    }

    private IEnumerator jumpEvaluation()
    {
        isjumping = true;
        float airTime = 0.0f;
        float evaluation = 0.0f;
        rb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
        do
        {
            evaluation = jumpFallof.Evaluate(airTime);
            rb.AddForce(new Vector3(0, evaluation * jumpForce, 0));
            airTime += Time.deltaTime;
            yield return null;
        }
        while (airTime <= timeBetweenJump);
        isjumping = false;
        yield return new WaitForSeconds(dragTime);
        rb.drag = drag;
        
        yield break;
    }


    private void OnCollisionEnter(Collision collision)
    {
        anim.SetBool("IsGrounded", true);
        rb.drag = 0.05f;
        if (!isjumping)
        {
            anim.SetBool("isFlying", false);
        }
        isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        anim.SetBool("IsGrounded", false);
        isGrounded = false;
    }


}
