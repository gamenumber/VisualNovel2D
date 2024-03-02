using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ZoomImage : MonoBehaviour
{
	public Image imageToZoom; // 확대하고 싶은 이미지
	public float zoomSpeed = 0.1f; // 확대 속도
	public Vector3 zoomAmount = new Vector3(1.1f, 1.1f, 1.1f); // 확대할 크기
	public Vector2[] positions; // 이미지의 특정 부분들의 위치

	private int currentPosIndex = 0; // 현재 보여주고 있는 위치의 인덱스

	void Start()
	{
		StartCoroutine(ZoomAndMove());
	}

	IEnumerator ZoomAndMove()
	{
		while (true)
		{
			// 이미지 확대
			imageToZoom.rectTransform.localScale += zoomAmount * zoomSpeed;

			// 이미지 위치 조정
			imageToZoom.rectTransform.anchoredPosition = -positions[currentPosIndex] * imageToZoom.rectTransform.localScale.x;

			// 다음 위치로 인덱스 이동
			currentPosIndex = (currentPosIndex + 1) % positions.Length;

			// 1초 대기
			yield return new WaitForSeconds(1f);
		}
	}
}
