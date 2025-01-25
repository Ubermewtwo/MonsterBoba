using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterPartTooltip : MonoBehaviour
{
    [SerializeField] private MonsterPart monsterPart;
    public void SetMonsterPart(MonsterPart monsterPart) => this.monsterPart = monsterPart;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI bobaTeaPartText;
    [SerializeField] private List<TextMeshProUGUI> flavoursText;

    [SerializeField] private Color colorBitter;
    [SerializeField] private Color colorSpicy;
    [SerializeField] private Color colorSweet;
    [SerializeField] private Color colorSalt;

    private Color color;
    /*
     * esto es para el sprite, pero da problemas cuando son dos asi que prefiero quitarlo por ahora
    [SerializeField] private Sprite spriteBitter;
    [SerializeField] private Sprite spriteSpicy;
    [SerializeField] private Sprite spriteSweet;
    [SerializeField] private Sprite spriteSalt;

    [SerializeField] private Image imageInTooltip;
    */

    private void OnEnable()
    {
        nameText.text = monsterPart.Name;
        bobaTeaPartText.text = monsterPart.BobaTeaPart.ToString();
        for (int i = 0; i < monsterPart.Flavours.Count; i++)
        {
            flavoursText[i].gameObject.SetActive(true);
            ChooseColorAndImageForText(i);

            flavoursText[i].text = "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + monsterPart.Flavours.Keys[i].ToString() +  ": " + monsterPart.Flavours.Values[i].ToString() + "</color>";
        }

        for (int i = monsterPart.Flavours.Count; i < flavoursText.Count; i++)
        {
            flavoursText[i].gameObject.SetActive(false);
            flavoursText[i].text = "";
        }
    }

    private void ChooseColorAndImageForText(int indice)
    {
        switch (monsterPart.Flavours.Keys[indice])
        {
            case FlavourType.Bitter:
                color = colorBitter;
                //imageInTooltip.sprite = spriteBitter;
                break;
            case FlavourType.Spicy:
                color = colorSpicy;
                //imageInTooltip.sprite = spriteSpicy;
                break;
            case FlavourType.Sweet:
                color = colorSweet;
                //imageInTooltip.sprite = spriteSweet;
                break;
            case FlavourType.Salty:
                color = colorSalt;
                //imageInTooltip.sprite = spriteSalt;
                break;
            default:
                color = colorSweet;
                //imageInTooltip.sprite = spriteSweet;
                break;
        }
    }
}
