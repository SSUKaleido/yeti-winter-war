using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb; // �÷��̾� Rigidbody, �÷��̾��� �浹 ������ ������ ����. Collider�� �浹���� ����.

    // �ӵ� ����
    [SerializeField]     // Inspector â���� private Ȯ��
    private float walkSpeed;
    private float crouchSpeed;  // �ɾ��� �� �ӵ�

    [SerializeField]
    private float jumpForce;

    // ���� ����
    private bool isGround = true;  // ���� �ִ��� ������
    private bool isCrouch = false;

    // �ɾ��� �� �󸶳� ������ ����
    [SerializeField]
    private float crouchPosY;   // �ɾ��� �� Y
    private float originPosY;   // ���� Y
    private float applyCrouchPosY;  // ���� Y

    // �� ���� ���� Ȯ��;
    private BoxCollider boxCollider;

    // �ΰ���
    [SerializeField]
    private float lookSensitivity;

    [SerializeField] 
    private float cameraRotationLimit;  // ī�޶� ���� ����
    private float currentCameraRotationX; // ī�޶� ����

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

    private void TryCrouch()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }

    private void Crouch()   // �ӵ� ���� ���� ����
    {
        isCrouch = !isCrouch;
        if(isCrouch)
        {
            // applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            // applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }

        //mainCamera.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x, applyCrouchPosY, mainCamera.transform.localPosition.z);
        StartCoroutine(CrouchCoroutine());
    }

    IEnumerator CrouchCoroutine()   // coroutine�� ������ �ڵ尡 ���� ó����. �ڿ������� ī�޶� ó��
    {
        float posY = mainCamera.transform.localPosition.y;
        int count = 0;

        while(posY != applyCrouchPosY)
        {
            count++;    //Ȯ���ϰ� 0.5���� => �ٽ� �˾ƺ���
            posY = Mathf.Lerp(posY, applyCrouchPosY, 0.3f); // x���� y���� z�� ������ ������
            mainCamera.transform.localPosition = new Vector3(0, posY, 0);
            if (count > 15) break;
            yield return null; // �� ������ ���
        }

        mainCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0f);
    }

    // ���� �پ��ִ��� Ȯ��
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, boxCollider.bounds.extents.y + 0.1f);   // Vector : ���������� �Ʒ���, boxCollider ���� y�� ���� ��ŭ Ȯ��
    }

    // ���� �õ�
    private void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if(isCrouch) Crouch(); // ������ �� �ɱ� ����
        playerRb.velocity = transform.up * jumpForce;
    }

    // w, s, a, d �̵�
    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveHorizontal = transform.right* moveX;
        Vector3 moveVertical= transform.forward* moveZ;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * walkSpeed;  // ������ ���� 1�� �������� ��.

        playerRb.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    // �¿� ĳ���� ȸ��
    private void CharacterRotation()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");  // ���콺 �¿�
        Vector3 charaterRotationY = new Vector3(0f, yRotation, 0f) * lookSensitivity;
        playerRb.MoveRotation(playerRb.rotation * Quaternion.Euler(charaterRotationY));
    }


    // ���� ī�޶� ȸ��
    private void CameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");  // ���콺 ���Ʒ�
        float cameraRotationX = xRotation * lookSensitivity; // ī�޶� �ʹ� Ȯ �������� �ʵ��� ����

        currentCameraRotationX -= cameraRotationX;
        
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        mainCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);

    }
    
}
