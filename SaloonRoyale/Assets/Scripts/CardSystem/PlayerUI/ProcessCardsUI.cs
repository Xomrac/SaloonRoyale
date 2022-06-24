using UnityEngine;

namespace CardSystem.PlayerUI
{
    public class ProcessCardsUI : MonoBehaviour
    {
        [SerializeField] private CardUI playerCard;
        [SerializeField] private CardUI enemyCard;
        
        [SerializeField] private CanvasGroup playerCardCanvasGroup;
        [SerializeField] private CanvasGroup enemyCardCanvasGroup;
        
        public void SetPlayerCard(Card card)
        {
            playerCard.SetCard(card);
        }

        public void SetEnemyCard(Card card)
        {
            enemyCard.SetCard(card);
        }

        public Card GetEnemyCard()
        {
            return enemyCard.GetCard();
        }

        public Card GetPlayerCard()
        {
            return playerCard.GetCard();
        }

        public void ShowEnemyCard()
        {
            enemyCardCanvasGroup.alpha = 1f;
            enemyCardCanvasGroup.interactable = true;
            enemyCardCanvasGroup.blocksRaycasts = true;
        }

        public void HideEnemyCard()
        {
            enemyCardCanvasGroup.alpha = 0;
            enemyCardCanvasGroup.interactable = false;
            enemyCardCanvasGroup.blocksRaycasts = false;
        }

        public void ShowPlayerCard()
        {
            playerCardCanvasGroup.alpha = 1f;
            playerCardCanvasGroup.interactable = true;
            playerCardCanvasGroup.blocksRaycasts = true;
        }

        public void HidePlayerCard()
        {
            playerCardCanvasGroup.alpha = 0;
            playerCardCanvasGroup.interactable = false;
            playerCardCanvasGroup.blocksRaycasts = false;
        }
    }
}