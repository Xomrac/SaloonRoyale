using System;
using CardSystem;
using UnityEngine;

namespace Core.States
{
    public class PlayerState : State
    {
        [SerializeField] private Character player;
        [SerializeField] private UIHand uiHand;
        private DeckHolder playerDeck;
        private Card cardPlayed;
        
        public Card CardPlayed => cardPlayed;
        
        public override void OnEnter(StateMachine stateMachine)
        {
            
            uiHand.DisplayHand();
            playerDeck = player.deckHolder;
            playerDeck.DrawToFillHand();
            cardPlayed = null;
            playerDeck.OnCardPlayed += GetPlayedCard;
        }

        public override void OnUpdate(StateMachine stateMachine)
        {
            if (cardPlayed!=null)
            {
                stateMachine.ChangeState(stateMachine.enemyState);
            }
        }
        
        private void GetPlayedCard(Card playedCard)
        {
            cardPlayed = playedCard;
            playerDeck.DiscardCard(playedCard);
        }
        
        public override void OnExit(StateMachine stateMachine)
        {
            uiHand.DisableHand();
            playerDeck.OnCardPlayed -= GetPlayedCard;
        }
    }
}