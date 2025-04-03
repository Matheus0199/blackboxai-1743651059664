using UnityEngine;

[System.Serializable]
public class Dilemma
{
    public string description;
    public int bribeAmount;
    public string consequenceIfAccepted;
    public string consequenceIfRejected;
    
    public static Dilemma GenerateRandom()
    {
        Dilemma dilemma = new Dilemma();
        
        string[] possibleDescriptions = {
            "Um contribuinte oferece um envelope com dinheiro para aprovar sua declaração irregular.",
            "Seu chefe sugere 'fechar os olhos' para algumas declarações de amigos políticos.",
            "Um colega propõe dividir propinas de declarações suspeitas.",
            "Um empresário oferece um 'presente' em troca de aprovação rápida."
        };
        
        string[] possibleConsequencesAccept = {
            "Você recebe o dinheiro, mas fica preocupado com possíveis investigações.",
            "Seu chefe fica satisfeito, mas você se sente culpado pela corrupção.",
            "Você e seu colega dividem o dinheiro, mas a fraude pode ser descoberta.",
            "O presente é valioso, mas você sabe que cometeu um crime."
        };
        
        string[] possibleConsequencesReject = {
            "Você mantém sua integridade, mas o contribuinte fica furioso.",
            "Seu chefe fica descontente com sua 'falta de flexibilidade'.",
            "Seu colega fica ofendido e pode prejudicar sua carreira.",
            "O empresário ameaça denunciá-lo por 'dificultar os negócios'."
        };
        
        dilemma.description = possibleDescriptions[Random.Range(0, possibleDescriptions.Length)];
        dilemma.bribeAmount = Random.Range(1000, 5001);
        dilemma.consequenceIfAccepted = possibleConsequencesAccept[Random.Range(0, possibleConsequencesAccept.Length)];
        dilemma.consequenceIfRejected = possibleConsequencesReject[Random.Range(0, possibleConsequencesReject.Length)];
        
        return dilemma;
    }
}