using System;
using System.Collections;
using System.Collections.Generic;
using CardSystem;
using UnityEngine;

public class UIHand : MonoBehaviour
{
    [SerializeField] private DeckHolder playerDeck;
    [SerializeField] private List<UICard> playerHandCards;
    [SerializeField] private CanvasGroup canvasGroup;


    public void DisplayHand()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    public void DisableHand()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void Awake()
    {
        playerDeck.OnCardDiscarded += DisplayCards;
        playerDeck.OnCardDrawn += DisplayCards;
    }

    private void Start()
    {
        DisplayCards();
    }

    public void DisplayCards()
    {
        for (int i = 0; i < playerDeck.HandCards.Count; i++)
        {
            playerHandCards[i].SetupCard(playerDeck.HandCards[i],playerDeck);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
