using Client.MirControls;
using Client.MirGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MirScenes.Dialogs
{
    public class EventDialog : MirImageControl
    {
        public MirLabel EventName, ObjectiveTitle, Objective, Percentage, RemainingCount, CompletionPercentage;
        int completedPercentage = 0;
        public List<MirLabel> MonsterObjectives = new List<MirLabel>();
        public List<MirLabel> ObjectiveMessages = new List<MirLabel>();
        public Font QuestFont = new Font(Settings.FontName, 8F);
        public MirImageControl _progress;
        public EventDialog()
        {
            Index = 18;
            Library = Libraries.Prguse3;
            Sort = true;
            Location = new Point(Settings.ScreenWidth - 260, 300);
            DrawImage = false;
            Visible = false;
            Movable = true;

            BeforeDraw += (o, e) =>
            {
                Libraries.Prguse3.Draw(17, Location.X, Location.Y);
                Libraries.Prguse3.Draw(18, Location.X, Location.Y);

                Libraries.Prguse3.Draw(19, new Rectangle(0, 0, (int)(completedPercentage* 2), 20), new Point(Location.X + 10, Location.Y + 184), Color.White, false);


            };

            EventName = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Yellow,
                Location = new Point(20, 40),
                Parent = this,
                NotControl = true,
            };
            ObjectiveTitle = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Yellow,
                Location = new Point(20, 80),
                Parent = this,
                NotControl = true,
                Text = "Objective",
            };
            Objective = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.White,
                Location = new Point(20, 100),
                Parent = this,
                NotControl = true,
            };
            RemainingCount = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.White,
                Location = new Point(20, 140),
                Parent = this,
                NotControl = true,
            };
            CompletionPercentage = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.White,
                Location = new Point(20, 160),
                Parent = this,
                NotControl = true,
            };
            _progress = new MirImageControl
            {
                NotControl = true,
                Location = new Point
                {
                    X = 14,
                    Y = 186
                },
                Index = 19,
                Library = Libraries.Prguse3,
                Parent = this,
                DrawImage = false,
            };
            _progress.BeforeDraw += _progress_BeforeDraw;
        }

        private void _progress_BeforeDraw(object sender, EventArgs e)
        {
            if (_progress.Library == null)
                return;
            if (currObjs == null ||
                currObjs.Count == 0)
                return;
            int totalCount = 0;
            int currentCount = 0;
            foreach (var monObj in currObjs)
            {
                totalCount += monObj.MonsterTotalCount;
                currentCount = monObj.MonsterTotalCount - monObj.MonsterAliveCount;
            }

            double percent = currentCount / (double)totalCount;
            if (percent > 1) percent = 1;
            if (percent < 0) return;
            Point DrawLocation = new Point(DisplayLocation.X, DisplayLocation.Y);
            Rectangle rect = new Rectangle
            {
                Size = new Size((int)((_progress.Size.Width - 3) * percent), _progress.Size.Height)
            };
            _progress.Library.DrawBlend(19, rect, _progress.DisplayLocation, Color.White, false);
        }

        public List<string> GetLines(string objective, int lineMaxLength)
        {
            List<string> lines = new List<string>();
            if (objective.Length > lineMaxLength)
            {
                var words = objective.Split(' ');
                if (words.Length > 1)
                {
                    string constructString = string.Empty;
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (constructString.Length + words[i].Length >= lineMaxLength)
                        {
                            lines.Add(constructString);
                            constructString = words[i];
                        }
                        else
                        {
                            if (constructString.Length == 0)
                                constructString = words[i];
                            else
                                constructString = string.Format("{0} {1}", constructString, words[i]);

                            if (constructString.Length >= lineMaxLength || i == words.Length - 1)
                            {
                                lines.Add(constructString);
                                constructString = words[i];

                            }
                        }
                    }
                }
                return lines;
            }
            else
                return new List<string>() { objective };
        }
        List<MonsterEventObjective> currObjs;
        public void UpdateDialog(string eventName, string objectiveMsg, List<MonsterEventObjective> objectives, int stage)
        {
            foreach (MirLabel lable in MonsterObjectives)
                lable.Dispose();
            if (currObjs != null &&
                currObjs.Count > 0)
                currObjs.Clear();
            currObjs = new List<MonsterEventObjective>();
            currObjs = objectives;
            ObjectiveMessages.Clear();
            MonsterObjectives.Clear();

            if (!string.IsNullOrEmpty(eventName))
                EventName.Text = eventName;
            int increment = 0;
            if (stage > 0)
            {
                ObjectiveMessages.Add(new MirLabel
                {
                    AutoSize = true,
                    OutLine = true,
                    Text = string.Format("Invasion Stage {0}", stage),
                    Visible = true,
                    ForeColour = Color.GreenYellow,
                    Location = new Point(20, 110 + increment)
                });
                increment += 15;
            }
            if (!string.IsNullOrEmpty(objectiveMsg))
            {
                int msgWidth = Settings.Resolution > 800 ? 30 : 17;
                List<string> msgs = GetLines(objectiveMsg, msgWidth);
                for (int i = 0; i < msgs.Count; i++)
                {
                    ObjectiveMessages.Add(new MirLabel
                    {
                        AutoSize = true,
                        OutLine = true,
                        Parent = this,
                        Text = msgs[i],
                        Visible = true,
                        ForeColour = Color.LightYellow,
                        Location = new Point(20, 94 + increment)
                    });
                    increment += 15;
                }
            }
            int totalCount = 0;
            int currentCount = 0;
            foreach (var monObj in objectives)
            {
                MirLabel lblMon = new MirLabel
                {
                    Text = string.Format("{0} : {1}/{2}", monObj.MonsterName, monObj.MonsterTotalCount - monObj.MonsterAliveCount, monObj.MonsterTotalCount),
                    AutoSize = true,
                    Font = QuestFont,
                    ForeColour = Color.YellowGreen,
                    Location = new Point(20, 98 + increment),
                    OutLine = true,
                    Parent = this,
                    Visible = true
                };
                totalCount += monObj.MonsterTotalCount;
                currentCount = monObj.MonsterTotalCount - monObj.MonsterAliveCount;
                increment += 15;
                MonsterObjectives.Add(lblMon);
            }
        }
 
        public void Show()
        {
            if (Visible) return;
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;

            foreach (MirLabel lable in MonsterObjectives)
                lable.Dispose();
            if (currObjs != null &&
                currObjs.Count > 0)
                currObjs.Clear();
            foreach (MirLabel lable in MonsterObjectives)
                lable.Dispose();
            foreach (MirLabel label in ObjectiveMessages)
                label.Dispose();
            currObjs.Clear();
            EventName.Text = "";
            Objective.Text = "";
            ObjectiveMessages.Clear();
            MonsterObjectives.Clear();
        }
    }
}