using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 태어날 때 앞방향으로 물리기반의 이동하고 싶다.
public class Bullet : MonoBehaviour
{
    public float speed = 5;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = rb.velocity.normalized;
    }
}
