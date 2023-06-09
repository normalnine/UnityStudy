using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자가 마우스 왼쪽 버튼을 누르면
// 총알공장에서 총알을 만들어서
// 총구위치에 배치하고 싶다.
// 마우스 왼쪽 버튼을 누르고 있으면 0.2초마다 총아링 계속 발사되게 하고싶다.
// 버튼을 때면 그만 발사하게 하고싶다.
public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory;
    public Transform firePosition;
    public float fireTime = 0.2f;
    public float currTime = 0;
    bool bAutoFire = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(bAutoFire)
        {
            currTime += Time.deltaTime;
            if (currTime > fireTime)
            {
                MakeBullet();
                currTime = 0;
            }
        }

        // 사용자가 마우스 왼쪽 버튼을 누르면
        if (Input.GetButtonDown("Fire1"))
        {
            bAutoFire = true;

            // 누르는 순간에도 발사
            MakeBullet();
            currTime = 0;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            bAutoFire = false;
        }            
    }
    
    void MakeBullet()
    {       
        // 총알공장에서 총알을 만들어서
        GameObject bullet = Instantiate(bulletFactory);
        // 총구위치에 배치하고 싶다.
        bullet.transform.position = firePosition.position;
        bullet.transform.rotation = firePosition.rotation;
    }
}
