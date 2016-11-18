using UnityEngine;

public class Coin : MonoBehaviour
{
    private float _height;

	// Use this for initialization
	void Start ()
	{
	    _height = GetComponent<Transform>().position.y;
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8, ForceMode2D.Impulse);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (GetComponent<Transform>().position.y < _height)
	    {
	        GameManager.instance.playerCoins++;
	        Destroy(gameObject);
	    }
	}
}
