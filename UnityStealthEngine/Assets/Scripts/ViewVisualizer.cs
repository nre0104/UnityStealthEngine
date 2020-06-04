using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewVisualizer : MonoBehaviour
{
    public float ViewRadius;
    [Range(0, 360)]
    public float ViewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    public bool visualizeViewInScene = true;
    public float MeshResolution;

    public MeshFilter ViewMeshFilter;
    private Mesh viewMesh;

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    void LateUpdate()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        ViewMeshFilter.mesh = viewMesh;

        DrawFieldOfView();
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, ViewRadius, targetMask);

        for (int i = 0; i < targetsInRadius.Length; i++)
        {
            Transform target = targetsInRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < ViewAngle/2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(ViewAngle * MeshResolution);
        float stepAngleSize = ViewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - ViewAngle/2 + stepAngleSize * i;

            if (visualizeViewInScene)
            {
                ViewCastInfo newViewCastInfo = ViewCast(angle);
                viewPoints.Add(newViewCastInfo.endPoint);
            }
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] verticies = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount-2) * 3];

        verticies[0] = Vector3.zero;
        for (int i = 0; i < vertexCount-1; i++)
        {
            verticies[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount-2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = verticies;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, ViewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * ViewRadius, ViewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 endPoint;
        public float dist;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _endPoint, float _dist, float _angle)
        {
            hit = _hit;
            endPoint = _endPoint;
            dist = _dist;
            angle = _angle;
        }
    }
}
