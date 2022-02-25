using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemakeArea : MonoBehaviour
{
    CatController catController;
    private void Awake()
    {
        catController = GameObject.FindGameObjectWithTag("CatController").GetComponent<CatController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BlackCat"))
        {
            catController.Remake();
        }
    }


}
