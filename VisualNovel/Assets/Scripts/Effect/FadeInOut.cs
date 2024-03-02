using UnityEngine;
using UnityEngine.UI;
using System.Collections; // 추가: IEnumerator을 사용하기 위해 필요한 네임스페이스
public class FadeInOut : MonoBehaviour
{
	public float fadeDuration = 1.0f; // 페이드 인/아웃에 걸리는 시간
	public CanvasGroup canvasGroup; // UI 요소의 CanvasGroup 컴포넌트

	private void Start()
	{
		// 만약 CanvasGroup이 없다면 추가합니다.
		if (canvasGroup == null)
		{
			canvasGroup = GetComponent<CanvasGroup>();
			if (canvasGroup == null)
			{
				canvasGroup = gameObject.AddComponent<CanvasGroup>();
			}
		}

		// 시작 시 페이드 인
		FadeIn();
	}

	public void FadeIn()
	{
		StartCoroutine(Fade(0, 1));
	}

	public void FadeOut()
	{
		StartCoroutine(Fade(1, 0));
	}

	private IEnumerator Fade(float startAlpha, float targetAlpha)
	{
		float elapsedTime = 0;

		while (elapsedTime < fadeDuration)
		{
			canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		canvasGroup.alpha = targetAlpha; // 최종 값으로 설정

		if (targetAlpha == 0)
		{
			gameObject.SetActive(false); // 페이드 아웃 후 객체 비활성화 (선택 사항)
		}
	}
}
