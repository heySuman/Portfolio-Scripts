using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Assertions.Must;

public class ParcelInfo : MonoBehaviour
{
    public TextMeshProUGUI parcelInfo;

    // Update is called once per frame
    void Update()
    {
        parcelInfo.text ="Parcel on node:" + GenerateParcel.GetEndNode().name +"\nCollection Status: " + (ParcelCollision.GetCollectionStatus() ? "Completed" : "Pending");
    }
}
