using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class QuickAccessSelector : MonoBehaviour
{
    private const int SlotCount = 5;

    [SerializeField] private List<Usable> usables = new List<Usable>();
    [SerializeField] private Usable[] quickAccessSlots = new Usable[SlotCount];
    [SerializeField] private Image[] images = new Image[SlotCount];

    [Header("Scrolling")]
    [SerializeField, Min(0.01f)] private float animationDuration = 0.25f;
    [SerializeField, Min(0f)] private float imageSpacing;
    [SerializeField] private Ease animationEase = Ease.OutCubic;

    private int currentStartIndex;
    private Sequence scrollSequence;
    private bool isScrolling;

    private void Awake()
    {
        currentStartIndex = 0;
        PopulateSlots();
    }

    private void OnDisable()
    {
        if (scrollSequence != null && scrollSequence.IsActive())
        {
            scrollSequence.Complete();
        }

        scrollSequence = null;
        isScrolling = false;
    }

    [ContextMenu("Go Left")]
    public void GoLeft()
    {
        if (!CanScroll())
        {
            return;
        }

        AnimateImages(-GetSlotDistance(), CompleteGoLeft);
    }
    [ContextMenu("Go Right")]
    public void GoRight()
    {
        if (!CanScroll())
        {
            return;
        }

        AnimateImages(GetSlotDistance(), CompleteGoRight);
    }

    private bool CanScroll()
    {
        if (isScrolling || usables == null || usables.Count == 0)
        {
            return false;
        }

        EnsureSlotCount();

        for (int imageIndex = 0; imageIndex < SlotCount; imageIndex++)
        {
            if (images[imageIndex] == null)
            {
                return false;
            }
        }

        return true;
    }

    private void AnimateImages(float movement, TweenCallback onComplete)
    {
        isScrolling = true;
        scrollSequence = DOTween.Sequence();

        for (int imageIndex = 0; imageIndex < SlotCount; imageIndex++)
        {
            RectTransform rectTransform = images[imageIndex].rectTransform;
            Tween movementTween = rectTransform
                .DOAnchorPosX(
                    rectTransform.anchoredPosition.x + movement,
                    animationDuration)
                .SetEase(animationEase);

            if (imageIndex == 0)
            {
                scrollSequence.Append(movementTween);
            }
            else
            {
                scrollSequence.Join(movementTween);
            }
        }

        scrollSequence.OnComplete(() =>
        {
            onComplete();
            isScrolling = false;
            scrollSequence = null;
        });
    }

    private void CompleteGoLeft()
    {
        Image recycledImage = images[0];

        for (int imageIndex = 0; imageIndex < SlotCount - 1; imageIndex++)
        {
            images[imageIndex] = images[imageIndex + 1];
        }

        images[SlotCount - 1] = recycledImage;

        RectTransform rightmostRectTransform =
            images[SlotCount - 2].rectTransform;

        Vector2 recycledPosition = recycledImage.rectTransform.anchoredPosition;
        recycledPosition.x =
            rightmostRectTransform.anchoredPosition.x + GetSlotDistance();

        recycledImage.rectTransform.anchoredPosition = recycledPosition;

        currentStartIndex = (currentStartIndex + 1) % usables.Count;
        PopulateSlots();
    }

    private void CompleteGoRight()
    {
        Image recycledImage = images[SlotCount - 1];

        for (int imageIndex = SlotCount - 1; imageIndex > 0; imageIndex--)
        {
            images[imageIndex] = images[imageIndex - 1];
        }

        images[0] = recycledImage;

        RectTransform leftmostRectTransform = images[1].rectTransform;

        Vector2 recycledPosition = recycledImage.rectTransform.anchoredPosition;
        recycledPosition.x =
            leftmostRectTransform.anchoredPosition.x - GetSlotDistance();

        recycledImage.rectTransform.anchoredPosition = recycledPosition;

        currentStartIndex =
            (currentStartIndex - 1 + usables.Count) % usables.Count;

        PopulateSlots();
    }

     
    private float GetSlotDistance()
    {
        return images[0].rectTransform.rect.width + imageSpacing;
    }

    private void PopulateSlots()
    {
        EnsureSlotCount();

        if (usables == null || usables.Count == 0)
        {
            ClearSlots();
            return;
        }

        for (int slotIndex = 0; slotIndex < SlotCount; slotIndex++)
        {
            int usableIndex = (currentStartIndex + slotIndex) % usables.Count;
            Usable usable = usables[usableIndex];

            quickAccessSlots[slotIndex] = usable;

            if (images[slotIndex] != null)
            {
                images[slotIndex].sprite = usable != null
                    ? usable.itemSprite
                    : null;
            }
        }
    }

    private void ClearSlots()
    {
        for (int slotIndex = 0; slotIndex < SlotCount; slotIndex++)
        {
            quickAccessSlots[slotIndex] = null;

            if (images[slotIndex] != null)
            {
                images[slotIndex].sprite = null;
            }
        }
    }

    private void EnsureSlotCount()
    {
        if (quickAccessSlots == null || quickAccessSlots.Length != SlotCount)
        {
            Array.Resize(ref quickAccessSlots, SlotCount);
        }

        if (images == null || images.Length != SlotCount)
        {
            Array.Resize(ref images, SlotCount);
        }
    }
}