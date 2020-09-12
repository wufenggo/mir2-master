using System.Collections.Generic;
using Server.MirDatabase;
using System.Drawing;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    class Armadilo : MonsterObject
    {
        public bool Visible, DoneDigOut;
        public long VisibleTime, DigOutTime;
        public Point DigOutLocation;
        public MirDirection DigOutDirection;

        protected override bool CanAttack
        {
            get
            {
                return Visible && base.CanAttack;
            }
        }
        protected override bool CanMove
        {
            get
            {
                return Visible && base.CanMove;
            }
        }
        public override bool Blocking
        {
            get
            {
                return Visible && base.Blocking;
            }
        }

        protected internal Armadilo(MonsterInfo info)
            : base(info)
        {
            Visible = false;
        }


        protected override void ProcessAI()
        {
            if (!Dead && Envir.Time > VisibleTime)
            {
                VisibleTime = Envir.Time + 2000;

                bool visible = FindNearby(4);

                if (!Visible && visible)
                {
                    Visible = true;
                    CellTime = Envir.Time + 500;
                    Broadcast(GetInfo());
                    Broadcast(new S.ObjectShow { ObjectID = ObjectID });
                    ActionTime = Envir.Time + 2000;
                    DigOutTime = Envir.Time;
                    DigOutLocation = CurrentLocation;
                    DigOutDirection = Direction;
                }
            }

            if (Visible && Envir.Time > DigOutTime + 800 && !DoneDigOut)
            {
                SpellObject ob = new SpellObject
                    {
                        Spell = Spell.Armadilo,
                        Value = 1,
                        ExpireTime = Envir.Time + (5 * 60 * 1000),
                        TickSpeed = 2000,
                        Caster = null,
                        CurrentLocation = DigOutLocation,
                        CurrentMap = this.CurrentMap,
                        Direction = DigOutDirection
                    };
                CurrentMap.AddObject(ob);
                ob.Spawned();
                DoneDigOut = true;                    
            }

            base.ProcessAI();
        }


        protected override void Attack()
        {

            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            ShockTime = 0;

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            ActionTime = Envir.Time + 500;
            AttackTime = Envir.Time + AttackSpeed;

            int damage;
            var rnd = Envir.Random.Next(100);

            if (rnd < 10)
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2 });
                damage = GetAttackPower(MinMC, MaxMC);
                if (damage == 0) return;

                AoeDmg(1, damage);
            }
            else if (rnd > 10 && rnd < 40)
            {

                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                damage = GetAttackPower(MinDC, MaxDC);
                if (damage == 0) return;

                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.AC);
                ActionList.Add(action);
                action = new DelayedAction(DelayedType.Damage, Envir.Time + 900, Target, damage, DefenceType.AC);
                ActionList.Add(action);

            }
            else 
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                damage = GetAttackPower(MinDC, MaxDC);
                if (damage == 0) return;

                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, Target, damage, DefenceType.AC);
                ActionList.Add(action);

            }

            if (Target.Dead)
                FindTarget();

        }

        private void AoeDmg(int dist, int dmg)
        {
            List<MapObject> targets = FindAllTargets(dist, CurrentLocation);
            if (targets.Count == 0) return;

            for (int i = 0; i < targets.Count; i++)
            {
                Target = targets[i];

                if (Target == null || !Target.IsAttackTarget(this) || Target.CurrentMap != CurrentMap || Target.Node == null) continue;

                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, Target, dmg, DefenceType.MAC);
                ActionList.Add(action);
            }
        }

        public override bool Walk(MirDirection dir)
        {
            return Visible && base.Walk(dir);
        }

        public override bool IsAttackTarget(MonsterObject attacker)
        {
            return Visible && base.IsAttackTarget(attacker);
        }
        public override bool IsAttackTarget(PlayerObject attacker)
        {
            return Visible && base.IsAttackTarget(attacker);
        }

        protected override void ProcessSearch()
        {
            if (Visible)
                base.ProcessSearch();
        }

        public override Packet GetInfo()
        {
            return !Visible ? null : base.GetInfo();
        }
    }
}