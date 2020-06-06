using System;
using UnityEditor;
using UnityEngine;
using Ping;

[CustomEditor(typeof(PingHandler))]
public class PingEditor : Editor
{
    void OnSceneGUI()
    {
        var pingHandler = (PingHandler)target;
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
