using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private LayerMask cat;
    void Start()
    {
        cat = LayerMask.GetMask("Cat");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        int mask = 1 << collision.gameObject.layer;
        if ((mask & cat.value) != 0)
        {
            CatController.instance.Remake();
        }
    }
}
