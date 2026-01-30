using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public bool DoNotDestroyOnLoad = false;
    public static T Instance;

    protected void Awake() 
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = (T)this;
        if (DoNotDestroyOnLoad)
            DontDestroyOnLoad(this);
    }
}
