using UnityEngine;

public class PopUpCounter : MonoBehaviour
{
    private static PopUpCounter instance;
    public static PopUpCounter Instance => instance;

    public int textIndex = 0;
    public int lastTextIndex = -1;

    public int secondTextIndex = 0;
    public int secondLastTextIndex = -1;

    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
        }
    }

}
