using JetBrains.Annotations;
using UnityEngine;

namespace CardSystem
{

	public abstract class Card : ScriptableObject
	{
		[SerializeField] private Suits cardSuit;
		[SerializeField] private Sprite cardImage;
		public Sprite CardImage => cardImage;
		public Suits CardSuit => cardSuit;
	}

}