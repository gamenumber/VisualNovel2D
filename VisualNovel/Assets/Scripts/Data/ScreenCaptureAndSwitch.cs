using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.EventSystems;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Reflection;
public class ScreenCaptureAndSwitch : MonoBehaviour
{
	public Camera mainCamera;
	public RawImage[] targetImages;
	public string sceneToLoad;
	public GameObject Save1;
	public GameObject Save2;
	public GameObject Save3;

	private bool isCaptureRoutineRunning = false;

	public Text[] SceneName;

	private int currentTargetImageIndex = -1;
	private List<EventTrigger> savedTriggers = new List<EventTrigger>();


	private int _currentPage = 0;

	void Start()
	{
		

			for (int i = 0; i < targetImages.Length; i++)
			{
				int index = i;
				EventTrigger trigger = targetImages[i].gameObject.AddComponent<EventTrigger>();
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener((data) => { OnImageClick(index); });
				trigger.triggers.Add(entry);

				// Save the trigger for later use
				savedTriggers.Add(trigger);

				// �̹����� �ε�
				string imageFilePathKey = "CapturedImageFilePath" + index;
				string imageFilePath = PlayerPrefs.GetString(imageFilePathKey);

				if (!string.IsNullOrEmpty(imageFilePath) && File.Exists(imageFilePath))
				{
					byte[] imageBytes = File.ReadAllBytes(imageFilePath);
					Texture2D tex = new Texture2D(2, 2);
					tex.LoadImage(imageBytes);
					targetImages[index].texture = tex;
				}

				// Set the scene name to the corresponding Text component
				if (index < SceneName.Length)
				{
					string filePath = GetFilePath(index);
					if (File.Exists(filePath))
					{
						string savedSceneName = File.ReadAllText(filePath);
						SceneName[index].text = savedSceneName;
					}
					else
					{
						SceneName[index].text = ""; // Change to your default scene name
					}
				}
			}
		

	}



	void OnImageClick(int index)
	{
		if (currentTargetImageIndex != index)
		{
			// �̹����� ���� ������� ���� ��� �̹��� ����
			if (targetImages[index].texture == null)
			{
				CaptureAfterOneFrame(index);
			}
			else
			{
				// �̹����� ����Ǿ� ������ �ش� ���� ����
				string sceneName = GetSceneName(index);
				if (!string.IsNullOrEmpty(sceneName))
				{
					StartCoroutine(LoadSceneAsync(sceneName));
				}
			}
		}
		else
		{
			// �̹����� Ŭ���� �̹����� ������ ��� �߰����� ó�� ����
		}
	}

	string GetSceneName(int index)
	{
		// ����� �� �̸��� �о��
		string filePath = GetFilePath(index);
		if (File.Exists(filePath))
		{
			return File.ReadAllText(filePath);
		}

		return null;
	}

	IEnumerator LoadSceneAsync(string sceneName)
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
		while (!asyncLoad.isDone)
		{
			yield return null;
		}

		// �߰����� �ε� �� ó�� ����
	}

	public void TakeScreenshot(int index)
	{
		// ���� �ڵ�...
		_currentPage = index;
		StartCoroutine(CaptureRoutine(index));


	}

	private string GetFilePath(int index)
	{
		return Path.Combine(Application.persistentDataPath, "savedSceneName_" + index + ".txt");
	}

	void CaptureAfterOneFrame(int index)
	{
		// Deactivate Save1, Save2, Save3 before capturing
		Save1.SetActive(false);
		Save2.SetActive(false);
		Save3.SetActive(false);

		StartCoroutine(CaptureRoutine(index));
	}

	IEnumerator CaptureRoutine(int index)
	{
		if (isCaptureRoutineRunning)
		{
			yield break;
		}
		// Wait for a few frames before capturing (adjust the frame count as needed)
		for (int i = 0; i < 1; i++)
		{
			yield return null;
		}

		yield return new WaitForEndOfFrame();

		Texture2D screenTexture = ScreenCapture.CaptureScreenshotAsTexture();
		targetImages[index].texture = screenTexture;


		// Get the current scene name
		string savedSceneName = SceneManager.GetActiveScene().name;

		if (string.IsNullOrEmpty(savedSceneName))
		{
			savedSceneName = SceneManager.GetSceneByBuildIndex(0).name;
		}

		// Display the scene name below the corresponding image
		if (index < SceneName.Length)
		{
			SceneName[index].text = savedSceneName;
		}

		// ���Ͽ� �� �̸� ����
		string sceneFilePath = GetFilePath(index);
		File.WriteAllText(sceneFilePath, savedSceneName);

		// �̹����� ���Ϸ� ����
		string fileName = "captured_image_" + index + ".png";
		string imageFilePath = Path.Combine(Application.persistentDataPath, fileName);
		File.WriteAllBytes(imageFilePath, screenTexture.EncodeToPNG());

		// �̹��� ���� ��θ� ����
		string imageFilePathKey = "CapturedImageFilePath" + index;
		PlayerPrefs.SetString(imageFilePathKey, imageFilePath);

		currentTargetImageIndex = index;

		// Reactivate Save1, Save2, Save3 after capturing
		Save1.SetActive(true);
		Save2.SetActive(true);
		Save3.SetActive(true);
	}



	public void LoadSceneOnClick()
	{
		
		if (!string.IsNullOrEmpty(sceneToLoad))
		{
			StartCoroutine(LoadSceneAsync(sceneToLoad));
		}
	}



	public void MoveHome()
	{
		MoveToPage(0);
	}

	public void Move1()
	{
		MoveToPage(1);
	}

	public void Move2()
	{
		MoveToPage(2);
	}

	public void Move3()
	{
		MoveToPage(3);
	}

	public void MoveToPage(int page)
	{
		_currentPage = page;
		Save1.SetActive(page == 1);
		Save2.SetActive(page == 2);
		Save3.SetActive(page == 3);
	}

	// �̹��� �ε� �� ǥ�� �޼���
	// �̹����� ���Ϸ� �����մϴ�.
	public void SaveImage(Texture2D image, string key)
	{
		byte[] imageBytes = image.EncodeToPNG();
		string filePath = Path.Combine(Application.persistentDataPath, key + ".png");
		File.WriteAllBytes(filePath, imageBytes);
	}

	// �̹����� ���Ͽ��� �ҷ��ɴϴ�.
	public Texture2D LoadImage(string key)
	{
		string filePath = Path.Combine(Application.persistentDataPath, key + ".png");

		if (File.Exists(filePath))
		{
			byte[] imageBytes = File.ReadAllBytes(filePath);
			Texture2D loadedImage = new Texture2D(2, 2);
			loadedImage.LoadImage(imageBytes);
			return loadedImage;
		}

		return null;
	}

	// ����� ��� �̹����� ���� �����մϴ�.
	public void DeleteAllSavedImagesAndScenes()
	{
		for (int i = 0; i < targetImages.Length; i++)
		{
			string imageFilePathKey = "CapturedImageFilePath" + i;
			DeleteFile(imageFilePathKey, () =>
			{
				targetImages[i].texture = null;
			});


			// �� ���� ����
			string sceneFilePathKey = GetFilePath(i);
			DeleteFile(sceneFilePathKey, () =>
			{
				SceneName[i].text = string.Empty;
			});
			// UI ���� ���� ����
			string uiStateFilePath = Path.Combine(Application.persistentDataPath, "ui_state_" + i + ".json");
			DeleteFile(uiStateFilePath, null); // actionAfterDeletion�� ������� ����

			// �� �̸� �� �̹��� Ű ����
			string sceneNameKey = "SceneNameKey_" + i;
			PlayerPrefs.DeleteKey(sceneNameKey);


			// �̹��� Ű ����
			string imageKey = "ImageKey_" + i;
			if (PlayerPrefs.HasKey(imageKey))
			{
				PlayerPrefs.DeleteKey(imageKey);
			}
		}

		PlayerPrefs.Save(); // ��������� ����

	}

	private void DeleteFile(string filePath, Action actionAfterDeletion = null)
	{
		if (FileExistsAndDelete(filePath))
		{
			Debug.Log("File deleted: " + filePath);
			actionAfterDeletion?.Invoke(); // actionAfterDeletion�� �����Ǹ� ȣ��
		}

		if (PlayerPrefs.HasKey(filePath))
		{
			string filePathValue = PlayerPrefs.GetString(filePath);
			if (FileExistsAndDelete(filePathValue))
			{
				PlayerPrefs.DeleteKey(filePath);
				actionAfterDeletion?.Invoke();
			}
		}
	}


	private bool FileExistsAndDelete(string filePath)
	{
		if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
		{
			File.Delete(filePath);
			return true;
		}

		return false;
	}


	


}
