using UnityEngine;

public class EclipsManager : MonoBehaviour
{
    [SerializeField] private EclipseData _eclipseData;

    void Start()
    {
        _eclipseData.ResetEclipseTimer();
    }
    void Update()
    {
        _eclipseData.UpdateEclipseTimer(Time.deltaTime);
    }
}
