using UnityEngine;

namespace CardSystem
{

	[CreateAssetMenu(fileName="ScriptableCard_Damage_",menuName="Card System/Cards/Damage Card")]
	public class DamageCard : Card
	{
		[SerializeField] private float cardDamage;
		public float CardDamage => cardDamage;
	}

}