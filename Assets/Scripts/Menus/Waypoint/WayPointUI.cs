using UnityEngine;
using UnityEngine.UI;

public class WayPointUI : MonoBehaviour
{
    public Transform target;
    public RectTransform icon;
    float screenEdge = 50f;

    bool isNewTarget = false;
    float hideDis = 20f;

    void Update()
    {
        

        if (target != null)
        {
            Vector3 iconPos= Camera.main.WorldToScreenPoint(target.position);


            iconPos.x = Mathf.Clamp(iconPos.x, screenEdge, Screen.width - screenEdge);

            iconPos.y = Mathf.Clamp(iconPos.y, screenEdge, Screen.height - screenEdge);

            icon.position= iconPos;

            float distance = Vector3.Distance(iconPos,Camera.main.WorldToScreenPoint(target.position));

            
            if (distance < hideDis && !isNewTarget)
            {
                icon.gameObject.SetActive(false);
                isNewTarget = true;
            }
            else
            {
                icon.gameObject.SetActive(true);
                icon.position = iconPos;
                isNewTarget = false;
            }          
        }
    }

    public void SetTarget(Transform newTarget)
    {
        
        target = newTarget;

        if (target != null)
        {
            Vector3 iconPos = Camera.main.WorldToScreenPoint(target.position);
            icon.gameObject.SetActive(true);
        }
    }
}
