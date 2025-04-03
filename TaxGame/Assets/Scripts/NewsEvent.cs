using UnityEngine;

[System.Serializable]
public class NewsEvent
{
    public string headline;
    public string content;
    public bool affectsRules;
    public string ruleChangeDescription;
    
    public static NewsEvent GenerateRandom()
    {
        NewsEvent news = new NewsEvent();
        
        string[] possibleHeadlines = {
            "Governo anuncia mudanças nas regras tributárias",
            "Economia em recessão: impactos no IR",
            "Novo ministro da Fazenda toma posse",
            "Escândalo de corrupção na Receita Federal",
            "Aumento no limite de dedução para educação"
        };
        
        string[] possibleContents = {
            "O governo anunciou hoje novas regras para declaração de IR que entram em vigor imediatamente.",
            "A crise econômica força mudanças nas alíquotas do imposto de renda para diversas categorias.",
            "O novo ministro promete simplificar o sistema tributário nos próximos meses.",
            "Investigação revela esquema de corrupção envolvendo auditores fiscais.",
            "A partir de hoje, o limite para dedução com educação aumenta em 15%."
        };
        
        string[] possibleRuleChanges = {
            "Novo limite para dedução de saúde: R$ 5.000",
            "Alíquota máxima aumentada para 27.5%",
            "Deduções para famílias com mais de 3 filhos aumentam em 20%",
            "Obrigatoriedade de comprovante para todas as deduções",
            "Novas profissões isentas de declaração"
        };
        
        news.headline = possibleHeadlines[Random.Range(0, possibleHeadlines.Length)];
        news.content = possibleContents[Random.Range(0, possibleContents.Length)];
        news.affectsRules = Random.value < 0.6f;
        
        if(news.affectsRules)
        {
            news.ruleChangeDescription = possibleRuleChanges[Random.Range(0, possibleRuleChanges.Length)];
        }
        
        return news;
    }
}