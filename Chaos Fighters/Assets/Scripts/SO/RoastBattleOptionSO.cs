using UnityEngine;

[CreateAssetMenu(menuName = "Game/Roast Battle Option", fileName = "New Roast Battle Option")]
public class RoastBattleOptionSO : ScriptableObject
{
    [SerializeField][TextArea] string sentence;
    [SerializeField] RoastOption[] player1Options;
    [SerializeField] RoastOption[] player2Options;

    public string Sentence => sentence;
    public RoastOption[] Player1Options => player1Options;
    public RoastOption[] Player2Options => player2Options;
}

[System.Serializable]
public class RoastOption
{
    [HorizontalGroup(2)]
    [SerializeField][TextArea] string option;
    [SerializeField][HideInInspector] int strength;

    public string Option => option;
    public int Strength => strength;
}