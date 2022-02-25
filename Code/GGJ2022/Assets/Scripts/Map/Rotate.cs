using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    float speed = 3f;
    private void Update()
    {
        transform.eulerAngles += new Vector3(0, 0, 200f) * Time.deltaTime;
    }
}
