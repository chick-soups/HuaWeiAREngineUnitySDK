using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(NativeLib_stringFromJNI());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
    [DllImport("arenginesdk-3.7.0.3")] 
    private static extern bool  HwArEnginesApk_isAREngineApkReady(IntPtr env,IntPtr applicationContext);

    [DllImport("unitypackagehelper")]
    private static extern string NativeLib_stringFromJNI();

    
}
