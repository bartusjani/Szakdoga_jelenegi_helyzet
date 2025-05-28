using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorialTrigger : MonoBehaviour
{
    public GameObject combatHelp;
    public GameObject blockHelp;
    public GameObject platformerHelp;
    public List<GameObject> tutorialSteps;
    public List<GameObject> combatSteps;

    int currentStep = 0;
    int helpcounter = 0;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlatformCollider") && helpcounter<1)
        {
            //platform
            helpcounter++;
            platformerHelp.SetActive(true);
            Time.timeScale = 0;
        }
        else if (collision.CompareTag("SecondDoor") && helpcounter < 2)
        {
            //combat
            helpcounter++;
            combatHelp.SetActive(true);
            Time.timeScale = 0;
        }
        else if (collision.CompareTag("BlockCollider") && helpcounter < 3)
        {
            //block
            helpcounter++;
            blockHelp.SetActive(true);
            Time.timeScale = 0;
        }
    }
    private void ShowStep(int index)
    {
        currentStep = index;
        for (int i = 0; i < tutorialSteps.Count; i++)
        {
            tutorialSteps[i].SetActive(i == index);
        }
    }

    public void NextTutorialStep()
    {
        if (currentStep < tutorialSteps.Count - 1)
        {
            tutorialSteps[currentStep].SetActive(false);
            currentStep++;
            tutorialSteps[currentStep].SetActive(true);
        }
    }

    private void ShowCombatStep(int index)
    {
        currentStep = index;
        for (int i = 0; i < combatSteps.Count; i++)
        {
            combatSteps[i].SetActive(i == index);
        }
    }

    public void NextCombatStep()
    {
        if (currentStep < combatSteps.Count - 1)
        {
            combatSteps[currentStep].SetActive(false);
            currentStep++;
            combatSteps[currentStep].SetActive(true);
        }
    }

    public void CloseTutorial(GameObject gameObject)
    {

        gameObject.SetActive(false);
        Time.timeScale = 1;


    }
}
