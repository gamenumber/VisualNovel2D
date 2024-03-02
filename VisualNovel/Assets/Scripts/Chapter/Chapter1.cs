using System.Collections;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86;

public class Chapter1 : MonoBehaviour
{
	public Image Background2;
	public Image Background3;

	public Image Background4;
	public Image Background5;
	public Image Background6;

    public GameObject Rightbuttons;
    public GameObject DiaLogbox; // Change Image to GameObject
    public Text DialogText;
    public float fadeDuration = 0.8f;
    public float delayBeforeSceneChange = 0.5f; // Adjust this value to set the delay
    public string Textl;

	public AudioSource Voice;
	private bool _isSkip = false;  // ��ȭ�� ��ŵ�ߴ��� �ƴ����� Ȯ���ϴ� ����

	public Image Phone;
	public float phoneFadeDuration = 1.5f;

	public string[] dialogues; // ��ȭ ������ ������ �迭
	public int dialogueIndex = 0; // ���� ��ȭ�� �ε����� ������ ����

	public Text CharacterName;

    public float targetImageFadeDuration = 1.5f;

	public GameObject clock2;
	public GameObject paper;

    public AudioClip run;
    public AudioClip surprise;
    public AudioClip clock;
	public AudioClip classroom;
    private AudioSource audioSource;

	public AudioClip Teayoon1;
	public AudioClip Teayoon2;
	public AudioClip Teayoon3;
	public AudioClip Teayoon4;
	public AudioClip Teayoon5;

	public Image Taeyoon;
	public Image Taeyoon_2;
	public GameObject Humuns;

	public Image TargetImage;

    private bool _startpadedone = false;

	public Text lunchtime;

	private bool _isTyping = false; // ��ȭ�� ���������� �ƴ����� Ȯ���ϴ� ����

	public Button[] choiceButtons;
	private int userChoice = -1;  // ������� ������ �����ϴ� ����

	public Button yourButton;
	public bool isButtonClicked = false;

	// ��ȭ ���� üũ�� ���� ����
	float dialogueTimer = 0f;
	// ��ȭ�� �ڵ����� �Ѿ�� �ð�
	float autoSkipTime = 10f;


	private void Start()
	{
		// Find the AudioContainer GameObject and get its AudioSource component
		audioSource = GameObject.Find("Backgroundmusic").GetComponent<AudioSource>();

		if (audioSource == null)
		{
			Debug.LogError("AudioSource component not found. Please make sure it is attached to the GameObject or its children.");
		}
		else
		{
			StartCoroutine(FadeOutMusicAndChange());
		}

		string playerName = PlayerPrefs.GetString("PlayerName", "DefaultPlayerName");
		CharacterName.text = playerName;

		for (int i = 0; i < choiceButtons.Length; i++)
		{
			Button btn = choiceButtons[i].GetComponent<Button>();
			btn.onClick.AddListener(OnClick);
		}
	}

	void OnClick()
	{
		isButtonClicked = true;
	}

	IEnumerator FadeTextToFullAlpha(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a < 1.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
			yield return null;
		}
	}

	IEnumerator FadeTextToZeroAlpha(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		while (i.color.a > 0.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			yield return null;
		}
	}
	void Update()
	{
		// ���콺 ���� ��ư Ŭ�� �̺�Ʈ ó��
		if (Input.GetMouseButtonDown(0) && dialogueIndex != 13)
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
		if (dialogueIndex != 13 && !_isTyping && dialogueIndex < dialogues.Length)
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




	public IEnumerator Talk()
	{
		if (dialogueIndex == 2)
		{
			StartFadeInPhone();
			audioSource.Stop();  // ���� ������� ����
			audioSource.clip = surprise;  // ���ο� ����� Ŭ������ ��ü
			audioSource.Play();  // ����� ���
			audioSource.loop = false;

		}

		if (dialogueIndex == 3)
		{
			audioSource.loop = true;
			paper.SetActive(true);
			// Stop the current audio source
			audioSource.Stop();

			// Assign the new audio clip to the audio source
			audioSource.clip = clock;
			// Start playing the new audio clip
			audioSource.Play();
		}

		if (dialogueIndex == 5)
		{
			audioSource.Stop();  // ���� ������� ����

			Phone.gameObject.SetActive(false);
			paper.SetActive(false);
		}

		if (dialogueIndex == 6)
		{
			clock2.gameObject.SetActive(true);
		}


		if (dialogueIndex == 7)
		{

			clock2.gameObject.SetActive(false);
			audioSource.Stop();
			audioSource.clip = run;
			audioSource.loop = false;
			audioSource.Play();

			StartCoroutine(FadeInTargetImage());

			Background2.gameObject.SetActive(true);
			yield return new WaitForSeconds(0.45f); // 1�� ������

			Background2.gameObject.SetActive(false);
			StartCoroutine(FadeOutTargetImage());
			
			yield return new WaitForSeconds(0.45f);
			

			Background3.gameObject.SetActive(true);

			Rightbuttons.gameObject.SetActive(false);
		}


		if (dialogueIndex == 8)
		{
			audioSource.Stop();
			audioSource.clip = classroom;
			audioSource.loop = true;
			audioSource.Play();

			StartCoroutine(FadeInDialogBox());
			Rightbuttons.gameObject.SetActive(true);
		}

		if (dialogueIndex == 9)
		{
			Humuns.gameObject.SetActive(true);
			CharacterName.text = "ģ����";
			string playerName = PlayerPrefs.GetString("PlayerName", "ȣ��");
			// playerName�� ��翡 �����ϱ�
			dialogues[9] = string.Format("{0}(��)��, �� ���� �� �˷����~", playerName);

			
			
		}

		if (dialogueIndex == 10)
		{

			
			
			StartCoroutine(FadeInTargetImage());
			Humuns.gameObject.SetActive(false);
	
		}

		if (dialogueIndex == 11)
		{
			// RGBA ���� ����Ͽ� ��Ȳ������ ����
			CharacterName.color = new Color(1, 0.5f, 0, 1);
			Taeyoon.gameObject.SetActive(true);
			CharacterName.text = "����(�Ҳ�ģ��)";
			StartCoroutine(FadeOutTargetImage());

			
			Voice.clip = Teayoon1;
			Voice.Play();
		}


		if (dialogueIndex == 12)
		{
			// RGBA ���� ����Ͽ� ��Ȳ������ ����
			
			Taeyoon.gameObject.SetActive(true);
			StartCoroutine(FadeOutTargetImage());

			choiceButtons[0].gameObject.SetActive(true);
			choiceButtons[1].gameObject.SetActive(true);

			Voice.clip = Teayoon2;
			Voice.Play();
		}


		if (dialogueIndex == 14)
		{ 

			Voice.clip = Teayoon4;
			Voice.Play();
		}

		if (dialogueIndex == 15)
		{
			// RGBA ���� ����Ͽ� �Ķ������� ����
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "ȣ��");
			CharacterName.text = playerName;
			
		}

		if (dialogueIndex == 16)
		{
			CharacterName.color = new Color(1, 0.5f, 0, 1);
			CharacterName.text = "����";
			Voice.clip = Teayoon5;
			Voice.Play();

			yield return new WaitForSeconds(6f);

			StartCoroutine (FadeInTargetImage());

		
			Background2.gameObject.SetActive(false);
			Background3.gameObject.SetActive(false);
			Background4.gameObject.SetActive(true);
			Taeyoon.gameObject.SetActive(false);
			Taeyoon_2.gameObject.SetActive(false);

			yield return new WaitForSeconds(2f);

			StartCoroutine(FadeOutTargetImage());

			StartCoroutine(FadeInTargetImage());

			Background4.gameObject.SetActive(false);
			Background5.gameObject.SetActive(true);


			yield return new WaitForSeconds(2f);

			StartCoroutine(FadeOutTargetImage());

			StartCoroutine(FadeInTargetImage());

			Background4.gameObject.SetActive(false);
			Background5.gameObject.SetActive(true);


			yield return new WaitForSeconds(2f);

			StartCoroutine(FadeOutTargetImage());


			StartCoroutine(FadeInTargetImage());

			Background5.gameObject.SetActive(false);
			Background6.gameObject.SetActive(true);


			yield return new WaitForSeconds(2f);

			StartCoroutine(FadeOutTargetImage());

			StartCoroutine(FadeInTargetImage());

			Background6.gameObject.SetActive(false);

			yield return new WaitForSeconds(7f);


			StartCoroutine(FadeOutTargetImage());

			yield return new WaitForSeconds(3f);
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "ȣ��");
			CharacterName.text = playerName;
		}

		if (dialogueIndex == 18)
		{
			CharacterName.color = new Color(62 / 255f, 108 / 255f, 176 / 255f);
			string playerName = PlayerPrefs.GetString("PlayerName", "ȣ��");
			CharacterName.text = playerName;
			
		}

		if(dialogueIndex == 19)
		{

			yield return new WaitForSeconds(5f);
			StartCoroutine(FadeInTargetImage());

			yield return new WaitForSeconds(3f);

			SceneManager.LoadScene("Chapter2");
		
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

	public void result()
	{
		choiceButtons[0].gameObject.SetActive(false);
		choiceButtons[1].gameObject.SetActive(false);

		Taeyoon.gameObject.SetActive(true);
		Taeyoon_2.gameObject.SetActive(false);


		Voice.clip = Teayoon3;
		Voice.Play();
		dialogueIndex++;
	}

	public void Esc()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


	private IEnumerator FadeOutMusicAndChange()
	{
		if (audioSource == null)
		{
			yield break; // Exit coroutine if AudioSource is not found
		}

		float startVolume = audioSource.volume;

		while (audioSource.volume > 0)
		{
			audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
			yield return null;
		}

		yield return new WaitForSeconds(3f);

		// Stop the current audio source
		audioSource.Stop();

		// Assign the new audio clip to the audio source
		audioSource.clip = clock; // Replace 'newAudioClip' with your desired AudioClip variable

		// Start playing the new audio clip
		audioSource.Play();

		// Gradually increase the volume
		float targetVolume = startVolume; // You can set this to your desired target volume
		while (audioSource.volume < targetVolume)
		{
			audioSource.volume += startVolume * Time.deltaTime / fadeDuration;
			yield return null;
		}

		StartFadeOutTargetImage();
		StartCoroutine(FadeInDialogBox());
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
