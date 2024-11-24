using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Assertions.Must;

public class ParcelCollision : MonoBehaviour
{
    static bool collectionStatus = false;

    public static bool GetCollectionStatus()
    {
        return collectionStatus;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding is the car
        if (other.CompareTag("Car"))
        {
            Debug.Log("Parcel Picked Up by Car!");
            Destroy(gameObject);
            collectionStatus = true;
        }
    }
}
