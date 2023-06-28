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

    private void OnCollisionEnter(Collision collision)
    {
        // 만약 부딪힌물체에 Rigidbody가 있다면
        var otherRB = collision.gameObject.GetComponent<Rigidbody>();

        // 내 앞방향으로 힘을 10 가하고 싶다.
        if(otherRB != null)
        {
            otherRB.AddForce(transform.forward * 2000, ForceMode.Impulse);
        }
        Destroy(gameObject);

    }
}
