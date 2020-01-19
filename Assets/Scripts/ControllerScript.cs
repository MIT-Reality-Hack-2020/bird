using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public GameObject Keyboard;
    public GameObject Head;

    public GameObject FloorMarker;

    bool enabled = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleKeyboard() {
        if(enabled) {
            Keyboard.gameObject.SetActive(false);
        }
        else {
            Keyboard.gameObject.SetActive(true);
            float y = Head.transform.position.y - FloorMarker.transform.position.y;
            Keyboard.transform.position = new Vector3(
                Head.transform.position.x,
                FloorMarker.transform.position.y + y - Mathf.Min(y/3, 2),
                Head.transform.position.z);
            // Debug.Log(Head.transform.rotation.y);
            Keyboard.transform.rotation = Quaternion.Euler(0, Head.transform.rotation.eulerAngles.y, 0);
        }
        enabled = !enabled;
    }
}
