using UnityEngine;

public class DistractionBeanController : MonoBehaviour
{
    private Rigidbody myBody;
    public float lifeTimer = 20f;
    private float timer;

    void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTimer)
        {
            Destroy(gameObject);
        }
    }
}
