using UnityEngine;

/**
 * Creates an Grid gridSize x gridSize x gridSize (X*Y*Z) with gridCubes of the scale of the gridCubePrefab
 */
public class GridCreator : MonoBehaviour
{
    public GameObject gridCubePrefab;
    public int gridCubeScale;
    public int gridSize;

    void Start()
    {
        CreateGrid();
    }
    private void CreateGrid()
    {
        GameObject raster = new GameObject("Grid");

        for (int x = -gridSize/2; x < gridSize; x += gridCubeScale)
        {
            for (int y = 0; y < gridSize; y += gridCubeScale)
            {
                for (int z = -gridSize/2; z < gridSize; z += gridCubeScale)
                {
                    GameObject obj = Instantiate(gridCubePrefab);
                    obj.transform.position = new Vector3(x, y, z);
                    obj.transform.parent = raster.transform;
                }
            }
        }
    }
}
