using CardSystem;
using DefaultNamespace;
using Sequencing;
using Sequencing.Points;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.States
{
    public class EnemyState : State
    {
        [SerializeField] private SequenceHandler sequencer;
        [SerializeField] private UICardDisplayer displayer;
        private Character currentEnemy;
        private DeckHolder enemyDeck;
        private Card cardPlayed;
        public Card CardPlayed => cardPlayed;
        public override void OnEnter(StateMachine stateMachine)
        {
            var point = sequencer.GetCurrentPoint() as EnemyPoint;
            currentEnemy = point.GetEnemy();
            enemyDeck = currentEnemy.deckHolder;
            enemyDeck.DrawToFillHand();
        }

        public override void OnUpdate(StateMachine stateMachine)
        {
            cardPlayed = enemyDeck.PlayRandomCardFromHand();
            displayer.onEnemyCardPlayed?.Invoke(cardPlayed);
            stateMachine.ChangeState(stateMachine.processCardState);
        }

        public override void OnExit(StateMachine stateMachine)
        {
            
        }
    }
}