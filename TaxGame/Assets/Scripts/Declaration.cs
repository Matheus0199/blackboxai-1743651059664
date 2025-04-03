using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Declaration
{
    public string taxpayerName;
    public string profession;
    public float income;
    public List<DeductibleExpense> expenses;
    public string suspiciousComments;
    public bool hasInconsistencies;
    public List<string> inconsistencies;
    
    public static Declaration GenerateRandom()
    {
        Declaration declaration = new Declaration();
        
        // Generate random taxpayer data
        string[] firstNames = {"João", "Maria", "Carlos", "Ana", "Pedro", "Lucia"};
        string[] lastNames = {"Silva", "Santos", "Oliveira", "Souza", "Rodrigues"};
        string[] professions = {"Médico", "Engenheiro", "Professor", "Advogado", "Empresário"};
        
        declaration.taxpayerName = firstNames[Random.Range(0, firstNames.Length)] + " " + 
                                  lastNames[Random.Range(0, lastNames.Length)];
        declaration.profession = professions[Random.Range(0, professions.Length)];
        declaration.income = Random.Range(2000, 20000);
        
        // Generate random expenses
        declaration.expenses = new List<DeductibleExpense>();
        int expenseCount = Random.Range(1, 5);
        for(int i = 0; i < expenseCount; i++)
        {
            declaration.expenses.Add(DeductibleExpense.GenerateRandom());
        }
        
        // 40% chance of having inconsistencies
        declaration.hasInconsistencies = Random.value < 0.4f;
        if(declaration.hasInconsistencies)
        {
            declaration.inconsistencies = new List<string>();
            int inconsistencyCount = Random.Range(1, 3);
            
            string[] possibleInconsistencies = {
                "Renda declarada inferior à média da profissão",
                "Despesas médicas superiores à renda",
                "Deduções de educação sem comprovantes",
                "Aluguel declarado como residência própria",
                "Doações superiores ao limite permitido"
            };
            
            for(int i = 0; i < inconsistencyCount; i++)
            {
                declaration.inconsistencies.Add(
                    possibleInconsistencies[Random.Range(0, possibleInconsistencies.Length)]
                );
            }
            
            declaration.suspiciousComments = "Declaração requer análise cuidadosa";
        }
        else
        {
            declaration.suspiciousComments = "Declaração parece regular";
        }
        
        return declaration;
    }
}

[System.Serializable]
public class DeductibleExpense
{
    public enum ExpenseType { Health, Education, Dependents, Other }
    
    public ExpenseType type;
    public float amount;
    public string description;
    
    public static DeductibleExpense GenerateRandom()
    {
        DeductibleExpense expense = new DeductibleExpense();
        expense.type = (ExpenseType)Random.Range(0, 4);
        expense.amount = Random.Range(100, 2000);
        
        switch(expense.type)
        {
            case ExpenseType.Health:
                expense.description = "Despesas médicas/hospitalares";
                break;
            case ExpenseType.Education:
                expense.description = "Mensalidades escolares/universitárias";
                break;
            case ExpenseType.Dependents:
                expense.description = "Despesas com dependentes";
                break;
            default:
                expense.description = "Outras despesas dedutíveis";
                break;
        }
        
        return expense;
    }
}