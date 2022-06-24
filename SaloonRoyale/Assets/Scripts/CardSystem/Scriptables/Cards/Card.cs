using JetBrains.Annotations;
using UnityEngine;

namespace CardSystem
{

	public abstract class Card : ScriptableObject
	{
		[SerializeField] private Suits cardSuit;
		[SerializeField] private Sprite suitImage;
		[SerializeField] private Sprite cardImage;
		[SerializeField] private Sprite cardSuitsSprite;
		[SerializeField] private string cardName;
		[SerializeField] private string cardEffect;
		[SerializeField] private Sprite cardBackground;
		
		public Sprite CardImage => cardImage;

		public Sprite SuitImage => suitImage;
		public Suits CardSuit => cardSuit;
		public Sprite CardSuitSprite => cardSuitsSprite;
		public string CardName => cardName;
		public string CardEffect => cardEffect;
		public Sprite CardBackground => cardBackground;
	}

}