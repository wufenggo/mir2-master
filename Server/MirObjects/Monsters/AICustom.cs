using Server.MirDatabase;
using Server.MirEnvir;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.MirObjects.Monsters
{
    public class AICustom : MonsterObject
    {
        public AI_Customiser Custom;
        public AI_Type Attack0;
        public AI_Type Attack1;
        public AI_Type Attack2;
        public AI_Type Attack3;
        public AI_Type Attack4;
        public AI_Type Attack5;
        public AI_Type Attack6;
        public long[] AttackTimes = new long[7];

        /// <summary>
        /// The Constructor will deal with assigning the mob it's AI
        /// </summary>
        /// <param name="info">The Monsters Info (I.E it's stats etc..)</param>
        public AICustom(MonsterInfo info)
            : base(info)
        {
            //  Get the AI without having to cycle.
            Custom = Envir.CustomAIs.Where(e => e.MobIndex == info.Index).FirstOrDefault();
            //  Ensure the Custom AI is valid
            if (Custom != null)
            {
                //  Assign the Attack Variables as long as it exists in the Custom AI.
                if (Custom.AttackTypes.Count >= 1)
                    Attack0 = Custom.AttackTypes[0];
                if (Custom.AttackTypes.Count >= 2)
                    Attack1 = Custom.AttackTypes[1];
                if (Custom.AttackTypes.Count >= 3)
                    Attack2 = Custom.AttackTypes[2];
                if (Custom.AttackTypes.Count >= 4)
                    Attack3 = Custom.AttackTypes[3];
                if (Custom.AttackTypes.Count >= 5)
                    Attack4 = Custom.AttackTypes[4];
                if (Custom.AttackTypes.Count >= 6)
                    Attack5 = Custom.AttackTypes[5];
                if (Custom.AttackTypes.Count >= 7)
                    Attack6 = Custom.AttackTypes[6];
            }
            
        }

        /// <summary>
        /// Using custom hit range we'll need to ensure the targets in range
        /// </summary>
        /// <param name="from">Point A</param>
        /// <param name="to">Point B</param>
        /// <param name="distance">Distance between Point A and B</param>
        /// <returns></returns>
        public bool InCustomRange(Point from, Point to, int distance)
        {
            return Functions.InRange(from, to, distance);
        }

        #region Attack Methods
        public void PerformAttack0()
        {

        }

        public void PerformAttack1()
        {

        }

        public void PerformAttack2()
        {

        }

        public void PerformAttack3()
        {

        }

        public void PerformAttack4()
        {

        }

        public void PerformAttack5()
        {

        }

        public void PerformAttack6()
        {

        }
        #endregion

        /// <summary>
        /// Find targets that are NOT monsters (Pets)
        /// </summary>
        public void FindNonPetTarget()
        {
            //if (CurrentMap.Players.Count < 1) return;
            Map Current = CurrentMap;

            for (int d = 0; d <= Info.ViewRange; d++)
            {
                for (int y = CurrentLocation.Y - d; y <= CurrentLocation.Y + d; y++)
                {
                    if (y < 0)
                        continue;
                    if (y >= Current.Height)
                        break;

                    for (int x = CurrentLocation.X - d; x <= CurrentLocation.X + d; x += Math.Abs(y - CurrentLocation.Y) == d ? 1 : d * 2)
                    {
                        if (x < 0)
                            continue;
                        if (x >= Current.Width)
                            break;
                        Cell cell = Current.Cells[x, y];
                        if (cell.Objects == null || !cell.Valid)
                            continue;
                        for (int i = 0; i < cell.Objects.Count; i++)
                        {
                            MapObject ob = cell.Objects[i];
                            switch (ob.Race)
                            {
                                case ObjectType.Player:
                                    PlayerObject playerob = (PlayerObject)ob;
                                    if (!ob.IsAttackTarget(this))
                                        continue;
                                    if (playerob.GMGameMaster || ob.Hidden && ( !CoolEye || Level < ob.Level ) || Envir.Time < HallucinationTime)
                                        continue;

                                    Target = ob;
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Override the Attacked method in order to ignore damage from pets if it's set.
        /// </summary>
        /// <param name="attacker">The pet</param>
        /// <param name="damage">Damage to calculate from</param>
        /// <param name="type">Defence type</param>
        /// <returns>Damage to deal</returns>
        public override int Attacked(MonsterObject attacker, int damage, DefenceType type = DefenceType.ACAgility)
        {
            if(Custom.IgnorePetDamage)
                damage = 0;
            return base.Attacked(attacker, damage, type);
        }

        #region This is where the AI is applied and calls the attacks
        /// <summary>
        /// Override the ProcessAI in order to apply our own AI.
        /// </summary>
        protected override void ProcessAI()
        {
            //Target has to be valid and not dead.
            if (Target == null || Target.Dead)
            {
                FindTarget();
                return;
            }
            
            if (Envir.Time > AttackTime)
            {
                //  Each Attack need to ensure it exists, then check the attack time of that attack
                if (Attack0 != null && Envir.Time > AttackTimes[0])
                {
                    //  If we're to ignore pets, we must find a Player to target instead
                    if (Attack0.IgnorePet && Target.Race == ObjectType.Monster)
                        FindNonPetTarget();
                    //  Ensure the targets in range to use the attack
                    if (InCustomRange(CurrentLocation, Target.CurrentLocation, Attack0.HitRange))
                    {
                        //  Perform the Attack
                        PerformAttack0();
                        //  Set the next Attack time
                        AttackTimes[0] = Envir.Time + Attack0.AttkTime;
                    }
                }
                else if (Attack1 != null && Envir.Time > AttackTimes[1])
                {
                    if (Attack1.IgnorePet && Target.Race == ObjectType.Monster)
                        FindNonPetTarget();
                    if (InCustomRange(CurrentLocation, Target.CurrentLocation, Attack1.HitRange))
                    {
                        PerformAttack1();
                        AttackTimes[1] = Envir.Time + Attack1.AttkTime;
                    }
                }
                else if (Attack2 != null && Envir.Time > AttackTimes[2])
                {
                    if (Attack2.IgnorePet && Target.Race == ObjectType.Monster)
                        FindNonPetTarget();
                    if (InCustomRange(CurrentLocation, Target.CurrentLocation, Attack2.HitRange))
                    {
                        PerformAttack2();
                        AttackTimes[2] = Envir.Time + Attack2.AttkTime;
                    }
                }
                else if (Attack3 != null && Envir.Time > AttackTimes[3])
                {
                    if (Attack3.IgnorePet && Target.Race == ObjectType.Monster)
                        FindNonPetTarget();
                    if (InCustomRange(CurrentLocation, Target.CurrentLocation, Attack3.HitRange))
                    {
                        PerformAttack3();
                        AttackTimes[3] = Envir.Time + Attack3.AttkTime;
                    }
                }
                else if (Attack4 != null && Envir.Time > AttackTimes[4])
                {
                    if (Attack4.IgnorePet && Target.Race == ObjectType.Monster)
                        FindNonPetTarget();
                    if (InCustomRange(CurrentLocation, Target.CurrentLocation, Attack4.HitRange))
                    {
                        PerformAttack4();
                        AttackTimes[4] = Envir.Time + Attack4.AttkTime;
                    }
                }
                else if (Attack5 != null && Envir.Time > AttackTimes[5])
                {
                    if (Attack5.IgnorePet && Target.Race == ObjectType.Monster)
                        FindNonPetTarget();
                    if (InCustomRange(CurrentLocation, Target.CurrentLocation, Attack5.HitRange))
                    {
                        PerformAttack5();
                        AttackTimes[5] = Envir.Time + Attack5.AttkTime;
                    }
                }
                else if (Attack6 != null && Envir.Time > AttackTimes[6])
                {
                    if (Attack6.IgnorePet && Target.Race == ObjectType.Monster)
                        FindNonPetTarget();
                    if (InCustomRange(CurrentLocation, Target.CurrentLocation, Attack6.HitRange))
                    {
                        PerformAttack6();
                        AttackTimes[6] = Envir.Time + Attack6.AttkTime;
                    }
                }
                AttackTime = Envir.Time + AttackSpeed;
            }

            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }
        }
        #endregion
    }
}
