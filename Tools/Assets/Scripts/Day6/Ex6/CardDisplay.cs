using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CardDisplay : MonoBehaviour
{
    public CardData card;
    public Text nameText;
    public Text descriptionText;
    public Text costValue;
    public Image manaTypeColor;
    public Image image;
    public Image frameColor; 

    void Start()
    {
        nameText.text = card.name;
        descriptionText.text = card.description;
        costValue.text = card.cost.ToString();
        //image.sprite = Sprite.Create(card.sprite, new Rect(0.0f, 0.0f, card.sprite.width, card.sprite.height), new Vector2(0.5f, 0.5f), 100.0f);
        frameColor.color = card.backGroundColor;

        switch (card.manaType)
        {
            case ManaEnum.Air:
                {
                    manaTypeColor.color = Color.cyan;
                    break;
                }
            case ManaEnum.Flame:
                {
                    manaTypeColor.color = Color.red;
                    break;
                }
            case ManaEnum.Water:
                {
                    manaTypeColor.color = Color.green;
                    break;
                }
            case ManaEnum.Wind:
                {
                    manaTypeColor.color = Color.red;
                    break;
                }
            default:
                {
                    manaTypeColor.color = Color.grey;
                    break;
                }
        }
    }
}
