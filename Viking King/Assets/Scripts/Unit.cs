using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLV;

    public float attackPower;
    public float damage;

    public float hp_max;
    public float hp_current;


    //�Ʒ��Լ� �߻�ȭ
    // abstract public void Attack();
}
