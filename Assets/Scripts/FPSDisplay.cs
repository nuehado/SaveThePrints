﻿using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;
    public float fps;
    [SerializeField] private bool displayFPS = false;
    [SerializeField] private int targetFPS = 60;

    public static FPSDisplay Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        fps = 1.0f / deltaTime;

        if (Input.GetKeyDown(KeyCode.F1))
        {
            displayFPS = !displayFPS;
        }
    }

    void OnGUI()
    {

        if (displayFPS)
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = new Color(1f, 0.3f, 0.0f, 1.0f);
            float msec = deltaTime * 1000.0f;

            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }

    }
}