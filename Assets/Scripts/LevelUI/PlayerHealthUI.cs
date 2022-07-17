using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _healthSprites;
    
    [SerializeField]
    private Image _healthImage;
    

    public void OnPlayerHealthChange(EventData data)
    {
        PlayerHealthEventData playerHealth = (PlayerHealthEventData)data;
        _healthImage.sprite = _healthSprites[playerHealth.Health];
    }
}
