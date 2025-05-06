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
    public int currentEXP = 0;
    public int maxEXP = 100;
    public float GetBaseDamage()
    {
        return baseAttackPower + bonusAttackPower;
    }
    public float GetMeleeDamage()
    {
        return GetBaseDamage() * 3f;
    }

    public virtual void GainEXP(int amount)
    {
        // PlayerStatus ����
        PlayerStatus.Instance.currentEXP += amount;

        // Unit �������� �ݿ�
        currentEXP = PlayerStatus.Instance.currentEXP;
        maxEXP = PlayerStatus.Instance.maxEXP;

        while (PlayerStatus.Instance.currentEXP >= PlayerStatus.Instance.maxEXP)
        {
            PlayerStatus.Instance.currentEXP -= PlayerStatus.Instance.maxEXP;

            // ����ȭ
            currentEXP = PlayerStatus.Instance.currentEXP;
            maxEXP = PlayerStatus.Instance.maxEXP;

            LevelUp();
        }
    }

    protected virtual void LevelUp()
    {
        // PlayerStatus ���� �� ����
        PlayerStatus.Instance.unitLV++;
        PlayerStatus.Instance.maxEXP += 20;
        PlayerStatus.Instance.maxHP += 10;

        // ü���� ȸ���ϵ�, maxHP�� ���� �ʵ��� ó��
        PlayerStatus.Instance.currentHP = Mathf.Min(
            PlayerStatus.Instance.currentHP + PlayerStatus.Instance.unitLV * 10,
            PlayerStatus.Instance.maxHP
        );

        // Unit �� ������ ����ȭ
        unitLV = PlayerStatus.Instance.unitLV;
        maxEXP = PlayerStatus.Instance.maxEXP;
        maxHP = PlayerStatus.Instance.maxHP;
        currentHP = PlayerStatus.Instance.currentHP;

        Debug.Log($"{PlayerStatus.Instance.unitName} ������! ���� ����: {unitLV}");
    }
}
