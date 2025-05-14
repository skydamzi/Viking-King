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
    public float criticalChance;
    public float moveSpeed;
    public int currentEXP = 0;
    public int maxEXP = 100;
    public float GetBaseDamage()
    {
        return baseAttackPower + bonusAttackPower;
    }
    public float GetMeleeDamage()
    {
        return GetBaseDamage() * 5f;
    }

    public virtual void GainEXP(int amount)
    {
        // PlayerStatus ����
        PlayerStatus.instance.currentEXP += amount;

        // Unit �������� �ݿ�
        currentEXP = PlayerStatus.instance.currentEXP;
        maxEXP = PlayerStatus.instance.maxEXP;

        while (PlayerStatus.instance.currentEXP >= PlayerStatus.instance.maxEXP)
        {
            PlayerStatus.instance.currentEXP -= PlayerStatus.instance.maxEXP;

            // ����ȭ
            currentEXP = PlayerStatus.instance.currentEXP;
            maxEXP = PlayerStatus.instance.maxEXP;

            LevelUp();
        }
    }

    protected virtual void LevelUp()
    {
        // PlayerStatus ���� �� ����
        PlayerStatus.instance.unitLV++;
        PlayerStatus.instance.maxEXP += 20;
        PlayerStatus.instance.maxHP += 10;

        // ü���� ȸ���ϵ�, maxHP�� ���� �ʵ��� ó��
        PlayerStatus.instance.currentHP = Mathf.Min(
            PlayerStatus.instance.currentHP + PlayerStatus.instance.unitLV * 10,
            PlayerStatus.instance.maxHP
        );

        // Unit �� ������ ����ȭ
        unitLV = PlayerStatus.instance.unitLV;
        maxEXP = PlayerStatus.instance.maxEXP;
        maxHP = PlayerStatus.instance.maxHP;
        currentHP = PlayerStatus.instance.currentHP;

        Debug.Log($"{PlayerStatus.instance.unitName} ������! ���� ����: {unitLV}");
    }
}
