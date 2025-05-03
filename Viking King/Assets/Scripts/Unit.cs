using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLV;

    // ����/��� ����
    public float baseAttackPower;   // �⺻ ���ݷ�
    public float bonusAttackPower;  // �߰� ���ݷ� (���� ��)
    public float armor;             // ����
    public float currentShield;     // ���� ����
    public float maxShield;         // �ִ� ����
    public float currentHP;
    public float maxHP;

    public float TotalAttack()
    {
        return baseAttackPower + bonusAttackPower;
    }
}
