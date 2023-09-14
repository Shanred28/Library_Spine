using UnityEngine.Events;

namespace Test
{
    public class LevelController : SingletonBase<LevelController>
    {
        public static UnityEvent FinishingEvent = new UnityEvent();

        private void Start()
        {
            Player.Instance.onDeadEvent.AddListener(GameOver);
            TriggerFinish.FinishTriggerEvent.AddListener(WinLevel);        }

        private void GameOver() 
        {
            PanelResult.Instance.FinishLevel(false);
            Player.Instance.onDeadEvent.RemoveListener(GameOver);
            var enem�ies = FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enem�ies)
            {
                enemy.StopZombiWin();
            }           
        }
        private void WinLevel()
        {
            PanelResult.Instance.FinishLevel(true);
            Player.Instance.FinishLevelWin();
            TriggerFinish.FinishTriggerEvent.RemoveListener(WinLevel);
        }
    }
}

