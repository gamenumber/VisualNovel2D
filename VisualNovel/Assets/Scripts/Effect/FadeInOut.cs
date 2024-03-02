using UnityEngine;
using UnityEngine.UI;
using System.Collections; // �߰�: IEnumerator�� ����ϱ� ���� �ʿ��� ���ӽ����̽�
public class FadeInOut : MonoBehaviour
{
	public float fadeDuration = 1.0f; // ���̵� ��/�ƿ��� �ɸ��� �ð�
	public CanvasGroup canvasGroup; // UI ����� CanvasGroup ������Ʈ

	private void Start()
	{
		// ���� CanvasGroup�� ���ٸ� �߰��մϴ�.
		if (canvasGroup == null)
		{
			canvasGroup = GetComponent<CanvasGroup>();
			if (canvasGroup == null)
			{
				canvasGroup = gameObject.AddComponent<CanvasGroup>();
			}
		}

		// ���� �� ���̵� ��
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

		canvasGroup.alpha = targetAlpha; // ���� ������ ����

		if (targetAlpha == 0)
		{
			gameObject.SetActive(false); // ���̵� �ƿ� �� ��ü ��Ȱ��ȭ (���� ����)
		}
	}
}
