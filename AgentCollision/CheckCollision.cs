using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    public GameObject[] agents; // Assign agents in the Inspector
    private float collisionThreshold = 15f; // Distance threshold for collision
    private float resumeThreshold = 20f; // Distance threshold to resume movement

    void Start()
    {
        if (agents == null || agents.Length == 0)
        {
            Debug.LogError("Agents array is not initialized!");
            return;
        }
    }

    void Update()
    {
        for (int i = 0; i < agents.Length; i++)
        {
            for (int j = i + 1; j < agents.Length; j++)
            {
                HandlePotentialCollision(agents[i], agents[j]);
            }
        }
    }

    void HandlePotentialCollision(GameObject agentA, GameObject agentB)
    {
        if (agentA == null || agentB == null) return;

        float distance = Vector3.Distance(agentA.transform.position, agentB.transform.position);

        if (distance <= collisionThreshold)
        {
            var agentAScript = agentA.GetComponent<PathfindingTester>();
            var agentBScript = agentB.GetComponent<PathfindingTester>();

            if (agentAScript != null && agentBScript != null)
            {
                float speedA = agentAScript.MoveSpeed;
                float speedB = agentBScript.MoveSpeed;

                // Stop the agent with less speed
                if (speedA < speedB)
                {
                    agentAScript.MoveSpeed = 0;
                    Debug.Log($"{agentA.name} stopped to avoid collision with {agentB.name}");
                }
                else if (speedB < speedA)
                {
                    agentBScript.MoveSpeed = 0;
                    Debug.Log($"{agentB.name} stopped to avoid collision with {agentA.name}");
                }
                else
                {
                    // If speeds are equal, stop agent A arbitrarily
                    agentAScript.MoveSpeed = 0;
                    Debug.Log($"{agentA.name} stopped to avoid collision with {agentB.name} (equal speeds)");
                }
            }
        }
        else
        {
            // Resume movement once the agents are far enough apart
            var agentAScript = agentA.GetComponent<PathfindingTester>();
            var agentBScript = agentB.GetComponent<PathfindingTester>();

            if (agentAScript != null && agentBScript != null)
            {
                // If the agents are far enough, resume movement
                if (Vector3.Distance(agentA.transform.position, agentB.transform.position) > resumeThreshold)
                {
                    if (agentAScript.MoveSpeed == 0)
                    {
                        agentAScript.ResumeAgent();
                        Debug.Log($"{agentA.name} resumed movement after avoiding collision with {agentB.name}");
                    }

                    if (agentBScript.MoveSpeed == 0)
                    {
                        agentBScript.ResumeAgent();
                        Debug.Log($"{agentB.name} resumed movement after avoiding collision with {agentA.name}");
                    }
                }
            }
        }
    }
}
