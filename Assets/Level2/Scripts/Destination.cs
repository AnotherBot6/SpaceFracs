using UnityEngine;

public class Destination : MonoBehaviour
{
    public Teletransporte teleporter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("source"))
        {
            teleporter.TeleportToDestination();
        }
    }
}
