using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public static Equipment instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("���� ���� (�ν����Ϳ��� ���� �� �ڵ� ����)")]
    public int maxEquipCount = 5;
    public List<Item> equippedItems = new List<Item>();  // �ν����� �����

    private HashSet<Item> _equippedSet = new HashSet<Item>();  // ���� ������

    void Update()
    {
        // 1. ���� ���� ������ ���� �� ����
        foreach (var item in equippedItems)
        {
            if (item == null) continue;

            if (!_equippedSet.Contains(item))
            {
                if (_equippedSet.Count >= maxEquipCount)
                {
                    Debug.LogWarning($"��� ���� �ʰ�: {item.itemName} ���� ���õ�");
                    continue;
                }

                Equip(item);
                _equippedSet.Add(item);
                Debug.Log($"[Equipment] {item.itemName} ������ (�ν����� ����)");
            }
        }

        // 2. ���ŵ� ������ ���� �� ����
        var toRemove = new List<Item>();
        foreach (var item in _equippedSet)
        {
            if (!equippedItems.Contains(item))
            {
                Unequip(item);
                toRemove.Add(item);
                Debug.Log($"[Equipment] {item.itemName} ������ (�ν����� ����)");
            }
        }
        foreach (var item in toRemove)
        {
            _equippedSet.Remove(item);
        }
    }

    // ���ο� ���� ���� ó��
    public void Equip(Item item)
    {
        if (item.itemType != ItemType.Equip)
        {
            Debug.LogWarning("�� �������� ����� �� �����ϴ�.");
            return;
        }

        ApplyEffects(item.effects);
        ApplyTraitEffects(item.traitEffects);
    }

    // ���ο� ���� ���� ó��
    public void Unequip(Item item)
    {
        RemoveEffects(item.effects);
        RemoveTraitEffects(item.traitEffects);
    }

    private void ApplyEffects(List<Effect> effects)
    {
        foreach (var effect in effects)
        {
            PlayerStatus.instance?.ApplyEffect(effect);
        }
    }

    private void RemoveEffects(List<Effect> effects)
    {
        foreach (var effect in effects)
        {
            PlayerStatus.instance?.RemoveEffect(effect);
        }
    }

    private void ApplyTraitEffects(List<TraitEffect> traitEffects)
    {
        foreach (var trait in traitEffects)
        {
            TraitSynergy.Instance?.AddTrait(trait.traitType, trait.stackAmount);
        }
    }

    private void RemoveTraitEffects(List<TraitEffect> traitEffects)
    {
        foreach (var trait in traitEffects)
        {
            TraitSynergy.Instance?.RemoveTrait(trait.traitType, trait.stackAmount);
        }
    }

    public void UnequipAll()
    {
        foreach (var item in _equippedSet)
        {
            Unequip(item);
        }
        _equippedSet.Clear();
        equippedItems.Clear();
        Debug.Log("��� ��� ���� �Ϸ�");
    }
}
