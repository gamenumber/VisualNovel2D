using System.Collections;
using System.Diagnostics.Contracts;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86;

public class Chapter4 : MonoBehaviour
{
	
	public GameObject Rightbuttons;
	public GameObject DiaLogbox; // Change Image to GameObject
	public Text DialogText;
	public float fadeDuration = 0.8f;
	public float delayBeforeSceneChange = 0.5f; // Adjust this value to set the delay
	
	public AudioSource Voice;
	private bool _isSkip = false;  // ��ȭ�� ��ŵ�ߴ��� �ƴ����� Ȯ���ϴ� ����

	public Image Watch;

	public float fadeTime = 1f; // ���̵� �ƿ����� �ɸ��� �ð�, 1�ʷ� ����


	public Image Dark;
	
	public float phoneFadeDuration = 1.5f;

	public string[] dialogues; // ��ȭ ������ ������ �迭
	public int dialogueIndex = 0; // ���� ��ȭ�� �ε����� ������ ����

	public Text CharacterName;

	public float targetImageFadeDuration = 1.5f;


	public AudioSource audioSource;


	public Image TargetImage;

	private bool _startpadedone = false;

	private bool _isTyping = false; // ��ȭ�� ���������� �ƴ����� Ȯ���ϴ� ����

	
	private int userChoice = -1;  // ������� ������ �����ϴ� ����


	public bool isButtonClicked = false;

	// ��ȭ ���� üũ�� ���� ����
	float dialogueTimer = 0f;
	// ��ȭ�� �ڵ����� �Ѿ�� �ð�
	float autoSkipTime = 10f;

	public string characterName = "Seona";
	public float loveGuageIncreaseAmount = 60f;


	private static string savePath;
	public void IncreaseLoveGuage()
	{
		// savePath �ʱ�ȭ
		if (string.IsNullOrEmpty(savePath))
		{
			savePath = Path.Combine(Application.persistentDataPath, "LoveGuageData.json");
		}

		// ������ ȣ������ �ҷ��ɴϴ�.
		float currentLoveGuage = LoveGuageSystem.LoadLoveGuage(characterName);

		// ȣ������ ����� ���� ���ٸ� �⺻���� ����մϴ�.
		if (currentLoveGuage == -1)
		{
			currentLoveGuage = 0f;
		}

		// ȣ������ ������ŵ�ϴ�.
		float newLoveGuage = currentLoveGuage + loveGuageIncreaseAmount;

		// ���ο� ȣ������ �����մϴ�.
		LoveGuageSystem.SaveLoveGuage(characterName, newLoveGuage);
	}


	IEnumerator FadeImage()
	{
		Dark.gameObject.SetActive(true);
		for (float i = 0; i <= fadeTime; i += Time.deltaTime)
		{
			float fadeAmount = i / fadeTime; // 0���� 1�� ������ ����
			Dark.color = new Color(Dark.color.r, Dark.color.g, Dark.color.b, fadeAmount); // ���İ��� fadeAmount�� ����
			yield return null;
		}
		Dark.color = new Color(Dark.color.r, Dark.color.g, Dark.color.b, 1); // ���̵� �ƿ��� ���� �Ŀ��� ���İ��� 1�� ����
	}

	private void Start()
	{
		IncreaseLoveGuage();
		audioSource.Play();
		StartCoroutine(FadeOutTargetImage());
		StartCoroutine(FadeInDialogBox());
		string playerName = PlayerPrefs.GetString("PlayerName", "DefaultPlayerName");
		CharacterName.text= playerName;
	}

	public void ChangeLoveGuageValue(string characterName, float value)
	{
		// ȣ���� �������� Ű �̸��� �����ɴϴ�.
		string prefKey = characterName + "LoveGuage";
		// ���� ���� �ҷ��ɴϴ�.
		float currentGuage = PlayerPrefs.GetFloat(prefKey, 0);
		// ���ο� ������ ������Ʈ�մϴ�.
		float newGuage = Mathf.Clamp(currentGuage + value, 0, 100);
		// ����� ���� �����մϴ�.
		PlayerPrefs.SetFloat(prefKey, newGuage);
		// ���� ������ �����մϴ�.
		PlayerPrefs.Save();
	}


	IEnumerator Talk()
	{
		if (dialogueIndex == 7)
		{
			StartCoroutine(FadeImage());
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			CharacterName.text = "";

			
		}

		if (dialogueIndex == 8)
		{
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "ȣ��");
			CharacterName.text = playerName;
			
		}


		if (dialogueIndex == 12)
		{
			
			CharacterName.text = "����";
			CharacterName.color = new Color(0, 0, 0);

		}

		if (dialogueIndex == 13)
		{
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "ȣ��");
			CharacterName.text = playerName;

		}

		if (dialogueIndex == 14)
		{
			CharacterName.text = "����";
			CharacterName.color = new Color(0, 0, 0);
		}

		if (dialogueIndex == 15)
		{
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "ȣ��");
			CharacterName.text = playerName;
		}

		if (dialogueIndex == 16)
		{
			CharacterName.text = "����";
			CharacterName.color = new Color(0, 0, 0);
		}


		if (dialogueIndex == 17)
		{
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "ȣ��");
			CharacterName.text = playerName;
		}

		if (dialogueIndex == 19)
		{
			CharacterName.text = "����";
			CharacterName.color = new Color(0, 0, 0);
		}


		if (dialogueIndex == 21)
		{
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "ȣ��");
			CharacterName.text = playerName;
		}

		if (dialogueIndex == 22)
		{
			CharacterName.text = "����";
			CharacterName.color = new Color(0, 0, 0);
		}

		

		if (dialogueIndex == 24)
		{
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "ȣ��");
			CharacterName.text = playerName;

		}

		if (dialogueIndex == 25)
		{
			CharacterName.text = "����";
			CharacterName.color = new Color(0, 0, 0);
		}

		if (dialogueIndex == 26)
		{
			CharacterName.text = "";
			CharacterName.color = new Color(0, 0, 0);
		}

		if (dialogueIndex == 27)
		{
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "ȣ��");
			CharacterName.text = playerName;

			
		}

		if (dialogueIndex == 28)
		{
			CharacterName.text = "";
			StartCoroutine(FadeOutDialogBox());
			yield return new WaitForSeconds(2);
			GoingChapter5();
		}

		yield return null;
	}

		void Update()
	{
		// ���콺 ���� ��ư Ŭ�� �̺�Ʈ ó��
		if (Input.GetMouseButtonDown(0))
		{
			StartCoroutine(Talk());

			// ��ȭ�� �������̶��
			if (_isTyping)
			{
				// ��ȭ�� ��ŵ�����Ƿ� isSkip�� true�� ����
				_isSkip = true;
			}
			else
			{
				// ��ȭ�� �������� �ƴ϶��
				if (dialogueIndex < dialogues.Length)
				{
					// Start typing the dialogue
					StartCoroutine(TypingDialogue(dialogues[dialogueIndex]));

					// ��ȭ�� ���۵Ǿ����Ƿ� isTyping�� true�� ����
					_isTyping = true;

					dialogueIndex++;
				}
			}
		}



		// ��ȭ�� �ڵ����� �Ѿ�� ����
		if (!_isTyping && dialogueIndex < dialogues.Length)
		{
			dialogueTimer += Time.deltaTime;

			if (dialogueTimer >= autoSkipTime)
			{
				StartCoroutine(TypingDialogue(dialogues[dialogueIndex]));
				_isTyping = true;
				dialogueIndex++;
			}
		}
		else
		{
			dialogueTimer = 0f;
		}

		if (isButtonClicked)
		{
			isButtonClicked = false;
		}
	}



	public void GoingChapter5()
	{
		SceneManager.LoadScene("Chapter5");
	}


	IEnumerator TypingDialogue(string text)
	{
		DialogText.text = ""; // ��ȭâ �ʱ�ȭ

		foreach (char letter in text.ToCharArray())
		{
			DialogText.text += letter;

			if (_isSkip)
			{
				// ��ȭ�� ���� ǥ��
				DialogText.text = text;

				// ��ȭ�� �������Ƿ� isTyping�� false�� ����
				_isTyping = false;

				// ��ȭ�� ��ŵ�����Ƿ� isSkip�� false�� ����
				_isSkip = false;

				break;
			}

			yield return new WaitForSeconds(0.04f);
		}

		if (!_isSkip)
		{
			// ��ȭ�� �������Ƿ� isTyping�� false�� ����
			_isTyping = false;
		}
	}
	public void Esc()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}


	public void GoingHome()
	{
		Rightbuttons.SetActive(false);
		StartCoroutine(FadeOutDialogBox());
		Invoke("LoadMainScene", delayBeforeSceneChange);
	}

	public void LoadMainScene()
	{
		// You can replace "YourNextSceneName" with the actual name of your scene
		SceneManager.LoadScene("Main");
	}


	private IEnumerator FadeOutDialogBox()
	{

		float elapsedTime = 0f;
		Color startColor = DiaLogbox.GetComponent<Image>().color; // Access Image component
		Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

		while (elapsedTime < fadeDuration)
		{
			float alpha = Mathf.Lerp(startColor.a, targetColor.a, elapsedTime / fadeDuration);
			DiaLogbox.GetComponent<Image>().color = new Color(startColor.r, startColor.g, startColor.b, alpha);
			DialogText.color = new Color(DialogText.color.r, DialogText.color.g, DialogText.color.b, alpha);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		DiaLogbox.GetComponent<Image>().color = targetColor;
		DialogText.color = targetColor;
	}


	private IEnumerator FadeInDialogBox()
	{
		float elapsedTime = 0f;
		Color startColor = DiaLogbox.GetComponent<Image>().color;
		Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Fully opaque

		while (elapsedTime < fadeDuration)
		{
			float alpha = Mathf.Lerp(startColor.a, targetColor.a, elapsedTime / fadeDuration);
			DiaLogbox.GetComponent<Image>().color = new Color(startColor.r, startColor.g, startColor.b, alpha);
			DialogText.color = new Color(DialogText.color.r, DialogText.color.g, DialogText.color.b, alpha);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		DiaLogbox.GetComponent<Image>().color = targetColor;
		DialogText.color = targetColor;

		_startpadedone = true;
	}

	private IEnumerator FadeOutTargetImage()
	{
		if (TargetImage == null || !TargetImage.gameObject.activeSelf)
		{
			yield break; // Exit coroutine if TargetImage is not active
		}

		float elapsedTime = 0f;
		Color startColor = TargetImage.color;
		Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f); // Fully transparent

		while (elapsedTime < targetImageFadeDuration)
		{
			float alpha = Mathf.Lerp(startColor.a, targetColor.a, elapsedTime / targetImageFadeDuration);
			TargetImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		TargetImage.color = targetColor;

		TargetImage.gameObject.SetActive(false);
	}

	private IEnumerator FadeInTargetImage()
	{ 
		TargetImage.color = Color.white; // �̹��� ������ ������� ����

		if (TargetImage == null || TargetImage.gameObject.activeSelf)
		{
			yield break; // Exit coroutine if TargetImage is already active
		}

		TargetImage.gameObject.SetActive(true); // Activate the TargetImage

		float elapsedTime = 0f;
		Color startColor = new Color(TargetImage.color.r, TargetImage.color.g, TargetImage.color.b, 0f); // Fully transparent
		Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Fully opaque

		while (elapsedTime < targetImageFadeDuration)
		{
			float alpha = Mathf.Lerp(startColor.a, targetColor.a, elapsedTime / targetImageFadeDuration);
			TargetImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		TargetImage.color = targetColor; // Ensure the color is set to the target color at the end

		SceneManager.LoadScene("Chapter3");
	}


	// Call this method when you want to initiate the fade-out for the TargetImage
	public void StartFadeOutTargetImage()
	{
		StartCoroutine(FadeOutTargetImage());

	}


	// Call this method when you want to initiate the fade-in for the Phone image
}

