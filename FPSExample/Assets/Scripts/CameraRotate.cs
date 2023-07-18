using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자의 입력에 따라 X와 Y축의 회전을 하고 싶다.
// Zoom 처리를 하고 싶다.
// Zoom 에 따라 총의 위치를 다르게 하고 싶다.
public class CameraRotate : MonoBehaviour
{
    float rx;
    float ry;
    public float rotSpeed = 200;
    // Start is called before the first frame update
    void Start()
    {
        float targetFOV = Camera.main.fieldOfView;
        gunTargetLocalPosition = zoomOutPosition.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        //1. 사용자의 입력에 따라
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");        
        //2. X와 Y추그이 값을 누적하고
        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;
        //3. rx에 대해 각도를 제한하고 싶다.
        rx = Mathf.Clamp(rx, -75, 75);
        //4. 그 누적값으로 회전을 하고 싶다.
        transform.eulerAngles = new Vector3(-rx, ry, 0);

        UpdateZoom();
    }

    public float zommInFOV = 15;
    public float zommOutFOV = 60;
    float targetFOV;
    // targetFOV 변수를 만들고 카메라의 FOV가 targetFOV로 수렴하고 싶다.
    // zoom에 따라 총으 ㅣ위치를 변경하고 싶다.
    public Transform zoomInPosition;
    public Transform zoomOutPosition;
    public Transform gun;
    Vector3 gunTargetLocalPosition;
    float zoomSpeed = 5;

    private void UpdateZoom()
    {

        // 마우스 오른쪽 버튼을 누르면 ZoomIn
        if(Input.GetButtonDown("Fire2"))
        {
            targetFOV = zommInFOV;
            Camera.main.fieldOfView = zommInFOV;
            gunTargetLocalPosition = zoomInPosition.localPosition;
            zoomSpeed = 20;
        }
        // 마우스 오른쪽 버튼을 때면 ZoomOut
        else if(Input.GetButtonUp("Fire2"))
        {
            targetFOV = zommOutFOV;
            gunTargetLocalPosition = zoomOutPosition.localPosition;
            zoomSpeed = 5;
        }

        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFOV, Time.deltaTime * 5);
        gun.localPosition = Vector3.Lerp(gun.localPosition, gunTargetLocalPosition,Time.deltaTime * zoomSpeed);
    }
}
