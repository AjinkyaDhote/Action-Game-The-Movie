using UnityEngine;

public class SoundManager3D : MonoBehaviour
{
    public MyAudioSource backgroundMusic;
    public MyAudioSource onHitByEnemyPlayer;
    public MyAudioSource gunEmpty;
    public MyAudioSource shotgun;
    public MyAudioSource pistol;
    public MyAudioSource onHitByEnemyPayload;
    public MyAudioSource intruderAlert;
    public MyAudioSource onEnemyHit;
    public MyAudioSource enemyDeath;
   
    [HideInInspector]
    public MyAudioSource[] myAudioSources;
    private static SoundManager3D _instance = null;
    public static SoundManager3D Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager3D>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        myAudioSources = new MyAudioSource[] { backgroundMusic, onHitByEnemyPlayer, gunEmpty, shotgun, pistol, onHitByEnemyPayload, intruderAlert, onEnemyHit, enemyDeath };
        for (int i = 0; i < myAudioSources.Length; i++)
        {
            MyAudioSource.Initilaize(gameObject, myAudioSources[i]);
        }
    }
}
