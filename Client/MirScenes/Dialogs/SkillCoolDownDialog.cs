using Client.MirControls;
using Client.MirGraphics;
using Client.MirObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MirScenes.Dialogs
{
    public class SkillCoolDownDialog : MirControl
    {
        public bool HasSkill = false;

        //public bool TopBind = !Settings.SkillMode;
        public List<SkillCoolDownInfo> SkillCoolDowns = new List<SkillCoolDownInfo>();
        public SkillCoolDownDialog()
        {
            Location = new Point((Settings.ScreenWidth - 150), 52);
            //Point((Settings.ScreenWidth - 150) - (buff.Hero? h : i - h) * 23 , 2 + (buff.Hero ? 25 : 0));         
        }

        public void AddSkillCoolDown(SkillCoolDownInfo info)
        {
            bool skillFound = false;
            for (int i = 0; i < SkillCoolDowns.Count; i++)
            {
                if (SkillCoolDowns[i].SpellType == info.SpellType && !skillFound)
                {
                    skillFound = true;
                    SkillCoolDowns[i].Visible = false;
                    if (SkillCoolDowns[i].IconIndex != info.IconIndex)
                        SkillCoolDowns[i].IconIndex = info.IconIndex;
                    SkillCoolDowns[i].CoolDown = info.CoolDown;
                    /*
                    SkillCoolDowns[i].CDBack = new MirImageControl()
                    {
                        Index = info.IconIndex,
                        Library = Libraries.MagIcon,
                        Parent = this,
                        Visible = info.CoolDown > 0 ? true : false,
                    };
                    SkillCoolDowns[i].CDImage = new MirAnimatedControl
                    {
                        Index = 1260,
                        AnimationCount = (22 - 0),
                        AnimationDelay = 100,
                        Library = Libraries.Prguse2,
                        Parent = info.CDBack,
                        Location = new Point(0, 0),
                        NotControl = true,
                        UseOffSet = true,
                        Loop = false,
                        Animated = true,
                        Opacity = 0.6F
                    };
                    */
                }
            }
            if (!skillFound)
            {
                info.CDBack = new MirImageControl()
                {
                    Index = info.IconIndex,
                    Library = Libraries.MagIcon,
                    Parent = this,
                    Visible = info.CoolDown > 0 ? true : false
                };
                info.CDImage = new MirAnimatedControl
                {
                    Index = 1260,
                    AnimationCount = (22 - 0),
                    AnimationDelay = 100,
                    Library = Libraries.Prguse2,
                    Parent = info.CDBack,
                    Location = new Point(0, 0),
                    NotControl = true,
                    UseOffSet = true,
                    Loop = false,
                    Animated = true,
                    Opacity = 0.6F
                };
                if (SkillCoolDowns.Count >= 1)
                    info.Location = new Point(SkillCoolDowns[SkillCoolDowns.Count - 1].DisplayRectangle.Left - SkillCoolDowns[SkillCoolDowns.Count - 1].Size.Width, 0);
                else
                    info.Location = new Point(DisplayRectangle.Right - info.Size.Width, 0);
                SkillCoolDowns.Add(info);
            }

            Process();
        }
        public void Show()
        {
            if (Visible) return;
            if (!HasSkill) return;

            Visible = true;
        }
        public void Process()
        {
            if (!Visible)
                Visible = true;
            Update();
            //ProcessSkillDelay();
        }
        public void Hide()
        {
            if (!Visible) return;
            Visible = false;
        }

        public void Update()
        {
            if (SkillCoolDowns != null && SkillCoolDowns.Count != 0)
            {
                for (int i = 0; i < SkillCoolDowns.Count; i++)
                {
                    if (SkillCoolDowns[i].CoolDown < CMain.Time)
                    {
                        //SkillCoolDowns[i].Visible = false;
                        if (SkillCoolDowns[i].CDImage != null &&
                            !SkillCoolDowns[i].CDImage.IsDisposed)
                        {
                            SkillCoolDowns[i].CDImage.Dispose();
                            //SkillCoolDowns[i].CDImage = null;
                        }
                        if (SkillCoolDowns[i].CDBack != null &&
                            !SkillCoolDowns[i].CDBack.IsDisposed)
                        {
                            SkillCoolDowns[i].CDBack.Dispose();
                            //SkillCoolDowns[i].CDBack = null;
                        }
                        SkillCoolDowns[i].CoolDown = 0;
                    }
                }
                ProcessSkillDelay();
            }            
        }

        private void ProcessSkillDelay()
        {
            if (SkillCoolDowns != null && SkillCoolDowns.Count != 0)
            {
                int x = SkillCoolDowns[0].DisplayRectangle.Left - 15;
                for (int i = 0; i < SkillCoolDowns.Count; i++)
                {
                    if (SkillCoolDowns[i].CoolDown > CMain.Time)
                    {
                        SkillCoolDowns[i].Visible = true;
                        SkillCoolDowns[i].Location = new Point(x, 0);
                        x = Location.X + Size.Width - (15 * i + 1);
                        if (SkillCoolDowns[i].CDBack != null)
                            SkillCoolDowns[i].CDBack.Visible = true;
                        int totalFrames = 22;
                        long timeLeft = SkillCoolDowns[i].CoolDown - CMain.Time;

                        if (timeLeft < 100 &&
                            SkillCoolDowns[i].CDImage != null &&
                            !SkillCoolDowns[i].CDImage.IsDisposed)
                        {
                            if (timeLeft > 0)
                                SkillCoolDowns[i].CDImage.Dispose();
                            else
                                continue;
                        }

                        int delayPerFrame = (int)(SkillCoolDowns[i].Delay / totalFrames);
                        int startFrame = totalFrames - (int)(timeLeft / delayPerFrame);
                        if (startFrame < 0)
                            startFrame = 0;
                        if (startFrame > 22)
                            startFrame = 22;
                        if (SkillCoolDowns[i].CDBack != null)
                            SkillCoolDowns[i].CDBack.Dispose();
                        if (SkillCoolDowns[i].CDImage != null)
                            SkillCoolDowns[i].CDImage.Dispose();
                        SkillCoolDowns[i].CDBack = new MirImageControl()
                        {
                            Index = SkillCoolDowns[i].IconIndex,
                            Library = Libraries.MagIcon,
                            Parent = this,
                            Visible = SkillCoolDowns[i].CoolDown > 0 ? true : false,
                        };
                        

                        SkillCoolDowns[i].CDImage = new MirAnimatedControl
                        {
                            Index = 1260 + startFrame,
                            AnimationCount = (totalFrames - startFrame),
                            AnimationDelay = delayPerFrame,
                            Library = Libraries.Prguse2,
                            Parent = SkillCoolDowns[i].CDBack,
                            Location = new Point(0, 0),
                            NotControl = true,
                            UseOffSet = true,
                            Loop = false,
                            Animated = true,
                            Opacity = 0.6F
                        };

                    }
                    else
                    {
                        if (SkillCoolDowns[i].CDImage != null &&
                            !SkillCoolDowns[i].CDImage.IsDisposed)
                        {
                            SkillCoolDowns[i].CDImage.Dispose();
                            //SkillCoolDowns[i].CDImage = null;
                        }
                        if (SkillCoolDowns[i].CDBack != null &&
                            !SkillCoolDowns[i].CDBack.IsDisposed)
                        {
                            SkillCoolDowns[i].CDBack.Dispose();
                            //SkillCoolDowns[i].CDBack = null;
                        }
                        SkillCoolDowns[i].Visible = false;
                        //SkillCoolDowns[i].CDBack.Visible = false;
                    }
                }
            }
        }
    }


    public class SkillCoolDownInfo : MirControl
    {
        //  The Spell type
        public Spell SpellType;
        //  The Current Cool-Down
        public long CoolDown;
        //  The Skill Delay
        public long Delay;
        //  The Background
        public MirImageControl CDBack;
        //  The CD Image
        public MirAnimatedControl CDImage;

        public int IconIndex;
    }
}
