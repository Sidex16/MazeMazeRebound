using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destinationTeleport; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            other.transform.position = destinationTeleport.position;
        }
    }
}
