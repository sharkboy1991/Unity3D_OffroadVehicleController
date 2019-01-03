using UnityEngine;
using System.Collections;

public class WheelPosition : MonoBehaviour
{
    public WheelCollider _wCol;

    private Vector3 vCenter;
    private RaycastHit hit;

    void Update()
    {
        vCenter = _wCol.transform.TransformPoint(_wCol.center);

        if (Physics.Raycast(vCenter, -_wCol.transform.up, out hit, _wCol.suspensionDistance + _wCol.radius))
        {
            transform.position = hit.point + (_wCol.transform.up * _wCol.radius);
        }
        else
        {
            transform.position = vCenter - (_wCol.transform.up * _wCol.suspensionDistance);
        }
    }
}