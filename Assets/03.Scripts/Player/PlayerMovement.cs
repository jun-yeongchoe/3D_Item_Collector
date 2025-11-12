using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float jumpForce = 8f;
    private float currentSpeed;
    private Rigidbody rb;
    private Animator anim;
    private bool isWalking;
    private bool isRunning;
    private float YPos;

    private PlayerGroundCheck PGC;

    private float xInput;
    private float zInput;

    private bool isRun;

    private float turnSpeed = 1f;

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
    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (!PGC.GroundCheck())
        {
            anim.SetBool("04_InAir", true);
            //currentSpeed = moveSpeed;
        }
        if (PGC.GroundCheck()) 
        {
            zInput = Input.GetAxisRaw("Vertical");
            #region : 달리기
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isRun = true;
                Run(isRun);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isRun = false;
                Run(isRun);
            }
            #endregion
        }

        #region : 점프
        if (Input.GetKeyDown(KeyCode.Space) && PGC.GroundCheck())
        {
            StartCoroutine(Jump());
        }
        if (PGC.GroundCheck()) anim.SetBool("04_InAir", false);
        #endregion

        #region: 회전
        float rotDir = xInput * turnSpeed;
        transform.Rotate(0, rotDir, 0);
        #endregion
    }

    private void FixedUpdate()
    {
        #region : 이동
        if(Mathf.Abs(zInput) >= 0.001f)
        {
            isWalking = true;
            anim.SetBool("01_Walk", isWalking);
            Move(zInput * currentSpeed);
        }
        else
        {
            isWalking = false;
            anim.SetBool("01_Walk", isWalking);
        }
        #endregion

        #region : 마우스 우클릭으로 이동
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

    private void Move(float inputVecZ) 
    {
        Vector3 forward = transform.forward * inputVecZ;
        rb.velocity = new Vector3(forward.x, rb.velocity.y, forward.z);
    }

    IEnumerator Jump()
    {
        anim.SetTrigger("03_Jump");
        //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        Vector3 v = rb.velocity;
        v.y = jumpForce;
        rb.velocity = v;
        yield return new WaitForSeconds(0.5f);
        if (!PGC.GroundCheck())
        {
            anim.SetBool("04_InAir", true);
        }
    }
    private void Run(bool isrunning)
    {
        if (isrunning)
        {
            isWalking = false;
            isRunning = true;
            anim.SetBool("02_Run", isRunning);
            currentSpeed = moveSpeed * 3;
        }
        else
        {
            isWalking = true;
            isRunning = false;
            anim.SetBool("02_Run", isRunning);
            currentSpeed = moveSpeed;
        }
    }
}
