using System;
using System.Collections;
using Audio;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.States
{
    public class StartGameState : State
    {
        [SerializeField] private CanvasGroup startGameCanvasGroup;
        
        [Title("UI Fade In Setting")]
        [SerializeField] public AnimationCurve fadeInUICurve;
        [SerializeField] public float fadeInUITime = 2f;
        
        [Title("UI Fade Out Settings")]
        [SerializeField] public AnimationCurve fadeOutUICurve;
        [SerializeField] public float fadeOutUITime = 2f;

        [Title("Audio Settings")] [SerializeField]
        private AudioTrack assignedAudioTrack;
        
        private AudioSystem _audioSystem;

        private Action _onGameStart;

        private Coroutine _canvasGroupCoroutine;

        public void OnEnable()
        {
            _onGameStart += PlayAudio;
        }

        public override void OnEnter(StateMachine stateMachine)
        {
            _audioSystem = FindObjectOfType<AudioSystem>();
            _onGameStart?.Invoke();
            
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
                stateMachine.ChangeState(stateMachine.checkSequenceState);
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

        private void PlayAudio()
        {
            var audioClip = assignedAudioTrack.GetClip();
            _audioSystem.PlaySoundtrack(audioClip);
        }

        private void OnDisable()
        {
            _onGameStart -= PlayAudio;
        }
    }
}