using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    private Vector2Int _hDResolution = new Vector2Int(1280, 720);
    private Vector2Int _fHDResolution = new Vector2Int(1920, 1080);
    private Vector2Int _UHDResolution = new Vector2Int(3840, 2160);

    private Vector2Int[] _resolutions;
    private int index;
    void Awake()
    {
        index = 0;
        _resolutions = new Vector2Int[]{_hDResolution, _fHDResolution, _UHDResolution};
        Screen.SetResolution(_resolutions[index].x, _resolutions[index].y, FullScreenMode.FullScreenWindow);
    }

    public void SetNewResolution(int dir)
    {
        index = Utilities.CycleThroughCollection(_resolutions.Length, index, dir);
        Screen.SetResolution(_resolutions[index].x, _resolutions[index].y, FullScreenMode.FullScreenWindow);
        Debug.Log($"Current Resolution is {Screen.width}, {Screen.height}");
    }

    
}
