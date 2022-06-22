using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CardSystem;
using UnityEngine;

namespace DefaultNamespace
{

	public class UICardDisplayer : MonoBehaviour
	{
		[SerializeField] private RectTransform playerCardDisplay;
		[SerializeField] private UICard enemyCardDisplay;
		[SerializeField]private float moveTime;
		
		public Action<UICard> onPlayerCardPlayed;
		public Action<Card> onEnemyCardPlayed;

		private UICard playerDisplayedCard;

		private void Awake()
		{
			onPlayerCardPlayed += MoveCard;
			onEnemyCardPlayed += DisplayEnemyCard;
		}

		public void PutBackCards()
		{
			playerDisplayedCard.canvasGroup.alpha = 0;
			playerDisplayedCard.transform.position = playerDisplayedCard.startPos;
			playerDisplayedCard.displayOnly = false;
			enemyCardDisplay.canvasGroup.alpha = 0;
		}

		private void MoveCard(UICard card)
		{
			playerDisplayedCard = card;
			card.displayOnly = true;
			card.moveCoroutine ??= StartCoroutine(MoveCoroutine(card));
		}
		public IEnumerator MoveCoroutine(UICard card)
		{
			float elapsedTime = 0;
			while (elapsedTime<=moveTime)
			{
				transform.position = Vector3.Lerp(card.startPos, playerCardDisplay.position, elapsedTime / moveTime);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
		}

		public void DisplayEnemyCard(Card enemyCard)
		{
			enemyCardDisplay.displayOnly = true;
			enemyCardDisplay.SetupCard(enemyCard);
		}
	}

}