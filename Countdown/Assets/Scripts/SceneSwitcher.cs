using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    public int scene;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(scene);
    }
}
