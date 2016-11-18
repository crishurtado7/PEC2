using UnityEngine;

public class Goomba : MonoBehaviour
{
    public float MaxSpeed = 7f;
    public float MinimumDistanceWithPlayer = 10;

    private Animator _goombaAnimator;
    private Rigidbody2D _rigidbody2D;
    private int _move = -1;
    private GameObject _player;   
    private bool _isDead;
    private bool _isActive;

    private CircleCollider2D _circleCollider2D;

    void Start()
    {
        _isDead = false;
        _isActive = false;
        _goombaAnimator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _goombaAnimator.SetBool("Active", false);
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void Dead()
    {
        _isDead = true;
        _circleCollider2D.enabled = false;
        _goombaAnimator.SetBool("Dead", true);
        Destroy(gameObject, 0.5f);
    }

    void FixedUpdate()
    {
        if (!_isDead)
        {
            if (!_isActive)
            {
                var playerDistance = CalculateDistanceWithPlayer();

                if (playerDistance <= MinimumDistanceWithPlayer)
                {
                    _isActive = true;
                    _goombaAnimator.SetBool("Active", true);
                }
            }
            if (_isActive)
            {
                _rigidbody2D.velocity = new Vector2(_move * MaxSpeed, _rigidbody2D.velocity.y);
            }
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
