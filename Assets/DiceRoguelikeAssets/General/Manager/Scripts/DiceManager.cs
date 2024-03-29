using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ramdom = UnityEngine.Random;

namespace LSemiRoguelike
{
    public abstract class DiceManager : MonoBehaviour
    {
        protected static DiceManager _instance;
        public static DiceManager Instance => _instance;

        protected PlayerUnit owner;
        protected MainSkill weaponAction;
        protected Dice[] dices;
        protected int power;
        protected System.Action<List<ActionSkill>> returnAction;

        private void Awake()
        {
            _instance = this;
        }

        public  void Init(PlayerUnit owner, MainSkill weaponSkill, Dice[] dices, System.Action<List<ActionSkill>> action)
        {
            this.owner = owner;
            this.weaponAction = weaponSkill;
            this.dices = dices;
            returnAction = action;
            Init();
        }

        protected abstract void Init();

        public abstract void GetActions(int power);
    }
}