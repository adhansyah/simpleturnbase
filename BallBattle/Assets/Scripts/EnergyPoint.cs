using UnityEngine;
using UnityEngine.UI;

public class EnergyPoint : MonoBehaviour
{
    public bool isEnemy = false;
    public GameObject energyPointValue;

    public void SetColor(bool _isEnemy)
    {
        isEnemy = _isEnemy;
        if (_isEnemy) {
            energyPointValue.GetComponent<Image>().color = Params.enemyColor;
        }
        else {
            energyPointValue.GetComponent<Image>().color = Params.playerColor;
        }
    }

    public void SetValue(float _perc) {
        RectTransform rt = energyPointValue.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().rect.width * _perc, rt.rect.height);
        Color c = energyPointValue.GetComponent<Image>().color;
        if (_perc < 1 && _perc > 0)
        {
            energyPointValue.GetComponent<Image>().color = new Color(c.r, c.g, c.b, .5f);
        }
        if (_perc == 1)
        {
            energyPointValue.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 1f);
        }
    }
}
