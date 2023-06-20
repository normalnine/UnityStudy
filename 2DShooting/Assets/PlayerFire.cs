using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자가 마우스 왼쪽 버튼을 누르면
// 총알공장에서 총알을 만들어서
// 총구위치에 배치하고 싶다.
// 마우스 왼쪽 버튼을 누르고 있으면 0.2초마다 총아링 계속 발사되게 하고싶다.
// 버튼을 때면 그만 발사하게 하고싶다.

// ObejectPool
// 태어날 때 총알을 미리 만들어서 총알 목록에 등록하고 비활성화 해놓고
// 총알이 발사될 때 목록에서 하나 가져와서 활성화하고 싶다.
// 총알이 화면 밖에 나가거나 적과 부딪혔을 때 비활성화하고 싶다.
// 비활성화 목록을 만들어서 비활성화된 총알을 검색하는 일을 하지않고 싶다.
public class PlayerFire : MonoBehaviour
{
    List<GameObject> bulletObjectPool;
    int bulletObjectPoolCount = 5;
    public static List<GameObject> deActivBulletObjectPool;
    public Transform bulletParent;

    public List<GameObject> DeActivBulletObjectPool
    {
        get { return deActivBulletObjectPool; }
    }
    public GameObject bulletFactory;
    public Transform firePosition;
    public float fireTime = 0.2f;
    public float currTime = 0;
    bool bAutoFire = false;

    // Start is called before the first frame update
    void Start()
    {
        bulletObjectPool = new List<GameObject>();
        deActivBulletObjectPool = new List<GameObject>();
        // 태어날 때 총알을 미리 만들어서 총알 목록에 등록하고 비활성화 해놓고
        for (int i=0; i<bulletObjectPoolCount; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);
            // 나의 부모 = 너
            bullet.transform.parent = bulletParent;
            bullet.SetActive(false);
            bulletObjectPool.Add(bullet);
            deActivBulletObjectPool.Add(bullet);
        }        
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
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            bAutoFire = false;
        }            
    }
    
    void MakeBullet()
    {
        // 총알이 발사될 때 목록에서 비활성화된 총알을 하나 가져와서 활성화하고 싶다.
        GameObject bullet = GetBulletFromObjectPool();
        if(bullet)
        {
            // 총구위치에 배치하고 싶다.
            bullet.transform.position = firePosition.position;
            bullet.transform.rotation = firePosition.rotation;
        }        
    }

    GameObject GetBulletFromObjectPool()
    {
        // 만약의 비활성 목록의 크기가 0보다 크다면
        if(DeActivBulletObjectPool.Count > 0)
        {
            // 비활성 목록의 0번째 항목을 반환하고 싶다.
            GameObject bullet = DeActivBulletObjectPool[0];
            bullet.SetActive(true);
            // 목록에서 bullet 지우고 싶다.
            DeActivBulletObjectPool.Remove(bullet);

            return bullet;
        }
        // 그렇지 않다면 null 을 반환하고 싶다.
        return null;

        //// 총알이 발사될 때 목록에서 비활성화된 총알을 하나 가져와서 활성화하고 반환하고 싶다.
        //for(int i = 0; i<bulletObjectPoolCount; i++)
        //{

        //    if (bulletObjectPool[i].activeSelf == false)
        //    {
        //        // 활성화
        //        bulletObjectPool[i].SetActive(true);
        //        // 반환
        //        return bulletObjectPool[i];
        //    }
        //}
    }
}
