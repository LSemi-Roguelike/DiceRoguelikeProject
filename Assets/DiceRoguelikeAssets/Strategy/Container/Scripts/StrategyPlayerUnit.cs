using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Strategy
{
    public class StrategyPlayerUnit : StrategyActerUnit
    {
        public new PlayerUnit Unit => base.Unit as PlayerUnit;

        protected int _selectedAction;

        public override void Init()
        {
            _unit = PlayerManager.Instance.Player;
            base.Init();
        }

        private void SelectAction(int actNum)
        {
            _selectedAction = actNum;
            if(_selectedAction >= 0)
                nowAct = ActType.SelectTarget;
            else
            {
                RangeViewManager.Inst.HideRange();
                nowAct = ActType.TurnEnd;
            }
        }

        protected override void TurnStart()
        {

        }

        protected override void WaitAction()
        {

        }

        protected override void SelectAction()
        {
            if (_actions.Count == 0)
            {
                nowAct = ActType.TurnEnd;
                return;
            }
            foreach (var action in _actions)
                action.SetRange(cellPos);

            ActionSelectUI.Inst.SetActionUI(_actions, SelectAction);
            nowAct = ActType.WaitAction;
        }

        protected override void SelectTarget()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3Int pos = InputManager.manager.GetMouseCellPos();
                if (Acting(_actions[_selectedAction], pos))
                {
                    _actions.RemoveAt(_selectedAction);
                    RangeViewManager.Inst.HideRange();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                RangeViewManager.Inst.HideRange();
                nowAct = ActType.SelectAction;
            }
        }

        protected override void TurnEnd()
        {
            RangeViewManager.Inst.HideRange();
        }

    }
}