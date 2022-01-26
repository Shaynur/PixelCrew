using Assets.PixelCrew.Components.GoBase;
using UnityEngine;

namespace Assets.PixelCrew.Creatures.Mobs.Boss {

    public class BossShootState : StateMachineBehaviour {

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

            var spawner = animator.GetComponent<CircularProjectileSpawner>();

            if (spawner != null)
                spawner.LaunchProjectiles();
        }
    }
}
