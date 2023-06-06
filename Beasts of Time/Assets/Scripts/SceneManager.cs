using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    /* For SceneManager_2.0:
     * Add Transform parameter "LevelStartLocation" and move the player location there.
     */

    public static SceneManager Instance { get; private set; }

    private void Awake()
    {
        // Create SceneManager singleton instance
        if (Instance != null)
        {
            Debug.LogError("There is more than one SceneManager instance");
        }
        Instance = this;
    }
}
