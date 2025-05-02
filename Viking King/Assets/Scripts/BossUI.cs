using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUI : MonoBehaviour
{
    public GameObject who;             // ü�� ����
    public RectTransform fillRT;       // ü�¹� fill ������Ʈ

    private Boss bossScript;

    void Start()
    {
        if (who != null)
            bossScript = who.GetComponent<Boss>(); // Boss ��ũ��Ʈ ����
    }

    void Update()
    {
        if (bossScript != null)
        {
            float rate = (float)bossScript.hp_current / bossScript.hp_max;
            SetHPbar(rate);
        }
    }

    public void SetHPbar(float rate)
    {
        if (bossScript.hp_current <= 0)
        {
            bossScript.hp_current = 0;
        }
        else 
            fillRT.localScale = new Vector2(rate, 1f);
    }
}
