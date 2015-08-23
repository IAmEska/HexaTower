using UnityEngine;
using System.Collections;

public class EditorManager : MonoBehaviour
{
	public float _verticalLevel = 1;
	public float _textureXOffset = 0.286f;
	public float _textureYOffset = 0.585f;

	public GameObject _editObject;

	public enum HexType
	{
		Grass = 0,
		Water = 1
	}

	public enum Tools
	{
		None = 0,
		RaiseTerrain = 1,
		LowerTerrain = 2,
		RemoveTerrain = 3,
		AddTerrain = 4
	}

	public Tools _tool;

	// Use this for initialization
	void Start ()
	{
		_tool = Tools.None;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	protected void CheckAroundHexes (GameObject obj, Vector3 oldPos)
	{
		Collider[] colliders = Physics.OverlapSphere (oldPos, obj.GetComponent<MeshRenderer> ().bounds.size.x);
		for (int i=0; i<colliders.Length; i++) {
			GameObject go = colliders [i].gameObject;
			if (go != obj) {
				if (Mathf.Abs (go.transform.position.y - obj.transform.position.y) > 1) {
					EditObject (go);
				}
			}
		}
	}

	public void ChangeObject (int type)
	{
		if (_editObject != null) {
			float y;
			switch ((HexType)type) {
			default:
				y = 0;
				break;

			case HexType.Water:
				y = _textureYOffset;
				break;
			}
			Renderer r = _editObject.GetComponent<Renderer> ();
			Vector2 offset = r.material.mainTextureOffset;
			offset.y = y;
			r.material.mainTextureOffset = offset;
		}
	}

	public void SetEditObject (GameObject obj)
	{
		_editObject = obj;
	}

	public void EditObject (GameObject obj)
	{
		Vector3 newPos, oldPos;
		switch (_tool) {
		case Tools.RaiseTerrain:
			newPos = obj.transform.position;
			if (newPos.y < 2) {
				newPos.y += _verticalLevel;
				oldPos = obj.transform.position;
				obj.transform.position = newPos;
				Renderer renderer = obj.GetComponent<Renderer> ();
				Vector2 matOffset = renderer.material.mainTextureOffset;
				matOffset.x += _textureXOffset;
				renderer.material.mainTextureOffset = matOffset;
				CheckAroundHexes (obj, oldPos);
			}
			break;

		case Tools.LowerTerrain:
			newPos = obj.transform.position;
			if (newPos.y > 0) {
				newPos.y -= _verticalLevel;
				oldPos = obj.transform.position;
				obj.transform.position = newPos;
				Renderer renderer = obj.GetComponent<Renderer> ();
				Vector2 matOffset = renderer.material.mainTextureOffset;
				matOffset.x -= _textureXOffset;
				renderer.material.mainTextureOffset = matOffset;
				CheckAroundHexes (obj, oldPos);
			}
			break;

		case Tools.RemoveTerrain:
			DestroyObject (obj);
			break;

		}
	}


	public void ChangeTool (int tool)
	{
		if (_editObject != null) {
			_tool = (Tools)tool;
			EditObject (_editObject);
		}
	}
}
