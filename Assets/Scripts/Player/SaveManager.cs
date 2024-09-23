using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveManager : MonoBehaviour
{

    public static SaveManager _instance;
    [SerializeField] private int worldSceneIndex = 1;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator LoadNewGame()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

        yield return null;
    }
}
