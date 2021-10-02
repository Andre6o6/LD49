using TMPro;
using UnityEngine;

public class UITurnCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameController _game;
    
    private void Awake()
    {
        _game.OnTurnAdvanced.AddListener(RenderTurn);
    }

    private void RenderTurn()
    {
        _text.text = _game.Turn.ToString();
    }
}
