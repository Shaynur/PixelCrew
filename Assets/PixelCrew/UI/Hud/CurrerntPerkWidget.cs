using Assets.PixelCrew.Model;
using Assets.PixelCrew.Model.Definitions.Repository;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.PixelCrew.UI.Hud {

    public class CurrerntPerkWidget : MonoBehaviour {

        [SerializeField] private Image _icon;
        [SerializeField] private Image _cooldownImage;

        public void Set(PerkDef perkDef) {
            _icon.sprite = perkDef.Icon;
        }

        private void Update() {
            var cooldown = GameSession.Instance.PerksModel.Cooldown;
            _cooldownImage.fillAmount = cooldown.RemainingTime / cooldown.Value;
        }
    }
}