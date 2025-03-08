using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float rotSpeed = 200.0f;

    //회전 값
    float mx = 0;
    float my = 0;



    void Update()
    {
        transform.position = target.position;

        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_y = Input.GetAxis("Mouse Y");

        //회전값을 누적 시킨다.
        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_y * rotSpeed * Time.deltaTime;

        //상하 회전값을 미리 제한을 둔다
        my = Mathf.Clamp(my, -90.0f, 90.0f);

        //방향이 결정된 대로 움직인다
        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}
