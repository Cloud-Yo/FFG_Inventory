using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class MovableIcon : MonoBehaviour
{
    private Image _myImage = null;
    [SerializeField] private float _blinkSpeed = 2f;

    void Start()
    {
        _myImage = GetComponent<Image>();
        _myImage.enabled = false;
        _myImage.raycastTarget = false;
    }

    public void ChangeImage(Sprite sprite)
    {
        _myImage.sprite = sprite;
        _myImage.SetNativeSize();
        _myImage.enabled = true;
        StartCoroutine(ImageBlinkAnimation());
    }

    public void DisableImage()
    {
        _myImage.enabled = false;
        StopCoroutine(ImageBlinkAnimation());
    }

    private IEnumerator ImageBlinkAnimation()
    {
        Color transparent = Color.white;
        float t = 0;
        while(_myImage.enabled)
        {
            t += Time.deltaTime * _blinkSpeed;
            transparent.a = Mathf.PingPong(t, 1);
            _myImage.color = transparent;
            yield return null;
        }
    }
}
