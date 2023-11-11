using UnityEngine;
using Services.Analytics.UnityAnalytics;

namespace Services.Analytics
{
    internal class AnalyticsManager : MonoBehaviour
    {
        private static AnalyticsManager _instance;
        internal static AnalyticsManager Instance { get => Instance = _instance; private set => _instance = value; }
        
        private IAnalyticsService[] _services;

        private void Awake()
        {
            InitializeAnalyticsManager();
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService()
            };
        }

        private void InitializeAnalyticsManager()
        {
            if (_instance == null)
            {
                _instance = this;
                return;
            }
            Destroy(gameObject);
        }

        public void SendMainMenuOpened() =>
            SendEvent("MainMenuOpened");

        public void SendGameStarted() =>
            SendEvent("Game Started");


        private void SendEvent(string eventName)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendEvent(eventName);
        }
    }
}
