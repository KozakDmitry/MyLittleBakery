using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Infostructure
{
    public class LoadingScreen : MonoBehaviour
    {

        public CanvasGroup Curtain;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Curtain.alpha = 1;

        }

        public void Hide()
        {
            StartCoroutine(FadeIn());
        }

        public IEnumerator FadeIn()
        {
            while (Curtain.alpha > 0)
            {
                Curtain.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }
            gameObject.SetActive(false);
        }
    }
}