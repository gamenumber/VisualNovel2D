using JetBrains.Annotations;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Chapter3 : MonoBehaviour
{
	public AudioSource soundeffect;

	public GameObject Radio;

	public GameObject flower;
	public GameObject flowerx;

	public GameObject Star;
	public GameObject Undergroundbutton;
	public GameObject Underground;

	public GameObject Upbutton;
	public Text floor;
	public GameObject rrrrr;
	public GameObject rrrrr_2;

	public GameObject rrrrrisdie;
	public Image White;

	public GameObject lrrlr;
	public GameObject lrrlr_2;
	public GameObject lrrlr_3;
	public GameObject lrrlr_4;
	public GameObject lrrlrisdie;

	public GameObject Rightbuttons;
	public GameObject DiaLogbox; // Change Image to GameObject
	public Text DialogText;
	public float fadeDuration = 0.8f;
	public float delayBeforeSceneChange = 0.5f; // Adjust this value to set the delay

	public GameObject button1;

	public AudioSource Voice;
	private bool _isSkip = false;  // 대화를 스킵했는지 아닌지를 확인하는 변수

	public Image Watch;

	public Image Hana;
	public Image Hana2;
	public Image Hana3;
	public Image Hana4;

	public Image Hana_2;
	public Image Hana_3;

	public AudioClip[] HanaVoice;
	public AudioClip[] InBuilding;
	public AudioClip[] Soundeffect;

	public AudioClip clock;

	public Image Background2;
	public Image Background3;

	public float fadeInTime = 2.0f;


	public float phoneFadeDuration = 1.5f;

	public string[] dialogues; // 대화 내용을 저장할 배열
	public int dialogueIndex = 0; // 현재 대화의 인덱스를 저장할 변수

	public Text CharacterName;

	public float targetImageFadeDuration = 1.5f;


	public AudioSource audioSource;


	public Image TargetImage;
	public Image TargetImage2;

	private bool _startpadedone = false;

	private bool _isTyping = false; // 대화가 진행중인지 아닌지를 확인하는 변수

	
	private int userChoice = -1;  // 사용자의 선택을 저장하는 변수

	public GameObject Select1;

	public bool isButtonClicked = false;

	// 대화 길이 체크를 위한 변수
	float dialogueTimer = 0f;
	// 대화가 자동으로 넘어가는 시간
	float autoSkipTime = 20f;

	// 캐릭터의 이름과 호감도 증가량
	public string characterName = "Hana";
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



	IEnumerator FadeImageOut()
	{
		White.gameObject.SetActive(true);
		Color color = White.color;
		while (color.a < 1f)
		{
			color.a += Time.deltaTime; // Time.deltaTime를 더하는 것은 초당 페이드아웃 속도를 조절합니다.
			White.color = color;
			yield return null;
		}
	}
	public void OnClickGoingdown()
	{
		// 카메라 쉐이크 스크립트가 부착된 게임 오브젝트
		CameraShake cameraShake = GetComponent<CameraShake>();

		// 이벤트 발생 시, 카메라 흔들기 시작
		StartCoroutine(cameraShake.Shake(0.5f, 0.4f));
		soundeffect.clip = Soundeffect[0];
		soundeffect.Play();

		Invoke("isvisible", 2.5f);
		Invoke("Wait",7f);
		Voice.clip = InBuilding[9];
		Voice.Play();
		Invoke("moremoremoretalk", 4f);
	

	}

	public void Wait()
	{
		flower.gameObject.SetActive(true);
		Invoke("Wait2", 2.5f);
	}

	public void Wait2()
	{
		flowerx.gameObject.SetActive(true);
	}

	public void Flowerxtouched()
	{
		flowerx.gameObject.SetActive(false);
		soundeffect.clip = Soundeffect[1];
		soundeffect.Play();

		Invoke("Backtohome", 2f);
	}

	public void Backtohome()
	{
		soundeffect.clip = Soundeffect[2];
		soundeffect.Play();
		audioSource.Stop();

		StartCoroutine(FadeImageOut());

		Voice.clip = InBuilding[11];
		Voice.Play();

		Invoke("mtalk", 5f);
	}

	public void mtalk()
	{
		Voice.clip = InBuilding[12];
		Voice.Play();

		Invoke("GoingChapter4", 5f);
	}

	public void GoingChapter4()
	{
		SceneManager.LoadScene("Chapter4");
	}


	public void moremoremoretalk()
	{
		Voice.clip = InBuilding[8];
		Voice.Play();
		
	}

	public void isvisible()
	{
		Underground.gameObject.SetActive(true);
		
	}
	public void lrrlr1()
	{
		Voice.clip = InBuilding[1];
		Voice.Play();
		Fadeincall();
		floor.text = "7층";
		lrrlr.gameObject.SetActive(false);
		lrrlr_2.gameObject.SetActive(true);
		lrrlrisdie.gameObject.SetActive(true);
		rrrrrisdie.gameObject.SetActive(false);
		rrrrr.gameObject.SetActive(true);
	}

	public void lrrlr2()
	{
		Fadeincall();
		floor.text = "15층";
		Voice.clip = InBuilding[6];
		Voice.Play();
		rrrrr_2.gameObject.SetActive(true);
		lrrlr_2.gameObject.SetActive(false);
		lrrlr_3.gameObject.SetActive(true);
		lrrlrisdie.gameObject.SetActive(false);
		rrrrrisdie.gameObject.SetActive(true);
		rrrrr.gameObject.SetActive(true);
	}

	public void lrrlr3()
	{
		Fadeincall();
		floor.text = "23층";
		Voice.clip = InBuilding[7];
		Voice.Play();
		lrrlr_3.gameObject.SetActive(false);
		lrrlr_4.gameObject.SetActive(true);
		lrrlrisdie.gameObject.SetActive(true);
		rrrrrisdie.gameObject.SetActive(false);
		rrrrr.gameObject.SetActive(true);
	}

	public void lrrlr4()
	{
		Fadeincall();
		floor.text = "32층";
		Voice.clip = InBuilding[10];
		Voice.Play();
		lrrlr_4.gameObject.SetActive(false);
		// Additional logic for the fourth floor if needed
		rrrrrisdie.gameObject.SetActive(false);
		rrrrr_2.gameObject.SetActive(false);
		// Additional logic for hiding other game objects if needed

		StartCoroutine(startvisible());
	}

	IEnumerator startvisible()
	{
		yield return new WaitForSeconds(3f);
		Star.gameObject.SetActive(true);
	

	}

	public void OnClickStar()
	{
		Star.gameObject.SetActive(false);
		StartCoroutine(startvisible2());
	}

	IEnumerator startvisible2()
	{
		yield return new WaitForSeconds(3f);
		Undergroundbutton.gameObject.SetActive(true);
		

	}



	public void rrrrr1()
	{
		Fadeincall();
		floor.text = "11층";
		Voice.clip = InBuilding[5];
		Voice.Play();

		lrrlrisdie.gameObject.SetActive(false);	
		rrrrr.gameObject.SetActive(false);
		rrrrrisdie.gameObject.SetActive(true);
		
	}

	public void rrrrr2()
	{
		Fadeincall();
		floor.text = "28층";
		Voice.clip = InBuilding[6];
		Voice.Play();
		lrrlrisdie.gameObject.SetActive(false);
		rrrrr.gameObject.SetActive(false);
		rrrrrisdie.gameObject.SetActive(true);

	}

	public void GoingUP()
	{
		Fadeincall();
		Upbutton.gameObject.SetActive(false);
		Voice.clip = InBuilding[3];
		Voice.Play();
		floor.text = "4층";

		Invoke("moretalk", 6f);

		button1.gameObject.SetActive(false);
		lrrlr.gameObject.SetActive(true);
		lrrlrisdie.gameObject.SetActive(false);
		rrrrrisdie.gameObject.SetActive(true);
		rrrrr.gameObject.SetActive(false);
	}

	public void moretalk()
	{
		Voice.clip = InBuilding[2];
		Voice.Play();
		Invoke("moremoretalk", 7f); 
	}

	public void moremoretalk()
	{
		Voice.clip = InBuilding[4];
		Voice.Play();
	}

	public void Isdie()
	{
		Voice.clip = clock;
		Voice.Play();
		Fadeincall();
		Invoke("Chapter3back", 1.5f);

	}


	public void buttonon1()
	{
		GoingUP();
	}


	void Chapter3back()
	{
		SceneManager.LoadScene("Chapter3");
	}

	public void GoingBuilding()
	{
		Fadeincall();
		StartCoroutine(FadeOutDialogBox());
		Background2.gameObject.SetActive(false);
		Background3.gameObject.SetActive(true);
		button1.gameObject.SetActive(false);
		DiaLogbox.gameObject.SetActive(false);
		DialogText.gameObject.SetActive(false);

		Voice.clip = InBuilding[0];
		Voice.Play();

		Upbutton.gameObject.SetActive(true);
		floor.gameObject.SetActive(true);

	}
	IEnumerator Fadeout()
	{
		
		Color color = TargetImage2.color;
		while (color.a > 0f)
		{
			color.a -= Time.deltaTime;
			TargetImage2.color = color;

			if (color.a <= 0f)
				color.a = 0f;

			yield return null;
		}
		TargetImage2.gameObject.SetActive(false);
		

	}

	IEnumerator Fadein()
	{
		TargetImage2.gameObject.SetActive(true);
		Color color = TargetImage2.color;
		while (color.a < 1f)
		{
			color.a += Time.deltaTime;
			TargetImage2.color = color;

			if (color.a >= 1f)
				color.a = 1f;

			yield return null;
		}

		yield return new WaitForSeconds(1.5f);
		StartCoroutine(Fadeout());
	}

	public void Fadeincall()
	{
		StartCoroutine(Fadein());
	}

	private void Start()
	{
		audioSource.Play();
		StartCoroutine(FadeOutTargetImage());
		StartCoroutine(FadeInDialogBox());
		string playerName = PlayerPrefs.GetString("PlayerName", "DefaultPlayerName");
		CharacterName.text = playerName;
		IncreaseLoveGuage();
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

	public void GoingMain() 
	{
		StartCoroutine(FadeOutDialogBox());
		Rightbuttons.gameObject.SetActive(false);
		SceneManager.LoadScene("Main");
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

	IEnumerator Talk()
	{
		if (dialogueIndex == 4)
		{

			StartCoroutine(FadeInTargetImage());
		}

		if (dialogueIndex == 5)
		{
			Hana4.gameObject.SetActive(false);
			StartCoroutine(FadeInTargetImage_3());
			CharacterName.text = "하나";
			CharacterName.color = new Color(1, 0.75f, 0.8f); // RGB 값으로 분홍
			Voice.clip = HanaVoice[0];
			Voice.Play();

		}

		if (dialogueIndex == 6)
		{
			Voice.clip = HanaVoice[1];
			Voice.Play();

		}

		if (dialogueIndex == 7)
		{
			Voice.clip = HanaVoice[2];
			Voice.Play();

		}

		if (dialogueIndex == 8)
		{


			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "호성");
			CharacterName.text = playerName;
		}

		if (dialogueIndex == 9)
		{
			Hana_3.gameObject.SetActive(false);
			Hana4.gameObject.SetActive(true);
			CharacterName.text = "하나";
			CharacterName.color = new Color(1, 0.75f, 0.8f); // RGB 값으로 분홍
			Voice.clip = HanaVoice[3];
			Voice.Play();

		}

		if (dialogueIndex == 10)
		{
			
			Voice.clip = HanaVoice[4];
			Voice.Play();

		}
		if (dialogueIndex == 11)
		{

			Voice.clip = HanaVoice[5];
			Voice.Play();

		}
		if (dialogueIndex == 12)
		{

			Voice.clip = HanaVoice[6];
			Voice.Play();

		}


		if (dialogueIndex == 13)
		{
			CharacterName.text = "하나";
			CharacterName.color = new Color(1, 0.75f, 0.8f); // RGB 값으로 분홍
			Voice.clip = HanaVoice[7];
			Voice.Play();

		}

		if (dialogueIndex == 14)
		{
			Voice.clip = HanaVoice[8];
			Voice.Play();

		}

		if (dialogueIndex == 15)
		{


			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "호성");
			CharacterName.text = playerName;
		}

		if (dialogueIndex == 16)
		{
			CharacterName.text = "하나";
			CharacterName.color = new Color(1, 0.75f, 0.8f); // RGB 값으로 분홍
			Voice.clip = HanaVoice[9];
			Voice.Play();

		}

		if (dialogueIndex == 17)
		{
			Hana4.gameObject.SetActive(false);
			StartCoroutine(FadeInTargetImage_2());
			Voice.clip = HanaVoice[10];
			Voice.Play();

		}

		if (dialogueIndex == 18)
		{

			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "호성");
			CharacterName.text = playerName;

		}


		if (dialogueIndex == 19)
		{
			Hana4.gameObject.SetActive(true);
			Hana_2.gameObject.SetActive(false);
			CharacterName.text = "하나";
			CharacterName.color = new Color(1, 0.75f, 0.8f); // RGB 값으로 분홍
			Voice.clip = HanaVoice[11];
			Voice.Play();

		}

		if (dialogueIndex == 20)
		{

			Voice.clip = HanaVoice[12];
			Voice.Play();

		}

		if (dialogueIndex == 21)
		{

			Voice.clip = HanaVoice[13];
			Voice.Play();

		}


		if (dialogueIndex == 22)
		{
			Voice.clip = HanaVoice[14];
			Voice.Play();


		}

		if (dialogueIndex == 23)
		{

			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "호성");
			CharacterName.text = playerName;

		}

		if (dialogueIndex == 24)
		{
			CharacterName.text = "하나";
			CharacterName.color = new Color(1, 0.75f, 0.8f); // RGB 값으로 분홍
			Voice.clip = HanaVoice[15];
			Voice.Play();

		}

		if (dialogueIndex == 25)
		{

			CharacterName.text = "하나";
			CharacterName.color = new Color(1, 0.75f, 0.8f); // RGB 값으로 분홍
			Voice.clip = HanaVoice[16];
			Voice.Play();

		}


		if (dialogueIndex == 26)
		{

			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "호성");
			CharacterName.text = playerName;
		}

		if (dialogueIndex == 27)
		{
			CharacterName.text = "하나";
			CharacterName.color = new Color(1, 0.75f, 0.8f); // RGB 값으로 분홍
			Voice.clip = HanaVoice[17];
			Voice.Play();



		}

		if (dialogueIndex == 28)
		{

			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "호성");
			CharacterName.text = playerName;


		}

		if (dialogueIndex == 29)
		{
			CharacterName.text = "하나";
			CharacterName.color = new Color(1, 0.75f, 0.8f); // RGB 값으로 분홍
			Voice.clip = HanaVoice[18];
			Voice.Play();

		}

		if (dialogueIndex == 30)
		{
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "호성");
			CharacterName.text = playerName;

		}

	



		if (dialogueIndex == 32)
		{
			CharacterName.text = "하나";
			CharacterName.color = new Color(1, 0.75f, 0.8f); // RGB 값으로 분홍
			Voice.clip = HanaVoice[19];
			Voice.Play();


		}

		if (dialogueIndex == 33)
		{
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "호성");
			CharacterName.text = playerName;


		}


		if (dialogueIndex == 36)
		{

			CharacterName.text = "하나";
			CharacterName.color = new Color(1, 0.75f, 0.8f); // RGB 값으로 분홍
			Fadeincall();
			Voice.clip = HanaVoice[20];
			Voice.Play();
			Background2.gameObject.SetActive(true);
			

		}

		if (dialogueIndex == 37)
		{
			
			
			Voice.clip = HanaVoice[21];
			Voice.Play();

		}

		if (dialogueIndex == 38)
		{


			Voice.clip = HanaVoice[22];
			Voice.Play();

		}

		if (dialogueIndex == 39)
		{


			Voice.clip = HanaVoice[23];
			Voice.Play();

			yield return new WaitForSeconds(3f);
			Radio.gameObject.SetActive(true);

			yield return new WaitForSeconds(3f);
			Radio.gameObject.SetActive(false);

		}

		if (dialogueIndex == 40)
		{
			

			Voice.clip = HanaVoice[24];
			Voice.Play();

		}

		if (dialogueIndex == 41)
		{

			Voice.clip = HanaVoice[25];
			Voice.Play();
			yield return new WaitForSeconds(3f);
			button1.gameObject.SetActive(true);
		}



		yield return null;
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
		DiaLogbox.gameObject.SetActive(true);	
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

	private IEnumerator FadeInTargetImage_2()
	{
		Hana_2.color = Color.white; // 이미지 색상을 흰색으로 변경

		if (TargetImage == null || Hana_2.gameObject.activeSelf)
		{
			yield break; // Exit coroutine if TargetImage is already active
		}

		Hana_2.gameObject.SetActive(true); // Activate the TargetImage

		float elapsedTime = 0f;
		Color startColor = new Color(Hana_2.color.r, Hana_2.color.g, Hana_2.color.b, 0f); // Fully transparent
		Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Fully opaque

		while (elapsedTime < targetImageFadeDuration)
		{
			float alpha = Mathf.Lerp(startColor.a, targetColor.a, elapsedTime / targetImageFadeDuration);
			Hana_2.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		Hana2.color = targetColor; // Ensure the color is set to the target color at the end

	}

	private IEnumerator FadeInTargetImage_3()
	{
		Hana_3.color = Color.white; // 이미지 색상을 흰색으로 변경

		if (TargetImage == null || Hana_3.gameObject.activeSelf)
		{
			yield break; // Exit coroutine if TargetImage is already active
		}

		Hana_3.gameObject.SetActive(true); // Activate the TargetImage

		float elapsedTime = 0f;
		Color startColor = new Color(Hana_3.color.r, Hana_3.color.g, Hana_3.color.b, 0f); // Fully transparent
		Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Fully opaque

		while (elapsedTime < targetImageFadeDuration)
		{
			float alpha = Mathf.Lerp(startColor.a, targetColor.a, elapsedTime / targetImageFadeDuration);
			Hana_3.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		Hana_3.color = targetColor; // Ensure the color is set to the target color at the end

	}






	private IEnumerator FadeInTargetImage()
	{ 
		Hana.color = Color.white; // 이미지 색상을 흰색으로 변경

		if (TargetImage == null || Hana.gameObject.activeSelf)
		{
			yield break; // Exit coroutine if TargetImage is already active
		}

		Hana.gameObject.SetActive(true); // Activate the TargetImage

		float elapsedTime = 0f;
		Color startColor = new Color(Hana.color.r, Hana.color.g, Hana.color.b, 0f); // Fully transparent
		Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Fully opaque

		while (elapsedTime < targetImageFadeDuration)
		{
			float alpha = Mathf.Lerp(startColor.a, targetColor.a, elapsedTime / targetImageFadeDuration);
			Hana.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		Hana.color = targetColor; // Ensure the color is set to the target color at the end

		StartCoroutine(FadeInTargetImage2());
	}

	private IEnumerator FadeInTargetImage2()
	{

		yield return new WaitForSeconds(0.5f);
		Hana2.color = Color.white; // 이미지 색상을 흰색으로 변경

		if (TargetImage == null || Hana2.gameObject.activeSelf)
		{
			yield break; // Exit coroutine if TargetImage is already active
		}

		Hana2.gameObject.SetActive(true); // Activate the TargetImage

		float elapsedTime = 0f;
		Color startColor = new Color(Hana2.color.r, Hana2.color.g, Hana2.color.b, 0f); // Fully transparent
		Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Fully opaque

		while (elapsedTime < targetImageFadeDuration)
		{
			float alpha = Mathf.Lerp(startColor.a, targetColor.a, elapsedTime / targetImageFadeDuration);
			Hana2.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		Hana2.color = targetColor; // Ensure the color is set to the target color at the end

		StartCoroutine(FadeInTargetImage3());
	}

	private IEnumerator FadeInTargetImage3()
	{

		yield return new WaitForSeconds(0.5f);
		Hana3.color = Color.white; // 이미지 색상을 흰색으로 변경

		if (TargetImage == null || Hana3.gameObject.activeSelf)
		{
			yield break; // Exit coroutine if TargetImage is already active
		}

		Hana3.gameObject.SetActive(true); // Activate the TargetImage

		float elapsedTime = 0f;
		Color startColor = new Color(Hana3.color.r, Hana3.color.g, Hana3.color.b, 0f); // Fully transparent
		Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Fully opaque

		while (elapsedTime < targetImageFadeDuration)
		{
			float alpha = Mathf.Lerp(startColor.a, targetColor.a, elapsedTime / targetImageFadeDuration);
			Hana3.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		Hana3.color = targetColor; // Ensure the color is set to the target color at the end

		StartCoroutine(FadeInTargetImage4());

		Hana.gameObject.SetActive(false);
		Hana2.gameObject.SetActive(false);
		Hana3.gameObject.SetActive(false);
	}

	private IEnumerator FadeInTargetImage4()
	{
		Hana4.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		Hana4.color = Color.white; // 이미지 색상을 흰색으로 변경

		if (TargetImage == null || Hana4.gameObject.activeSelf)
		{
			yield break; // Exit coroutine if TargetImage is already active
		}

		Hana4.gameObject.SetActive(true); // Activate the TargetImage

		float elapsedTime = 0f;
		Color startColor = new Color(Hana4.color.r, Hana4.color.g, Hana4.color.b, 0f); // Fully transparent
		Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Fully opaque

		while (elapsedTime < targetImageFadeDuration)
		{
			float alpha = Mathf.Lerp(startColor.a, targetColor.a, elapsedTime / targetImageFadeDuration);
			Hana3.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		Hana4.color = targetColor; // Ensure the color is set to the target color at the end


	}


	// Call this method when you want to initiate the fade-out for the TargetImage
	public void StartFadeOutTargetImage()
	{
		StartCoroutine(FadeOutTargetImage());

	}


	// Call this method when you want to initiate the fade-in for the Phone image
}

