using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    public GameObject trailPrefab;
    public GameObject spawnTrailAt;
    public PaintSceneManager paintSceneManager;
    GameObject currentTrail;

    public Transform newParent;

    List<GameObject> trailUndoList;
    int falsePresses = 0;

    // Start is called before the first frame update
    void Start()
    {
        trailUndoList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateTrail() {
        Debug.Log("trail created");
        currentTrail = Instantiate(trailPrefab);
        // gameObject.Add()
        currentTrail.transform.parent = spawnTrailAt.transform;
        currentTrail.transform.localPosition = new Vector3();
    }

    public void ReleaseTrail() {
        currentTrail.transform.parent = newParent;
        trailUndoList.Add(currentTrail);
        currentTrail = null;
    }

    public void UndoTrail() {
        if(currentTrail == null) {
            int i = trailUndoList.Count - 1;
            if (i < 0) {
                if(falsePresses++ > 5){
                    paintSceneManager.HandleHMDMounted();
                    falsePresses = 0;
                }
                return;
            }
            Destroy(trailUndoList[i]);
            trailUndoList.RemoveAt(i);
            falsePresses = 0;
        }
    }

    public void ResetTrailList() {
        trailUndoList = new List<GameObject>();
    }
}
