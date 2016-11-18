using UnityEngine;

public class SuperMushroom : MonoBehaviour
{
    public float MaxSpeed = 4f;
    private Rigidbody2D _rigidbody2D;
    private int _move = -1;
    public LayerMask WhatIsGround;

    // Use this for initialization
    void Start ()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        _rigidbody2D.velocity = new Vector2(_move * MaxSpeed, _rigidbody2D.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D other)
    {       
        var ground = other.gameObject.layer == LayerMask.NameToLayer("BlockingLayer");
        if (!ground)
        {
            _move = _move * -1;
        }
    }
}
