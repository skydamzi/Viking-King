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
            if (bossScript.currentHP < 0)
            {
                fillRT.gameObject.SetActive(false);
            }
            float rate = bossScript.currentHP / bossScript.maxHP;
            SetHPbar(rate);
        }
    }

    public void SetHPbar(float rate)
    {
        if (bossScript.currentHP <= 0)
        {
            bossScript.currentHP = 0;
            fillRT.gameObject.SetActive(false); // ü�� 0�̸� �ƿ� �� ���̰�
            return;
        }

        fillRT.localScale = new Vector3(rate, 1f, 1f);
    }
}
