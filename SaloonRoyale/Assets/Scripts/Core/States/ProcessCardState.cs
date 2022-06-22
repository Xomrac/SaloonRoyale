using System;
using System.Collections;
using CardSystem;
using DefaultNamespace;
using JetBrains.Annotations;
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
		[SerializeField] private UICardDisplayer displayer;
		private Card playerCard;
		private Card enemyCard;

		private CardActions playerAction;
		private CardActions enemyAction;

		private Health playerHealth;
		private Health enemyhealth;

		private bool cardDisplaying;

		public override void OnEnter(StateMachine stateMachine)
		{
			playerCard = stateMachine.playerState.CardPlayed;
			enemyCard = stateMachine.enemyState.CardPlayed;
			playerAction = playerCard switch
			{
				DamageCard damageCard => CardActions.Damage,
				HealCard healCard => CardActions.Heal,
				MissedCard missedCard => CardActions.Dodge,
				_ => playerAction
			};
			enemyAction = enemyCard switch
			{
				DamageCard damageCard => CardActions.Damage,
				HealCard healCard => CardActions.Heal,
				MissedCard missedCard => CardActions.Dodge,
				_ => enemyAction
			};
			StartCoroutine(DisplayCardsCoroutine());
		}

		public IEnumerator DisplayCardsCoroutine()
		{
			cardDisplaying = true;
			float elapsedTime = 0;
			while (elapsedTime<=cardDisplayTime)
			{
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			cardDisplaying = false;
		}

		public override void OnUpdate(StateMachine stateMachine)
		{
			if (!cardDisplaying)
			{
				switch (playerAction)
				{
					case CardActions.Damage:
					{
						int playerDamage = enemyAction != CardActions.Dodge ? (playerCard as DamageCard).CardDamage : 0;
						enemyhealth.Deal(playerDamage);
						break;
					}
					case CardActions.Heal:
					{
						int playerHealAmount = (playerCard as HealCard).CardHealValue;
						playerHealth.Heal(playerHealAmount);
						break;
					}
				}

				switch (enemyAction)
				{
					case CardActions.Damage:
					{
						int enemyDamage = playerAction != CardActions.Dodge ? (enemyCard as DamageCard).CardDamage : 0;
						playerHealth.Deal(enemyDamage);
						break;
					}
					case CardActions.Heal:
					{
						int enemyHealAmount = (enemyCard as HealCard).CardHealValue;
						enemyhealth.Heal(enemyHealAmount);
						break;
					}
				}
				displayer.PutBackCards();
				stateMachine.ChangeState(stateMachine.checkPlayerState);
			}
		}
		
		

		public override void OnExit(StateMachine stateMachine)
		{
			throw new System.NotImplementedException();
		}
	}

}