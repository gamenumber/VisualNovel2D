using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Image fadePanel; // UI Image ��ü�� �Ҵ��ؾ� �մϴ�.
    public float fadeDuration = 3.0f; // ���̵� ��/�ƿ� ���� �ð��� 3�ʷ� ����
    public string Opening; // �̵��� ���� ���� �̸�
    private bool isFading = false; // ���̵� ������ ���θ� ��Ÿ���� �÷���

    public GameObject Star;
    public GameObject InformationPage;


    public GameObject SeeGaugePage;

	public void OnClickStart()
    {
        if (!isFading)
        {
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    IEnumerator FadeOutAndLoadScene()
    {
        fadePanel.gameObject.SetActive(true);
        isFading = true;

        float elapsedTime = 0f; // Start from 0 for gradual fade-out
        Color originalColor = fadePanel.color;

        while (elapsedTime <= fadeDuration)
        {
            fadePanel.color = Color.Lerp(originalColor, Color.black, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime * 0.75f;
            yield return null;
        }

        fadePanel.color = Color.black;

        yield return new WaitForSeconds(3.0f); // ���̵� �ƿ� ���� �� �ʸ� ��ٸ��� ����

        SceneManager.LoadScene(Opening);

        yield return null;
    }

    public void OnClickStar()
    {
        Star.SetActive(true);
    }

    public void Backhome()
    {
		Star.SetActive(false);
	}

	public void OnClickInformation()
	{
		InformationPage.SetActive(true);
	}

	public void OnClickbacktothevoicepage()
	{
		InformationPage.SetActive(false);
        
	}

    public void OnClickHeart()
    {
		SeeGaugePage.SetActive(true);
	}

    public void OnClickBackhome2()
    {
		SeeGaugePage.SetActive(false);
	}
	
	
}
