using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Core.States
{
    public class EndGameState : State
    {
        [SerializeField] private CanvasGroup startGameCanvasGroup;
        [SerializeField] private TextMeshProUGUI customMessage;
        
        [Title("UI Fade In Setting")]
        [SerializeField] public AnimationCurve fadeInUICurve;
        [SerializeField] public float fadeInUITime = 2f;
        
        [Title("UI Fade Out Settings")]
        [SerializeField] public AnimationCurve fadeOutUICurve;
        [SerializeField] public float fadeOutUITime = 2f;

        private Coroutine _canvasGroupCoroutine;
        
        public override void OnEnter(StateMachine stateMachine)
        {
            if (_canvasGroupCoroutine != null)
            {
                StopCoroutine(_canvasGroupCoroutine);
            }

            _canvasGroupCoroutine = StartCoroutine(ShowGroup());
        }

        public override void OnUpdate(StateMachine stateMachine)
        {
            if (Input.anyKeyDown)
            {
                // Application quit or return to the menu
                Application.Quit();
            }
        }

        public override void OnExit(StateMachine stateMachine)
        {
            if (_canvasGroupCoroutine != null)
            {
                StopCoroutine(_canvasGroupCoroutine);
            }

            _canvasGroupCoroutine = StartCoroutine(HideGroup());
        }

        private IEnumerator ShowGroup()
        {
            var timer = 0f;
            var t = 0f;

            while (timer <= fadeInUITime)
            {
                timer += Time.deltaTime;
                t = timer / fadeInUITime;
                startGameCanvasGroup.alpha = fadeInUICurve.Evaluate(t);
                yield return null;
            }

            startGameCanvasGroup.alpha = 1.0f;
        }

        private IEnumerator HideGroup()
        {
            var timer = 0f;
            var t = 0f;

            while (timer <= fadeOutUITime)
            {
                timer += Time.deltaTime;
                t = 1.0f - (timer / fadeOutUITime);
                startGameCanvasGroup.alpha = fadeOutUICurve.Evaluate(t);
                yield return null;
            }

            startGameCanvasGroup.alpha = 0.0f;
        }

        public void SetCustomMessage(string message)
        {
            if (customMessage)
            {
                customMessage.text = message;
            }
        }
    }
}