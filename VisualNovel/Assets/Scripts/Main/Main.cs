using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Image fadePanel; // UI Image 객체를 할당해야 합니다.
    public float fadeDuration = 3.0f; // 페이드 인/아웃 지속 시간을 3초로 설정
    public string Opening; // 이동할 다음 씬의 이름
    private bool isFading = false; // 페이드 중인지 여부를 나타내는 플래그

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

        yield return new WaitForSeconds(3.0f); // 페이드 아웃 이후 몇 초를 기다릴지 설정

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
