using UnityEngine;
using UnityEngine.UI;

public class EclipsManager : MonoBehaviour
{
    [SerializeField] private EclipseData _eclipseData;
    [SerializeField] private Slider eclipseSlider;
    [SerializeField] private Image eclipseUp;
    [SerializeField] private Image eclipseDown;
    [SerializeField] private GameObject eclipseEdge;

    void Start()
    {
        _eclipseData.ResetEclipseTimer();
    }
    void Update()
    {
        _eclipseData.UpdateEclipseTimer(Time.deltaTime);

        if (_eclipseData.GetTime() >= _eclipseData.eclipsTimeStart)
        {
            eclipseSlider.value = 1 - (_eclipseData.GetTime() - _eclipseData.eclipsTimeStart) / _eclipseData.eclipsDuration;
            eclipseUp.enabled = false;
            eclipseDown.enabled = true;
            eclipseEdge.SetActive(true);
        }
        else
        {
            eclipseSlider.value = _eclipseData.GetTime() / _eclipseData. eclipsTimeStart;
            eclipseUp.enabled = true;
            eclipseDown.enabled = false;
            eclipseEdge.SetActive(false);
        }
    }
}
