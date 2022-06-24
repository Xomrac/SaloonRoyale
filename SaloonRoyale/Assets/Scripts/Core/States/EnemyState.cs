using CardSystem;
using CardSystem.PlayerUI;
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
        [SerializeField] private ProcessCardsUI processCardsUI;
        
        private Character _enemyCharacter;
        private DeckHolder _enemyDeckHolder;

        public override void OnEnter(StateMachine stateMachine)
        {
            var point = sequencer.GetCurrentPoint() as EnemyPoint;
            if (point)
            {
                _enemyCharacter = point.GetEnemy();
                _enemyDeckHolder = _enemyCharacter.deckHolder;
                _enemyDeckHolder.DrawToFillHand();

                var randomCardFromHand = _enemyDeckHolder.PlayRandomCardFromHand();
                processCardsUI.SetEnemyCard(randomCardFromHand);
                _enemyDeckHolder.DiscardCard(randomCardFromHand);
                stateMachine.ChangeState(stateMachine.processCardState);
            }
            else
            {
                Debug.LogError("Enter in enemy state but no enemy in sequence.");
            }
        }

        public override void OnUpdate(StateMachine stateMachine) {}

        public override void OnExit(StateMachine stateMachine)
        {
            processCardsUI.ShowEnemyCard();
        }
    }
}