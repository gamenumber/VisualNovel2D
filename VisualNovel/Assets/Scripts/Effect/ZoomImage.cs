using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ZoomImage : MonoBehaviour
{
	public Image imageToZoom; // Ȯ���ϰ� ���� �̹���
	public float zoomSpeed = 0.1f; // Ȯ�� �ӵ�
	public Vector3 zoomAmount = new Vector3(1.1f, 1.1f, 1.1f); // Ȯ���� ũ��
	public Vector2[] positions; // �̹����� Ư�� �κе��� ��ġ

	private int currentPosIndex = 0; // ���� �����ְ� �ִ� ��ġ�� �ε���

	void Start()
	{
		StartCoroutine(ZoomAndMove());
	}

	IEnumerator ZoomAndMove()
	{
		while (true)
		{
			// �̹��� Ȯ��
			imageToZoom.rectTransform.localScale += zoomAmount * zoomSpeed;

			// �̹��� ��ġ ����
			imageToZoom.rectTransform.anchoredPosition = -positions[currentPosIndex] * imageToZoom.rectTransform.localScale.x;

			// ���� ��ġ�� �ε��� �̵�
			currentPosIndex = (currentPosIndex + 1) % positions.Length;

			// 1�� ���
			yield return new WaitForSeconds(1f);
		}
	}
}
