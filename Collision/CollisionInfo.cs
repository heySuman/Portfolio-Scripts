using System.Collections.Generic;
using UnityEngine;

public class CollisionInfo : MonoBehaviour
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

    public static void SetCollectedCount(string agentTag){
        // Increment the parcel count for the specific agent
            if (!agentParcelCount.ContainsKey(agentTag))
            {
                agentParcelCount[agentTag] = 0;
            }

            agentParcelCount[agentTag]++;
    }

}
