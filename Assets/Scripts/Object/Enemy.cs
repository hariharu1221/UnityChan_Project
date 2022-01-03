using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Transform target;
    Animator anim;
    NavMeshAgent agent;

    enum State
    {
        Idle,
        Run,
        Attack
    }
    State state;

    void Start()
    {
        state = State.Idle;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (state == State.Idle)
        {
            SetState();
        }
        else if (state == State.Run)
        {
            UpdateRun();
        }
        else if (state == State.Attack)
        {
            UpdateAttack();
        }
    }

    private void UpdateAttack()
    {
        agent.speed = 0;

        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > 2)
        {
            state = State.Run;
            anim.SetTrigger("Run");
        }
    }

    private void UpdateRun()
    {
        agent.speed = 3.5f;
        agent.destination = target.transform.position;
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance <= 2)
        {
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
    }

    private void SetState()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < 15)
        {
            state = State.Run;
            anim.SetTrigger("Run");
        }
    }
}
