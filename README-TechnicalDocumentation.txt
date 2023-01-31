# THREAT

## Ajout d'une nouvelle menace xxx

1. Créer fichier ScriptableObjects/Actions/xxxOutcomesSO.cs
    - Implémenter "ThreatOutcomesSO"
    - Coder le comportement dans les 3 méthodes Concrete... override
    - utiliser les Hook virtual si besoin déjouter du comportement avant/après les méthodes de base.

2. [Unity] Dossier /data/Cards/Threats : Create=>Card=>ThreatOutcome=>xxxOutcomes
    - Préciser le nom avec [CreateAssetMenu(fileName = "xxxOutcomes", menuName = "Card/ThreatOutcome/xxxOutcomes")]

3. [Unity] : Dossier /data/Cards/Threats : Create=>Card=>Threat
    - La nommer "xxx" et lui assigner le Scriptable Object xxxOutcomes dans le champ Outcomes

4. [Unity] Retourner sur le Scriptable Object créé en 2. "xxxOutcomes" et assigner le Scriptable Object "xxx" au champ "Base Card SO". Cela permet au code de faire le lien entre les outcomes et leur parent.

5. [Unity] Ajouter le nouveau Scriptable Object "xxx" à une location ou autre pour pouvoir la faire spawner