using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatusItem : MonoBehaviour
{
    

    [SerializeField] private List<StatusInfo> _statusCollection = new List<StatusInfo>();

    public List<StatusInfo> StatusCollection => _statusCollection;
}
