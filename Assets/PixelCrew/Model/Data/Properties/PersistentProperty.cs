namespace Assets.PixelCrew.Model.Data.Properties {
    public abstract class PersistentProperty<TPropertyType> : ObservableProperty<TPropertyType> {

        protected TPropertyType _stored;
        private readonly TPropertyType _defaultValue;

        protected abstract void Write(TPropertyType value);
        protected abstract TPropertyType Read(TPropertyType defaultValue);

        public PersistentProperty(TPropertyType defaultValue) {
            _defaultValue = defaultValue;
        }

        public override TPropertyType Value {
            get => _stored;
            set {
                var isEquals = _stored.Equals(value);
                if (isEquals) return;
                var oldValue = _stored;
                Write(value);
                _stored = _value = value;
                InvokeChangedEvent(value, oldValue);
            }
        }

        protected void Init() {
            _stored = _value = Read(_defaultValue);
        }

        public void Validate() {
            if (!_stored.Equals(_value)) {
                Value = _value;
            }
        }
    }
}