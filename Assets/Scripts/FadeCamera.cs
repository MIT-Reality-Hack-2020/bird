
using UnityEngine;
 
public class FadeCamera : MonoBehaviour
{
    public AnimationCurve FadeCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.6f, 0.7f, -1.8f, -1.2f), new Keyframe(1, 0));
 
    private float _alpha = 1;
    private Texture2D _texture;
    private bool _done;
    private float _time;
    private bool _black;

    public GameObject fadeObj;
 
    public void Reset()
    {
        _black = false;
        _done = false;
        _alpha = 1;
        _time = 0;
    }
 
    [RuntimeInitializeOnLoadMethod]
    public void RedoFade()
    {
        Reset();
    }

    public void SetToBlack() {
        _black = true;
    }
 
    public void OnGUI()
    {
 
        _time += Time.deltaTime;
        _alpha = FadeCurve.Evaluate(_time);

        if (_black) {
            _alpha = 1;
        }

        var col = fadeObj.GetComponent<Renderer> ().material.color;
        col.a = _alpha;
        fadeObj.GetComponent<Renderer> ().material.color = col;
 
        if (_alpha <= 0) _done = true;
    }
}