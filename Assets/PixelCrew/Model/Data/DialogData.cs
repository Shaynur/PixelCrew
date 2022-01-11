using System;
using System.Collections;
using UnityEngine;

namespace Assets.PixelCrew.Model.Data {

    [Serializable]
    public class DialogData {

        [SerializeField] private string[] _sentences;
        public string[] Sentences => _sentences;
    }
}