using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeedMult;
    [SerializeField] private float runSpeedMult;
    [SerializeField] private float flySpeedMult;
    [SerializeField] private float jumpForceMult;
    [SerializeField] private float gravityMult;

    [SerializeField] private string horizAxisName;
    [SerializeField] private string vertAxisName;
    [SerializeField] private string jumpAxisName;
    [SerializeField] private string sprintAxisName;
    [SerializeField] private string flyToggleAxisName;
    [SerializeField] private string flyDownAxisName;

    [SerializeField] private float groundCheckDist;

    [SerializeField] private AnimationCurve jumpFallof;

    private bool canFly;

    public void Start()
    {
        canFly = false;
        groundCheckDist += GetComponent<Collider>().bounds.extents.y;
    }

    public void Update()
    {
        if(Input.GetAxis(flyToggleAxisName) == 1)
        {
            canFly = !canFly;
        }

        if(canFly)
        {
            GetComponent<Rigidbody>().useGravity = false;
            Fly();
        }
        else
        {
            GetComponent<Rigidbody>().useGravity = true;
            Move();
        }             
    }

    public void Fly()
    {
        Vector3 movVec = new Vector3(Input.GetAxis(horizAxisName), 0, Input.GetAxis(vertAxisName));

        if (Input.GetAxis(jumpAxisName) == 1)
        {
            movVec += Vector3.up;
        }
        else if(Input.GetAxis(flyDownAxisName) == 1)
        {
            movVec -= Vector3.up;
        }    
        
        Vector3.ClampMagnitude(movVec, 1);

        movVec *= flySpeedMult;

        gameObject.transform.Translate(movVec * Time.deltaTime);
    }

    public void Move()
    {

        Vector3 movVec = Vector3.ClampMagnitude(new Vector3(Input.GetAxis(horizAxisName), 0, Input.GetAxis(vertAxisName)), 1);

        switch(Input.GetAxis(sprintAxisName))
        {
            case 1: movVec *= runSpeedMult * Time.deltaTime;
                break;
            default: movVec *= walkSpeedMult * Time.deltaTime;
                break;
        }

        if(!IsGrounded())
        {
            movVec += Vector3.down * gravityMult * Time.deltaTime;
        }
        
        gameObject.transform.Translate(movVec);
        if (Input.GetAxis(jumpAxisName) != 0 && IsGrounded())
        {
            Jump();
        }
    }

    public void Jump()
    {
        StartCoroutine(jumpEvaluation());
    }

    private IEnumerator jumpEvaluation()
    {
        float airTime = 0.0f;
        float evaluation = 0.0f;
        do
        {
            evaluation = jumpFallof.Evaluate(airTime);
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, evaluation * jumpForceMult, 0));
            airTime += Time.deltaTime;
            yield return null;
        }
        while (!IsGrounded());
        yield break;
    }


    public bool IsGrounded()
    {
        //  Debug.DrawRay(transform.position, -transform.up * groundCheckDist, Color.red);

        if (Physics.Raycast(transform.position,-transform.up, groundCheckDist))
        {
            return true;
        }
        return false;
    }
}
