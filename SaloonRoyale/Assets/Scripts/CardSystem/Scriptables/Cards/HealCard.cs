using UnityEngine;

namespace CardSystem
{
	[CreateAssetMenu(fileName="ScriptableCard_Heal_",menuName="Card System/Cards/Heal Card")]

	public class HealCard : Card
	{
		[SerializeField] private float cardHealValue;
		public float CardHealValue => cardHealValue;
	}

}