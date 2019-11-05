using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private byte movSpeedMult;
    [SerializeField] private byte flySpeedMult;
    [SerializeField] private byte jumpForceMult;

    [SerializeField] private string horizAxisName;
    [SerializeField] private string vertAxisName;
    [SerializeField] private string jumpAxisName;

    [SerializeField] private float groundCheckDist;

    [SerializeField] private AnimationCurve jumpFallof;

    private int fwdangle;
    private int bckangle;
    private int rigangle;
    private int lefangle;

    public void Start()
    {
        groundCheckDist += GetComponent<Collider>().bounds.extents.y;
    }

    public void Update()
    {
        Move();      
    }


    public void Move()
    {
        if((Input.GetAxis(horizAxisName) != 0 || Input.GetAxis(vertAxisName) != 0) && !IsOnSlope())
        {
            Vector3 movVec = Vector3.ClampMagnitude(new Vector3(Input.GetAxis(horizAxisName), 0, Input.GetAxis(vertAxisName)), 1);
            movVec *= movSpeedMult * Time.deltaTime;
            gameObject.transform.Translate(movVec);
            if(Input.GetAxis(jumpAxisName) != 0 && IsGrounded())
            {
                Jump();
            }
        }
        if(IsOnSlope())
        {

        }
    }

    public void Jump()
    {
        StartCoroutine(jumpEvaluation());
    }

    public Vector3 SlopeSliding()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, -Vector3.up, out hit, groundCheckDist + (groundCheckDist / 2)))
        {
            return Vector3.zero - hit.normal;
        }
        return Vector3.zero;
       
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

        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z + (GetComponent<Collider>().bounds.extents.z / 4)), -transform.up * groundCheckDist);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z - (GetComponent<Collider>().bounds.extents.z / 4)), -transform.up * groundCheckDist);
        Debug.DrawRay(new Vector3(transform.position.x + (GetComponent<Collider>().bounds.extents.x / 4), transform.position.y, transform.position.z), -transform.up * groundCheckDist);
        Debug.DrawRay(new Vector3(transform.position.x - (GetComponent<Collider>().bounds.extents.x / 4), transform.position.y, transform.position.z), -transform.up * groundCheckDist);

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + (GetComponent<Collider>().bounds.extents.z / 4)), -Vector3.up, groundCheckDist) ||
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - (GetComponent<Collider>().bounds.extents.z / 4)), -Vector3.up, groundCheckDist) ||
            Physics.Raycast(new Vector3(transform.position.x + (GetComponent<Collider>().bounds.extents.x / 4), transform.position.y, transform.position.z), -Vector3.up, groundCheckDist) ||
            Physics.Raycast(new Vector3(transform.position.x - (GetComponent<Collider>().bounds.extents.x / 4), transform.position.y, transform.position.z), -Vector3.up, groundCheckDist))
        {
            return true;
        }
        return false;
    }

    public bool IsOnSlope()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit, groundCheckDist + (groundCheckDist / 2)))
        {
            if (hit.normal != Vector3.up)
            {
                return true;
            }
        }
        return false;
    }

}
