using UnityEngine;

[CreateAssetMenu(fileName = "EclipseData", menuName = "Scriptable Objects/EclipseData")]
public class EclipseData : ScriptableObject
{
    public float eclipsTimeStart;
    public float eclipsDuration;
    public int eclipseLevel;
    private float _eclipseTimer;

    public bool IsEclipseActive => _eclipseTimer >= eclipsTimeStart;

    public void UpdateEclipseTimer(float deltaTime)
    {
        _eclipseTimer += deltaTime;
        if (_eclipseTimer >= eclipsTimeStart + eclipsDuration)
        {
            _eclipseTimer = 0;
        }
    }
}
