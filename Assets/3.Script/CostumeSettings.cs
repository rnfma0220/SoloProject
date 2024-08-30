using UnityEngine;

public class CostumeSettings : MonoBehaviour
{
	public bool ReduceUsingMesh = true;

	public int PartSize;

	public Transform CollidersRoot;

	public Collider[] KeepInColliders = new Collider[0];

	public Collider[] KeepOutColliders = new Collider[0];

	public Transform MeshOffset;
}
