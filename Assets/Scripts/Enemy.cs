using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Move,
        Stop,
        Attack,
        Hit,
        Death
    }

    float MoveSpeed;

    EnemyState state;
    Vector3 PlayerPos =Vector3.zero; 

    //플레이어 탐지
    public bool FindPlayer = false;
   

    //캐릭터 컨트롤러 컴포먼트
    CharacterController cc;


    // Start is called before the first frame update
    void Start()
    {
        state = EnemyState.Idle;
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer = GetComponent<EnemyFind>().isCollision;

        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;

            case EnemyState.Move:
                Move();
                break;

            case EnemyState.Stop:
                Stop();
                break;

            case EnemyState.Attack:
                attack();
                break;

        }

    }

    void Idle()
    {
        if (FindPlayer)
            state = EnemyState.Move;
    }

    void Move()
    {
        if (FindPlayer)
        {
            MoveSpeed = 3.0f;
            Vector3 dir = (PlayerPos - transform.position).normalized;


            //컨트롤러 사용한 이동
            cc.Move(dir * MoveSpeed * Time.deltaTime);

            //플레이어 방향으로 전환
            transform.forward = dir;

        }
    }

    void Stop()
    {

    }

    void attack()
    {

    }

    public void FindPlayerPos(Vector3 Pos)
    {
        PlayerPos = Pos;
    }

}
