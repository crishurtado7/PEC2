using UnityEngine;

public class Goomba : MonoBehaviour
{
    public float MaxSpeed = 7f;   
    public float MinimumDistanceWithPlayer = 10;

    private Animator _goombaAnimator;
    private Rigidbody2D _rigidbody2D;
    private int _move = -1;
    private GameObject _player;

    void Start()
    {
        _goombaAnimator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _goombaAnimator.SetBool("Active", false);
    }

    private void OnDisable()
    {
        _goombaAnimator.SetBool("Dead", true);
        Destroy(this, 5f);
    }

    void FixedUpdate()
    {
        var playerDistance = CalculateDistanceWithPlayer();

        if (playerDistance <= MinimumDistanceWithPlayer)
        {
            _goombaAnimator.SetBool("Active", true);
            _rigidbody2D.velocity = new Vector2(_move * MaxSpeed, _rigidbody2D.velocity.y);
        }
    }

    private float CalculateDistanceWithPlayer()
    {
        return Mathf.Abs(_player.transform.position.x - transform.position.x);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag != "Floor")
        {
            _move = _move * -1;
        }      
    }
}
