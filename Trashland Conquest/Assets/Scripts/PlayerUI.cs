using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject who;             // ü�� ����
    public RectTransform fillRT;       // ü�¹� fill ������Ʈ
    public RectTransform shieldRT;
    public Text hpText;
    public RectTransform expRT;  // ����ġ ��
    public Text expText;

    public Text attack_powerText;
    public Text armorText;
    public Text levelText;
    public Text nameText;

    public Text milkText;
    public Text slushText;
    public Text alcoholText;
    public Text sodaText;
    public Text energyDrinkText;
    public Text coffeeText;
    public Text pesticideText;
    public Text purifiedWaterText;

    private Player playerScript;
    private TraitSynergy traitSynergy;
    private float prevHP = -1f;

    void Start()
    {
        if (who != null)
        {
            playerScript = who.GetComponent<Player>();
        }

        traitSynergy = TraitSynergy.Instance;

    }
    void Update()
    {
        if (playerScript != null)
        {
            playerScript.currentHP = Mathf.Max(playerScript.currentHP, 0);
            playerScript.currentShield = Mathf.Max(playerScript.currentShield, 0);

            float rateHP = playerScript.currentHP / playerScript.maxHP;
            float rateShield = playerScript.currentShield / playerScript.maxShield;
            if (playerScript.currentHP < prevHP)
            {
                StartCoroutine(ShakeUI(fillRT, 0.1f, 3f));
                StartCoroutine(ShakeUI(this.GetComponent<RectTransform>(), 0.1f, 5f));
            }
            if (playerScript.currentShield > 0f)
            {
                shieldRT.gameObject.SetActive(true);
                SetShieldbar(rateShield);
            }
            else
                shieldRT.gameObject.SetActive(false);

            prevHP = playerScript.currentHP;
            if (rateHP <= 0f)
            {
                fillRT.gameObject.SetActive(false);
            }
            else
            {
                fillRT.gameObject.SetActive(true);
                SetHPbar(rateHP);
            }
            hpText.text = $"{playerScript.currentHP} / {playerScript.maxHP}";

            float expRate = (float)playerScript.currentEXP / playerScript.maxEXP;
            expRT.localScale = new Vector3(expRate, 1f, 1f);
            expText.text = $"{playerScript.currentEXP} / {playerScript.maxEXP}";

            // �Ӽ� �ó��� ǥ�� UI
            nameText.text = $"�̸�: {playerScript.unitName}";
            levelText.text = $"����: {playerScript.unitLV}";
            attack_powerText.text = $"���ݷ�: {playerScript.baseAttackPower} (+ {playerScript.bonusAttackPower})";
            armorText.text = $"����: {playerScript.armor}";
            milkText.text = $"����: {traitSynergy.GetStack(TraitSynergy.TraitType.Milk)}";
            slushText.text = $"������: {traitSynergy.GetStack(TraitSynergy.TraitType.Slush)}";
            alcoholText.text = $"��: {traitSynergy.GetStack(TraitSynergy.TraitType.Alcohol)}";
            sodaText.text = $"ź��: {traitSynergy.GetStack(TraitSynergy.TraitType.Soda)}";
            energyDrinkText.text = $"�̿�: {traitSynergy.GetStack(TraitSynergy.TraitType.EnergyDrink)}";
            coffeeText.text = $"Ŀ��: {traitSynergy.GetStack(TraitSynergy.TraitType.Coffee)}";
            pesticideText.text = $"���: {traitSynergy.GetStack(TraitSynergy.TraitType.Pesticide)}";
            purifiedWaterText.text = $"������: {traitSynergy.GetStack(TraitSynergy.TraitType.PurifiedWater)}";
        }
    }
    public void SetHPbar(float rate)
    {
        fillRT.localScale = new Vector3(rate, 1f, 1f);
    }
    public void SetShieldbar(float rate)
    {
        shieldRT.localScale = new Vector3(rate, 1f, 1f);
    }
    IEnumerator ShakeUI(RectTransform target, float duration = 0.1f, float magnitude = 5f)
    {
        Vector3 originalPos = target.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;
            target.anchoredPosition = originalPos + new Vector3(offsetX, offsetY, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        target.anchoredPosition = originalPos;
    }
}
