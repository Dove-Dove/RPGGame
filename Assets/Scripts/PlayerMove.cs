using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;

    //캐릭터 컨트롤러
    CharacterController cc;

    float playerSpeed = 10f;

    //y축 속력
    float y_velocity;
    //중력
    float gravity = -9.8f;

    //점프 
    bool jump = false;

    //마우스 속도 
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

        // 이동 방향을 결정하는 백터
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        dir = Camera.main.transform.TransformDirection(dir);

        //캐릭터 Y축 속도에 맞춰 중력 수치를 적용한다.
        y_velocity += gravity * Time.deltaTime;
        dir.y = y_velocity;

        //마우스 움직임 관련 
        float mouse_X = Input.GetAxis("Mouse X");

        mx += mouse_X * rotSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, mx, 0);

        //최종 움직임
        cc.Move(dir * playerSpeed * Time.deltaTime);
    }
}
