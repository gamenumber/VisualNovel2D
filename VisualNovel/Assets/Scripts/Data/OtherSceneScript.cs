using UnityEngine;

public class OtherSceneScript : MonoBehaviour
{
    void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName");

        if (!string.IsNullOrEmpty(playerName))
        {
            Debug.Log("Player Name: " + playerName);
        }
        else
        {
            Debug.Log("Player Name not found.");
        }
    }
}
