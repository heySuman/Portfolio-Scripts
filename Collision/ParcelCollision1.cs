using UnityEngine;

public class ParcelCollision1 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding is the car
        if (other.CompareTag("Agent1"))
        {
            Debug.Log("Parcel Picked Up by Agent1!");
            Destroy(gameObject);
            
            CollisionInfo.SetCollectedCount("Agent1");
        }
    }
}
