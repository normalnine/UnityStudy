using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자가 마우스 왼쪽 버튼을 누르면
// 총알공장에서 총알을 만들고
// 그 총알을 총구위치에 배치하고 싶다.
public class PlayerFire : MonoBehaviour
{
    enum BImpactName
    {
        Floor,
        Enemy
    }

    public GameObject bulletFactory;
    public Transform firePosition;
    // 총알자국공장
    public GameObject[] bImpactFactorys;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateFire();
        //UpadateGrenade();        
    }

    private void UpdateFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // 1. 카메라에서 카메라의 앞방향으로 시선을 만들고
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hitInfo;
            // 2. 바라보고 싶다.
            if (Physics.Raycast(ray, out hitInfo))
            {
                // 3. 시선이 닿은 곳에 총알 자국 공장에서 총알 자국을 만들어섭 배치하고 싶다.
                GameObject bulletImpact = Instantiate(bImpactFactorys[(int)BImpactName.Floor]);
                bulletImpact.transform.position = hitInfo.point;
                // 방향을 회전하고 싶다. 튀는 방향(forward)을 부딪힌 면의 Normal방향으로
                bulletImpact.transform.forward = hitInfo.normal;
            }
            else
            {
                // 허공
            }
        }
       
    }

    private void UpadateGrenade()
    {
        // 만약 사용자가 마우스 왼쪽 버튼을 누르면 폭탄을 던지고 싶다.
        if (Input.GetKeyDown(KeyCode.G))
        {
            // 총알공장에서 총알을 만들고
            GameObject bullet = Instantiate(bulletFactory);
            // 그 총알을 총구위치에 배치하고 싶다.
            bullet.transform.position = firePosition.position;
            bullet.transform.forward = firePosition.forward;

            // 총알의 speed를 바꾸고 싶다.
            Bullet bulletComp = bullet.GetComponent<Bullet>();
            bulletComp.speed = 20;
        }
    }
}
