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
 * 1. ���� ���콺 ��ġ�� �����´�.
 * Vector2 curMousePos = (Vector2) Input.mousePosition;
 * 2. ���� ���콺 ��ġ�� ���� ������ ���콺 ��ġ�� ���ϰ� ������ ���Ѵ�.
 * Vector2 dir = (MouseDeltaPos - curMousePos).normalized;
 * 3. ����� ���� �ӵ��� �����ش�.
 * dir *= RoatationSpeed;
 * 4. ���� ����ŭ ī�޶� �����ش�.
 * Vector3 targetVector = new Vector3(dir.y, dir.x, 0);
 * transform.Rotate();
 * 5. ���� ������ ���콺 ��ġ�� ���� ���콺 ��ġ ���� �־��ش�.
 */