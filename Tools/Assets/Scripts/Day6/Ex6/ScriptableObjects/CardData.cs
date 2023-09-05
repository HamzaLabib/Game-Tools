using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/New")]
public class CardData : ScriptableObject
{
    public new string name = "";
    public string description = "";
    public int cost = 0;
    public ManaEnum manaType = ManaEnum.Air;
    public Color backGroundColor = Color.white;
    public Texture2D sprite;
}
