using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SaloonRoyale;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CardSystem
{

	public enum PileType
	{
		Draw,
		Discard,
		Hand
	}

	public class DeckHolder : MonoBehaviour
	{

		#region Fields

		#region Player Settings

		[PropertySpace(10)] [Title("PLAYER SETTINGS")] [SerializeField]
		private Deck playerDeck;
		public Deck PlayerDeck => playerDeck;

		// [SerializeField] private int maxHandSize;
		// public int MaxHandSize => maxHandSize;

		#endregion

		#region Piles

		[PropertySpace(10)] [Title("PILES")] [SerializeField]
		private DrawPile drawPile;
		public List<Card> DrawPileCards => drawPile.pileCards;

		[SerializeField] private HandPile hand;
		public List<Card> HandCards => hand.pileCards;

		[SerializeField] private DiscardPile discardPile;
		public List<Card> DiscardPileCards => discardPile.pileCards;

		#endregion

		#region Actions

		public Action OnCardDiscarded;
		public Action OnCardDrawn;
		public Action OnCardPlayed;

		#endregion

		#endregion

		#region Methods

		public bool HandIsEmpty => hand.pileCards.Count == 0;
		public bool DrawCardsAreFinished => drawPile.pileCards.Count == 0;

		public Suits GetTopCardSuit(CardPile pileToCheck) => pileToCheck.pileCards.First().CardSuit;


		public void RepopulateDrawPile()
		{
			List<Card> shuffledCards = discardPile.pileCards;
			shuffledCards.Shuffle();
			drawPile.pileCards = new List<Card>(shuffledCards);
			discardPile.pileCards.Clear();
		}
		private void ChangeCardPile([CanBeNull] Card card, CardPile fromPile, CardPile destinationPile)
		{
			if (fromPile.pileCards.Count > 0)
			{
				if ((destinationPile is HandPile && hand.pileCards.Count < hand.maxHandSize) || (destinationPile is not HandPile))
				{
					fromPile.pileCards.Remove(card);
					destinationPile.pileCards.Add(card);
					if (fromPile is DrawPile && fromPile.pileCards.Count == 0)
					{
						RepopulateDrawPile();
					}
				}
			}
			else if (fromPile is DrawPile && fromPile.pileCards.Count == 0)
			{
				if (discardPile.pileCards.Count > 0)
				{
					RepopulateDrawPile();
				}
			}
		}

		#region Cards

		#region Card Drawing

		public void DrawCardFromPile([CanBeNull] Card cardToDraw, CardPile pileToDrawFrom)
		{
			ChangeCardPile(cardToDraw, pileToDrawFrom, hand);
			OnCardDrawn?.Invoke();
		}

		public void DrawCardsFromPile(int amount, CardPile pileToDrawFrom)
		{
			if (pileToDrawFrom is DrawPile && pileToDrawFrom.pileCards.Count == 0)
			{
				if (discardPile.pileCards.Count > 0)
				{
					RepopulateDrawPile();
				}
			}
			else
			{
				for (int i = 0; i < amount; i++)
				{
					DrawCardFromPile(pileToDrawFrom.pileCards[0], pileToDrawFrom);
				}
			}
		}

		public void DrawCardsFromPile(List<Card> cardsToDraw, CardPile pileToDrawFrom)
		{
			foreach (Card card in cardsToDraw)
			{
				DrawCardFromPile(card, pileToDrawFrom);
			}
		}

		public void DrawToFillHand(CardPile pileToDrawFrom)
		{
			int amountToDraw = hand.maxHandSize - hand.pileCards.Count;
			DrawCardsFromPile(amountToDraw, pileToDrawFrom);
		}

		public void DrawRandomCardFromPile(CardPile pileToDrawFrom)
		{
			DrawCardFromPile(GetRandomCardFromPile(pileToDrawFrom), pileToDrawFrom);
		}

		public void DrawrandomCardsFromPile(int amount, CardPile pileToDrawFrom)
		{
			DrawCardsFromPile(GetRandomCardsFromPile(amount, pileToDrawFrom), pileToDrawFrom);
		}

		#endregion

		#region Card Discarding

		public void DiscardCard(Card cardToDiscard)
		{
			if (!HandIsEmpty)
			{
				ChangeCardPile(cardToDiscard, hand, discardPile);
				OnCardDiscarded?.Invoke();
			}
		}

		public void DiscardCards(int amount)
		{
			foreach (Card card in hand.pileCards.GetRange(0, amount))
			{
				DiscardCard(card);
			}
		}

		public void DiscardCards(List<Card> cardsToDiscard)
		{
			foreach (Card card in cardsToDiscard)
			{
				DiscardCard(card);
			}
		}

		public void DiscardRandomCard()
		{
			ChangeCardPile(GetRandomCardFromPile(hand), hand, discardPile);
		}

		#endregion

		#region Get Random Cards

		private Card GetRandomCardFromPile(CardPile pileToGetFrom) => pileToGetFrom.pileCards[Random.Range(0, pileToGetFrom.pileCards.Count)];

		private List<Card> GetRandomCardsFromPile(int amount, CardPile pileToGetFrom)
		{
			var randomCards = new List<Card>(amount);
			for (int i = 0; i < amount;)
			{
				Card randomCard = GetRandomCardFromPile(pileToGetFrom);
				if (!randomCards.Contains(randomCard))
				{
					randomCards.Add(randomCard);
					i++;
				}
			}
			return randomCards;
		}

		#endregion

		#endregion

		public void PlayCard(Card cardToPlay)
		{
			OnCardPlayed?.Invoke();
		}

		#region Piles

		#region Piles Checking

		private bool CheckIfCanDraw(int amountToDraw, List<Card> pileToDrawFrom)
		{
			return amountToDraw <= pileToDrawFrom.Count;
		}

		private int CountHowMuchCanDraw(int amountToDraw, List<Card> pileToDrawFrom)
		{
			return CheckIfCanDraw(amountToDraw, pileToDrawFrom) ? amountToDraw : pileToDrawFrom.Count;
		}

		#endregion

		#region Piles Shuffle

		private void ShuffleAPile(IList<Card> pileToShuffle)
		{
			pileToShuffle.Shuffle();
		}

		public void ShuffleDrawPile()
		{
			ShuffleAPile(drawPile.pileCards);
		}

		public void ShuffleDiscardPile()
		{
			ShuffleAPile(discardPile.pileCards);
		}

		public void ShuffleHand()
		{
			ShuffleAPile(hand.pileCards);
		}

		#endregion

		#region Piles Reset

		private void ResetAllPiles()
		{
			ResetDrawPile();
			ResetHand();
			ResetDiscardPile();
		}

		public void ResetDrawPile()
		{
			drawPile.pileCards = new List<Card>(playerDeck.DeckCards);
		}

		public void ResetDiscardPile()
		{
			discardPile.pileCards.Clear();
		}

		public void ResetHand()
		{
			hand.pileCards.Clear();
		}

		#endregion

		#endregion

		#endregion

		private void Start()
		{
			ResetAllPiles();
		}

		#region Debug Methods

#if UNITY_EDITOR

		[PropertySpace(30)]
		[Title("DEBUG")]
		[Button("Init Piles")]
		private void DebugInitPiles()
		{
			ResetAllPiles();
		}

		[Button("Draw N Cards")]
		private void DebugDrawFromDraw(int amount)
		{
			DrawCardsFromPile(amount, drawPile);
		}

		[Button("Draw N Random Cards")]
		private void DebugDrawRandomFromDraw(int amount)
		{
			DrawrandomCardsFromPile(amount, drawPile);
		}

		[Button("Discard Random Card")]
		private void DebugDiscardRandomFromHand()
		{
			DiscardRandomCard();
		}

		[Button("Reset Draw")]
		private void DebugResetDraw()
		{
			ResetDrawPile();
		}

		[Button("Reset Hand")]
		private void DebugResetHand()
		{
			ResetHand();
		}

		[Button("Reset Discard")]
		private void DebugResetDiscard()
		{
			ResetDiscardPile();
		}

		[Button("Reset All Piles")]
		private void DebugResetAllPiles()
		{
			ResetAllPiles();
		}

#endif

		#endregion

	}

}