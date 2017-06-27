using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectable : MonoBehaviour
{
    protected virtual void OnPugHit(HeroPug pug)
    {
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        HeroPug pug = collider.GetComponent<HeroPug>();
        if (pug != null)
        {
            this.OnPugHit(pug);
        }

    }
    public void CollectedHide()
    {
        Destroy(this.gameObject);
    }
}
