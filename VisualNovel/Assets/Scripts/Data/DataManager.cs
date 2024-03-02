// 데이터를 저장하고 관리하는 클래스
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{

	void Start()
	{
		string key = "CapturedImageFilePath";

		// DataManager에서 이미지 로드
		Texture2D loadedImage = DataManager.Instance.LoadImage(key);

		if (loadedImage != null)
		{
			// RawImage 컴포넌트를 가진 UI 게임 오브젝트에 접근
			RawImage targetImage = GameObject.Find("ImageObjectName").GetComponent<RawImage>();

			// 이미지 적용
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

	// 이미지 데이터를 저장하는 딕셔너리
	public Dictionary<string, Texture2D> images = new Dictionary<string, Texture2D>();

	// 클래스가 생성될 때 호출되는 메소드
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

	// 이미지를 저장하는 메소드
	// 이미지를 저장하는 메소드
	// 이미지를 저장하는 메소드
	// 이미지를 바이트 배열로 저장하는 메소드
	public void SaveImage(string key, Texture2D image)
	{
		byte[] imageBytes = image.EncodeToPNG();
		System.IO.File.WriteAllBytes(Application.persistentDataPath + "/" + key + ".png", imageBytes);
	}

	// 바이트 배열을 이미지로 불러오는 메소드
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

