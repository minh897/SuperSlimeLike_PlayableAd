using UnityEngine;

public class TileSlot : MonoBehaviour
{
    public Material GetMaterial() => TileMeshRenderer.sharedMaterial;
    public Mesh GetMesh() => TileMeshFilter.sharedMesh;
    public Collider GetCollider() => TileCollider;

    private MeshRenderer TileMeshRenderer => GetComponent<MeshRenderer>();
    private MeshFilter TileMeshFilter => GetComponent<MeshFilter>();
    private Collider TileCollider => GetComponent<Collider>();

    public void SwitchTile(GameObject tilePrefab)
    {
        var newRenderer = tilePrefab.GetComponent<MeshRenderer>();
        var newFilter = tilePrefab.GetComponent<MeshFilter>();

        // Replace mesh renderer and material
        TileMeshRenderer.material = newRenderer.sharedMaterial;
        TileMeshFilter.mesh = newFilter.sharedMesh;
    }

    public void AdjustYRotation(int dir)
    {
        transform.Rotate(0, 90 * dir, 0);
    }

    public void AdjustYPosition(int verticalDir)
    {
        transform.position += new Vector3(0, 0.1f * verticalDir, 0);
    }
}
