using UnityEngine;
using System.Collections;

public class Gizmo : MonoBehaviour {
	public float gizmoSize = .75f;
	public Color gizmoColor = Color.yellow;
	// Use this for initialization
	void OnDrawGizmos() {
		Gizmos.color = gizmoColor;
		Gizmos.DrawWireSphere (transform.position, gizmoSize);
	}

}
