using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTipTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BlackCat"))
        {
            TipMessage.instance.SetWord("��ֻèҪһ���߽�������");
        }
    }
}
