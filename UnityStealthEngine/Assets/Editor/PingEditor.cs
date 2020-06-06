using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Ping.PingHandler))]
public class PingEditor : Editor
{
    void OnSceneGUI()
    {
        Ping.PingHandler pingHandler = (Ping.PingHandler)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(pingHandler.transform.position, Vector3.up, Vector3.forward, 360, pingHandler.pingRadius);
    }
}
