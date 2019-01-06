using UnityEngine;
using System.Collections;

public class Player_controller : MonoBehaviour {

    Rigidbody rb;

    public float horizontal_movement;
    float vertical_movement;
    Vector3 MoveVelocity;
    Vector3 PlayerPos;
    private float MoveSpeed = 1000;
    public float maxSpeed = 500;
    public float minSpeed = 400;

    bool JumpButtonPressed = false;
    int jumpCount = 2;
    public LayerMask groundlayer;
    public int maxJumpCount = 2;
    public float jumpPower=700f;
    
    bool isCarryingPickup;
    bool interact;


    Vector3 PlayerRotation;

    float gravityMultiplier=3;

    Animator anim_control;
    public bool isInAir = false;
    CapsuleCollider col;

    // this is use to toggle between up and down movement based on where the player is facing
    public float verticalMovementMultiplier =1f;



    RaycastHit Hit;

    // this layer mask is used to check if the player is colliding with wall and still trying to move
    // this checks if the player is allowed to move
    public LayerMask layerMaskToAvoidVelocity;

    public static bool PlayerCanMove = true;



   
    void Start ()
    {
        PlayerPos = transform.position;

        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        anim_control = GetComponent<Animator>();
        
       
    }
	
	// Update is called once per frame
	void Update ()
    {
       
        GetInput();
        SetAnimations();


        if (JumpButtonPressed)
        {
            Jump();
        }


        //   Debug.Log("ladder :" + GlobalVariables.stairLayerCollision);
        // Debug.Log("is in air :" + isInAir);
    }

    void GetInput()
    {
        horizontal_movement = Input.GetAxis("Horizontal");
        JumpButtonPressed = Input.GetButtonDown("Jump");

      



        
        
        
        

    }

    private void FixedUpdate()
    {
        isInAir = !Physics.Raycast(transform.position, -Vector3.up, 1.4f, groundlayer);
        //isInAir = !Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 0.5f, groundlayer);
        
        if (GlobalVariables.playerCanRotate)
        {
            changePlayerRotation();
            MoveSpeed = maxSpeed;

        }
        else
        {
            MoveSpeed = minSpeed;
        }

        // DISABLES MOVEMENT IF ALLOWANYMOVEMENT IS FALSE
        if (GlobalVariables.allowAnyMovement && rb != null)
        {
            if (PlayerCanMove)
            {
                characterMovement();
            }
            


        }

        if (GlobalVariables.HorizontalMovement)
            AddGravity();

       
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Platform")
        {
            Debug.Log("Attached");
            transform.parent = col.transform;
        }

       

       
    }

    private void OnCollisionStay(Collision collision)
    {

       
            if (isInAir && !collision.gameObject.CompareTag("Ladder"))
            {
                changeCharacterMovementState();
               // Debug.Log("is in air");
            }
        else
        {
            PlayerCanMove = true;
        }



    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Platform")
        {
            transform.parent = null;
          
        }
        PlayerCanMove = true;
    }

    void PlayerVerticalMovement()
    {
        rb.useGravity = false;
        MoveVelocity.y = verticalMovementMultiplier* horizontal_movement*Time.deltaTime*MoveSpeed/100; // this makes player move in y position
        MoveVelocity.x = 0;
        MoveSpeed = 150F;
        rb.velocity = new Vector3(0, 0, 0);
        transform.Translate(MoveVelocity,Space.Self);
    }

    void PlayerHorizontalMovement()
    {
        MoveVelocity.x = horizontal_movement*Time.deltaTime*MoveSpeed;  // this makes player move in x position
        MoveVelocity.y = rb.velocity.y;
        rb.useGravity = true;
        MoveSpeed =500F;

        if (rb != null)
        {
            rb.velocity = MoveVelocity;

        }
        else
        {
            Debug.Log("No rigid body attached");
        }

        
    }

    void Jump()
    {
        //Alternative method for making character jump
        // bool jump =Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 0.9f, groundlayer);

       // isInAir = !Physics.Raycast(transform.position, -Vector3.up, 1.2f, groundlayer);
        //1.1f below is used to check if ray is colliding with the ground layer


        if (!isInAir)
        {
            jumpCount = 0;

           
            

        }

        if (jumpCount<maxJumpCount)
        {
            //  rb.AddForce(Vector3.up * jumpPower * Time.deltaTime, ForceMode.Impulse);
            rb.velocity = Vector3.up * jumpPower * Time.deltaTime;
            ++jumpCount;
            isInAir = true;
        }
    }


    void characterMovement()
    {
        if (GlobalVariables.HorizontalMovement)
        {
            PlayerHorizontalMovement();
            Debug.Log("horizontal called");
           
        }
        else
        {
            PlayerVerticalMovement();
            Debug.Log("vertical called");
        }

        

       
    }


    void changePlayerRotation()
    {
        if (rb == null)
        {
            return;
        }

        if (GlobalVariables.HorizontalMovement)
        {
            float newRot = GlobalVariables.Get_Y_Rot_from_Velocity(rb.velocity);
            PlayerRotation.y = newRot;


            if (Mathf.Abs(horizontal_movement) > 0.1)
            {
                //  transform.rotation = Quaternion.Euler(PlayerRotation);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(PlayerRotation), 1600 * Time.deltaTime);
            }
        }
        else
        {
            if (verticalMovementMultiplier == 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
      
        
        
    }

    public void AddGravity()
    {
        Vector3 extraGravityForce = (Physics.gravity * gravityMultiplier);
        rb.AddForce(extraGravityForce);
    }

    void SetAnimations()
    {
        if (anim_control == null) return;
        
        anim_control.SetFloat("MoveX", Mathf.Abs(horizontal_movement));
        anim_control.SetBool("IsInAir", isInAir);
    }

    void changeCharacterMovementState()
    {

        if (horizontal_movement >0.1)
        {
            PlayerCanMove = Physics.Raycast(transform.position, Vector3.right, 5f, layerMaskToAvoidVelocity);
        }
        else if (horizontal_movement <-0.1)
        {
            PlayerCanMove = Physics.Raycast(transform.position, Vector3.left, 5f, layerMaskToAvoidVelocity);
        }
      
        //Debug.Log("Player is colliding : " + PlayerCanMove);
    }

  

 







}
