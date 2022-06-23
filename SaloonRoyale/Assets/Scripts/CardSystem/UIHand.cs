using System;
using System.Collections;
using System.Collections.Generic;
using CardSystem;
using DefaultNamespace;
using UnityEngine;

public class UIHand : MonoBehaviour
{
    [SerializeField] private DeckHolder playerDeck;
    [SerializeField] private List<UICard> playerHandCards;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private UICardDisplayer displayer;


    public void DisplayHand()
    {
        Debug.Log("test");
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        DisplayCards();
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
    

    public void DisplayCards()
    {
        for (int i = 0; i < playerDeck.HandCards.Count; i++)
        {
            playerHandCards[i].SetupCard(playerDeck.HandCards[i],playerDeck,displayer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
