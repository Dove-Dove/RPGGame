using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;

    //ĳ���� ��Ʈ�ѷ�
    CharacterController cc;

    float playerSpeed = 10f;

    //y�� �ӷ�
    float y_velocity;
    //�߷�
    float gravity = -9.8f;

    //���� 
    bool jump = false;

    //���콺 �ӵ� 
    public float rotSpeed = 200.0f;

    float mx = 0.0f;

    void Start()
    {
        cc = GetComponent<CharacterController>();

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // �̵� ������ �����ϴ� ����
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        dir = Camera.main.transform.TransformDirection(dir);

        //ĳ���� Y�� �ӵ��� ���� �߷� ��ġ�� �����Ѵ�.
        y_velocity += gravity * Time.deltaTime;
        dir.y = y_velocity;

        //���콺 ������ ���� 
        float mouse_X = Input.GetAxis("Mouse X");

        mx += mouse_X * rotSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, mx, 0);

        //���� ������
        cc.Move(dir * playerSpeed * Time.deltaTime);
    }
}
