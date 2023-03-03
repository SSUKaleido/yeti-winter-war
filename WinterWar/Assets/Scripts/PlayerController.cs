using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb; // 플레이어 Rigidbody, 플레이어의 충돌 영역에 물리를 입힘. Collider는 충돌영역 설정.

    // 속도 변수
    [SerializeField]     // Inspector 창에서 private 확인
    private float walkSpeed;
    [SerializeField]
    private float crouchSpeed;  // 앉았을 때 속도
    [SerializeField]
    private float boostSpeed;   // 부스터 사용했을 때 속도
    private float applySpeed;   // 실제 적용 속도

    [SerializeField]
    private float jumpForce;

    // 상태 변수
    private bool isGround = true;  // 땅에 있는지 없는지
    private bool isCrouch = false;

    private bool isRun = false;   // 걷고 있는지


    // 앉았을 때 얼마나 앉을지 변수
    [SerializeField]
    private float crouchPosY;   // 앉았을 때 Y
    private float originPosY;   // 기존 Y
    private float applyCrouchPosY;  // 종합 Y

    // 땅 착지 여부 확인;
    private BoxCollider boxCollider;

    /*
     * 아이템 관련 변수 자리
    */


    // 카메라 민감도
    [SerializeField]
    private float lookSensitivity;

    [SerializeField] 
    private float cameraRotationLimit;  // 카메라 범위 조절
    private float currentCameraRotationX; // 카메라 정면

    [SerializeField]
    private Camera mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        boxCollider= GetComponent<BoxCollider>();
        originPosY = mainCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");

        IsGround();
        TryJump();
        TryCrouch();
        Move();
        CameraRotation();
        CharacterRotation();
    }

    // 앉기 시도
    private void TryCrouch()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }

    // 앉기
    private void Crouch()
    {
        isCrouch = !isCrouch;
        if(isCrouch)
        {
            applySpeed = crouchSpeed;
            
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }

        //mainCamera.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x, applyCrouchPosY, mainCamera.transform.localPosition.z); 에서 변경함.
        StartCoroutine(CrouchCoroutine());
    }

    IEnumerator CrouchCoroutine()   // 앉기 시 카메라 : coroutine을 만나면 코드가 병렬 처리됨. 자세 변화 시 자연스러운 카메라 처리
    {
        float posY = mainCamera.transform.localPosition.y;
        int count = 0;

        while(posY != applyCrouchPosY)
        {
            count++;    //확실하게 0.5도달 => 다시 알아보기
            posY = Mathf.Lerp(posY, applyCrouchPosY, 0.3f); // x에서 y까지 z의 비율로 증가함
            mainCamera.transform.localPosition = new Vector3(0, posY, 0);
            if (count > 15) break;
            yield return null; // 한 프레임 대기
        }

        mainCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0f);
    }

    private void Run()   // 아이템 사용시 달리기
    {
        // 키입력 등등
    }

    // 땅에 붙어있는지 확인
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, boxCollider.bounds.extents.y + 0.1f);   // Vector : 뒤집어져도 아래로, boxCollider 영역 y의 절반 만큼 확인
    }

    // 점프
    private void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            if (isCrouch) Crouch(); // 점프할 때 앉기 해제
            playerRb.velocity = transform.up * jumpForce;
        }
    }


    // w, s, a, d 이동
    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveHorizontal = transform.right* moveX;
        Vector3 moveVertical= transform.forward* moveZ;

        if (!isRun) applySpeed = walkSpeed;
        else applySpeed = boostSpeed;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * applySpeed;  // 벡터의 합이 1이 나오도록 함.

        playerRb.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    // 좌우 캐릭터 회전(camera)
    private void CharacterRotation()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");  // 마우스 좌우
        Vector3 charaterRotationY = new Vector3(0f, yRotation, 0f) * lookSensitivity;
        playerRb.MoveRotation(playerRb.rotation * Quaternion.Euler(charaterRotationY));
    }


    // 상하 카메라 회전
    private void CameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");  // 마우스 위아래
        float cameraRotationX = xRotation * lookSensitivity; // 카메라가 너무 확 움직이지 않도록 조절
        
        currentCameraRotationX -= cameraRotationX;
        
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        mainCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);

    }
    
}
