using System.Collections.Generic;
using UnityEngine;

namespace CardSystem.PlayerUI
{
    public class HandUI : MonoBehaviour
    {
        [SerializeField] private DeckHolder deck;
        [SerializeField] private List<CardUI> cards;
        [SerializeField] private CanvasGroup handCanvasGroup;
        
        private void Start()
        {
            deck.OnCardDrawn += UpdateCards;
            deck.OnCardDiscarded += UpdateCards;
        }

        public void UpdateCards()
        {
            for (int i = 0; i < deck.HandCards.Count; i++)
            {
                cards[i].SetCard(deck.HandCards[i]);
            }
        }

        public void Hide()
        {
            handCanvasGroup.alpha = 0;
            handCanvasGroup.interactable = false;
            handCanvasGroup.blocksRaycasts = false;
        }

        public void Show()
        {
            handCanvasGroup.alpha = 1f;
            handCanvasGroup.interactable = true;
            handCanvasGroup.blocksRaycasts = true;
        }
    }
}