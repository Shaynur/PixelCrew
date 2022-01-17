using Assets.PixelCrew.Model.Data;
using UnityEngine;

namespace Assets.PixelCrew.UI.Hud.Dialogs {
    public class PersonalizedDialogBoxController : DialogBoxController {

        [SerializeField] private DialogContent _right;

        protected override DialogContent CurrentContent => CurrentSentence.Side == Side.Left ? _content : _right;

        protected override void OnStartDialogAnimation() {
            SetContentsActivity();
            base.OnStartDialogAnimation();
        }

        public override void ShowDialog(DialogData data) {
            base.ShowDialog(data);
            SetContentsActivity();
        }

        private void SetContentsActivity() {
            _right.gameObject.SetActive(CurrentSentence.Side == Side.Right);
            _content.gameObject.SetActive(CurrentSentence.Side == Side.Left);
        }
    }
}