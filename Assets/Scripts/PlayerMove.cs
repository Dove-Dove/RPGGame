using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;

    //캐릭터 컨트롤러
    CharacterController cc;

    float playerSpeed = 4f;

    //y축 속력
    float y_velocity;
    //중력
    float gravity = -9.8f;
    //점프 
    bool jump = false;
    //앉기 
    bool sit = false;
    //마우스 속도 
    public float rotSpeed = 200.0f;
    //애니메이터
    Animator anim;

    //


    float mx = 0.0f;

    void Start()
    {
        cc = GetComponent<CharacterController>();

        rb = GetComponent<Rigidbody>();

        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        // 이동 방향을 결정하는 백터
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        dir = Camera.main.transform.TransformDirection(dir);


        anim.SetFloat("MoveMotion", dir.magnitude);

        if (Input.GetKey(KeyCode.C))
            sit = true;
        else
            sit = false;

        if (Input.GetKey(KeyCode.Space))
        {
            float Distance = 0.85f;
            RaycastHit hit;
            Vector3 playerPos = transform.position;
            playerPos.y -= 0.3f;
            Debug.DrawRay(playerPos, transform.forward * Distance, Color.yellow);

            if (Physics.Raycast(playerPos, transform.forward, out hit, Distance))
            {
                // 닿은 물체의 이름을 출력
                //Debug.Log(hit.collider.gameObject.name);
                if(hit.collider.gameObject.tag == "Well")
                {
                    float moveAction = 0;
                    //충돌한 오브젝트와 플레이어의 높이 차이를 확인
                    float objectHeight = hit.collider.gameObject.transform.position.y - transform.position.y + 0.5f;
                    //anim.SetTrigger("JumpWell");

                    //애니메이션 시작

                    if(objectHeight < 0.15f)
                    {
                        anim.CrossFade("Jumping Over Into Combat", 0.2f);
                        moveAction = hit.transform.localScale.z + 0.6f;
                    }
                    else
                    {
                        anim.CrossFade("slideAction", 0.2f);
                        moveAction = hit.transform.localScale.z + 0.6f;
                    }


                    StartCoroutine(pAction(1.1f, moveAction, hit.collider.transform));
                }
            }

        }


        if (sit)
        {
            anim.SetBool("Sit", sit);
            
        }
        else
        {
            anim.SetBool("Sit", sit);
            if(Input.GetKey(KeyCode.S))
            {
                anim.SetFloat("MoveMotion", -1);
                playerSpeed = 2.0f;
            }

            else if(Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetFloat("MoveMotion", 2);
                playerSpeed = 7.0f;
            }
            else
            {         
                playerSpeed = 4.0f;
            }    

        }


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

    IEnumerator pAction(float actionTime, float move, Transform hitObject)
    {
        rb.isKinematic = true;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition;
        float elapsedTime = 0f;


        // 플레이어와 충돌한 물체 간의 방향 벡터 계산
        Vector3 directionToPlayer = transform.position - hitObject.position;
        Vector3 objectForward = hitObject.forward; 

        float dotProduct = Vector3.Dot(objectForward, directionToPlayer);
        bool isBehind = dotProduct > 0;

        

        // 이동 방향 설정
        if (isBehind)
        {
            targetPosition -= -transform.forward  * move;
            print(isBehind);
        }     
        else
        {
            targetPosition += transform.forward * move;
            print(isBehind);
        }




        while (elapsedTime < actionTime)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / actionTime);
            elapsedTime += Time.deltaTime;
            //print(isBehind);
            if (isBehind)
                transform.rotation = Quaternion.Euler(transform.rotation.x, hitObject.eulerAngles.y - 180f, transform.rotation.z);
            else
                transform.rotation = Quaternion.Euler(transform.rotation.x, hitObject.eulerAngles.y, transform.rotation.z);
            yield return null;
        }

        transform.position = targetPosition;

        rb.isKinematic = false;

    }

}
