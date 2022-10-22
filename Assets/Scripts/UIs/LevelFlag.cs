using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelFlag : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] LevelHoverCard hoverCard;
    [SerializeField] bool hasShownHoverCard;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!hasShownHoverCard)
        {
            ShowHoverCard();
        }
    }

    void ShowHoverCard()
    {
        hoverCard.gameObject.SetActive(true);
        hasShownHoverCard = true;
    }

    public void HideHoverCard()
    {
        hoverCard.gameObject.SetActive(false);
        hasShownHoverCard = false;
    }
}
