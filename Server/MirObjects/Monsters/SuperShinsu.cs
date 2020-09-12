using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    class SuperShinsu : MonsterObject
    {
        public bool Mode = false;
        public bool Summoned;
        public long ModeTime;

        protected override bool CanAttack
        {
            get
            {
                return base.CanAttack && Mode;
            }
        }

        protected internal SuperShinsu(MonsterInfo info) : base(info)
        {
            ActionTime = Envir.Time + 1000;
        }


        protected override void ProcessAI()
        {
            if (!Dead && Envir.Time > ActionTime)
            {
                if (Target != null) ModeTime = Envir.Time + 30000;

                if (!Mode && Envir.Time < ModeTime)
                {
                    Mode = true;
                    Broadcast(new S.ObjectShow { ObjectID = ObjectID });
                    ActionTime = Envir.Time + 1000;
                }
                else if (Mode && Envir.Time > ModeTime)
                {
                    Mode = false;
                    Broadcast(new S.ObjectHide { ObjectID = ObjectID });
                    ActionTime = Envir.Time + 1000;
                }
            }

            base.ProcessAI();
        }

        protected override bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap) return false;
            if (Target.CurrentLocation == CurrentLocation) return false;

            int x = Math.Abs(Target.CurrentLocation.X - CurrentLocation.X);
            int y = Math.Abs(Target.CurrentLocation.Y - CurrentLocation.Y);

            if (x > 2 || y > 2) return false;


            return (x <= 1 && y <= 1) || (x == y || x % 2 == y % 2);
        }
        protected override void Attack()
        {

            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
            ActionList.Add(new DelayedAction(DelayedType.Damage, Envir.Time + 500));

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;
            ShockTime = 0;

            if (Target.Dead)
                FindTarget();
        }

        private void LineAttack(int distance)
        {
            int damage = GetAttackPower(MinDC, MaxDC); //into this atk
            if (damage == 0) return;
            int boost = 0;
            switch (PetLevel)
            {
                case 1:
                    boost = 5;
                    break;
                case 2:
                    boost = 10;
                    break;
                case 3:
                    boost = 15;
                    break;
                case 4:
                    boost = 20;
                    break;
                case 5:
                    boost = 25;
                    break;
                case 6:
                    boost = 30;
                    break;
                case 7:
                    boost = 35;
                    break;
            }
            if (boost > 0)
            {
                int tmp = damage * boost / 100;
                damage += tmp;
            }
            for (int i = 1; i <= distance; i++)
            {
                Point target = Functions.PointMove(CurrentLocation, Direction, i);

                if (Target != null && target == Target.CurrentLocation)
                    Target.Attacked(this, damage, DefenceType.MACAgility);
                else
                {
                    if (!CurrentMap.ValidPoint(target)) continue;

                    Cell cell = CurrentMap.GetCell(target);
                    if (cell.Objects == null) continue;

                    for (int o = 0; o < cell.Objects.Count; o++)
                    {
                        MapObject ob = cell.Objects[o];
                        if (ob.Race == ObjectType.Monster || ob.Race == ObjectType.Player)
                        {
                            if (!ob.IsAttackTarget(this)) continue;

                            ob.Attacked(this, damage, DefenceType.MACAgility);
                        }
                        else continue;

                        break;
                    }
                }
            }
        }

        public override void Spawned()
        {
            base.Spawned();

            Summoned = true;
        }

        protected override void CompleteAttack(IList<object> data)
        {
            LineAttack(2);
        }

        public override Packet GetInfo() // Ice New Shins
        {
            int temp = -1;
            if (Mode)
            {
                if (Info.Name == Settings.SuperShinsuName)
                {
                    temp = 0;
                }
                else if (Info.Name == Settings.SuperShinsuName2)
                {
                    temp = 2;
                }
                else if (Info.Name == Settings.SuperShinsuName4) // Ice New Shins
                {
                    temp = 4;
                }
            }
            else
            {
                if (Info.Name == Settings.SuperShinsuName)
                {
                    temp = 1;
                }
                else if (Info.Name == Settings.SuperShinsuName2)
                {
                    temp = 3;
                }
                else if (Info.Name == Settings.SuperShinsuName4) // Ice New Shins
                {
                    temp = 5;
                }
            }

            return new S.ObjectMonster
            {
                ObjectID = ObjectID,
                Name = Name,
                NameColour = NameColour,
                Location = CurrentLocation,
                Image = temp == 0 ? Monster.SuperShinsu1 : // Ice New Shins
                        temp == 1 ? Monster.SuperShinsu :
                        temp == 2 ? Monster.SuperShinsu3 :
                        temp == 3 ? Monster.SuperShinsu2 :
                        temp == 4 ? Monster.SuperShinsu5 :
                        temp == 5 ? Monster.SuperShinsu4 : Monster.Shinsu,
                Direction = Direction,
                Effect = Info.Effect,
                AI = Info.AI,
                Light = Info.Light,
                Dead = Dead,
                Skeleton = Harvested,
                Poison = CurrentPoison,
                Hidden = Hidden,
                Extra = Summoned,
                AC = Info.MinAC,
                MAC = Info.MinMAC,
                DC = Info.MinDC,
                MC = Info.MinMC,
                SC = Info.MinSC,
                MaxAC = Info.MaxAC,
                MaxMAC = Info.MaxMAC,
                MaxDC = Info.MaxDC,
                MaxMC = Info.MaxMC,
                MaxSC = Info.MaxSC,
                Acc = Info.Accuracy,
                Agil = Info.Agility,
                AttkSpeed = Info.AttackSpeed,
                Health = Info.HP,
                CurrentHP = HP,
                IsPet = Master == null ? false : true,
                Level = Info.Level,
                IsTamable = Info.CanTame,
                IsPushable = Info.CanPush,
                IsUndead = Info.Undead,
                MoveSpeed = Info.MoveSpeed,
                Experience = Info.Experience,
                PetEnhancer = HasPetEnhancer()
            };
        }
    }
}
