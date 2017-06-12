//This script triggers the attack animation of the "Tentacle_Plant_Monster" asset
#pragma strict



function TriggerAttack () {
    //When this function gets called, the animation controller gets a message to enabled its "Attack" trigger
    (this.GetComponent("Animator") as UnityEngine.Animator).SetTrigger("Attack");
}