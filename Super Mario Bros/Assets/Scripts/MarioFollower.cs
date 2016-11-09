using UnityEngine;

public class MarioFollower : MonoBehaviour
{
    public Transform player;
    private int distance = 3;

    void Update()
    {
        var position = new Vector3(player.position.x, -7, -10);
        transform.position = position;
    }

}
