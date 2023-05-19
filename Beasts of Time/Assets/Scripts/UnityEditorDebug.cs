using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityEditorDebug : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogget.logEnabled = false;
#endif
    }
}
