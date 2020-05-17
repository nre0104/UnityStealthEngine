using UnityEngine;

public class Builder : MonoBehaviour
{
    public LayerMask gridLayer;
    public Camera camera;
    public GameObject wall;
    public GameObject ramp;
    public GameObject floor;
    public Material previewMaterial;
    public int buldingMaxRange;

    private RaycastHit hit;
    private float gridWidth = 2.5f;
    private GameObject preview;
    private int placeableObjectCounter = 0;
    private Material objectMaterial;

    void Update()
    {
        Vector3 placeablePosition = new Vector3(0, 0, 0);

        // Get position of grid-cube in front of player
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        Debug.DrawLine(ray.origin, ray.GetPoint(buldingMaxRange));

        if (Physics.Raycast(ray, out hit, buldingMaxRange, gridLayer))
        {
            // Update position only if no preview shown
            if (preview != null)
            {
                placeablePosition = hit.transform.position;
                preview.transform.position = placeablePosition;
            }
        }

        // Builder mode activated
        if (Input.GetKeyDown(KeyCode.R) && hit.transform != null)
        {
            Debug.Log("Hit ");

            // Choose placable
            if (preview != null)
            {
                Destroy(preview);
            }

            placeableObjectCounter++;

            if (placeableObjectCounter > 3)
            {
                placeableObjectCounter = 0;
            }

            Debug.Log("Placeable: " + placeableObjectCounter);

            // Instantiate preview object
            if (placeableObjectCounter == 1)
            {
                preview = Instantiate(wall,
                    new Vector3(placeablePosition.x + gridWidth, placeablePosition.y,
                        placeablePosition.z), hit.transform.rotation);
            }
            else if (placeableObjectCounter == 2)
            {
                preview = Instantiate(ramp,
                    new Vector3(placeablePosition.x + gridWidth, placeablePosition.y,
                        placeablePosition.z), hit.transform.rotation);
            }
            else if (placeableObjectCounter == 3)
            {
                preview = Instantiate(floor,
                    new Vector3(placeablePosition.x + gridWidth, placeablePosition.y,
                        placeablePosition.z), hit.transform.rotation);
            }

            objectMaterial = preview.transform.GetChild(0).GetComponent<MeshRenderer>().material;
            preview.transform.GetChild(0).GetComponent<MeshRenderer>().material = previewMaterial;
        }

        // Rotate placable in grid via mouse scrollwheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && preview != null) // +
        {
            preview.transform.transform.Rotate(new Vector3(preview.transform.rotation.x,
                +90, preview.transform.rotation.z));
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && preview != null) // -
        {
            preview.transform.transform.Rotate(new Vector3(preview.transform.rotation.x,
                -90, preview.transform.rotation.z));
        }

        // Make preview a "real" object
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (preview != null)
            {
                preview.transform.GetChild(0).GetComponent<MeshRenderer>().material = objectMaterial;
                preview = null;
                placeableObjectCounter = 0;
            }
        }
    }
}