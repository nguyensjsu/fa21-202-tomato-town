using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBaseState : IAgentState
{
    protected readonly Timer atkTimer = new Timer();
    protected BaseAgent user;
    protected Attack curAttack;

    public abstract void UpdateState();
    public abstract void FixedUpdateState();

    public AttackBaseState(BaseAgent b, Attack a) {
        curAttack = a;
        user = b;
    }

    public void SetAttack(Attack desiredAttack) { curAttack = desiredAttack; }

    public void InitializeState() {
        hitList.Clear();
        hasHitTarget = false;
        atkTimer.ResetTimer();
        user._animator.SetTrigger(curAttack.animationTrigger);
        Debug.Log(curAttack.animationTrigger);
    }

    public void ExitState() {
        atkTimer.ResetTimer();
        user._animator.SetTrigger("endAttack");
    }


    // Allows for additional code to be done when user hits a target
    protected virtual void PerformOnHit() { }

    // Check if given hitbox connected with target hurtbox with adjusted positions
    protected bool IsHitTarget(Hitbox userHit,float userScaleX,Vector2 userPos,
        Hitbox targetHurt,float targetScaleX,Vector2 targetPos) {
        var userFlip = (int)Mathf.Sign(userScaleX);
        userHit.UpdateBox(userPos,userFlip);
        var tarFlip = (int)Mathf.Sign(targetScaleX);
        targetHurt.UpdateBox(targetPos,tarFlip);

        return userHit.CheckHit(targetHurt);
    }

    // Check if given hitbox connected with target hurtbox with adjusted positions
    public bool IsHitTarget(Hitbox[] userHit,GameObject user,Hitbox targetHurt,GameObject target) {
        bool hasHitTarget = false;
        float userScale = user.transform.localScale.x;
        Vector2 userPos = user.transform.position;
        float targetScale = target.transform.localScale.x;
        Vector2 targetPos = target.transform.position;

        DrawHitbox(userHit, user);
        for(int i = 0; i < userHit.Length && !hasHitTarget; i++) {
            hasHitTarget = IsHitTarget(userHit[i],userScale,userPos,targetHurt,targetScale,targetPos);
        }
        return hasHitTarget;
    }

    // Check to see of the current attack's hitbox overlapped with a target
    private bool hasHitTarget;
    private HashSet<BaseAgent> hitList = new HashSet<BaseAgent>();
    protected void Attack(BaseAgent target,bool advanceTime = true) {
        if(advanceTime) atkTimer.AdvanceTime();

        // See if the move has ended
        if(atkTimer.CurrentFrame() >= curAttack.GetTotalFrames()) { user.RevertState(); return; }

        // Check if the move connected during its active frames
        else if(curAttack.IsActive(atkTimer.CurrentFrame()) && !hitList.Contains(target)) {
            // Setup knockback and pushback data
            Vector2 userDir = new Vector2(Mathf.Sign(user.transform.localScale.x),1);

            // Check if attack connected with any target if it has a hitbox
            if(IsHitTarget(curAttack.hitboxes,user.gameObject,target.hurtbox,target.gameObject)) {
                // Apply pushback if haven't already
                if(!hasHitTarget) {
                    Vector2 push = curAttack.pushback * userDir;
                    if(push.x != 0) user.velocity.x = push.x;
                    if(push.y != 0) user.velocity.y = push.y;
                    //Debug.Log(user.velocity);
                }

                hasHitTarget = true;
                PerformOnHit();
                hitList.Add(target);
                var knockback = curAttack.knockback * userDir;
                target.Attacked(knockback);
            }
        }
    }

    protected void Attack(List<BaseAgent> targets) {
        // End the attack if it's done
        if(atkTimer.WaitForXFrames(curAttack.GetTotalFrames())) { user.RevertState(); return; }

        //DrawHitbox(curAttack.hitboxes,user.gameObject);

        // Otherwise attack each enemy
        for(int i = 0; i < targets.Count; i++) { Attack(targets[i],false); }
    }

    // TODO: REMOVE FUNCTION
    protected void DrawHitbox(Hitbox[] userHit,GameObject user) {
        float userScale = user.transform.localScale.x;
        Vector2 userPos = user.transform.position;
        var userFlip = (int)Mathf.Sign(userScale);
        for(int i = 0; i < userHit.Length; i++) {
            userHit[i].UpdateBox(userPos,userFlip);
            userHit[i].DrawBox(Color.red);
        }
    }
}
