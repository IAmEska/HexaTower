using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterNavigation : MonoBehaviour
{

	public Vector3 _location;

	protected NavMeshAgent _navMeshAgent;

	// Use this for initialization
	void Start ()
	{

		_navMeshAgent = GetComponent<NavMeshAgent> ();

		if (_location != null) {
			_navMeshAgent.SetDestination (_location);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
