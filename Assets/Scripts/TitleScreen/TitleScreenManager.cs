using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartNewGame()
    {
        StartCoroutine(SaveManager._instance.LoadNewGame());
    }
}
