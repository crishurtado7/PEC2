using UnityEngine;

public class ItemBlock : MonoBehaviour
{
    private GameObject _reward;
    private Transform _transform;
    private Animator _animator;

    // Use this for initialization
    void Start()
    {
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetupReward(GameObject reward)
    {
        _reward = reward;
    }

    public void ReturnReward()
    {
        _animator.SetBool("Used", true);
        Instantiate(_reward, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
    }
}
