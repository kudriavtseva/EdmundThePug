using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCat : Collectable
{
    public float Speed = 2.0f;
    public Vector3 Direction;
    public float PointB = 2.0f;
    Vector3 PointA;
    public void Start()
    {
        PointA = transform.position;

    }
    public void Update()
    {

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (transform.position.x > PointA.x + PointB)
        {
            Direction = -Direction;
            sr.flipX = true;
        }
        if (transform.position.x < PointA.x)
        {
            Direction = Vector3.right;
            sr.flipX = false;
        }
        transform.Translate(Direction * Speed * Time.deltaTime);

    }

    protected override void OnPugHit(HeroPug pug)
    {
        Animator animator = GetComponent<Animator>();
        if (pug.transform.position.y - this.transform.position.y >= 1.4)
        {
            Destroy(this.gameObject);
        }
        else
        {
            pug.removeHealth(1);
        }

    }

}
