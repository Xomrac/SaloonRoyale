using CardSystem;
using CardSystem.PlayerUI;
using UnityEngine;

namespace Core.States
{
    public class PlayerState : State
    {
        [SerializeField] private DeckHolder playerDeckHolder;
        [SerializeField] private HandUI handUI;
        [SerializeField] private ProcessCardsUI processCardUI;

        private StateMachine _stateMachine;
        
        public override void OnEnter(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            playerDeckHolder.DrawToFillHand();
            playerDeckHolder.OnCardPlayed += SetPlayedCard;
            handUI.Show();
            handUI.UpdateCards();
        }

        public override void OnUpdate(StateMachine stateMachine)
        {
            var selectedObject = Utility.GetPointedObject();

            if (selectedObject == null)
            {
                return;
            }

            var cardUI = selectedObject.GetComponent<CardUI>();
            if (cardUI)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    playerDeckHolder.PlayCard(cardUI.GetCard());
                }

                cardUI.Select();
            }
        }
        
        private void SetPlayedCard(Card playedCard)
        {
            playerDeckHolder.OnCardPlayed -= SetPlayedCard;
            processCardUI.SetPlayerCard(playedCard);
            playerDeckHolder.DiscardCard(playedCard);
            _stateMachine.ChangeState(_stateMachine.enemyState);
        }
        
        public override void OnExit(StateMachine stateMachine)
        {
            processCardUI.ShowPlayerCard();
            handUI.Hide();
        }
    }
}