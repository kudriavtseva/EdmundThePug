using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public float Speed = 2.0f; //how quickly the platform moves
    public Vector3 Direction;      //напрям руху
    public float PointB = 5.0f;    //точка прибуття, де платформа повертає назад
    Vector3 PointA;     //початкова точка
    public void Start()
    {
        PointA = transform.position;
    }
    public void FixedUpdate()
    {
        //так як ми самі задаємо напрям в налаштуваннях компонента, перевіряємо його в скрипті:
        if (Direction == Vector3.right || Direction == Vector3.left)
        {
            if (transform.position.x > PointA.x + PointB)
                Direction = -Direction;
            if (transform.position.x < PointA.x)
                Direction = Vector3.right;
        }
        else
        {
            if (transform.position.y > PointA.y + PointB)
                Direction = -Direction;
            if (transform.position.y < PointA.y)
                Direction = Vector3.up;
        }
        transform.Translate(Direction * Speed * Time.deltaTime);
    }
}