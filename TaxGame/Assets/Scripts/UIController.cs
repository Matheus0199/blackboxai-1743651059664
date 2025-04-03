using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    [Header("Main Game UI")]
    public GameObject mainPanel;
    public TMP_Text dayText;
    public TMP_Text reputationText;
    public TMP_Text salaryText;
    public TMP_Text declarationsReviewedText;

    [Header("Declaration Review UI")]
    public GameObject declarationPanel;
    public TMP_Text taxpayerNameText;
    public TMP_Text professionText;
    public TMP_Text incomeText;
    public Transform expensesContainer;
    public GameObject expensePrefab;
    public TMP_Text suspiciousCommentsText;
    public TMP_Text inconsistenciesText;
    public Button approveButton;
    public Button rejectButton;

    [Header("News UI")]
    public GameObject newsPanel;
    public TMP_Text newsHeadlineText;
    public TMP_Text newsContentText;
    public TMP_Text ruleChangeText;
    public Button continueFromNewsButton;

    [Header("Dilemma UI")]
    public GameObject dilemmaPanel;
    public TMP_Text dilemmaDescriptionText;
    public TMP_Text bribeAmountText;
    public TMP_Text consequencesText;
    public Button acceptButton;
    public Button refuseButton;

    private Declaration currentDeclaration;
    private List<NewsEvent> pendingNews = new List<NewsEvent>();
    private Dilemma currentDilemma;

    void Start()
    {
        // Initialize UI elements
        UpdatePlayerStats();
        ShowMainPanel();

        // Set up button listeners
        approveButton.onClick.AddListener(() => ReviewDeclaration(true));
        rejectButton.onClick.AddListener(() => ReviewDeclaration(false));
        continueFromNewsButton.onClick.AddListener(ContinueFromNews);
        acceptButton.onClick.AddListener(() => ResolveDilemma(true));
        refuseButton.onClick.AddListener(() => ResolveDilemma(false));

        // Start the first day
        GameManager.Instance.GenerateDailyContent();
        ShowNextDeclaration();
    }

    void UpdatePlayerStats()
    {
        dayText.text = $"Dia: {GameManager.Instance.day}";
        reputationText.text = $"Reputação: {GameManager.Instance.reputation}";
        salaryText.text = $"Salário: R$ {GameManager.Instance.salary}";
        declarationsReviewedText.text = $"Revisadas: {GameManager.Instance.correctReviews + GameManager.Instance.incorrectReviews}";
    }

    public void ShowNextDeclaration()
    {
        if (pendingNews.Count > 0)
        {
            ShowNews(pendingNews[0]);
            pendingNews.RemoveAt(0);
            return;
        }

        if (GameManager.Instance.todaysDilemmas.Count > 0 && 
            Random.value < 0.3f) // 30% chance to show dilemma
        {
            currentDilemma = GameManager.Instance.todaysDilemmas[0];
            ShowDilemma(currentDilemma);
            GameManager.Instance.todaysDilemmas.RemoveAt(0);
            return;
        }

        if (GameManager.Instance.currentDeclarationIndex >= GameManager.Instance.declarationsPerDay)
        {
            // End of day
            GameManager.Instance.StartNewDay();
            UpdatePlayerStats();
        }

        currentDeclaration = GameManager.Instance.todaysDeclarations[GameManager.Instance.currentDeclarationIndex];
        ShowDeclaration(currentDeclaration);
    }

    void ShowDeclaration(Declaration declaration)
    {
        declarationPanel.SetActive(true);
        mainPanel.SetActive(false);
        newsPanel.SetActive(false);
        dilemmaPanel.SetActive(false);

        taxpayerNameText.text = $"Contribuinte: {declaration.taxpayerName}";
        professionText.text = $"Profissão: {declaration.profession}";
        incomeText.text = $"Renda Anual: R$ {declaration.income:F2}";
        suspiciousCommentsText.text = $"Comentários: {declaration.suspiciousComments}";

        // Clear previous expenses
        foreach (Transform child in expensesContainer)
        {
            Destroy(child.gameObject);
        }

        // Add current expenses
        foreach (var expense in declaration.expenses)
        {
            var expenseObj = Instantiate(expensePrefab, expensesContainer);
            var expenseText = expenseObj.GetComponent<TMP_Text>();
            expenseText.text = $"{expense.description}: R$ {expense.amount:F2}";
        }

        // Show inconsistencies if any
        if (declaration.hasInconsistencies && declaration.inconsistencies.Count > 0)
        {
            inconsistenciesText.gameObject.SetActive(true);
            inconsistenciesText.text = "Possíveis inconsistências:\n";
            foreach (var inc in declaration.inconsistencies)
            {
                inconsistenciesText.text += $"- {inc}\n";
            }
        }
        else
        {
            inconsistenciesText.gameObject.SetActive(false);
        }
    }

    void ReviewDeclaration(bool approved)
    {
        bool wasCorrect = (approved && !currentDeclaration.hasInconsistencies) || 
                         (!approved && currentDeclaration.hasInconsistencies);

        GameManager.Instance.ProcessDeclarationReview(approved, wasCorrect);
        UpdatePlayerStats();
        ShowMainPanel();
        ShowNextDeclaration();
    }

    void ShowNews(NewsEvent news)
    {
        newsPanel.SetActive(true);
        mainPanel.SetActive(false);
        declarationPanel.SetActive(false);
        dilemmaPanel.SetActive(false);

        newsHeadlineText.text = news.headline;
        newsContentText.text = news.content;

        if (news.affectsRules)
        {
            ruleChangeText.gameObject.SetActive(true);
            ruleChangeText.text = $"Mudança nas regras: {news.ruleChangeDescription}";
        }
        else
        {
            ruleChangeText.gameObject.SetActive(false);
        }
    }

    void ContinueFromNews()
    {
        ShowMainPanel();
        ShowNextDeclaration();
    }

    void ShowDilemma(Dilemma dilemma)
    {
        dilemmaPanel.SetActive(true);
        mainPanel.SetActive(false);
        declarationPanel.SetActive(false);
        newsPanel.SetActive(false);

        dilemmaDescriptionText.text = dilemma.description;
        bribeAmountText.text = $"Valor do suborno: R$ {dilemma.bribeAmount}";
        consequencesText.text = $"Se aceitar: {dilemma.consequenceIfAccepted}\n\nSe recusar: {dilemma.consequenceIfRejected}";
    }

    void ResolveDilemma(bool accepted)
    {
        GameManager.Instance.ProcessDilemmaChoice(accepted);
        UpdatePlayerStats();
        ShowMainPanel();
        ShowNextDeclaration();
    }

    void ShowMainPanel()
    {
        mainPanel.SetActive(true);
        declarationPanel.SetActive(false);
        newsPanel.SetActive(false);
        dilemmaPanel.SetActive(false);
    }
}