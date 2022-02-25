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
                TipMessage.instance.SetWord("获取的道具，回到原点后也不会消失！");
                lastObtained = true;
            }
        }
    }

    IEnumerator MainCoroutine()
    {
        yield return new WaitForSeconds(1f);

        TipMessage.instance.SetWord("按A和D移动黑猫，空格来跳跃！");
        yield return new WaitForSeconds(2f);
        TipMessage.instance.SetWord("按A和D移动黑猫，空格来跳跃！");
        yield return new WaitForSeconds(3.5f);

        TipMessage.instance.SetWord("按Q切换猫猫，同时另一只猫会变成方块！");
        yield return new WaitForSeconds(2f);
        TipMessage.instance.SetWord("按Q切换猫猫，同时另一只猫会变成方块！");
        yield return new WaitForSeconds(3.5f);

        TipMessage.instance.SetWord("用WASD移动白猫，两只猫可以互相叠加！");
        yield return new WaitForSeconds(2f);
        TipMessage.instance.SetWord("用WASD移动白猫，两只猫可以互相叠加！");
        yield return new WaitForSeconds(3.5f);

        TipMessage.instance.SetWord("如果两只猫叠加在一起，就不能切换！");
        yield return new WaitForSeconds(3.5f);

        TipMessage.instance.SetWord("两只猫都要到达右上角的终点，然后叠加起来才行！");
        yield return new WaitForSeconds(2f);
        TipMessage.instance.SetWord("两只猫都要到达右上角的终点，然后叠加起来才行！");
        yield return new WaitForSeconds(3.5f);

        TipMessage.instance.SetWord("如果黑猫遭遇不测，两只猫就都会回到原点！");
        yield return new WaitForSeconds(3.5f);

        TipMessage.instance.SetWord("想暂停或者回到原点的话，按下Esc喵！");
        yield return new WaitForSeconds(3.5f);
    }
}
