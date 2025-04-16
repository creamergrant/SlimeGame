using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public Rigidbody rb;
    public GameObject pt;

    public float bulletSpeed = 5;

    void Start()
    {
        pt = GameObject.Find("gunTip");
        rb.AddForce(pt.transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        Destroy(gameObject, 4f);
    }

}
