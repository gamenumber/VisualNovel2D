using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
	public AudioClip Seona1;
	public AudioClip Seona2;
	public AudioClip Seona3;
	public AudioClip Seona4;

	public AudioClip Hana1;
	public AudioClip Hana2;
	public AudioClip Hana3;
	public AudioClip Hana4;

	public AudioSource Voiceacteraudio;

	// SeonA AudioClip 재생 함수
	public void PlaySeonA(int clipNumber)
	{
		switch (clipNumber)
		{
			case 1:
				Voiceacteraudio.clip = Seona1;
				break;
			case 2:
				Voiceacteraudio.clip = Seona2;
				break;
			case 3:
				Voiceacteraudio.clip = Seona3;
				break;
			case 4:
				Voiceacteraudio.clip = Seona4;
				break;
		}
		Voiceacteraudio.Play();
	}

	// Hana AudioClip 재생 함수
	public void PlayHana(int clipNumber)
	{
		switch (clipNumber)
		{
			case 1:
				Voiceacteraudio.clip = Hana1;
				break;
			case 2:
				Voiceacteraudio.clip = Hana2;
				break;
			case 3:
				Voiceacteraudio.clip = Hana3;
				break;
			case 4:
				Voiceacteraudio.clip = Hana4;
				break;
		}
		Voiceacteraudio.Play();
	}
}
