using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������� �Է¿� ���� �յ��¿�� �̵��ϰ� �ʹ�.
// ����ڰ� ������ư�� ������ ������ �ٰ� �ʹ�.
public class PlayerMove : MonoBehaviour
{
    public float jumpPower = 2;
    public float gravity = -9.81f;
    float yVelocity;
    public float speed = 5;

    CharacterController cc;

    Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        cc = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // �߷��� ���� �ۿ��ؾ��Ѵ�.
        yVelocity += gravity * Time.deltaTime;

        // ���� ���� ���ִ� �׸��� ����ڰ� ������ư�� ������
        if (cc.isGrounded&&Input.GetButtonDown("Jump"))
        {
            // JumpPower�� �ۿ��ؾ��Ѵ�.
            yVelocity = jumpPower;
        }

        // 1. ������� �Է¿� ����
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        // 2. ������ �����
        Vector3 dir = new Vector3(h, 0, v);
        
        // 3. ���� ������ ī�޶��� �չ����� �������� ��ȯ�ϰ� �ʹ�.
        dir = cam.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();
        // ������ y�ӵ��� dir�� y�׸� �ݿ��Ǿ���Ѵ�.
        Vector3 velocity = dir * speed;
        velocity.y = yVelocity;
        // 4. �� �������� �̵��ϰ� �ʹ�.
        //transform.position += velocity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position+Vector3.up*yVelocity);
    }
}
