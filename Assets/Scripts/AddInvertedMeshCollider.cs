using UnityEngine;
using System.Linq;
using System.Collections;
 
[ExecuteInEditMode]
public class AddInvertedMeshCollider : MonoBehaviour
{
  public bool removeExistingColliders = true;
 
  public void Start()
  {
    if (removeExistingColliders)
      RemoveExistingColliders();
 
      InvertMesh();
     
      gameObject.AddComponent<MeshCollider>();
  }
 
  private void RemoveExistingColliders()
  {
    Collider[] colliders = GetComponents<Collider>();
    for (int i = 0; i < colliders.Length; i++)
      DestroyImmediate(colliders[i]);
  }
 
  private void InvertMesh()
  {
    Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
    mesh.triangles = mesh.triangles.Reverse().ToArray();
  }
}