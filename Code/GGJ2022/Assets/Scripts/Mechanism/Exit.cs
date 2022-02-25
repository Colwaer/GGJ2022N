using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    CatController catController;
    private void Awake()
    {
        catController = GameObject.FindGameObjectWithTag("CatController").GetComponent<CatController>();
    }
    private void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("BlackCat"))
        {
            if (catController.CatOnSamePos())
            {
                MySceneManager.Instance.LoadNextScene();
            }
            //if (catController.EnableSwitchCat() && catController.EnablePressSwitch)
            //{
            //    catController.SwitchCat();
            //    catController.EnablePressSwitch = false;
            //}
        }
    }
}
