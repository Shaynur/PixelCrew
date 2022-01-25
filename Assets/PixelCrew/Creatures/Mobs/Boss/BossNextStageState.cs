using Assets.PixelCrew.Components.GoBase;
using Assets.PixelCrew.Creatures.Mobs.Boss;
using UnityEngine;

public class BossNextStageState : StateMachineBehaviour {

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var spawner = animator.GetComponent<CircularProjectileSpawner>();
        if (spawner != null) {
            spawner.Stage++;
        }
        var changeLight = animator.GetComponent<ChangeLightsComponent>();
        changeLight.SetColor();

    }
}
