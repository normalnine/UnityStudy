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

    public GameObject grenadeFactory;
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
        UpadateGrenade();        
    }

    private void UpdateFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // 1. 카메라에서 카메라의 앞방향으로 시선을 만들고
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            //int layer = (1 << LayerMask.NameToLayer("Default"));
            RaycastHit hitInfo;
            // 2. 바라보고 싶다.
            if (Physics.Raycast(ray, out hitInfo, 10, int.MaxValue))
            {
                bool isEnemy = false;
                // 3. 시선이 닿은 곳에 총알 자국 공장에서 총알 자국을 만들어섭 배치하고 싶다.
                BImpactName biName = BImpactName.Floor;
                print(hitInfo.transform.gameObject.layer);
                // 만약 hitInfo의 물체의 레이어가 Enemy라면
                if(hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    biName = BImpactName.Enemy;
                    isEnemy = true;
                }
           
                GameObject bulletImpact = Instantiate(bImpactFactorys[(int)biName]);
                bulletImpact.transform.position = hitInfo.point;
                // 방향을 회전하고 싶다. 튀는 방향(forward)을 부딪힌 면의 Normal방향으로
                bulletImpact.transform.forward = hitInfo.normal;

                // 만약 총에 맞은 것이 적이라면
                if(isEnemy)
                {
                    // 적(Enemy2)에게 너 총에 맞았어!(DamageProcess()) 라고 알려주고 싶다.
                    Enemy2 enemy = hitInfo.transform.GetComponent<Enemy2>();
                    enemy.DamageProcess();

                }
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
            GameObject g = Instantiate(grenadeFactory);
            g.transform.position = firePosition.position;
            g.transform.forward = firePosition.forward;

            g.GetComponent<Grenade>().speed = 10;
        }
    }
}
