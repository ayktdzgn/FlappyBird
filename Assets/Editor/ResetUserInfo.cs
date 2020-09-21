using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ResetUserInfo : Editor
{
    [MenuItem("FlapyBird/Reset User Info")]
    static void ResetUserInfos()
    {
        PlayerPrefs.DeleteAll();
    }
}
