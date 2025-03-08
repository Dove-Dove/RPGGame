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

    EnemyState state;

    //플레이어 탐지
    public bool FindPlayer = false;


    // Start is called before the first frame update
    void Start()
    {
        state = EnemyState.Idle;

    }

    // Update is called once per frame
    void Update()
    {
       // FindPlayer = GetComponent<EnemyFind>().isCollision;
    }
}
