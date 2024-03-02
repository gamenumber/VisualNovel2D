// �����͸� �����ϰ� �����ϴ� Ŭ����
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{

	void Start()
	{
		string key = "CapturedImageFilePath";

		// DataManager���� �̹��� �ε�
		Texture2D loadedImage = DataManager.Instance.LoadImage(key);

		if (loadedImage != null)
		{
			// RawImage ������Ʈ�� ���� UI ���� ������Ʈ�� ����
			RawImage targetImage = GameObject.Find("ImageObjectName").GetComponent<RawImage>();

			// �̹��� ����
			targetImage.texture = loadedImage;
		}
	}


	private static DataManager instance;
	public static DataManager Instance
	{
		get
		{
			if (instance == null)
			{
				GameObject go = new GameObject("DataManager");
				instance = go.AddComponent<DataManager>();
			}

			return instance;
		}
	}

	// �̹��� �����͸� �����ϴ� ��ųʸ�
	public Dictionary<string, Texture2D> images = new Dictionary<string, Texture2D>();

	// Ŭ������ ������ �� ȣ��Ǵ� �޼ҵ�
	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(this.gameObject);
	}

	// �̹����� �����ϴ� �޼ҵ�
	// �̹����� �����ϴ� �޼ҵ�
	// �̹����� �����ϴ� �޼ҵ�
	// �̹����� ����Ʈ �迭�� �����ϴ� �޼ҵ�
	public void SaveImage(string key, Texture2D image)
	{
		byte[] imageBytes = image.EncodeToPNG();
		System.IO.File.WriteAllBytes(Application.persistentDataPath + "/" + key + ".png", imageBytes);
	}

	// ����Ʈ �迭�� �̹����� �ҷ����� �޼ҵ�
	public Texture2D LoadImage(string key)
	{
		string filePath = Application.persistentDataPath + "/" + key + ".png";

		if (System.IO.File.Exists(filePath))
		{
			byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);
			Texture2D tex = new Texture2D(2, 2);
			tex.LoadImage(imageBytes);
			return tex;
		}
		return null;
	}
}

