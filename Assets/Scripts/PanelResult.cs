using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Test
{
    public class PanelResult : SingletonBase<PanelResult>
    {
        [SerializeField] private TMP_Text _textFinishResult;
        [SerializeField] private Button _buttonFinish;
        [SerializeField] private TMP_Text _textButtonResult;

        private void Start()
        {
            gameObject.SetActive(false);
        }
        public void FinishLevel(bool finish)
        {
            gameObject.SetActive(true);
            _textFinishResult.text = finish ? "Победа!" : "Увы, тыквы победили. Может еще разок?";
            _textButtonResult.text = finish ? "Следующий уровень" : "Еще раз!";
        }
        public void OnClickButtonFinish()
        {
            SceneManager.LoadScene(0);
        }
    }
}

