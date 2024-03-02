using UnityEngine;

public class CustomRenderer : MonoBehaviour
{
	// Material for custom rendering
	public Material customMaterial;

	void OnRenderObject()
	{
		if (customMaterial != null)
		{
			// Set the custom material
			customMaterial.SetPass(0);

			// Draw the object with the custom material
			Graphics.DrawMeshNow(GetComponent<MeshFilter>().mesh, transform.localToWorldMatrix);
		}
	}
}
