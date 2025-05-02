using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Unit
{
    public GameObject playerDamage;
    public Text damageText;
    void Start()
    {
        hp_current = hp_max;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon")) // ���� �±� �ٿ����� Ȯ��
        {
            float damage = playerDamage.GetComponent<Player>().damage;
            TakeDamage(damage); // ������ 10 ���
        }
    }

    public void TakeDamage(float damage)
    {
        damageText.text = damage.ToString();
        hp_current -= damage;
        
        Debug.Log("Boss HP: " + hp_current);

        if (hp_current <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss Dead!");
        Destroy(gameObject);
    }
}
