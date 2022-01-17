using System;

namespace Assets.PixelCrew.Model.Data.Properties {

    [Serializable]
    public class StringProperty : ObservableProperty<string> {
        public StringProperty() {
            _value = string.Empty;
        }
    }
}
