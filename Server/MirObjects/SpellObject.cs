using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects
{
    public class SpellObject : MapObject
    {
        public override ObjectType Race
        {
            get { return ObjectType.Spell; }
        }

        public override string Name { get; set; }
        public override int CurrentMapIndex { get; set; }
        public override Point CurrentLocation { get; set; }
        public override MirDirection Direction { get; set; }
        public override ushort Level { get; set; }
        public override bool Blocking
        {
            get
            {
                return false;
            }
        }

        public long TickTime, StartTime;
        public PlayerObject Caster;
        public MonsterObject MobCaster;
        public int Value, TickSpeed, PvPValue;
        public Spell Spell;
        public Point CastLocation;
        public bool Show, Decoration, LMS_Circle;
        public int PoisonTickDamage, PoisonTickTime;
        public long PoisonDuration;

        //ExplosiveTrap
        public int ExplosiveTrapID;
        public int ExplosiveTrapCount;
        public bool DetonatedTrap;

        //Portal
        public Map ExitMap;
        public Point ExitCoord;

        public override uint Health
        {
            get { throw new NotSupportedException(); }
        }
        public override uint MaxHealth
        {
            get { throw new NotSupportedException(); }
        }


        public override void Process()
        {
            if (Decoration) return;
            if (LMS_Circle)
            {
                if (Envir.Time > ExpireTime)
                {
                    CurrentMap.RemoveObject(this);
                    Despawn();
                }
                return;
            }
            if (Caster == null && MobCaster == null)
                return;
            if (Caster != null && Caster.Node == null)
                Caster = null;
            if (MobCaster != null && MobCaster.Node == null)
                MobCaster = null;
            if (
                    (
                        MobCaster == null && Caster == null
                        &&
                        (
                            Spell == Spell.FireWall ||
                            Spell == Spell.HealingCircle ||
                            Spell == Spell.Portal ||
                            Spell == Spell.ExplosiveTrap ||
                            Spell == Spell.Reincarnation ||
                            Spell == Spell.MobFireWall ||
                            Spell == Spell.MobBlizzard ||
                            Spell == Spell.MobMeteorStrike ||
                            Spell == Spell.MobPoisonCloud ||
                            Spell == Spell.MeteorStrike ||
                            Spell == Spell.LavaKing ||
                            Spell == Spell.Blizzard ||
                            Spell == Spell.FrozenRains ||
                            Spell == Spell.MoonMist
                        )
                    )
                    ||
                    ( Envir.Time > ExpireTime ) || ( Spell == Spell.Trap && Target != null ))
            {
                if (Spell == Spell.TrapHexagon && Target != null || Spell == Spell.Trap && Target != null)
                {
                    MonsterObject ob = (MonsterObject)Target;

                    if (Envir.Time < ExpireTime && ob.ShockTime != 0)
                        return;
                }
                if (MobCaster is HeroObject tmp)
                {
                    if (tmp != null)
                    {
                        tmp.LavaKingCasting = false;
                    }
                }
                if (Spell == Spell.Reincarnation && Caster != null)
                {
                    Caster.ReincarnationReady = true;
                    Caster.ReincarnationExpireTime = Envir.Time + 6000;
                }

                CurrentMap.RemoveObject(this);
                Despawn();
                return;
            }
            

            if (Spell == Spell.Reincarnation && !Caster.ActiveReincarnation)
            {
                CurrentMap.RemoveObject(this);
                Despawn();
                return;
            }

            if (Spell == Spell.ExplosiveTrap && FindObject(Caster.ObjectID, 20) == null && Caster != null)
            {
                CurrentMap.RemoveObject(this);
                Despawn();
                return;
            }

            if (Spell == Spell.FireWall && FindObject(Caster.ObjectID, 12) == null && Caster != null)  // Ice Firewall last time.
            {
                CurrentMap.RemoveObject(this);
                Despawn();
                return;
            }

            if (Envir.Time < TickTime) return;
            TickTime = Envir.Time + TickSpeed;

            Cell cell = CurrentMap.GetCell(CurrentLocation);
            for (int i = 0; i < cell.Objects.Count; i++)
                ProcessSpell(cell.Objects[i]);

            if ((Spell == Spell.MapLava) || (Spell == Spell.MapLightning)) Value = 0;
        }
        public void ProcessSpell(MapObject ob)
        {
            if (Envir.Time < StartTime) return;
            switch (Spell)
            {
                case Spell.MoonMist:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster && ob.Race != ObjectType.Hero) return;
                        if (ob.Dead) return;
                        if (Caster != null && !Caster.ActiveBlizzard) return;
                        if (!ob.IsAttackTarget(Caster)) return;

                        if (ob.Race == ObjectType.Player)
                            ob.Attacked(Caster, PvPValue, DefenceType.ACAgility, false);
                        else
                            ob.Attacked(Caster, Value, DefenceType.ACAgility, false);
                    }
                    break;
                case Spell.FireWall:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster && ob.Race != ObjectType.Hero) return;
                        if (ob.Dead) return;
                        if (!ob.IsAttackTarget(Caster)) return;

                        if (ob.Race == ObjectType.Player)
                            ob.Attacked(Caster, PvPValue, DefenceType.MAC, false);
                        else
                            ob.Attacked(Caster, Value, DefenceType.MAC, false);
                    }
                    break;
                case Spell.HealingCircle:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster && ob.Race != ObjectType.Hero)
                            return;
                        if (ob.Dead)
                            return;
                        if (Caster != null && ob.ObjectID == Caster.ObjectID) return;
                        if (ob.Race == ObjectType.Player)
                        {
                            if (Caster.AMode == AttackMode.All)
                            {
                                if (ob.IsAttackTarget(Caster))
                                {
                                    
                                    ob.Attacked(Caster, PvPValue, DefenceType.MAC, false);
                                    return;
                                }
                            }
                            else if (Caster.AMode == AttackMode.Group)
                            {
                                if (Caster.GroupMembers != null &&
                                    Caster.GroupMembers.Contains(ob))
                                {
                                    if (ob.Health < ob.MaxHealth)
                                    {
                                        ob.HealAmount = (ushort)Math.Min(ushort.MaxValue, ob.HealAmount + Value);
                                        ob.OperateTime = 0;
                                        return;
                                    }
                                }
                                else
                                {
                                    if (ob.IsAttackTarget(Caster))
                                    {
                                        ob.Attacked(Caster, PvPValue, DefenceType.MAC, false);
                                        return;
                                    }
                                }
                            }
                            else if (Caster.AMode == AttackMode.Guild)
                            {
                                if (Caster.MyGuild != null)
                                {
                                    if (ob.Race == ObjectType.Player)
                                    {
                                        PlayerObject tmp = (PlayerObject)ob;
                                        if (tmp.MyGuild != null &&
                                            tmp.MyGuild == Caster.MyGuild)
                                        {
                                            if (ob.Health < ob.MaxHealth)
                                            {
                                                ob.HealAmount = (ushort)Math.Min(ushort.MaxValue, ob.HealAmount + Value);
                                                ob.OperateTime = 0;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            if (ob.IsAttackTarget(Caster))
                                            {
                                                ob.Attacked(Caster, PvPValue, DefenceType.MAC, false);
                                                return;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (ob.IsAttackTarget(Caster))
                                    {
                                        ob.Attacked(Caster, PvPValue, DefenceType.MAC, false);
                                        return;
                                    }
                                }
                            }
                            else if (Caster.AMode == AttackMode.RedBrown)
                            {
                                if (ob.IsAttackTarget(Caster))
                                {
                                    if (ob.Race == ObjectType.Player)
                                    {
                                        PlayerObject tmp = (PlayerObject)ob;//PKPoints < 200 & Envir.Time > BrownTime
                                        if (tmp.PKPoints > 200 && Envir.Time > ob.BrownTime)
                                        {
                                            ob.Attacked(Caster, PvPValue, DefenceType.MAC, false);
                                            return;
                                        }
                                    }
                                }
                            }
                            else if (ob.Race == ObjectType.Player)
                            {
                                if (ob.Health < ob.MaxHealth)
                                {
                                    ob.HealAmount = (ushort)Math.Min(ushort.MaxValue, ob.HealAmount + Value);
                                    ob.OperateTime = 0;
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (ob.IsAttackTarget(Caster))
                                ob.Attacked(Caster, Value, DefenceType.MAC, false);
                            return;
                        }
                    }
                    break;
                case Spell.Healing: //SafeZone
                    {
                        if (ob.Race != ObjectType.Player && (ob.Race != ObjectType.Monster || ob.Master == null || ob.Master.Race != ObjectType.Player)) return;
                        if (ob.Dead || ob.HealAmount != 0 || ob.PercentHealth == 100) return;

                        ob.HealAmount += 25;
                        Broadcast(new S.ObjectEffect { ObjectID = ob.ObjectID, Effect = SpellEffect.Healing });
                    }
                    break;
                case Spell.MobFireWall:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster && ob.Race != ObjectType.Hero)
                            return;
                        if (ob.Dead)
                            return;

                        if (!ob.IsAttackTarget(MobCaster))
                            return;

                        ob.Attacked(MobCaster, Value, DefenceType.MAC);
                    }
                    break;
                case Spell.CrystalBeastBlizz:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster && ob.Race != ObjectType.Hero)
                            return;
                        if (ob.Dead)
                            return;
                        if (!ob.IsAttackTarget(MobCaster))
                            return;
                        ob.Attacked(MobCaster, Value, DefenceType.MAC);
                        if (!ob.Dead && Envir.Random.Next(8) == 0)
                        {
                            ob.ApplyPoison(new Poison
                            {
                                Duration = Envir.Random.Next(5),
                                Owner = MobCaster,
                                PType = PoisonType.Slow,
                                TickSpeed = 2000,
                            }, MobCaster);
                        }
                    }
                    break;
                case Spell.MobPoisonCloud:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster && ob.Race != ObjectType.Hero)
                            return;
                        if (ob.Dead)
                            return;

                        if (!ob.IsAttackTarget(MobCaster))
                            return;
                        ob.Attacked(MobCaster, Value, DefenceType.MAC);
                        if (!ob.Dead)
                            ob.ApplyPoison(new Poison
                            {
                                Duration = 15,
                                Owner = MobCaster,
                                PType = PoisonType.Green,
                                TickSpeed = 2000,
                                Value = Value / 20
                            }, MobCaster, false, false);
                    }
                    break;
                case Spell.MobBlizzard:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster && ob.Race != ObjectType.Hero)
                            return;
                        if (ob.Dead)
                            return;
                        if (!ob.IsAttackTarget(MobCaster))
                            return;
                        ob.Attacked(MobCaster, Value, DefenceType.MAC);
                        if (!ob.Dead && Envir.Random.Next(8) == 0)
                            ob.ApplyPoison(new Poison
                            {
                                Duration = 5,
                                Owner = MobCaster,
                                PType = PoisonType.Slow,
                                TickSpeed = 2000,
                            }, MobCaster);
                    }
                    break;
                case Spell.MobMeteorStrike:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster && ob.Race != ObjectType.Hero)
                            return;
                        if (ob.Dead)
                            return;
                        if (!ob.IsAttackTarget(MobCaster))
                            return;
                        ob.Attacked(MobCaster, Value, DefenceType.MAC);
                    }
                    break;
                case Spell.PoisonCloud:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;

                        if (!ob.IsAttackTarget(Caster)) return;
                        if (ob.Race == ObjectType.Player)
                            ob.Attacked(Caster, PvPValue, DefenceType.MAC, false);
                        else
                            ob.Attacked(Caster, Value, DefenceType.MAC, false);
                        if (!ob.Dead)
                        {
                            if (ob.Race == ObjectType.Player)
                                ob.ApplyPoison(new Poison
                                {
                                    Duration = 15,
                                    Owner = Caster,
                                    PType = PoisonType.Green,
                                    TickSpeed = 2000,
                                    Value = PvPValue / 20,
                                }, Caster);
                            else
                                ob.ApplyPoison(new Poison
                                {
                                    Duration = 15,
                                    Owner = Caster,
                                    PType = PoisonType.Green,
                                    TickSpeed = 2000,
                                    Value = Value / 20,
                                }, Caster);
                        }
                    }
                    break;
                case Spell.Blizzard:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster && ob.Race != ObjectType.Hero) return;
                        if (ob.Dead) return;
                        if (Caster != null && !Caster.ActiveBlizzard) return;
                        if (!ob.IsAttackTarget(Caster)) return;
                        //  This is the map event, every time the Process is trigger it'll run this code until it's expired
                        if (ob.Race == ObjectType.Player)
                            ob.Attacked(Caster, PvPValue, DefenceType.MAC, false);
                        else
                            ob.Attacked(Caster, Value, DefenceType.MAC, false);


                        if (!ob.Dead && Envir.Random.Next(8) == 0)
                            ob.ApplyPoison(new Poison
                            {
                                Duration = 5 + Envir.Random.Next(Caster.Freezing),
                                Owner = Caster,
                                PType = PoisonType.Slow,
                                TickSpeed = 2000,
                            }, Caster);
                    }
                    break;
                case Spell.FrozenRains:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster && ob.Race != ObjectType.Hero) return;
                        if (ob.Dead) return;

                        if (Caster != null && !Caster.ActiveBlizzard) return;
                        if (!ob.IsAttackTarget(Caster)) return;
                        if (ob.Race == ObjectType.Player)
                            ob.Attacked(Caster, PvPValue, DefenceType.MAC, false);
                        else
                            ob.Attacked(Caster, Value, DefenceType.MAC, false);

                        if (!ob.Dead && Envir.Random.Next(8) == 0)
                            ob.ApplyPoison(new Poison
                            {
                                Duration = 5 + Envir.Random.Next(Caster.Freezing),
                                Owner = Caster,
                                PType = PoisonType.Slow,
                                TickSpeed = 2000,
                            }, Caster);
                    }
                    break;
                case Spell.LavaKing:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster && ob.Race != ObjectType.Hero) return;
                        if (ob.Dead) return;
                        if (Caster != null && !Caster.ActiveBlizzard) return;
                        if (Caster != null)
                        {
                            if (!ob.IsAttackTarget(Caster)) return;
                            byte magicLevel = 0;
                            int poisonVal = 2;

                            if (ob.Race == ObjectType.Player)
                            {
                                poisonVal =
                                    magicLevel == 0 ? 2 :
                                    magicLevel == 1 ? 3 :
                                    magicLevel == 2 ? 4 : 6;
                            }
                            else
                            {
                                poisonVal =
                                    magicLevel == 0 ? 2 :
                                    magicLevel == 1 ? 4 :
                                    magicLevel == 2 ? 6 : 8;
                            }
                            if (ob.Race == ObjectType.Player)
                                ob.Attacked(Caster, PvPValue, DefenceType.MAC, false);
                            else
                                ob.Attacked(Caster, Value, DefenceType.MAC, false);
                            if (!ob.Dead && Envir.Random.Next(8) == 0)
                                ob.ApplyPoison(new Poison
                                {
                                    Duration = Envir.Random.Next(2, 8),
                                    Owner = Caster,
                                    PType = PoisonType.Burning,
                                    TickSpeed = 2000,
                                    Value = poisonVal
                                    
                                }, Caster);
                        }
                        else if (MobCaster != null)
                        {
                            if (!ob.IsAttackTarget(MobCaster)) return;
                            ob.Attacked(MobCaster, ob.Race == ObjectType.Player ? (int)(Value * 0.75f) : Value, DefenceType.MAC);
                            if (!ob.Dead && Envir.Random.Next(8) == 0)
                                ob.ApplyPoison(new Poison 
                            {
                                Duration = Envir.Random.Next(2, 8),
                                Owner = MobCaster,
                                PType = PoisonType.Burning,
                                TickSpeed = 2000,
                                Value = ob.Race == ObjectType.Player ? 2 : 4
                            }, Caster);
                        }
                        else
                            return;
                    }
                    break;
                case Spell.MeteorStrike:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster && ob.Race != ObjectType.Hero) return;
                        if (ob.Dead) return;
                        if (Caster != null && !Caster.ActiveBlizzard) return;
                        if (!ob.IsAttackTarget(Caster)) return;
                        if (ob.Race == ObjectType.Player)
                            ob.Attacked(Caster, PvPValue, DefenceType.MAC, false);
                        else
                            ob.Attacked(Caster, Value, DefenceType.MAC, false);
                    }
                    break;
                case Spell.SoulReaper:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster && ob.Race != ObjectType.Hero)
                            return;
                        if (ob.Dead)
                            return;
                        if (MobCaster == null)
                        {
                            if (Caster == null) return;
                            if (ob.IsAttackTarget(Caster))
                            {
                                if (ob.Race == ObjectType.Player)
                                {
                                    if (ob.Attacked(Caster, PvPValue, DefenceType.MAC) > 0 &&
                                        !ob.Dead && Envir.Random.Next(8) == 0)
                                    {
                                        ob.ApplyPoison(new Poison
                                        {
                                            Duration = 3,
                                            Owner = Caster,
                                            PType = PoisonType.Frozen,
                                            TickSpeed = 2000
                                        }, Caster);
                                    }
                                }
                                else
                                if (ob.Attacked(Caster, Value, DefenceType.MAC) > 0 &&
                                        !ob.Dead && Envir.Random.Next(8) == 0)
                                {
                                    ob.ApplyPoison(new Poison
                                    {
                                        Duration = 3,
                                        Owner = Caster,
                                        PType = PoisonType.Frozen,
                                        TickSpeed = 2000
                                    }, Caster);
                                }

                            }
                        }
                        else
                        {
                            if (MobCaster.Master == null) return;
                            if (!ob.IsAttackTarget(((PlayerObject)MobCaster.Master)))
                                return;
                            ob.Attacked(MobCaster, Value, DefenceType.MAC);
                            if (!ob.Dead && Envir.Random.Next(8) == 0)
                                ob.ApplyPoison(new Poison
                                {
                                    Duration = 3,
                                    Owner = MobCaster,
                                    PType = PoisonType.Frozen,
                                    TickSpeed = 2000,
                                }, MobCaster);
                        }
                    }
                    break;
                case Spell.ExplosiveTrap:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;
                        if (!ob.IsAttackTarget(Caster)) return;
                        if (DetonatedTrap) return;//make sure explosion happens only once
                        DetonateTrapNow();
                        if (ob.Race == ObjectType.Player)
                            ob.Attacked(Caster, PvPValue, DefenceType.MAC, false);
                        else
                            ob.Attacked(Caster, Value, DefenceType.MAC, false);
                    }
                    break;
                case Spell.MapLava:
                case Spell.MapLightning:
                case Spell.MapQuake1:
                case Spell.MapQuake2:
                    {
                        if (Value == 0) return;
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster && ob.Race != ObjectType.Hero) return;
                        if (ob.Dead) return;
                        ob.Struck(Value, DefenceType.MAC);
                    }
                    break;
                case Spell.Portal:
                    {
                        if (ob.Race != ObjectType.Player) return;
                        if (Caster != ob && (Caster == null || (Caster.GroupMembers == null) || (!Caster.GroupMembers.Contains((PlayerObject)ob)))) return;

                        if (ExitMap == null) return;

                        MirDirection dir = ob.Direction;

                        Point newExit = Functions.PointMove(ExitCoord, dir, 1);

                        if (!ExitMap.ValidPoint(newExit)) return;

                        ob.Teleport(ExitMap, newExit, false);

                        Value = Value - 1;

                        if (Value < 1)
                        {
                            ExpireTime = Envir.Time;
                            return;
                        }
                    }
                    break;
            }
        }

        public void DetonateTrapNow()
        {
            DetonatedTrap = true;
            Broadcast(GetInfo());
            ExpireTime = Envir.Time + 1000;
        }

        public override void SetOperateTime()
        {
            long time = Envir.Time + 2000;

            if (TickTime < time && TickTime > Envir.Time)
                time = TickTime;

            if (OwnerTime < time && OwnerTime > Envir.Time)
                time = OwnerTime;

            if (ExpireTime < time && ExpireTime > Envir.Time)
                time = ExpireTime;

            if (PKPointTime < time && PKPointTime > Envir.Time)
                time = PKPointTime;

            if (LastHitTime < time && LastHitTime > Envir.Time)
                time = LastHitTime;

            if (EXPOwnerTime < time && EXPOwnerTime > Envir.Time)
                time = EXPOwnerTime;

            if (BrownTime < time && BrownTime > Envir.Time)
                time = BrownTime;

            for (int i = 0; i < ActionList.Count; i++)
            {
                if (ActionList[i].Time >= time && ActionList[i].Time > Envir.Time) continue;
                time = ActionList[i].Time;
            }

            for (int i = 0; i < PoisonList.Count; i++)
            {
                if (PoisonList[i].TickTime >= time && PoisonList[i].TickTime > Envir.Time) continue;
                time = PoisonList[i].TickTime;
            }

            for (int i = 0; i < Buffs.Count; i++)
            {
                if (Buffs[i].ExpireTime >= time && Buffs[i].ExpireTime > Envir.Time) continue;
                time = Buffs[i].ExpireTime;
            }


            if (OperateTime <= Envir.Time || time < OperateTime)
                OperateTime = time;
        }

        public override void Process(DelayedAction action)
        {
            throw new NotSupportedException();
        }
        public override bool IsAttackTarget(PlayerObject attacker)
        {
            throw new NotSupportedException();
        }
        public override bool IsAttackTarget(MonsterObject attacker)
        {
            throw new NotSupportedException();
        }
        public override int Attacked(PlayerObject attacker, int damage, DefenceType type = DefenceType.ACAgility, bool damageWeapon = true)
        {
            throw new NotSupportedException();
        }
        public override int Attacked(MonsterObject attacker, int damage, DefenceType type = DefenceType.ACAgility)
        {
            throw new NotSupportedException();
        }

        public override int Struck(int damage, DefenceType type = DefenceType.ACAgility)
        {
            throw new NotSupportedException();
        }
        public override bool IsFriendlyTarget(PlayerObject ally)
        {
            throw new NotSupportedException();
        }
        public override bool IsFriendlyTarget(MonsterObject ally)
        {
            throw new NotSupportedException();
        }
        public override void ReceiveChat(string text, ChatType type, List<UserItem> items = null)
        {
            throw new NotSupportedException();
        }

        public override Packet GetInfo()
        {
            switch (Spell)
            {
                //  Don't show SZ Healing
                case Spell.Healing:
                    return null;
                //  Show only the Cast Location
                case Spell.PoisonCloud:
                case Spell.FrozenRains:
                case Spell.Blizzard:
                case Spell.LavaKing:
                case Spell.HealingCircle:
                case Spell.MeteorStrike:
                case Spell.SoulReaper:
                    if (!Show)
                        return null;

                    return new S.ObjectSpell
                    {
                        ObjectID = ObjectID,
                        Location = CastLocation,
                        Spell = Spell,
                        Direction = Direction
                    };
                case Spell.ExplosiveTrap:
                    return new S.ObjectSpell
                    {
                        ObjectID = ObjectID,
                        Location = CurrentLocation,
                        Spell = Spell,
                        Direction = Direction,
                        Param = DetonatedTrap
                    };

                default:
                    //  Fire Wall type spells
                    return new S.ObjectSpell
                    {
                        ObjectID = ObjectID,
                        Location = CurrentLocation,
                        Spell = Spell,
                        Direction = Direction
                    };
            }

        }

        public override void ApplyPoison(Poison p, MapObject Caster = null, bool NoResist = false, bool ignoreDefence = true)
        {
            throw new NotSupportedException();
        }
        public override void Die()
        {
            throw new NotSupportedException();
        }
        public override int Pushed(MapObject pusher, MirDirection dir, int distance)
        {
            throw new NotSupportedException();
        }
        public override void SendHealth(PlayerObject player)
        {
            throw new NotSupportedException();
        }
        public override void Despawn()
        {
            base.Despawn();

            if (Spell == Spell.Reincarnation && Caster != null && Caster.Node != null)
            {
                Caster.ActiveReincarnation = false;
                Caster.Enqueue(new S.CancelReincarnation { });
            }

            if (Spell == Spell.ExplosiveTrap && Caster != null)
                Caster.ExplosiveTrapDetonated(ExplosiveTrapID, ExplosiveTrapCount);

            if (Spell == Spell.Portal && Caster != null)
            {
                if (Caster.PortalObjectsArray[0] == this)
                {
                    Caster.PortalObjectsArray[0] = null;

                    if (Caster.PortalObjectsArray[1] != null)
                    {
                        Caster.PortalObjectsArray[1].ExpireTime = 0;
                        Caster.PortalObjectsArray[1].Process();
                    }
                }
                else
                {
                    Caster.PortalObjectsArray[1] = null;
                }
            }
        }

        public override void BroadcastInfo()
        {
            if ((Spell != Spell.ExplosiveTrap) || (Caster == null))
                base.BroadcastInfo();
            Packet p;
            if (CurrentMap == null) return;

            for (int i = CurrentMap.Players.Count - 1; i >= 0; i--)
            {
                PlayerObject player = CurrentMap.Players[i];
                if (Functions.InRange(CurrentLocation, player.CurrentLocation, Globals.DataRange))
                {
                    if ((Caster == null) || (player == null)) continue;
                    if ((player == Caster) || (player.IsFriendlyTarget(Caster)))
                    {
                        p = GetInfo();
                        if (p != null)
                            player.Enqueue(p);
                    }
                }
            }
        }
    }
}
