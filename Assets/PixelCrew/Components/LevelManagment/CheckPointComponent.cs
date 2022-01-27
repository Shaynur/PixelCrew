using Assets.PixelCrew.Components.GoBase;
using Assets.PixelCrew.Model;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.PixelCrew.Components.LevelManagment {

    [RequireComponent(typeof(SpawnComponent))]
    public class CheckPointComponent : MonoBehaviour {

        [SerializeField] private string _id;
        [SerializeField] private SpawnComponent _heroSpawner;
        [SerializeField] private UnityEvent _setChecked;
        [SerializeField] private UnityEvent _setUnchecked;

        public string Id => _id;

        private void Start() {
            if (GameSession.Instance.IsChecked(_id))
                _setChecked?.Invoke();
            else
                _setUnchecked?.Invoke();
        }

        public void Check() {
            GameSession.Instance.SetChecked(_id);
            _setChecked?.Invoke();
        }

        public void SpawnHero() {
            _heroSpawner.Spawn();
        }
    }
}