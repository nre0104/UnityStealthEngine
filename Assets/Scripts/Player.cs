using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject gun;
    public GameObject boxPrefab;
    public GameObject ballPrefab;
    public GameObject bombObject;
    public int throwForce;

    public LayerMask interactionLayer;
    public Camera camera;
    public float maxDist;

    private GameObject objInHand;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in gun.transform)
        {
            if (child.name == "BulletOrigin")
            {
                var origin = child.gameObject;
            }
        }

        gun.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 0.25f;

            } else if (Time.timeScale == 0.25f)
            {
                Time.timeScale = 1f;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(boxPrefab, new Vector3(rightHand.transform.position.x, rightHand.transform.position.y, rightHand.transform.position.z), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject ball = Instantiate(ballPrefab, rightHand.transform.position, Quaternion.identity);
            Rigidbody r = ball.GetComponent<Rigidbody>();
            r.AddForce(rightHand.transform.forward * throwForce);
        }

        if (Input.GetKeyDown(KeyCode.Q) && objInHand == null)
        {
            GameObject bomb = Instantiate(bombObject, rightHand.transform.position, Quaternion.identity);
            Rigidbody r = bomb.GetComponent<Rigidbody>();
            r.AddForce(rightHand.transform.forward * throwForce * 1.05f);
        }

        // Grab something only if gun is disabled
        if (Input.GetKeyDown(KeyCode.Mouse1) && !gun.activeSelf)
        {
            Ray ray = new Ray(camera.transform.position, camera.transform.forward);
            Debug.DrawLine(ray.origin, ray.GetPoint(maxDist));

            if (objInHand == null)
            {
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, maxDist, interactionLayer))
                {
                    if (hit.transform.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        objInHand = hit.transform.gameObject;
                        objInHand.transform.position = rightHand.transform.position;

                        objInHand.GetComponent<Rigidbody>().isKinematic = true;
                        objInHand.transform.parent = rightHand.transform;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse2) && objInHand == null)
        {
            if (!gun.activeSelf)
            {
                gun.SetActive(true);

            } else if (gun.activeSelf)
            {
                gun.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && objInHand != null)
        {
            objInHand.transform.parent = null;
            objInHand.GetComponent<Rigidbody>().isKinematic = false;
            objInHand.GetComponent<Rigidbody>().AddForce(camera.transform.forward * throwForce);
            objInHand = null;
        }
    }
}
