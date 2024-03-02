using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Typing : MonoBehaviour
{
	private float _waittime = 1f;

	public GameObject RightButtons;

	public GameObject Yesno;
	public Image fadeImage;
	public Text DialogText;
	public Image Panel;
	public InputField inputField;
	public GameObject What;

	private bool _hasDisplayed = false;
	private bool _fadeOutStarted = false;
	private bool _ison = false;
	private bool _ison2 = false;

	void Start()
	{
		// 씬 시작 시 _hasDisplayed 변수를 초기화합니다.
		_hasDisplayed = false;

		DialogText.text = "";
		string sampleText = "당신의 이름을 알려주세요";
		StartCoroutine(TypingText(sampleText));
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (!_hasDisplayed && !_fadeOutStarted)
			{
				StopCoroutine("TypingText");
				_hasDisplayed = true;
			}

			if (_hasDisplayed && !_ison)
			{
				StartCoroutine(FadeOutTextAndPanel());
				_ison = true;
			}

			if (_hasDisplayed && _ison2)
			{
				string playerName = PlayerPrefs.GetString("PlayerName", "호성");
				string differentText = "당신의 이름은 " + playerName + "입니다.";
				StartCoroutine(TypingText(differentText));
				_ison2 = false;
			}
		}

		Esc();
	}

	public void SavePlayerName(string name)
	{
		PlayerPrefs.SetString("PlayerName", name);
	}

	public void Yes()
	{
		StartCoroutine(FadeOutTextAndPanel2());
		SavePlayerName(inputField.text); // inputField.text를 SavePlayerName에 전달
	}

	public void No()
	{
		StartCoroutine(FadeOutTextAndPanel());
	}

	public void OK()
	{
		What.SetActive(false);

		if (string.IsNullOrEmpty(inputField.text))
		{
			inputField.text = "호성";
		}

		string inputText = inputField.text;

		string message = inputText + " 이/가 맞습니까?";
		DialogText.text = message;

		StartCoroutine(FadeInPanelAndText());
	}

	public void GoingHome()
	{
		StartCoroutine(FadeOutTextAndPanel3());

		Yesno.gameObject.SetActive(false);
		What.gameObject.SetActive(false);
	}

	IEnumerator FadeInPanelAndText()
	{
		float startTime = Time.time;
		float fadeInDuration = 1f;

		while (Time.time < startTime + fadeInDuration)
		{
			float normalizedTime = (Time.time - startTime) / fadeInDuration;

			Color panelOriginalColor = Panel.color;
			panelOriginalColor.a = Mathf.Lerp(0f, 0.5f, normalizedTime);
			Panel.color = panelOriginalColor;

			Color textOriginalColor = DialogText.color;
			textOriginalColor.a = Mathf.Lerp(0f, 1f, normalizedTime);
			DialogText.color = textOriginalColor;

			yield return null;
		}

		yield return new WaitForSeconds(_waittime);

		Yesno.SetActive(true);
	}

	IEnumerator TypingText(string text)
	{
		foreach (char letter in text.ToCharArray())
		{
			if (!_hasDisplayed)
			{
				DialogText.text += letter;
				yield return new WaitForSeconds(0.03f);
			}
		}
		
		
	}

	IEnumerator TypingText2(string text)
	{
		foreach (char letter in text.ToCharArray())
		{
			if (_hasDisplayed)
			{
				DialogText.text += letter;
				yield return new WaitForSeconds(0.03f);
			}
		}


	}

	IEnumerator FadeOutTextAndPanel()
	{
		_fadeOutStarted = true;
		float startTime = Time.time;
		float fadeDuration = 1f;

		while (Time.time < startTime + fadeDuration)
		{
			float normalizedTime = (Time.time - startTime) / fadeDuration;

			Color textOriginalColor = DialogText.color;
			textOriginalColor.a = Mathf.Lerp(0.4f, 0f, normalizedTime);
			DialogText.color = textOriginalColor;

			Color panelOriginalColor = Panel.color;
			panelOriginalColor.a = Mathf.Lerp(0.4f, 0f, normalizedTime);
			Panel.color = panelOriginalColor;

			Yesno.SetActive(false);

			yield return null;
		}

		_fadeOutStarted = false;

		// 다음 사용을 위해 텍스트를 지웁니다.
		DialogText.text = "";

		yield return new WaitForSeconds(_waittime);

		What.SetActive(true);
	}

	IEnumerator FadeOutTextAndPanel2()
	{
		_fadeOutStarted = true;
		float startTime = Time.time;
		float fadeDuration = 1f;

		// First fade-out
		while (Time.time < startTime + fadeDuration)
		{
			float normalizedTime = (Time.time - startTime) / fadeDuration;

			Color textOriginalColor = DialogText.color;
			textOriginalColor.a = Mathf.Lerp(0.5f, 0f, normalizedTime);
			DialogText.color = textOriginalColor;

			Color panelOriginalColor = Panel.color;
			panelOriginalColor.a = Mathf.Lerp(0.5f, 0f, normalizedTime);
			Panel.color = panelOriginalColor;

			Yesno.SetActive(false);

			yield return null;
		}

		_fadeOutStarted = false;

		// Clear the text
		DialogText.text = "";

		yield return new WaitForSeconds(_waittime);

		// Second fade-in with different text
		startTime = Time.time;

		while (Time.time < startTime + fadeDuration)
		{
			float normalizedTime = (Time.time - startTime) / fadeDuration;

			Color textOriginalColor = DialogText.color;
			textOriginalColor.a = Mathf.Lerp(0f, 0.5f, normalizedTime);
			DialogText.color = textOriginalColor;

			Color panelOriginalColor = Panel.color;
			panelOriginalColor.a = Mathf.Lerp(0f, 0.5f, normalizedTime);
			Panel.color = panelOriginalColor;

			Yesno.SetActive(false);
			yield return null;
		}

		string playerName = PlayerPrefs.GetString("PlayerName", "호성");
		string differentText = "당신의 이름은 " + playerName + "입니다.";
		StartCoroutine(TypingText2(differentText));

		yield return new WaitForSeconds(2.5f);

		DialogText.text = "";

		yield return new WaitForSeconds(0.3f);

		string differentText2 = "당신은 생활고에 시달리는 고등학생이자 자취생입니다.";
		StartCoroutine(TypingText2(differentText2));

		yield return new WaitForSeconds(3.5f);
		DialogText.text = "";

		string differentText3 = "작은 방에서 당신의 이야기가 시작됩니다. ";
		StartCoroutine(TypingText2(differentText3));

		RightButtons.gameObject.SetActive(false);

		yield return new WaitForSeconds(4f);

		float startTime2 = Time.time;
		float fadeDuration2 = 1f;
		// First fade-out
		while (Time.time < startTime2 + fadeDuration2)
		{
			float normalizedTime = (Time.time - startTime2) / fadeDuration2;

			Color textOriginalColor = DialogText.color;
			textOriginalColor.a = Mathf.Lerp(0.5f, 0f, normalizedTime);
			DialogText.color = textOriginalColor;

			Color panelOriginalColor = Panel.color;
			panelOriginalColor.a = Mathf.Lerp(0.5f, 0f, normalizedTime);
			Panel.color = panelOriginalColor;

			Yesno.SetActive(false);

			yield return null;
		}
		yield return new WaitForSeconds(3f);

		SceneManager.LoadScene("Chapter1");
	}



	IEnumerator FadeOutTextAndPanel3()
	{
		_fadeOutStarted = true;
		float startTime = Time.time;
		float fadeDuration = 1f;

		while (Time.time < startTime + fadeDuration)
		{
			float normalizedTime = (Time.time - startTime) / fadeDuration;

			Color textOriginalColor = DialogText.color;
			textOriginalColor.a = Mathf.Lerp(0.5f, 0f, normalizedTime);
			DialogText.color = textOriginalColor;

			Color panelOriginalColor = Panel.color;
			panelOriginalColor.a = Mathf.Lerp(0.5f, 0f, normalizedTime);
			Panel.color = panelOriginalColor;

			yield return null;
		}

		_fadeOutStarted = false;
		Yesno.SetActive(false);
		RightButtons.gameObject.SetActive(false);

		// 다음 사용을 위해 텍스트를 지웁니다.
		DialogText.text = "";

		yield return new WaitForSeconds(_waittime);

		SceneManager.LoadScene("Main");
	}

	public void Esc()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
