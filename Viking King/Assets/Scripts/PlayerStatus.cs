using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;

    // �⺻ ����
    public string unitName = "�ź���";
    public int unitLV = 1;

    // ���� ����
    public float baseAttackPower = 30f;
    public float bonusAttackPower = 5f;
    public float armor = 0f;

    // ü��/����
    public float currentHP = 100f;
    public float maxHP = 100f;
    public float currentShield = 50f;
    public float maxShield = 50f;

    //����ġ
    public int currentEXP = 0;
    public int maxEXP = 100;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
