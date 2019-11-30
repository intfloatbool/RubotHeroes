using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusPanel : MonoBehaviour
{
    [SerializeField] private Text _nickName;
    [SerializeField] private Image _hpBar;
    [SerializeField] private Image _colorIdentyIcon;
    [SerializeField] private RobotStatus _robotStatus;
    [SerializeField] private float _changeSpeed = 4f;
    [SerializeField] private Text _energyCountText;

    private float _robotMaxHP;
    private float _neededValue = 1f;

    private void Update()
    {
        if (Mathf.Approximately(_hpBar.fillAmount, _neededValue))
            return;
        
        _hpBar.fillAmount = Mathf.Lerp(_hpBar.fillAmount, _neededValue, _changeSpeed * Time.deltaTime);
    }

    public void InitializeStatusPanel(Player player)
    {
        _nickName.text = player.NickName;
        _robotMaxHP = _robotStatus.HealthPoints;
        _colorIdentyIcon.color = player.Color;
        this._robotStatus.OnHealthChanged += OnHpChanged;
        this._robotStatus.OnChargesChanged += OnChargesChanged;

    }

    private void OnHpChanged(float currentHP)
    {
        _neededValue = 1f / (_robotMaxHP / currentHP);
    }
    
    private void OnChargesChanged(int charges)
    {
        _energyCountText.text = charges.ToString();
    }
}
