using System;
using UnityEngine;

namespace City
{
    public class InstallerCity : MonoBehaviour
    {
        [SerializeField] private RulesInCity _rulesInCity;

        private void Start()
        {
            var json = ServiceLocator.Instance.GetService<IMapPersistenceService>().LoadMap();
            _rulesInCity.Configure(json);
        }
    }
}