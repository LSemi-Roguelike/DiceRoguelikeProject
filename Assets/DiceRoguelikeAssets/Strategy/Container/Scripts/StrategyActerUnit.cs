using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike.Strategy
{
    public abstract class StrategyActerUnit : StrategyContainer
    {
        public static float InitTurnCount = 100.0f;
        protected enum ActType { WaitAction, SelectAction, SelectTarget, TurnEnd }
        protected ActType nowAct;

        [HideInInspector] public float turnCount;

        protected List<StrategyAction> _actions;
        protected int turnPoint;
        private bool _onTurn;

        public new ActerUnit Unit { get { return base.Unit as ActerUnit; } }
        public bool OnTurn => _onTurn;
        public bool isDead => Unit.IsDead;

        public override void Init()
        {
            base.Init();
            ResetTurnCount();
            Unit.SetActionCallback(SetActions);
        }

        public float ProgressTurn(float progress)
        {
            return turnCount -= (Unit.TotalAbility.speed + 10) * progress;
        }

        protected void ResetTurnCount()
        {
            turnCount += InitTurnCount;
        }

        protected void SetActions(List<ActionSkill> actions)
        {
            _actions = new List<StrategyAction>();
            for (int i = 0; i < actions.Count; i++)
            {
                _actions.Add(new StrategyAction(actions[i], cellPos));
            }
            nowAct = ActType.SelectAction;
        }

        protected abstract void TurnStart();
        protected abstract void WaitAction();
        protected abstract void SelectAction();
        protected abstract void SelectTarget();
        protected abstract void TurnEnd();

        public IEnumerator GetTurn()
        {
            _onTurn = true;
            nowAct = ActType.WaitAction;
            Unit.Passive();
            Unit.Activate();
            TurnStart();
            Unit.GetSkill();

            while (OnTurn)
            {
                switch (nowAct)
                {
                    case ActType.WaitAction:
                        WaitAction();
                        break;
                    case ActType.SelectAction:
                        SelectAction();
                        break;
                    case ActType.SelectTarget:
                        SelectTarget();
                        break;
                    case ActType.TurnEnd:
                        TurnEnd();
                        _onTurn = false;
                        break;
                    default:
                        break;
                }
                yield return null;
            }
            ResetTurnCount();
            TurnManager.manager.TurnRotate();
        }

        protected bool Acting(StrategyAction action, Vector3Int targetPos)
        {
            if (action.skill is MainSkill)
            {
                var targets = action.targets;
                foreach (var target in targets)
                {
                    if (target.cellPos == targetPos)
                    {
                        if (!((action.skill as MainSkill).TargetCheck(target)))
                        {
                            Debug.Log("incorrect target");
                            return false;
                        }
                        nowAct = ActType.WaitAction;
                        StartCoroutine(SkillCast(action.skill as MainSkill, target));
                        return true;
                    }
                }
                return false;
            }
            else
            {
                var rangeRoutes = action.routes;
                Route targetRoute = null;
                for (int i = 0; i < rangeRoutes.Length; i++)
                {
                    if (rangeRoutes[i].pos == targetPos)
                    {
                        targetRoute = rangeRoutes[i];
                    }
                }
                if (targetRoute == null)
                    return false;

                //set move route
                List<Vector3> moveRoute = new List<Vector3>();
                while (targetRoute.preRoute != null)
                {
                    moveRoute.Insert(0, TileMapManager.manager.CellToWorld(targetRoute.pos));
                    targetRoute = targetRoute.preRoute;
                }
                nowAct = ActType.WaitAction;
                StartCoroutine(MoveTo(moveRoute));
                return true;
            }
        }

        protected IEnumerator SkillCast(MainSkill skill, StrategyContainer target)
        {
            skill.Cast(target);
            Unit.Attack();
            yield return new WaitForSeconds(skill.delayTime);
            nowAct = ActType.SelectAction;
        }

        protected IEnumerator MoveTo(List<Vector3> moveRoute)
        {
            //on moving
            int moveCount = 0;
            while (moveRoute.Count > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, moveRoute[0], Utils.tileMoveSpeed * Time.deltaTime);
                if (transform.position == moveRoute[0])
                {
                    Vector3Int temp = TileMapManager.manager.WorldToCell(moveRoute[0]);
                    TileMapManager.manager.MoveUnit(cellPos, temp);
                    cellPos = temp;
                    moveRoute.RemoveAt(0);
                    moveCount++;
                }
                yield return null;
            }
            Unit.Special();
            nowAct = ActType.SelectAction;
        }

        protected void Death()
        {
            TurnManager.manager.RemoveUnit(this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}