using UnityEngine;

namespace Assets.PixelCrew.Creatures.Mobs.Boss {

    public class BossFloodState : StateMachineBehaviour {

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

            var spawner = animator.GetComponent<FloodController>();

            if (spawner != null)
                spawner.StartFlooding();
        }
    }
}