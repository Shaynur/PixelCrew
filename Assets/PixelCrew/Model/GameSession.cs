using System.Collections;
using Assets.PixelCrew.Model.Data;
using UnityEngine;

namespace Assets.PixelCrew.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;
        private PlayerData _save;

        private void Awake()
        {
            if (IsSessionExist())
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(this);
            }
        }

        private void Start()
        {
            SavePlayerData();
        }

        public void SavePlayerData()
        {
            _save = _data.Clone();
        }

        public void LoadPlayerData()
        {
            _data = _save.Clone();
        }

        private bool IsSessionExist()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions)
            {
                if (gameSession != this)
                {
                    return true;
                }
            }
            return false;
        }

    }
}