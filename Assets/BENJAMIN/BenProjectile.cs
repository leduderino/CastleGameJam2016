﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BenProjectile : BenColored {

	// Use this for initialization
    public static List<BenProjectile> projectiles = new List<BenProjectile>();

    public float speed = 1, lifeTime = 5;
    public bool followPlayer, bounce, followEnemy, canHitPlayer, canHitEnemy;
    public Vector3 direction;
    float countdown;

    [HideInInspector]
    public bool disabled;

	public void Init(Vector3 position, Vector3 direction, float timeOffset, float angle)
    {
        projectiles.Add(this);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        countdown = lifeTime;
        transform.position = position;
        this.direction = direction;

        transform.position += direction.normalized * speed * timeOffset;
        
    }

    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
        countdown -= Time.deltaTime;
        if (countdown <= 0)
            this.Destroy();
    }

    void Destroy()
    {
        projectiles.Remove(this);
        Destroy(gameObject);
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //if (col.gameObject == BenTestScript.playerGO)
    }


}
