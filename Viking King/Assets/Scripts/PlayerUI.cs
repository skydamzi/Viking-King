using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject who;             // ü�� ����
    public RectTransform fillRT;       // ü�¹� fill ������Ʈ

    private Player playerScript;

    void Start()
    {
        if (who != null)
            playerScript = who.GetComponent<Player>();
    }

    void Update()
    {
        if (playerScript != null)
        {
            float rate = (float)playerScript.hp_current / playerScript.hp_max;
            SetHPbar(rate);
        }
    }

    public void SetHPbar(float rate)
    {
        if (playerScript.hp_current <= 0)
        {
            playerScript.hp_current = 0;
        }
        else
            fillRT.localScale = new Vector2(rate, 1f);
    }
}
