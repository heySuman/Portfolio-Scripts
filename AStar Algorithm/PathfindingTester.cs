using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTester : MonoBehaviour
{
    public float moveSpeed = 20f;
    private AStarManager AStarManager = new AStarManager();
    private List<GameObject> Waypoints = new List<GameObject>();
    private List<Connection> ConnectionArray = new List<Connection>();
    public GameObject start;
    private GameObject end;
    private Vector3 OffSet = new Vector3(0, 0.3f, 0);
    private List<Vector3> pathPoints = new List<Vector3>();
    private int currentWaypointIndex = 0;
    private float distanceThreshold = 0.1f;


    void Start()
    {
        // Get the parcel's end node.
        end = GenerateParcel.GetEndNode();
        transform.position = start.transform.position;

        if (start == null || end == null)
        {
            Debug.Log("No start or end waypoints.");
            return;
        }

        // Find all waypoints in the level.
        GameObject[] GameObjectsWithWaypointTag = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (GameObject waypoint in GameObjectsWithWaypointTag)
        {
            WaypointCON tmpWaypointCon = waypoint.GetComponent<WaypointCON>();
            if (tmpWaypointCon)
            {
                Waypoints.Add(waypoint);
            }
        }

        // Create connections for A*.
        foreach (GameObject waypoint in Waypoints)
        {
            WaypointCON tmpWaypointCon = waypoint.GetComponent<WaypointCON>();
            foreach (GameObject WaypointConNode in tmpWaypointCon.Connections)
            {
                Connection aConnection = new Connection();
                aConnection.SetFromNode(waypoint);
                aConnection.SetToNode(WaypointConNode);
                AStarManager.AddConnection(aConnection);
            }
        }

        // Find path using A*.
        ConnectionArray = AStarManager.PathfindAStar(start, end);

        // Reset path points and populate them.
        foreach (Connection aConnection in ConnectionArray)
        {
            pathPoints.Add(aConnection.GetFromNode().transform.position + OffSet);
        }

        // Add final destination node to the path.
        pathPoints.Add(end.transform.position + OffSet);
    }

    void Update()
    {
        // If there are no path points or we've reached the end of the path, stop
        if (pathPoints.Count == 0 || currentWaypointIndex >= pathPoints.Count)
            return;


        // Move the car along the path
        Vector3 targetWaypoint = pathPoints[currentWaypointIndex];

        // for rotation
        Vector3 direction = (targetWaypoint - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
        }

        // move the car toward the next waypoint in the array
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, moveSpeed * Time.deltaTime);

        // If the car has reached the current waypoint
        if (Vector3.Distance(transform.position, targetWaypoint) < distanceThreshold)
        {
            currentWaypointIndex++;

            // Check if we reached the final point in the reversed path (which is the start)
            if (currentWaypointIndex == pathPoints.Count)
            {
                // Finished the return journey
                Debug.Log("Returning to the home");
            }
        }


    }

}
