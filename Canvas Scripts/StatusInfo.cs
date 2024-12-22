using TMPro;
using UnityEngine;

public class ParcelStatusDisplay : MonoBehaviour
{
    public TextMeshProUGUI agent1Status;
    public TextMeshProUGUI agent2Status;
    public TextMeshProUGUI agent3Status;

    void Update()
    {
        agent1Status.text = "Agent1 Parcels: " + ParcelCollision.GetCollectedCount("Agent1");
        agent2Status.text = "Agent2 Parcels: " + ParcelCollision.GetCollectedCount("Agent2");
        agent3Status.text = "Agent3 Parcels: " + ParcelCollision.GetCollectedCount("Agent3");
    }
}
