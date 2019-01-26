using UnityEngine;

public class PlayerController : MonoBehaviour {     //GIVE THE PLAYER OBJECT A RIGIDBODY AND A BOX COLLIDER AND SET DRAG TO 1. SET COLLISION DETECTION TO CONTINUOUS
                                                                                            //CREATE A PHYSICS MAT WITH 0.6 IN BOTH FRICTION AND ASSIGN TO COLLIDER
    [SerializeField]                            //Speed for both prone and standing  
    private float fPlayerSpeedProne;            //Rough value of about 40
    [SerializeField]
    private float fPlayerSpeedStanding;         //Rough value of about 25
    [SerializeField]
    private float fJumpHeight;                  //Jump height. Rough value of about 40

    private bool bStance;                       //Stores current stance false = prone, true = standing

    private bool bIsGrounded;                   //Stores whether or not the player is grounded
    private float fDistToGround;                //Y axis half extent 
    private float fXExtents;                    //X axis half extent
    private float fGroundCheckExtent;           //Amount that the raycast extends out of the bottom of the collider

    [SerializeField]
    private Rigidbody rb;                       //Players rigidbody
    [SerializeField]
    private Collider col;                       //Players collider

    // Use this for initialization
    void Start () {
        bStance = true;                         //Stance starts as standing
        fDistToGround = col.bounds.extents.y;       //Gets the Y half extent of the collider
        fXExtents = col.bounds.extents.x;           //Gets the X half extent of the collider
        fGroundCheckExtent = 0.3f;
	}

    void FixedUpdate()
    {
       if(!bStance)         //Prone. Moves faster and is unable to jump
       {
            if (Input.GetAxis("Horizontal") != 0) { rb.velocity = new Vector3(rb.velocity.x + fPlayerSpeedProne * Time.deltaTime * Input.GetAxis("Horizontal"), rb.velocity.y, 0); }
            else if (Input.GetAxis("Horizontal") != 0 && !GroundCheck()) { rb.velocity = new Vector3(rb.velocity.x + (fPlayerSpeedProne) * 0.8f * Time.deltaTime * Input.GetAxis("Horizontal"), rb.velocity.y, 0); }
            //Moves the player on the x axis when prone (faster). Slows the players strafe speed down when in the air.
        }
       if(bStance)          //Standing. Moves slower but can jump.
       {
            if (Input.GetAxis("Horizontal") != 0 && GroundCheck()) { rb.velocity = new Vector3(rb.velocity.x + fPlayerSpeedStanding * Time.deltaTime * Input.GetAxis("Horizontal"), rb.velocity.y, 0); }
            else if (Input.GetAxis("Horizontal") != 0 && !GroundCheck()) { rb.velocity = new Vector3(rb.velocity.x + (fPlayerSpeedStanding) * 0.8f * Time.deltaTime * Input.GetAxis("Horizontal"), rb.velocity.y, 0); }
            //Moves the player on the x axis when standing (slower). Slows the players strafe speed down when in the air.

            if (Input.GetButtonDown("Jump") && GroundCheck()) { rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y + fJumpHeight * 10* Time.deltaTime,0); } 
            //If space key is pressed and GroundChecks is true (player on ground), the player jumps.
       }

       if (!GroundCheck()) {                //If not touching the ground, remove friction
            col.material.dynamicFriction = 0;
            col.material.staticFriction = 0;
       }
       else if (GroundCheck()) {                //If touching the ground, apply friction
            col.material.dynamicFriction = 0.6f;    //These two statements prevent the player from sticking to the side of objects
            col.material.staticFriction = 0.6f;
       }

       if (Input.GetButtonDown("Stance")) { bStance = Invert(bStance); }   //Changes the stance when left ctrl is pressed
    }

    bool Invert(bool val) { return !val; }          //Function to invert a bool. Used for stance change

    bool GroundCheck()
    {        //Function to check if the player is grounded using raycast
        bool checkMid = Physics.Raycast(rb.transform.position, Vector3.down, fDistToGround + fGroundCheckExtent);
        bool checkRight = Physics.Raycast(rb.transform.position + new Vector3(0,0,fXExtents), Vector3.down, fDistToGround + fGroundCheckExtent);
        bool checkLeft = Physics.Raycast(rb.transform.position - new Vector3(0, 0, fXExtents), Vector3.down, fDistToGround + fGroundCheckExtent);

        if (checkLeft || checkMid || checkRight) { return true; }
        else return false;
    }       
}
