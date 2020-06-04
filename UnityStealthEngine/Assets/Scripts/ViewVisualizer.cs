using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewVisualizer : MonoBehaviour
{
    public float ViewAngle;
    public float ViewRadius;

    public Vector3 DirFromAngle(float angleInDegrees)
    {
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
