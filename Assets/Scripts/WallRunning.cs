using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("WallRun")]
    public LayerMask WhatIsWall;
    public LayerMask WallIsGround;
    public float WallRunForce;
    public float MaxWallRunTime;
    public float WallRunTimer;

    [Header("Inputs")]
    private float horizontalInput;
    private float verticalInput;

    [Header("Detection")]
    public float wallCheckDist;
    public float minJumpHeight;
    private RaycastHit LeftwallHit;
    private RaycastHit RightwallHit;
    private bool WallLeft;
    private bool WallRight;

    [Header("Reference")]
    public Transform Orientation;
    private ThirdPersonController pm;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<ThirdPersonController>();

    }

    private void Update()
    {
        CheckWall();
    }

    private void FixedUpdate()
    {
        CheckWall();
        StateMachine();
        if (pm.wallrunning)
        {
            WallRunningMovement();  
        }
    }

    private void CheckWall()
    {
        WallRight = Physics.Raycast(transform.position, Orientation.right, out RightwallHit, wallCheckDist, layerMask: WhatIsWall);
        WallLeft = Physics.Raycast(transform.position, -Orientation.right, out LeftwallHit, wallCheckDist, layerMask: WhatIsWall);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, WallIsGround);
    }

    private void StateMachine()
    {
       // Getting inputs
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        //State 1 - Wallrun
        if((WallLeft || WallRight) && verticalInput > 0 && AboveGround())
        {
            // Start Wallrun
            if (!pm.wallrunning)
            {
                StartWallRun();
            }
            else if (pm.wallrunning)
            {
                StopWallRun();
            }
        }
    }

    private void WallRunningMovement()
    {
        rb.useGravity = true;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.angularVelocity.z);

        Vector3 wallNormal = WallRight ? RightwallHit.normal : LeftwallHit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((Orientation.forward - wallForward).magnitude > (Orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        //Forwards force
        rb.AddForce(wallForward * WallRunForce, ForceMode.Force);

        // Push to wall
       if (!(WallLeft && horizontalInput > 0) && !(WallRight && horizontalInput < 0))
        {
            rb.AddForce(-wallForward * 100, ForceMode.Force);
        }
    }

    private void StopWallRun()
    {
        pm.wallrunning = false;
    }

    private void StartWallRun()
    {
        pm.wallrunning = true;
    }

}
