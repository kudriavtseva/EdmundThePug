
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour
{
    public HeroPug pug;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Отримали доступ до компонентів Transform кроля та камери відповідно 
        //(this бо скріпт застосовується до камери)
        Transform pug_transform = pug.transform;
        Transform camera_transform = this.transform;

        //Отримали доступ до координат кролика
        Vector3 pug_position = pug_transform.position;
        Vector3 camera_position = camera_transform.position;

        //Задаємо рух камери відповіднім до руху кроля
        camera_position.x = pug_position.x;
        camera_position.y  = pug_position.y;


        //Встановлюємо координати камери
        camera_transform.position = camera_position;
    }
}