using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelTrigger : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<HeroPug>() != null)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
