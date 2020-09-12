using Server.MirDatabase;
using System.Collections.Generic;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class RestlessJar : MonsterObject
    {
        public long SpawnDelay;

        protected override bool CanMove { get { return false; } }

        protected internal RestlessJar(MonsterInfo info)
            : base(info)
        {
            Direction = MirDirection.Up;
            SpawnDelay = Envir.Time + 120 * Settings.Second;
        }

        protected override void ProcessPoison()
        {
        }

        protected override void Attack() { }

        public override void Turn(MirDirection dir)
        {
        }
        public override bool Walk(MirDirection dir) 
        { 
            return false; 
        }

        private void SpawnSlaves()
        {
            int count = System.Math.Min(5, 25 - SlaveList.Count);

            MonsterObject mob = null;
            var nr = Envir.Random.Next(3);


            for (int i = 0; i < count; i++)
            {

                switch (nr)
                {
                    case 0:
                        mob = GetMonster(Envir.GetMonsterInfo(Settings.Jar1Mob));
                        break;
                    case 1:
                        mob = GetMonster(Envir.GetMonsterInfo(Settings.Jar2Mob));
                        break;
                    case 2:
                        mob = GetMonster(Envir.GetMonsterInfo(Settings.Jar3Mob));
                        break;

                }

                if (mob == null) continue;

                mob.Spawn(CurrentMap, Target.CurrentLocation);

                mob.Target = Target;
                mob.ActionTime = Envir.Time + 2000;
                SlaveList.Add(mob);
            }
        }

        protected override void ProcessTarget()
        {


            if (!CanAttack || Target == null) return;

            var tornado = (FindAllTargets(2, CurrentLocation, false));

             if (Envir.Random.Next(4) == 1 && Envir.Time > SpawnDelay)
            {
                SpawnDelay = Envir.Time +  60 * Settings.Second;

                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation , Type = 2});
                Broadcast(new S.ObjectEffect { ObjectID = ObjectID, Effect = SpellEffect.RestlessJarSpawn });
                Broadcast(new S.ObjectEffect { ObjectID = Target.ObjectID, Effect = SpellEffect.RestlessJarSpawnOnPlayer });

                SpawnSlaves();
            }
            else  if (Envir.Random.Next(15) == 0 && tornado.Count > 0)
            {
                ShockTime = 0;

                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
                Broadcast(new S.ObjectEffect { ObjectID = ObjectID, Effect = SpellEffect.RestlessJarTornado });
                for (int i = 0; i < tornado.Count; i++)
                {
                    Target = tornado[i];

                    MirDirection pushdir = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                    Target.Pushed(this, pushdir, 3 + Envir.Random.Next(3));
                }

                List<MapObject> targets = FindAllTargets(7, CurrentLocation, false);
                if (targets.Count != 0)
                {
                    for (int i = 0; i < targets.Count; i++)
                    {
                        Target = targets[i];

                        Target.ApplyPoison(new Poison { PType = PoisonType.Paralysis, Duration = 5, TickSpeed = 1000 }, this);
                    }
                }



            }
            else
            {
                List<MapObject> targets = FindAllTargets(Info.ViewRange, CurrentLocation, false);
                if (targets.Count == 0) return;

                ShockTime = 0;

                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation , Type = 2});
                for (int i = 0; i < targets.Count; i++)
                {
                    Target = targets[i];
                    Target.Attacked(this,GetAttackPower(MinDC,MaxDC),DefenceType.ACAgility);

                    var p = new S.ObjectEffect { ObjectID = ObjectID, Effect = SpellEffect.RestlessJarMultipleHit, TargetID = Target.ObjectID };
                    Broadcast(p);

                }

            }

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

        }   

        public override void ApplyPoison(Poison p, MapObject Caster = null, bool NoResist = false, bool ignoreDefence = true) { }
    }
}
