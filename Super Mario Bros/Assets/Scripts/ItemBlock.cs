using UnityEngine;

public class ItemBlock : MonoBehaviour
{
    private GameObject _reward;
    private Transform _transform;
    private Animator _animator;

    private bool _used;

    // Use this for initialization
    void Start()
    {
        _used = false;
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
        if (!_used)
        {
            _used = true;
            _animator.SetBool("Used", true);
            Instantiate(_reward, new Vector3(_transform.position.x, _transform.position.y + 1, _transform.position.z), Quaternion.identity);
        }
    }
}
