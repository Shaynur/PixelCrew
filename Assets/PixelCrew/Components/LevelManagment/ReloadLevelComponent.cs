using Assets.PixelCrew.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.PixelCrew.Components.LevelManagment
{
    public class ReloadLevelComponent : MonoBehaviour
    {
        public void Reload()
        {
            var session = FindObjectOfType<GameSession>();
            session.LoadPlayerData();

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
