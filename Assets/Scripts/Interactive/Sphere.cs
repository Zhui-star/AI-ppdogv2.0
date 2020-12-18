using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {
    private Rigidbody rigidbody;
    private bool startMove = false;
    private Transform player;
    public float speed = 8;
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }
	// Update is called once per frame
	void Update () {
        this.GetComponent<Rigidbody>().AddForce(transform.forward*20);

    }
    void OnCollisionEnter(Collision collison)
    {
        if (collison.collider.tag == Tags.ground)
        {
            this.rigidbody.useGravity = false;
            this.rigidbody.isKinematic = true;
            SphereCollider col = this.GetComponent<SphereCollider>();//掉落到地面上即便成触发器
            col.isTrigger = true;
            col.radius = 2;
        }

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == Tags.player)
        {
            startMove = true;
            player = col.transform;
        }
    }
}
   
