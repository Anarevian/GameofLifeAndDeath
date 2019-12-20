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

    [SerializeField] private float groundCheckDist;

    [SerializeField] private AnimationCurve jumpFallof;
    [SerializeField] private float timeBetweenJump;

    [SerializeField] private Animator anim;
    [SerializeField] private int maxJumps;

    public int jumpCount = 0;
    private bool isjumping;

    private Rigidbody rb;

    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        groundCheckDist += (GetComponent<Collider>().bounds.extents.y / 2);
    }

    public void Update()
    {
        Move();
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

        if (!IsGrounded())
        {
            movVec += Vector3.down * gravityMult * Time.deltaTime;
        }

        gameObject.transform.Translate(movVec);
        if (Input.GetKeyDown(jumpAxisName) && jumpCount < maxJumps && isjumping == false)
        {
            anim.SetBool("isFlying", true);
            anim.SetBool("IsIdleing", false);
            Jump();
            jumpCount++;
        }
        else if (jumpCount >= maxJumps && IsGrounded())
        {
            jumpCount = 0;
        }


    }

    public void Jump()
    {
        StartCoroutine(jumpEvaluation());
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


    public bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -transform.up * groundCheckDist, Color.red);

        if (Physics.Raycast(transform.position, -transform.up, groundCheckDist))
        {
            anim.SetBool("IsGrounded", true);
            rb.drag = 0.05f;
            if (!isjumping)
            {
                anim.SetBool("isFlying", false);
            }
            return true;
        }
        anim.SetBool("IsGrounded", false);
        return false;
    }
}
