using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialController : MonoBehaviour
{
    [Header("Tutorial UI Elements")]
    public GameObject tutorialPanel;
    public TMP_Text tutorialTitle;
    public TMP_Text tutorialContent;
    public Button nextButton;
    public Button skipButton;

    [Header("Tutorial Steps")]
    public TutorialStep[] steps;
    private int currentStep = 0;

    void Start()
    {
        tutorialPanel.SetActive(false);
        nextButton.onClick.AddListener(NextStep);
        skipButton.onClick.AddListener(SkipTutorial);
    }

    public void StartTutorial()
    {
        tutorialPanel.SetActive(true);
        currentStep = 0;
        ShowCurrentStep();
    }

    void ShowCurrentStep()
    {
        if (currentStep < steps.Length)
        {
            tutorialTitle.text = steps[currentStep].title;
            tutorialContent.text = steps[currentStep].content;
        }
        else
        {
            EndTutorial();
        }
    }

    void NextStep()
    {
        currentStep++;
        ShowCurrentStep();
    }

    void SkipTutorial()
    {
        EndTutorial();
    }

    void EndTutorial()
    {
        tutorialPanel.SetActive(false);
        GameManager.Instance.StartNewDay();
    }
}

[System.Serializable]
public class TutorialStep
{
    public string title;
    [TextArea(3, 10)]
    public string content;
}