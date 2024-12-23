using TMPro;
using UnityEngine;

public class ParcelStatusDisplay : MonoBehaviour
{
    public TextMeshProUGUI agent1Status;
    public TextMeshProUGUI agent2Status;
    public TextMeshProUGUI agent3Status;

    void Update()
    {
        agent1Status.text = "Parcel Collected: " + CollisionInfo.GetCollectedCount("Agent1");
        agent2Status.text = "Parcel Collected: " + CollisionInfo.GetCollectedCount("Agent2");
        agent3Status.text = "Parcel Collected: " + CollisionInfo.GetCollectedCount("Agent3");
    }
}
