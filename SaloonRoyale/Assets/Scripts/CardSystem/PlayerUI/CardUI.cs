using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardSystem.PlayerUI
{
    public class CardUI : MonoBehaviour
    {
        [SerializeField] private Image cardSpriteImage;
        [SerializeField] private Image cardSuitsImage;
        [SerializeField] private TextMeshProUGUI cardDescriptionText;

        [SerializeField] private float requiredTimeToSelect = 1.5f;
        [SerializeField] private AnimationCurve selectionCurve;
        [SerializeField] private Transform defaultTransform;
        [SerializeField] private Transform selectedTransform;
        
        private Card _card;
        private float _timer;
        private float _t;

        public void SetCard(Card card)
        {
            _card = card;
            cardDescriptionText.text = _card.CardEffect;
            cardSuitsImage.sprite = _card.CardSuitSprite;
            cardSpriteImage.sprite = _card.CardImage;
        }

        public Card GetCard()
        {
            return _card;
        }

        public void Select()
        {
            _timer += Time.deltaTime * 5f;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            _timer = Mathf.Clamp(_timer, 0.0f, requiredTimeToSelect);
            _t = _timer / requiredTimeToSelect;

            transform.position = Vector3.Lerp
            (
                    defaultTransform.position,
                    selectedTransform.position,
                    selectionCurve.Evaluate(_t)
            );
        }
    }
}