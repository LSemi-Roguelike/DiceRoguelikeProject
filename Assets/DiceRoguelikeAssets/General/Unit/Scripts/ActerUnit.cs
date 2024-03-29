using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class ActerUnit : BaseUnit
    {
        public abstract void SetActionCallback(System.Action<List<ActionSkill>> action);
        public abstract void GetSkill();
        public abstract void Attack();
        public abstract void Special();
        protected abstract void Damaged();
        public abstract void Passive();
        public override void GetEffect(Effect effect)
        {
            base.GetEffect(effect);
            if(effect.effectType == Effect.EffectType.Main)
                Damaged();
        }
    }
}
