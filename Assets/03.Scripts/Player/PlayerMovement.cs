using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private float jumpForce = 50f;
    private float currentSpeed;
    private Rigidbody rb;
    private Animator anim;
    private bool isWalking;
    private bool isRunning;
    private float YPos;

    private PlayerGroundCheck PGC;

    //마우스 우클릭으로 이동
    #region
    private NavMeshAgent agent;
   
    private Vector3 arrivePos;
    private float arriveRadius = 0.2f;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        PGC = GetComponent<PlayerGroundCheck>();
        agent = GetComponent<NavMeshAgent>(); //마우스이동용
        currentSpeed = moveSpeed;
        YPos = transform.position.y;
    }

    private void FixedUpdate()
    {
        anim.SetBool("04_InAir", false);
        #region : 점프
        if (Input.GetKeyDown(KeyCode.Space) && PGC.GroundCheck())
        {
            StartCoroutine(Jump());
        }
        #endregion

        #region : 이동
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        if(Mathf.Abs(xInput) >=0.001f || Mathf.Abs(zInput) >= 0.001f)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isWalking = false;
                isRunning = true;
                anim.SetBool("02_Run",isRunning);
                currentSpeed = moveSpeed * 3;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isWalking = true;
                isRunning = false;
                anim.SetBool("02_Run", isRunning);
                currentSpeed = moveSpeed;
            }
            isWalking = true;
            anim.SetBool("01_Walk", isWalking);
            Move(new Vector3(xInput, 0, zInput).normalized * currentSpeed);
            
        }
        else
        {
            isWalking = false;
            anim.SetBool("01_Walk", isWalking);
        }
        #endregion

        //마우스 우클릭으로 이동
        #region 
        //if (Input.GetMouseButtonDown(1)) 
        //{
        //    RaycastHit hit;
        //    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    if(Physics.Raycast(ray.origin, ray.direction, out hit))
        //    {
        //        isWalking = true;
        //        anim.enabled = isWalking;
        //        anim.SetBool("01_Walk", isWalking);
        //        agent.destination = hit.point;
        //        arrivePos = hit.point;
        //    }
        //}
        //;
        //if(Vector3.Distance(transform.position, arrivePos) <= arriveRadius)
        //{
        //    isWalking = false;
        //    anim.SetBool("01_Walk", isWalking);
        //}
        #endregion 
    }

    private void Move(Vector3 inputVec) 
    {
        rb.velocity = inputVec;
    }

    IEnumerator Jump()
    {
        anim.SetTrigger("03_Jump");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        if (!PGC.GroundCheck())
        {
            anim.SetBool("04_InAir", true);
        }
        
    }
}
