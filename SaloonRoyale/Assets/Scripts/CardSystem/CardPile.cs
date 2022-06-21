using System;
using System.Collections.Generic;

namespace CardSystem
{

	public abstract class CardPile
	{
		public List<Card> pileCards;
	}
	[Serializable]
	public class HandPile : CardPile
	{
		public int maxHandSize;
		

	}
	[Serializable]
	public class DrawPile : CardPile
	{
		
	}
	[Serializable]
	public class DiscardPile : CardPile
	{
		
	}

}