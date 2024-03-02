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
	private bool _isSkip = false;  // 대화를 스킵했는지 아닌지를 확인하는 변수

	public Image Watch;

	public float fadeTime = 1f; // 페이드 아웃동안 걸리는 시간, 1초로 설정


	public Image Dark;
	
	public float phoneFadeDuration = 1.5f;

	public string[] dialogues; // 대화 내용을 저장할 배열
	public int dialogueIndex = 0; // 현재 대화의 인덱스를 저장할 변수

	public Text CharacterName;

	public float targetImageFadeDuration = 1.5f;


	public AudioSource audioSource;


	public Image TargetImage;

	private bool _startpadedone = false;

	private bool _isTyping = false; // 대화가 진행중인지 아닌지를 확인하는 변수

	
	private int userChoice = -1;  // 사용자의 선택을 저장하는 변수


	public bool isButtonClicked = false;

	// 대화 길이 체크를 위한 변수
	float dialogueTimer = 0f;
	// 대화가 자동으로 넘어가는 시간
	float autoSkipTime = 10f;

	public string characterName = "Seona";
	public float loveGuageIncreaseAmount = 60f;


	private static string savePath;
	public void IncreaseLoveGuage()
	{
		// savePath 초기화
		if (string.IsNullOrEmpty(savePath))
		{
			savePath = Path.Combine(Application.persistentDataPath, "LoveGuageData.json");
		}

		// 기존의 호감도를 불러옵니다.
		float currentLoveGuage = LoveGuageSystem.LoadLoveGuage(characterName);

		// 호감도가 저장된 적이 없다면 기본값을 사용합니다.
		if (currentLoveGuage == -1)
		{
			currentLoveGuage = 0f;
		}

		// 호감도를 증가시킵니다.
		float newLoveGuage = currentLoveGuage + loveGuageIncreaseAmount;

		// 새로운 호감도를 저장합니다.
		LoveGuageSystem.SaveLoveGuage(characterName, newLoveGuage);
	}


	IEnumerator FadeImage()
	{
		Dark.gameObject.SetActive(true);
		for (float i = 0; i <= fadeTime; i += Time.deltaTime)
		{
			float fadeAmount = i / fadeTime; // 0에서 1로 가도록 수정
			Dark.color = new Color(Dark.color.r, Dark.color.g, Dark.color.b, fadeAmount); // 알파값을 fadeAmount로 설정
			yield return null;
		}
		Dark.color = new Color(Dark.color.r, Dark.color.g, Dark.color.b, 1); // 페이드 아웃이 끝난 후에는 알파값을 1로 유지
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
		// 호감도 게이지의 키 이름을 가져옵니다.
		string prefKey = characterName + "LoveGuage";
		// 현재 값을 불러옵니다.
		float currentGuage = PlayerPrefs.GetFloat(prefKey, 0);
		// 새로운 값으로 업데이트합니다.
		float newGuage = Mathf.Clamp(currentGuage + value, 0, 100);
		// 변경된 값을 저장합니다.
		PlayerPrefs.SetFloat(prefKey, newGuage);
		// 변경 사항을 저장합니다.
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
			string playerName = PlayerPrefs.GetString("PlayerName", "호성");
			CharacterName.text = playerName;
			
		}


		if (dialogueIndex == 12)
		{
			
			CharacterName.text = "선아";
			CharacterName.color = new Color(0, 0, 0);

		}

		if (dialogueIndex == 13)
		{
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "호성");
			CharacterName.text = playerName;

		}

		if (dialogueIndex == 14)
		{
			CharacterName.text = "선아";
			CharacterName.color = new Color(0, 0, 0);
		}

		if (dialogueIndex == 15)
		{
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "호성");
			CharacterName.text = playerName;
		}

		if (dialogueIndex == 16)
		{
			CharacterName.text = "선아";
			CharacterName.color = new Color(0, 0, 0);
		}


		if (dialogueIndex == 17)
		{
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "호성");
			CharacterName.text = playerName;
		}

		if (dialogueIndex == 19)
		{
			CharacterName.text = "선아";
			CharacterName.color = new Color(0, 0, 0);
		}


		if (dialogueIndex == 21)
		{
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "호성");
			CharacterName.text = playerName;
		}

		if (dialogueIndex == 22)
		{
			CharacterName.text = "선아";
			CharacterName.color = new Color(0, 0, 0);
		}

		

		if (dialogueIndex == 24)
		{
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "호성");
			CharacterName.text = playerName;

		}

		if (dialogueIndex == 25)
		{
			CharacterName.text = "선아";
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
			string playerName = PlayerPrefs.GetString("PlayerName", "호성");
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
		// 마우스 왼쪽 버튼 클릭 이벤트 처리
		if (Input.GetMouseButtonDown(0))
		{
			StartCoroutine(Talk());

			// 대화가 진행중이라면
			if (_isTyping)
			{
				// 대화를 스킵했으므로 isSkip을 true로 설정
				_isSkip = true;
			}
			else
			{
				// 대화가 진행중이 아니라면
				if (dialogueIndex < dialogues.Length)
				{
					// Start typing the dialogue
					StartCoroutine(TypingDialogue(dialogues[dialogueIndex]));

					// 대화가 시작되었으므로 isTyping을 true로 설정
					_isTyping = true;

					dialogueIndex++;
				}
			}
		}



		// 대화가 자동으로 넘어가는 로직
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
		DialogText.text = ""; // 대화창 초기화

		foreach (char letter in text.ToCharArray())
		{
			DialogText.text += letter;

			if (_isSkip)
			{
				// 대화를 전부 표시
				DialogText.text = text;

				// 대화가 끝났으므로 isTyping을 false로 설정
				_isTyping = false;

				// 대화를 스킵했으므로 isSkip을 false로 설정
				_isSkip = false;

				break;
			}

			yield return new WaitForSeconds(0.04f);
		}

		if (!_isSkip)
		{
			// 대화가 끝났으므로 isTyping을 false로 설정
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
		TargetImage.color = Color.white; // 이미지 색상을 흰색으로 변경

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

