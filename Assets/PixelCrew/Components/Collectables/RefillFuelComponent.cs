using Assets.PixelCrew.Model;
using UnityEngine;

namespace Assets.PixelCrew.Components.Collectables {

    public class RefillFuelComponent : MonoBehaviour {

        public void Refill() {
            GameSession.Instance.Data.Fuel.Value = 100;
        }
    }
}