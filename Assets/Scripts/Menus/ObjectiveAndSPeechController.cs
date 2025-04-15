using UnityEngine;

//public class ObjectiveAndSPeechController : MonoBehaviour
//{
//    public SpeechBubble prefab;
//    private SpeechBubble activeBubble;
//    private bool isPlayerInTrigger;

//    public ObjectiveBubble prefab;
//    public string message;
//    public Sprite image;


//    public PopUpBubble objPrefab;
//    private PopUpBubble objActiveBubble;
//    public string objMessage;
//    public Sprite objImage;
//    bool wasSpeaking = false;

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            isPlayerInTrigger = true;
//            SetSpeech(message, image);
//            SetObj(objMessage, objImage);
//        }
//        else
//        {
//            isPlayerInTrigger = true;
//        }
//    }

//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        isPlayerInTrigger = false;
//        if (activeBubble != null)
//        {
//            Destroy(activeBubble.gameObject);
//            activeBubble = null;
//        }
//        if (objActiveBubble != null)
//        {
//            Destroy(objActiveBubble.gameObject);
//            objActiveBubble = null;
//        }
//        Destroy(gameObject);
//    }

//    void SetSpeech(string message, Sprite image)
//    {
//        if (activeBubble == null && !wasSpeaking)
//        {
//            Transform parent = GameObject.Find("PlayerSpeechBubbles").transform;

//            activeBubble = Instantiate(prefab, parent);
//            activeBubble.SetText(message);
//            activeBubble.SetSprite(image);
//            wasSpeaking = true;
//        }
//    }

//    void SetObj(string message, Sprite image)
//    {
//        if (objActiveBubble == null)
//        {
//            Transform parent = GameObject.Find("PopUps").transform;

//            objActiveBubble = Instantiate(objPrefab, parent);
//            objActiveBubble.SetText(message);
//            objActiveBubble.SetSprite(image);
//        }
//    }
//}
