using System;
using System.Collections;
using UnityEngine;

namespace Assets.PixelCrew.Model.Data {

    [Serializable]
    public class DialogData {

        [SerializeField] private Sentence[] _sentences;
        [SerializeField] private DialogType _type;
        public Sentence[] Sentences => _sentences;
        public DialogType Type => _type;
    }

    [Serializable]
    public struct Sentence {
        [SerializeField] private string _valued;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Side _side;
        public string Valued => _valued;
        public Sprite Icon => _icon;
        public Side Side => _side;
    }

    public enum Side {
        Left,
        Right
    }

    public enum DialogType {
        Simple,
        Personalized
    }
}