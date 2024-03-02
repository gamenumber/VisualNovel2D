using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoveGuageSystem : MonoBehaviour
{
	public Slider[] loveGuageSliders;
	public GameObject[] specialVoiceObjects;
	private string[] characterNames = { "Hana", "Seona" };

	private void OnEnable()
	{
		for (int i = 0; i < loveGuageSliders.Length; i++)
		{
			int index = i;
			loveGuageSliders[i].onValueChanged.AddListener((value) => OnLoveGuageChanged(value, index));
			float loadedValue = LoadLoveGuage(characterNames[i]);
			if (loadedValue != -1)
			{
				loveGuageSliders[i].value = loadedValue;
			}
		}
	}

	private void OnLoveGuageChanged(float value, int index)
	{
		Debug.Log("OnLoveGuageChanged - " + characterNames[index] + ": " + value * 100);
		SaveLoveGuage(characterNames[index], value * 100);

		if (value >= 0.6)
		{
			specialVoiceObjects[index].SetActive(false);
		}
		else
		{
			specialVoiceObjects[index].SetActive(true);
		}
	}

	public static void SaveLoveGuage(string characterName, float value)
	{
		// JSON 파일로 저장
		string savePath = Path.Combine(Application.persistentDataPath, "LoveGuageData.json");

		var data = new LoveGuageData
		{
			CharacterName = characterName,
			LoveGuage = value
		};

		string json = JsonUtility.ToJson(data);
		File.WriteAllText(savePath, json);

		// PlayerPrefs로 저장
		PlayerPrefs.SetFloat(characterName, value);
		PlayerPrefs.Save();

		Debug.Log("SaveLoveGuage - " + characterName + ": " + value);
	}

	public static float LoadLoveGuage(string characterName)
	{
		// JSON 파일에서 불러오기
		string savePath = Path.Combine(Application.persistentDataPath, "LoveGuageData.json");

		if (File.Exists(savePath))
		{
			string json = File.ReadAllText(savePath);
			var data = JsonUtility.FromJson<LoveGuageData>(json);

			if (data.CharacterName == characterName)
			{
				Debug.Log("LoadLoveGuage - " + characterName + ": " + data.LoveGuage);
				return data.LoveGuage / 100;
			}
		}

		// PlayerPrefs에서 불러오기
		if (PlayerPrefs.HasKey(characterName))
		{
			float value = PlayerPrefs.GetFloat(characterName);
			Debug.Log("LoadLoveGuage - " + characterName + ": " + value);
			return value / 100;
		}

		return -1;
	}

}

[System.Serializable]
public class LoveGuageData
{
	public string CharacterName;
	public float LoveGuage;
}
