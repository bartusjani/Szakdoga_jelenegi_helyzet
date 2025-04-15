using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public RoomTP firstTP;
    public RoomTP secondTP;

    bool firstFadingDone;
    bool secondFadingDone;

    private void Update()
    {
        if (firstTP.isFading)
        {
            secondTP.isFading = true;
        }
        else if (firstTP.isFading == false)
        {
            secondTP.isFading = false;
        }
    }
}
