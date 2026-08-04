using System.Collections;
using UnityEngine;

namespace PixelCross.UI
{
    public static class UIFade
    {
        public static IEnumerator FadeCanvasGroup(CanvasGroup group, float from, float to, float duration)
        {
            group.alpha = from;

            if (duration <= 0f)
            {
                group.alpha = to;
                yield break;
            }

            var elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                group.alpha = Mathf.Lerp(from, to, elapsed / duration);
                yield return null;
            }

            group.alpha = to;
        }
    }
}
