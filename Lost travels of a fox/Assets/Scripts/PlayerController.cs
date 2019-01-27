using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {     //GIVE THE PLAYER OBJECT A RIGIDBODY AND A BOX COLLIDER AND SET DRAG TO 1. SET COLLISION DETECTION TO CONTINUOUS
                                                                                            //CREATE A PHYSICS MAT WITH 0.6 IN BOTH FRICTION AND ASSIGN TO COLLIDER
    [SerializeField]                            //Speed for both prone and standing  
    private float fPlayerSpeedProne;            //Rough value of about 40
    [SerializeField]
    private float fPlayerSpeedStanding;         //Rough value of about 25
    [SerializeField]
    private float fJumpHeight;                  //Jump height. Rough value of about 40

    private bool bStance;                       //Stores current stance false = prone, true = standing

    [SerializeField]
    float maxXVel;

    private bool bIsGrounded;                   //Stores whether or not the player is grounded
    private float fDistToGround;                //Y axis half extent 
    private float fXExtents;                    //X axis half extent
    private float fGroundCheckExtent;           //Amount that the raycast extends out of the bottom of the collider
    bool lastGroundCheck;

    [SerializeField]
    private Rigidbody rb;                       //Players rigidbody
    [SerializeField]
    private Collider standCol;                       //Players collider
    [SerializeField]
    private Collider crawlCol;                       //Players collider
    [SerializeField]
    Transform trans;

    // Use this for initialization
    void Start () {
        bStance = true;                         //Stance starts as standing
        fDistToGround = standCol.bounds.extents.y;       //Gets the Y half extent of the collider
        fXExtents = standCol.bounds.extents.x;           //Gets the X half extent of the collider
        fGroundCheckExtent = 0.1f;

        m_anFox = GetComponent<Animator>();

        StartCoroutine(manageAnimator());

        m_fsFoxState = FoxStates.Standing;

        
    }

    void FixedUpdate()
    {
       if(!bStance)         //Prone. Moves faster and is unable to jump
       {
            //col.transform.rotation = Quaternion.Euler(new Vector3(90, 90, 0));
            standCol.enabled = false;
            crawlCol.enabled = true;

            if (Input.GetAxis("Horizontal") != 0) {
                rb.velocity = new Vector3(rb.velocity.x + fPlayerSpeedProne * Time.deltaTime * Input.GetAxis("Horizontal"), rb.velocity.y, 0);
                m_fsFoxState = FoxStates.Running;
            }
            if (Input.GetAxis("Horizontal") != 0 && !GroundCheck())
            {
                rb.velocity = new Vector3(rb.velocity.x + (fPlayerSpeedProne) * 0.8f * Time.deltaTime * Input.GetAxis("Horizontal"), rb.velocity.y, 0);
                //m_fsFoxState = FoxStates.Jumping;
            }
            else if (Input.GetAxis("Horizontal") == 0)
            {
                m_fsFoxState = FoxStates.Crawling;
            }

            if (rb.velocity.x >= maxXVel)
            {
                rb.velocity = new Vector3(maxXVel, rb.velocity.y, 0);
            }
            if (rb.velocity.x <= -maxXVel)
            {
                rb.velocity = new Vector3(-maxXVel, rb.velocity.y, 0);
            }

            //Moves the player on the x axis when prone (faster). Slows the players strafe speed down when in the air.
        }
       if(bStance)          //Standing. Moves slower but can jump.
       {
            //col.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));

            standCol.enabled = true;    //Enables standing collider
            crawlCol.enabled = false;

            if (Input.GetAxis("Horizontal") != 0 && GroundCheck())       //Moving and on ground
            {
                rb.velocity = new Vector3(rb.velocity.x + fPlayerSpeedStanding * Time.deltaTime * Input.GetAxis("Horizontal"), rb.velocity.y, 0);
                if(!lastGroundCheck)
                {
                    m_fsFoxState = FoxStates.Landing;
                }
                else
                {
                    m_fsFoxState = FoxStates.Running;
                }
            }
            if (Input.GetAxis("Horizontal") != 0 && !GroundCheck()) 
            {
                rb.velocity = new Vector3(rb.velocity.x + (fPlayerSpeedStanding) * 0.8f * Time.deltaTime * Input.GetAxis("Horizontal"), rb.velocity.y, 0);
                //m_fsFoxState = FoxStates.Jumping;
            }
            else if (Input.GetAxis("Horizontal") == 0)
            {
                m_fsFoxState = FoxStates.Standing;
            }
            //Moves the player on the x axis when standing (slower). Slows the players strafe speed down when in the air.

            if (Input.GetButtonDown("Jump") && GroundCheck())
            {
                // rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y + fJumpHeight * 10* Time.deltaTime,0);
                rb.AddForce(new Vector3(0, fJumpHeight * 10f * Time.deltaTime, 0), ForceMode.Impulse);
                m_fsFoxState = FoxStates.Jumping;
            }
            //If space key is pressed and GroundChecks is true (player on ground), the player jumps.

            if(rb.velocity.x >= maxXVel){       //Sets max vel for moving to right
                rb.velocity = new Vector3(maxXVel,rb.velocity.y,0);
            }
            if (rb.velocity.x <= -maxXVel){     //Sets max vel for moving to left
                rb.velocity = new Vector3(-maxXVel, rb.velocity.y, 0);
            }
        }

       if(Input.GetAxis("Horizontal") > 0)
        {
            rb.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            rb.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }

       if(Input.GetAxis("Horizontal") == 0 && GroundCheck())
       {
            rb.velocity = new Vector3((rb.velocity.x * 0.8f), 0, 0);    //Prevents inertia 
       }
       
       if (!GroundCheck()) {                //If not touching the ground, remove friction
            standCol.material.dynamicFriction = 0;
            standCol.material.staticFriction = 0;
       }
       else if (GroundCheck()) {                //If touching the ground, apply friction
            standCol.material.dynamicFriction = 0.6f;    //These two statements prevent the player from sticking to the side of objects
            standCol.material.staticFriction = 0.6f;
       }

       if (Input.GetButtonDown("Stance")) { bStance = Invert(bStance); }   //Changes the stance when left ctrl is pressed
    }

    bool Invert(bool val) { return !val; }          //Function to invert a bool. Used for stance change

    bool GroundCheck()
    {        //Function to check if the player is grounded using raycast
        bool checkMid = Physics.Raycast(trans.transform.position, Vector3.down, fDistToGround + fGroundCheckExtent);        //Raycheck for mid
        bool checkRight = Physics.Raycast(trans.transform.position + new Vector3(0,0,fXExtents), Vector3.down, fDistToGround + fGroundCheckExtent);     //Raycheck for right
        bool checkLeft = Physics.Raycast(trans.transform.position - new Vector3(0, 0, fXExtents), Vector3.down, fDistToGround + fGroundCheckExtent);    //Raycheck for left

        if (checkMid || checkRight || checkLeft)        //Checks that any of the raycasts are true
        {
            if (!lastGroundCheck)
            {
                m_fsFoxState = FoxStates.Landing;       //Changes animation state to landing
            }
            lastGroundCheck = true;
            return true;
        }
        else
        {
            lastGroundCheck = false;
            return false;
        }
    }

    Animator m_anFox;

    enum FoxStates
    {
        Standing,
        Running,
        Crawling,
        Jumping,
        Landing
    }
    FoxStates m_fsFoxState;
    bool m_bStanding = false;
    bool m_bLanded = false;

    IEnumerator manageAnimator()
    {
        while (true)
        {
            switch (m_fsFoxState)
            {
                case FoxStates.Standing:
                    m_anFox.SetBool("Standing", true);
                    m_anFox.SetBool("Crawling", false);
                    m_anFox.SetBool("Running", false);
                    m_bStanding = true;
                    break;
                case FoxStates.Running:
                    m_anFox.SetBool("Running", true);
                    break;
                case FoxStates.Crawling:
                    m_anFox.SetBool("Standing", false);
                    m_anFox.SetBool("Crawling", true);
                    m_anFox.SetBool("Running", false);
                    m_bStanding = false;
                    break;
                case FoxStates.Jumping:
                    //m_anFox.SetBool("Jumping", true);
                    //m_anFox.SetBool("Landing", false);
                    //m_bLanded = false;
                    break;
                case FoxStates.Landing:
                    //m_anFox.SetBool("Jumping", false);
                    m_anFox.SetBool("Landing", true);
                    Debug.Log("Landing State hit");
                    m_bLanded = true;
                    break;
            }
            yield return null;
        }
    }
}
