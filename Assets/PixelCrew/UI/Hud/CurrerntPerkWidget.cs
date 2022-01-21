using Assets.PixelCrew.Model;
using Assets.PixelCrew.Model.Definitions.Repository;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.PixelCrew.UI.Hud {

    public class CurrerntPerkWidget : MonoBehaviour {

        [SerializeField] private Image _icon;
        [SerializeField] private Image _cooldownImage;

        private GameSession _session;

        private void Start() {
            _session = FindObjectOfType<GameSession>();
        }

        public void Set(PerkDef perkDef) {
            _icon.sprite = perkDef.Icon;
        }

        private void Update() {
            var cooldown = _session.PerksModel.Cooldown;
            _cooldownImage.fillAmount = cooldown.RemainingTime / cooldown.Value;
        }
    }
}