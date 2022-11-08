using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames = 0;
    private float fps;
    // Use this for initialization
    void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            fps = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;
        }
    }

    void OnGUI()
    {
        GUIStyle bb = new GUIStyle();
        bb.normal.background = null;
        bb.normal.textColor = new Color(1, 0, 0);
        bb.fontSize = 50;

        GUI.Label(new Rect(Screen.width-250, 0, 200, 200), string.Format("fps:{0}", fps.ToString("n")), bb);

    }

}
