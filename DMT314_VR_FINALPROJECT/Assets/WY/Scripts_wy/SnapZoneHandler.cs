using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapZoneHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WY_Snap Grabbable"))
        {
            other.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WY_Snap Grabbable"))
        {
            other.transform.SetParent(null);
        }
    }
}
