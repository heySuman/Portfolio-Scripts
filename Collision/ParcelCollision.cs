using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParcelCollision : MonoBehaviour
{
    // Dictionary to store parcel count for each agent
    private static Dictionary<string, int> agentParcelCount = new Dictionary<string, int>();

    public static int GetCollectedCount(string agentTag)
    {
        if (agentParcelCount.ContainsKey(agentTag))
        {
            return agentParcelCount[agentTag];
        }
        return 0;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding is an agent
        if (other.CompareTag("Agent1") || other.CompareTag("Agent2") || other.CompareTag("Agent3"))
        {
            string agentTag = other.tag;

            Debug.Log($"Parcel Picked Up by {agentTag}!");

            // Increment the parcel count for the specific agent
            if (!agentParcelCount.ContainsKey(agentTag))
            {
                agentParcelCount[agentTag] = 0; // Initialize if not present
            }

            agentParcelCount[agentTag]++;

            // Destroy the parcel object
            Destroy(gameObject);
        }
    }
}
