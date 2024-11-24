using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateParcel : MonoBehaviour
{
    public GameObject parcelPrefab;
    public GameObject[] nodes;

    static GameObject parcelNode;

    void Start()
    {
        if (nodes == null || nodes.Length == 0)
        {
            nodes = GameObject.FindGameObjectsWithTag("Waypoint");
        }

        // Ensure there are nodes available to spawn the parcel
        if (nodes != null && nodes.Length > 0)
        {
            // Get a random node index
            int randomIndex = Random.Range(1, nodes.Length);

            // Get the random node from the array
            parcelNode = nodes[randomIndex];

            // Instantiate the parcel at the random node's position and rotation
            Instantiate(parcelPrefab, parcelNode.transform.position, parcelNode.transform.rotation);

            Debug.Log("Parcel Generated at " + parcelNode.name );
        }
        else
        {
            Debug.LogWarning("No waypoints found with the tag 'Waypoint'");
        }
    }

    public static GameObject GetEndNode(){
        if(!parcelNode) Debug.LogError("Parcel node not set");
        return parcelNode;
    }
}
