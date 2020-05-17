using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage;
    public float range;

    public Camera rightHand;
    public GameObject impactEffect;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(rightHand.transform.position, rightHand.transform.forward, out hit, range, 1 << LayerMask.NameToLayer("InteractionLayer")))
        {
            Debug.DrawLine(rightHand.transform.forward, hit.transform.position, Color.white);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                Debug.Log(target.name + " at " + target.transform.position);
                target.TakeDamage(damage);
            }

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);
        }
    }
}
