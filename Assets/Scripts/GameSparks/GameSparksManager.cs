using GameSparks.Core;
using UnityEngine;

public class GameSparksManager : MonoBehaviour
{
    private static GameSparksManager _instance = null;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }      
    }
}
