using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{//�÷��̾��� ������ �ӵ��� �����ϴ� ����
    [Header("Player Movement")]
    public float moveSpeed = 5.0f;              //�̵� �ӵ�
    public float jumpForce = 5.0f;              //���� ��

    //ī�޶� ���� ����
    [Header("Camera Settings")]
    public Camera firstPersonCamera;            //1��Ī ī�޶�
    public Camera thirdPersonCamera;            //3��Ī ī�޶�
    public float mouseSensitivity = 2.0f;       //���콺 ����

    public float radius = 5.0f;                 //3��Ī ī�޶�� �÷��̾� ���� �Ÿ�
    public float minRadius = 1.0f;              //ī�޶� �ּ� �Ÿ�
    public float maxRadius = 10.0f;             //ī�޶� �ִ� �Ÿ�

    public float yMinLimit = -90;               //ī�޶� ���� ȸ�� �ּҰ�
    public float yMaxLimit = 90;                //ī�޶� ���� ȸ�� �ִ밢

    private float theta = 0.0f;                 //ī�޶��� ���� ȸ�� ����
    private float phi = 0.0f;                   //ī�޶��� ���� ȸ�� ����
    private float targetVecticalRotation = 0;   //��ǥ ���� ȸ�� ����
    private float verticalRotationSpeed = 240f; //���� ȸ�� ����

    //���� ������
    private bool isFirstPerson = true;          //1��Ī ��� ���� ����     
    private bool isGrounded;                    //�÷��̾ ���� �ִ��� ����
    private Rigidbody rb;                       //�÷��̾��� Rigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        SetupCameras();
        SetActiveCamera();
    }

    void Update()
    {
        HandelMovement();
        HandleRotation();
        HandleJump();
        GandelCameraToggle();
    }

    //Ȱ��ȭ�� ī�޶� �����ϴ� �Լ�
    void SetActiveCamera()
    {
        firstPersonCamera.gameObject.SetActive(isFirstPerson);      //1��Ī ī�޶� Ȱ��ȭ ����
        thirdPersonCamera.gameObject.SetActive(!isFirstPerson);     //3��Ī ī�޶� Ȱ��ȭ ����
    }

    //ī�޶� �ʱ� ��ġ �� ȸ���� �����ϴ� �Լ�
    void SetupCameras()
    {
        firstPersonCamera.transform.localPosition = new Vector3(0.0f, 0.6f, 0.0f);          //1��Ī ī�޶� ��ġ
        firstPersonCamera.transform.localRotation = Quaternion.identity;                    //1��Ī ī�޶� ȸ�� �ʱ�ȭ
    }

    //ī�޶� �� ĳ���� ȸ�� ó���ϴ� �Լ�
    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;         //���콺 �¿� �Է�
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;         //���콺 ���� �Է�

        //���� ȸ��(theta ��)
        theta += mouseX;                            //���콺 �Է°� �߰�
        theta = Mathf.Repeat(theta, 360.0f);        //���� ���� 360�� ���� �ʵ��� ����

        //���� ȸ�� ó��
        targetVecticalRotation -= mouseY;
        targetVecticalRotation = Mathf.Clamp(targetVecticalRotation, yMinLimit, yMaxLimit);     //���� ȸ�� ����
        phi = Mathf.MoveTowards(phi, targetVecticalRotation, verticalRotationSpeed * Time.deltaTime);

        //�÷��̾� ȸ��(ĳ���Ͱ� �������θ� ȸ��)
        transform.rotation = Quaternion.Euler(0.0f, theta, 0.0f);

        if (isFirstPerson)
        {
            firstPersonCamera.transform.localRotation = Quaternion.Euler(phi, 0.0f, 0.0f);       //1��Ī ī�޶� ���� ȸ��
        }
        else
        {
            //3��Ī ī�޶� ���� ��ǥ�迡�� ��ġ �� ȸ�� ���
            float x = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Cos(Mathf.Deg2Rad * theta);
            float y = radius * Mathf.Cos(Mathf.Deg2Rad * phi);
            float z = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Sin(Mathf.Deg2Rad * theta);

            thirdPersonCamera.transform.position = transform.position + new Vector3(x, y, z);
            thirdPersonCamera.transform.LookAt(transform);      //ī�޶� �׻� �÷��̾ �ٶ󺸵��� ����

            //���콺 ��ũ���� ����Ͽ� ī�޶� �� ����
            radius = Mathf.Clamp(radius - Input.GetAxis("Mouse ScrollWheel") * 5, minRadius, maxRadius);
        }
    }

    //1��Ī�� 3��Ī ī�޶� ��ȯ�ϴ� �Լ�
    void GandelCameraToggle()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isFirstPerson = !isFirstPerson;     //ī�޶� ��� ��ȯ
            SetActiveCamera();
        }
    }

    //�÷��̾� ������ ó���ϴ� �Լ�
    void HandleJump()
    {
        //���� ��ư�� ������ ���� ������
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);     //�������� ���� ���� ���� (ForceMode.Impulse ���������� ���� ���Ҷ�)
            isGrounded = false;
        }
    }

    //�÷��̾��� �̵��� ó���ϴ� �Լ�
    void HandelMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");             //�¿� �Է�(-1 ~ 1)
        float moveVertical = Input.GetAxis("Vertical");                 //�յ� �Է�(1 ~ -1)


        if (!isFirstPerson)      //3��Ī ����϶�, ī�޶� �������� �̵� ó��
        {
            Vector3 cameraForward = thirdPersonCamera.transform.forward;    //ī�޶� �� ����
            cameraForward.y = 0.0f;
            cameraForward.Normalize();      //���� ���� ����ȭ (0~1) ���� ������ ������ش�.

            Vector3 cameraRight = thirdPersonCamera.transform.right;        //ī�޶� ������ ����
            cameraRight.y = 0.0f;
            cameraRight.Normalize();

            //ĳ���� �������� �̵�
            Vector3 movement = cameraRight * moveHorizontal + cameraForward * moveVertical;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);                           //���� ��� �̵�
        }
        else
        {
            //ĳ���� �������� �̵�
            Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);                           //���� ��� �̵�
        }
    }

    //�÷��̾ ���� ��� �ִ��� ����
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;              //�浹 ���̸� �÷��̾�� ���� �ִ�.
    }
}