using UnityEngine;
using System.Collections;

public class Mario : MonoBehaviour
{
    public float maxSpeed = 10f;

    private Animator marioAnimator;
    private Rigidbody2D rigidbody2D;
    private int coins;
    bool facingRight = true;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 500f;

	// Use this for initialization
	void Start ()
	{
	    marioAnimator = GetComponent<Animator>();
	    rigidbody2D = GetComponent<Rigidbody2D>();
	    coins = 0;
	}

    private void OnDisable()
    {
        GameManager.instance.playerCoins = coins;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        marioAnimator.SetBool("Ground", grounded);
        marioAnimator.SetFloat("vSpeed", rigidbody2D.velocity.y);

	    float move = Input.GetAxis("Horizontal");

        marioAnimator.SetFloat("Speed", Mathf.Abs(move));

        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

	    if (move > 0 && !facingRight)
	    {
	        Flip();
	    }

        else if (move < 0 && facingRight)
        {
            Flip();
        }
    }

    void Update()
    {
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            marioAnimator.SetBool("Ground", false);
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
