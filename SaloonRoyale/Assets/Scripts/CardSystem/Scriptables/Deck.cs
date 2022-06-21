using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{

	[CreateAssetMenu(fileName = "ScriptableDeck_", menuName = "Card System/Deck")]
	public class Deck : ScriptableObject
	{
		[SerializeField] private List<Card> deckCards;
		public List<Card> DeckCards => deckCards;
	}

}