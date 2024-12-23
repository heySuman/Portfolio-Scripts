using UnityEngine;

public class ParcelGenerator : MonoBehaviour
{
    public static void GenerateParcel(GameObject parcel, GameObject node){
        Instantiate(parcel, node.transform.position, node.transform.rotation);
        Debug.Log("Parcel Generated at node " + node.name);
    }
}
