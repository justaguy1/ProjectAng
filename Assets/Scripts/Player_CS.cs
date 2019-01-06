using UnityEngine;
using System.Collections;

public class Player_CS : MonoBehaviour
{
   

	public static Player_CS instance;
	public bool canMove = true;
	public float gravityMultiplier = 2f;
	public float moveDirection;
	public float maxSpeed = 6.0f;



	public GameObject blood;

	public float speed;

	public bool facingRight = true;

	//Jumping
	public bool isGrounded = false;
	Collider[] groundCollisions;
	public Transform groundCheck;
	public float groundCheckRadius = 0.1f;
	public LayerMask groundLayer;
	public float jumpPower = 12f;

	//Double Jump
	public bool canDoubleJump = false;


    //Item
    public bool haveItem = false;

	// wall Slide
	public bool onWall = false;
	public Transform wallCheckPoint;
    public LayerMask wallLayerMask;
	public bool canSlide = true;
    Collider[] wallCollisions;

	Rigidbody _rigidbody;

	private IEnumerator coroutine;
	void Awake(){
		instance = this;
       // angManager = GameObject.Find("AngManager").GetComponent<AngGameManager>();
        groundCheck = GameObject.Find ("GroundCheck").transform;
        wallCheckPoint = GameObject.Find ("WallCheck").transform;
	}

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody> ();

        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;
	}
	
	// Update is called once per frame
	void Update ()
	{
		moveDirection = Input.GetAxis("Horizontal");

		if (canMove && (Input.GetButtonDown ("Jump")) && !onWall)
			jump ();

		if(!isGrounded && (Input.GetKeyDown(KeyCode.F)))
			JumpDown();

		if (!isGrounded) {
			AddGravity ();
			if (Input.GetKeyDown (KeyCode.W)) {
				AddResistance ();
			}
		}
	}

	void FixedUpdate()
	{
        speed = _rigidbody.velocity.x;

		groundCollisions = Physics.OverlapSphere (groundCheck.position, groundCheckRadius, groundLayer);

		if (groundCollisions.Length > 0)
			isGrounded = true;
		else
			isGrounded = false;
        
        if (canMove) {
			_rigidbody.velocity = new Vector2 (moveDirection * maxSpeed, _rigidbody.velocity.y);

			if (Input.GetAxis("Horizontal") > 0.0f && !facingRight) {
				Flip ();
			} else if (Input.GetAxis("Horizontal") < 0.0f && facingRight) {
				Flip ();
			}
		} else {
			// Player should not move
			_rigidbody.velocity = new Vector2 (0, _rigidbody.velocity.y);
		}
	}

    void HandleWallBehaviour()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, -0.2f, _rigidbody.velocity.z);
        canSlide = true;

        if(Input.GetButtonDown("Jump"))
        {
            if(facingRight)
            {
                _rigidbody.velocity = new Vector3(-5, jumpPower, _rigidbody.velocity.z);
                
            }
            else
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, jumpPower, _rigidbody.velocity.z);
            }
        }
    }


    void jump()
	{
		if (isGrounded) {
			_rigidbody.velocity = new Vector3 (_rigidbody.velocity.x, jumpPower, _rigidbody.velocity.z);
			isGrounded = false;
			canDoubleJump = true;
		}
		else 
		{
			if (canDoubleJump) {
				_rigidbody.velocity = new Vector3 (_rigidbody.velocity.x, jumpPower, _rigidbody.velocity.z);
				canDoubleJump = false;
			}
		}
	}

	void AddResistance(){
		
	}
	
	void JumpDown()
	{
		_rigidbody.velocity = new Vector3 (_rigidbody.velocity.x, -jumpPower * 1.5f, _rigidbody.velocity.z);
	}

	void Flip()
	{
		facingRight = !facingRight;
		transform.Rotate (Vector3.up, 180.0f, Space.World);
	}

	public void AddGravity()
	{
		Vector3 extraGravityForce = (Physics.gravity * gravityMultiplier) - Physics.gravity;
		_rigidbody.AddForce (extraGravityForce);
	}

	public void Die(string how)
	{
		canMove = false;
		switch(how)
        {
            case "fall":
                Debug.Log("Watch out the edges");
                break;
            case "height":
                Debug.Log("Watch out the heights");
                Debug.Log("Play fall animation");
                break;
            case "explosive":
                Debug.Log("Watch out the explosive");
                break;

        }
	}

	public void Live()
	{
		canMove = true;
	}

    public void ChangeItem()
    {
        haveItem = !haveItem;
    }

    public bool CheckItem()
    {
        return haveItem;
    }

	public IEnumerator Slide(){
		canSlide = false;
		yield return new WaitForSeconds (0.5f);
		canSlide = true;
	}


    void OnCollisionEnter(Collision coll)
    {
        
        Debug.Log("Velocity" + coll.relativeVelocity.magnitude);
        if (coll.relativeVelocity.magnitude >=23)
        {
		    Instantiate (blood, transform.position, transform.rotation);
            Die("height");
          //  angManager.GoToLastCheckPoint();
        }
    }
}
