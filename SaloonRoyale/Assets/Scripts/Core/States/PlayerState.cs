using System;
using CardSystem;
using UnityEngine;

namespace Core.States
{
    public class PlayerState : State
    {
        private Character player;
        private DeckHolder playerDeck;
        private Card cardPlayed;
        [SerializeField] private UIHand uiHand;
        public Card CardPlayed => cardPlayed;
        
        public override void OnEnter(StateMachine stateMachine)
        {
            uiHand.DisplayCards();
            player = stateMachine.player;
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
        }
        
        public override void OnExit(StateMachine stateMachine)
        {
            uiHand.DisableHand();
            playerDeck.OnCardPlayed -= GetPlayedCard;
        }
    }
}