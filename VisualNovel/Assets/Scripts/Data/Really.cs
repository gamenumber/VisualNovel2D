using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Really : MonoBehaviour
{
	public GameObject ReallyPage;
	public float waittime = 2f;
	public static string playerName;
	public InputField inputField;
	public Text text;
	public Image fadeImage; // Reference to the Image component for fade-out
	public float fadeOutDuration = 1f; // Set the duration of the fade-out effect

	void Start()
	{
		// Assuming you want the ReallyPage initially hidden
		ReallyPage.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		// Check for Enter key press
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			// Call the method to open the ReallyPage
			ReallyPageOpen();
		}
	}

	public void SavePlayerName(string name)
	{
		playerName = name;
		PlayerPrefs.SetString("PlayerName", playerName);
	}

	public void ReallyPageOpen()
	{
		// Create a message with the input text
		string inputText = inputField.text;
		string message = inputText + "이/가 맞습니까?";

		// Set the text property of the Text component to display the message
		text.text = message;

		// Show the ReallyPage
		ReallyPage.SetActive(true);

	}

	public void No()
	{
		// Hide the ReallyPage
		ReallyPage.SetActive(false);
	}

	public void Yes()
	{
		// Get the input text again
		string inputText = inputField.text;

		// Save the player name
		SavePlayerName(inputText);

		// Start the fade-out coroutine
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeOut()
	{
		fadeImage.gameObject.SetActive(true);

		float elapsedTime = 0f;

		while (elapsedTime < fadeOutDuration)
		{
			float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeOutDuration);
			fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		

		// Reset alpha to fully visible for next time
		fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);

		new WaitForSeconds(waittime);
	}
}
