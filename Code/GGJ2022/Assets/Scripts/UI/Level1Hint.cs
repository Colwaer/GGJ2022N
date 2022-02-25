using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Hint : MonoBehaviour
{
    public Key key;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MainCoroutine());
    }

    // Update is called once per frame
    private bool lastObtained = false;
    void Update()
    {
        if(!lastObtained)
        {
            if(key.isObtained)
            {
                TipMessage.instance.SetWord("��ȡ�ĵ��ߣ��ص�ԭ���Ҳ������ʧ��");
                lastObtained = true;
            }
        }
    }

    IEnumerator MainCoroutine()
    {
        yield return new WaitForSeconds(1f);

        TipMessage.instance.SetWord("��A��D�ƶ���è���ո�����Ծ��");
        yield return new WaitForSeconds(2f);
        TipMessage.instance.SetWord("��A��D�ƶ���è���ո�����Ծ��");
        yield return new WaitForSeconds(3.5f);

        TipMessage.instance.SetWord("��Q�л�èè��ͬʱ��һֻè���ɷ��飡");
        yield return new WaitForSeconds(2f);
        TipMessage.instance.SetWord("��Q�л�èè��ͬʱ��һֻè���ɷ��飡");
        yield return new WaitForSeconds(3.5f);

        TipMessage.instance.SetWord("��WASD�ƶ���è����ֻè���Ի�����ӣ�");
        yield return new WaitForSeconds(2f);
        TipMessage.instance.SetWord("��WASD�ƶ���è����ֻè���Ի�����ӣ�");
        yield return new WaitForSeconds(3.5f);

        TipMessage.instance.SetWord("�����ֻè������һ�𣬾Ͳ����л���");
        yield return new WaitForSeconds(3.5f);

        TipMessage.instance.SetWord("��ֻè��Ҫ�������Ͻǵ��յ㣬Ȼ������������У�");
        yield return new WaitForSeconds(2f);
        TipMessage.instance.SetWord("��ֻè��Ҫ�������Ͻǵ��յ㣬Ȼ������������У�");
        yield return new WaitForSeconds(3.5f);

        TipMessage.instance.SetWord("�����è�������⣬��ֻè�Ͷ���ص�ԭ�㣡");
        yield return new WaitForSeconds(3.5f);

        TipMessage.instance.SetWord("����ͣ���߻ص�ԭ��Ļ�������Esc����");
        yield return new WaitForSeconds(3.5f);
    }
}
