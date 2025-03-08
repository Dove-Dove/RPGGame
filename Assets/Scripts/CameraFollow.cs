using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float rotSpeed = 200.0f;

    //ȸ�� ��
    float mx = 0;
    float my = 0;



    void Update()
    {
        transform.position = target.position;

        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_y = Input.GetAxis("Mouse Y");

        //ȸ������ ���� ��Ų��.
        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_y * rotSpeed * Time.deltaTime;

        //���� ȸ������ �̸� ������ �д�
        my = Mathf.Clamp(my, -90.0f, 90.0f);

        //������ ������ ��� �����δ�
        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}
