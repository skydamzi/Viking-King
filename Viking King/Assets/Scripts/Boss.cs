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
        currentHP = maxHP;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("BoxingGlove")) // �Ǵ� �±� ���
        {
            Player player = other.GetComponentInParent<Player>();
            if (player != null && player.isNeckAttacking)
            {
                TakeDamage(player.TotalAttack());
                Debug.Log("����: �۷��� �°� ������ ����");
            }
            else
            {
                Debug.Log("����: ��� ������ ���� ���� �ƴ�");
            }
        }
        if (other.CompareTag("Bullet"))
        {
            float damage = playerDamage.GetComponent<Player>().TotalAttack();
            TakeDamage(damage); // ������ 10 ���
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        damageText.text = damage.ToString();
        currentHP -= damage;
        
        Debug.Log("Boss HP: " + currentHP);

        if (currentHP <= 0)
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
