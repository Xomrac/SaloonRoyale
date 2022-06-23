using System;
using System.Collections;
using DefaultNamespace;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardSystem
{

	public class UICard : MonoBehaviour
	{
		private Card card;
		public Image suitImage;
		public Image cardBackground;
		public Image cardIllustration;
		public TextMeshProUGUI cardName;
		public TextMeshProUGUI cardEffect;
		public Button selectButton;
		public CanvasGroup canvasGroup;

		private Coroutine enterCoroutine;
		private Coroutine exitCoroutine;
		public Coroutine moveCoroutine;
		public Vector3 startPos;
		private float verticalOffset;
		[SerializeField]private float focusTime;
		
		public bool displayOnly=false;
		private bool cardPlayed=false;

		private void Start()
		{
			startPos = transform.position;
			if (displayOnly)
			{
				canvasGroup.alpha = 0;
				canvasGroup.interactable = false;
				canvasGroup.blocksRaycasts = false;
			}
			
		}

		public IEnumerator FocusCoroutine()
		{
			float elapsedTime = 0;
			while (elapsedTime<=focusTime)
			{
				transform.position = new Vector3(startPos.x, Mathf.Lerp(startPos.y, startPos.y + verticalOffset, elapsedTime / focusTime),startPos.z);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
		}
		public IEnumerator UnFocusCoroutine()
		{
			float elapsedTime = 0;
			while (elapsedTime<=focusTime)
			{
				
				transform.position = new Vector3(startPos.x, Mathf.Lerp(startPos.y + verticalOffset, startPos.y, elapsedTime / focusTime),startPos.z);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
		}

		public void SetupCard(Card cardToDisplay)
		{
			cardPlayed = true;
			card = cardToDisplay;
			if (card.CardBackground!=null)
			{
				cardBackground.sprite = card.CardBackground;
			}
			if (card.CardImage!=null)
			{
				cardIllustration.sprite = card.CardImage;
			}
			cardName.text = card.CardName;
			cardEffect.text = card.CardEffect;
			canvasGroup.alpha = 1;
		}
		
		public void SetupCard(Card cardToDisplay, DeckHolder playerDeck, UICardDisplayer uiCardDisplayer)
		{
			cardPlayed = false;
			void SelectCard()
			{
				playerDeck.OnCardPlayed?.Invoke(cardToDisplay);
				uiCardDisplayer.onPlayerCardPlayed?.Invoke(this);
				canvasGroup.interactable = false;
				canvasGroup.blocksRaycasts = false;
				cardPlayed = true;
			}
			card = cardToDisplay;
			if (card.CardBackground!=null)
			{
				cardBackground.sprite = card.CardBackground;
			}
			if (card.CardImage!=null)
			{
				cardIllustration.sprite = card.CardImage;
			}
			cardName.text = card.CardName;
			cardEffect.text = card.CardEffect;
			if (!displayOnly)
			{
				selectButton.onClick.RemoveAllListeners();
				selectButton.onClick.AddListener(SelectCard);
				canvasGroup.interactable = true;
				canvasGroup.blocksRaycasts = true;
			}
			canvasGroup.alpha = 1;
		}
		
		public void FocusCard()
		{
			if (!cardPlayed)
			{
				enterCoroutine ??= StartCoroutine(FocusCoroutine());
			}
			
		}

		public void UnFocusCard()
		{
			if (!cardPlayed)
			{
				exitCoroutine ??= StartCoroutine(UnFocusCoroutine());
			}
		}

		
		
	}

}