using System.Collections;
using CardSystem;
using CardSystem.PlayerUI;
using Sequencing;
using Sequencing.Points;
using UnityEngine;

namespace Core.States
{
	public enum CardActions
	{
		Damage,
		Dodge,
		Heal
	}

	public class ProcessCardState : State
	{
		public float cardDisplayTime;

		private CardActions playerAction;
		private CardActions enemyAction;

		[SerializeField] private Character playerCharacter;
		[SerializeField] private SequenceHandler sequencer;
		[SerializeField] private ProcessCardsUI processCardUI;
		
		private StateMachine _stateMachine;

		public override void OnEnter(StateMachine stateMachine)
		{
			_stateMachine = stateMachine;
			
			var playerCard = processCardUI.GetPlayerCard();
			var enemyCard = processCardUI.GetEnemyCard();

			var enemyPoint = sequencer.GetCurrentPoint() as EnemyPoint;
			if (enemyPoint == null)
			{
				Debug.LogError("Enter in process card state but no enemy in sequence.");
				return;
			}
			
			var enemy = enemyPoint.GetEnemy();

			playerAction = playerCard switch
			{
				DamageCard => CardActions.Damage,
				HealCard => CardActions.Heal,
				MissedCard => CardActions.Dodge,
				_ => playerAction
			};
			enemyAction = enemyCard switch
			{
				DamageCard => CardActions.Damage,
				HealCard => CardActions.Heal,
				MissedCard => CardActions.Dodge,
				_ => enemyAction
			};
			
			switch (playerAction)
			{
				case CardActions.Damage:
				{
					int playerDamage = enemyAction != CardActions.Dodge ? (playerCard as DamageCard).CardDamage : 0;
					enemy.health.Deal(playerDamage);
					break;
				}
				case CardActions.Heal:
				{
					int playerHealAmount = (playerCard as HealCard).CardHealValue;
					playerCharacter.health.Heal(playerHealAmount);
					break;
				}
			}

			switch (enemyAction)
			{
				case CardActions.Damage:
				{
					int enemyDamage = playerAction != CardActions.Dodge ? (enemyCard as DamageCard).CardDamage : 0;
					playerCharacter.health.Deal(enemyDamage);
					break;
				}
				case CardActions.Heal:
				{
					int enemyHealAmount = (enemyCard as HealCard).CardHealValue;
					enemy.health.Heal(enemyHealAmount);
					break;
				}
			}
			
			StartCoroutine(DisplayCardsCoroutine());
		}

		private IEnumerator DisplayCardsCoroutine()
		{
			float elapsedTime = 0;
			while (elapsedTime <= cardDisplayTime)
			{
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			
			_stateMachine.ChangeState(_stateMachine.checkPlayerState);
		}

		public override void OnUpdate(StateMachine stateMachine) {}

		public override void OnExit(StateMachine stateMachine)
		{
			processCardUI.HideEnemyCard();
			processCardUI.HidePlayerCard();
		}
	}

}