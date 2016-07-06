﻿using UnityEngine;
using System.Collections;

public class AndiTestEnemy : FreBaseEnemy
{

    private Vector3 targetLocation;

    public float speed = 2;
    public int value = 100;
    public int life = 100;
    public GameObject playerDummy;
    private bool engaging = true;

    private Camera camera2d;
    private bool moveAllowed = true;
    private bool slowed = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void TurnToTarget()
    {
        if (engaging)
        {
            targetLocation = playerDummy.transform.position;
        }
        Vector3 vectorToTarget = targetLocation - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }

    private Vector3 GetNewTargetLocationOutSide()
    {
        return RotateAroundPoint(playerDummy.transform.position, transform.position, Quaternion.Euler(0, 0, Random.Range(-80, 80)));
    }


    private Vector3 RotateAroundPoint(Vector3 pivot, Vector3 point, Quaternion angle)
    {
        return (angle * (point - pivot) + pivot) + (pivot - point) * -Random.Range(3f, 5f);
    }

    public virtual void Move()
    {
        TurnToTarget();

        transform.position = transform.position + transform.right * speed * Time.deltaTime;

        if (engaging && Vector3.Distance(transform.position, targetLocation) < 3)
        {
            targetLocation = GetNewTargetLocationOutSide();
            engaging = !engaging;
            return;
        }

        if (Vector3.Distance(transform.position, targetLocation) < 1)
        {
            engaging = !engaging;
        }
    }
}