using UnityEngine;

public class ParcelCollision3 : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding is the car
        if (other.CompareTag("Agent3"))
        {
            Debug.Log("Parcel Picked Up by Agent3!");
            Destroy(gameObject);

            CollisionInfo.SetCollectedCount("Agent3");
        }
    }
}
