using System.Collections;
using PixelCross.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCross.UI
{
    // White-background studio logo splash: fades in, holds, fades out, then
    // loads the title screen. Tap/click/any-key skips straight to the fade-out.
    [RequireComponent(typeof(CanvasGroup))]
    public class SplashScreenController : MonoBehaviour
    {
        [SerializeField] private float fadeInDuration = 0.5f;
        [SerializeField] private float holdDuration = 1.2f;
        [SerializeField] private float fadeOutDuration = 0.5f;
        [SerializeField] private string nextSceneName = SceneNames.Title;

        private CanvasGroup _canvasGroup;
        private bool _skipRequested;
        private bool _isFadingOut;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            StartCoroutine(PlaySplashSequence());
        }

        private void Update()
        {
            if (_isFadingOut) return;

            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 || Input.anyKeyDown)
            {
                _skipRequested = true;
            }
        }

        private IEnumerator PlaySplashSequence()
        {
            yield return StartCoroutine(UIFade.FadeCanvasGroup(_canvasGroup, 0f, 1f, fadeInDuration));

            var elapsed = 0f;
            while (elapsed < holdDuration && !_skipRequested)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            _isFadingOut = true;
            yield return StartCoroutine(UIFade.FadeCanvasGroup(_canvasGroup, 1f, 0f, fadeOutDuration));

            SceneManager.LoadScene(nextSceneName);
        }
    }
}
