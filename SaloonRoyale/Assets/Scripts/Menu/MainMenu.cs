using System;
using System.Collections;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private AudioTrack assignedAudioTrack;

        private AudioSystem _audioSystem;

        private Action _onMenuStart;

        private void OnEnable()
        {
            _onMenuStart += PlayAudio;
        }

        private void Awake()
        {
            playButton.onClick.AddListener(LoadScene);
        
            _audioSystem = FindObjectOfType<AudioSystem>();
        }

        private void Start()
        {
            _onMenuStart?.Invoke();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        private void LoadScene()
        {
            StartCoroutine(LoadSceneCo());
        }

        private IEnumerator LoadSceneCo()
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }
    
        private void PlayAudio()
        {
            var audioClip = assignedAudioTrack.GetClip();
            _audioSystem.PlaySoundtrack(audioClip);
        }

        private void OnDisable()
        {
            _onMenuStart -= PlayAudio;
        }
    }
}
