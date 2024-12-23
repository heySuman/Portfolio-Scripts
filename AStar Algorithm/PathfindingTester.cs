using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PathfindingTester : MonoBehaviour
{
    public float baseSpeed = 20f;
    private float moveSpeed;
    public bool isStopped = false;

    public float MoveSpeed
    {
        get => isStopped ? 0 : moveSpeed;
        set
        {
            moveSpeed = value;
            if (value == 0) isStopped = true;
        }
    }

    public TextMeshProUGUI speedInfoText;
    public TextMeshProUGUI parcelToCollectInfo;
    private AStarManager AStarManager = new AStarManager();
    private List<GameObject> Waypoints = new List<GameObject>();
    private List<Connection> ConnectionArray = new List<Connection>();
    public GameObject start;
    public GameObject end;
    public GameObject parcelPrefab;
    public int parcelCount;
    private Vector3 OffSet = new Vector3(0, 0.3f, 0);
    private List<Vector3> pathPoints = new List<Vector3>();
    private int currentWaypointIndex = 0;
    private float distanceThreshold = 0.1f;
    private bool isReturning = false;
    private string agentTag;

    void Start()
    {
        MoveSpeed = baseSpeed;
        agentTag = gameObject.tag;

        transform.position = start.transform.position;

        parcelToCollectInfo.text = $"Total Parcel to Collect: {parcelCount}";

        for (int i = 0; i < parcelCount; i++)
        {
            ParcelGenerator.GenerateParcel(parcelPrefab, end);
        }

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

        // Shortest path in the array
        foreach (Connection aConnection in ConnectionArray)
        {
            pathPoints.Add(aConnection.GetFromNode().transform.position + OffSet);
        }

        // Add final destination node to the path.
        pathPoints.Add(end.transform.position + OffSet);
    }

    void OnDrawGizmos()
    {
        // Draw path.
        foreach (Connection aConnection in ConnectionArray)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(aConnection.GetFromNode().transform.position + OffSet,
            aConnection.GetToNode().transform.position + OffSet);
        }
    }

    void Update()
    {
        if (isStopped)
        {
            // When the agent is stopped, set speed to 0 and display "Speed: 0"
            speedInfoText.text = "Speed: 0";
            return; 
        }

        if (pathPoints.Count == 0 || currentWaypointIndex >= pathPoints.Count)
            return;

        Vector3 targetWaypoint = pathPoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint - transform.position).normalized;

        // Rotate toward the target
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, MoveSpeed * Time.deltaTime / 2f);
        }

        // Move toward the target waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, MoveSpeed * Time.deltaTime);

        // Update speed based on parcel collection
        int collectedCount = CollisionInfo.GetCollectedCount(agentTag);
        MoveSpeed = baseSpeed * Mathf.Pow(0.9f, collectedCount);

        // Ensure speed doesn't go below a certain threshold (e.g., 1 unit)
        MoveSpeed = Mathf.Max(MoveSpeed, 1f);

        // Update the speed UI text
        speedInfoText.text = $"Speed: {MoveSpeed:F2}";

        // If the agent has reached the current waypoint
        if (Vector3.Distance(transform.position, targetWaypoint) < distanceThreshold)
        {
            currentWaypointIndex++;

            if (!isReturning && currentWaypointIndex == pathPoints.Count)
            {
                Debug.Log("Reached the destination. Returning to the start.");
                isReturning = true;
                pathPoints.Reverse();
                currentWaypointIndex = 0;
            }
            else if (isReturning && currentWaypointIndex == pathPoints.Count)
            {
                Debug.Log("Returned to the start. Journey complete.");
                enabled = false;
                speedInfoText.text = "Speed: 0";
            }
        }
    }

    public void ResumeAgent()
    {
        isStopped = false;
        MoveSpeed = baseSpeed;
    }
}
