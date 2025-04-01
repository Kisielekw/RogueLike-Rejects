using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] PlayerData _playerData;
    [SerializeField] GameObject _digit0, _digit1, _digit2;
    [SerializeField] Sprite[] _digits;

    void Update()
    {
        //get the first second and 3rd digit of the score
        int digit2 = _playerData.Score % 10;
        int digit1 = (_playerData.Score / 10) % 10;
        int digit0 = (_playerData.Score / 100) % 10;

        _digit2.GetComponent<Image>().sprite = _digits[digit2];
        if(digit0 > 0 || digit1 > 0)
            _digit1.GetComponent<Image>().sprite = _digits[digit1];
        else
            _digit1.GetComponent<Image>().sprite = _digits[10];

        _digit0.GetComponent<Image>().sprite = digit0 > 0 ? _digits[digit0] : _digits[10];
    }
}
