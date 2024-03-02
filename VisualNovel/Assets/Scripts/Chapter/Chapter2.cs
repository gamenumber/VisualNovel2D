using System.Collections;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86;

public class Chapter2 : MonoBehaviour
{
	
	public GameObject Rightbuttons;
	public GameObject DiaLogbox; // Change Image to GameObject
	public Text DialogText;
	public float fadeDuration = 0.8f;
	public float delayBeforeSceneChange = 0.5f; // Adjust this value to set the delay
	public string Textl;

	public AudioClip Cracksound;


	public Image CrackObject;
	public Image CrackObject2;
	public Image CrackObject3;
	public AudioSource Voice;
	private bool _isSkip = false;  // ��ȭ�� ��ŵ�ߴ��� �ƴ����� Ȯ���ϴ� ����

	public Image Watch;

	public Image Phone;
	public Image Phone2;
	public Image Phone3;
	public float phoneFadeDuration = 1.5f;

	public string[] dialogues; // ��ȭ ������ ������ �迭
	public int dialogueIndex = 0; // ���� ��ȭ�� �ε����� ������ ����

	public Text CharacterName;

	public float targetImageFadeDuration = 1.5f;

	public AudioClip mistary;
	public AudioClip clock;
	public AudioClip BBYm;
	


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


	private void Start()
	{
		audioSource.Play();
		StartCoroutine(FadeOutTargetImage());
		StartCoroutine(FadeInDialogBox());
		string playerName = PlayerPrefs.GetString("PlayerName", "DefaultPlayerName");
		CharacterName.text = playerName;



	}



	void Update()
	{
		// ���콺 ���� ��ư Ŭ�� �̺�Ʈ ó��
		if (Input.GetMouseButtonDown(0))
		{
			if (dialogueIndex == 0)
			{
				audioSource.Stop();

				Phone.gameObject.SetActive(false);
			}

			if (dialogueIndex == 4)
			{
				
				Watch.gameObject.SetActive(true);
			}

			if (dialogueIndex == 5)
			{

				audioSource.clip = clock;
				audioSource.Play();
			}
			

			if (dialogueIndex == 6)
			{
				Watch.gameObject.SetActive(false);
				Phone2.gameObject.SetActive(true);
			}

			if (dialogueIndex == 9)
			{
				Phone2.gameObject.SetActive(false);
				audioSource.clip = mistary;
				audioSource.loop = true;
				audioSource.Play();
			}

			if (dialogueIndex == 12)
			{
				Watch.gameObject.gameObject.SetActive(true);
				audioSource.clip = clock;
				audioSource.loop = false;
				audioSource.Play();
			}

			if (dialogueIndex == 13)
			{
				Watch.gameObject.gameObject.SetActive(false);
				Phone3.gameObject.SetActive(true);
			}

			if (dialogueIndex == 15)
			{
				Watch.gameObject.gameObject.SetActive(false);
				Phone3.gameObject.SetActive(false);
				audioSource.clip = mistary;
				audioSource.loop = true;
				audioSource.Play();
			}

			if (dialogueIndex == 28)
			{
				audioSource.clip = BBYm;
				audioSource.loop = false;
				audioSource.Play();


				

			}

			if (dialogueIndex == 29)
			{

				Watch.gameObject.SetActive(true);
				




			}


			if (dialogueIndex == 30)
			{

				Watch.gameObject.SetActive(true);

				audioSource.clip = mistary;
				audioSource.loop = true;
				audioSource.Play();



			}

			if (dialogueIndex == 33)
			{


				StartCoroutine(FadeInTargetImage());


			}

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

		yield return new WaitForSeconds(2);

		
		CrackObject.gameObject.SetActive(true);

		yield return new WaitForSeconds(0.3f);

		CrackObject2.gameObject.SetActive(true);

		yield return new WaitForSeconds(0.3f);

		CrackObject3.gameObject.SetActive(true);
		Voice.clip = Cracksound;
		Voice.Play();

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
		LoveGuageSystem.SaveLoveGuage("Hana", 60);
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

	// Call this method when you want to initiate the fade-out for the TargetImage
	public void StartFadeOutTargetImage()
	{
		StartCoroutine(FadeOutTargetImage());

	}

	private IEnumerator FadeInPhone()
	{
		Phone.gameObject.SetActive(true);

		float elapsedTime = 0f;
		Color startColor = Phone.color;
		Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Fully opaque

		while (elapsedTime < phoneFadeDuration)
		{
			float alpha = Mathf.Lerp(startColor.a, targetColor.a, elapsedTime / phoneFadeDuration);
			Phone.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		Phone.color = targetColor;
	}

	// Call this method when you want to initiate the fade-in for the Phone image
	public void StartFadeInPhone()
	{
		StartCoroutine(FadeInPhone());
	}

}

