using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float fPlayerSpeedProne;            //Speed and jump height for both prone and standing
    [SerializeField]
    private float fPlayerSpeedStanding;
    [SerializeField]
    private float fJumpHeight;

    private bool bStance;           //Stores current stance false = prone, true = standing

    private bool bIsGrounded;
    private float fDistToGround;

    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Collider col;

    private Vector3 playerVel;

    // Use this for initialization
    void Start () {
        
        bStance = true;
        playerVel = rb.velocity;
        fDistToGround = col.bounds.extents.y;
	}

    void FixedUpdate()
    {
       if(!bStance)         //Prone
       {
            if (Input.GetAxis("Horizontal") != 0) { rb.velocity = new Vector3(rb.velocity.x + fPlayerSpeedProne * Time.deltaTime * Input.GetAxis("Horizontal"), rb.velocity.y, 0); }
            //Moves the player on the x axis when prone (faster) 

            //Jumping is disabled in prone
       }
       if(bStance)          //Standing
       {
            if (Input.GetAxis("Horizontal") != 0) { rb.velocity = new Vector3(rb.velocity.x + fPlayerSpeedStanding * Time.deltaTime * Input.GetAxis("Horizontal"), rb.velocity.y, 0); }
            //Moves the player on the x axis when standing (slower)

            if(Input.GetButtonDown("Submit") && IsGrounded())
            {
                Debug.Log("Jump");
                rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y + fJumpHeight * Time.deltaTime,0);
            }
       }

       if(Input.GetButtonDown("Jump"))
       {
            bStance = Invert(bStance);
       }
    }

    bool Invert(bool val) { return !val; }

    bool IsGrounded()
    {
        return Physics.Raycast(rb.transform.position, Vector3.down, fDistToGround + 0.1f);
    }
}
