using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QuickAccessSelector : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private const int SlotCount = 5;


    [SerializeField] Button senderButton; // a button we will back to after selecting the Usable.

    [SerializeField] private List<Usable> usables = new List<Usable>();
    private Usable[] quickAccessSlots = new Usable[SlotCount];
    [SerializeField] private Image[] images = new Image[SlotCount];

    [Header("Scrolling")]
    [SerializeField, Min(0.01f)] private float animationDuration = 0.25f;
    [SerializeField, Min(0f)] private float imageSpacing;
    [SerializeField] private Ease animationEase = Ease.OutCubic;

    [SerializeField] Direction direction; // the direction of this quick access selector. There are 4 of them.

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


    public void OnNavigate(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        if (direction.x > 0f)
        {
             
            GoRight();
        }
        else if (direction.x  < 0f)
        {
            GoLeft();

        }
    }

    [ContextMenu("Go Left")]
    public void GoLeft()
    {
        if (!CanScroll())
        {
            return;
        }

        float slotDistance = GetSlotDistance();

        PrepareGoLeft(slotDistance);
        AnimateImages(slotDistance);
    }

    [ContextMenu("Go Right")]
    public void GoRight()
    {
        if (!CanScroll())
        {
            return;
        }

        float slotDistance = GetSlotDistance();

        PrepareGoRight(slotDistance);
        AnimateImages(-slotDistance);
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

    private void PrepareGoLeft(float slotDistance)
    {
        Image recycledImage = images[SlotCount - 1];

        for (int imageIndex = SlotCount - 1; imageIndex > 0; imageIndex--)
        {
            images[imageIndex] = images[imageIndex - 1];
        }

        images[0] = recycledImage;

        Vector2 recycledPosition = recycledImage.rectTransform.anchoredPosition;
        recycledPosition.x =
            images[1].rectTransform.anchoredPosition.x - slotDistance;

        recycledImage.rectTransform.anchoredPosition = recycledPosition;

        currentStartIndex =
            (currentStartIndex - 1 + usables.Count) % usables.Count;

        PopulateSlots();
    }

    private void PrepareGoRight(float slotDistance)
    {
        Image recycledImage = images[0];

        for (int imageIndex = 0; imageIndex < SlotCount - 1; imageIndex++)
        {
            images[imageIndex] = images[imageIndex + 1];
        }

        images[SlotCount - 1] = recycledImage;

        Vector2 recycledPosition = recycledImage.rectTransform.anchoredPosition;
        recycledPosition.x =
            images[SlotCount - 2].rectTransform.anchoredPosition.x
            + slotDistance;

        recycledImage.rectTransform.anchoredPosition = recycledPosition;

        currentStartIndex = (currentStartIndex + 1) % usables.Count;

        PopulateSlots();
    }

    private void AnimateImages(float movement)
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
            isScrolling = false;
            scrollSequence = null;
        });
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
    public Usable GetCentralItem()
    {
        if (usables == null || usables.Count == 0)
        {
            return null;
        }

        int centralSlotIndex = SlotCount / 2;
        int usableIndex = (currentStartIndex + centralSlotIndex) % usables.Count;

        return usables[usableIndex];
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Selected button.");
        // disable nav
        InputManager.Instance.DisableUINav();

        InputManager.Instance.inputActions.FindAction("Navigate").performed += OnNavigate;

        // hook the function to nav

    }

    public void OnDeselect(BaseEventData eventData)
    {
        // reenable nav
        InputManager.Instance.EnableUINav();

        // dehook the function from nav 
        InputManager.Instance.inputActions.FindAction("Navigate").performed -= OnNavigate;

        

    }

    public void OnClick()
    {
        Debug.Log("Clicked.");

        senderButton.Select();
    }
}