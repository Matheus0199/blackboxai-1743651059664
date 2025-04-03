using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    // Player stats
    public int reputation = 100;
    public int salary = 5000;
    public int day = 1;
    public int correctReviews = 0;
    public int incorrectReviews = 0;
    
    // Game settings
    public int declarationsPerDay = 5;
    public int currentDeclarationIndex = 0;
    
    public List<Declaration> todaysDeclarations = new List<Declaration>();
    public List<NewsEvent> todaysNews = new List<NewsEvent>();
    public List<Dilemma> todaysDilemmas = new List<Dilemma>();
    
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
    
    public void StartNewDay()
    {
        day++;
        currentDeclarationIndex = 0;
        GenerateDailyContent();
    }
    
    void GenerateDailyContent()
    {
        // Generate declarations, news and dilemmas for the day
        todaysDeclarations.Clear();
        for(int i = 0; i < declarationsPerDay; i++)
        {
            todaysDeclarations.Add(Declaration.GenerateRandom());
        }
        
        // Generate 1-3 news events
        int newsCount = Random.Range(1, 4);
        for(int i = 0; i < newsCount; i++)
        {
            todaysNews.Add(NewsEvent.GenerateRandom());
        }
        
        // 30% chance for a dilemma
        if(Random.value < 0.3f)
        {
            todaysDilemmas.Add(Dilemma.GenerateRandom());
        }
    }
    
    public void ProcessDeclarationReview(bool approved, bool wasCorrect)
    {
        if(wasCorrect)
        {
            correctReviews++;
            reputation += 5;
        }
        else
        {
            incorrectReviews++;
            reputation -= 10;
            salary -= 200;
        }
        
        currentDeclarationIndex++;
        if(currentDeclarationIndex >= declarationsPerDay)
        {
            // End of day
            StartNewDay();
        }
    }
    
    public void ProcessDilemmaChoice(bool acceptedBribe)
    {
        if(acceptedBribe)
        {
            salary += 1000;
            reputation -= 20;
        }
        else
        {
            reputation += 10;
        }
    }
}