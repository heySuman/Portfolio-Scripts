using UnityEngine;

public class ParcelCollision2 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding is the car
        if (other.CompareTag("Agent2"))
        {
            Debug.Log("Parcel Picked Up by Agent2!");
            Destroy(gameObject);

            CollisionInfo.SetCollectedCount("Agent2");
        }
    }
}
