using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.MirDatabase;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    class BoneFamiliar : MonsterObject
    {
        public bool Summoned;

        protected internal BoneFamiliar(MonsterInfo info) : base(info)
        {
            Direction = MirDirection.DownLeft;
        }

        public void CHMAttack()
        {
            List<MapObject> targets = FindAllTargets(1, CurrentLocation, false);
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i].IsAttackTarget(this))
                {
                    int damage = GetAttackPower(MinDC, MaxDC); // This here
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
                    targets[i].Attacked(this, damage, DefenceType.AC);
                }
            }
            Broadcast(new S.ObjectEffect { ObjectID = ObjectID, Effect = SpellEffect.CHM, Direction = Direction });
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
            AttackTime = Envir.Time + AttackSpeed;
        }

        protected override void ProcessTarget()
        {
            if (Target == null || !CanAttack)
                return;
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            if (InAttackRange())
            {
                if (Envir.Time > AttackTime)
                {
                    if (Master != null)
                    {
                        if (Master.Race == ObjectType.Player)
                        {
                            PlayerObject ob = (PlayerObject)Master;
                            UserMagic tmp = ob.GetMagic(Spell.SummonSkeleton);
                            if (tmp != null &&
                                tmp.Level >= 3)
                                CHMAttack();
                            else
                                Attack();
                        }
                        if (Target.Dead)
                            FindTarget();
                        return;
                    }
                    else
                        Attack();
                    if (Target.Dead)
                        FindTarget();
                    return;
                }

            }

            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }

            if (Info.TeleportBack && Envir.Random.Next(7) == 0 && Target.CurrentMap == CurrentMap)
                Teleport(Target.CurrentMap, Target.Back);
            else
                MoveTo(Target.CurrentLocation);
        }

        public override void Spawned()
        {
            base.Spawned();

            Summoned = true;
        }


        public override Packet GetInfo()
        {
            return new S.ObjectMonster
            {
                ObjectID = ObjectID,
                Name = Name,
                NameColour = NameColour,
                Location = CurrentLocation,
                Image = Info.Image,
                Direction = Direction,
                Effect = Info.Effect,
                GlowAura = Info.LightColar,
                LightEffect = Info.LightEffect,
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
