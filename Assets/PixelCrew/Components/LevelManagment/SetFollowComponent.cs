using Assets.PixelCrew.Creatures.Hero;
using Cinemachine;
using UnityEngine;

namespace Assets.PixelCrew.Components.LevelManagment {

    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class SetFollowComponent : MonoBehaviour {

        private void Start() {
            var vCamera = GetComponent<CinemachineVirtualCamera>();
            vCamera.Follow = FindObjectOfType<Hero>().transform;
        }
    }
}