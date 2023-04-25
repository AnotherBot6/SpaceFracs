using UnityEngine;

public class Teletransporte : MonoBehaviour
{
    public Transform destination;

    public void TeleportToDestination()
    {
        transform.position = destination.position;
    }
}

