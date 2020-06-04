using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ViewVisualizer))]
public class ViewVisualizerEditor : Editor
{
    void OnSceneGUI()
    {
        ViewVisualizer viewVisualizer = (ViewVisualizer)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(viewVisualizer.transform.position, Vector3.up, Vector3.forward, 360, viewVisualizer.ViewRadius);

        Vector3 viewAngleA = viewVisualizer.DirFromAngle(-viewVisualizer.ViewAngle/2, null, false);
        Vector3 viewAngleB = viewVisualizer.DirFromAngle(viewVisualizer.ViewAngle/2, null, false);

        Handles.DrawLine(viewVisualizer.transform.position, viewVisualizer.transform.position + viewAngleA * viewVisualizer.ViewRadius);
        Handles.DrawLine(viewVisualizer.transform.position, viewVisualizer.transform.position + viewAngleB * viewVisualizer.ViewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTargets in viewVisualizer.visibleTargets)
        {
            Handles.DrawLine(viewVisualizer.transform.position, visibleTargets.position);
        }
    }
}
