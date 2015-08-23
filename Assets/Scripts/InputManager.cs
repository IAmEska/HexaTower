using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
	public Shader hightLightShader;

	protected Camera _mainCamera;
	protected EditorManager _editorManager;
	public GameObject _selectedObject;
	protected GameObject _highLightedObject;

	// Use this for initialization
	void Start ()
	{
		_mainCamera = Camera.main;
		_editorManager = GetComponent<EditorManager> ();
	}

	protected void Highlight ()
	{
		Ray ray = _mainCamera.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			GameObject hightlight = hit.collider.gameObject;
			if (hightlight.layer == LayerMask.NameToLayer ("Hex")) {
				_selectedObject = hightlight;
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		var move = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		_mainCamera.transform.position += move * 10 * Time.deltaTime;
		if (Input.GetMouseButtonUp (0)) {
			Highlight ();
			_editorManager.SetEditObject (_selectedObject);
		}
	}

	void OnPostRender ()
	{
		// remove shader from previous highlighted object
		/*if (_highLightedObject != null && _highLightedObject != _selectedObject) {
			Renderer renderer = _highLightedObject.GetComponent<Renderer> ();
			renderer.material.shader = null;
		}

		// highlight new object
		if (_selectedObject != null && _highLightedObject != _selectedObject) {
			Renderer renderer = _selectedObject.GetComponent<Renderer> ();
			renderer.material.shader = hightLightShader;
			_highLightedObject = _selectedObject;
		}*/
	}


}
