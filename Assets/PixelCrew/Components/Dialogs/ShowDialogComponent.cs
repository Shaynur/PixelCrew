using System;
using Assets.PixelCrew.Model.Data;
using Assets.PixelCrew.Model.Definitions;
using Assets.PixelCrew.UI.Hud.Dialogs;
using UnityEngine;

namespace Assets.PixelCrew.Components.Dialogs {

    public class ShowDialogComponent : MonoBehaviour {

        [SerializeField] private Mode _mode;
        [SerializeField] private DialogData _bound;
        [SerializeField] private DialogDef _external;

        public DialogData Data {
            get {
                switch (_mode) {
                    case Mode.Bound:
                        return _bound;
                    case Mode.External:
                        return _external.Data;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private DialogBoxController _dialogBox;
        public void Show() {
            if (_dialogBox == null) {
                _dialogBox = FindObjectOfType<DialogBoxController>();
            }
            _dialogBox.ShowDialog(Data);
        }

        public void Show(DialogDef def) {
            _external = def;
            Show();
        }

        public enum Mode {
            Bound,
            External
        }
    }
}