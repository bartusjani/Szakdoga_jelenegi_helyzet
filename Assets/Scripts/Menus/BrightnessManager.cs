using UnityEngine;

public class BrightnessManager : MonoBehaviour
{
    public static BrightnessManager Instance;

    public float currentBrightness = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
