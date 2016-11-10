using UnityEngine;

public class Mario : MonoBehaviour
{
    public float MaxSpeed = 7f;
    public Transform GroundCheck;
    public LayerMask WhatIsGround;
    public float JumpForce = 10f;

    private Animator _marioAnimator;
    private Rigidbody2D _rigidbody2D;
    private bool _facingRight = true;
    private bool _grounded;    
    private float groundRadius = 0.2f;

    private BoxCollider2D _boxCollider2D;
    private CircleCollider2D _circleCollider2D;

	// Use this for initialization
	void Start ()
	{
	    _marioAnimator = GetComponent<Animator>();
	    _rigidbody2D = GetComponent<Rigidbody2D>();
	    _circleCollider2D = GetComponent<CircleCollider2D>();
	    _boxCollider2D = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    _grounded = Physics2D.OverlapCircle(GroundCheck.position, groundRadius, WhatIsGround);
        _marioAnimator.SetBool("Ground", _grounded);

	    float move = Input.GetAxis("Horizontal");

        _marioAnimator.SetFloat("Speed", Mathf.Abs(move));

        _rigidbody2D.velocity = new Vector2(move * MaxSpeed, _rigidbody2D.velocity.y);

	    if (move > 0 && !_facingRight)
	    {
	        Flip();
	    }

        else if (move < 0 && _facingRight)
        {
            Flip();
        }
    }

    void Update()
    {
        if (_grounded && Input.GetKeyDown(KeyCode.Space))
        {
            _marioAnimator.SetBool("Ground", false);
            _rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    if(other.gameObject.tag == "BreakingBlock")
    //    {
    //        Destroy(other.gameObject);
    //    }       
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            //Invoke("Restart", restartLevelDelay);
            //enabled = false;
        }
        else if (other.tag == "SuperMushroom")
        {
            //food += pointsPerFood;
            //other.gameObject.SetActive(false);
        }
        //else if (other.tag == "Soda")
        //{
        //    food += pointsPerSoda;
        //    other.gameObject.SetActive(false);
        //}
    }

    private void Dead()
    {
        _marioAnimator.SetBool("Dead", true);
        gameObject.SetActive(false);
        Destroy(this, 5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;

        if (collider.tag == "Enemy")
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collider.bounds.center;

            bool top = contactPoint.y > center.y;

            if (top)
            {
                collider.gameObject.SetActive(false);
            }

            else
            {
                Dead();
            }
        }
    }
}
