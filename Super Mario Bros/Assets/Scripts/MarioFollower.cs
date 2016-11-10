using UnityEngine;

public class MarioFollower : MonoBehaviour
{
    public Transform Player;
    private const int YPosition = -7;

    void Update()
    {
        transform.position = new Vector3(Player.position.x, YPosition, -10);
    }

}
