using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자의 입력에 따라서 앞뒤좌우로 이동하고 싶다.
// 사용자가 점프버튼을 누르면 점프를 뛰고 싶다.
public class PlayerMove : MonoBehaviour
{
    public float jumpPower = 2;
    public float gravity = -9.81f;
    float yVelocity;
    public float speed = 5;

    int jumpCount = 0;
    public int maxJumpCount = 2;

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
        // 중력의 힘이 작용해야한다.
        yVelocity += gravity * Time.deltaTime;

        // 땅에 닿았다면 점프카운트를 초기화하고 싶다.
        if (cc.isGrounded)
        {
            jumpCount = 0;
            // 땅에 서있을 때는 y속도가 변화하지 않게 하고 싶다.
            yVelocity = 0;
        }

        // 만약 점프카운트가 최대 보다 작다 그리고 그리고 사용자가 점프버튼을 누르면
        if (jumpCount < maxJumpCount && Input.GetButtonDown("Jump"))
        {
            // JumpPower가 작용해야한다.
            yVelocity = jumpPower;
            jumpCount++;
        }
        

        // 1. 사용자의 입력에 따라
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        // 2. 방향을 만들고
        Vector3 dir = new Vector3(h, 0, v);
        
        // 3. 현재 방향을 카메라의 앞방향을 기준으로 변환하고 싶다.
        dir = cam.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();
        // 결정된 y속도를 dir의 y항목에 반영되어야한다.
        Vector3 velocity = dir * speed;
        velocity.y = yVelocity;
        // 4. 그 방향으로 이동하고 싶다.
        //transform.position += velocity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {    
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position+Vector3.up*yVelocity);
    }
}
