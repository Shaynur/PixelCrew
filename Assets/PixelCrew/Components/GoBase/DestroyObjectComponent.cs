using Assets.PixelCrew.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.PixelCrew.Components.GoBase {

    public class DestroyObjectComponent : MonoBehaviour {

        [SerializeField] private GameObject _objectToDestroy;
        [SerializeField] private bool _saveInDestroyedList;

        private string fullObjectName;

        private void Start() {
            if (_saveInDestroyedList) {
                fullObjectName = SceneManager.GetActiveScene().name + "_" + gameObject.name;

                var isDestroyed = FindObjectOfType<GameSession>().RestoreState(fullObjectName);
                if (isDestroyed)
                    Destroy(gameObject);
            }
        }

        public void DestroyObject() {
            if (_saveInDestroyedList) {
                //Debug.Log("Save in 'Destroyed' list: " + fullObjectName);
                FindObjectOfType<GameSession>().StoreState(fullObjectName);
            }
            Destroy(_objectToDestroy);
        }
    }
}