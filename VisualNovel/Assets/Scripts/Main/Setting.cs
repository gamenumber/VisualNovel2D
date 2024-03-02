using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
	public Slider volumeSlider;
	public Slider brightnessSlider;
	public Slider voiceSlider;
	public GameObject SettingPage;
	public Canvas canvas;
	private Image overlay;

	public AudioSource backgroundMusicSource;
	public AudioSource voiceSource;


	private float savedVolume = 1.0f;
	private float savedBrightness = 1.0f;
	private float savedVoiceVolume = 1.0f;

	void Awake()
	{
		LoadSettings();

		overlay = new GameObject("Overlay").AddComponent<Image>();
		overlay.transform.SetParent(canvas.transform, false);
		overlay.color = new Color(0, 0, 0, 1 - savedBrightness);
		overlay.rectTransform.anchorMin = new Vector2(0, 0);
		overlay.rectTransform.anchorMax = new Vector2(1, 1);
		overlay.rectTransform.sizeDelta = Vector2.zero;

		var canvasGroup = overlay.gameObject.AddComponent<CanvasGroup>();
		canvasGroup.blocksRaycasts = false;

		GameObject bgMusicGameObject = GameObject.Find("Backgroundmusic");
		if (bgMusicGameObject != null)
		{
			backgroundMusicSource = bgMusicGameObject.GetComponent<AudioSource>();
		}

		GameObject voiceGameObject = GameObject.Find("Voice");
		if (voiceGameObject != null)
		{
			voiceSource = voiceGameObject.GetComponent<AudioSource>();
		}
	}

	void Start()
	{
		volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
		brightnessSlider.onValueChanged.AddListener(OnBrightnessChanged);
		voiceSlider.onValueChanged.AddListener(OnVoiceVolumeChanged);

		volumeSlider.value = savedVolume;
		brightnessSlider.value = savedBrightness;
		voiceSlider.value = savedVoiceVolume;
	}

	void OnVolumeChanged(float volume)
	{
		if (backgroundMusicSource != null)
		{
			backgroundMusicSource.volume = volume;
		}
		savedVolume = volume;
		SaveSettings();
	}

	void OnBrightnessChanged(float newBrightness)
	{
		// Invert the brightness value
		float invertedBrightness = 1 - Mathf.Clamp01(newBrightness);

		// Set overlay color alpha
		overlay.color = new Color(0, 0, 0, invertedBrightness);

		savedBrightness = newBrightness;  // Save the original brightness value
		SaveSettings();
	}

	void OnVoiceVolumeChanged(float volume)
	{
		if (voiceSource != null)
		{
			voiceSource.volume = volume;
		}
		savedVoiceVolume = volume;  // Save the value directly, without inverting
		SaveSettings();
	}

	public void OnClickSetting()
	{
		SettingPage.SetActive(true);
	}

	public void OnClickSetting2()
	{
		SettingPage.SetActive(false);
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		GameObject bgMusicGameObject = GameObject.Find("Backgroundmusic");
		if (bgMusicGameObject != null)
		{
			backgroundMusicSource = bgMusicGameObject.GetComponent<AudioSource>();
		}

		GameObject voiceGameObject = GameObject.Find("Voice");
		if (voiceGameObject != null)
		{
			voiceSource = voiceGameObject.GetComponent<AudioSource>();
		}

		ApplySavedSettings();
	}

	void ApplySavedSettings()
	{
		if (backgroundMusicSource != null)
		{
			backgroundMusicSource.volume = savedVolume;
		}

		if (voiceSource != null)
		{
			voiceSource.volume = savedVoiceVolume;
		}

		float newAlpha = 1 - Mathf.Clamp01(savedBrightness);
		overlay.color = new Color(0, 0, 0, newAlpha);
	}

	void SaveSettings()
	{
		PlayerPrefs.SetFloat("SavedVolume", savedVolume);
		PlayerPrefs.SetFloat("SavedBrightness", savedBrightness);
		PlayerPrefs.SetFloat("SavedVoiceVolume", savedVoiceVolume);
		PlayerPrefs.Save();
	}

	void LoadSettings()
	{
		savedVolume = PlayerPrefs.GetFloat("SavedVolume", 1.0f);
		savedBrightness = PlayerPrefs.GetFloat("SavedBrightness", 1.0f);
		savedVoiceVolume = PlayerPrefs.GetFloat("SavedVoiceVolume", 1.0f);

		volumeSlider.value = savedVolume;
		brightnessSlider.value = savedBrightness;
		voiceSlider.value = savedVoiceVolume;
	}

	private void OnApplicationQuit()
	{
		SaveSettings();
	}
}
