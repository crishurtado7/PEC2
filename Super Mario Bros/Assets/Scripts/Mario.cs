﻿using System;
using UnityEngine;

public class Mario : MonoBehaviour
{
    public Transform GroundCheck;
    public LayerMask WhatIsGround;
    public float JumpForce = 16f;
    public float MaxSpeed = 7f;
    public AudioClip JumpSound;
    public AudioClip PowerUpSound;

    private Animator _marioAnimator;
    private Rigidbody2D _rigidbody2D;
    private bool _facingRight = true;
    private bool _grounded;    
    private float groundRadius = 0.2f;
    private bool _isDead;
    private int _health;
    private bool _isBig;

    private BoxCollider2D _boxCollider2D;
    private CircleCollider2D _circleCollider2D;
    private AudioSource _audioSource;

    private const int yPositionDeath = -14;

	// Use this for initialization
	void Start ()
	{
        _isDead = false;
        _health = 1;
	    _isBig = false;
        _marioAnimator = GetComponent<Animator>();
	    _rigidbody2D = GetComponent<Rigidbody2D>();
	    _circleCollider2D = GetComponent<CircleCollider2D>();
	    _boxCollider2D = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        _rigidbody2D.isKinematic = false;
    }

    // Update is called once per frame
    void FixedUpdate ()
	{
        if (!_isDead)
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
    }

    private void PlaySound(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }

    void Update()
    {
        CheckYPosition();
        if (_grounded && Input.GetKeyDown(KeyCode.Space))
        {
            _marioAnimator.SetBool("Ground", false);
            PlaySound(JumpSound);
            _rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }

    private void CheckYPosition()
    {
        if(!_isDead && transform.position.y < yPositionDeath)
        {
            GameManager.instance.GameOver();
        }
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            //Invoke("Restart", restartLevelDelay);
            //enabled = false;
        }
    }

    public void Dead()
    {
        _isDead = true;
        _rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        foreach (Collider2D collider in gameObject.GetComponents<Collider2D>()) collider.enabled = false;
        _marioAnimator.SetBool("Dead", true);
        GameManager.instance.Invoke("GameOver", 1f);
    }

    public void OnDestroy()
    {
        GameManager.instance.GameOver();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;

        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 center = collider.bounds.center;

        if (collider.tag == "Enemy")
        {
            bool top = contactPoint.y > center.y;
            _health--;

            if (top)
            {
                collider.gameObject.SendMessage("Dead");
            }

            else if(_health == 0)
            {
                Dead();
            }

            else if(_isBig)
            {
                _isBig = false;
                _marioAnimator.SetBool("Big", false);
                ResizeComponents(false);
            }
        }

        else if(collider.tag == "BreakingBlock")
        {
            bool bottom = contactPoint.y < center.y;

            if (bottom && _isBig)
            {
                Destroy(collider.gameObject);
            }
        }

        else if(collider.tag == "ItemBlock")
        {
            bool bottom = contactPoint.y < center.y;

            if (bottom)
            {
                collider.gameObject.SendMessage("ReturnReward");
            }
        }

        else if (collider.tag == "SuperMushroom")
        {
            if (!_isBig)
            {
                _isBig = true;
                ++_health;            
                _marioAnimator.SetBool("Big", true);
                ResizeComponents(true);
            }
            PlaySound(PowerUpSound);
            Destroy(collider.gameObject);
        }
    }

    private void ResizeComponents(bool toUpper)
    {
        var size = toUpper ? 1 : -1;
        GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y + size * .5f, GetComponent<Transform>().position.z);
        _boxCollider2D.size = new Vector2(_boxCollider2D.size.x + size * .2f, _boxCollider2D.size.y + size * 1f);
        _circleCollider2D.offset = new Vector2(_circleCollider2D.offset.x, _circleCollider2D.offset.y - size * .5f);
        GroundCheck.position = new Vector3(GroundCheck.position.x, GroundCheck.position.y - size * .5f, GroundCheck.position.z);
    }
}
