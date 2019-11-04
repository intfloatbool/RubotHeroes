using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusPanel : MonoBehaviour
{
    [SerializeField] private Image _hpBar;
    [SerializeField] private Image _colorIdentyIcon;
    [SerializeField] private RobotStatus _robotStatus;
    [SerializeField] private float _changeSpeed = 4f;

    private float _robotMaxHP;
    private float _neededValue = 1f;

    private void Start()
    {
        InitializeStatusPanel();
    }

    private void Update()
    {
        if (Mathf.Approximately(_hpBar.fillAmount, _neededValue))
            return;
        
        _hpBar.fillAmount = Mathf.Lerp(_hpBar.fillAmount, _neededValue, _changeSpeed * Time.deltaTime);
    }

    public void InitializeStatusPanel()
    {
        _robotMaxHP = _robotStatus.HealthPoints;
        this._robotStatus.OnDamaged += OnHpChanged;

        InitializeColor();
    }

    private void InitializeColor()
    {
        Color colorForPlayer = PlayerIdenty.Instance.GetColorByOwner(_robotStatus.Owner);
        _colorIdentyIcon.color = colorForPlayer;
    }

    private void OnHpChanged(float currentHP)
    {
        _neededValue = 1f / (_robotMaxHP / currentHP);
    }
}
