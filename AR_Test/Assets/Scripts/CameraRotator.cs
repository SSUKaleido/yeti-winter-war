using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField]
    public Camera mainCam;

    public float rotateSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        float vertical = Input.GetAxis("Mouse Y");
        float horizontal = Input.GetAxis("Mouse X");

        mainCam.transform.Rotate(new Vector3(-vertical, horizontal, 0), rotateSpeed);
    }
}

/*
 * public Vector2 MouseDeltaPos;
 * 
 * if( MouseDeltaPos == null)
 * 
 * 
 * 1. 현재 마우스 위치를 가져온다.
 * Vector2 curMousePos = (Vector2) Input.mousePosition;
 * 2. 현재 마우스 위치와 이전 프레임 마우스 위치를 비교하고 방향을 정한다.
 * Vector2 dir = (MouseDeltaPos - curMousePos).normalized;
 * 3. 방향과 도는 속도를 곱해준다.
 * dir *= RoatationSpeed;
 * 4. 곱한 값만큼 카메라를 돌려준다.
 * Vector3 targetVector = new Vector3(dir.y, dir.x, 0);
 * transform.Rotate();
 * 5. 이전 프레임 마우스 위치에 현재 마우스 위치 값을 넣어준다.
 */