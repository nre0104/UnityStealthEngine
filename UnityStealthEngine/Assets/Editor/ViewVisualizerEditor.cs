using UnityEditor;
using UnityEngine;

namespace Vision
{
    [CustomEditor(typeof(ViewVisualizer))]
    public class ViewVisualizerEditor : Editor
    {
        void OnSceneGUI()
        {
            ViewVisualizer viewVisualizer = (ViewVisualizer) target;
            Handles.color = Color.white;
            Handles.DrawWireArc(viewVisualizer.transform.position, Vector3.up, Vector3.forward, 360,
                viewVisualizer.viewRadius);

            Vector3 viewAngleA = viewVisualizer.DirFromAngle(-viewVisualizer.viewAngle / 2, false);
            Vector3 viewAngleB = viewVisualizer.DirFromAngle(viewVisualizer.viewAngle / 2, false);

            Handles.DrawLine(viewVisualizer.transform.position,
                viewVisualizer.transform.position + viewAngleA * viewVisualizer.viewRadius);
            Handles.DrawLine(viewVisualizer.transform.position,
                viewVisualizer.transform.position + viewAngleB * viewVisualizer.viewRadius);

            Handles.color = Color.red;

            foreach (Transform visibleTargets in viewVisualizer.visibleTargets)
            {
                Handles.DrawLine(viewVisualizer.transform.position, visibleTargets.position);
            }
        }
    }
}