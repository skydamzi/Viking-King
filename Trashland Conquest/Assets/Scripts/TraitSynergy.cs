using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitSynergy : MonoBehaviour
{
    // �̱��� �ν��Ͻ� (�ٸ� ������ TraitSynergy.Instance�� ���� ����)
    public static TraitSynergy Instance;

    void Awake()
    {
        // �ߺ� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �Ӽ� ���� ���� (TraitType�̶�� �̸��� ������ enum)
    public enum TraitType
    {
        None,           // �⺻�� (�ƹ� �Ӽ��� ����)
        Milk,
        Slush,
        Alcohol,
        Soda,
        EnergyDrink,
        Coffee,
        Pesticide,
        PurifiedWater
    }

    // �Ӽ��� �ش� ���� ���� �����ϴ� Ŭ����
    [System.Serializable]
    public class TraitStack
    {
        public TraitType trait;
        public int stack;

        public TraitStack(TraitType trait, int stack)
        {
            this.trait = trait;
            this.stack = stack;
        }
    }

    // ���� ���� ���� �Ӽ� ���
    public List<TraitStack> activeTraits = new List<TraitStack>();

    // �Ӽ� �� �ִ� ���� ��
    private const int MaxStack = 5;

    // �Ӽ� �߰� �Լ� (�ߺ��̸� ���� ����, ������ ���� �߰�)
    public void AddTrait(TraitType trait, int amount = 1)
    {
        if (trait == TraitType.None) return;

        TraitStack found = activeTraits.Find(t => t.trait == trait);

        if (found != null)
        {
            // ������ �ִٸ� ���ø� ����
            found.stack = Mathf.Min(found.stack + amount, MaxStack);
        }
        else
        {
            // ������ ���� �߰�
            int stackToAdd = Mathf.Min(amount, MaxStack);
            activeTraits.Add(new TraitStack(trait, stackToAdd));
        }

        Debug.Log($"[TraitSynergy] �Ӽ� �߰���: {trait}, ���� ����: {GetStack(trait)}");
    }
    public void RemoveTrait(TraitType trait, int amount = 1)
    {
        TraitStack found = activeTraits.Find(t => t.trait == trait);
        if (found != null)
        {
            found.stack -= amount;
            if (found.stack <= 0)
            {
                activeTraits.Remove(found);
            }
        }

        Debug.Log($"[TraitSynergy] �Ӽ� ���ŵ�: {trait}, ���� ����: {GetStack(trait)}");
    }
    // Ư�� �Ӽ��� ���� ���� ���� Ȯ��
    public int GetStack(TraitType trait)
    {
        foreach (TraitStack t in activeTraits)
        {
            if (t.trait == trait)
                return t.stack;
        }
        return 0; // ������ 0
    }

    // Ư�� �Ӽ��� ������ �ִ��� Ȯ��
    public bool HasTrait(TraitType trait)
    {
        return GetStack(trait) > 0;
    }

    // �� �Ӽ��� ��� ������ �ִ��� (�ó��� ����)
    public bool HasSynergy(TraitType traitA, TraitType traitB)
    {
        return HasTrait(traitA) && HasTrait(traitB);
    }

    private bool randomTraitsGiven = false;

    public void AssignRandomTraitsOnce()
    {
        if (randomTraitsGiven) return;

        AddTrait(TraitType.Milk, Random.Range(0, 6));
        AddTrait(TraitType.Slush, Random.Range(0, 6));
        AddTrait(TraitType.Alcohol, Random.Range(0, 6));
        AddTrait(TraitType.Soda, Random.Range(0, 6));
        AddTrait(TraitType.EnergyDrink, Random.Range(0, 6));
        AddTrait(TraitType.Coffee, Random.Range(0, 6));
        AddTrait(TraitType.Pesticide, Random.Range(0, 6));
        AddTrait(TraitType.PurifiedWater, Random.Range(0, 6));

        randomTraitsGiven = true;
        Debug.Log("[TraitSynergy] ���� �Ӽ� �ʱ�ȭ �Ϸ�");
    }

    public void ResetTraits()
    {
        activeTraits.Clear();
        randomTraitsGiven = false;
    }
}
