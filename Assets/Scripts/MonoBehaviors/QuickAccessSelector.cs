using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickAccessSelector : MonoBehaviour
{
    private const int SlotCount = 5;

    [SerializeField] private List<int> numbers = new List<int>();
    [SerializeField] private int[] quickAccessSlots = new int[SlotCount];

    [SerializeField] Image[] images;

    private int currentStartIndex;

    private void Awake()
    {
        currentStartIndex = 0;
        PopulateSlots();
    }

    public void GoLeft()
    {
        if (numbers == null || numbers.Count == 0)
        {
            return;
        }

        currentStartIndex = (currentStartIndex + 1) % numbers.Count;
        PopulateSlots();
    }

    public void GoRight()
    {
        if (numbers == null || numbers.Count == 0)
        {
            return;
        }

        currentStartIndex =
            (currentStartIndex - 1 + numbers.Count) % numbers.Count;

        PopulateSlots();
    }

    private void PopulateSlots()
    {
        EnsureSlotCount();

        if (numbers == null || numbers.Count == 0)
        {
            for (int slotIndex = 0; slotIndex < quickAccessSlots.Length; slotIndex++)
            {
                quickAccessSlots[slotIndex] = 0;
            }

            return;
        }

        for (int slotIndex = 0; slotIndex < quickAccessSlots.Length; slotIndex++)
        {
            int numberIndex = (currentStartIndex + slotIndex) % numbers.Count;
            quickAccessSlots[slotIndex] = numbers[numberIndex];
        }
    }

    private void EnsureSlotCount()
    {
        if (quickAccessSlots == null || quickAccessSlots.Length != SlotCount)
        {
            quickAccessSlots = new int[SlotCount];
        }
    }
}