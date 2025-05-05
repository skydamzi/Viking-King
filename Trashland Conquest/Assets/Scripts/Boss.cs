using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Unit
{
    public GameObject playerObject;
    private Player player;
    public Text damageText;
    private bool hasBeenHit = false;
    public AudioClip glove_punchSound;

    void Start()
    {
        if (playerObject != null)
            player = playerObject.GetComponent<Player>();
        currentHP = maxHP;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("BoxingGlove"))
        {
            if (player != null && player.isNeckAttacking && !hasBeenHit && player.isPunchFrame)
            {
                hasBeenHit = true;
                SoundManager.Instance.PlaySFX(glove_punchSound);
                TakeDamage(player.TotalAttack());
                Debug.Log("����: �۷��� �°� ������ ����");
            }
            else
                Debug.Log("����: ��� ������ ���� ���� �ƴ�");
        }

        else if (other.CompareTag("Bullet"))
        {
            if (player != null)
                TakeDamage(player.TotalAttack());
            else
                Debug.LogWarning("�Ѿ� �浹: Player ���� ����");

            Destroy(other.gameObject);
        }
    }
    public void ResetHitFlag()
    {
        hasBeenHit = false;
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
        if (player != null)
        {
            player.GainEXP(currentEXP);
        }
        Destroy(gameObject);
    }
}
