using System;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PingController))]
public class PingEditor : Editor
{
    void OnSceneGUI()
    {
        var pingHandler = (PingController)target;
        if (pingHandler != null)
        {
            Handles.color = Color.white;
            Handles.DrawWireArc(pingHandler.transform.position, Vector3.up, Vector3.forward, 360, pingHandler.PingRadius);
        }
        else
        {
            throw new ArgumentNullException(nameof(pingHandler));
        }
    }
}
