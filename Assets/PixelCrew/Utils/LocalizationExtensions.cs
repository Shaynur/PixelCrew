using Assets.PixelCrew.Model.Definitions.Localization;

namespace Assets.PixelCrew.Utils {

    public static class LocalizationExtensions {

        public static string Localize(this string key) {
            return LocalizationManager.I.Localize(key);
        }
    }
}