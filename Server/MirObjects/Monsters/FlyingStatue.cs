using Server.MirDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class FlyingStatue : MonsterObject
    {
        public bool Cast = false;
        public long NextCastTime;
        public long AttackCastTime;
        public FlyingStatue(MonsterInfo info)
            : base(info)
        {

        }

        protected override void ProcessTarget()
        {
            if (Target == null)
                return;

            if (CanAttack)
            {
                if (Functions.InRange(CurrentLocation, Target.CurrentLocation, 10))
                {
                    if (Cast && Envir.Time > AttackCastTime)
                    {
                        int damage = GetAttackPower(MinDC, MaxDC);
                        if (Target.Attacked(this, damage, DefenceType.None) > 0)
                        {
                            if (Envir.Random.Next(2) == 1)
                            {
                                Target.ApplyPoison(new Poison { Duration = Envir.Random.Next(5), Owner = this, PType = PoisonType.Frozen, TickSpeed = 2000, Value = 20 }, this);
                                Broadcast(new S.ObjectEffect { ObjectID = Target.ObjectID, Direction = Direction, Effect = SpellEffect.FlyingStatuePurple });
                            }
                            else if (Envir.Random.Next(2) <= 1)
                            {
                                Target.ApplyPoison(new Poison { Duration = Envir.Random.Next(5), Owner = this, PType = PoisonType.Green, TickSpeed = 1000, Value = Envir.Random.Next(10) }, this);
                                Broadcast(new S.ObjectEffect { ObjectID = Target.ObjectID, Direction = Direction, Effect = SpellEffect.FlyingStatueGreen });
                            }
                        }

                        Cast = false;
                        NextCastTime = Envir.Time + Settings.Second * 10;
                        ActionTime = Envir.Time + 500;
                        AttackTime = Envir.Time + AttackSpeed;
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction });
                        return;
                    }
                    else if (Envir.Time > AttackCastTime && Envir.Time > NextCastTime && !Cast)
                    {
                        Cast = true;
                        Broadcast(new S.ObjectEffect { ObjectID = ObjectID, Effect = SpellEffect.FlyingStatueCast, Direction = Direction });
                        AttackCastTime = Envir.Time + Settings.Second * 2;
                        ActionTime = Envir.Time + 500;
                        AttackTime = Envir.Time + AttackSpeed;
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction });
                        return;
                    }
                }
                if (InAttackRange())
                {
                    Attack();
                    return;
                }
            }

            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }

            MoveTo(Target.CurrentLocation);
        }
    }
}
