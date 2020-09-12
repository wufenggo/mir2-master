using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirObjects;
using Client.MirSounds;
using Microsoft.DirectX.Direct3D;
using Font = System.Drawing.Font;
using S = ServerPackets;
using C = ClientPackets;
using Effect = Client.MirObjects.Effect;

using Client.MirScenes.Dialogs;
using System.Drawing.Imaging;
using Client.Custom;

namespace Client.MirScenes
{
    public enum PanelType
    {
        Sell, 
        Repair, 
        SpecialRepair,
        Consign, 
        Disassemble, 
        Downgrade,
        Reset,
        Refine,
        CheckRefine,
        CollectRefine,
        ReplaceWedRing,
    }

    public class ProcessChecking
    {

    }

    public sealed class GameScene : MirScene
    {
        public static GameScene Scene;

        public static UserObject User
        {
            get { return MapObject.User; }
            set { MapObject.User = value; }
        }

        public static S.HeroInformation Hero = null;
        public HeroBehaviour heroBehaviour = HeroBehaviour.None;
        public static long OrigMoveTime, OrigAttackTime, OrigNextRun, OrigLastRun;
        public static long MoveTime, AttackTime, NextRunTime, LogTime, LastRunTime , UpdateQuestDisplay = 0;
        public static bool OrigCanMove, OrigCanRun;
        public static bool CanMove, CanRun;
        public MapControl MapControl;
        public MainDialog MainDialog;
        public MainDialogLeft MainDialogLeft;
        public MainDialogRight MainDialogRight;
        public ChatDialog ChatDialog;
        public ChatControlBar ChatControl;
        public InventoryDialog InventoryDialog;
        public CharacterDialog CharacterDialog;
        public StorageDialog StorageDialog;
        public BeltDialog BeltDialog;
        public MiniMapDialog MiniMapDialog;
        public ComboBarDialog ComboBarBialog;
        public InspectDialog InspectDialog;
        public OptionDialog OptionDialog;
        public MenuDialog MenuDialog;
        public NPCDialog NPCDialog;
        public NPCGoodsDialog NPCGoodsDialog;
        public NPCDropDialog NPCDropDialog;
        public NPCAwakeDialog NPCAwakeDialog;
        public HelpDialog HelpDialog;
        public MountDialog MountDialog;
        public FishingDialog FishingDialog;
        public FishingStatusDialog FishingStatusDialog;
        public RefineDialog RefineDialog;
        public BossInfoDialog BossDialog;
        public GroupDialog GroupDialog;
        public GuildDialog GuildDialog;
        public BounsDialog BounsDialog;
        public BigMapDialog BigMapDialog;
        public TrustMerchantDialog TrustMerchantDialog;
        public CharacterDuraPanel CharacterDuraPanel;
        public TradeDialog TradeDialog;
        public GuestTradeDialog GuestTradeDialog;
        public List<SkillBarDialog> SkillBarDialogs = new List<SkillBarDialog>();
        public ChatOptionDialog ChatOptionDialog;
        public ChatNoticeDialog ChatNoticeDialog;
        public TimerDialog TimerDialog;
        public QuestListDialog QuestListDialog;
        public QuestDetailDialog QuestDetailDialog;
        public QuestDiaryDialog QuestLogDialog;
        public QuestTrackingDialog QuestTrackingDialog;
        public RankingDialog RankingDialog;
        public CraftingDialog CraftingDialog;
        public MailListDialog MailListDialog;
        public MailComposeLetterDialog MailComposeLetterDialog;
        public MailComposeParcelDialog MailComposeParcelDialog;
        public MailReadLetterDialog MailReadLetterDialog;
        public MailReadParcelDialog MailReadParcelDialog;

        public IntelligentCreatureDialog IntelligentCreatureDialog;
        public IntelligentCreatureOptionsDialog IntelligentCreatureOptionsDialog;
        public IntelligentCreatureOptionsGradeDialog IntelligentCreatureOptionsGradeDialog;
        public ItemRentingDialog ItemRentingDialog;
        public ItemRentDialog ItemRentDialog;
        public ItemRentalDialog ItemRentalDialog;
        public FriendDialog FriendDialog;
        public MemoDialog MemoDialog;
        public RelationshipDialog RelationshipDialog;
        public MentorDialog MentorDialog;
        public GameShopDialog GameShopDialog;
        public ReportDialog ReportDialog;
        public GuildTerritoryDialog GuildTerritoryDialog;
        public GuildBoardDialog GuildBoardDialog;
        public GuildReadLetterDialog GuildReadLetterDialog;
        public GuildLetterDialog GetGuildLetterDialog;
        public HeroHeaderDialog HeroHeaderDialog;
        public HeroDialog HeroDialog;
        public HeroCreationDialog HeroCreationDialog;
        public HeroCharacterDialog HeroCharacterDialog;
        public HeroInventoryDialog HeroInventoryDialog;
        public HeroBeltDialog HeroBeltDialog;
        public HeroHelpDialog HeroHelpDialog;
        public HeroAIDialog HeroAIDialog;
        public HeroCharacterDuraPanel HeroCharacterDuraPanel;
        public HeroStashDialog HeroStashDialog;
        public HeroRankingDialog HeroRankingDialog;
        public HeroAssignKeyPanel HeroAssignKeyPanel;
        public Recipe_Shop Recipe_ShopDialog;
        public GroupInfoDialog GroupInfoDialog;
        public JournalDialog JournalDialog;
        public SkillCoolDownDialog SkillCoolDownDialog;
        public KeyboardLayoutDialog KeyboardLayoutDialog;
        public LoginNoticeDialog LoginNoticeDialog;
        public LMS_BR_SignupDialog LMS_BR_SignupDialog;

        public EventDialog EventDialog;

        public byte LightEffectFrame = 0;
        public int LightEffectRotation = 0;


        public static List<ItemInfo> ItemInfoList = new List<ItemInfo>();
        public static List<UserId> UserIdList = new List<UserId>();
        public static List<ChatItem> ChatItemList = new List<ChatItem>();
        public static List<ClientQuestInfo> QuestInfoList = new List<ClientQuestInfo>();
        public static List<GameShopItem> GameShopInfoList = new List<GameShopItem>();

        public List<Buff> Buffs = new List<Buff>();

        public static UserItem[] Storage = new UserItem[80];
        public static UserItem[] GuildStorage = new UserItem[112];
        public static UserItem[] Refine = new UserItem[16];
        public static UserItem HoverItem;
        public static MirItemCell SelectedCell;


        public static UserItem[] ItemSocketing = new UserItem[4];
        public static UserItem[] CraftEndResult = new UserItem[1];
        public UserItem[] _Items = new UserItem[2];
        public SocketingDialog SocketDialog;
        public UserItem Hover_Item
        {
            get { return HoverItem; }
            set { HoverItem = value; }
        }
        public List<ItemInfo> Item_Info_List
        {
            get { return ItemInfoList; }
            set { ItemInfoList = value; }
        }


        public static bool PickedUpGold;
        public MirControl ItemLabel, MailLabel, MemoLabel, GuildBuffLabel;
        public MirControl MonsterLabel;
        public static long UseItemTime, PickUpTime, DropViewTime, TargetDeadTime;
        public static uint RealGold, RealCredit, RealguildGold;
        public static uint Gold, Credit;
        public static long InspectTime;
        public bool ShowReviveMessage;


        public bool NewMail;
        public int NewMailCounter = 0;


        public AttackMode AMode;
        public PetMode PMode;
        public HeroMode HMode;
        public LightSetting Lights;

        public static long NPCTime;
        public static uint NPCID;
        public static float NPCRate;
        public static uint DefaultNPCID;

        public bool craftClciked = false;

        public long ToggleTime;
        public static bool Slaying, Thrusting, HalfMoon, CrossHalfMoon, DoubleSlash, TwinDrakeBlade, FlamingSword, SliceNDice, BlazingSword, CresSlash;
        public static long SpellTime;

        public long PingTime;
        public long NextPing = 0;

        public MirLabel[] OutputLines = new MirLabel[10];
        public List<OutPutMessage> OutputMessages = new List<OutPutMessage>();

        public List<MirImageControl> BuffList = new List<MirImageControl>();

        public long OutputDelay;

        public MirImageControl BossInfoBase = new MirImageControl();
        public MirImageControl BossInfoImg = new MirImageControl();
        public MirLabel BossInfoLabel = new MirLabel();

        public GameScene()
        {
            MapControl.AutoRun = false;
            MapControl.AutoHit = false;
            Slaying = false;
            Thrusting = false;
            HalfMoon = false;
            CrossHalfMoon = false;
            DoubleSlash = false;
            TwinDrakeBlade = false;
            FlamingSword = false;
            SliceNDice = false;
            Scene = this;
            BackColour = Color.Transparent;
            MoveTime = CMain.Time;

            KeyDown += GameScene_KeyDown;
            MainDialog = new MainDialog { Parent = this };
            MainDialogLeft = new MainDialogLeft { Parent = this };
            MainDialogRight = new MainDialogRight { Parent = this };
            ChatDialog = new ChatDialog { Parent = this };
            ChatControl = new ChatControlBar { Parent = this };
            InventoryDialog = new InventoryDialog { Parent = this };
            CharacterDialog = new CharacterDialog { Parent = this, Visible = false };
            BeltDialog = new BeltDialog { Parent = this };
            StorageDialog = new StorageDialog { Parent = this, Visible = false };
            MiniMapDialog = new MiniMapDialog { Parent = this };
            ComboBarBialog = new ComboBarDialog { Parent = this };
            InspectDialog = new InspectDialog { Parent = this, Visible = false };
            OptionDialog = new OptionDialog { Parent = this, Visible = false };
            MenuDialog = new MenuDialog { Parent = this, Visible = false };
            NPCDialog = new NPCDialog { Parent = this, Visible = false };
            NPCGoodsDialog = new NPCGoodsDialog { Parent = this, Visible = false };
            NPCDropDialog = new NPCDropDialog { Parent = this, Visible = false };
            NPCAwakeDialog = new NPCAwakeDialog { Parent = this, Visible = false };          
            MountDialog = new MountDialog { Parent = this, Visible = false };
            FishingDialog = new FishingDialog { Parent = this, Visible = false };
            FishingStatusDialog = new FishingStatusDialog { Parent = this, Visible = false };            
            GroupDialog = new GroupDialog { Parent = this, Visible = false };
            GuildDialog = new GuildDialog { Parent = this, Visible = false };
            BounsDialog = new BounsDialog { Parent = this, Visible = false };
            GuildTerritoryDialog = new GuildTerritoryDialog { Parent = this, Visible = false };
            BigMapDialog = new BigMapDialog { Parent = this, Visible = false };
            TrustMerchantDialog = new TrustMerchantDialog { Parent = this, Visible = false };
            CharacterDuraPanel = new CharacterDuraPanel { Parent = this, Visible = false };
            TradeDialog = new TradeDialog { Parent = this, Visible = false };
            GuestTradeDialog = new GuestTradeDialog { Parent = this, Visible = false };
            SkillBarDialog Bar1 = new SkillBarDialog { Parent = this, Visible = false, BarIndex = 0 };
            SkillBarDialogs.Add(Bar1);
            SkillBarDialog Bar2 = new SkillBarDialog { Parent = this, Visible = false, BarIndex = 1 };
            SkillBarDialogs.Add(Bar2);
            ChatOptionDialog = new ChatOptionDialog { Parent = this, Visible = false };
            ChatNoticeDialog = new ChatNoticeDialog { Parent = this, Visible = false };
            TimerDialog = new TimerDialog { Parent = this, Visible = false };
            QuestListDialog = new QuestListDialog { Parent = this, Visible = false };
            QuestDetailDialog = new QuestDetailDialog {Parent = this, Visible = false};
            QuestTrackingDialog = new QuestTrackingDialog { Parent = this, Visible = false };
            QuestLogDialog = new QuestDiaryDialog {Parent = this, Visible = false};

            RankingDialog = new RankingDialog { Parent = this, Visible = false };
            CraftingDialog = new CraftingDialog { Parent = this, Visible = false };
            MailListDialog = new MailListDialog { Parent = this, Visible = false };
            MailComposeLetterDialog = new MailComposeLetterDialog { Parent = this, Visible = false };
            MailComposeParcelDialog = new MailComposeParcelDialog { Parent = this, Visible = false };
            MailReadLetterDialog = new MailReadLetterDialog { Parent = this, Visible = false };
            MailReadParcelDialog = new MailReadParcelDialog { Parent = this, Visible = false };

            IntelligentCreatureDialog = new IntelligentCreatureDialog { Parent = this, Visible = false };
            IntelligentCreatureOptionsDialog = new IntelligentCreatureOptionsDialog { Parent = this, Visible = false };
            IntelligentCreatureOptionsGradeDialog = new IntelligentCreatureOptionsGradeDialog { Parent = this, Visible = false };

            RefineDialog = new RefineDialog { Parent = this, Visible = false };
            RelationshipDialog = new RelationshipDialog { Parent = this, Visible = false };
            FriendDialog = new FriendDialog { Parent = this, Visible = false };
            MemoDialog = new MemoDialog { Parent = this, Visible = false };
            MentorDialog = new MentorDialog { Parent = this, Visible = false };
            GameShopDialog = new GameShopDialog { Parent = this, Visible = false };
            Recipe_ShopDialog = new Recipe_Shop { Parent = this, Visible = false };
            ReportDialog = new ReportDialog { Parent = this, Visible = false };
            ItemRentingDialog = new ItemRentingDialog { Parent = this, Visible = false };
            ItemRentDialog = new ItemRentDialog { Parent = this, Visible = false };
            ItemRentalDialog = new ItemRentalDialog { Parent = this, Visible = false };
            BossDialog = new BossInfoDialog { Parent = this, Visible = false };
            HeroHeaderDialog = new HeroHeaderDialog { Parent = this };
            HeroDialog = new HeroDialog { Parent = this, Visible = false };
            HeroCreationDialog = new HeroCreationDialog { Parent = this, Visible = false };
            HeroCharacterDialog = new HeroCharacterDialog { Parent = this, Visible = false };
            HeroHelpDialog = new HeroHelpDialog { Parent = this, Visible = false };
            HeroInventoryDialog = new HeroInventoryDialog { Parent = this };
            HeroBeltDialog = new HeroBeltDialog { Parent = this , Visible = false};
            HeroAIDialog = new HeroAIDialog { Parent = this };
            HeroCharacterDuraPanel = new HeroCharacterDuraPanel { Parent = this, Visible = false };
            HelpDialog = new HelpDialog { Parent = this, Visible = false };
            HeroStashDialog = new HeroStashDialog { Parent = this, Visible = false };
            HeroRankingDialog = new HeroRankingDialog { Parent = this, Visible = false };
            GroupInfoDialog = new GroupInfoDialog { Parent = this, Visible = false };
            JournalDialog = new JournalDialog { Parent = this, Visible = false };
            SkillCoolDownDialog = new SkillCoolDownDialog { Parent = this, Visible = false };
            SocketDialog = new SocketingDialog { Parent = this, Visible = false };
            KeyboardLayoutDialog = new KeyboardLayoutDialog { Parent = this, Visible = false };
            GuildBoardDialog = new GuildBoardDialog { Parent = this, Visible = false };
            GuildReadLetterDialog = new GuildReadLetterDialog { Parent = this, Visible = false };
            LoginNoticeDialog = new LoginNoticeDialog { Parent = this, Visible = false };
            EventDialog = new EventDialog { Parent = this, Visible = false };
            LMS_BR_SignupDialog = new LMS_BR_SignupDialog { Parent = this, Visible = false };
            for (int i = 0; i < OutputLines.Length; i++)
                OutputLines[i] = new MirLabel
                {
                    AutoSize = true,
                    BackColour = Color.Transparent,
                    Font = new Font(Settings.FontName, 10F),
                    ForeColour = Color.LimeGreen,
                    Location = new Point(20, 25 + i * 13),
                    OutLine = true,
                };
        }

        public void OutputMessage(string message, OutputMessageType type = OutputMessageType.Normal)
        {
            OutputMessages.Add(new OutPutMessage { Message = message, ExpireTime = CMain.Time + 5000, Type = type });
            if (OutputMessages.Count > 10)
                OutputMessages.RemoveAt(0);
        }

        private void ProcessOuput()
        {
            for (int i = 0; i < OutputMessages.Count; i++)
            {
                if (CMain.Time >= OutputMessages[i].ExpireTime)
                    OutputMessages.RemoveAt(i);
            }

            for (int i = 0; i < OutputLines.Length; i++)
            {
                if (OutputMessages.Count > i)
                {
                    Color color;
                    switch (OutputMessages[i].Type)
                    {
                        case OutputMessageType.Quest:
                            color = Color.Gold;
                            break;
                        case OutputMessageType.Guild:
                            color = Color.DeepPink;
                            break;
                        default:
                            color = Color.LimeGreen;
                            break;
                    }

                    OutputLines[i].Text = OutputMessages[i].Message;
                    OutputLines[i].ForeColour = color;
                    OutputLines[i].Visible = true;
                }
                else
                {
                    OutputLines[i].Text = string.Empty;
                    OutputLines[i].Visible = false;
                }
            }
        }
        private void GameScene_KeyDown(object sender, KeyEventArgs e)
        {
            bool skillMode = Settings.SkillMode ? CMain.Tilde : CMain.Ctrl;
            //bool altBind = skillMode ? Settings.SkillSet : !Settings.SkillSet;
            foreach (KeyBind current in CMain.InputKeys.Keylist)
            {
                bool flag = current.CutomKey.Key == Keys.None;
                if (!flag)
                {
                    bool flag2 = current.CutomKey.Key != e.KeyCode;
                    if (!flag2)
                    {
                        bool flag3 = current.CutomKey.RequireAlt != 2 && current.CutomKey.RequireAlt != (CMain.Alt ? 1 : 0);
                        if (!flag3)
                        {
                            bool flag4 = current.CutomKey.RequireShift != 2 && current.CutomKey.RequireShift != (CMain.Shift ? 1 : 0);
                            if (!flag4)
                            {
                                bool flag5 = current.CutomKey.RequireCtrl != 2 && current.CutomKey.RequireCtrl != (CMain.Ctrl ? 1 : 0);
                                if (!flag5)
                                {
                                    bool flag6 = current.CutomKey.RequireTilde != 2 && current.CutomKey.RequireTilde != (CMain.Tilde ? 1 : 0);
                                    if (!flag6)
                                    {
                                        switch (current.function)
                                        {
                                            case KeybindOptions.Bar1Skill1:
                                                {
                                                    bool flag7 = CMain.Shift && MapObject.TargetObject == null;
                                                    if (!flag7)
                                                    {
                                                        UseSpell(1);
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Bar1Skill2:
                                                {
                                                    bool flag8 = CMain.Shift && MapObject.TargetObject == null;
                                                    if (!flag8)
                                                    {
                                                        UseSpell(2);
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Bar1Skill3:
                                                {
                                                    bool flag9 = CMain.Shift && MapObject.TargetObject == null;
                                                    if (!flag9)
                                                    {
                                                        UseSpell(3);
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Bar1Skill4:
                                                {
                                                    bool flag10 = CMain.Shift && MapObject.TargetObject == null;
                                                    if (!flag10)
                                                    {
                                                        UseSpell(4);
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Bar1Skill5:
                                                {
                                                    bool flag11 = CMain.Shift && MapObject.TargetObject == null;
                                                    if (!flag11)
                                                    {
                                                        UseSpell(5);
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Bar1Skill6:
                                                {
                                                    bool flag12 = CMain.Shift && MapObject.TargetObject == null;
                                                    if (!flag12)
                                                    {
                                                        UseSpell(6);
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Bar1Skill7:
                                                {
                                                    bool flag13 = CMain.Shift && MapObject.TargetObject == null;
                                                    if (!flag13)
                                                    {
                                                        UseSpell(7);
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Bar1Skill8:
                                                {
                                                    bool flag14 = CMain.Shift && MapObject.TargetObject == null;
                                                    if (!flag14)
                                                    {
                                                        UseSpell(8);
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Bar2Skill1:
                                                {
                                                    bool flag15 = CMain.Shift && MapObject.TargetObject == null;
                                                    if (!flag15)
                                                    {
                                                        UseSpell(9);
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Bar2Skill2:
                                                {
                                                    bool flag16 = CMain.Shift && MapObject.TargetObject == null;
                                                    if (!flag16)
                                                    {
                                                        UseSpell(10);
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Bar2Skill3:
                                                {
                                                    bool flag17 = CMain.Shift && MapObject.TargetObject == null;
                                                    if (!flag17)
                                                    {
                                                        UseSpell(11);
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Bar2Skill4:
                                                {
                                                    bool flag18 = CMain.Shift && MapObject.TargetObject == null;
                                                    if (!flag18)
                                                    {
                                                        UseSpell(12);
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Bar2Skill5:
                                                {
                                                    bool flag19 = CMain.Shift && MapObject.TargetObject == null;
                                                    if (!flag19)
                                                    {
                                                        UseSpell(13);
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Bar2Skill6:
                                                {
                                                    bool flag20 = CMain.Shift && MapObject.TargetObject == null;
                                                    if (!flag20)
                                                    {
                                                        UseSpell(14);
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Bar2Skill7:
                                                {
                                                    bool flag21 = CMain.Shift && MapObject.TargetObject == null;
                                                    if (!flag21)
                                                    {
                                                        UseSpell(15);
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Bar2Skill8:
                                                {
                                                    bool flag22 = CMain.Shift && MapObject.TargetObject == null;
                                                    if (!flag22)
                                                    {
                                                        UseSpell(16);
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Inventory:
                                            case KeybindOptions.Inventory2:
                                                {
                                                    bool flag23 = !InventoryDialog.Visible;
                                                    if (flag23)
                                                    {
                                                        InventoryDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        InventoryDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Equipment:
                                            case KeybindOptions.Equipment2:
                                                {
                                                    bool flag24 = !CharacterDialog.Visible || !CharacterDialog.CharacterPage.Visible;
                                                    if (flag24)
                                                    {
                                                        CharacterDialog.Show();
                                                        CharacterDialog.ShowCharacterPage();
                                                    }
                                                    else
                                                    {
                                                        CharacterDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Skills:
                                            case KeybindOptions.Skills2:
                                                {
                                                    bool flag25 = !CharacterDialog.Visible || !CharacterDialog.SkillPage.Visible;
                                                    if (flag25)
                                                    {
                                                        CharacterDialog.Show();
                                                        CharacterDialog.ShowSkillPage();
                                                    }
                                                    else
                                                    {
                                                        CharacterDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Creature:
                                                {
                                                    bool flag26 = !IntelligentCreatureDialog.Visible;
                                                    if (flag26)
                                                    {
                                                        IntelligentCreatureDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        IntelligentCreatureDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.MountWindow:
                                                {
                                                    bool flag27 = !MountDialog.Visible;
                                                    if (flag27)
                                                    {
                                                        MountDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        MountDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Mount:
                                                {
                                                    bool flag28 = !ChatDialog.ChatTextBox.Visible && GameScene.Scene.MountDialog.CanRide();
                                                    if (flag28)
                                                    {
                                                        GameScene.Scene.MountDialog.Ride();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Fishing:
                                                {
                                                    bool flag29 = !FishingDialog.Visible;
                                                    if (flag29)
                                                    {
                                                        FishingDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        FishingDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Skillbar:
                                                {
                                                    if (!Settings.SkillBar)
                                                        foreach (SkillBarDialog Bar in SkillBarDialogs)
                                                            Bar.Show();
                                                    else
                                                        foreach (SkillBarDialog Bar in SkillBarDialogs)
                                                            Bar.Hide();
                                                    break;
                                                }
                                            case KeybindOptions.Mentor:
                                                {
                                                    bool flag31 = !MentorDialog.Visible;
                                                    if (flag31)
                                                    {
                                                        MentorDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        MentorDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Relationship:
                                                {
                                                    bool flag32 = !RelationshipDialog.Visible;
                                                    if (flag32)
                                                    {
                                                        RelationshipDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        RelationshipDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Friends:
                                                {
                                                    bool flag33 = !FriendDialog.Visible;
                                                    if (flag33)
                                                    {
                                                        FriendDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        FriendDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Guilds:
                                                {
                                                    bool flag34 = !GuildDialog.Visible;
                                                    if (flag34)
                                                    {
                                                        GuildDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        GuildDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.GameShop:
                                                {
                                                    bool flag35 = !GameShopDialog.Visible;
                                                    if (flag35)
                                                    {
                                                        GameShopDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        GameShopDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Quests:
                                                {
                                                    bool flag36 = !QuestLogDialog.Visible;
                                                    if (flag36)
                                                    {
                                                        QuestLogDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        QuestLogDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Rental:
                                                {
                                                    bool flag37 = !ItemRentalDialog.Visible;
                                                    if (flag37)
                                                    {
                                                        ItemRentalDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        ItemRentalDialog.Hide();
                                                    }
                                                    break;
                                                }

                                            case KeybindOptions.Closeall:
                                                {
                                                    InventoryDialog.Hide();
                                                    CharacterDialog.Hide();
                                                    OptionDialog.Hide();
                                                    MenuDialog.Hide();
                                                    NPCDialog.Hide();
                                                    HelpDialog.Hide();
                                                    KeyboardLayoutDialog.Hide();
                                                    RankingDialog.Hide();
                                                    IntelligentCreatureDialog.Hide();
                                                    IntelligentCreatureOptionsDialog.Hide();
                                                    IntelligentCreatureOptionsGradeDialog.Hide();
                                                    MountDialog.Hide();
                                                    FishingDialog.Hide();
                                                    FriendDialog.Hide();
                                                    RelationshipDialog.Hide();
                                                    MentorDialog.Hide();
                                                    GroupDialog.Hide();
                                                    GuildDialog.Hide();
                                                    InspectDialog.Hide();
                                                    StorageDialog.Hide();
                                                    TrustMerchantDialog.Hide();
                                                    QuestListDialog.Hide();
                                                    QuestDetailDialog.Hide();
                                                    QuestLogDialog.Hide();
                                                    NPCAwakeDialog.Hide();
                                                    BigMapDialog.Hide();
                                                    LoginNoticeDialog.Hide();
                                                    LMS_BR_SignupDialog.Hide();
                                                    HeroCharacterDialog.Hide();
                                                    HeroInventoryDialog.Hide();
                                                    bool bEscExit = FishingStatusDialog.bEscExit;
                                                    if (bEscExit)
                                                    {
                                                        FishingStatusDialog.Cancel();
                                                    }
                                                    HeroInventoryDialog.Hide();
                                                    GameShopDialog.Hide();
                                                    MailComposeLetterDialog.Hide();
                                                    MailComposeParcelDialog.Hide();
                                                    MailListDialog.Hide();
                                                    MailReadLetterDialog.Hide();
                                                    MailReadParcelDialog.Hide();
                                                    JournalDialog.Hide();
                                                    CraftingDialog.Hide();
                                                    GameScene.Scene.DisposeItemLabel();
                                                    break;
                                                }
                                            case KeybindOptions.KeySettings:
                                                {
                                                    bool flag39 = !KeyboardLayoutDialog.Visible;
                                                    if (flag39)
                                                    {
                                                        KeyboardLayoutDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        KeyboardLayoutDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Options:
                                                {
                                                    bool flag40 = !OptionDialog.Visible;
                                                    if (flag40)
                                                    {
                                                        OptionDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        OptionDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Group:
                                                {
                                                    bool flag41 = !GroupDialog.Visible;
                                                    if (flag41)
                                                    {
                                                        GroupDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        GroupDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Belt:
                                                {
                                                    if (!BeltDialog.Visible) BeltDialog.Show();
                                                    else BeltDialog.Hide();
                                                    break;
                                                }
                                            case KeybindOptions.Pickup:
                                                if (CMain.Time > PickUpTime)
                                                {
                                                    PickUpTime = CMain.Time + 200;
                                                    Network.Enqueue(new C.PickUp());
                                                }
                                                break;
                                            case KeybindOptions.Belt1:
                                            case KeybindOptions.Belt1Alt:
                                                BeltDialog.Grid[0].UseItem();
                                                break;
                                            case KeybindOptions.Belt2:
                                            case KeybindOptions.Belt2Alt:
                                                BeltDialog.Grid[1].UseItem();
                                                break;
                                            case KeybindOptions.Belt3:
                                            case KeybindOptions.Belt3Alt:
                                                BeltDialog.Grid[2].UseItem();
                                                break;
                                            case KeybindOptions.Belt4:
                                            case KeybindOptions.Belt4Alt:
                                                BeltDialog.Grid[3].UseItem();
                                                break;
                                            case KeybindOptions.Belt5:
                                            case KeybindOptions.Belt5Alt:
                                                BeltDialog.Grid[4].UseItem();
                                                break;
                                            case KeybindOptions.Belt6:
                                            case KeybindOptions.Belt6Alt:
                                                BeltDialog.Grid[5].UseItem();
                                                break;
                                            case KeybindOptions.Logout:
                                                LogOut();
                                                break;
                                            case KeybindOptions.Exit:
                                                QuitGame();
                                                return;
                                            case KeybindOptions.CreaturePickup:
                                                Network.Enqueue(new C.IntelligentCreaturePickup
                                                {
                                                    MouseMode = true,
                                                    Location = MapControl.MapLocation
                                                });
                                                break;
                                            case KeybindOptions.CreatureAutoPickup:
                                                Network.Enqueue(new C.IntelligentCreaturePickup
                                                {
                                                    MouseMode = false,
                                                    Location = MapControl.MapLocation
                                                });
                                                break;
                                            case KeybindOptions.Minimap:
                                                MiniMapDialog.Toggle();
                                                break;
                                            case KeybindOptions.Bigmap:
                                                BigMapDialog.Toggle();
                                                break;
                                            case KeybindOptions.Trade:
                                                Network.Enqueue(new C.TradeRequest());
                                                break;
                                            case KeybindOptions.ChangeAttackmode:
                                                switch (AMode)
                                                {
                                                    case AttackMode.Peace:
                                                        Network.Enqueue(new C.ChangeAMode { Mode = AttackMode.Group });
                                                        return;
                                                    case AttackMode.Group:
                                                        Network.Enqueue(new C.ChangeAMode { Mode = AttackMode.Guild });
                                                        return;
                                                    case AttackMode.Guild:
                                                        Network.Enqueue(new C.ChangeAMode { Mode = AttackMode.EnemyGuild });
                                                        return;
                                                    case AttackMode.EnemyGuild:
                                                        Network.Enqueue(new C.ChangeAMode { Mode = AttackMode.RedBrown });
                                                        return;
                                                    case AttackMode.RedBrown:
                                                        Network.Enqueue(new C.ChangeAMode { Mode = AttackMode.All });
                                                        return;
                                                    case AttackMode.All:
                                                        Network.Enqueue(new C.ChangeAMode { Mode = AttackMode.Peace });
                                                        return;
                                                }
                                                break;
                                            case KeybindOptions.AttackmodePeace:
                                                Network.Enqueue(new C.ChangeAMode
                                                {
                                                    Mode = AttackMode.Peace
                                                });
                                                return;
                                            case KeybindOptions.AttackmodeGroup:
                                                Network.Enqueue(new C.ChangeAMode
                                                {
                                                    Mode = AttackMode.Group
                                                });
                                                return;
                                            case KeybindOptions.AttackmodeGuild:
                                                Network.Enqueue(new C.ChangeAMode
                                                {
                                                    Mode = AttackMode.Guild
                                                });
                                                return;
                                            case KeybindOptions.AttackmodeEnemyguild:
                                                Network.Enqueue(new C.ChangeAMode
                                                {
                                                    Mode = AttackMode.EnemyGuild
                                                });
                                                return;
                                            case KeybindOptions.AttackmodeRedbrown:
                                                Network.Enqueue(new C.ChangeAMode
                                                {
                                                    Mode = AttackMode.RedBrown
                                                });
                                                return;
                                            case KeybindOptions.AttackmodeAll:
                                                Network.Enqueue(new C.ChangeAMode
                                                {
                                                    Mode = AttackMode.All
                                                });
                                                return;
                                            case KeybindOptions.ChangePetmode:
                                                {
                                                    PetMode pMode = PMode;
                                                    if (pMode == PetMode.Both)
                                                    {
                                                        Network.Enqueue(new C.ChangePMode
                                                        {
                                                            Mode = PetMode.MoveOnly
                                                        });
                                                        return;
                                                    }
                                                    if (pMode == PetMode.MoveOnly)
                                                    {
                                                        Network.Enqueue(new C.ChangePMode
                                                        {
                                                            Mode = PetMode.AttackOnly
                                                        });
                                                        return;
                                                    }
                                                    if (pMode == PetMode.AttackOnly)
                                                    {
                                                        Network.Enqueue(new C.ChangePMode
                                                        {
                                                            Mode = PetMode.None
                                                        });
                                                        return;
                                                    }
                                                    if (pMode == PetMode.None)
                                                    {
                                                        Network.Enqueue(new C.ChangePMode
                                                        {
                                                            Mode = PetMode.Both
                                                        });
                                                        return;
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.PetmodeBoth:
                                                Network.Enqueue(new C.ChangePMode
                                                {
                                                    Mode = PetMode.Both
                                                });
                                                return;
                                            case KeybindOptions.PetmodeMoveonly:
                                                Network.Enqueue(new C.ChangePMode
                                                {
                                                    Mode = PetMode.MoveOnly
                                                });
                                                return;
                                            case KeybindOptions.PetmodeAttackonly:
                                                Network.Enqueue(new C.ChangePMode
                                                {
                                                    Mode = PetMode.AttackOnly
                                                });
                                                return;
                                            case KeybindOptions.PetmodeNone:
                                                Network.Enqueue(new C.ChangePMode
                                                {
                                                    Mode = PetMode.None
                                                });
                                                return;
                                            case KeybindOptions.Help:
                                                {
                                                    bool flag46 = !HelpDialog.Visible;
                                                    if (flag46)
                                                    {
                                                        HelpDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        HelpDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Autorun:
                                                MapControl.AutoRun = !MapControl.AutoRun;
                                                break;
                                            case KeybindOptions.Cameramode:
                                                {
                                                    bool flag47 = !MainDialog.Visible;
                                                    if (flag47)
                                                    {
                                                        MainDialog.Show();
                                                        MainDialogLeft.Show();
                                                        MainDialogRight.Show();
                                                        ChatDialog.Show();
                                                        BeltDialog.Show();
                                                        ChatControl.Show();
                                                        MiniMapDialog.Show();
                                                        CharacterDuraPanel.Show();
                                                        HeroCharacterDuraPanel.Show();
                                                    }
                                                    else
                                                    {
                                                        MainDialog.Hide();
                                                        MainDialogLeft.Hide();
                                                        MainDialogRight.Hide();
                                                        ChatDialog.Hide();
                                                        BeltDialog.Hide();
                                                        ChatControl.Hide();
                                                        MiniMapDialog.Hide();
                                                        CharacterDuraPanel.Hide();
                                                        HeroCharacterDuraPanel.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.DropView:
                                                {
                                                    if (CMain.Time > DropViewTime)
                                                        DropViewTime = CMain.Time + 5000;
                                                    break;
                                                }
                                            case KeybindOptions.Ranking:
                                                {
                                                    bool flag49 = !RankingDialog.Visible;
                                                    if (flag49)
                                                    {
                                                        RankingDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        RankingDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Mail:
                                                {
                                                    bool flag53 = !MailListDialog.Visible;
                                                    if (flag53)
                                                    {
                                                        MailListDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        MailListDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.Crafting:
                                                {
                                                    bool flag54 = !CraftingDialog.Visible;
                                                    if (flag54)
                                                    {
                                                        CraftingDialog.Show();
                                                    }
                                                    else
                                                    {
                                                        CraftingDialog.Hide();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.TargetDead:
                                                if (CMain.Time > TargetDeadTime)
                                                    TargetDeadTime = CMain.Time + 5000;
                                                break;
                                            case KeybindOptions.SummonHero:
                                                if (GameScene.Scene.HeroHeaderDialog.delaySpawn > CMain.Time) return;
                                                GameScene.Scene.HeroHeaderDialog.delaySpawn = CMain.Time + 1000;

                                                if (User.HeroState == HeroState.Spawned)
                                                {
                                                    Network.Enqueue(new C.Chat { Message = "@DESPAWNHERO" });
                                                }
                                                else if (User.HeroState == HeroState.Unspawned)
                                                {
                                                    Network.Enqueue(new C.Chat { Message = "@SPAWNHERO" });
                                                }
                                                break;
                                            case KeybindOptions.HeroInventory:
                                                {
                                                    if (GameScene.Hero != null && GameScene.User.HeroState != HeroState.Unspawned && !HeroInventoryDialog.Visible) HeroInventoryDialog.Show();
                                                    else HeroInventoryDialog.Hide();
                                                    break;
                                                }
                                            case KeybindOptions.HeroEquipment:
                                                {
                                                    if (GameScene.Scene.HeroCharacterDialog.Visible && GameScene.Scene.HeroCharacterDialog.CharacterPage.Visible)
                                                        GameScene.Scene.HeroCharacterDialog.Hide();
                                                    else
                                                    {
                                                        GameScene.Scene.HeroCharacterDialog.Show();
                                                        GameScene.Scene.HeroCharacterDialog.ShowCharacterPage();
                                                    }
                                                    break;
                                                }
                                            case KeybindOptions.HeroSkills:
                                                {
                                                    if (GameScene.Hero != null && GameScene.User.HeroState != HeroState.Unspawned && (!HeroCharacterDialog.Visible || !HeroCharacterDialog.SkillPage.Visible))
                                                    {
                                                        HeroCharacterDialog.Show();
                                                        HeroCharacterDialog.ShowSkillPage();
                                                    }
                                                    else HeroCharacterDialog.Hide();
                                                    break;
                                                }
                                            case KeybindOptions.HeroBelt1:
                                            case KeybindOptions.HeroBelt1Alt:
                                                if (GameScene.Hero != null && GameScene.User.HeroState == HeroState.Spawned)
                                                    HeroBeltDialog.Grid[0].UseItem(); ;
                                                break;
                                            case KeybindOptions.HeroBelt2:
                                            case KeybindOptions.HeroBelt2Alt:
                                                if (GameScene.Hero != null && GameScene.User.HeroState == HeroState.Spawned)
                                                    HeroBeltDialog.Grid[1].UseItem();
                                                break;
                                            case KeybindOptions.Bar3Skill1: UseHeroSpell(1); break;
                                            case KeybindOptions.Bar3Skill2: UseHeroSpell(2); break;
                                            case KeybindOptions.Bar3Skill3: UseHeroSpell(3); break;
                                            case KeybindOptions.Bar3Skill4: UseHeroSpell(4); break;
                                            case KeybindOptions.Bar3Skill5: UseHeroSpell(5); break;
                                            case KeybindOptions.Bar3Skill6: UseHeroSpell(6); break;
                                            case KeybindOptions.Bar3Skill7: UseHeroSpell(7); break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void UseHeroSpell(int key)
        {
            if (Hero == null)
                return;
            if (Hero.Magics.Count == 0)
                return;

            ClientMagic magic = null;

            for (int i = 0; i < Hero.Magics.Count; i++)
            {
                if (Hero.Magics[i].Key != key) continue;
                magic = Hero.Magics[i];
                break;
            }

            if (magic == null) return;

            MirDirection dir = Functions.DirectionFromPoint(Hero.Location, User.CurrentLocation);
            Point point = User.CurrentLocation;
            uint uid = 0;
            MapObject tempTarget = null;
            CellInfo cell = MapControl.M2CellInfo[MapControl.MapLocation.X, MapControl.MapLocation.Y];
            if (cell.CellObjects != null)
            {
                for (int i = cell.CellObjects.Count - 1; i >= 0; i--)
                {
                    MapObject ob = cell.CellObjects[i];
                    if (!ob.MouseOver(CMain.MPoint)) continue;

                    tempTarget = ob;
                    MapObject.MouseObject = ob;
                    if (tempTarget != null && MapObject.MouseObject == null)
                    {
                        MapObject.MouseObject = tempTarget;
                    }
                }
            }
            switch (magic.Spell)
            {
                #region Attack Targets
                case Spell.SoulFireBall:
                case Spell.Poisoning:
                case Spell.FireBall:
                case Spell.FrostCrunch:
                case Spell.Vampirism:
                case Spell.ThunderBolt:
                case Spell.TurnUndead:
                case Spell.FlameDisruptor:
                case Spell.Revelation:
                case Spell.Healing:
                case Spell.UltimateEnhancer:
                case Spell.Purification:
                    if (tempTarget != null)
                    {
                        dir = Functions.DirectionFromPoint(Hero.Location, tempTarget.CurrentLocation);
                        point = tempTarget.CurrentLocation;
                        uid = tempTarget.ObjectID;
                    }
                    else
                    {
                        if (MapObject.MouseObject != null)
                        {
                            dir = Functions.DirectionFromPoint(Hero.Location, MapObject.MouseObject.MapLocation);
                            point = MapControl.MapLocation;
                            uid = MapObject.MouseObject.ObjectID;
                        }
                        else if (User.NextMagicObject != null)
                        {
                            dir = Functions.DirectionFromPoint(Hero.Location, User.NextMagicObject.MapLocation);
                            point = User.NextMagicObject.MapLocation;
                            uid = User.NextMagicObject.ObjectID;
                        }
                        else if (MapObject.MagicObject != null)
                        {
                            dir = Functions.DirectionFromPoint(Hero.Location, MapObject.MagicObject.MapLocation);
                            point = MapControl.MapLocation;
                            uid = MapObject.MouseObject.ObjectID;
                        }
                    }
                    
                    if (uid == 0)
                        return;
                    break;
                #endregion
                #region Location Targets
                case Spell.SoulShield:
                case Spell.BlessedArmour:
                case Spell.MassHealing:
                case Spell.IceStorm:
                case Spell.DragNet:
                case Spell.FireBang:
                case Spell.FireWall:
                case Spell.Lightning:
                case Spell.HellFire:
                    if (MapObject.MouseObject != null ||
                        User.NextMagicObject != null ||
                        MapObject.MagicObject != null)
                    {
                        //  Mouse Object first
                        if (MapObject.MouseObject != null)
                        {
                            dir = Functions.DirectionFromPoint(Hero.Location, MapObject.MouseObject.MapLocation);
                            point = MapObject.MouseObject.MapLocation;
                            uid = MapObject.MouseObject.ObjectID;
                        }
                        //  Users last/current skill object
                        else if (User.NextMagicObject != null)
                        {
                            dir = Functions.DirectionFromPoint(Hero.Location, User.NextMagicObject.MapLocation);
                            point = User.NextMagicObject.MapLocation;
                            uid = User.NextMagicObject.ObjectID;
                        }
                        //  Last Target
                        else if (MapObject.MagicObject != null)
                        {
                            dir = Functions.DirectionFromPoint(Hero.Location, MapObject.MagicObject.MapLocation);
                            point = MapObject.MagicObject.MapLocation;
                            uid = MapObject.MagicObject.ObjectID;
                        }
                        if (uid == 0)
                        {
                            dir = Functions.DirectionFromPoint(Hero.Location, MapControl.MapLocation);
                            point = MapControl.MapLocation;
                        }
                    }
                    //  The Current Location of the mouse
                    else if (MapControl.MapLocation != null)
                    {
                        dir = Functions.DirectionFromPoint(Hero.Location, MapControl.MapLocation);
                        point = MapControl.MapLocation;
                    }
                    
                    break;
                #endregion
                #region Own Location Targets
                case Spell.ThunderStorm:
                case Spell.FlameField:
                    {
                        dir = Functions.DirectionFromPoint(Hero.Location, MapControl.MapLocation);
                        point = Hero.Location;
                    }
                    break;
                    #endregion
            }
            Network.Enqueue(new C.HeroUseMagic { Spell = magic.Spell, Direction = dir, Location = point, TargetID = uid });
        }
        ushort nextShadowStepCost = 0;
        public void UseSpell(int key)
        {
            if (User.RidingMount || User.Fishing) return;

            if(!User.HasClassWeapon && User.Weapon >= 0)
            {
                ChatDialog.ReceiveChat("You must be wearing a suitable weapon to perform this skill", ChatType.System);
                return;
            }

            if ((!User.FastChannel && CMain.Time < User.BlizzardStopTime) || CMain.Time < User.ReincarnationStopTime || CMain.Time < User.comboSpell.Delay) return;

            ClientMagic magic = null;

            for (int i = 0; i < User.Magics.Count; i++)
            {
                if (User.Magics[i].Key != key) continue;
                magic = User.Magics[i];
                break;
            }

            if (magic == null) return;

            switch (magic.Spell)
            {
                case Spell.CounterAttack:
                    if ((CMain.Time < magic.CastTime + magic.Delay) && magic.CastTime != 0)
                    {
                        if (CMain.Time >= OutputDelay)
                        {
                            OutputDelay = CMain.Time + 1000;
                            Scene.OutputMessage(string.Format("You cannot cast {0} for another {1} seconds.", magic.Spell.ToString(), ((magic.CastTime + magic.Delay) - CMain.Time - 1) / 1000 + 1));
                        }

                        return;
                    }
                    magic.CastTime = CMain.Time;
                    break;
            }

            int cost;
            switch (magic.Spell)
            {
                case Spell.ShadowStep:
                    cost = nextShadowStepCost;
                    if (cost > MapObject.User.MP)
                        return;
                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();
                    break;
                case Spell.ThunderClap:
                    if (Hero == null || User.HeroState != HeroState.Spawned) return;
                    if (Hero.Magics.FirstOrDefault(x => x.Spell == Spell.ThunderClap) == null) return;

                    if (ComboBarBialog.HeroTime > CMain.Time || User.comboSpell.Delay > CMain.Time) return;

                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();

                    break;
                case Spell.ThunderRolls:
                    if (User.comboSpell.Delay > CMain.Time) return;

                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();
                    
                    break;
                case Spell.DragonFlames:
                    if (Hero == null || User.HeroState != HeroState.Spawned) return;
                    if (Hero.Magics.FirstOrDefault(x => x.Spell == Spell.DragonFlames) == null) return;

                    if (ComboBarBialog.HeroTime > CMain.Time || User.comboSpell.Delay > CMain.Time) return;

                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();

                    break;
                case Spell.DragonTears:
                    if (User.comboSpell.Delay > CMain.Time) return;

                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();

                    break;
                case Spell.BrokenSoulCut:
                    if (Hero == null || User.HeroState != HeroState.Spawned) return;
                    if (Hero.Magics.FirstOrDefault(x => x.Spell == Spell.BrokenSoulCut) == null) return;

                    if (ComboBarBialog.HeroTime > CMain.Time || User.comboSpell.Delay > CMain.Time) return;

                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();

                    break;
                case Spell.BrokenSoul:
                    if (User.comboSpell.Delay > CMain.Time) return;

                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();
                    break;
                case Spell.SoulReaper:
                    if (Hero == null || User.HeroState != HeroState.Spawned) return;
                    if (Hero.Magics.FirstOrDefault(x => x.Spell == Spell.SoulReaper) == null) return;

                    if (ComboBarBialog.HeroTime > CMain.Time || User.comboSpell.Delay > CMain.Time) return;

                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();

                    break;
                case Spell.SoulStealer:

                    if (User.comboSpell.Delay > CMain.Time) return;

                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();
                    break;
                case Spell.ChopChopStar:
                    if (MapObject.MouseObject.Dead || MapObject.MouseObject.Race == ObjectType.Item || MapObject.MouseObject.Race == ObjectType.Merchant)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat("You need a target", ChatType.System);
                        return;
                    }
                        if (Hero == null || User.HeroState != HeroState.Spawned) return;
                    if (Hero.Magics.FirstOrDefault(x => x.Spell == Spell.ChopChopStar) == null) return;

                    if (ComboBarBialog.HeroTime > CMain.Time || User.comboSpell.Delay > CMain.Time) return;

                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();

                    break;
                case Spell.StarPower:
                    if (MapObject.MouseObject.Dead || MapObject.MouseObject.Race == ObjectType.Item || MapObject.MouseObject.Race == ObjectType.Merchant)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat("You need a target", ChatType.System);
                        return;
                    }
                    if (User.comboSpell.Delay > CMain.Time) return;

                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();
                    break;
                case Spell.SoulEaterSwamp:
                    if (MapObject.MouseObject.Dead || MapObject.MouseObject.Race == ObjectType.Item || MapObject.MouseObject.Race == ObjectType.Merchant)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat("You need a target", ChatType.System);
                        return;
                    }
                    if (Hero == null || User.HeroState != HeroState.Spawned) return;
                    if (Hero.Magics.FirstOrDefault(x => x.Spell == Spell.SoulEaterSwamp) == null) return;

                    if (ComboBarBialog.HeroTime > CMain.Time || User.comboSpell.Delay > CMain.Time) return;

                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();

                    break;
                case Spell.SoulEater:
                    if (MapObject.MouseObject.Dead || MapObject.MouseObject.Race == ObjectType.Item || MapObject.MouseObject.Race == ObjectType.Merchant)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat("You need a target", ChatType.System);
                        return;
                    }
                    if (User.comboSpell.Delay > CMain.Time) return;
                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();

                    break;
                case Spell.HandOfGod:
                    if (MapObject.MouseObject.Dead || MapObject.MouseObject.Race == ObjectType.Item || MapObject.MouseObject.Race == ObjectType.Merchant)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat("You need a target", ChatType.System);
                        return;
                    }
                    if (Hero == null || User.HeroState != HeroState.Spawned) return;
                    if (Hero.Magics.FirstOrDefault(x => x.Spell == Spell.HandOfGod) == null) return;

                    if (ComboBarBialog.HeroTime > CMain.Time || User.comboSpell.Delay > CMain.Time) return;

                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();

                    break;
                case Spell.GodsHand:
                    if (MapObject.MouseObject.Dead || MapObject.MouseObject.Race == ObjectType.Item || MapObject.MouseObject.Race == ObjectType.Merchant)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat("You need a target", ChatType.System);
                        return;
                    }

                    if (User.comboSpell.Delay > CMain.Time) return;

                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();

                    break;
                case Spell.LastJudgement:
                    if (Hero == null || User.HeroState != HeroState.Spawned) return;
                    if (Hero.Magics.FirstOrDefault(x => x.Spell == Spell.LastJudgement) == null) return;

                    if (ComboBarBialog.HeroTime > CMain.Time || User.comboSpell.Delay > CMain.Time) return;

                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();

                    break;
                    case Spell.Judgement:
                    if (User.comboSpell.Delay > CMain.Time) return;

                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();

                    break;
                case Spell.Fencing:
                case Spell.FatalSword:
                case Spell.MPEater:
                case Spell.Hemorrhage:
                case Spell.SpiritSword:
                case Spell.Slaying:
                
                case Spell.Focus:
                case Spell.Meditation:
                    return;
                case Spell.Thrusting:
                    if (CMain.Time < ToggleTime) return;
                    Thrusting = !Thrusting;
                    ChatDialog.ReceiveChat(Thrusting ? "Use Thrusting." : "Do not use Thrusting.", ChatType.Hint);
                    ToggleTime = CMain.Time + 1000;
                    Network.Enqueue(new C.SpellToggle { Spell = magic.Spell, CanUse = Thrusting });
                    break;
                case Spell.HalfMoon:
                    if (CMain.Time < ToggleTime) return;
                    HalfMoon = !HalfMoon;
                    ChatDialog.ReceiveChat(HalfMoon ? "Use Half Moon." : "Do not use Half Moon.", ChatType.Hint);
                    ToggleTime = CMain.Time + 1000;
                    Network.Enqueue(new C.SpellToggle { Spell = magic.Spell, CanUse = HalfMoon });
                    break;
                case Spell.CrescentSlash:
                    if (CMain.Time < ToggleTime) return;
                    CresSlash = !CresSlash;
                    ChatDialog.ReceiveChat(CresSlash ? "Use CrescentSlash." : "Do not use CrescentSlash.", ChatType.Hint);
                    Network.Enqueue(new C.SpellToggle { Spell = magic.Spell, CanUse = CresSlash });
                    User.Effects.Add(new Effect(Libraries.Magic2, 2620 + (int)User.Direction * 20, 4, 400, User));
                    break;
                case Spell.CrossHalfMoon:
                    if (CMain.Time < ToggleTime) return;
                    CrossHalfMoon = !CrossHalfMoon;
                    ChatDialog.ReceiveChat(CrossHalfMoon ? "Use Cross Half Moon." : "Do not use Cross Half Moon.", ChatType.Hint);
                    ToggleTime = CMain.Time + 1000;
                    Network.Enqueue(new C.SpellToggle { Spell = magic.Spell, CanUse = CrossHalfMoon });
                    break;
                case Spell.DoubleSlash:
                    if (CMain.Time < ToggleTime) return;
                    DoubleSlash = !DoubleSlash;
                    ChatDialog.ReceiveChat(DoubleSlash ? "Use Double Slash." : "Do not use Double Slash.", ChatType.Hint);
                    ToggleTime = CMain.Time + 1000;
                    Network.Enqueue(new C.SpellToggle { Spell = magic.Spell, CanUse = DoubleSlash });
                    break;
                case Spell.TwinDrakeBlade:
                    if (CMain.Time < ToggleTime) return;
                    ToggleTime = CMain.Time + 500;

                    cost = magic.Level * magic.LevelCost + magic.BaseCost;
                    if (cost > MapObject.User.MP)
                    {
                        Scene.OutputMessage("Not Enough Mana to cast.");
                        return;
                    }
                    TwinDrakeBlade = true;
                    Network.Enqueue(new C.SpellToggle { Spell = magic.Spell, CanUse = true });
                    User.Effects.Add(new Effect(Libraries.Magic2, 210, 6, 500, User));
                    break;
                case Spell.SliceNDice:
                    if (CMain.Time < ToggleTime)
                        return;
                    ToggleTime = CMain.Time + 500;
                    cost = magic.Level * magic.LevelCost + magic.BaseCost;
                    if (cost > MapObject.User.MP)
                    {
                        Scene.OutputMessage("Not Enough Mana to cast.");
                        return;
                    }
                    Network.Enqueue(new C.SpellToggle { Spell = magic.Spell, CanUse = true });
                    break;
                case Spell.FlamingSword:
                    if (CMain.Time < ToggleTime) return;
                    ToggleTime = CMain.Time + 500;

                    cost = magic.Level * magic.LevelCost + magic.BaseCost;
                    if (cost > MapObject.User.MP)
                    {
                        Scene.OutputMessage("Not Enough Mana to cast.");
                        return;
                    }
                    Network.Enqueue(new C.SpellToggle { Spell = magic.Spell, CanUse = true });
                    break;
                case Spell.BlazingSword:
                    if (CMain.Time < ToggleTime) return;
                    ToggleTime = CMain.Time + 500;

                    cost = magic.Level * magic.LevelCost + magic.BaseCost;
                    if (cost > MapObject.User.MP)
                    {
                        Scene.OutputMessage("Not Enough Mana to cast.");
                        return;
                    }
                    Network.Enqueue(new C.SpellToggle { Spell = magic.Spell, CanUse = true });
                    break;
                case Spell.CounterAttack:
                    cost = magic.Level * magic.LevelCost + magic.BaseCost;
                    if (cost > MapObject.User.MP)
                    {
                        Scene.OutputMessage("Not Enough Mana to cast.");
                        return;
                    }

                    SoundManager.PlaySound(20000 + (ushort)Spell.CounterAttack * 10);
                    Network.Enqueue(new C.SpellToggle { Spell = magic.Spell, CanUse = true });
                    break;
                case Spell.MentalState:
                    if (CMain.Time < ToggleTime) return;
                    ToggleTime = CMain.Time + 500;
                    Network.Enqueue(new C.SpellToggle { Spell = magic.Spell, CanUse = true });
                    break;
                default:
                    User.NextMagic = magic;
                    User.NextMagicLocation = MapControl.MapLocation;
                    User.NextMagicObject = MapObject.MouseObject;
                    User.NextMagicDirection = MapControl.MouseDirection();
                    break;
            }

        }

        public void BeforeEnterGame()    
        {
            MainDialog.SetBaseImage();
        }

        public void QuitGame()
        {
            if (CMain.Time >= LogTime)
            {
                //If Last Combat < 10 CANCEL
                MirMessageBox messageBox = new MirMessageBox("Do you want to quit Legend of Mir?", MirMessageBoxButtons.YesNo);
                messageBox.YesButton.Click += (o, e) =>
                {
                    Program.CanClose = false;
                    Program.Form.Close();                    
                };
                messageBox.Show();
            }
            else
            {
                ChatDialog.ReceiveChat("Cannot leave game for " + (LogTime - CMain.Time) / 1000 + " seconds.", ChatType.System);
            }
        }

        public void LogOut()
        {
            if (CMain.Time >= LogTime)
            {
                //If Last Combat < 10 CANCEL
                MirMessageBox messageBox = new MirMessageBox("Do you want to log out of Legend of Mir?", MirMessageBoxButtons.YesNo);
                messageBox.YesButton.Click += (o, e) =>
                {
                    Network.Enqueue(new C.LogOut());
                    Enabled = false;
                };
                messageBox.Show();
            }
            else
            {
                ChatDialog.ReceiveChat("Cannot leave game for " + (LogTime - CMain.Time) / 1000 + " seconds.", ChatType.System);
            }
        }

        protected internal override void DrawControl()
        {
            if (MapControl != null && !MapControl.IsDisposed)
                MapControl.DrawControl();

            base.DrawControl();


            if (PickedUpGold || (SelectedCell != null && SelectedCell.Item != null))
            {
                int image = PickedUpGold ? 116 : SelectedCell.Item.Image;
                Size imgSize = Libraries.Items.GetTrueSize(image);
                Point p = CMain.MPoint.Add(-imgSize.Width / 2, -imgSize.Height / 2);

                if (p.X + imgSize.Width >= Settings.ScreenWidth)
                    p.X = Settings.ScreenWidth - imgSize.Width;

                if (p.Y + imgSize.Height >= Settings.ScreenHeight)
                    p.Y = Settings.ScreenHeight - imgSize.Height;

                Libraries.Items.Draw(image, p.X, p.Y);
            }

            for (int i = 0; i < OutputLines.Length; i++)
                OutputLines[i].Draw();
        }

        public string ToReadableString(TimeSpan span)
        {
            string formatted = string.Format("{0}\n{1}\n{2}\n{3}",
                span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds == 1 ? String.Empty : "s") : string.Empty);

            if (formatted.EndsWith(", "))
                formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted))
                formatted = "0 seconds";

            return formatted;
        }
        public long NextCheatProcessCheck;
        public override void Process()
        {
            if (MapControl == null || User == null)
                return;
            if (CMain.Time >= MoveTime)
            {
                MoveTime += 100; //Move Speed
                OrigMoveTime = Math.Min(long.MaxValue, MoveTime * 30 / 100);
                CanMove = true;
                MapControl.AnimationCount++;
                MapControl.TextureValid = false;

                if (LightEffectFrame < 10)
                    LightEffectFrame++;
                else
                    LightEffectFrame = 0;

                if (LightEffectRotation < float.MaxValue)
                    LightEffectRotation++;
                else
                    LightEffectRotation = 0;
            }
            else
                CanMove = false;

            if (CraftingDialog != null &&
                CraftingDialog.Visible)
            {
                if (CraftingDialog.ProgressLabel != null &&
                    CraftingDialog.ProgressLabel.Visible)
                {
                    //craftProgress
                    DateTime timeNow = DateTime.UtcNow;
                    DateTime crafttime = new DateTime(GameScene.Scene.CraftingDialog.craftProgress);
                    TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                    timeNow = TimeZoneInfo.ConvertTimeFromUtc(timeNow, zone);
                    crafttime = TimeZoneInfo.ConvertTimeFromUtc(crafttime, zone);
                    TimeSpan span = new TimeSpan();
                    span = crafttime - timeNow;
                    CraftingDialog.ProgressLabel.Text = string.Format("Your request is in progress.\nWill be ready in : \n{0}", ToReadableString(span));
                }
            }
            else if (CraftingDialog != null &&
                !CraftingDialog.Visible &&
                CraftingDialog.ProgressLabel != null &&
                CraftingDialog.ProgressLabel.Text.Contains("Your request is in progree"))
            {
                CraftingDialog.ProgressLabel.Text = "";
            }
            if (CMain.Time >= NextPing)
            {
                NextPing = CMain.Time + 1000;
                Network.Enqueue(new C.KeepAlive() { Time = CMain.Time });
            }

            MirItemCell cell = MouseControl as MirItemCell;

            if (cell != null && HoverItem != cell.Item)
            {
                DisposeItemLabel();
                HoverItem = null;
                CreateItemLabel(cell.Item);
            }

            if (BossDialog != null)
            {
                BossDialog.BringToFront();

                int x = CMain.MPoint.X + 15, y = CMain.MPoint.Y;
                if (x + BossDialog.Size.Width > Settings.ScreenWidth)
                    x = Settings.ScreenWidth - BossDialog.Size.Width;

                if (y + BossDialog.Size.Height > Settings.ScreenHeight)
                    y = Settings.ScreenHeight - BossDialog.Size.Height;
                BossDialog.Location = new Point(x, y);
            }

            if (ItemLabel != null && !ItemLabel.IsDisposed)
            {
                ItemLabel.BringToFront();

                int x = CMain.MPoint.X + 15, y = CMain.MPoint.Y;
                if (x + ItemLabel.Size.Width > Settings.ScreenWidth)
                    x = Settings.ScreenWidth - ItemLabel.Size.Width;

                if (y + ItemLabel.Size.Height > Settings.ScreenHeight)
                    y = Settings.ScreenHeight - ItemLabel.Size.Height;
                ItemLabel.Location = new Point(x, y);
            }

            if (MonsterLabel != null && !MonsterLabel.IsDisposed)
            {
                MonsterLabel.BringToFront();

                int x = CMain.MPoint.X - MonsterLabel.Size.Width / 2, y = CMain.MPoint.Y + 20;
                if (x + MonsterLabel.Size.Width > Settings.ScreenWidth)
                    x = Settings.ScreenWidth - MonsterLabel.Size.Width;

                if (y + MonsterLabel.Size.Height > Settings.ScreenHeight)
                    y = Settings.ScreenHeight - MonsterLabel.Size.Height;
                MonsterLabel.Location = new Point(x, y);

                MonsterLabel.Visible = true;
            }

            if (MailLabel != null && !MailLabel.IsDisposed)
            {
                MailLabel.BringToFront();

                int x = CMain.MPoint.X + 15, y = CMain.MPoint.Y;
                if (x + MailLabel.Size.Width > Settings.ScreenWidth)
                    x = Settings.ScreenWidth - MailLabel.Size.Width;

                if (y + MailLabel.Size.Height > Settings.ScreenHeight)
                    y = Settings.ScreenHeight - MailLabel.Size.Height;
                MailLabel.Location = new Point(x, y);
            }

            if (MemoLabel != null && !MemoLabel.IsDisposed)
            {
                MemoLabel.BringToFront();

                int x = CMain.MPoint.X + 15, y = CMain.MPoint.Y;
                if (x + MemoLabel.Size.Width > Settings.ScreenWidth)
                    x = Settings.ScreenWidth - MemoLabel.Size.Width;

                if (y + MemoLabel.Size.Height > Settings.ScreenHeight)
                    y = Settings.ScreenHeight - MemoLabel.Size.Height;
                MemoLabel.Location = new Point(x, y);
            }

            if (GuildBuffLabel != null && !GuildBuffLabel.IsDisposed)
            {
                GuildBuffLabel.BringToFront();

                int x = CMain.MPoint.X + 15, y = CMain.MPoint.Y;
                if (x + GuildBuffLabel.Size.Width > Settings.ScreenWidth)
                    x = Settings.ScreenWidth - GuildBuffLabel.Size.Width;

                if (y + GuildBuffLabel.Size.Height > Settings.ScreenHeight)
                    y = Settings.ScreenHeight - GuildBuffLabel.Size.Height;
                GuildBuffLabel.Location = new Point(x, y);
            }

            if (!User.Dead) ShowReviveMessage = false;

            if (ShowReviveMessage && CMain.Time > User.DeadTime && User.CurrentAction == MirAction.Dead)
            {
                ShowReviveMessage = false;
                MirMessageBox messageBox = new MirMessageBox("You have died, Do you want to revive in town?", MirMessageBoxButtons.YesNo, false);

                messageBox.YesButton.Click += (o, e) =>
                {
                    if (User.Dead) Network.Enqueue(new C.TownRevive());
                };

                messageBox.AfterDraw += (o, e) =>
                {
                    if (!User.Dead) messageBox.Dispose();
                };

                messageBox.Show();
            }


            ComboBarBialog.Process();
            UpdateBuffs();
            MapControl.Process();
            MainDialog.Process();
            InventoryDialog.Process();
            GameShopDialog.Process();
            MiniMapDialog.Process();
            GroupInfoDialog.UpdateListGroup();
            
            foreach (SkillBarDialog Bar in Scene.SkillBarDialogs)
                Bar.Process();
            SkillCoolDownDialog.Process();
            DialogProcess();

            ProcessOuput();

            if (CMain.Time > UpdateQuestDisplay)
            {
                UpdateQuestDisplay = CMain.Time + 1000;

                foreach (var q in User.CurrentQuests)
                    if (q.EndTime > 0) q.EndTime--;

                Scene.QuestTrackingDialog.DisplayQuests();

            }
            if (CMain.Time > NextCheatProcessCheck)
            {
                bool cheatFound = false;
                byte result = 0;
                NextCheatProcessCheck = CMain.Time + 5000;
                #region Process Scan
                List<System.Diagnostics.Process> processes = System.Diagnostics.Process.GetProcesses().ToList();
                if (processes != null &&
                    processes.Count >= 0)
                {
                    for (int i = 0; i < processes.Count; i++)
                    {
                        if (processes[i].ProcessName.ToLower().Contains("cheat") &&
                            processes[i].ProcessName.ToLower().Contains("engine"))
                        {
                            result = 1;
                            cheatFound = true;
                        }
                    }
                }
                #endregion
                #region Currency checks
                if (Math.Min(uint.MaxValue, Gold * 30 / 100) != RealGold)
                {
                    result = 2;
                    cheatFound = true;
                }
                if (Math.Min(uint.MaxValue, Credit * 30 / 100) != RealCredit)
                {
                    result = 3;
                    cheatFound = true;
                }
                if (Math.Min(uint.MaxValue, GuildDialog.Gold * 30 / 100) != RealguildGold)
                {
                    result = 4;
                    cheatFound = true;
                }
                /*
                if (Math.Min(long.MaxValue, AttackTime * 30 / 100) != OrigAttackTime)
                {
                    result = 5;
                    cheatFound = true;
                }
                if (Math.Min(long.MaxValue, MoveTime * 30 / 100) != OrigMoveTime)
                {
                    result = 6;
                    cheatFound = true;
                }
                if (Math.Min(long.MaxValue, LastRunTime * 30 / 100) != OrigLastRun)
                {
                    result = 7;
                    cheatFound = true;
                }
                if (Math.Min(long.MaxValue, NextRunTime * 30 / 100) != OrigNextRun)
                {
                    result = 8;
                    cheatFound = true;
                }
                */
                if (Math.Min(int.MaxValue, User.ASpeed * 30 / 100) != User.OrigAttackSpeed)
                {
                    result = 12;
                    cheatFound = true;
                }
                if (Math.Min(ushort.MaxValue, User.Level * 30 / 100) != User.OrigLevel)
                {
                    result = 13;
                    cheatFound = true;
                }
                #endregion
                if (cheatFound)
                    Network.Enqueue(new C.ReportCheat { Result = result });
            }
            HeroHeaderDialog.Process();
        }
        
        public void DialogProcess()
        {
            if(Settings.SkillBar)
            {
                foreach (SkillBarDialog Bar in Scene.SkillBarDialogs)
                    Bar.Show();
            }
            else
            {
                foreach (SkillBarDialog Bar in Scene.SkillBarDialogs)
                    Bar.Hide();
            }

            for (int i = 0; i < Scene.SkillBarDialogs.Count; i++)
            {
                if (i * 2 > Settings.SkillbarLocation.Length) break;
                if ((Settings.SkillbarLocation[i, 0] > Settings.Resolution - 100) || (Settings.SkillbarLocation[i, 1] > 700)) continue;//in theory you'd want the y coord to be validated based on resolution, but since client only allows for wider screens and not higher :(
                Scene.SkillBarDialogs[i].Location = new Point(Settings.SkillbarLocation[i, 0], Settings.SkillbarLocation[i, 1]);
            }

            if (Settings.DuraView)
                CharacterDuraPanel.Show();
            else
                CharacterDuraPanel.Hide();
        }

        public override void ProcessPacket(Packet p)
        {
            switch (p.Index)
            {
                case (short)ServerPacketIds.KeepAlive:
                    KeepAlive((S.KeepAlive)p);
                    break;
                case (short)ServerPacketIds.MapInformation: //MapInfo
                    MapInformation((S.MapInformation)p);
                    break;
                case (short)ServerPacketIds.UserInformation:
                    UserInformation((S.UserInformation)p);
                    break;
                case (short)ServerPacketIds.HeroInformation:
                    HeroInformation((S.HeroInformation)p);
                    break;

                case (short)ServerPacketIds.HeroStats:
                    HeroStats((S.HeroStats)p);
                    break;
                case (short)ServerPacketIds.UserLocation:
                    UserLocation((S.UserLocation)p);
                    break;
                case (short)ServerPacketIds.ObjectPlayer:
                    ObjectPlayer((S.ObjectPlayer)p);
                    break;
                case (short)ServerPacketIds.ObjectRemove:
                    ObjectRemove((S.ObjectRemove)p);
                    break;
                case (short)ServerPacketIds.ObjectTurn:
                    ObjectTurn((S.ObjectTurn)p);
                    break;
                case (short)ServerPacketIds.ObjectWalk:
                    ObjectWalk((S.ObjectWalk)p);
                    break;
                case (short)ServerPacketIds.ObjectRun:
                    ObjectRun((S.ObjectRun)p);
                    break;
                case (short)ServerPacketIds.Chat:
                    ReceiveChat((S.Chat)p);
                    break;
                case (short)ServerPacketIds.ObjectChat:
                    ObjectChat((S.ObjectChat)p);
                    break;
                case (short)ServerPacketIds.MoveItem:
                    MoveItem((S.MoveItem)p);
                    break;
                case (short)ServerPacketIds.EquipItem:
                    EquipItem((S.EquipItem)p);
                    break;
                case (short)ServerPacketIds.HeroEquipItem:
                    HeroEquipItem((S.HeroEquipItem)p);
                    break;
                case (short)ServerPacketIds.MergeItem:
                    MergeItem((S.MergeItem)p);
                    break;
                case (short)ServerPacketIds.RemoveItem:
                    RemoveItem((S.RemoveItem)p);
                    break;
                case (short)ServerPacketIds.HeroRemoveItem:
                    HeroRemoveItem((S.HeroRemoveItem)p);
                    break;
                case (short)ServerPacketIds.RemoveSlotItem:
                    RemoveSlotItem((S.RemoveSlotItem)p);
                    break;
                case (short)ServerPacketIds.TakeBackItem:
                    TakeBackItem((S.TakeBackItem)p);
                    break;
                case (short)ServerPacketIds.HeroInventoryToInventory:
                    HeroInventoryToInventory((S.HeroInventoryToInventory)p);
                    break;
                case (short)ServerPacketIds.StoreItem:
                    StoreItem((S.StoreItem)p);
                    break;
                case (short)ServerPacketIds.InventoryToHeroInventory:
                    InventoryToHeroInventory((S.InventoryToHeroInventory)p);
                    break;
                case (short)ServerPacketIds.DepositRefineItem:
                    DepositRefineItem((S.DepositRefineItem)p);
                    break;
                case (short)ServerPacketIds.RetrieveRefineItem:
                    RetrieveRefineItem((S.RetrieveRefineItem)p);
                    break;
                case (short)ServerPacketIds.RefineCancel:
                    RefineCancel((S.RefineCancel)p);
                    break;
                case (short)ServerPacketIds.RefineItem:
                    RefineItem((S.RefineItem)p);
                    break;
                case (short)ServerPacketIds.DepositTradeItem:
                    DepositTradeItem((S.DepositTradeItem)p);
                    break;
                case (short)ServerPacketIds.RetrieveTradeItem:
                    RetrieveTradeItem((S.RetrieveTradeItem)p);
                    break;
                case (short)ServerPacketIds.SplitItem:
                    SplitItem((S.SplitItem)p);
                    break;
                case (short)ServerPacketIds.SplitItem1:
                    SplitItem1((S.SplitItem1)p);
                    break;
                case (short)ServerPacketIds.UseItem:
                    UseItem((S.UseItem)p);
                    break;
                case (short)ServerPacketIds.HeroUseItem:
                    HeroUseItem((S.HeroUseItem)p);
                    break;
                case (short)ServerPacketIds.DropItem:
                    DropItem((S.DropItem)p);
                    break;
                case (short)ServerPacketIds.PlayerUpdate:
                    PlayerUpdate((S.PlayerUpdate)p);
                    break;
                case (short)ServerPacketIds.PlayerInspect:
                    PlayerInspect((S.PlayerInspect)p);
                    break;
                case (short)ServerPacketIds.LogOutSuccess:
                    LogOutSuccess((S.LogOutSuccess)p);
                    break;
                case (short)ServerPacketIds.LogOutFailed:
                    LogOutFailed((S.LogOutFailed)p);
                    break;
                case (short)ServerPacketIds.TimeOfDay:
                    TimeOfDay((S.TimeOfDay)p);
                    break;
                case (short)ServerPacketIds.ChangeAMode:
                    ChangeAMode((S.ChangeAMode)p);
                    break;
                case (short)ServerPacketIds.ChangePMode:
                    ChangePMode((S.ChangePMode)p);
                    break;
                case (short)ServerPacketIds.ChangeHMode:
                    ChangeHMode((S.ChangeHMode)p);
                    break;
                case (short)ServerPacketIds.ObjectItem:
                    ObjectItem((S.ObjectItem)p);
                    break;
                case (short)ServerPacketIds.ObjectGold:
                    ObjectGold((S.ObjectGold)p);
                    break;
                case (short)ServerPacketIds.GainedItem:
                    GainedItem((S.GainedItem)p);
                    break;
                case (short)ServerPacketIds.SetHumUp:
                    SetHumUp((S.SetHumUp)p);
                    break;
                case (short)ServerPacketIds.SetAutoPot:
                    SetAutoPot((S.SetAutoPot)p);
                    break;
                case (short)ServerPacketIds.SetHero:
                    SetHero((S.SetHero)p);
                    break;
                case (short)ServerPacketIds.SetHeroSpawned:
                    SetHeroSpawned((S.SetHeroSpawned)p);
                    break;
                case (short)ServerPacketIds.SetHeroLocked:
                    SetHeroLocked((S.SetHeroLocked)p);
                    break;
                case (short)ServerPacketIds.GainedGold:
                    GainedGold((S.GainedGold)p);
                    break;
                case (short)ServerPacketIds.LoseGold:
                    LoseGold((S.LoseGold)p);
                    break;
                case (short)ServerPacketIds.GainedCredit:
                    GainedCredit((S.GainedCredit)p);
                    break;
                case (short)ServerPacketIds.LoseCredit:
                    LoseCredit((S.LoseCredit)p);
                    break;
                case (short)ServerPacketIds.ObjectMonster:
                    ObjectMonster((S.ObjectMonster)p);
                    break;
                case (short)ServerPacketIds.ObjectAttack:
                    ObjectAttack((S.ObjectAttack)p);
                    break;
                case (short)ServerPacketIds.Struck:
                    Struck((S.Struck)p);
                    break;
                case (short)ServerPacketIds.DamageIndicator:
                    DamageIndicator((S.DamageIndicator)p);
                    break;
                case (short)ServerPacketIds.ObjectStruck:
                    ObjectStruck((S.ObjectStruck)p);
                    break;
                case (short)ServerPacketIds.DuraChanged:
                    DuraChanged((S.DuraChanged)p);
                    break;
                case (short)ServerPacketIds.HealthChanged:
                    HealthChanged((S.HealthChanged)p);
                    break;
                case (short)ServerPacketIds.HeroHealthChanged:
                    HeroHealthChanged((S.HeroHealthChanged)p);
                    break;
                case (short)ServerPacketIds.HeroStatsChanged:
                    HeroStatsChanged((S.HeroStatsChanged)p);
                    break;
                case (short)ServerPacketIds.DeleteItem:
                    DeleteItem((S.DeleteItem)p);
                    break;
                case (short)ServerPacketIds.Death:
                    Death((S.Death)p);
                    break;
                case (short)ServerPacketIds.ObjectDied:
                    ObjectDied((S.ObjectDied)p);
                    break;
                case (short)ServerPacketIds.ColourChanged:
                    ColourChanged((S.ColourChanged)p);
                    break;
                case (short)ServerPacketIds.ObjectColourChanged:
                    ObjectColourChanged((S.ObjectColourChanged)p);
                    break;
                case (short)ServerPacketIds.ObjectGuildNameChanged:
                    ObjectGuildNameChanged((S.ObjectGuildNameChanged)p);
                    break;
                case (short)ServerPacketIds.GainExperience:
                    GainExperience((S.GainExperience)p);
                    break;
                case (short)ServerPacketIds.HeroGainExperience:
                    HeroGainExperience((S.HeroGainExperience)p);
                    break;
                case (short)ServerPacketIds.GainShieldExperience:
                    GainShieldEXP((S.GainShieldEXP)p);
                    break;
                case (short)ServerPacketIds.ItemGainLevel:
                    ItemGainLevel((S.ItemGainLevel)p);
                    break;
                case (short)ServerPacketIds.HeroLevelChanged:
                    HeroLevelChanged((S.HeroLevelChanged)p);
                    break;
                case (short)ServerPacketIds.LevelChanged:
                    LevelChanged((S.LevelChanged)p);
                    break;
                case (short)ServerPacketIds.ObjectLeveled:
                    ObjectLeveled((S.ObjectLeveled)p);
                    break;
                case (short)ServerPacketIds.ObjectHarvest:
                    ObjectHarvest((S.ObjectHarvest)p);
                    break;
                case (short)ServerPacketIds.ObjectHarvested:
                    ObjectHarvested((S.ObjectHarvested)p);
                    break;
                case (short)ServerPacketIds.ObjectNpc:
                    ObjectNPC((S.ObjectNPC)p);
                    break;
                case (short)ServerPacketIds.NPCResponse:
                    NPCResponse((S.NPCResponse)p);
                    break;
                case (short)ServerPacketIds.ObjectHide:
                    ObjectHide((S.ObjectHide)p);
                    break;
                case (short)ServerPacketIds.ObjectShow:
                    ObjectShow((S.ObjectShow)p);
                    break;
                case (short)ServerPacketIds.Poisoned:
                    Poisoned((S.Poisoned)p);
                    break;
                case (short)ServerPacketIds.ObjectPoisoned:
                    ObjectPoisoned((S.ObjectPoisoned)p);
                    break;
                case (short)ServerPacketIds.MapChanged:
                    MapChanged((S.MapChanged)p);
                    break;
                case (short)ServerPacketIds.ObjectTeleportOut:
                    ObjectTeleportOut((S.ObjectTeleportOut)p);
                    break;
                case (short)ServerPacketIds.ObjectTeleportIn:
                    ObjectTeleportIn((S.ObjectTeleportIn)p);
                    break;
                case (short)ServerPacketIds.TeleportIn:
                    TeleportIn();
                    break;
                case (short)ServerPacketIds.NPCGoods:
                    NPCGoods((S.NPCGoods)p);
                    break;
                case (short)ServerPacketIds.NPCSell:
                    NPCSell();
                    break;
                case (short)ServerPacketIds.NPCRepair:
                    NPCRepair((S.NPCRepair)p);
                    break;
                case (short)ServerPacketIds.NPCSRepair:
                    NPCSRepair((S.NPCSRepair)p);
                    break;
                case (short)ServerPacketIds.NPCRefine:
                    NPCRefine((S.NPCRefine)p);
                    break;
                case (short)ServerPacketIds.NPCCheckRefine:
                    NPCCheckRefine((S.NPCCheckRefine)p);
                    break;
                case (short)ServerPacketIds.NPCCollectRefine:
                    NPCCollectRefine((S.NPCCollectRefine)p);
                    break;
                case (short)ServerPacketIds.NPCReplaceWedRing:
                    NPCReplaceWedRing((S.NPCReplaceWedRing)p);
                    break;
                case (short)ServerPacketIds.NPCStorage:
                    NPCStorage();
                    break;
                case (short)ServerPacketIds.NPCRequestInput:
                    NPCRequestInput((S.NPCRequestInput)p);
                    break;
                case (short)ServerPacketIds.SellItem:
                    SellItem((S.SellItem)p);
                    break;
                case (short)ServerPacketIds.RepairItem:
                    RepairItem((S.RepairItem)p);
                    break;
                case (short)ServerPacketIds.ItemRepaired:
                    ItemRepaired((S.ItemRepaired)p);
                    break;
                case (short)ServerPacketIds.NewMagic:
                    NewMagic((S.NewMagic)p);
                    break;
                case (short)ServerPacketIds.RefreshMagic:
                    RefreshMagic((S.RefreshMagic)p);
                    break;
                case (short)ServerPacketIds.HeroNewMagic:
                    HeroNewMagic((S.HeroNewMagic)p);
                    break;
                case (short)ServerPacketIds.MagicLeveled:
                    MagicLeveled((S.MagicLeveled)p);
                    break;
                case (short)ServerPacketIds.HeroMagicLeveled:
                    HeroMagicLeveled((S.HeroMagicLeveled)p);
                    break;
                case (short)ServerPacketIds.Magic:
                    Magic((S.Magic)p);
                    break;
                case (short)ServerPacketIds.MagicDelay:
                    MagicDelay((S.MagicDelay)p);
                    break;
                case (short)ServerPacketIds.MagicCast:
                    MagicCast((S.MagicCast)p);
                    break;
                case (short)ServerPacketIds.ObjectMagic:
                    ObjectMagic((S.ObjectMagic)p);
                    break;
                case (short)ServerPacketIds.ObjectEffect:
                    ObjectEffect((S.ObjectEffect)p);
                    break;
                case (short)ServerPacketIds.RangeAttack:
                    RangeAttack((S.RangeAttack)p);
                    break;
                case (short)ServerPacketIds.Pushed:
                    Pushed((S.Pushed)p);
                    break;
                case (short)ServerPacketIds.ObjectPushed:
                    ObjectPushed((S.ObjectPushed)p);
                    break;
                case (short)ServerPacketIds.ObjectName:
                    ObjectName((S.ObjectName)p);
                    break;
                case (short)ServerPacketIds.UserStorage:
                    UserStorage((S.UserStorage)p);
                    break;
                case (short)ServerPacketIds.SwitchGroup:
                    SwitchGroup((S.SwitchGroup)p);
                    break;
                case (short)ServerPacketIds.DeleteGroup:
                    DeleteGroup();
                    break;
                case (short)ServerPacketIds.DeleteMember:
                    DeleteMember((S.DeleteMember)p);
                    break;
                case (short)ServerPacketIds.GroupInvite:
                    GroupInvite((S.GroupInvite)p);
                    break;
                case (short)ServerPacketIds.AddMember:
                    AddMember((S.AddMember)p);
                    break;
                case (short)ServerPacketIds.Revived:
                    Revived();
                    break;
                case (short)ServerPacketIds.ObjectRevived:
                    ObjectRevived((S.ObjectRevived)p);
                    break;
                case (short)ServerPacketIds.SpellToggle:
                    SpellToggle((S.SpellToggle)p);
                    break;
                case (short)ServerPacketIds.ObjectHealth:
                    ObjectHealth((S.ObjectHealth)p);
                    break;
                case (short)ServerPacketIds.MapEffect:
                    MapEffect((S.MapEffect)p);
                    break;
                case (short)ServerPacketIds.ObjectRangeAttack:
                    ObjectRangeAttack((S.ObjectRangeAttack)p);
                    break;
                case (short)ServerPacketIds.AddBuff:
                    AddBuff((S.AddBuff)p);
                    break;
                case (short)ServerPacketIds.RemoveBuff:
                    RemoveBuff((S.RemoveBuff)p);
                    break;
                case (short)ServerPacketIds.ObjectHidden:
                    ObjectHidden((S.ObjectHidden)p);
                    break;
                case (short)ServerPacketIds.RefreshItem:
                    RefreshItem((S.RefreshItem)p);
                    break;
                case (short)ServerPacketIds.ObjectSpell:
                    ObjectSpell((S.ObjectSpell)p);
                    break;
                case (short)ServerPacketIds.UserDash:
                    UserDash((S.UserDash)p);
                    break;
                case (short)ServerPacketIds.ObjectDash:
                    ObjectDash((S.ObjectDash)p);
                    break;
                case (short)ServerPacketIds.UserDashFail:
                    UserDashFail((S.UserDashFail)p);
                    break;
                case (short)ServerPacketIds.ObjectDashFail:
                    ObjectDashFail((S.ObjectDashFail)p);
                    break;
                case (short)ServerPacketIds.NPCConsign:
                    NPCConsign();
                    break;
                case (short)ServerPacketIds.NPCMarket:
                    NPCMarket((S.NPCMarket)p);
                    break;
                case (short)ServerPacketIds.NPCMarketPage:
                    NPCMarketPage((S.NPCMarketPage)p);
                    break;
                case (short)ServerPacketIds.GuildTerritoryPage:
                    GuildTerritoryPage((S.GuildTerritoryPage)p);
                    break;
                case (short)ServerPacketIds.ConsignItem:
                    ConsignItem((S.ConsignItem)p);
                    break;
                case (short)ServerPacketIds.MarketFail:
                    MarketFail((S.MarketFail)p);
                    break;
                case (short)ServerPacketIds.MarketSuccess:
                    MarketSuccess((S.MarketSuccess)p);
                    break;
                case (short)ServerPacketIds.ObjectSitDown:
                    ObjectSitDown((S.ObjectSitDown)p);
                    break;
                case (short)ServerPacketIds.InTrapRock:
                    S.InTrapRock packetdata = (S.InTrapRock)p;
                    User.InTrapRock = packetdata.Trapped;
                    break;
                case (short)ServerPacketIds.RemoveMagic:
                    RemoveMagic((S.RemoveMagic)p);
                    break;
                case (short)ServerPacketIds.BaseStatsInfo:
                    BaseStatsInfo((S.BaseStatsInfo)p);
                    break;
                case (short)ServerPacketIds.UserName:
                    UserName((S.UserName)p);
                    break;
                case (short)ServerPacketIds.ChatItemStats:
                    ChatItemStats((S.ChatItemStats)p);
                    break;
                case (short)ServerPacketIds.GuildInvite:
                    GuildInvite((S.GuildInvite)p);
                    break;
                case (short)ServerPacketIds.GuildMemberChange:
                    GuildMemberChange((S.GuildMemberChange)p);
                    break;
                case (short)ServerPacketIds.GuildNoticeChange:
                    GuildNoticeChange((S.GuildNoticeChange)p);
                    break;
                case (short)ServerPacketIds.GuildStatus:
                    GuildStatus((S.GuildStatus)p);
                    break;
                case (short)ServerPacketIds.GuildExpGain:
                    GuildExpGain((S.GuildExpGain)p);
                    break;
                case (short)ServerPacketIds.GuildNameRequest:
                    GuildNameRequest((S.GuildNameRequest)p);
                    break;
                case (short)ServerPacketIds.GuildStorageGoldChange:
                    GuildStorageGoldChange((S.GuildStorageGoldChange)p);
                    break;
                case (short)ServerPacketIds.GuildStorageItemChange:
                    GuildStorageItemChange((S.GuildStorageItemChange)p);
                    break;
                case (short)ServerPacketIds.GuildStorageList:
                    GuildStorageList((S.GuildStorageList)p);
                    break;
                case (short)ServerPacketIds.GuildRequestWar:
                    GuildRequestWar((S.GuildRequestWar)p);
                    break;
                case (short)ServerPacketIds.DefaultNPC:
                    DefaultNPC((S.DefaultNPC)p);
                    break;
                case (short)ServerPacketIds.NPCUpdate:
                    NPCUpdate((S.NPCUpdate)p);
                    break;
                case (short)ServerPacketIds.NPCImageUpdate:
                    NPCImageUpdate((S.NPCImageUpdate)p);
                    break;
                case (short)ServerPacketIds.MarriageRequest:
                    MarriageRequest((S.MarriageRequest)p);
                    break;
                case (short)ServerPacketIds.DivorceRequest:
                    DivorceRequest((S.DivorceRequest)p);
                    break;
                case (short)ServerPacketIds.MentorRequest:
                    MentorRequest((S.MentorRequest)p);
                    break;
                case (short)ServerPacketIds.TradeRequest:
                    TradeRequest((S.TradeRequest)p);
                    break;
                case (short)ServerPacketIds.TradeAccept:
                    TradeAccept((S.TradeAccept)p);
                    break;
                case (short)ServerPacketIds.TradeGold:
                    TradeGold((S.TradeGold)p);
                    break;
                case (short)ServerPacketIds.TradeItem:
                    TradeItem((S.TradeItem)p);
                    break;
                case (short)ServerPacketIds.TradeConfirm:
                    TradeConfirm();
                    break;
                case (short)ServerPacketIds.TradeCancel:
                    TradeCancel((S.TradeCancel)p);
                    break;
                case (short)ServerPacketIds.MountUpdate:
                    MountUpdate((S.MountUpdate)p);
                    break;
                case (short)ServerPacketIds.TransformUpdate:
                    TransformUpdate((S.TransformUpdate)p);
                    break;
                case (short)ServerPacketIds.EquipSlotItem:
                    EquipSlotItem((S.EquipSlotItem)p);
                    break;
                case (short)ServerPacketIds.FishingUpdate:
                    FishingUpdate((S.FishingUpdate)p);
                    break;
                case (short)ServerPacketIds.ChangeQuest:
                    ChangeQuest((S.ChangeQuest)p);
                    break;
                case (short)ServerPacketIds.CompleteQuest:
                    CompleteQuest((S.CompleteQuest)p);
                    break;
                case (short)ServerPacketIds.ShareQuest:
                    ShareQuest((S.ShareQuest)p);
                    break;
                case (short)ServerPacketIds.GainedQuestItem:
                    GainedQuestItem((S.GainedQuestItem)p);
                    break;
                case (short)ServerPacketIds.DeleteQuestItem:
                    DeleteQuestItem((S.DeleteQuestItem)p);
                    break;
                case (short)ServerPacketIds.CancelReincarnation:
                    User.ReincarnationStopTime = 0;
                    break;
                case (short)ServerPacketIds.RequestReincarnation:
                    if (!User.Dead) return;
                    RequestReincarnation();
                    break;
                case (short)ServerPacketIds.UserBackStep:
                    UserBackStep((S.UserBackStep)p);
                    break;
                case (short)ServerPacketIds.ObjectBackStep:
                    ObjectBackStep((S.ObjectBackStep)p);
                    break;
                case (short)ServerPacketIds.UserDashAttack:
                    UserDashAttack((S.UserDashAttack)p);
                    break;
                case (short)ServerPacketIds.ObjectDashAttack:
                    ObjectDashAttack((S.ObjectDashAttack)p);
                    break;
                case (short)ServerPacketIds.UserAttackMove://Warrior Skill - SlashingBurst
                    UserAttackMove((S.UserAttackMove)p);
                    break;
                case (short)ServerPacketIds.CombineItem:
                    CombineItem((S.CombineItem)p);
                    break;
                case (short)ServerPacketIds.ItemUpgraded:
                    ItemUpgraded((S.ItemUpgraded)p);
                    break;
                case (short)ServerPacketIds.SetConcentration:
                    SetConcentration((S.SetConcentration)p);
                    break;
                case (short)ServerPacketIds.SetObjectConcentration:
                    SetObjectConcentration((S.SetObjectConcentration)p);
                    break;
                case (short)ServerPacketIds.SetElemental:
                    SetElemental((S.SetElemental)p);
                    break;
                case (short)ServerPacketIds.SetObjectElemental:
                    SetObjectElemental((S.SetObjectElemental)p);
                    break;
                case (short)ServerPacketIds.RemoveDelayedExplosion:
                    RemoveDelayedExplosion((S.RemoveDelayedExplosion)p);
                    break;
                case (short)ServerPacketIds.ObjectDeco:
                    ObjectDeco((S.ObjectDeco)p);
                    break;
                case (short)ServerPacketIds.ObjectSneaking:
                    ObjectSneaking((S.ObjectSneaking)p);
                    break;
                case (short)ServerPacketIds.ObjectLevelEffects:
                    ObjectLevelEffects((S.ObjectLevelEffects)p);
                    break;
                case (short)ServerPacketIds.SetBindingShot:
                    SetBindingShot((S.SetBindingShot)p);
                    break;
                case (short)ServerPacketIds.SendOutputMessage:
                    SendOutputMessage((S.SendOutputMessage)p);
                    break;
                case (short)ServerPacketIds.NPCAwakening:
                    NPCAwakening();
                    break;
                case (short)ServerPacketIds.NPCDisassemble:
                    NPCDisassemble();
                    break;
                case (short)ServerPacketIds.NPCDowngrade:
                    NPCDowngrade();
                    break;
                case (short)ServerPacketIds.NPCReset:
                    NPCReset();
                    break;
                case (short)ServerPacketIds.AwakeningNeedMaterials:
                    AwakeningNeedMaterials((S.AwakeningNeedMaterials)p);
                    break;
                case (short)ServerPacketIds.AwakeningLockedItem:
                    AwakeningLockedItem((S.AwakeningLockedItem)p);
                    break;
                case (short)ServerPacketIds.Awakening:
                    Awakening((S.Awakening)p);
                    break;
                case (short)ServerPacketIds.ReceiveMail:
                    ReceiveMail((S.ReceiveMail)p);
                    break;
                case (short)ServerPacketIds.MailLockedItem:
                    MailLockedItem((S.MailLockedItem)p);
                    break;
                case (short)ServerPacketIds.MailSent:
                    MailSent((S.MailSent)p);
                    break;
                case (short)ServerPacketIds.MailSendRequest:
                    MailSendRequest((S.MailSendRequest)p);
                    break;
                case (short)ServerPacketIds.ParcelCollected:
                    ParcelCollected((S.ParcelCollected)p);
                    break;
                case (short)ServerPacketIds.MailCost:
                    MailCost((S.MailCost)p);
                    break;
                case (short)ServerPacketIds.ResizeInventory:
                    ResizeInventory((S.ResizeInventory)p);
                    break;
                case (short)ServerPacketIds.ResizeStorage:
                    ResizeStorage((S.ResizeStorage)p);
                    break;
                case (short)ServerPacketIds.ResizeHeroInventory:
                    ResizeHeroInventory((S.ResizeHeroInventory)p);
                    break;
                case (short)ServerPacketIds.ComboHero:
                    ComboHero((S.ComboHero)p);
                    break;
                case (short)ServerPacketIds.ComboStance:
                    ComboStance((S.ComboStance)p);
                    break;
                case (short)ServerPacketIds.NewIntelligentCreature:
                    NewIntelligentCreature((S.NewIntelligentCreature)p);
                    break;
                case (short)ServerPacketIds.UpdateIntelligentCreatureList:
                    UpdateIntelligentCreatureList((S.UpdateIntelligentCreatureList)p);
                    break;
                case (short)ServerPacketIds.IntelligentCreatureEnableRename:
                    IntelligentCreatureEnableRename((S.IntelligentCreatureEnableRename)p);
                    break;
                case (short)ServerPacketIds.IntelligentCreaturePickup:
                    IntelligentCreaturePickup((S.IntelligentCreaturePickup)p);
                    break;
                case (short)ServerPacketIds.NPCPearlGoods:
                    NPCPearlGoods((S.NPCPearlGoods)p);
                    break;
                case (short)ServerPacketIds.FriendUpdate:
                    FriendUpdate((S.FriendUpdate)p);
                    break;
                case (short)ServerPacketIds.LoverUpdate:
                    LoverUpdate((S.LoverUpdate)p);
                    break;
                case (short)ServerPacketIds.MentorUpdate:
                    MentorUpdate((S.MentorUpdate)p);
                    break;
                case (short)ServerPacketIds.GuildBuffList:
                    GuildBuffList((S.GuildBuffList)p);
                    break;
                case (short)ServerPacketIds.GameShopInfo:
                    GameShopUpdate((S.GameShopInfo)p);
                    break;
                case (short)ServerPacketIds.HeroStashInfo:
                    HeroStashInfo((S.HeroStashInfo)p);
                    break;
                case (short)ServerPacketIds.HeroStashActivate:
                    HeroStashActivate((S.HeroStashActivate)p);
                    break;
                case (short)ServerPacketIds.GameShopStock:
                    GameShopStock((S.GameShopStock)p);
                    break;
                case (short)ServerPacketIds.Rankings:
                    Rankings((S.Rankings)p);
                    break;
                case (short)ServerPacketIds.EEHeroRank:
                    HeroRanks((S.HeroRanking)p);
                    break;
                case (short)ServerPacketIds.Opendoor:
                    Opendoor((S.Opendoor)p);
                    break;
                case (short)ServerPacketIds.MobHealthChanged:
                    MobHealthChanged((S.MobHealthChanged)p);
                    break;
                case (short)ServerPacketIds.Crafting:
                    SeeCraftResult((S.Crafting)p);
                    break;
                case (short)ServerPacketIds.UpdatePlayerRecipes:
                    UpdatePlayerRecipes((S.UpdatePlayerRecipes)p);
                    break;
                case (short)ServerPacketIds.CreateHero:
                    OpenHeroCreate((S.HeroCreation)p);
                    break;
                case (short)ServerPacketIds.RecipieShopContents:
                    DisplayRecipes((S.RecipieShopContents)p);
                    break;
                case (short)ServerPacketIds.PurchaseResult:
                    PurchaseResult((S.PurchaseResult)p);
                    break;
                case (short)ServerPacketIds.EESocketDisplay:
                    SocketDialog.PopulateRuneCells((S.GetSocketInfo)p);
                    break;
                case (short)ServerPacketIds.EEOpenSocketDialog:
                    if (!SocketDialog.Visible)
                        SocketDialog.Show();
                    break;
                case (short)ServerPacketIds.EERuneSocketResult:
                    SocketDialog.DisplayResult((S.AddRuneResult)p);
                    break;
                case (short)ServerPacketIds.EERemoveRune:
                    SocketDialog.RemoveRune((S.RemoveRune)p);
                    break;
                case (short)ServerPacketIds.EEOpenGuildBoardDialog:
                    if (!GuildBoardDialog.Visible)
                        GuildBoardDialog.Show();
                    break;
                case (short)ServerPacketIds.OpenRankDialog:
                    if (!RankingDialog.Visible)
                        RankingDialog.Show();
                    break;
                case (short)ServerPacketIds.OpenHeroRankDialog:
                    if (!HeroRankingDialog.Visible)
                        HeroRankingDialog.Show();
                    break;
                case (short)ServerPacketIds.LogNotice:
                    ShowLogNotice((S.UpdateLogNotice)p);
                    break;
                case (short)ServerPacketIds.EEClientCompletedQuests:
                    UpdateClientCompletedList((S.ClientCompletedQuests)p);
                    break;
                case (short)ServerPacketIds.EEClientAvailableQuests:
                    UpdateAvailableQuests((S.ClientAvaileQuests)p);
                    break;
                case (short)ServerPacketIds.RefreshHeroCap:
                    UpdateHeroCap((S.RefreshHeroCap)p);
                    break;
                case (short)ServerPacketIds.EnterPublicEvent:
                    EnterPublicEvent((S.EnterOrUpdatePublicEvent)p);
                    break;
                case (short)ServerPacketIds.LeavePublicEvent:
                    LeavePublicEvent((S.LeavePublicEvent)p);
                    break;
                case (short)ServerPacketIds.ActivateEvent:
                    ActivateEvent((S.ActivateEvent)p);
                    break;
                case (short)ServerPacketIds.DeactivateEvent:
                    DeactivateEvent((S.DeactivateEvent)p);
                    break;
                case (short)ServerPacketIds.HelmetChange:
                    UpdatePlayerHelmet((S.UpdateHelmet)p);
                    break;
                case (short)ServerPacketIds.ToggleBuff:
                    ToggleBuff((S.ToggleBuff)p);
                    break;
                case (short)ServerPacketIds.LMSDialog:
                    if (!LMS_BR_SignupDialog.Visible)
                        LMS_BR_SignupDialog.Show();
                    break;
                case (short)ServerPacketIds.BRList:
                    LMS_BR_SignupDialog.UpdateInfo((S.LMS_BRs)p);
                    break;
                case (short)ServerPacketIds.BigMapQuestLocations:

                    break;
                default:
                    base.ProcessPacket(p);
                    break;
            }
        }

        public void ToggleBuff(S.ToggleBuff p)
        {
            for (int i = 0; i < Buffs.Count; i++)
            {
                if (Buffs[i].Type == BuffType.LoverRecall)
                    continue;
                if (Buffs[i].Type == p.BType &&
                    Buffs[i].Paused != p.Paused)
                {
                    if (Buffs[i].Paused && !p.Paused)
                    //set the time to the original
                    {
                        Buffs[i].Expire = Buffs[i].Expire + CMain.Time;
                        Buffs[i].Infinite = false;
                    }
                    else
                    {
                        Buffs[i].Expire = Buffs[i].Expire - CMain.Time;
                        Buffs[i].Infinite = true;
                    }

                    Buffs[i].Paused = p.Paused;
                }
            }
        }

        public void UpdatePlayerHelmet(S.UpdateHelmet p)
        {
            //  Ignore our selfs as we have no need to update it for self as it's done within the refresh libs
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                if (ob is PlayerObject tmpP)
                    tmpP.Helmet = p.Helmet;
            }
        }

        public void ActivateEvent(S.ActivateEvent p)
        {
            if (!MapControl.MapEvents.Any(i => i.Index == p.Event.Index))
                MapControl.MapEvents.Add(p.Event);
        }

        public void DeactivateEvent(S.DeactivateEvent p)
        {
            MapControl.MapEvents.RemoveAll(o => o.Index == p.Event.Index);
        }

        public void EnterPublicEvent(S.EnterOrUpdatePublicEvent p)
        {
            EventDialog.UpdateDialog(p.EventName, p.ObjectiveMessage, p.Objectives, p.Stage);
            EventDialog.Show();

            if (p.Objectives.All(o => o.MonsterAliveCount == 0))
            {
                EventDialog.Hide();
            }
        }
        public void LeavePublicEvent(S.LeavePublicEvent p)
        {
            EventDialog.Hide();
        }

        public void UpdateHeroCap(S.RefreshHeroCap p)
        {
            User.HeroCap = p.CapNow;
            if (HeroStashDialog.Visible)
                HeroStashDialog.RefreshGUI();
        }

        public void UpdateAvailableQuests(S.ClientAvaileQuests p)
        {
            if (p == null) return;
            User.AvailableQuests = p.list;
        }

        public void UpdateClientCompletedList(S.ClientCompletedQuests p)
        {
            if (p == null) return;
            User.ClientCompletedQuests = p.list;
        }

        public void ShowLogNotice(S.UpdateLogNotice p)
        {
            string[] stringSeps = new string[] { "\r\n" };
            LoginNoticeDialog.notice = p.list;
            List<string> temp = new List<string>();
            for(int i = 0; i < p.list.Count; i++)
            {
                if (i == 0)
                    temp.Add("------------------------------");
                temp.Add(p.list[i].Title);
                if (p.list[i].LogString.Contains("\r\n"))
                {
                    string[] lines = p.list[i].LogString.Split(stringSeps, StringSplitOptions.None);
                    foreach (string s in lines)
                        temp.Add(s);
                }
                temp.Add("------------------------------");
            }
            LoginNoticeDialog.NewText(temp);
            LoginNoticeDialog.Visible = true;
        }
        

        public void PurchaseResult(S.PurchaseResult p)
        {
            //  Error building :P
            switch(p.Result)
            {
                case -1:
                    ChatDialog.ReceiveChat("Invalid info", ChatType.System);
                    break;
                case -2:
                    ChatDialog.ReceiveChat("No Shop Info", ChatType.System);
                    break;
                case -4:
                case 8:
                    ChatDialog.ReceiveChat("Could not find item(s)!", ChatType.System);
                    break;
                case 0:
                    break;
                case 1:
                    ChatDialog.ReceiveChat("No item selected!", ChatType.System);
                    break;
                case 2:
                    ChatDialog.ReceiveChat("You must purchase at least 1!", ChatType.System);
                    break;                
                case 4:
                    ChatDialog.ReceiveChat("Invalid Item!", ChatType.System);
                    break;
                case 5:
                case 9:
                    ChatDialog.ReceiveChat("Could not purchase the item(s), check your bag space", ChatType.System);
                    break;
                case 6:
                case 10:
                    ChatDialog.ReceiveChat("Invalid Shop Contents!", ChatType.System);
                    break;
                case 3:
                case 7:
                    ChatDialog.ReceiveChat("Not enough gold!", ChatType.System);
                    break;
            }
        }

        public void DisplayRecipes(S.RecipieShopContents p)
        {
            Recipe_ShopDialog.UpdateContent(p);
            Recipe_ShopDialog.Show();
        }

        public void OpenHeroCreate(S.HeroCreation p)
        {
            HeroCreationDialog.Show();

            if (!p.allowArc)
                HeroCreationDialog.ArcherButton.Enabled = false;
            else
                HeroCreationDialog.ArcherButton.Enabled = true;

            if (!p.allowSin)
                HeroCreationDialog.AssassinButton.Enabled = false;
            else
                HeroCreationDialog.AssassinButton.Enabled = true;
        }

        public void UpdatePlayerRecipes(S.UpdatePlayerRecipes p)
        {
            //  After clicking the button, it should request the recipes (current and 'hidden') and recieve here
            if (p.PlayerRecipe != null &&
                p.PlayerRecipe.Count > 0)
                User.Recipes.Clear();

            for (int i = 0; i < p.PlayerRecipe.Count; i++)
            {
                PlayerRecipe recipe = new PlayerRecipe
                {
                    Recipe = p.PlayerRecipe[i].Recipe,
                    Learnt = p.PlayerRecipe[i].Learnt,
                    CraftEndTime = p.PlayerRecipe[i].CurrentCraftTime,
                    CraftEnd = p.PlayerRecipe[i].Finishedcraft,
                    InPrcoess = p.PlayerRecipe[i].InProgress,
                    Collected = p.PlayerRecipe[i].Collected
                };
                User.Recipes.Add(recipe);
            }

            if (p.RecipeList != null)
                CraftingDialog.RecipeList = p.RecipeList;

            //  This should just refresh the UI if it's already visible
            if (CraftingDialog.Visible)
                CraftingDialog.RefreshRecipes();

            //  Not really sure why that was like that but
            //  This will display the UI if not already showing
            if (!CraftingDialog.Visible && craftClciked) 
                CraftingDialog.Show();
        }

        public void SeeCraftResult(S.Crafting p)
        {

            if (InventoryDialog.Visible)
                InventoryDialog.Hide();
            for (int i = 0; i < p.RemoveIds.Count; i++)
            {
                MirItemCell cell = InventoryDialog.GetCell((ulong)p.RemoveIds[i]);
                if (cell != null)
                {

                    if (p.ReduceCount[i] > 0)
                    {
                        cell.Item.Count -= (uint)p.ReduceCount[i];
                    }
                    else
                    {
                        cell.Item = null;
                        cell.Locked = false;
                    }                    
                }
                cell = BeltDialog.GetCell((ulong)p.RemoveIds[i]);
                if(cell != null)
                {
                    if (p.ReduceCount[i] > 0)
                        cell.Item.Count -= (uint)p.ReduceCount[i];
                    else
                    {
                        cell.Item = null;
                        cell.Locked = false;
                    }
                }
            }

            switch (p.result)
            {
                case 0:
                    GameScene.Scene.ChatDialog.ReceiveChat("Invalid Recipe(1)!", ChatType.System2);
                    break;
                case -1:
                    GameScene.Scene.ChatDialog.ReceiveChat("Invalid Recipe(2)!", ChatType.System2);
                    break;
                case -2:
                    GameScene.Scene.ChatDialog.ReceiveChat("Invalid Level!", ChatType.System2);
                    break;
                case -3:
                    GameScene.Scene.ChatDialog.ReceiveChat("Invalid Class!", ChatType.System2);
                    break;
                case -4:
                    GameScene.Scene.ChatDialog.ReceiveChat("Not enough Gold!", ChatType.System2);
                    break;
                case -5:
                    GameScene.Scene.ChatDialog.ReceiveChat("Not enough materials!", ChatType.System2);
                    break;
                case -6:
                    GameScene.Scene.ChatDialog.ReceiveChat("You have not learn the recipe or do not have the recipe item!", ChatType.System2);
                    break;
            }
            if (p.result == 2)
            {
                if (CraftingDialog.Visible)
                    CraftingDialog.RefreshRecipes();
            }
            else if (p.result == 3)
            {

            }
            if (CraftingDialog.Visible)
                CraftingDialog.RefreshRecipes();
        }

        public void CreateBuff(Buff buff)
        {
            string text = "";
            int buffImage = BuffImage(buff.Type);

            MLibrary buffLibrary = Libraries.BuffIcon;

            if (buffImage >= 20000)
            {
                buffImage -= 20000;
                buffLibrary = Libraries.MagIcon;
            }

            if (buffImage >= 10000)
            {
                buffImage -= 10000;
                buffLibrary = Libraries.Prguse2;
            }

            MirImageControl image = new MirImageControl
            {
                Library = buffLibrary,
                Parent = this,
                Visible = true,
                Sort = false,
                Index = buffImage
            };

            new MirLabel
            {
                DrawFormat = TextFormatFlags.Right,
                NotControl = true,
                ForeColour = Color.Yellow,
                Location = new Point(-7, 10),
                Size = new Size(30, 20),
                Parent = image
            };
            
            switch (buff.Type)
            {
                case BuffType.Impact:
                    text = string.Format("DC increased by 0-{0} for {1} seconds.", buff.Values[0], (buff.Expire - CMain.Time) / 1000);
                    break;
                case BuffType.Magic:
                    text = string.Format("MC increased by 0-{0} for {1} seconds.", buff.Values[0], (buff.Expire - CMain.Time) / 1000);
                    break;
                case BuffType.Taoist:
                    text = string.Format("SC increased by 0-{0} for {1} seconds.", buff.Values[0], (buff.Expire - CMain.Time) / 1000);
                    break;
                case BuffType.Storm:
                    text = string.Format("A.Speed increased by {0} for {1} seconds.", buff.Values[0], (buff.Expire - CMain.Time) / 1000);
                    break;
                case BuffType.Accuracy:
                    text = string.Format("Accuracy increased by {0} for {1} seconds.", buff.Values[0], (buff.Expire - CMain.Time) / 1000);
                    break;
                case BuffType.Agility:
                    text = string.Format("Agility increased by {0} for {1} seconds.", buff.Values[0], (buff.Expire - CMain.Time) / 1000);
                    break;
                case BuffType.HealthAid:
                    text = string.Format("HP increased by {0} for {1} seconds.", buff.Values[0], (buff.Expire - CMain.Time) / 1000);
                    break;
                case BuffType.ManaAid:
                    text = string.Format("MP increased by {0} for {1} seconds.", buff.Values[0], (buff.Expire - CMain.Time) / 1000);
                    break;
                case BuffType.Defence:
                    text = string.Format("Max AC increased by {0} for {1} seconds.", buff.Values[0], (buff.Expire - CMain.Time) / 1000);
                    break;
                case BuffType.MagicDefence:
                    text = string.Format("Max MAC increased by {0} for {1} seconds.", buff.Values[0], (buff.Expire - CMain.Time) / 1000);
                    break;
            }

            if (text != "") GameScene.Scene.ChatDialog.ReceiveChat(text, ChatType.Hint);
            BuffList.Insert(0, image);

        }
        public void UpdateBuffs()
        {
            int h = 0;
            int p = 0;
            for (int i = 0; i < BuffList.Count; i++)
            {
                MirImageControl image = BuffList[i];

                //jp test
                if (i >= Buffs.Count) break;

                Buff buff = Buffs[i];

                int buffImage = BuffImage(buff.Type);
                MLibrary buffLibrary = Libraries.BuffIcon;

                //ArcherSpells - VampireShot,PoisonShot
                if (buffImage >= 20000)
                {
                    buffImage -= 20000;
                    buffLibrary = Libraries.MagIcon;
                }

                if (buffImage >= 10000)
                {
                    buffImage -= 10000;
                    buffLibrary = Libraries.Prguse2;
                }

                image.Location = new Point((Settings.ScreenWidth - 150) - (buff.Hero ? h : p) * 23 , 2 + (buff.Hero ? 25 : 0));
                image.Hint = buff.ToString();
                image.Index = buffImage;
                image.Library = buffLibrary;
                image.Visible = MainDialog.Visible;
                if (buff.Hero)
                {
                    if (!buff.Infinite && buff.Expire > CMain.Time)
                        h++;
                    image.ForeColour = Color.Gray;
                }
                else
                {
                    p++;
                    image.ForeColour = Color.White;
                }

                if ( ( !buff.Infinite && Math.Round((buff.Expire - CMain.Time) / 1000D) <= 5))
                {
                    double time = (buff.Expire - CMain.Time) / 100D;

                    if (Math.Round(time) % 10 < 5) image.Index = -1;
                }
                /*
                if (!HasEffect(Buffs[i].Type))
                    User.AddBuffEffect(Buffs[i].Type);
                    */
                
                //((MirLabel)image.Controls[0]).Text = buff.Infinite ? "" : timeRemaining.ToString();   
            }
            User.ProcessBuffs();
        }

        public bool HasEffect(BuffType type)
        {
            if (User.Effects == null)
                return false;
            if (User.Effects.Count > 0)
            {
                for (int i = 0; i < User.Effects.Count; i++)
                {
                    if (User.Effects[i] is BuffEffect)
                    {
                        BuffEffect buffef = (BuffEffect)User.Effects[i];
                        if (buffef != null)
                        {
                            if (buffef.BuffType == type)
                                return true;
                        }
                    }
                }
            }
            return false;
        }
 
        public int BuffImage(BuffType type) // Buff Icons
        {
            switch (type)
            {
                //Skills
                case BuffType.Fury:
                    return 128;
                case BuffType.Rage:
                    return 49;
                case BuffType.ImmortalSkin:
                    return 80;
                case BuffType.CounterAttack:
                    return 257;
                case BuffType.MagicBooster:
                    return 256;
                case BuffType.MoonMist:
                    return 264;
                case BuffType.MagicShield:
                    return 30;
                case BuffType.Hiding:
                case BuffType.Cloak:
                    return 17;
                case BuffType.Haste:
                    return 60;
                case BuffType.SoulShield:
                    return 13;
                case BuffType.BlessedArmour:
                    return 14;
                case BuffType.ProtectionField:
                    return 50;
                case BuffType.UltimateEnhancer:
                case BuffType.UltimateEnhancerQuest:
                    return 35;
                case BuffType.Curse:
                    return 45;
                case BuffType.EnergyShield:
                case BuffType.HeroEnergyShield:
                    return 57;

                case BuffType.HumUp:
                case BuffType.SwBuff:
                    return 105;//508

                case BuffType.SwiftFeet:
                    return 67;
                case BuffType.FastMove:
                    return 130;
                case BuffType.LightBody:
                    return 68;
                case BuffType.MoonLight:
                    return 65;
                case BuffType.DarkBody:
                    return 70;
                case BuffType.CombinedBuff:
                    return 78;

                case BuffType.Concentration:
                    return 96;
                case BuffType.VampireShot:
                    return 100;
                case BuffType.PoisonShot:
                    return 102;
                case BuffType.MentalState:
                    return 199;
                case BuffType.VIP:
                    return 181;
                case BuffType.NewbieGuild:
                    return 260;
                case BuffType.HeroBuff:
                    return 186;
                case BuffType.Global:
                    return 182;
                case BuffType.Group:
                    return 260;

                //Special
                case BuffType.GameMaster:
                    return 173;
                case BuffType.General:
                    return 182;
                case BuffType.Exp:
                case BuffType.ExpQuest:
                    return 260;
                case BuffType.Drop:
                case BuffType.DropQuest:
                    return 162;
                case BuffType.Gold:
                    return 168;
                case BuffType.Knapsack:
                case BuffType.BagWeight:
                    return 235;
                case BuffType.Transform:
                    return 241;
                case BuffType.Mentor:
                    return 248;
                case BuffType.Mentee:
                    return 248;
                case BuffType.RelationshipEXP:
                    return 201;
                case BuffType.Guild:
                    return 203;
                case BuffType.Rested:
                    return 240;
                case BuffType.TemporalFlux:
                    return 261;
                case BuffType.ShadowStepFlux:
                    return 261;

                //Stats
                case BuffType.Impact:
                    return 249;
                case BuffType.Magic:
                    return 165;
                case BuffType.Taoist:
                    return 250;
                case BuffType.Storm:
                case BuffType.StormQuest:
                    return 170;
                case BuffType.Accuracy:
                case BuffType.AccuracyQuest:
                    return 161;
                case BuffType.Agility:
                    return 68;
                case BuffType.HealthAid:
                    return 161;
                case BuffType.ManaAid:
                    return 169;
                case BuffType.Defence:
                    return 166;
                case BuffType.MagicDefence:
                    return 158;
                case BuffType.LoverRecall:
                    return 230;
                case BuffType.WonderDrug:
                    return 252;
                case BuffType.PinPoint:
                case BuffType.Enrage:
                case BuffType.IronWall:
                case BuffType.Evasive:
                case BuffType.SpeedyMagician:
                case BuffType.MobDebuff:
                    return 255;
                default:
                    return 0;
            }
        }
        private void KeepAlive(S.KeepAlive p)
        {
            if (p.Time == 0) return;
            PingTime = (CMain.Time - p.Time);
        }
        private void MapInformation(S.MapInformation p)
        {
            if (MapControl != null && !MapControl.IsDisposed)
                MapControl.Dispose();
            MapControl = new MapControl { FileName = Path.Combine(Settings.MapPath, p.FileName + ".map"), Title = p.Title, MiniMap = p.MiniMap, BigMap = p.BigMap, Lights = p.Lights, Lightning = p.Lightning, Fire = p.Fire, MapDarkLight = p.MapDarkLight, Music = p.Music };
            MapControl.LoadMap();
            MapControl.MapEvents.Clear();
            MapControl.MapEvents.AddRange(p.MapEvents);
            MapControl.QuestLocations.Clear();
            //MapControl.QuestLocations.AddRange(p.QuestLocations);
            InsertControl(0, MapControl);
            
        }
        private void UserInformation(S.UserInformation p)
        {
            User = new UserObject(p.ObjectID);
            User.Load(p);
            MainDialog.PModeLabel.Visible = true;
            Gold = p.Gold;
            RealGold = Math.Min(uint.MaxValue, p.Gold * 30 / 100);
            Credit = p.Credit;
            RealCredit = Math.Min(uint.MaxValue, p.Credit * 30 / 100);

            BeforeEnterGame();
            InventoryDialog.RefreshInventory();
            foreach (SkillBarDialog Bar in SkillBarDialogs)
                Bar.Update();

        }

        private void HeroInformation(S.HeroInformation p)
        {
            Hero = p;

            BindAllItems();

            GameScene.Scene.HeroInventoryDialog.autoPotDialog.LoadInterface();
            HeroInventoryDialog.autoPotDialog.RefreshInterface();
        }

        private void HeroStats(S.HeroStats p)
        {
            if (Hero == null) return;

            Hero.MinAC = p.MinAC;
            Hero.MaxAC = p.MaxAC;

            Hero.MinMAC = p.MinMAC;
            Hero.MaxMAC = p.MaxMAC;

            Hero.MinDC = p.MinDC;
            Hero.MaxDC = p.MaxDC;

            Hero.MinMC = p.MinMC;
            Hero.MaxMC = p.MaxMC;

            Hero.MinSC = p.MinSC;
            Hero.MaxSC = p.MaxSC;

            Hero.Accuracy = p.Accuracy;
            Hero.Agility = p.Agility;

            Hero.MaxHP = (ushort)p.MaxHP;
            Hero.MaxMP = (ushort)p.MaxMP;

            Hero.Luck = p.Luck;
            Hero.ASpeed = p.ASpeed;

            Hero.CriticalDamage = p.CriticalDamage;
            Hero.CriticalRate = p.CriticalRate;
            Hero.CurrentBagWeight = p.CurrentBagWeight;
            Hero.CurrentHandWeight = p.CurrentHandWeight;
            Hero.CurrentWearWeight = p.CurrentWearWeight;
            Hero.MaxBagWeight = p.MaxBagWeight;
            Hero.MaxHandWeight = p.MaxHandWeight;
            Hero.MaxWearWeight = p.MaxWearWeight;
            Hero.MagicResist = p.MagicResist;
            Hero.PoisonAttack = p.PoisonAttack;
            Hero.PoisonRecovery = p.PoisonRecovery;
            Hero.PoisonResist = p.PoisonResist;
            Hero.Freezing = p.Freezing;
            Hero.HealthRecovery = p.HealthRecovery;
            Hero.SpellRecovery = p.SpellRecovery;
            Hero.Holy = p.Holy;
        }

        public void BindAllItems()
        {
            for (int i = 0; i < Hero.Inventory.Length; i++)
            {
                if (Hero.Inventory[i] == null) continue;
                GameScene.Bind(Hero.Inventory[i]);
            }

            for (int i = 0; i < Hero.Equipment.Length; i++)
            {
                if (Hero.Equipment[i] == null) continue;
                GameScene.Bind(Hero.Equipment[i]);
            }

            for (int i = 0; i < Hero.QuestInventory.Length; i++)
            {
                if (Hero.QuestInventory[i] == null) continue;
                GameScene.Bind(Hero.QuestInventory[i]);
            }
        }


        private void UserLocation(S.UserLocation p)
        {
            MapControl.NextAction = 0;
            if (User.CurrentLocation == p.Location && User.Direction == p.Direction) return;

            if (Settings.DebugMode)
            {
                ReceiveChat(new S.Chat { Message = "Displacement", Type = ChatType.System });
            }

            MapControl.RemoveObject(User);
            User.CurrentLocation = p.Location;
            User.MapLocation = p.Location;
            MapControl.AddObject(User);

            MapControl.FloorValid = false;
            MapControl.InputDelay = CMain.Time + 400;

            if (User.Dead) return;

            User.ClearMagic();
            User.QueuedAction = null;

            for (int i = User.ActionFeed.Count - 1; i >= 0; i--)
            {
                if (User.ActionFeed[i].Action == MirAction.Pushed) continue;
                User.ActionFeed.RemoveAt(i);
            }

            User.SetAction();
        }
        private void ReceiveChat(S.Chat p)
        {
            for (int i = 0; i < p.ChatItems.Count; i++)
            {
                bool hasItem = false;
                for (int x = 0; x < ChatDialog.ChatItems.Count; x++)
                {
                    if (ChatDialog.ChatItems[x].Info == null)
                        ChatDialog.ChatItems[x].Info = GetInfo(ChatDialog.ChatItems[x].ItemIndex);
                    if (ChatDialog.ChatItems[x].UniqueID == p.ChatItems[i].UniqueID)
                    {
                        //  Update it as it's the same Unique ID
                        ChatDialog.ChatItems[x] = p.ChatItems[i];
                        hasItem = true;
                    }
                }
                if (!hasItem)
                {
                    if (p.ChatItems[i].Info == null)
                        p.ChatItems[i].Info = GetInfo(p.ChatItems[i].ItemIndex);
                    ChatDialog.ChatItems.Add(p.ChatItems[i]);
                }
            }
            ChatDialog.ReceiveChat(p.Message, p.Type, p.ChatItems);
        }
        private void ObjectPlayer(S.ObjectPlayer p)
        {
            PlayerObject player = new PlayerObject(p.ObjectID);
            player.Load(p);
        }
        private void ObjectRemove(S.ObjectRemove p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                ob.Remove();
            }
        }
        private void ObjectTurn(S.ObjectTurn p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                ob.ActionFeed.Add(new QueuedAction { Action = MirAction.Standing, Direction = p.Direction, Location = p.Location });
                return;
            }
        }
        private void ObjectWalk(S.ObjectWalk p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                ob.ActionFeed.Add(new QueuedAction { Action = MirAction.Walking, Direction = p.Direction, Location = p.Location });
                return;
            }
        }
        private void ObjectRun(S.ObjectRun p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                ob.ActionFeed.Add(new QueuedAction { Action = MirAction.Running, Direction = p.Direction, Location = p.Location });
                return;
            }
        }
        private void ObjectChat(S.ObjectChat p)
        {
            for (int i = 0; i < p.ChatItems.Count; i++)
            {
                bool hasItem = false;
                for (int x = 0; x < ChatDialog.ChatItems.Count; x++)
                {
                    if (ChatDialog.ChatItems[x].Info == null)
                        ChatDialog.ChatItems[x].Info = GetInfo(ChatDialog.ChatItems[x].ItemIndex);
                    if (ChatDialog.ChatItems[x].UniqueID == p.ChatItems[i].UniqueID)
                    {
                        //  Update it as it's the same Unique ID
                        ChatDialog.ChatItems[x] = p.ChatItems[i];
                        hasItem = true;
                    }
                }
                if (!hasItem)
                {
                    if (p.ChatItems[i].Info == null)
                        p.ChatItems[i].Info = GetInfo(p.ChatItems[i].ItemIndex);
                    ChatDialog.ChatItems.Add(p.ChatItems[i]);
                }
            }
            
            ChatDialog.ReceiveChat(p.Text, p.Type, p.ChatItems);

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                ob.Chat(p.Text);
                return;
            }

        }
        private void MoveItem(S.MoveItem p)
        {
            MirItemCell toCell, fromCell;

            switch (p.Grid)
            {
                case MirGridType.Inventory:
                    fromCell = p.From < User.BeltIdx ? BeltDialog.Grid[p.From] : InventoryDialog.Grid[p.From - User.BeltIdx];
                    break;
                case MirGridType.Storage:
                    fromCell = StorageDialog.Grid[p.From];
                    break;
                case MirGridType.Trade:
                    fromCell = TradeDialog.Grid[p.From];
                    break;
                case MirGridType.Refine:
                    fromCell = RefineDialog.Grid[p.From];
                    break;
                case MirGridType.HeroInventory:
                    fromCell = p.From < 2 ? HeroBeltDialog.Grid[p.From] : HeroInventoryDialog.Grid[p.From - 2];
                    break;
                default:
                    return;
            }

            switch (p.Grid)
            {
                case MirGridType.Inventory:
                    toCell = p.To < User.BeltIdx ? BeltDialog.Grid[p.To] : InventoryDialog.Grid[p.To - User.BeltIdx];
                    break;
                case MirGridType.Storage:
                    toCell = StorageDialog.Grid[p.To];
                    break;
                case MirGridType.Trade:
                    toCell = TradeDialog.Grid[p.To];
                    break;
                case MirGridType.Refine:
                    toCell = RefineDialog.Grid[p.To];
                    break;
                case MirGridType.HeroInventory:
                    toCell = p.To < 2 ? HeroBeltDialog.Grid[p.To] : HeroInventoryDialog.Grid[p.To - 2];
                    break;
                default:
                    return;
            }

            if (toCell == null || fromCell == null) return;

            toCell.Locked = false;
            fromCell.Locked = false;

            if (p.Grid == MirGridType.Trade)
                TradeDialog.ChangeLockState(false);

            if (!p.Success) return;

            UserItem i = fromCell.Item;
            fromCell.Item = toCell.Item;
            toCell.Item = i;

            User.RefreshStats();
            if (CraftingDialog.Visible)
                CraftingDialog.RefreshRecipes();
            CharacterDuraPanel.GetCharacterDura();

            if (GameScene.Hero != null)
                HeroInventoryDialog.autoPotDialog.RefreshInterface();
        }
        private void EquipItem(S.EquipItem p)
        {
            MirItemCell fromCell;

            MirItemCell toCell = CharacterDialog.Grid[p.To];

            switch (p.Grid)
            {
                case MirGridType.Inventory:
                    fromCell = InventoryDialog.GetCell(p.UniqueID) ?? BeltDialog.GetCell(p.UniqueID);
                    break;
                case MirGridType.Storage:
                    fromCell = StorageDialog.GetCell(p.UniqueID) ?? BeltDialog.GetCell(p.UniqueID);
                    break;
                default:
                    return;
            }

            if (toCell == null || fromCell == null) return;

            toCell.Locked = false;
            fromCell.Locked = false;

            if (!p.Success) return;

            UserItem i = fromCell.Item;
            fromCell.Item = toCell.Item;
            toCell.Item = i;
            CharacterDuraPanel.UpdateCharacterDura(i);
            User.RefreshStats();
        }

        private void HeroEquipItem(S.HeroEquipItem p)
        {

            MirItemCell fromCell;

            MirItemCell toCell = HeroCharacterDialog.Grid[p.To];

            switch (p.Grid)
            {
                case MirGridType.Inventory:
                    fromCell = InventoryDialog.GetCell(p.UniqueID) ?? BeltDialog.GetCell(p.UniqueID);
                    break;
                case MirGridType.HeroInventory:
                    fromCell = HeroInventoryDialog.GetCell(p.UniqueID) ?? HeroBeltDialog.GetCell(p.UniqueID);
                    break;
                default:
                    return;
            }

            if (toCell == null || fromCell == null) return;

            toCell.Locked = false;
            fromCell.Locked = false;

            if (!p.Success)
            {
                return;
            }




            UserItem i = fromCell.Item;
            fromCell.Item = toCell.Item;
            toCell.Item = i;

            if (GameScene.Hero != null)
                HeroInventoryDialog.autoPotDialog.RefreshInterface();
        }
        private void EquipSlotItem(S.EquipSlotItem p)
        {
            MirItemCell fromCell;
            MirItemCell toCell;

            switch (p.GridTo)
            {
                case MirGridType.Mount:
                    toCell = MountDialog.Grid[p.To];
                    break;
                case MirGridType.Fishing:
                    toCell = FishingDialog.Grid[p.To];
                    break;
                default:
                    return;
            }

            switch (p.Grid)
            {
                case MirGridType.Inventory:
                    fromCell = InventoryDialog.GetCell(p.UniqueID) ?? BeltDialog.GetCell(p.UniqueID);
                    break;
                case MirGridType.Storage:
                    fromCell = StorageDialog.GetCell(p.UniqueID) ?? BeltDialog.GetCell(p.UniqueID);
                    break;
                default:
                    return;
            }

            //if (toCell == null || fromCell == null) return;

            toCell.Locked = false;
            fromCell.Locked = false;

            if (!p.Success) return;

            UserItem i = fromCell.Item;
            fromCell.Item = null;
            toCell.Item = i;
            User.RefreshStats();
        }

        private void CombineItem(S.CombineItem p)
        {
            MirItemCell fromCell = InventoryDialog.GetCell(p.IDFrom) ?? BeltDialog.GetCell(p.IDFrom);
            MirItemCell toCell = InventoryDialog.GetCell(p.IDTo) ?? BeltDialog.GetCell(p.IDTo);

            if (toCell == null || fromCell == null) return;

            toCell.Locked = false;
            fromCell.Locked = false;

            if (p.Destroy) toCell.Item = null;

            if (!p.Success) return;

            if (fromCell.Item.Count <= 1)
                fromCell.Item = null;
            else
                fromCell.Item.Count--;

            User.RefreshStats();
        }

        private void MergeItem(S.MergeItem p)
        {
            MirItemCell toCell, fromCell;

            switch (p.GridFrom)
            {
                case MirGridType.Inventory:
                    fromCell = InventoryDialog.GetCell(p.IDFrom) ?? BeltDialog.GetCell(p.IDFrom);
                    break;
                case MirGridType.Storage:
                    fromCell = StorageDialog.GetCell(p.IDFrom);
                    break;
                case MirGridType.Equipment:
                    fromCell = CharacterDialog.GetCell(p.IDFrom);
                    break;
                case MirGridType.Trade:
                    fromCell = TradeDialog.GetCell(p.IDFrom);
                    break;
                case MirGridType.Fishing:
                    fromCell = FishingDialog.GetCell(p.IDFrom);
                    break;
                case MirGridType.HeroInventory:
                    fromCell = HeroInventoryDialog.GetCell(p.IDFrom) ?? HeroBeltDialog.GetCell(p.IDFrom);
                    break;
                default:
                    return;
            }

            switch (p.GridTo)
            {
                case MirGridType.Inventory:
                    toCell = InventoryDialog.GetCell(p.IDTo) ?? BeltDialog.GetCell(p.IDTo);
                    break;
                case MirGridType.Storage:
                    toCell = StorageDialog.GetCell(p.IDTo);
                    break;
                case MirGridType.Equipment:
                    toCell = CharacterDialog.GetCell(p.IDTo);
                    break;
                case MirGridType.Trade:
                    toCell = TradeDialog.GetCell(p.IDTo);
                    break;
                case MirGridType.Fishing:
                    toCell = FishingDialog.GetCell(p.IDTo);
                    break;
                case MirGridType.HeroInventory:
                    toCell = HeroInventoryDialog.GetCell(p.IDTo) ?? HeroBeltDialog.GetCell(p.IDTo);
                    break;
                default:
                    return;
            }

            if (toCell == null || fromCell == null) return;

            toCell.Locked = false;
            fromCell.Locked = false;

            if (p.GridFrom == MirGridType.Trade || p.GridTo == MirGridType.Trade)
                TradeDialog.ChangeLockState(false);

            if (!p.Success) return;
            if (fromCell.Item.Count <= toCell.Item.Info.StackSize - toCell.Item.Count)
            {
                toCell.Item.Count += fromCell.Item.Count;
                fromCell.Item = null;
            }
            else
            {
                fromCell.Item.Count -= toCell.Item.Info.StackSize - toCell.Item.Count;
                toCell.Item.Count = toCell.Item.Info.StackSize;
            }

            User.RefreshStats();

            if (GameScene.Hero != null)
                HeroInventoryDialog.autoPotDialog.RefreshInterface();
        }
        private void RemoveItem(S.RemoveItem p)
        {
            MirItemCell toCell;

            int index = -1;

            for (int i = 0; i < MapObject.User.Equipment.Length; i++)
            {
                if (MapObject.User.Equipment[i] == null || MapObject.User.Equipment[i].UniqueID != p.UniqueID) continue;
                index = i;
                break;
            }

            MirItemCell fromCell = CharacterDialog.Grid[index];


            switch (p.Grid)
            {
                case MirGridType.Inventory:
                    toCell = p.To < User.BeltIdx ? BeltDialog.Grid[p.To] : InventoryDialog.Grid[p.To - User.BeltIdx];
                    break;
                case MirGridType.Storage:
                    toCell = StorageDialog.Grid[p.To];
                    break;
                default:
                    return;
            }

            if (toCell == null || fromCell == null) return;

            toCell.Locked = false;
            fromCell.Locked = false;

            if (!p.Success) return;
            toCell.Item = fromCell.Item;
            fromCell.Item = null;
            CharacterDuraPanel.GetCharacterDura();
            User.RefreshStats();
        }



        private void HeroRemoveItem(S.HeroRemoveItem p)
        {
            MirItemCell toCell;

            int index = -1;

            for (int i = 0; i < GameScene.Hero.Equipment.Length; i++)
            {
                if (GameScene.Hero.Equipment[i] == null || GameScene.Hero.Equipment[i].UniqueID != p.UniqueID) continue;
                index = i;
                break;
            }

            MirItemCell fromCell = HeroCharacterDialog.Grid[index];


            switch (p.Grid)
            {
                case MirGridType.Inventory:
                    toCell = p.To < User.BeltIdx ? BeltDialog.Grid[p.To] : InventoryDialog.Grid[p.To - User.BeltIdx];
                    break;
                case MirGridType.HeroInventory:
                    toCell = p.To < 2 ? HeroBeltDialog.Grid[p.To] : HeroInventoryDialog.Grid[p.To - 2];
                    break;
                default:
                    return;
            }

            if (toCell == null || fromCell == null) return;

            toCell.Locked = false;
            fromCell.Locked = false;

            if (!p.Success) return;
            toCell.Item = fromCell.Item;
            fromCell.Item = null;

            if (GameScene.Hero != null)
                HeroInventoryDialog.autoPotDialog.RefreshInterface();
        }


        private void RemoveSlotItem(S.RemoveSlotItem p)
        {
            MirItemCell fromCell;
            MirItemCell toCell;

            //int index = -1;

            switch (p.Grid)
            {
                case MirGridType.Mount:
                    fromCell = MountDialog.GetCell(p.UniqueID);
                    break;
                case MirGridType.Fishing:
                    fromCell = FishingDialog.GetCell(p.UniqueID);
                    break;
                default:
                    return;
            }

            switch (p.GridTo)
            {
                case MirGridType.Inventory:
                    toCell = p.To < User.BeltIdx ? BeltDialog.Grid[p.To] : InventoryDialog.Grid[p.To - User.BeltIdx];
                    break;
                case MirGridType.Storage:
                    toCell = StorageDialog.Grid[p.To];
                    break;
                default:
                    return;
            }

            if (toCell == null || fromCell == null) return;

            toCell.Locked = false;
            fromCell.Locked = false;

            if (!p.Success) return;
            toCell.Item = fromCell.Item;
            fromCell.Item = null;
            CharacterDuraPanel.GetCharacterDura();
            if (CraftingDialog.Visible)
                CraftingDialog.RefreshRecipes();
            User.RefreshStats();
        }
        private void TakeBackItem(S.TakeBackItem p)
        {
            MirItemCell fromCell = StorageDialog.Grid[p.From];

            MirItemCell toCell = p.To < User.BeltIdx ? BeltDialog.Grid[p.To] : InventoryDialog.Grid[p.To - User.BeltIdx];

            if (toCell == null || fromCell == null) return;

            toCell.Locked = false;
            fromCell.Locked = false;

            if (!p.Success) return;
            toCell.Item = fromCell.Item;
            fromCell.Item = null;
            User.RefreshStats();
            if (CraftingDialog.Visible)
                CraftingDialog.RefreshRecipes();
            CharacterDuraPanel.GetCharacterDura();
        }

        private void HeroInventoryToInventory(S.HeroInventoryToInventory p)
        {
            MirItemCell fromCell = p.From < 2 ? HeroBeltDialog.Grid[p.From] : HeroInventoryDialog.Grid[p.From - 2];

            MirItemCell toCell = p.To < User.BeltIdx ? BeltDialog.Grid[p.To] : InventoryDialog.Grid[p.To - User.BeltIdx];

            if (toCell == null || fromCell == null) return;
            toCell.Locked = false;
            fromCell.Locked = false;

            if (!p.Success) return;
            toCell.Item = fromCell.Item;
            fromCell.Item = null;
            User.RefreshStats();
            if (CraftingDialog.Visible)
                CraftingDialog.RefreshRecipes();
            CharacterDuraPanel.GetCharacterDura();

            if (GameScene.Hero != null)
                HeroInventoryDialog.autoPotDialog.RefreshInterface();
        }
        private void StoreItem(S.StoreItem p)
        {
            MirItemCell fromCell = p.From < User.BeltIdx ? BeltDialog.Grid[p.From] : InventoryDialog.Grid[p.From - User.BeltIdx];

            MirItemCell toCell = StorageDialog.Grid[p.To];

            if (toCell == null || fromCell == null) return;

            toCell.Locked = false;
            fromCell.Locked = false;

            if (!p.Success) return;
            toCell.Item = fromCell.Item;
            fromCell.Item = null;
            if (CraftingDialog.Visible)
                CraftingDialog.RefreshRecipes();
            User.RefreshStats();
        }

        private void InventoryToHeroInventory(S.InventoryToHeroInventory p)
        {
            MirItemCell fromCell = p.From < User.BeltIdx ? BeltDialog.Grid[p.From] : InventoryDialog.Grid[p.From - User.BeltIdx];

            MirItemCell toCell =p.To < 2 ? HeroBeltDialog.Grid[p.To] : HeroInventoryDialog.Grid[p.To - 2];

            if (toCell == null || fromCell == null) return;

            toCell.Locked = false;
            fromCell.Locked = false;

            if (!p.Success) return;
            toCell.Item = fromCell.Item;
            fromCell.Item = null;
            if (CraftingDialog.Visible)
                CraftingDialog.RefreshRecipes();
            User.RefreshStats();

            if (GameScene.Hero != null)
                HeroInventoryDialog.autoPotDialog.RefreshInterface();
        }
        private void DepositRefineItem(S.DepositRefineItem p)
        {
            MirItemCell fromCell = p.From < User.BeltIdx ? BeltDialog.Grid[p.From] : InventoryDialog.Grid[p.From - User.BeltIdx];

            MirItemCell toCell = RefineDialog.Grid[p.To];

            if (toCell == null || fromCell == null) return;

            toCell.Locked = false;
            fromCell.Locked = false;

            if (!p.Success) return;
            toCell.Item = fromCell.Item;
            fromCell.Item = null;

            User.RefreshStats();
        }

        private void RetrieveRefineItem(S.RetrieveRefineItem p)
        {
            MirItemCell fromCell = RefineDialog.Grid[p.From];
            MirItemCell toCell = p.To < User.BeltIdx ? BeltDialog.Grid[p.To] : InventoryDialog.Grid[p.To - User.BeltIdx];

            if (toCell == null || fromCell == null) return;

            toCell.Locked = false;
            fromCell.Locked = false;

            if (!p.Success) return;
            toCell.Item = fromCell.Item;
            fromCell.Item = null;
            User.RefreshStats();
        }

        private void RefineCancel(S.RefineCancel p)
        {
            RefineDialog.RefineReset();  
        }

        private void RefineItem(S.RefineItem p)
        {
            RefineDialog.RefineReset();
            for (int i = 0; i < User.Inventory.Length; i++)
            {
                if (User.Inventory[i] != null && User.Inventory[i].UniqueID == p.UniqueID)
                {
                    User.Inventory[i] = null;
                    break;
                }
            }
            NPCDialog.Hide();
        }


        private void DepositTradeItem(S.DepositTradeItem p)
        {
            MirItemCell fromCell = p.From < User.BeltIdx ? BeltDialog.Grid[p.From] : InventoryDialog.Grid[p.From - User.BeltIdx];

            MirItemCell toCell = TradeDialog.Grid[p.To];

            if (toCell == null || fromCell == null) return;

            toCell.Locked = false;
            fromCell.Locked = false;
            TradeDialog.ChangeLockState(false);

            if (!p.Success) return;
            toCell.Item = fromCell.Item;
            fromCell.Item = null;
            if (CraftingDialog.Visible)
                CraftingDialog.RefreshRecipes();
            User.RefreshStats();
        }
        private void RetrieveTradeItem(S.RetrieveTradeItem p)
        {
            MirItemCell fromCell = TradeDialog.Grid[p.From];
            MirItemCell toCell = p.To < User.BeltIdx ? BeltDialog.Grid[p.To] : InventoryDialog.Grid[p.To - User.BeltIdx];

            if (toCell == null || fromCell == null) return;

            toCell.Locked = false;
            fromCell.Locked = false;
            TradeDialog.ChangeLockState(false);

            if (!p.Success) return;
            toCell.Item = fromCell.Item;
            fromCell.Item = null;
            if (CraftingDialog.Visible)
                CraftingDialog.RefreshRecipes();
            User.RefreshStats();
        }
        private void SplitItem(S.SplitItem p)
        {
            Bind(p.Item);

            UserItem[] array;
            switch (p.Grid)
            {
                case MirGridType.Inventory:
                    array = MapObject.User.Inventory;
                    break;
                case MirGridType.Storage:
                    array = Storage;
                    break;
                default:
                    return;
            }

            if (p.Grid == MirGridType.Inventory && (p.Item.Info.Type == ItemType.Potion || p.Item.Info.Type == ItemType.Scroll || p.Item.Info.Type == ItemType.Amulet || (p.Item.Info.Type == ItemType.Script && p.Item.Info.Effect == 1)))
            {
                if (p.Item.Info.Type == ItemType.Potion || p.Item.Info.Type == ItemType.Scroll || (p.Item.Info.Type == ItemType.Script && p.Item.Info.Effect == 1))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (array[i] != null) continue;
                        array[i] = p.Item;
                        if (CraftingDialog.Visible)
                            CraftingDialog.RefreshRecipes();
                        User.RefreshStats();
                        return;
                    }
                }
                else if (p.Item.Info.Type == ItemType.Amulet)
                {
                    for (int i = 4; i < GameScene.User.BeltIdx; i++)
                    {
                        if (array[i] != null) continue;
                        array[i] = p.Item;
                        if (CraftingDialog.Visible)
                            CraftingDialog.RefreshRecipes();
                        User.RefreshStats();
                        return;
                    }
                }
            }

            for (int i = GameScene.User.BeltIdx; i < array.Length; i++)
            {
                if (array[i] != null) continue;
                array[i] = p.Item;
                if (CraftingDialog.Visible)
                    CraftingDialog.RefreshRecipes();
                User.RefreshStats();
                return;
            }

            for (int i = 0; i < GameScene.User.BeltIdx; i++)
            {
                if (array[i] != null) continue;
                array[i] = p.Item;
                if (CraftingDialog.Visible)
                    CraftingDialog.RefreshRecipes();
                User.RefreshStats();
                return;
            }
        }
        private void SplitItem1(S.SplitItem1 p)
        {
            MirItemCell cell;

            switch (p.Grid)
            {
                case MirGridType.Inventory:
                    cell = InventoryDialog.GetCell(p.UniqueID) ?? BeltDialog.GetCell(p.UniqueID);
                    break;
                case MirGridType.Storage:
                    cell = StorageDialog.GetCell(p.UniqueID);
                    break;
                default:
                    return;
            }

            if (cell == null) return;

            cell.Locked = false;

            if (!p.Success) return;
            cell.Item.Count -= p.Count;
            if (CraftingDialog.Visible)
                CraftingDialog.RefreshRecipes();
            User.RefreshStats();
        }

        private void HeroUseItem(S.HeroUseItem p)
        {
            MirItemCell cell = HeroInventoryDialog.GetCell(p.UniqueID) ?? HeroBeltDialog.GetCell(p.UniqueID);

            if (cell == null) return;

            cell.Locked = false;

            if (!p.Success) return;
            if (cell.Item.Count > 1) cell.Item.Count--;
            else cell.Item = null;

            HeroInventoryDialog.autoPotDialog.RefreshInterface();
        }

        private void UseItem(S.UseItem p)
        {
            MirItemCell cell = InventoryDialog.GetCell(p.UniqueID) ?? BeltDialog.GetCell(p.UniqueID);

            if (cell == null) return;

            cell.Locked = false;

            if (!p.Success) return;
            if (cell.Item.Count > 1) cell.Item.Count--;
            else cell.Item = null;
            if (CraftingDialog.Visible)
                CraftingDialog.RefreshRecipes();
            User.RefreshStats();
        }
        private void DropItem(S.DropItem p)
        {
            MirItemCell cell = InventoryDialog.GetCell(p.UniqueID) ?? BeltDialog.GetCell(p.UniqueID);

            if (cell == null) return;

            cell.Locked = false;

            if (!p.Success) return;

            if (p.Count == cell.Item.Count)
                cell.Item = null;
            else
                cell.Item.Count -= p.Count;
            if (CraftingDialog.Visible)
                CraftingDialog.RefreshRecipes();
            User.RefreshStats();
        }


        private void MountUpdate(S.MountUpdate p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                if (MapControl.Objects[i].ObjectID != p.ObjectID) continue;

                PlayerObject player = MapControl.Objects[i] as PlayerObject;
                if (player != null)
                {
                    player.MountUpdate(p);
                }
                break;
            }

            if (p.ObjectID != User.ObjectID) return;

            CanRun = false;

            User.RefreshStats();

            GameScene.Scene.MountDialog.RefreshDialog();
            GameScene.Scene.Redraw();
        }

        private void TransformUpdate(S.TransformUpdate p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                if (MapControl.Objects[i].ObjectID != p.ObjectID) continue;
                if (p.ObjectID == User.ObjectID)
                    User.TransformType = p.TransformType;
                else//maybe :P
                {
                    PlayerObject player = MapControl.Objects[i] as PlayerObject;
                    if (player != null)
                    {
                        player.TransformType = p.TransformType;
                    }
                }
                break;
            }
        }

        private void FishingUpdate(S.FishingUpdate p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                if (MapControl.Objects[i].ObjectID != p.ObjectID) continue;

                PlayerObject player = MapControl.Objects[i] as PlayerObject;
                if (player != null)
                {
                    player.FishingUpdate(p);
                    
                }
                break;
            }

            if (p.ObjectID != User.ObjectID) return;

            GameScene.Scene.FishingStatusDialog.ProgressPercent = p.ProgressPercent;
            GameScene.Scene.FishingStatusDialog.ChancePercent = p.ChancePercent;

            GameScene.Scene.FishingStatusDialog.ChanceLabel.Text = string.Format("{0}%", GameScene.Scene.FishingStatusDialog.ChancePercent);

            if (p.Fishing)
                GameScene.Scene.FishingStatusDialog.Show();
            else
                GameScene.Scene.FishingStatusDialog.Hide();

            Redraw();
        }

        private void CompleteQuest(S.CompleteQuest p)
        {
            User.CompletedQuests = p.CompletedQuests;
        }

        private void ShareQuest(S.ShareQuest p)
        {
            ClientQuestInfo quest = GameScene.QuestInfoList.FirstOrDefault(e => e.Index == p.QuestIndex);
            
            if (quest == null) return;

            MirMessageBox messageBox = new MirMessageBox(string.Format("{0} would like to share a quest with you. Do you accept?", p.SharerName), MirMessageBoxButtons.YesNo);

            messageBox.YesButton.Click += (o, e) => Network.Enqueue(new C.AcceptQuest { NPCIndex = 0, QuestIndex = quest.Index });

            messageBox.Show();
        }

        public void CloseGame()
        {
            if (CMain.Time >= LogTime)
            {
                //If Last Combat < 10 CANCEL
                MirMessageBox messageBox = new MirMessageBox("Do you want to quit Legend of Mir?", MirMessageBoxButtons.YesNo);
                messageBox.YesButton.Click += (o, e) =>
                {
                    Program.CanClose = false;
                    Program.Form.Close();                    
                };
                messageBox.Show();
            }
            else
            {
                ChatDialog.ReceiveChat("Cannot leave game for " + ( LogTime - CMain.Time ) / 1000 + " seconds.", ChatType.System);
            }
        }

        private void ChangeQuest(S.ChangeQuest p)
        {
            switch(p.QuestState)
            {
                case QuestState.Add:
                    if (User.CurrentQuests.Any(x => x.Id == p.Quest.Id)) return;

                    User.CurrentQuests.Add(p.Quest);

                    foreach (ClientQuestProgress quest in User.CurrentQuests)
                        BindQuest(quest);
                    if (Settings.TrackedQuests.Contains(p.Quest.Id))
                    {
                        GameScene.Scene.QuestTrackingDialog.AddQuest(p.Quest, true);
                    }

                    if (p.TrackQuest)
                    {
                        GameScene.Scene.QuestTrackingDialog.AddQuest(p.Quest);
                    }

                    break;
                case QuestState.Update:
                    for (int i = 0; i < User.CurrentQuests.Count; i++)
                    {
                        if (User.CurrentQuests[i].Id != p.Quest.Id) continue;

                        User.CurrentQuests[i] = p.Quest;
                    }

                    foreach (ClientQuestProgress quest in User.CurrentQuests)
                        BindQuest(quest);

                    break;
                case QuestState.Remove:

                    for (int i = User.CurrentQuests.Count - 1; i >= 0; i--)
                    {
                        if (User.CurrentQuests[i].Id != p.Quest.Id) continue;

                        User.CurrentQuests.RemoveAt(i);
                    }

                    GameScene.Scene.QuestTrackingDialog.RemoveQuest(p.Quest);

                    break;
            }

            GameScene.Scene.QuestTrackingDialog.DisplayQuests();

            if (Scene.QuestListDialog.Visible)
            {
                Scene.QuestListDialog.DisplayInfo();
            }

            if (Scene.QuestLogDialog.Visible)
            {
                Scene.QuestLogDialog.UpdateInterface(0);
            }
        }

        private void PlayerUpdate(S.PlayerUpdate p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                if (MapControl.Objects[i].ObjectID != p.ObjectID) continue;

                PlayerObject player = MapControl.Objects[i] as PlayerObject;
                if (player != null) player.Update(p);
                return;
            }
        }
        private void PlayerInspect(S.PlayerInspect p)
        {
            InspectDialog.Items = p.Equipment;

            InspectDialog.Name = p.Name;
            InspectDialog.GuildName = p.GuildName;
            InspectDialog.GuildRank = p.GuildRank;
            InspectDialog.Class = p.Class;
            InspectDialog.Gender = p.Gender;
            InspectDialog.Hair = p.Hair;
            InspectDialog.Level = p.Level;
            InspectDialog.LoverName = p.LoverName;
            InspectDialog.humUp = p.HumUp;
            InspectDialog.hero = p.Hero;

            InspectDialog.RefreshInferface();
            InspectDialog.Show();

            if (p.Hero)
            {
                InspectDialog.Index = 505;
                InspectDialog.Library = Libraries.Title;
                InspectDialog.CharacterPage.Location = new Point(8, 90);
            }
            else
            {
                InspectDialog.Index = 430;
                InspectDialog.Library = Libraries.CustomPrguse;
                InspectDialog.CharacterPage.Location = new Point(8, 70);
            }
        }
        private void LogOutSuccess(S.LogOutSuccess p)
        {
            for (int i = 0; i <= 3; i++)
                SoundManager.StopSound(20000 + 126 * 10 + 5 + i);

            User = null;
            if (Settings.Resolution <= 800)
                CMain.SetResolution(800, 600);
            if (Settings.Resolution >= 1024)
                CMain.SetResolution(1024, 768);
            ActiveScene = new SelectScene(p.Characters);

            Dispose();
        }
        private void LogOutFailed(S.LogOutFailed p)
        {
            Enabled = true;
        }

        private void TimeOfDay(S.TimeOfDay p)
        {
            Lights = p.Lights;
            switch (Lights)
            {
                case LightSetting.Day:
                case LightSetting.Normal:
                    MiniMapDialog.LightSetting.Index = 2093;
                    break;
                case LightSetting.Dawn:
                    MiniMapDialog.LightSetting.Index = 2095;
                    break;
                case LightSetting.Evening:
                    MiniMapDialog.LightSetting.Index = 2094;
                    break;
                case LightSetting.Night:
                    MiniMapDialog.LightSetting.Index = 2092;
                    break;
            }
        }
        private void ChangeAMode(S.ChangeAMode p)
        {
            AMode = p.Mode;

            switch (p.Mode)
            {
                case AttackMode.Peace:
                    ChatDialog.ReceiveChat("[Attack Mode: Peaceful]", ChatType.Hint);
                    break;
                case AttackMode.Group:
                    ChatDialog.ReceiveChat("[Attack Mode: Group]", ChatType.Hint);
                    break;
                case AttackMode.Guild:
                    ChatDialog.ReceiveChat("[Attack Mode: Guild]", ChatType.Hint);
                    break;
                case AttackMode.EnemyGuild:
                    ChatDialog.ReceiveChat("[Attack Mode: Enemy Guild]", ChatType.Hint);
                    break;
                case AttackMode.RedBrown:
                    ChatDialog.ReceiveChat("[Attack Mode: Red+Brown]", ChatType.Hint);
                    break;
                case AttackMode.All:
                    ChatDialog.ReceiveChat("[Attack Mode: All]", ChatType.Hint);
                    break;
            }
        }
        private void ChangePMode(S.ChangePMode p)
        {

            PMode = p.Mode;
            switch (p.Mode)
            {
                case PetMode.Both:
                    ChatDialog.ReceiveChat("[Pet Mode: Attack and Move]", ChatType.Hint);
                    break;
                case PetMode.MoveOnly:
                    ChatDialog.ReceiveChat("[Pet Mode: Do Not Attack]", ChatType.Hint);
                    break;
                case PetMode.AttackOnly:
                    ChatDialog.ReceiveChat("[Pet Mode: Do Not Move]", ChatType.Hint);
                    break;
                case PetMode.None:
                    ChatDialog.ReceiveChat("[Pet Mode: Do Not Attack or Move]", ChatType.Hint);
                    break;
            }

            MainDialog.PModeLabel.Visible = true;
        }


        private void ChangeHMode(S.ChangeHMode p)
        {

            HMode = p.Mode;
            if (User.HasHero) //Ice
                switch (p.Mode)
            {
                case HeroMode.Defensive:
                    ChatDialog.ReceiveChat("[Hero Mode: Defensive]", ChatType.Hint);
                    break;
                case HeroMode.DontAttack:
                    ChatDialog.ReceiveChat("[Hero Mode: Do Not Attack]", ChatType.Hint);
                    break;
                case HeroMode.DontMove:
                    ChatDialog.ReceiveChat("[Hero Mode: Do Not Move]", ChatType.Hint);
                    break;
                case HeroMode.Guard:
                    ChatDialog.ReceiveChat("[Hero Mode: Guard]", ChatType.Hint);
                    break;
                case HeroMode.Offensive:
                    ChatDialog.ReceiveChat("[Hero Mode: Offensive]", ChatType.Hint);
                    break;
            }
            HeroAIDialog.Update(p.Mode);
        }


        private void ObjectItem(S.ObjectItem p)
        {
            ItemObject ob = new ItemObject(p.ObjectID);
            ob.Load(p);
        }
        private void ObjectGold(S.ObjectGold p)
        {
            ItemObject ob = new ItemObject(p.ObjectID);
            ob.Load(p);
        }
        private void GainedItem(S.GainedItem p)
        {
            Bind(p.Item);
            if (p.hero)
            {
                HeroAddItem(p.Item);
                OutputMessage(string.Format("Hero gained {0}.", p.Item.FriendlyName));
            }
            else
            {
                AddItem(p.Item);
                User.RefreshStats();
                if (CraftingDialog.Visible)
                    CraftingDialog.RefreshRecipes();
                OutputMessage(string.Format("You gained {0}.", p.Item.FriendlyName));
            }
        }
        private void GainedQuestItem(S.GainedQuestItem p)
        {
            Bind(p.Item);
            AddQuestItem(p.Item,p.hero);
        }

        private void GainedGold(S.GainedGold p)
        {
            if (p.Gold == 0) return;

            Gold += p.Gold;
            RealGold = Math.Min(uint.MaxValue, Gold * 30 / 100);
            if (CraftingDialog.Visible)
                CraftingDialog.RefreshRecipes();
            SoundManager.PlaySound(SoundList.Gold);
            OutputMessage(string.Format("You gained {0:###,###,###} Gold.", p.Gold));
        }
        private void LoseGold(S.LoseGold p)
        {
            Gold -= p.Gold;
            RealGold = Math.Min(uint.MaxValue, Gold * 30 / 100);
            if (CraftingDialog.Visible)
                CraftingDialog.RefreshRecipes();
            SoundManager.PlaySound(SoundList.Gold);
        }
        private void GainedCredit(S.GainedCredit p)
        {
            if (p.Credit == 0) return;

            Credit += p.Credit;
            RealCredit = Math.Min(uint.MaxValue, Credit * 30 / 100);
            SoundManager.PlaySound(SoundList.Gold);
            OutputMessage(string.Format("You gained {0:###,###,###} Credit.", p.Credit));
        }
        private void LoseCredit(S.LoseCredit p)
        {
            Credit -= p.Credit;
            RealCredit = Math.Min(uint.MaxValue, Credit * 30 / 100);
            SoundManager.PlaySound(SoundList.Gold);
        }
        private void ObjectMonster(S.ObjectMonster p)
        {
            MonsterObject mob;
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID == p.ObjectID)
                {
                    mob = (MonsterObject)ob;
                    mob.Load(p, true);
                    return;
                }
            }
            mob = new MonsterObject(p.ObjectID);
            mob.Load(p);
        }

        private void ObjectAttack(S.ObjectAttack p)
        {
            if (p.ObjectID == User.ObjectID) return;

            QueuedAction action = null;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                if (ob.Race == ObjectType.Player)
                {
                    action = new QueuedAction { Action = MirAction.Attack1, Direction = p.Direction, Location = p.Location, Params = new List<object>() }; //FAR Close up attack
                }
                else
                {
                    switch (p.Type)
                    {
                        default:
                            {
                                action = new QueuedAction { Action = MirAction.Attack1, Direction = p.Direction, Location = p.Location, Params = new List<object>() };
                                break;
                            }
                        case 1:
                            {
                                action = new QueuedAction { Action = MirAction.Attack2, Direction = p.Direction, Location = p.Location, Params = new List<object>() };
                                break;
                            }
                        case 2:
                            {
                                action = new QueuedAction { Action = MirAction.Attack3, Direction = p.Direction, Location = p.Location, Params = new List<object>() };
                                break;
                            }
                        case 3:
                            {
                                action = new QueuedAction { Action = MirAction.Attack4, Direction = p.Direction, Location = p.Location, Params = new List<object>() };
                                break;
                            }
                    }
                }
                action.Params.Add(p.Spell);
                action.Params.Add(p.Level);
                action.Params.Add(p.TargetID);
                ob.ActionFeed.Add(action);
                return;
            }
        }


        public void StopComboSpell(UserObject ob)
        {
            ob.comboSpell.Delay = 0;
            ob.comboSpell.Spell = Spell.None;


            if (ob == User)
            {
                GameScene.Scene.ComboBarBialog.HeroPercent = 0;
                GameScene.Scene.ComboBarBialog.PlayerPercent = 0;
            }
        }


        private void Struck(S.Struck p)
        {
            LogTime = CMain.Time + Globals.LogDelay;

            NextRunTime = CMain.Time + 2500;
            GameScene.OrigNextRun = Math.Min(long.MaxValue, GameScene.NextRunTime * 30 / 100);
            //User.BlizzardStopTime = 0;
            StopComboSpell(User);
            User.Effects.RemoveAll(x => x.spell == Spell.MoonMist);
            User.ClearMagic();
            if (User.ReincarnationStopTime > CMain.Time)
                Network.Enqueue(new C.CancelReincarnation {});

            MirDirection dir = User.Direction;
            Point location = User.CurrentLocation;

            for (int i = 0; i < User.ActionFeed.Count; i++)
                if (User.ActionFeed[i].Action == MirAction.Struck) return;


            if (User.ActionFeed.Count > 0)
            {
                dir = User.ActionFeed[User.ActionFeed.Count - 1].Direction;
                location = User.ActionFeed[User.ActionFeed.Count - 1].Location;
            }

            if (User.Buffs.Any(a => a == BuffType.EnergyShield))
            {
                for (int j = 0; j < User.Effects.Count; j++)
                {
                    BuffEffect effect = null;
                    effect = User.Effects[j] as BuffEffect;

                    if (effect != null && effect.BuffType == BuffType.EnergyShield)
                    {
                        effect.Clear();
                        effect.Remove();

                        User.Effects.Add(effect = new BuffEffect(Libraries.Magic2, 1890, 6, 600, User, true, BuffType.EnergyShield) { Repeat = false });
                        SoundManager.PlaySound(20000 + (ushort)Spell.EnergyShield * 10 + 1);
                        
                        effect.Complete += (o, e) =>
                        {
                            User.Effects.Add(new BuffEffect(Libraries.Magic2, 1900, 2, 800, User, true, BuffType.EnergyShield) { Repeat = true });
                        };


                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < User.Effects.Count; i++)
                {
                    if (User.Effects[i] is BuffEffect bEffect)
                    {
                        if (bEffect.BuffType == BuffType.EnergyShield)
                            bEffect.Repeat = false;
                    }
                }
            }
            QueuedAction action = new QueuedAction { Action = MirAction.Struck, Direction = dir, Location = location, Params = new List<object>() };
            action.Params.Add(p.AttackerID);
            User.ActionFeed.Add(action);

        }
        private void ObjectStruck(S.ObjectStruck p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;

                if (ob.SkipFrames) return;
                if (ob.ActionFeed.Count > 0 && ob.ActionFeed[ob.ActionFeed.Count - 1].Action == MirAction.Struck) return;

                if (ob.Race == ObjectType.Player)
                {
                    ((PlayerObject)ob).BlizzardStopTime = 0;
                    ((PlayerObject)ob).comboSpell.Delay = 0;
                    ((PlayerObject)ob).comboSpell.Spell = Spell.None;

                    ((PlayerObject)ob).Effects.RemoveAll(x => x.spell == Spell.MoonMist);
                }
                QueuedAction action = new QueuedAction { Action = MirAction.Struck, Direction = p.Direction, Location = p.Location, Params = new List<object>() };
                action.Params.Add(p.AttackerID);
                ob.ActionFeed.Add(action);

                if (ob.Buffs.Any(a => a == BuffType.EnergyShield))
                {
                    for (int j = 0; j < ob.Effects.Count; j++)
                    {
                        BuffEffect effect = null;
                        effect = ob.Effects[j] as BuffEffect;

                        if (effect != null && effect.BuffType == BuffType.EnergyShield)
                        {
                            effect.Clear();
                            effect.Remove();

                            ob.Effects.Add(effect = new BuffEffect(Libraries.Magic2, 1890, 6, 600, ob, true, BuffType.EnergyShield) { Repeat = false });
                            SoundManager.PlaySound(20000 + (ushort)Spell.EnergyShield * 10 + 1);

                            effect.Complete += (o, e) =>
                            {
                                ob.Effects.Add(new BuffEffect(Libraries.Magic2, 1900, 2, 800, ob, true, BuffType.EnergyShield) { Repeat = true });
                            };

                            break;
                        }
                    }
                }

                return;
            }
        }

        private void DamageIndicator(S.DamageIndicator p)
        {
            if (Settings.DisplayDamage)
            {
                for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
                {
                    MapObject obj = MapControl.Objects[i];
                    if (obj.ObjectID != p.ObjectID) continue;

                    switch (p.Type)
                    {
                        case DamageType.Hit: //add damage level colours
                            if (Settings.DamageIndicator)
                                obj.Damages.Add(new Damage(p.Damage.ToString("#,##0"), 1000, Color.DarkOrange, 50));
                            break;
                        case DamageType.MagicalHit: //add damage level colours
                            if (Settings.MagicDamageIndicator)
                                obj.Damages.Add(new Damage(p.Damage.ToString("#,##0"), 1000, Color.DarkBlue, 50));
                            break;
                        case DamageType.Miss:
                            if (Settings.MissIndicator)
                                obj.Damages.Add(new Damage("Miss", 1200, obj.Race == ObjectType.Player ? Color.Yellow : Color.White, 50));
                            break;
                        case DamageType.Critical:
                            if (Settings.CriticalIndicator)
                                obj.Damages.Add(new Damage("Crit", 1000, obj.Race == ObjectType.Player ? Color.Red : Color.Red, 50) { Offset = 15 });
                            break;
                        case DamageType.Hp:
                            if (Settings.HealIndicator)
                                obj.Damages.Add(new Damage("+" + p.Damage.ToString("#,##0"), 1000, obj.Race == ObjectType.Player ? Color.Red : Color.White, 50)); //Ice Show pot ticks
                            break;
                        case DamageType.Mp:
                            if (Settings.ManaRecovIndicator)
                                obj.Damages.Add(new Damage("+" + p.Damage.ToString("#,##0"), 1000, obj.Race == ObjectType.Player ? Color.Blue : Color.White, 50) { Offset = 15 });
                            break;
                        case DamageType.GreenPoison:
                            if (Settings.DamageIndicator)
                                obj.Damages.Add(new Damage(p.Damage.ToString("#,##0"), 1000, obj.Race == ObjectType.Player ? Color.LawnGreen : Color.GreenYellow, 50) { Offset = -15 });
                            break;
                        case DamageType.Bleeding:
                            if (Settings.DamageIndicator)
                                obj.Damages.Add(new Damage(p.Damage.ToString("#,##0"), 1000, obj.Race == ObjectType.Player ? Color.OrangeRed : Color.Red, 50) { Offset = -30 });
                            break;
                        case DamageType.Burning:
                            if (Settings.DamageIndicator)
                                obj.Damages.Add(new Damage(p.Damage.ToString("#,##0"), 1000, obj.Race == ObjectType.Player ? Color.Orange : Color.DarkOrange, 50) { Offset = -45 });
                            break;

                        case DamageType.ElementAir:
                            obj.Damages.Add(new Damage("Air Damage +" + p.Damage.ToString("#,##0"), 1000, obj.Race == ObjectType.Player ? Color.White : Color.White, 50) { Offset = 100 });
                            break;
                        case DamageType.ElementDark:
                            obj.Damages.Add(new Damage("Dark Damage +" + p.Damage.ToString("#,##0"), 1000, obj.Race == ObjectType.Player ? Color.Black : Color.Black, 50) { Offset = -100, OutLineColour = Color.White });
                            break;
                        case DamageType.ElementEarth:
                            obj.Damages.Add(new Damage("Earth Damage +" + p.Damage.ToString("#,##0"), 1000, obj.Race == ObjectType.Player ? Color.Brown : Color.Brown, 50) { Offset = 115, OutLineColour = Color.Goldenrod });
                            break;
                        case DamageType.ElementFire:
                            obj.Damages.Add(new Damage("Fire Damage +" + p.Damage.ToString("#,##0"), 1000, obj.Race == ObjectType.Player ? Color.OrangeRed : Color.OrangeRed, 50) { Offset = -115, OutLineColour = Color.LightYellow });
                            break;
                        case DamageType.ElementHoly:
                            obj.Damages.Add(new Damage("Holy Damage +" + p.Damage.ToString("#,##0"), 1000, obj.Race == ObjectType.Player ? Color.LightGoldenrodYellow : Color.LightGoldenrodYellow, 50) { Offset = 130, OutLineColour = Color.Red });
                            break;
                        case DamageType.ElementPhantom:
                            obj.Damages.Add(new Damage("Phantom Damage +" + p.Damage.ToString("#,##0"), 1000, obj.Race == ObjectType.Player ? Color.MediumPurple : Color.MediumPurple, 50) { Offset = -130, OutLineColour = Color.Green });
                            break;
                        case DamageType.ElementWater:
                            obj.Damages.Add(new Damage("Water Damage +" + p.Damage.ToString("#,##0"), 1000, obj.Race == ObjectType.Player ? Color.CadetBlue : Color.CadetBlue, 50) { Offset = 155, OutLineColour = Color.Yellow });
                            break;
                        case DamageType.ElementWind:
                            obj.Damages.Add(new Damage("Wind Damage +" + p.Damage.ToString("#,##0"), 1000, obj.Race == ObjectType.Player ? Color.Gray : Color.Gray, 50) { Offset = -155, OutLineColour = Color.White });
                            break;
                    }

                }
            }
        }

        private void DuraChanged(S.DuraChanged p)
        {
            var hero = false;
            UserItem item = null;
            for (int i = 0; i < User.Inventory.Length; i++)
                if (User.Inventory[i] != null && User.Inventory[i].UniqueID == p.UniqueID)
                {
                    item = User.Inventory[i];
                    break;
                }


            if (item == null)
                for (int i = 0; i < User.Equipment.Length; i++)
                {
                    if (User.Equipment[i] != null && User.Equipment[i].UniqueID == p.UniqueID)
                    {
                        item = User.Equipment[i];
                        break;
                    }
                    if (User.Equipment[i] != null && User.Equipment[i].Slots != null)
                    {
                        for (int j = 0; j < User.Equipment[i].Slots.Length; j++)
                        {
                            if (User.Equipment[i].Slots[j] != null && User.Equipment[i].Slots[j].UniqueID == p.UniqueID)
                            {
                                item = User.Equipment[i].Slots[j];
                                break;
                            }
                        }

                        if (item != null) break;
                    }
                }

            if (item == null && Hero != null)
            {
                for (int i = 0; i < Hero.Inventory.Length; i++)
                    if (Hero.Inventory[i] != null && Hero.Inventory[i].UniqueID == p.UniqueID)
                    {
                        item = Hero.Inventory[i];
                        hero = true;
                        break;
                    }

            }

            if (item == null && Hero != null)
            {
                for (int i = 0; i < Hero.Equipment.Length; i++)
                {
                    if (Hero.Equipment[i] != null && Hero.Equipment[i].UniqueID == p.UniqueID)
                    {
                        item = Hero.Equipment[i];
                        hero = true;
                        break;
                    }
                    if (Hero.Equipment[i] != null && Hero.Equipment[i].Slots != null)
                    {
                        for (int j = 0; j < Hero.Equipment[i].Slots.Length; j++)
                        {
                            if (Hero.Equipment[i].Slots[j] != null && Hero.Equipment[i].Slots[j].UniqueID == p.UniqueID)
                            {
                                item = Hero.Equipment[i].Slots[j];
                                hero = true;
                                break;
                            }
                        }

                        if (item != null) break;
                    }

                }
            }

            if (item == null) return;

            item.CurrentDura = p.CurrentDura;

            if (item.CurrentDura == 0)
            {
                User.RefreshStats();
                switch (item.Info.Type)
                {
                    case ItemType.Mount:
                        ChatDialog.ReceiveChat(string.Format("{0} is no longer loyal to you.", item.Info.Name), ChatType.System);
                        break;
                    case ItemType.Stone:
                        break;
                    default:
                        ChatDialog.ReceiveChat(string.Format("{0}'s dura has dropped to 0.", item.Info.Name), ChatType.System);
                        break;
                }
                
            }

            if (HoverItem == item)
            {
                DisposeItemLabel();
                CreateItemLabel(item);
            }
            
            if (hero == false)
                CharacterDuraPanel.UpdateCharacterDura(item);
            else if (Hero != null)
                HeroCharacterDuraPanel.UpdateHeroDura(item);
        }

        private void MobHealthChanged(S.MobHealthChanged p)
        {
        }

        private void HealthChanged(S.HealthChanged p)
        {
            User.HP = p.HP;
            User.MP = p.MP;

            User.PercentHealth = (byte)(User.HP / (float)User.MaxHP * 100);
        }

        private void HeroHealthChanged(S.HeroHealthChanged p)
        {
            if (User.HeroState != HeroState.Spawned || GameScene.Hero == null) return;

            GameScene.Hero.HP = p.HP;
            GameScene.Hero.MP = p.MP;

        }

        private void HeroStatsChanged(S.HeroStatsChanged p)
        {
            if (User.HeroState != HeroState.Spawned || GameScene.Hero == null) return;

            GameScene.Hero.HP = p.HP;
            GameScene.Hero.MP = p.MP;
            GameScene.Hero.MaxHP = p.MaxHP;
            GameScene.Hero.MaxMP = p.MaxMP;

        }


        private void DeleteQuestItem(S.DeleteQuestItem p)
        {
            for (int i = 0; i < User.QuestInventory.Length; i++)
            {
                UserItem item = User.QuestInventory[i];

                if (item == null || item.UniqueID != p.UniqueID) continue;

                if (item.Count == p.Count)
                    User.QuestInventory[i] = null;
                else
                    item.Count -= p.Count;
                break;
            }

            if (Hero != null)
            {
                for (int i = 0; i < Hero.QuestInventory.Length; i++)
                {
                    UserItem item = Hero.QuestInventory[i];

                    if (item == null || item.UniqueID != p.UniqueID) continue;

                    if (item.Count == p.Count)
                        Hero.QuestInventory[i] = null;
                    else
                        item.Count -= p.Count;
                    break;
                }
            }
        }

        private void DeleteItem(S.DeleteItem p)
        {
            if (Hero != null)
            {
                for (int i = 0; i < Hero.Inventory.Length; i++)
                {
                    UserItem item = Hero.Inventory[i];

                    if (item == null || item.UniqueID != p.UniqueID) continue;

                    if (item.Count == p.Count)
                        Hero.Inventory[i] = null;
                    else
                        item.Count -= p.Count;
                    break;
                }

                for (int i = 0; i < Hero.Equipment.Length; i++)
                {
                    UserItem item = Hero.Equipment[i];

                    if (item != null && item.Slots.Length > 0)
                    {
                        for (int j = 0; j < item.Slots.Length; j++)
                        {
                            UserItem slotItem = item.Slots[j];

                            if (slotItem == null || slotItem.UniqueID != p.UniqueID) continue;

                            if (slotItem.Count == p.Count)
                                item.Slots[j] = null;
                            else
                                slotItem.Count -= p.Count;
                            break;
                        }
                    }

                    if (item == null || item.UniqueID != p.UniqueID) continue;

                    if (item.Count == p.Count)
                        Hero.Equipment[i] = null;
                    else
                        item.Count -= p.Count;
                    break;
                }

            }

                for (int i = 0; i < User.Inventory.Length; i++)
                {
                    UserItem item = User.Inventory[i];

                    if (item == null || item.UniqueID != p.UniqueID) continue;

                    if (item.Count == p.Count)
                        User.Inventory[i] = null;
                    else
                        item.Count -= p.Count;
                    break;
                }

                for (int i = 0; i < User.Equipment.Length; i++)
                {
                    UserItem item = User.Equipment[i];

                    if (item != null && item.Slots.Length > 0)
                    {
                        for (int j = 0; j < item.Slots.Length; j++)
                        {
                            UserItem slotItem = item.Slots[j];

                            if (slotItem == null || slotItem.UniqueID != p.UniqueID) continue;

                            if (slotItem.Count == p.Count)
                                item.Slots[j] = null;
                            else
                                slotItem.Count -= p.Count;
                            break;
                        }
                    }

                    if (item == null || item.UniqueID != p.UniqueID) continue;

                    if (item.Count == p.Count)
                        User.Equipment[i] = null;
                    else
                        item.Count -= p.Count;
                    break;
                }
                if (CraftingDialog.Visible)
                    CraftingDialog.RefreshRecipes();
                User.RefreshStats();
            
        }
        private void Death(S.Death p)
        {
            User.Dead = true;

            User.ActionFeed.Add(new QueuedAction { Action = MirAction.Die, Direction = p.Direction, Location = p.Location });
            ShowReviveMessage = true;

            LogTime = 0;
        }
        private void ObjectDied(S.ObjectDied p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;

                switch(p.Type)
                {
                    default:
                        ob.ActionFeed.Add(new QueuedAction { Action = MirAction.Die, Direction = p.Direction, Location = p.Location });
                        ob.Dead = true;
                        if (ob.Race == ObjectType.Monster || ob.Race == ObjectType.Hero)
                        {
                            MonsterObject mob = (MonsterObject)ob;
                            if (mob.IsBoss && !mob.IsPet && mob.Dead && BossDialog.Visible)
                                BossDialog.Hide();
                        }
                        break;
                    case 1:
                        MapControl.Effects.Add(new Effect(Libraries.Magic2, 690, 10, 1000, ob.CurrentLocation));
                        ob.Remove();
                        break;
                    case 2:
                        SoundManager.PlaySound(20000 + (ushort)Spell.DarkBody * 10 + 1);
                        MapControl.Effects.Add(new Effect(Libraries.Magic2, 2600, 10, 1200, ob.CurrentLocation));
                        ob.Remove();
                        break;
                }
                return;
            }
        }
        private void ColourChanged(S.ColourChanged p)
        {
            User.NameColour = p.NameColour;
            User.OutLine_Colour = p.OutlineColour;
        }
        private void ObjectColourChanged(S.ObjectColourChanged p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                ob.NameColour = p.NameColour;
                ob.OutLine_Colour = p.OutlineColour;
                return;
            }
        }

        private void ObjectGuildNameChanged(S.ObjectGuildNameChanged p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                PlayerObject obPlayer = (PlayerObject)ob;
                obPlayer.GuildName = p.GuildName;
                return;
            }
        }

        private void GainExperience(S.GainExperience p)
        {
            OutputMessage(string.Format("Experience Gained {0}.", p.Amount));
            MapObject.User.Experience += p.Amount;
        }
        private void LevelChanged(S.LevelChanged p)
        {
            User.Level = p.Level;
            User.OrigLevel = Math.Min(ushort.MaxValue, (ushort)(p.Level * 30 / 100));
            User.Experience = p.Experience;
            User.MaxExperience = p.MaxExperience;
            User.RefreshStats();
            OutputMessage("Level Increased!");
            User.Effects.Add(new Effect(Libraries.Magic2, 1200, 20, 2000, User));
            SoundManager.PlaySound(SoundList.LevelUp);
            ChatDialog.ReceiveChat("Congratulations! You have leveled up. Your HP and MP have been restored.", ChatType.LevelUp); 
        }

        private void HeroGainExperience(S.HeroGainExperience p)
        {
            OutputMessage(string.Format("Hero Experience Gained {0}.", p.Amount));
            GameScene.Hero.Experience += p.Amount;
        }
        private void HeroLevelChanged(S.HeroLevelChanged p)
        {
            GameScene.Hero.Level = p.Level;
            GameScene.Hero.Experience = p.Experience;
            GameScene.Hero.MaxExperience = p.MaxExperience;
            OutputMessage("Hero Level Increased!");
            ChatDialog.ReceiveChat("Congratulations! Your hero has leveled up.", ChatType.LevelUp);
        }
        private void ObjectLeveled(S.ObjectLeveled p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                ob.Effects.Add(new Effect(Libraries.Magic2, 1180, 16, 2500, ob));
                SoundManager.PlaySound(SoundList.LevelUp);
                return;
            }
        }

        private void GainShieldEXP(S.GainShieldEXP p)
        {
            OutputMessage(string.Format("Shield EXP Gained {0}.", p.Amount));
        }

        private void ItemGainLevel(S.ItemGainLevel p)
        {
            UserItem item = null;

            for (int i = 0; i < User.Equipment.Length; i++)
                if (User.Equipment[i] != null && User.Equipment[i].UniqueID == p.ObjectID)
                {
                    item = User.Equipment[i];
                    break;
                }

            if (item == null) return;

            item.LvlSystem = (int)p.Level;
        }

        private void ObjectHarvest(S.ObjectHarvest p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                ob.ActionFeed.Add(new QueuedAction { Action = MirAction.Harvest, Direction = ob.Direction, Location = ob.CurrentLocation });
                return;
            }
        }
        private void ObjectHarvested(S.ObjectHarvested p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                ob.ActionFeed.Add(new QueuedAction { Action = MirAction.Skeleton, Direction = ob.Direction, Location = ob.CurrentLocation });
                return;
            }
        }
        private void ObjectNPC(S.ObjectNPC p)
        {
            NPCObject ob = new NPCObject(p.ObjectID);
            ob.Load(p);
        }
        private void NPCResponse(S.NPCResponse p)
        {
            NPCTime = 0;
            NPCDialog.Index = p.WindowImage;
            NPCDialog.NewText(p.Page);

            if (p.WindowImage != -1)
            {
                if (p.exitX != -1 && p.exitY != -1)
                    NPCDialog.CloseButton.Location = new Point(p.exitX, p.exitY);
                if (p.questX != -1 && p.questY != -1)
                    NPCDialog.QuestButton.Location = new Point(p.questX, p.questY);
            }
            else
            {
                NPCDialog.CloseButton.Location = new Point(413, 3);
                NPCDialog.QuestButton.Location = new Point((440 - 96) / 2, 224 - 30);
            }


            if (p.Page.Count > 0)
                NPCDialog.Show();
            else
                NPCDialog.Hide();

            NPCGoodsDialog.Hide();
            // BuyBackDialog.Hide();
            NPCDropDialog.Hide();
            StorageDialog.Hide();
        }
        private void NPCUpdate(S.NPCUpdate p)
        {
            GameScene.NPCID = p.NPCID; //Updates the client with the correct NPC ID if it's manually called from the client
        }
        private void DefaultNPC(S.DefaultNPC p)
        {
            GameScene.DefaultNPCID = p.ObjectID; //Updates the client with the correct Default NPC ID
        }

        private void NPCImageUpdate(S.NPCImageUpdate p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID || ob.Race != ObjectType.Merchant) continue;

                NPCObject npc = (NPCObject)ob;
                npc.Image = p.Image;
                npc.Colour = p.Colour;
                npc.Quests = GameScene.QuestInfoList.Where(c => c.NPCIndex == npc.ObjectID).ToList();

                npc.LoadLibrary();
                return;
            }
        }


        private void ObjectHide(S.ObjectHide p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                ob.ActionFeed.Add(new QueuedAction { Action = MirAction.Hide, Direction = ob.Direction, Location = ob.CurrentLocation });
                return;
            }
        }
        private void ObjectShow(S.ObjectShow p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                ob.ActionFeed.Add(new QueuedAction { Action = MirAction.Show, Direction = ob.Direction, Location = ob.CurrentLocation });
                return;
            }
        }
        private void Poisoned(S.Poisoned p)
        {
            User.Poison = p.Poison;
            if (p.Poison.HasFlag(PoisonType.Stun) || p.Poison.HasFlag(PoisonType.Frozen) || p.Poison.HasFlag(PoisonType.Paralysis) || p.Poison.HasFlag(PoisonType.LRParalysis))
            {
                    User.ClearMagic();
            }
        }
        private void ObjectPoisoned(S.ObjectPoisoned p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                ob.Poison = p.Poison;
                return;
            }
        }
        private void MapChanged(S.MapChanged p)
        {
            MapControl.FileName = Path.Combine(Settings.MapPath, p.FileName + ".map");
            MapControl.Title = p.Title;
            MapControl.MiniMap = p.MiniMap;
            MapControl.BigMap = p.BigMap;
            MapControl.Lights = p.Lights;
            MapControl.MapDarkLight = p.MapDarkLight;
            MapControl.Music = p.Music;
            MapControl.LoadMap();
            MapControl.NextAction = 0;

            User.CurrentLocation = p.Location;
            User.MapLocation = p.Location;
            MapControl.AddObject(User);

            User.Direction = p.Direction;

            User.QueuedAction = null;
            User.ActionFeed.Clear();
            User.ClearMagic();
            User.SetAction();

            GameScene.CanRun = false;

            MapControl.FloorValid = false;
            MapControl.InputDelay = CMain.Time + 400;

            MapControl.MapEvents.Clear();
            MapControl.MapEvents.AddRange(p.MapEvents);
            MapControl.QuestLocations.Clear();
            MapControl.QuestLocations.AddRange(p.QuestLocations);

        }
        private void ObjectTeleportOut(S.ObjectTeleportOut p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                Effect effect = null;

                bool playDefaultSound = true;

                switch (p.Type)
                {
                    case 1: //Yimoogi
                        {
                            effect = new Effect(Libraries.Magic2, 1300, 10, 500, ob);
                            break;
                        }
                    case 2: //RedFoxman
                        {
                            effect = new Effect(Libraries.Monsters[(ushort)Monster.RedFoxman], 243, 10, 500, ob);
                            break;
                        }
                    case 4: //MutatedManWorm
                        {
                            effect = new Effect(Libraries.Monsters[(ushort)Monster.MutatedManworm], 272, 6, 500, ob);

                            SoundManager.PlaySound(((ushort)Monster.MutatedManworm) * 10 + 7);
                            playDefaultSound = false;
                            break;
                        }
                    case 5: //WitchDoctor
                        {
                            effect = new Effect(Libraries.Monsters[(ushort)Monster.WitchDoctor], 328, 20, 1000, ob);
                            break;
                        }
                    case 6: //TurtleKing
                        {
                            effect = new Effect(Libraries.Monsters[(ushort)Monster.TurtleKing], 946, 10, 500, ob);
                            break;
                        }
                    default:
                        {
                            effect = new Effect(Libraries.Magic, 250, 10, 500, ob);
                            break;
                        }
                }

                if (effect != null)
                {
                    effect.Complete += (o, e) => ob.Remove();
                    ob.Effects.Add(effect);
                }

                if(playDefaultSound)
                    SoundManager.PlaySound(SoundList.Teleport);

                return;
            }
        }
        private void ObjectTeleportIn(S.ObjectTeleportIn p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;

                bool playDefaultSound = true;

                switch (p.Type)
                {
                    case 1: //Yimoogi
                        {
                            ob.Effects.Add(new Effect(Libraries.Magic2, 1310, 10, 500, ob));
                            break;
                        }
                    case 2: //RedFoxman
                        {
                            ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RedFoxman], 253, 10, 500, ob));
                            break;
                        }
                    case 4: //MutatedManWorm
                        {
                            ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MutatedManworm], 278, 7, 500, ob));

                            SoundManager.PlaySound(((ushort)Monster.MutatedManworm) * 10 + 7);
                            playDefaultSound = false;
                            break;
                        }
                    case 5: //WitchDoctor
                        {
                            ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WitchDoctor], 348, 20, 1000, ob));
                            break;
                        }
                    case 6: //TurtleKing
                        {
                            ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TurtleKing], 956, 10, 500, ob));
                            break;
                        }
                    default:
                        {
                            ob.Effects.Add(new Effect(Libraries.Magic, 260, 10, 500, ob));
                            break;
                        }
                }

                if(playDefaultSound)
                    SoundManager.PlaySound(SoundList.Teleport);

                return;
            }

            
        }
        private void TeleportIn()
        {
            User.Effects.Add(new Effect(Libraries.Magic, 260, 10, 500, User));
            SoundManager.PlaySound(SoundList.Teleport);
        }
        private void NPCGoods(S.NPCGoods p)
        {
            for (int i = 0; i < p.List.Count; i++)
            {
                p.List[i].Info = GetInfo(p.List[i].ItemIndex);
            }

            NPCRate = p.Rate;
            if (!NPCDialog.Visible) return;
            NPCGoodsDialog.usePearls = false;
            NPCGoodsDialog.NewGoods(p.List);
            NPCGoodsDialog.Show();


        }
        private void NPCSell()
        {
            if (!NPCDialog.Visible) return;
            NPCDropDialog.PType = PanelType.Sell;
            NPCDropDialog.Show();
        }
        private void NPCRepair(S.NPCRepair p)
        {
            NPCRate = p.Rate;
            if (!NPCDialog.Visible) return;
            NPCDropDialog.PType = PanelType.Repair;
            NPCDropDialog.Show();
        }
        private void NPCStorage()
        {
            if (NPCDialog.Visible)
                StorageDialog.Show();
        }
        private void NPCRequestInput(S.NPCRequestInput p)
        {
            MirInputBox inputBox = new MirInputBox("Please enter the required information.");
            inputBox.KeyDown += (o, e) =>
            {
                
                if (!int.TryParse(inputBox.InputTextBox.Text, out int tmpI))
                    return;
            };
            inputBox.OKButton.Click += (o1, e1) =>
            {
                Network.Enqueue(new C.NPCConfirmInput { Value = inputBox.InputTextBox.Text, NPCID = p.NPCID, PageName = p.PageName });
                inputBox.Dispose();
            };
            inputBox.Show();
        }

        private void NPCSRepair(S.NPCSRepair p)
        {
            NPCRate = p.Rate;
            if (!NPCDialog.Visible) return;
            NPCDropDialog.PType = PanelType.SpecialRepair;
            NPCDropDialog.Show();
        }

        private void NPCRefine(S.NPCRefine p)
        {
            NPCRate = p.Rate;
            if (!NPCDialog.Visible) return;
            NPCDropDialog.PType = PanelType.Refine;
            if (p.Refining)
            {
                NPCDropDialog.Hide();
                NPCDialog.Hide();
            }
            else
                NPCDropDialog.Show();
        }

        private void NPCCheckRefine(S.NPCCheckRefine p)
        {
            if (!NPCDialog.Visible) return;
            NPCDropDialog.PType = PanelType.CheckRefine;
            NPCDropDialog.Show();
        }

        private void NPCCollectRefine(S.NPCCollectRefine p)
        {
            if (!NPCDialog.Visible) return;
            NPCDialog.Hide();
        }

        private void NPCReplaceWedRing(S.NPCReplaceWedRing p)
        {
            if (!NPCDialog.Visible) return;
            NPCRate = p.Rate;
            NPCDropDialog.PType = PanelType.ReplaceWedRing;
            NPCDropDialog.Show();
        }


        private void SellItem(S.SellItem p)
        {
            MirItemCell cell = InventoryDialog.GetCell(p.UniqueID) ?? BeltDialog.GetCell(p.UniqueID);

            if (cell == null) return;

            cell.Locked = false;

            if (!p.Success) return;

            if (p.Count == cell.Item.Count)
                cell.Item = null;
            else
                cell.Item.Count -= p.Count;
            if (CraftingDialog.Visible)
                CraftingDialog.RefreshRecipes();
            User.RefreshStats();
        }
        private void RepairItem(S.RepairItem p)
        {
            MirItemCell cell = InventoryDialog.GetCell(p.UniqueID) ?? BeltDialog.GetCell(p.UniqueID);

            if (cell == null) return;

            cell.Locked = false;
        }

        private void ItemRepaired(S.ItemRepaired p)
        {
            UserItem item = null;
            for (int i = 0; i < User.Inventory.Length; i++)
                if (User.Inventory[i] != null && User.Inventory[i].UniqueID == p.UniqueID)
                {
                    item = User.Inventory[i];
                    break;
                }

            if (item == null)
                for (int i = 0; i < User.Equipment.Length; i++)
                    if (User.Equipment[i] != null && User.Equipment[i].UniqueID == p.UniqueID)
                    {
                        item = User.Equipment[i];
                        break;
                    }
            if (item == null && Hero != null)
                for (int i =0; i < Hero.Inventory.Length; i++)
                    if (Hero.Inventory[i] != null && Hero.Inventory[i].UniqueID == p.UniqueID)
                    {
                        item = Hero.Inventory[i];
                        break;
                    }
            if (item == null && Hero != null)
                for (int i = 0; i < Hero.Equipment.Length; i++)
                    if (Hero.Equipment[i] != null && Hero.Equipment[i].UniqueID == p.UniqueID)
                    {
                        item = Hero.Equipment[i];
                        break;
                    }
                if (item == null) return;

            item.MaxDura = p.MaxDura;
            item.CurrentDura = p.CurrentDura;

            if (HoverItem == item)
            {
                DisposeItemLabel();
                CreateItemLabel(item);
            }
        }

        private void ItemUpgraded(S.ItemUpgraded p) //i break gems looks here.
        {
            UserItem item = null;
            for (int i = 0; i < User.Inventory.Length; i++)
                if (User.Inventory[i] != null && User.Inventory[i].UniqueID == p.Item.UniqueID)
                {
                    item = User.Inventory[i];
                    break;
                }

            if (item == null) return;

            item.DC = p.Item.DC;
            item.MC = p.Item.MC;
            item.SC = p.Item.SC;

            item.AC = p.Item.AC;
            item.MAC = p.Item.MAC;
            item.MaxDura = p.Item.MaxDura;
            item.CriticalDamage = p.Item.CriticalDamage;
            item.CriticalRate = p.Item.CriticalRate;
            item.HpDrainRate = p.Item.HpDrainRate;
            item.Reflect = p.Item.Reflect;
            item.AttackSpeed = p.Item.AttackSpeed;
            item.Agility = p.Item.Agility;
            item.Accuracy = p.Item.Accuracy;
            item.PoisonAttack = p.Item.PoisonAttack;
            item.Freezing = p.Item.Freezing;
            item.MagicResist = p.Item.MagicResist;
            item.PoisonResist = p.Item.PoisonResist;
            item.RefinedValue = p.Item.RefinedValue;
            item.RefineAdded = p.Item.RefineAdded;
            item.SocketCount = p.Item.SocketCount;
            item.SocketAdd = p.Item.SocketAdd;

            GameScene.Scene.InventoryDialog.DisplayItemGridEffect(item.UniqueID, 0);

            //MirAnimatedControl anim = new MirAnimatedControl
            //{
            //    Animated = true,
            //    AnimationCount = 9,
            //    DisplayLocation = GameScene.Scene.InventoryDialog
            //};

            if (HoverItem == item)
            {
                DisposeItemLabel();
                CreateItemLabel(item);
            }
        }

        private void NewMagic(S.NewMagic p)
        {
            ClientMagic magic = p.Magic;

            User.Magics.Add(magic);
            User.RefreshStats();
            foreach (SkillBarDialog Bar in SkillBarDialogs)
                Bar.Update();
        }

        private void RefreshMagic(S.RefreshMagic p)
        {
            ClientMagic info = p.Magic;

            var updateMagic = User.Magics.FirstOrDefault(x => x.Spell == info.Spell);

            if (updateMagic != null)
            {
                updateMagic.Spell = info.Spell;
                updateMagic.BaseCost = info.BaseCost;
                updateMagic.LevelCost = info.LevelCost;
                updateMagic.Icon = info.Icon;

                updateMagic.Level1 = info.Level1;
                updateMagic.Level2 = info.Level2;
                updateMagic.Level3 = info.Level3;
                updateMagic.Level4 = info.Level4;
                updateMagic.Level5 = info.Level5;

                updateMagic.Need1 = info.Need1;
                updateMagic.Need2 = info.Need2;
                updateMagic.Need3 = info.Need3;
                updateMagic.Need4 = info.Need4;
                updateMagic.Need5 = info.Need5;

                updateMagic.Range = info.Range;

                updateMagic.HumUpEnable = info.HumUpEnable;
                updateMagic.OverrideHumUp = info.OverrideHumUp;
            }
           
            
            User.RefreshStats();
            foreach (SkillBarDialog Bar in SkillBarDialogs)
                Bar.Update();
        }

        private void HeroNewMagic(S.HeroNewMagic p)
        {
            ClientMagic magic = p.Magic;

            GameScene.Hero.Magics.Add(magic);
            HeroCharacterDialog.RefreshInterface();
        }

        private void RemoveMagic(S.RemoveMagic p)
        {
            if (p.Hero)
            {
                GameScene.Hero.Magics.RemoveAt(p.PlaceId);
                HeroCharacterDialog.RefreshInterface();
            }
            else
            {
                User.Magics.RemoveAt(p.PlaceId);
                User.RefreshStats();
                foreach (SkillBarDialog Bar in SkillBarDialogs)
                    Bar.Update();
            }
        }

        private void MagicLeveled(S.MagicLeveled p)
        {
            for (int i = 0; i < User.Magics.Count; i++)
            {
                ClientMagic magic = User.Magics[i];
                if (magic.Spell != p.Spell) continue;

                if (magic.Level != p.Level)
                {
                    magic.Level = p.Level;
                    User.RefreshStats();
                }

                magic.Experience = p.Experience;
                break;
            }


        }


        private void HeroMagicLeveled(S.HeroMagicLeveled p)
        {
            if (User.HasHero && Hero != null)

            for (int i = 0; i < Hero.Magics.Count; i++)
            {
                ClientMagic magic = Hero.Magics[i];
                if (magic.Spell != p.Spell) continue;

                if (magic.Level != p.Level)
                {
                    magic.Level = p.Level;
                }

                magic.Experience = p.Experience;
                break;
            }

            HeroCharacterDialog.RefreshInterface();


        }


        private void Magic(S.Magic p)
        {
            User.Spell = p.Spell;
            User.Cast = p.Cast;
            User.TargetID = p.TargetID;
            User.TargetPoint = p.Target;
            User.SpellLevel = p.Level;

            if (!p.Cast) return;

            ClientMagic magic = User.GetMagic(p.Spell);
            
            magic.CastTime = CMain.Time;
        }

        private void MagicDelay(S.MagicDelay p)
        {
            ClientMagic magic = User.GetMagic(p.Spell);
            if (Buffs.Where(o => o.Type == BuffType.SpeedyMagician).FirstOrDefault() != null)
            {
                long origDelay = p.Delay;
                int tmp = 0;
                for (int i = 0; i < Buffs.Count; i++)
                    if (Buffs[i].Type == BuffType.SpeedyMagician &&
                        tmp == 0)
                        tmp = Buffs[i].Values[0];
                if (tmp > 0)
                {
                    long reduceTime = (origDelay * tmp) / 100;
                    origDelay = origDelay - reduceTime;
                    //ReceiveChat(string.Format("Delay after {0}", delay), ChatType.System);
                    magic.Delay = origDelay;
                }
            }
            magic.Delay = p.Delay;
        }

        private void MagicCast(S.MagicCast p)
        {
            ClientMagic magic = User.GetMagic(p.Spell);
            magic.CastTime = CMain.Time;
        }

        private void ObjectMagic(S.ObjectMagic p)
        {
            if (p.SelfBroadcast == false && p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;

                QueuedAction action = new QueuedAction { Action = MirAction.Spell, Direction = p.Direction, Location = p.Location, Params = new List<object>() };
                action.Params.Add(p.Spell);
                action.Params.Add(p.TargetID);
                action.Params.Add(p.Target);
                action.Params.Add(p.Cast);
                action.Params.Add(p.Level);


                ob.ActionFeed.Add(action);
                return;
            }

        }
        private void ObjectEffect(S.ObjectEffect p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                PlayerObject player;
                switch (p.Effect)
                {
                    case SpellEffect.FatalSword:
                        ob.Effects.Add(new Effect(Libraries.Magic2, 1940, 4, 400, ob));
                        SoundManager.PlaySound(20000 + (ushort)Spell.FatalSword * 10);
                        break;
                    case SpellEffect.StormEscape:
                        ob.Effects.Add(new Effect(Libraries.Magic3, 610, 10, 600, ob));
                        SoundManager.PlaySound(SoundList.Teleport);
                        break;
                    case SpellEffect.Teleport:
                        ob.Effects.Add(new Effect(Libraries.Magic, 1600, 10, 600, ob));
                        SoundManager.PlaySound(SoundList.Teleport);
                        break;
                    case SpellEffect.Healing:
                        SoundManager.PlaySound(20000 + (ushort)Spell.Healing * 10 + 1);
                        ob.Effects.Add(new Effect(Libraries.Magic, 370, 10, 800, ob));
                        break;
                    case SpellEffect.RedMoonEvil:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RedMoonEvil], 32, 6, 400, ob) { Blend = false });
                        break;
                    case SpellEffect.TucsonGeneralEffect:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TucsonGeneral], 744, 18, 2000, ob) { Blend = true });
                        break;
                    case SpellEffect.LavaBurning:
                        ob.Effects.Add(new Effect(Libraries.Dragon, 440, 20, 1600, ob));
                        break;
                    case SpellEffect.TwinDrakeBlade:
                        ob.Effects.Add(new Effect(Libraries.Magic2, 380, 6, 800, ob));
                        break;
                   case SpellEffect.MPEater:
                        for (int j = MapControl.Objects.Count - 1; j >= 0; j--)
                        {
                            MapObject ob2 = MapControl.Objects[j];
                            if (ob2.ObjectID == p.EffectType)
                            {
                                ob2.Effects.Add(new Effect(Libraries.Magic2, 2411, 19, 1900, ob2));
                                break;
                            }
                        }
                        ob.Effects.Add(new Effect(Libraries.Magic2, 2400, 9, 900, ob));
                        SoundManager.PlaySound(20000 + (ushort)Spell.FatalSword * 10);
                        break;
                    case SpellEffect.Bleeding:
                        ob.Effects.Add(new Effect(Libraries.Magic3, 60, 3, 400, ob));
                        break;
                    case SpellEffect.Hemorrhage:
                        SoundManager.PlaySound(20000 + (ushort)Spell.Hemorrhage * 10);
                        ob.Effects.Add(new Effect(Libraries.Magic3, 0, 4, 400, ob));
                        ob.Effects.Add(new Effect(Libraries.Magic3, 28, 6, 600, ob));
                        ob.Effects.Add(new Effect(Libraries.Magic3, 46, 8, 800, ob));
                        break;
                    case SpellEffect.MagicShieldUp:
                        if (ob.Race != ObjectType.Player) return;
                        player = (PlayerObject)ob;
                        if (player.ShieldEffect != null)
                        {
                            player.ShieldEffect.Clear();
                            player.ShieldEffect.Remove();
                        }

                        player.MagicShield = true;
                        player.Effects.Add(player.ShieldEffect = new Effect(Libraries.Magic, 3890, 3, 600, ob) { Repeat = true });
                        break;
                    case SpellEffect.MagicShieldDown:
                        if (ob.Race != ObjectType.Player) return;
                        player = (PlayerObject)ob;
                        if (player.ShieldEffect != null)
                        {
                            player.ShieldEffect.Clear();
                            player.ShieldEffect.Remove();
                        }
                        player.ShieldEffect = null;
                        player.MagicShield = false;
                        break;
                    case SpellEffect.FireSprayHit:

                        Missile missile = CreateProjectile(410, Libraries.Magic, true, 6, 30, 4, p.TargetID, ob.CurrentLocation, ob, (int)p.DelayTime, false);

                        if (missile.Target != null)
                        {
                            missile.Complete += (o, e) =>
                            {
                                if (missile.Target.CurrentAction == MirAction.Dead) return;
                                missile.Target.Effects.Add(new Effect(Libraries.Magic, 570, 10, 600, missile.Target));
                                SoundManager.PlaySound(20000 + (ushort)Spell.FireSpray * 10 + 2);
                            };
                        }

                        break;
                    case SpellEffect.GreatFoxSpirit:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GreatFoxSpirit], 375 + (CMain.Random.Next(3) * 20), 20, 1400, ob));
                        SoundManager.PlaySound(((ushort)Monster.GreatFoxSpirit * 10) + 5);
                        break;
                    case SpellEffect.Entrapment:
                        ob.Effects.Add(new Effect(Libraries.Magic2, 1010, 10, 1500, ob));
                        ob.Effects.Add(new Effect(Libraries.Magic2, 1020, 8, 1200, ob));
                        break;
                    case SpellEffect.ChopChopStarEffect:
                        ob.Effects.Add(new Effect(Libraries.HeroComboSpell, 495, 24, 2200, ob));
                        break;
                    case SpellEffect.Critical:
                        ob.Effects.Add(new Effect(Libraries.CustomEffects, 0, 12, 60, ob));
                        break;
                    case SpellEffect.Reflect:
                        ob.Effects.Add(new Effect(Libraries.Effect, 580, 10, 70, ob));
                        break;
                    case SpellEffect.ElementalBarrierUp:
                        if (ob.Race != ObjectType.Player) return;
                        player = (PlayerObject)ob;
                        if (player.ElementalBarrierEffect != null)
                        {
                            player.ElementalBarrierEffect.Clear();
                            player.ElementalBarrierEffect.Remove();
                        }

                        player.ElementalBarrier = true;
                        player.Effects.Add(player.ElementalBarrierEffect = new Effect(Libraries.Magic3, 1890, 10, 2000, ob) { Repeat = true });
                        break;
                    case SpellEffect.ElementalBarrierDown:
                        if (ob.Race != ObjectType.Player) return;
                        player = (PlayerObject)ob;
                        if (player.ElementalBarrierEffect != null)
                        {
                            player.ElementalBarrierEffect.Clear();
                            player.ElementalBarrierEffect.Remove();
                        }
                        player.ElementalBarrierEffect = null;
                        player.ElementalBarrier = false;
                        player.Effects.Add(player.ElementalBarrierEffect = new Effect(Libraries.Magic3, 1910, 7, 1400, ob));
                        SoundManager.PlaySound(20000 + 131 * 10 + 5);
                        break;
                    case SpellEffect.DelayedExplosion:
                        int effectid = DelayedExplosionEffect.GetOwnerEffectID(ob.ObjectID);
                        if (effectid < 0)
                        {
                            ob.Effects.Add(new DelayedExplosionEffect(Libraries.Magic3, 1590, 8, 1200, ob, true, 0, 0));
                        }
                        else if (effectid >= 0)
                        {
                            if (DelayedExplosionEffect.effectlist[effectid].stage < p.EffectType)
                            {
                                DelayedExplosionEffect.effectlist[effectid].Remove();
                                ob.Effects.Add(new DelayedExplosionEffect(Libraries.Magic3, 1590 + ((int)p.EffectType * 10), 8, 1200, ob, true, (int)p.EffectType, 0));
                            }
                        }

                        //else
                        //    ob.Effects.Add(new DelayedExplosionEffect(Libraries.Magic3, 1590 + ((int)p.EffectType * 10), 8, 1200, ob, true, (int)p.EffectType, 0));
                        break;
                    case SpellEffect.AwakeningSuccess:
                        {
                            Effect ef = new Effect(Libraries.Magic3, 900, 16, 1600, ob, CMain.Time + p.DelayTime);
                            ef.Played += (o, e) => SoundManager.PlaySound(50002);
                            ef.Complete += (o, e) => MapControl.AwakeningAction = false;
                            ob.Effects.Add(ef);
                            ob.Effects.Add(new Effect(Libraries.Magic3, 840, 16, 1600, ob, CMain.Time + p.DelayTime) { Blend = false });
                        }
                        break;
                    case SpellEffect.AwakeningFail:
                        {
                            Effect ef = new Effect(Libraries.Magic3, 920, 9, 900, ob, CMain.Time + p.DelayTime);
                            ef.Played += (o, e) => SoundManager.PlaySound(50003);
                            ef.Complete += (o, e) => MapControl.AwakeningAction = false;
                            ob.Effects.Add(ef);
                            ob.Effects.Add(new Effect(Libraries.Magic3, 860, 9, 900, ob, CMain.Time + p.DelayTime) { Blend = false });
                        }
                        break;
                    case SpellEffect.AwakeningHit:
                        {
                            Effect ef = new Effect(Libraries.Magic3, 880, 5, 500, ob, CMain.Time + p.DelayTime);
                            ef.Played += (o, e) => SoundManager.PlaySound(50001);
                            ob.Effects.Add(ef);
                            ob.Effects.Add(new Effect(Libraries.Magic3, 820, 5, 500, ob, CMain.Time + p.DelayTime) { Blend = false });
                        }
                        break;
                    case SpellEffect.AwakeningMiss:
                        {
                            Effect ef = new Effect(Libraries.Magic3, 890, 5, 500, ob, CMain.Time + p.DelayTime);
                            ef.Played += (o, e) => SoundManager.PlaySound(50000);
                            ob.Effects.Add(ef);
                            ob.Effects.Add(new Effect(Libraries.Magic3, 830, 5, 500, ob, CMain.Time + p.DelayTime) { Blend = false });
                        }
                        break;
                    case SpellEffect.TurtleKing:
                        {
                            Effect ef = new Effect(Libraries.Monsters[(ushort)Monster.TurtleKing], CMain.Random.Next(2) == 0 ? 922 : 934, 12, 1200, ob);
                            ef.Played += (o, e) => SoundManager.PlaySound(20000 + (ushort)Spell.HellFire * 10 + 1);
                            ob.Effects.Add(ef);
                        }
                        break;
                    case SpellEffect.Behemoth:
                        {
                            MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Behemoth], 788, 10, 1500, ob.CurrentLocation));
                            MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Behemoth], 778, 10, 1500, ob.CurrentLocation, 0, true) { Blend = false });
                        }
                        break;
                    case SpellEffect.Stunned:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.StoningStatue], 632, 10, 1000, ob)
                        {
                            Repeat = p.Time > 0,
                            RepeatUntil = p.Time > 0 ? CMain.Time + p.Time : 0
                        });
                        break;
                    case SpellEffect.IcePillar:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IcePillar], 18, 8, 800, ob));
                        break;
                    #region Weapon Effects start here
                    case SpellEffect.EyeEff:
                        ob.Effects.Add(new Effect(Libraries.Magic10, 30, 21, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.PhoenixEff:
                        ob.Effects.Add(new Effect(Libraries.Magic10, 110, 40, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.ElectricExplosion:
                        ob.Effects.Add(new Effect(Libraries.Magic10, 220, 9, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.GFBHit:
                        ob.Effects.Add(new Effect(Libraries.Magic, 570, 10, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.PsnHit:
                        ob.Effects.Add(new Effect(Libraries.Magic, 770, 10, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.SFBHit:
                        ob.Effects.Add(new Effect(Libraries.Magic, 1360, 10, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.Shocking:
                        ob.Effects.Add(new Effect(Libraries.Magic, 1570, 10, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.UEnhance:
                        ob.Effects.Add(new Effect(Libraries.Magic2, 160, 15, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.TDBEff0:
                        ob.Effects.Add(new Effect(Libraries.Magic2, 210, 6, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.TDBEff1:
                        ob.Effects.Add(new Effect(Libraries.Magic2, 400, 10, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.Cake:
                        ob.Effects.Add(new Effect(Libraries.Magic2, 1430, 30, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.ExplosionEff:
                        ob.Effects.Add(new Effect(Libraries.Magic3, 1370, 7, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.RainbowEff:
                        ob.Effects.Add(new Effect(Libraries.Magic3, 1660, 10, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.GoldenBubble:
                        ob.Effects.Add(new Effect(Libraries.Magic3, 1880, 8, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.PurpleBlastEff:
                        ob.Effects.Add(new Effect(Libraries.Magic3, 2490, 7, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.GreenExplosion:
                        ob.Effects.Add(new Effect(Libraries.Magic3, 2500, 6, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.Swirling:
                        ob.Effects.Add(new Effect(Libraries.Magic3, 2710, 8, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.TrapEff:
                        ob.Effects.Add(new Effect(Libraries.Magic10, 1080, 8, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.CircleEff:
                        ob.Effects.Add(new Effect(Libraries.Magic3, 680, 24, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.PurpleExplosion:
                        ob.Effects.Add(new Effect(Libraries.Magic3, 480, 10, 800, ob) { Blend = true });
                        break;
                    case SpellEffect.CrystalBeastBlast:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalBeast], 2010, 7, 800, ob));
                        break;
                    case SpellEffect.CrystalBeastExplosion:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalBeast], 2120, 10, 800, ob));
                        break;
                    case SpellEffect.CrystalBeastSpin:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalBeast], 2100, 7, 800, ob));
                        break;
                    case SpellEffect.CrystalBeastSplash:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalBeast], 2130, 7, 800, ob));
                        break;
                    case SpellEffect.CastingBlizzard:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalBeast], 2110, 5, 800, ob));
                        break;
                    case SpellEffect.Jar2Effect:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Jar2], 624, 8, 900, ob));
                        break;
                    case SpellEffect.RestlessJarSpawn:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RestlessJar], 471, 5, 700, ob));
                        break;
                    case SpellEffect.RestlessJarSpawnOnPlayer:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RestlessJar], 512, 7, 900, ob));
                        break;
                    case SpellEffect.RestlessJarTornado:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RestlessJar], 384, 7, 900, ob));
                        break;
                    case SpellEffect.RestlessJarMultipleHit:
                        Missile mis = CreateProjectile(476, Libraries.Monsters[(ushort)Monster.RestlessJar], true, 2, 30, 0, p.TargetID , ob.CurrentLocation,ob, true);
                        break;
                    case SpellEffect.BlizzardCast:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalBeast], 2140, 13, 800, ob));
                        break;
                    case SpellEffect.CHM:
                        ob.Effects.Add(new Effect(Libraries.Magic2, 40 + (int)p.Direction * 10, 6, 600, ob));
                        break;
                    #endregion
                    case SpellEffect.MutantWeb:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlamingMutant], 320, 10, 800, ob));
                        break;
                    case SpellEffect.MutantWebHold:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlamingMutant], 330, 10, 800, ob)
                        {
                            Repeat = p.Time > 0,
                            RepeatUntil = p.Time > 0 ? CMain.Time + p.Time : 0
                        });
                        break;
                    case SpellEffect.FlyingStatueCast:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlyingStatue], 314, 20, 800, ob));
                        break;
                    case SpellEffect.FlyingStatueGreen:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlyingStatue], 352, 10, 800, ob));
                        break;
                    case SpellEffect.FlyingStatuePurple:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlyingStatue], 334, 10, 800, ob));
                        break;
                    case SpellEffect.BlackHammerCatSmash:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlackHammerCat], 736 + (int)p.Direction * 8, 8, 800, ob));
                        break;
                    case SpellEffect.KingGuardHold:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingGuard], 753, 10, 800, ob));
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingGuard], 763, 10, 800, ob));
                        break;
                    case SpellEffect.KingGuardSmash:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingGuard], 773, 10, 800, ob));
                        break;
                    case SpellEffect.FalconShield:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FalconLord], 360, 10, 900, ob) { Repeat = true, RepeatUntil = CMain.Time + p.Time });
                        break;
                    case SpellEffect.BearFlameSpin:
                        ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BearMinotaur], 360, 10, 900, ob) { Repeat = true, RepeatUntil = CMain.Time + 1500, Blend = true, Rate = 0.75F });
                        break;
                    #region Human Mob Skill Effects.
                    case SpellEffect.HumanBlizzardCast:
                        ob.Effects.Add(new Effect(Libraries.Magic2, 1540, 8, 800, ob));
                        break;
                    case SpellEffect.HumanCastBlessArm:
                        ob.Effects.Add(new Effect(Libraries.Magic, 1340, 16, 800, ob));
                        break;
                    case SpellEffect.HumanCastPoisonCloud:
                        ob.Effects.Add(new Effect(Libraries.Magic, 760, 10, 800, ob));
                        break;
                    case SpellEffect.HumanCastSoulShield:
                        ob.Effects.Add(new Effect(Libraries.Magic, 1320, 16, 800, ob));
                        break;
                    case SpellEffect.HumanCrossHalfMoon:
                        ob.Effects.Add(new Effect(Libraries.Magic2, 40 + ( (int)p.Direction * 10 ), 6, 600, ob, CMain.Time + 50));
                        break;
                    case SpellEffect.HumanCurseCast:
                        ob.Effects.Add(new Effect(Libraries.Magic2, 950, 24, 800, ob));
                        break;
                    case SpellEffect.HumanFireBall:
                        {
                            ob.Effects.Add(new Effect(Libraries.Magic, 0, 10, 800, ob));
                            MapObject tOID = MapControl.GetObject(p.TargetID);
                            if (tOID != null)
                                CreateProjectile(10, Libraries.Magic, true, 6, 30, 4, p.TargetID, ob.CurrentLocation, ob, (int)p.DelayTime, false);
                        }
                        break;
                    case SpellEffect.HumanFireWallCast:
                        ob.Effects.Add(new Effect(Libraries.Magic, 1620, 10, 800, ob));
                        break;
                    case SpellEffect.HumanFlamingSword:
                        ob.Effects.Add(new Effect(Libraries.Magic, 3480 + ( (int)p.Direction * 10 ), 6, 700, ob, CMain.Time + 50));
                        break;
                    case SpellEffect.HumanHalfMoon:
                        ob.Effects.Add(new Effect(Libraries.Magic, 2560 + ( (int)p.Direction * 10 ), 6, 600, ob));
                        break;
                    case SpellEffect.HumanMagicShield:
                        ob.Effects.Add(new Effect(Libraries.Magic, 1620, 10, 800, ob) { RepeatUntil = CMain.Time + p.Time * 1000 });
                        break;
                    case SpellEffect.HumanMeteorCast:
                        ob.Effects.Add(new Effect(Libraries.Magic2, 1590, 8, 800, ob));
                        break;
                    case SpellEffect.HumanPoisoning:
                        {
                            ob.Effects.Add(new Effect(Libraries.Magic, 760, 10, 800, ob));
                            MapObject tOID = MapControl.GetObject(p.TargetID);
                            if (tOID != null)
                                tOID.Effects.Add(new Effect(Libraries.Magic, 770, 10, 800, ob, CMain.Time + 800));
                        }
                        break;
                    case SpellEffect.HumanRage:
                        ob.Effects.Add(new Effect(Libraries.Magic3, 230, 6, 600, ob) { RepeatUntil = CMain.Time + p.Time * 1000 });
                        break;
                    case SpellEffect.HumanRepulse:
                        ob.Effects.Add(new Effect(Libraries.Magic, 900, 6, 800, ob));
                        break;
                    case SpellEffect.HumanSoulFireBall:
                        {
                            ob.Effects.Add(new Effect(Libraries.Magic, 0, 10, 800, ob));
                            MapObject tOID = MapControl.GetObject(p.TargetID);
                            if (tOID != null)
                            {
                                missile = CreateProjectile(1160, Libraries.Magic, true, 3, 30, 8, p.TargetID, ob.CurrentLocation, ob, (int)p.DelayTime, false);

                                if (missile.Target != null)
                                {
                                    missile.Complete += (o, e) =>
                                    {
                                        if (missile.Target.CurrentAction == MirAction.Dead)
                                            return;
                                        missile.Target.Effects.Add(new Effect(Libraries.Magic, 1360, 10, 600, missile.Target));
                                    };
                                }
                                break;
                            }
                        }
                        break;

                    case SpellEffect.SoulEaterSwampEffect:
                        {
                            MapObject tOID = MapControl.GetObject(p.TargetID);
                            if (tOID != null)
                            {
                                missile = CreateProjectile(620, Libraries.HeroComboSpell, true, 6, 30, 4, p.TargetID, ob.CurrentLocation, ob, (int)p.DelayTime, true);

                                if (missile.Target != null)
                                {
                                    missile.Complete += (o, e) =>
                                    {
                                        if (missile.Target.CurrentAction == MirAction.Dead)
                                            return;
                                        missile.Target.Effects.Add(new Effect(Libraries.HeroComboSpell, 780, 36, 3000, missile.Target));
                                    };
                                }
                                break;
                            }
                        }
                        break;
                    case SpellEffect.HandOfGodEffect:
                        {
                            ob.Effects.Add(new Effect(Libraries.HeroComboSpell, 1255, 24, 2100, ob));
                        }
                        break;
                    case SpellEffect.HumanThrusting:
                        ob.Effects.Add(new Effect(Libraries.Magic, 2190 + ( (int)p.Direction * 10 ), 6, 700, ob, CMain.Time + 50));
                        break;
                    case SpellEffect.HumanThunderBolt:
                        ob.Effects.Add(new Effect(Libraries.Magic2, 20, 3, 300, ob));
                        ob.Effects.Add(new Effect(Libraries.Magic2, 10, 3, 600, ob, CMain.Time + 800));
                        break;
                    case SpellEffect.HumanTwinDrakeBlade:
                        ob.Effects.Add(new Effect(Libraries.Magic2, 220 + ( (int)p.Direction * 20 ), 6, 600, ob, CMain.Time + 50));
                        ob.Effects.Add(new Effect(Libraries.Magic2, 226 + ( (int)p.Direction * 20 ), 6, 600, ob, CMain.Time + 50));
                        break;
                        #endregion
                }
                return;
            }

        }


        private Missile CreateProjectile(int baseIndex, MLibrary library, bool blend, int count, int interval, int skip, uint target, Point loc, MapObject owner, int delay, bool direction16 = true)
        {
            MapObject ob = MapControl.GetObject(target);
            Point TargetPoint = new Point();
            if (ob != null)
                TargetPoint = ob.CurrentLocation;

            int duration = Functions.MaxDistance(loc, TargetPoint) * 50;


            Missile missile = new Missile(library, baseIndex, duration / interval, duration, owner, TargetPoint, direction16)
            {
                Target = ob,
                Interval = interval,
                FrameCount = count,
                Blend = blend,
                Skip = skip,
                Delay = delay
            };

            owner.Effects.Add(missile);

            return missile;
        }

        private Missile CreateProjectile(int baseIndex, MLibrary library, bool blend, int count, int interval, int skip, uint target, Point loc, MapObject owner, bool direction16 = true)
        {
            MapObject ob = MapControl.GetObject(target);
            Point TargetPoint = new Point();
            if (ob != null)
                TargetPoint = ob.CurrentLocation;

            int duration = Functions.MaxDistance(loc, TargetPoint) * 50;


            Missile missile = new Missile(library, baseIndex, duration / interval, duration, owner, TargetPoint, direction16)
            {
                Target = ob,
                Interval = interval,
                FrameCount = count,
                Blend = blend,
                Skip = skip
            };

            owner.Effects.Add(missile);

            return missile;
        }

        private void RangeAttack(S.RangeAttack p)
        {
            User.TargetID = p.TargetID;
            User.TargetPoint = p.Target;
            User.Spell = p.Spell;
        }

        private void Pushed(S.Pushed p)
        {
            User.ActionFeed.Add(new QueuedAction { Action = MirAction.Pushed, Direction = p.Direction, Location = p.Location });
        }
        private void ObjectPushed(S.ObjectPushed p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                ob.ActionFeed.Add(new QueuedAction { Action = MirAction.Pushed, Direction = p.Direction, Location = p.Location });

                return;
            }
        }
        private void ObjectName(S.ObjectName p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                ob.Name = p.Name;
                return;
            }
        }
        private void UserStorage(S.UserStorage p)
        {

            if (Storage.Length != p.Storage.Length)
            {
                Array.Resize(array: ref Storage, newSize: p.Storage.Length);
            }
        
            Storage = p.Storage;

            for (int i = 0; i < Storage.Length; i++)
            {
                if (Storage[i] == null) continue;
                Bind(Storage[i]);
            }
        }
        private void SwitchGroup(S.SwitchGroup p)
        {
            GroupDialog.AllowGroup = p.AllowGroup;
        }
        private void DeleteGroup()
        {
            GroupDialog.GroupList.Clear();
            ChatDialog.ReceiveChat("You have left the group.", ChatType.Group);
        }
        private void DeleteMember(S.DeleteMember p)
        {
            GroupDialog.GroupList.Remove(p.Name);
            ChatDialog.ReceiveChat(string.Format("-{0} has left the group.", p.Name), ChatType.Group);
        }
        private void GroupInvite(S.GroupInvite p)
        {
            MirMessageBox messageBox = new MirMessageBox(string.Format("Do you want to group with {0}?", p.Name), MirMessageBoxButtons.YesNo);

            messageBox.YesButton.Click += (o, e) => Network.Enqueue(new C.GroupInvite { AcceptInvite = true });
            messageBox.NoButton.Click += (o, e) => Network.Enqueue(new C.GroupInvite { AcceptInvite = false });

            messageBox.Show();
        }
        private void AddMember(S.AddMember p)
        {
            GroupDialog.GroupList.Add(p.Name);
            ChatDialog.ReceiveChat(string.Format("-{0} has joined the group.", p.Name), ChatType.Group);
        }
        private void Revived()
        {
            User.SetAction();
            User.Dead = false;
            User.Effects.Add(new Effect(Libraries.Magic2, 1220, 20, 2000, User));
            SoundManager.PlaySound(SoundList.Revive);
        }
        private void ObjectRevived(S.ObjectRevived p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                if (p.Effect)
                {
                    ob.Effects.Add(new Effect(Libraries.Magic2, 1220, 20, 2000, ob));
                    SoundManager.PlaySound(SoundList.Revive);
                }
                ob.Dead = false;
                ob.ActionFeed.Clear();
                ob.ActionFeed.Add(new QueuedAction { Action = MirAction.Revive, Direction = ob.Direction, Location = ob.CurrentLocation });
                return;
            }
        }
        private void SpellToggle(S.SpellToggle p)
        {
            switch (p.Spell)
            {
                //Warrior
                case Spell.Slaying:
                    Slaying = p.CanUse;
                    break;
                case Spell.Thrusting:
                    Thrusting = p.CanUse;
                    ChatDialog.ReceiveChat(Thrusting ? "Use Thrusting." : "Do not use Thrusting.", ChatType.Hint);
                    break;
                case Spell.CrescentSlash:
                    {
                        CresSlash = p.CanUse;
                        ChatDialog.ReceiveChat(CresSlash ? "Use Crescent Slash." : "Do not use CrescentSlash.", ChatType.Hint);
                        /*if (CresSlash)
                        {
                            MapControl.NextAction = CMain.Time + 2500;
                            SpellTime = CMain.Time + 1500; //Spell Delay
                        }*/
                    }
                    break;
                case Spell.HalfMoon:
                    HalfMoon = p.CanUse;
                    ChatDialog.ReceiveChat(HalfMoon ? "Use HalfMoon." : "Do not use HalfMoon.", ChatType.Hint);
                    break;
                case Spell.CrossHalfMoon:
                    CrossHalfMoon = p.CanUse;
                    ChatDialog.ReceiveChat(CrossHalfMoon ? "Use CrossHalfMoon." : "Do not use CrossHalfMoon.", ChatType.Hint);
                    break;
                case Spell.DoubleSlash:
                    DoubleSlash = p.CanUse;
                    ChatDialog.ReceiveChat(DoubleSlash ? "Use DoubleSlash." : "Do not use DoubleSlash.", ChatType.Hint);
                    break;
                case Spell.SliceNDice:
                    SliceNDice = p.CanUse;
                    if (SliceNDice)
                        ChatDialog.ReceiveChat("Weapon is charged!", ChatType.Hint);
                    else
                        ChatDialog.ReceiveChat("Weapon has lost it's charge", ChatType.Hint);
                    break;
                case Spell.FlamingSword:
                    FlamingSword = p.CanUse;
                    if (FlamingSword)
                        ChatDialog.ReceiveChat("Your weapon is glowed by spirit of fire.", ChatType.Hint);
                    else
                        ChatDialog.ReceiveChat("The spirits of fire disappeared.", ChatType.System);
                    break;
                case Spell.BlazingSword:
                    BlazingSword = p.CanUse;
                    if (BlazingSword)
                        ChatDialog.ReceiveChat("Molten Lava pump's through your veins.", ChatType.Hint);
                    else
                        ChatDialog.ReceiveChat("The Molten rage has subsided.", ChatType.System);
                    break;
            }
        }
        private void ObjectHealth(S.ObjectHealth p)
        {
            if (p.Percent == 0) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                if (ob is MonsterObject tmp)
                {
                    if (p.MobType == 0)
                    {
                        tmp.QuestMob = false;
                        tmp.EventMob = false;
                    }
                    tmp.QuestMob = p.MobType == 1 ? true : false;
                    tmp.EventMob = p.MobType == 2 ? true : false;
                    tmp.Enhanced = p.PetEnhancer;
                }
                ob.PercentHealth = p.Percent;
                ob.HealthTime = CMain.Time + p.Expire * 1000;
                if (p.ManaPercent == -1) continue;
                ob.ManaPercent = p.ManaPercent;
                return;
            }
        }
        private void MapEffect(S.MapEffect p)
        {
            switch (p.Effect)
            {
                case SpellEffect.Mine:
                    SoundManager.PlaySound(10091);
                    Effect HitWall = new Effect(Libraries.Effect, 8 * p.Value, 3, 240, p.Location) { Light = 0 };
                    MapControl.Effects.Add(HitWall);
                    break;
                case SpellEffect.BrokenSoulCutEffect:
                    MapControl.Effects.Add(new Effect(Libraries.HeroComboSpell, 10 + p.Value * 20, 17, 2000, p.Location));
                    break;
                case SpellEffect.DragonFlamesEffect:
                    MapControl.Effects.Add(new Effect(Libraries.HeroComboSpell, 561,37, 2500, p.Location));
                    break;
                case SpellEffect.ThunderClapEffect:
                    MapControl.Effects.Add(new Effect(Libraries.HeroComboSpell, 390, 25, 2000, p.Location));
                    break;
                case SpellEffect.LastJudgementEffect:
                    MapControl.Effects.Add(new Effect(Libraries.HeroComboSpell, 230, 22, 2000, p.Location));
                    break;
            }
        }
        private void ObjectRangeAttack(S.ObjectRangeAttack p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                QueuedAction action = null;
                //  Players
                if (ob.Race == ObjectType.Player)
                {
                    //  They only have 1 type of range attack
                    switch (p.Type)
                    {
                        default:
                            {
                                action = new QueuedAction { Action = MirAction.AttackRange1, Direction = p.Direction, Location = p.Location, Params = new List<object>() };
                                break;
                            }
                    }
                }
                else
                {
                    switch (p.Type)
                    {
                        case 1:
                            {
                                action = new QueuedAction { Action = MirAction.AttackRange2, Direction = p.Direction, Location = p.Location, Params = new List<object>() };
                                break;
                            }
                        case 2:
                            {
                                action = new QueuedAction { Action = MirAction.AttackRange3, Direction = p.Direction, Location = p.Location, Params = new List<object>() };
                                break;
                            }
                        default://0 or > 3 will use the default of AttackRange1
                            {
                                action = new QueuedAction { Action = MirAction.AttackRange1, Direction = p.Direction, Location = p.Location, Params = new List<object>() };
                                break;
                            }
                    }
                }
                //  Adds the target id to be used
                action.Params.Add(p.TargetID);
                action.Params.Add(p.Target);
                //  if we need it
                action.Params.Add(p.Spell);
                ob.ActionFeed.Add(action);
                return;
            }
        }

        private void ShowMentalState(Buff buff)
        {
            if (buff.Type == BuffType.MentalState)
            {
                switch (buff.Values[0])
                {
                    case 0:
                        ChatDialog.ReceiveChat("Mentalstate: Agressive.", ChatType.Hint);
                        break;
                    case 1:
                        ChatDialog.ReceiveChat("Mentalstate: Trick shot.", ChatType.Hint);
                        break;
                    case 2:
                        ChatDialog.ReceiveChat("Mentalstate: Group mode.", ChatType.Hint);
                        break;
                }

            }
        }
        private void AddBuff(S.AddBuff p)
        {
            if (p.Type == BuffType.ShadowStepFlux)
                nextShadowStepCost = (ushort)p.Values[0];
            Buff buff = new Buff
            {
                Type = p.Type,
                Caster = p.Caster,
                Expire = CMain.Time + p.Expire,
                Values = p.Values,
                Infinite = p.Infinite,
                ObjectID = p.ObjectID,
                Visible = p.Visible,
                Hero = p.Hero
            };
            if (buff.Hero)
            {
                for (int i = 0; i < Buffs.Count; i++)
                {
                    if (Buffs[i].Type != buff.Type) continue;
                    if (Buffs[i].Hero != buff.Hero) continue;

                    Buffs[i] = buff;
                    User.RefreshStats();
                    ShowMentalState(buff);
                    return;
                }
                Buffs.Add(buff);
                CreateBuff(buff);
                User.RefreshStats();
                ShowMentalState(buff);
                //User.ProcessBuffs();
            }
            else if (buff.ObjectID == User.ObjectID)
            {
                for (int i = 0; i < Buffs.Count; i++)
                {
                    if (Buffs[i].Type != buff.Type ) continue;
                    if (Buffs[i].Hero != buff.Hero) continue;

                    Buffs[i] = buff;
                    User.RefreshStats();
                    ShowMentalState(buff);
                    return;
                }

                Buffs.Add(buff);
                CreateBuff(buff);
                User.RefreshStats();
                ShowMentalState(buff);               
            }

            if (!buff.Visible || buff.ObjectID <= 0) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != buff.ObjectID) continue;
                if ((ob is PlayerObject) || (ob is MonsterObject))
                {
                    if (!ob.Buffs.Contains(buff.Type))
                    {
                        ob.Buffs.Add(buff.Type);
                    }

                    ob.AddBuffEffect(buff.Type);
                    return;
                }
            }
        }
        private void RemoveBuff(S.RemoveBuff p)
        {
            if (p.Type == BuffType.ShadowStepFlux)
                nextShadowStepCost = 0;
            for (int i = 0; i < Buffs.Count; i++)
            {


                if (Buffs[i].Type != p.Type || 
                    Buffs[i].Hero != p.Hero) continue;

                switch (Buffs[i].Type)
                {
                    case BuffType.SwiftFeet:
                        User.Sprint = false;
                        break;
                    case BuffType.FastMove:
                        User.FastChannel = true;
                        break;
                    case BuffType.Transform:
                        User.TransformType = -1;
                        break;
                }

                Buffs.RemoveAt(i);
                BuffList[i].Dispose();
                BuffList.RemoveAt(i);
            }

            if (User.ObjectID == p.ObjectID)
                User.RefreshStats();

            if (p.ObjectID <= 0) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];

                if (ob.ObjectID != p.ObjectID) continue;

                ob.Buffs.Remove(p.Type);
                ob.RemoveBuffEffect(p.Type);
                return;
            }
        }

        private void ObjectHidden(S.ObjectHidden p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                ob.Hidden = p.Hidden;
                return;
            }
        }
        private void ObjectSneaking(S.ObjectSneaking p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
               // ob.SneakingActive = p.SneakingActive;
                return;
            }
        }

        private void ObjectLevelEffects(S.ObjectLevelEffects p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID || ob.Race != ObjectType.Player) continue;

                PlayerObject temp = (PlayerObject)ob;

                temp.LevelEffects = p.LevelEffects;

                temp.SetEffects();
                return;
            }
        }

        private void RefreshItem(S.RefreshItem p)
        {
            Bind(p.Item);

            if (SelectedCell != null && SelectedCell.Item.UniqueID == p.Item.UniqueID)
                SelectedCell = null;

            if (HoverItem != null && HoverItem.UniqueID == p.Item.UniqueID)
            {
                DisposeItemLabel();
                CreateItemLabel(p.Item);
            }

            for (int i = 0; i < User.Inventory.Length; i++)
                if (User.Inventory[i] != null && User.Inventory[i].UniqueID == p.Item.UniqueID)
                {
                    User.Inventory[i] = p.Item;
                    User.RefreshStats();
                    return;
                }

            for (int i = 0; i < User.Equipment.Length; i++)
                if (User.Equipment[i] != null && User.Equipment[i].UniqueID == p.Item.UniqueID)
                {
                    User.Equipment[i] = p.Item;
                    User.RefreshStats();
                    return;
                }

            if (Hero != null)
            {
                for (int i = 0; i < Hero.Inventory.Length; i++)
                    if (Hero.Inventory[i] != null && Hero.Inventory[i].UniqueID == p.Item.UniqueID)
                    {
                        Hero.Inventory[i] = p.Item;
                        return;
                    }

                for (int i = 0; i < User.Equipment.Length; i++)
                    if (Hero.Equipment[i] != null && Hero.Equipment[i].UniqueID == p.Item.UniqueID)
                    {
                        Hero.Equipment[i] = p.Item;
                        return;
                    }

            }

        }
        private void ObjectSpell(S.ObjectSpell p)
        {
            SpellObject ob = new SpellObject(p.ObjectID);
            ob.Load(p);
        }
        private void ObjectDeco(S.ObjectDeco p)
        {
            DecoObject ob = new DecoObject(p.ObjectID);
            ob.Load(p);
        }

        private void UserDash(S.UserDash p)
        {
            if (User.Direction == p.Direction && User.CurrentLocation == p.Location)
            {
                MapControl.NextAction = 0;
                return;
            }
            MirAction action = User.CurrentAction == MirAction.DashL ? MirAction.DashR : MirAction.DashL;
            for (int i = User.ActionFeed.Count - 1; i >= 0; i--)
            {
                if (User.ActionFeed[i].Action == MirAction.DashR)
                {
                    action = MirAction.DashL;
                    break;
                }
                if (User.ActionFeed[i].Action == MirAction.DashL)
                {
                    action = MirAction.DashR;
                    break;
                }
            }

            User.ActionFeed.Add(new QueuedAction { Action = action, Direction = p.Direction, Location = p.Location });

        }
        private void UserDashFail(S.UserDashFail p)
        {
            MapControl.NextAction = 0;
            User.ActionFeed.Add(new QueuedAction { Action = MirAction.DashFail, Direction = p.Direction, Location = p.Location });
        }
        private void ObjectDash(S.ObjectDash p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;

                MirAction action = MirAction.DashL;

                if (ob.ActionFeed.Count > 0 && ob.ActionFeed[ob.ActionFeed.Count - 1].Action == action)
                    action = MirAction.DashR;

                ob.ActionFeed.Add(new QueuedAction { Action = action, Direction = p.Direction, Location = p.Location });

                return;
            }
        }
        private void ObjectDashFail(S.ObjectDashFail p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;

                ob.ActionFeed.Add(new QueuedAction { Action = MirAction.DashFail, Direction = p.Direction, Location = p.Location });

                return;
            }
        }
        private void UserBackStep(S.UserBackStep p)
        {
            if (User.Direction == p.Direction && User.CurrentLocation == p.Location)
            {
                MapControl.NextAction = 0;
                return;
            }
            User.ActionFeed.Add(new QueuedAction { Action = MirAction.Jump, Direction = p.Direction, Location = p.Location });
        }
        private void ObjectBackStep(S.ObjectBackStep p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;

                ((PlayerObject)ob).JumpDistance = p.Distance;

                ob.ActionFeed.Add(new QueuedAction { Action = MirAction.Jump, Direction = p.Direction, Location = p.Location });

                return;
            }
        }
        private void UserDashAttack(S.UserDashAttack p)
        {
            if (User.Direction == p.Direction && User.CurrentLocation == p.Location)
            {
                MapControl.NextAction = 0;
                return;
            }
            //User.JumpDistance = p.Distance;
            User.ActionFeed.Add(new QueuedAction { Action = MirAction.DashAttack, Direction = p.Direction, Location = p.Location });
        }
        private void ObjectDashAttack(S.ObjectDashAttack p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;

                ((PlayerObject)ob).JumpDistance = p.Distance;

                ob.ActionFeed.Add(new QueuedAction { Action = MirAction.DashAttack, Direction = p.Direction, Location = p.Location });

                return;
            }
        }
        private void UserAttackMove(S.UserAttackMove p)//Warrior Skill - SlashingBurst
        {
            MapControl.NextAction = 0;
            if (User.CurrentLocation == p.Location && User.Direction == p.Direction) return;


            MapControl.RemoveObject(User);
            User.CurrentLocation = p.Location;
            User.MapLocation = p.Location;
            MapControl.AddObject(User);


            MapControl.FloorValid = false;
            MapControl.InputDelay = CMain.Time;


            if (User.Dead) return;


            User.ClearMagic();
            User.QueuedAction = null;


            for (int i = User.ActionFeed.Count - 1; i >= 0; i--)
            {
                if (User.ActionFeed[i].Action == MirAction.Pushed) continue;
                User.ActionFeed.RemoveAt(i);
            }


            User.SetAction();


            User.ActionFeed.Add(new QueuedAction { Action = MirAction.Standing, Direction = p.Direction, Location = p.Location });
        }

        private void SetConcentration(S.SetConcentration p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                User.Concentrating = p.Enabled;
                User.ConcentrateInterrupted = p.Interrupted;
                if (p.Enabled && !p.Interrupted)
                {
                    int idx = InterruptionEffect.GetOwnerEffectID(User.ObjectID);
                    if (idx < 0)
                    {
                        //    InterruptionEffect.effectlist[idx] = new InterruptionEffect(Libraries.Magic3, 1860, 8, 8 * 100, User, true);
                        //else
                        User.Effects.Add(new InterruptionEffect(Libraries.Magic3, 1860, 8, 8 * 100, User, true));
                        SoundManager.PlaySound(20000 + 129 * 10);
                    }
                }
                break;
            }
        }
        private void SetObjectConcentration(S.SetObjectConcentration p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;

                ((PlayerObject)ob).Concentrating = p.Enabled;
                ((PlayerObject)ob).ConcentrateInterrupted = p.Interrupted;

                if (p.Enabled && !p.Interrupted)
                {
                    //int idx = InterruptionEffect.GetOwnerEffectID(ob.ObjectID);
                    //if (idx < 0)
                    //    InterruptionEffect.effectlist[idx] = new InterruptionEffect(Libraries.Magic3, 1860, 8, 8 * 100, ob, true);
                    if (((PlayerObject)ob).ConcentratingEffect == null)
                    {
                        ((PlayerObject)ob).Effects.Add(((PlayerObject)ob).ConcentratingEffect = new InterruptionEffect(Libraries.Magic3, 1860, 8, 8 * 100, ob, true));
                        SoundManager.PlaySound(20000 + 129 * 10);
                    }
                }
                break;
            }
        }

        private void SetElemental(S.SetElemental p)
        {
            if (User.ObjectID != p.ObjectID) return;

            User.HasElements = p.Enabled;
            User.ElementsLevel = (int)p.Value;
            int elementType = (int)p.ElementType;
            int maxExp = (int)p.ExpLast;

            if (p.Enabled && p.ElementType > 0)
                User.Effects.Add(new ElementsEffect(Libraries.Magic3, 1630 + ((elementType - 1) * 10), 10, 10 * 100, User, true, 1 + (elementType - 1), maxExp, (elementType == 4 || elementType == 3) ? true : false));
        }
        private void SetObjectElemental(S.SetObjectElemental p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;

                ((PlayerObject)ob).HasElements = p.Enabled;
                ((PlayerObject)ob).ElementCasted = p.Casted;
                ((PlayerObject)ob).ElementsLevel = (int)p.Value;
                int elementType = (int)p.ElementType;
                int maxExp = (int)p.ExpLast;

                if (p.Enabled && p.ElementType > 0)
                    ((PlayerObject)ob).Effects.Add(new ElementsEffect(Libraries.Magic3, 1630 + ((elementType - 1) * 10), 10, 10 * 100, ob, true, 1 + (elementType - 1), maxExp));
            }
        }

        private void RemoveDelayedExplosion(S.RemoveDelayedExplosion p)
        {
            //if (p.ObjectID == User.ObjectID) return;

            int effectid = DelayedExplosionEffect.GetOwnerEffectID(p.ObjectID);
            if (effectid >= 0)
                DelayedExplosionEffect.effectlist[effectid].Remove();
        }

        private void SetBindingShot(S.SetBindingShot p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                if (ob.Race != ObjectType.Monster) continue;

                TrackableEffect NetCast = new TrackableEffect(new Effect(Libraries.MagicC, 0, 8, 700, ob))
                {
                    EffectName = "BindingShotDrop"
                };

                //TrackableEffect NetDropped = new TrackableEffect(new Effect(Libraries.ArcherMagic, 7, 1, 1000, ob, CMain.Time + 600) { Repeat = true, RepeatUntil = CMain.Time + (p.Value - 1500) });
                TrackableEffect NetDropped = new TrackableEffect(new Effect(Libraries.MagicC, 7, 1, 1000, ob) { Repeat = true, RepeatUntil = CMain.Time + (p.Value - 1500) })
                {
                    EffectName = "BindingShotDown"
                };

                TrackableEffect NetFall = new TrackableEffect(new Effect(Libraries.MagicC, 8, 8, 700, ob))
                {
                    EffectName = "BindingShotFall"
                };

                NetDropped.Complete += (o1, e1) =>
                {
                    SoundManager.PlaySound(20000 + 130 * 10 + 6);//sound M130-6
                    ob.Effects.Add(NetFall);
                };
                NetCast.Complete += (o, e) =>
                {
                    SoundManager.PlaySound(20000 + 130 * 10 + 5);//sound M130-5
                    ob.Effects.Add(NetDropped);
                };
                ob.Effects.Add(NetCast);
                break;
            }
        }

        private void GuildTerritoryPage(S.GuildTerritoryPage p)
        {
            if (!GuildTerritoryDialog.Visible)
            {
                GuildTerritoryDialog.Show();
            }

            GuildTerritoryDialog.GTMapList = p.Listings;
            GuildTerritoryDialog.Lenght = p.lenght;
            GuildTerritoryDialog.UpdateInterface();
        }

        private void SendOutputMessage(S.SendOutputMessage p)
        {
            OutputMessage(p.Message, p.Type);
        }

        private void NPCConsign()
        {
            if (!NPCDialog.Visible) return;
            NPCDropDialog.PType = PanelType.Consign;
            NPCDropDialog.Show();
        }
        private void NPCMarket(S.NPCMarket p)
        {
            for (int i = 0; i < p.Listings.Count; i++)
                Bind(p.Listings[i].Item);

            TrustMerchantDialog.Show();
            TrustMerchantDialog.UserMode = p.UserMode;
            TrustMerchantDialog.Listings = p.Listings;
            TrustMerchantDialog.Page = 0;
            TrustMerchantDialog.PageCount = p.Pages;
            TrustMerchantDialog.UpdateInterface();
        }
        private void NPCMarketPage(S.NPCMarketPage p)
        {
            if (!TrustMerchantDialog.Visible) return;

            for (int i = 0; i < p.Listings.Count; i++)
                Bind(p.Listings[i].Item);

            TrustMerchantDialog.Listings.AddRange(p.Listings);
            TrustMerchantDialog.Page = (TrustMerchantDialog.Listings.Count - 1) / 10;
            TrustMerchantDialog.UpdateInterface();
        }

        private void ConsignItem(S.ConsignItem p)
        {
            MirItemCell cell = InventoryDialog.GetCell(p.UniqueID) ?? BeltDialog.GetCell(p.UniqueID);

            if (cell == null) return;

            cell.Locked = false;

            if (!p.Success) return;

            cell.Item = null;

            User.RefreshStats();
        }
        private void MarketFail(S.MarketFail p)
        {
            TrustMerchantDialog.MarketTime = 0;
            switch (p.Reason)
            {
                case 0:
                    MirMessageBox.Show("You cannot use the TrustMerchant when dead.");
                    break;
                case 1:
                    MirMessageBox.Show("You cannot buy from the TrustMerchant without using.");
                    break;
                case 2:
                    MirMessageBox.Show("This item has already been sold.");
                    break;
                case 3:
                    MirMessageBox.Show("This item has Expired and cannot be brought.");
                    break;
                case 4:
                    MirMessageBox.Show("You do not have enough gold or credit to buy this item.");
                    break;
                case 5:
                    MirMessageBox.Show("You do not have enough weight or space spare to buy this item.");
                    break;
                case 6:
                    MirMessageBox.Show("You cannot buy your own items.");
                    break;
                case 7:
                    MirMessageBox.Show("You are too far away from the Trust Merchant.");
                    break;
                case 8:
                    MirMessageBox.Show("You cannot hold enough gold to get your sale");
                    break;
            }

        }
        private void MarketSuccess(S.MarketSuccess p)
        {
            TrustMerchantDialog.MarketTime = 0;
            MirMessageBox.Show(p.Message);
        }
        private void ObjectSitDown(S.ObjectSitDown p)
        {
            if (p.ObjectID == User.ObjectID) return;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;
                if (ob.Race != ObjectType.Monster) continue;
                ob.SitDown = p.Sitting;
                ob.ActionFeed.Add(new QueuedAction { Action = MirAction.SitDown, Direction = p.Direction, Location = p.Location });
                return;
            }
        }

        private void BaseStatsInfo(S.BaseStatsInfo p)
        {
            User.CoreStats = p.Stats;
            User.RefreshStats();
        }

        private void UserName(S.UserName p)
        {
            for (int i = 0; i < UserIdList.Count; i++)
                if (UserIdList[i].Id == p.Id)
                {
                    UserIdList[i].UserName = p.Name;
                    break;
                }
            DisposeItemLabel();
            HoverItem = null;
        }

        private void ChatItemStats(S.ChatItemStats p)
        {

        }

        private void GuildInvite(S.GuildInvite p)
        {
            MirMessageBox messageBox = new MirMessageBox(string.Format("Do you want to join the {0} guild?", p.Name), MirMessageBoxButtons.YesNo);

            messageBox.YesButton.Click += (o, e) => Network.Enqueue(new C.GuildInvite { AcceptInvite = true });
            messageBox.NoButton.Click += (o, e) => Network.Enqueue(new C.GuildInvite { AcceptInvite = false });

            messageBox.Show();
        }

        private void GuildNameRequest(S.GuildNameRequest p)
        {
            MirInputBox inputBox = new MirInputBox("Please enter a guild name, length must be 3~20 characters.");
            inputBox.InputTextBox.TextBox.KeyPress += (o, e) =>
            {
                string Allowed = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                if (!Allowed.Contains(e.KeyChar))
                    e.Handled = true;
            };
            inputBox.OKButton.Click += (o, e) =>
            {
                if (inputBox.InputTextBox.Text.Contains('\\'))
                {
                    ChatDialog.ReceiveChat("You cannot use the \\ sign in a guildname!", ChatType.System);
                    inputBox.InputTextBox.Text = "";
                }
                Network.Enqueue(new C.GuildNameReturn { Name = inputBox.InputTextBox.Text });
                inputBox.Dispose();
            };
            inputBox.Show();
        }

        private void GuildRequestWar(S.GuildRequestWar p)
        {
            MirInputBox inputBox = new MirInputBox("Please enter the guild you would like to go to war with.");

            inputBox.OKButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.GuildWarReturn { Name = inputBox.InputTextBox.Text });
                inputBox.Dispose();
            };
            inputBox.Show();
        }

        private void GuildNoticeChange(S.GuildNoticeChange p)
        {
            if (p.update == -1)
                GuildDialog.NoticeChanged = true;
            else
                GuildDialog.NoticeChange(p.notice);
        }
        private void GuildMemberChange(S.GuildMemberChange p)
        {
            switch (p.Status)
            {
                case 0: // logged of
                    GuildDialog.MemberStatusChange(p.Name, false);
                    break;
                case 1: // logged on
                    ChatDialog.ReceiveChat(String.Format("{0} logged on.", p.Name), ChatType.Guild);
                    GuildDialog.MemberStatusChange(p.Name, true);
                    break;
                case 2://new member
                    ChatDialog.ReceiveChat(String.Format("{0} joined guild.", p.Name), ChatType.Guild);
                    GuildDialog.MemberCount++;
                    GuildDialog.MembersChanged = true;
                    break;
                case 3://kicked member
                    ChatDialog.ReceiveChat(String.Format("{0} got removed from the guild.", p.Name), ChatType.Guild);
                    GuildDialog.MembersChanged = true;
                    break;
                case 4://member left
                    ChatDialog.ReceiveChat(String.Format("{0} left the guild.", p.Name), ChatType.Guild);
                    GuildDialog.MembersChanged = true;
                    break;
                case 5://rank change (name or different rank)
                    GuildDialog.MembersChanged = true;
                    break;
                case 6: //new rank
                    if (p.Ranks.Count > 0)
                        GuildDialog.NewRankRecieved(p.Ranks[0]);
                    break;
                case 7: //rank option changed
                    if (p.Ranks.Count > 0)
                        GuildDialog.RankChangeRecieved(p.Ranks[0]);
                    break;
                case 8: //my rank changed
                    if (p.Ranks.Count > 0)
                        GuildDialog.MyRankChanged(p.Ranks[0]);
                    break;
                case 255:
                    GuildDialog.NewMembersList(p.Ranks);
                    break;
            }
        }

        private void GuildStatus(S.GuildStatus p)
        {
            if ((User.GuildName == "") && (p.GuildName != ""))
            {
                GuildDialog.NoticeChanged = true;
                GuildDialog.MembersChanged = true;
            }
            if (p.GuildName == "")
            {
                GuildDialog.Hide();
            }

            if ((User.GuildName == p.GuildName) && (GuildDialog.Level < p.Level))
            {
                //guild leveled
            }
            bool GuildChange = User.GuildName != p.GuildName;
            User.GuildName = p.GuildName;
            User.GuildRankName = p.GuildRankName;
            GuildDialog.Level = p.Level;
            GuildDialog.Experience = p.Experience;
            GuildDialog.MaxExperience = p.MaxExperience;
            GuildDialog.Gold = p.Gold;
            RealguildGold = Math.Min(uint.MaxValue, p.Gold * 30 / 100);
            GuildDialog.SparePoints = p.SparePoints;
            GuildDialog.MemberCount = p.MemberCount;
            GuildDialog.MaxMembers = p.MaxMembers;
            GuildDialog.Voting = p.Voting;
            GuildDialog.ItemCount = p.ItemCount;
            GuildDialog.BuffCount = p.BuffCount;
            GuildDialog.StatusChanged(p.MyOptions);
            GuildDialog.MyRankId = p.MyRankId;
            GuildDialog.UpdateMembers();
            GuildDialog.WarsRank.Text = p.WarPoints.ToString();
            GuildDialog.WarsWins.Text = p.WarWins.ToString();
            GuildDialog.WarsLoses.Text = p.WarLoses.ToString();
            GuildDialog.WarsGuildName.Text = p.EnemyGuild;
            if (p.MyRankId == 0)
            {
                GuildDialog.TaxRateBox.ReadOnly = false;
                GuildDialog.AdjustTaxButton.Visible = true;
            }
            else
            {
                GuildDialog.TaxRateBox.ReadOnly = true;
                GuildDialog.AdjustTaxButton.Visible = false;
            }
            GuildDialog.TaxRateBox.Text = p.TaxRate.ToString();
            //reset guildbuffs
            if (GuildChange)
            {
                GuildDialog.EnabledBuffs.Clear();
                GuildDialog.UpdateActiveStats();
                RemoveBuff(new S.RemoveBuff { ObjectID = User.ObjectID, Type = BuffType.Guild });
                User.RefreshStats();
            }
        }

        private void GuildExpGain(S.GuildExpGain p)
        {
            //OutputMessage(string.Format("Guild Experience Gained {0}.", p.Amount));
            GuildDialog.Experience += p.Amount;
        }

        private void GuildStorageGoldChange(S.GuildStorageGoldChange p)
        {
            switch (p.Type)
            {
                case 0:
                    ChatDialog.ReceiveChat(String.Format("{0} donated {1} gold to guild funds.", p.Name, p.Amount), ChatType.Guild);
                    GuildDialog.Gold += p.Amount;
                    RealguildGold = Math.Min(uint.MaxValue, GuildDialog.Gold * 30 /100);
                    break;
                case 1:
                    ChatDialog.ReceiveChat(String.Format("{0} retrieved {1} gold from guild funds.", p.Name, p.Amount), ChatType.Guild);
                    if (GuildDialog.Gold > p.Amount)
                        GuildDialog.Gold -= p.Amount;
                    else
                        GuildDialog.Gold = 0;
                    RealguildGold = Math.Min(uint.MaxValue, GuildDialog.Gold * 30 / 100);
                    break;
                case 2:
                    if (GuildDialog.Gold > p.Amount)
                        GuildDialog.Gold -= p.Amount;
                    else
                        GuildDialog.Gold = 0;
                    RealguildGold = Math.Min(uint.MaxValue, GuildDialog.Gold * 30 / 100);
                    break;
                case 3:
                    GuildDialog.Gold += p.Amount;
                    RealguildGold = Math.Min(uint.MaxValue, GuildDialog.Gold * 30 / 100);
                    break;
            }
        }

        private void GuildStorageItemChange(S.GuildStorageItemChange p)
        {
            MirItemCell fromCell = null;
            MirItemCell toCell = null;
            switch (p.Type)
            {
                case 0://store
                    toCell = GuildDialog.StorageGrid[p.To];

                    if (toCell == null) return;

                    toCell.Locked = false;
                    toCell.Item = p.Item.Item;
                    Bind(toCell.Item);
                    if (p.User != User.Id) return;
                    fromCell = p.From < User.BeltIdx ? BeltDialog.Grid[p.From] : InventoryDialog.Grid[p.From - User.BeltIdx];
                    fromCell.Locked = false;
                    if (fromCell != null)
                        fromCell.Item = null;
                    User.RefreshStats();
                    break;
                case 1://retrieve


                    fromCell = GuildDialog.StorageGrid[p.From];

                    if (fromCell == null) return;
                    fromCell.Locked = false;

                    if (p.User != User.Id)
                    {
                        fromCell.Item = null;
                        return;
                    }
                    toCell = p.To < User.BeltIdx ? BeltDialog.Grid[p.To] : InventoryDialog.Grid[p.To - User.BeltIdx];
                    if (toCell == null) return;
                    toCell.Locked = false;
                    toCell.Item = fromCell.Item;
                    fromCell.Item = null;
                    break;

                case 2:
                    toCell = GuildDialog.StorageGrid[p.To];
                    fromCell = GuildDialog.StorageGrid[p.From];

                    if (toCell == null || fromCell == null) return;

                    toCell.Locked = false;
                    fromCell.Locked = false;
                    fromCell.Item = toCell.Item;
                    toCell.Item = p.Item.Item;
                    
                    Bind(toCell.Item);
                    if (fromCell.Item != null) 
                        Bind(fromCell.Item);

                    break;

                case 3://failstore
                    fromCell = p.From < User.BeltIdx ? BeltDialog.Grid[p.From] : InventoryDialog.Grid[p.From - User.BeltIdx];

                    toCell = GuildDialog.StorageGrid[p.To];

                    if (toCell == null || fromCell == null) return;

                    toCell.Locked = false;
                    fromCell.Locked = false;
                    break;
                case 4://failretrieve
                    toCell = p.From < User.BeltIdx ? BeltDialog.Grid[p.From] : InventoryDialog.Grid[p.From - User.BeltIdx];

                    fromCell = GuildDialog.StorageGrid[p.To];

                    if (toCell == null || fromCell == null) return;

                    toCell.Locked = false;
                    fromCell.Locked = false;
                    break;
                case 5://failmove
                    
                    if (toCell == null || fromCell == null) return;

                    GuildDialog.StorageGrid[p.From].Locked = false;
                    GuildDialog.StorageGrid[p.To].Locked = false;
                    break;
            }
        }
        private void GuildStorageList(S.GuildStorageList p)
        {
            for (int i = 0; i < p.Items.Length; i++)
            {
                if (i >= GuildDialog.StorageGrid.Length) break;
                if (p.Items[i] == null)
                {
                    GuildDialog.StorageGrid[i].Item = null;
                    continue;
                }
                GuildDialog.StorageGrid[i].Item = p.Items[i].Item;
                Bind(GuildDialog.StorageGrid[i].Item);
            }
        }

        private void MarriageRequest(S.MarriageRequest p)
        {
            MirMessageBox messageBox = new MirMessageBox(string.Format("{0} has asked for your hand in marriage.", p.Name), MirMessageBoxButtons.YesNo);

            messageBox.YesButton.Click += (o, e) => Network.Enqueue(new C.MarriageReply { AcceptInvite = true });
            messageBox.NoButton.Click += (o, e) => { Network.Enqueue(new C.MarriageReply { AcceptInvite = false }); messageBox.Dispose(); };

            messageBox.Show();
        }

        private void DivorceRequest(S.DivorceRequest p)
        {
            MirMessageBox messageBox = new MirMessageBox(string.Format("{0} has requested a divorce", p.Name), MirMessageBoxButtons.YesNo);

            messageBox.YesButton.Click += (o, e) => Network.Enqueue(new C.DivorceReply { AcceptInvite = true });
            messageBox.NoButton.Click += (o, e) => { Network.Enqueue(new C.DivorceReply { AcceptInvite = false }); messageBox.Dispose(); };

            messageBox.Show();
        }

        private void MentorRequest(S.MentorRequest p)
        {
            MirMessageBox messageBox = new MirMessageBox(string.Format("{0} (Level {1}) has requested you teach him the ways of the {2}.", p.Name, p.Level, GameScene.User.Class.ToString()), MirMessageBoxButtons.YesNo);

            messageBox.YesButton.Click += (o, e) => Network.Enqueue(new C.MentorReply { AcceptInvite = true });
            messageBox.NoButton.Click += (o, e) => { Network.Enqueue(new C.MentorReply { AcceptInvite = false }); messageBox.Dispose(); };

            messageBox.Show();
        }

        private bool UpdateGuildBuff(GuildBuff buff, bool Remove = false)
        {
            for (int i = 0; i < GuildDialog.EnabledBuffs.Count; i++)
            {
                if (GuildDialog.EnabledBuffs[i].Id == buff.Id)
                {
                    if (Remove)
                    {
                        GuildDialog.EnabledBuffs.RemoveAt(i);
                    }
                    else
                        GuildDialog.EnabledBuffs[i] = buff;
                    return true;
                }
            }
            return false;
        }

        private void GuildBuffList(S.GuildBuffList p)
        {
            //getting the list of all guildbuffs on server?
            if (p.GuildBuffs.Count > 0)
                GuildDialog.GuildBuffInfos.Clear();
            for (int i = 0; i < p.GuildBuffs.Count; i++)
            {
                GuildDialog.GuildBuffInfos.Add(p.GuildBuffs[i]);
            }
            //getting the list of all active/removedbuffs?
            for (int i = 0; i < p.ActiveBuffs.Count; i++)
            {
                //if (p.ActiveBuffs[i].ActiveTimeRemaining > 0)
                //    p.ActiveBuffs[i].ActiveTimeRemaining = Convert.ToInt32(CMain.Time / 1000) + (p.ActiveBuffs[i].ActiveTimeRemaining * 60);
                if (UpdateGuildBuff(p.ActiveBuffs[i], p.Remove == 1)) continue;
                if (!(p.Remove == 1))
                {
                    GuildDialog.EnabledBuffs.Add(p.ActiveBuffs[i]);
                    //CreateGuildBuff(p.ActiveBuffs[i]);
                }
            }

            for (int i = 0; i < GuildDialog.EnabledBuffs.Count; i++)
            {
                if (GuildDialog.EnabledBuffs[i].Info == null)
                {
                    GuildDialog.EnabledBuffs[i].Info = GuildDialog.FindGuildBuffInfo(GuildDialog.EnabledBuffs[i].Id);
                }
            }

            Buff buff = Buffs.FirstOrDefault(e => e.Type == BuffType.Guild);

            if (GuildDialog.EnabledBuffs.Any(e => e.Active))
            {
                if (buff == null)
                {
                    buff = new Buff { Type = BuffType.Guild, ObjectID = User.ObjectID, Caster = "Guild", Infinite = true };

                    Buffs.Add(buff);
                    CreateBuff(buff);
                }

                GuildDialog.UpdateActiveStats();
            }
            else
            {
                RemoveBuff(new S.RemoveBuff { ObjectID = User.ObjectID, Type = BuffType.Guild });
            }

            User.RefreshStats();
        }

        private void TradeRequest(S.TradeRequest p)
        {
            MirMessageBox messageBox = new MirMessageBox(string.Format("Player {0} has requested to trade with you.", p.Name), MirMessageBoxButtons.YesNo);

            messageBox.YesButton.Click += (o, e) => Network.Enqueue(new C.TradeReply { AcceptInvite = true });
            messageBox.NoButton.Click += (o, e) => { Network.Enqueue(new C.TradeReply { AcceptInvite = false }); messageBox.Dispose(); };

            messageBox.Show();
        }
        private void TradeAccept(S.TradeAccept p)
        {
            GuestTradeDialog.GuestName = p.Name;
            TradeDialog.TradeAccept();
        }
        private void TradeGold(S.TradeGold p)
        {
            GuestTradeDialog.GuestGold = p.Amount;
            TradeDialog.ChangeLockState(false);
            TradeDialog.RefreshInterface();
        }
        private void TradeItem(S.TradeItem p)
        {
            GuestTradeDialog.GuestItems = p.TradeItems;
            TradeDialog.ChangeLockState(false);
            TradeDialog.RefreshInterface();
        }
        private void TradeConfirm()
        {
            TradeDialog.TradeReset();
        }
        private void TradeCancel(S.TradeCancel p)
        {
            if (p.Unlock)
            {
                TradeDialog.ChangeLockState(false);
            }
            else
            {
                TradeDialog.TradeReset();

                MirMessageBox messageBox = new MirMessageBox("Deal cancelled.\r\nTo deal correctly you must face the other party.", MirMessageBoxButtons.OK);
                messageBox.Show();
            }
        }
        private void NPCAwakening()
        {
            if (NPCAwakeDialog.Visible != true)
                NPCAwakeDialog.Show();
        }
        private void NPCDisassemble()
        {
            if (!NPCDialog.Visible) return;
            NPCDropDialog.PType = PanelType.Disassemble;
            NPCDropDialog.Show();
        }
        private void NPCDowngrade()
        {
            if (!NPCDialog.Visible) return;
            NPCDropDialog.PType = PanelType.Downgrade;
            NPCDropDialog.Show();
        }
        private void NPCReset()
        {
            if (!NPCDialog.Visible) return;
            NPCDropDialog.PType = PanelType.Reset;
            NPCDropDialog.Show();
        }
        private void AwakeningNeedMaterials(S.AwakeningNeedMaterials p)
        {
            NPCAwakeDialog.setNeedItems(p.Materials, p.MaterialsCount);
        }
        private void AwakeningLockedItem(S.AwakeningLockedItem p)
        {
            MirItemCell cell = InventoryDialog.GetCell(p.UniqueID);
            if (cell != null)
                cell.Locked = p.Locked;
        }
        private void Awakening(S.Awakening p)
        {
            if (NPCAwakeDialog.Visible)
                NPCAwakeDialog.Hide();
            if (InventoryDialog.Visible)
                InventoryDialog.Hide();

            MirItemCell cell = InventoryDialog.GetCell((ulong)p.removeID);
            if (cell != null)
            {
                cell.Locked = false;
                cell.Item = null;
            }

            for (int i = 0; i < InventoryDialog.Grid.Length; i++)
            {
                if (InventoryDialog.Grid[i].Locked == true)
                {
                    InventoryDialog.Grid[i].Locked = false;

                    //if (InventoryDialog.Grid[i].Item.UniqueID == (ulong)p.removeID)
                    //{
                    //    InventoryDialog.Grid[i].Item = null;
                    //}
                }
            }

            for (int i = 0; i < NPCAwakeDialog.ItemsIdx.Length; i++)
            {
                NPCAwakeDialog.ItemsIdx[i] = 0;
            }

            MirMessageBox messageBox = null;

            switch (p.result)
            {
                case -4:
                    messageBox = new MirMessageBox("You have not supplied enough materials.", MirMessageBoxButtons.OK);
                    MapControl.AwakeningAction = false;
                    break;
                case -3:
                    messageBox = new MirMessageBox("You do not have enough gold.", MirMessageBoxButtons.OK);
                    MapControl.AwakeningAction = false;
                    break;
                case -2:
                    messageBox = new MirMessageBox("Awakening already at maximum level.", MirMessageBoxButtons.OK);
                    MapControl.AwakeningAction = false;
                    break;
                case -1:
                    messageBox = new MirMessageBox("Cannot awaken this item.", MirMessageBoxButtons.OK);
                    MapControl.AwakeningAction = false;
                    break;
                case 0:
                    //messageBox = new MirMessageBox("Upgrade Failed.", MirMessageBoxButtons.OK);
                    break;
                case 1:
                    //messageBox = new MirMessageBox("Upgrade Success.", MirMessageBoxButtons.OK);
                    break;

            }

            if (messageBox != null) messageBox.Show();
        }

        private void ReceiveMail(S.ReceiveMail p)
        {
            NewMail = false;
            NewMailCounter = 0;
            User.Mail.Clear();

            User.Mail = p.Mail.OrderByDescending(e => !e.Locked).ThenByDescending(e => e.DateSent).ToList();

            foreach(ClientMail mail in User.Mail)
            {
                foreach(UserItem itm in mail.Items)
                {
                    Bind(itm);
                }
            }

            //display new mail received
            if (User.Mail.Any(e => e.Opened == false))
            {
                NewMail = true;
            }

            GameScene.Scene.MailListDialog.UpdateInterface();
        }

        private void MailLockedItem(S.MailLockedItem p)
        {
            MirItemCell cell = InventoryDialog.GetCell(p.UniqueID);
            if (cell != null)
                cell.Locked = p.Locked;
        }

        private void MailSendRequest(S.MailSendRequest p)
        {
            MirInputBox inputBox = new MirInputBox("Please enter the name of the person you would like to mail.");

            inputBox.OKButton.Click += (o1, e1) =>
            {
                GameScene.Scene.MailComposeParcelDialog.ComposeMail(inputBox.InputTextBox.Text);
                GameScene.Scene.InventoryDialog.Show();

                //open letter dialog, pass in name
                inputBox.Dispose();
            };

            inputBox.Show();
        }

        private void MailSent(S.MailSent p)
        {
            for (int i = 0; i < InventoryDialog.Grid.Length; i++)
            {
                if (InventoryDialog.Grid[i].Locked == true)
                {
                    InventoryDialog.Grid[i].Locked = false;
                }
            }

            for (int i = 0; i < BeltDialog.Grid.Length; i++)
            {
                if (BeltDialog.Grid[i].Locked == true)
                {
                    BeltDialog.Grid[i].Locked = false;
                }
            }

            GameScene.Scene.MailComposeParcelDialog.Hide();
        }

        private void ParcelCollected(S.ParcelCollected p)
        {
            switch(p.Result)
            {
                case -1:
                    MirMessageBox messageBox = new MirMessageBox(string.Format("No parcels to collect."), MirMessageBoxButtons.OK);
                    messageBox.Show();
                    break;
                case 0:
                    messageBox = new MirMessageBox(string.Format("All parcels have been collected."), MirMessageBoxButtons.OK);
                    messageBox.Show();
                    break;
                case 1:
                    GameScene.Scene.MailReadParcelDialog.Hide();
                    break;
            }
        }

        private void ResizeInventory(S.ResizeInventory p)
        {
            Array.Resize(ref User.Inventory, p.Size);
            InventoryDialog.RefreshInventory2();
        }

        private void ResizeHeroInventory(S.ResizeHeroInventory p)
        {
            if (GameScene.Hero != null)
            {
                Array.Resize(ref Hero.Inventory, p.Size);
                HeroInventoryDialog.ShowBag();
            }
        }

        private void ComboHero(S.ComboHero p)
        {
            if (GameScene.Hero == null || GameScene.User.HeroState != HeroState.Spawned) return;

            GameScene.Scene.ComboBarBialog.HeroTime = CMain.Time + p.Time;
            GameScene.Scene.ComboBarBialog.HeroDuration = p.Time;

        }

        private void ComboStance(S.ComboHero p)
        {
            if (GameScene.Hero == null || GameScene.User.HeroState != HeroState.Spawned) return;

            GameScene.Scene.ComboBarBialog.HeroTime = CMain.Time + p.Time;
            GameScene.Scene.ComboBarBialog.HeroDuration = p.Time;

        }

        private void ComboStance(S.ComboStance p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;

                PlayerObject pob = ob as PlayerObject;

                pob.comboSpell.Spell = p.spell;
                pob.comboSpell.Delay = CMain.Time + p.Time;
                pob.comboSpell.Duration = CMain.Time + p.Time;

                switch(p.spell)
                {
                    case Spell.DragonFlames:
                        ob.Effects.Add(new Effect(Libraries.HeroComboSpell, 600, 8, 800, ob) {Repeat = true, RepeatUntil = CMain.Time + p.Time});
                        break;
                    case Spell.ThunderClap:
                        ob.Effects.Add(new Effect(Libraries.HeroComboSpell, 420, 16, 1600, ob) { Repeat = true, RepeatUntil = CMain.Time + p.Time });
                        break;
                    case Spell.LastJudgement:
                        ob.Effects.Add(new Effect(Libraries.HeroComboSpell, 180, 6, 800, ob) { Repeat = true, RepeatUntil = CMain.Time + p.Time });
                        break;
                    case Spell.BrokenSoulCut:
                        ob.Effects.Add(new Effect(Libraries.HeroComboSpell, 190, 10, 1200, ob) { Repeat = true, RepeatUntil = CMain.Time + p.Time });
                        break;
                    case Spell.ChopChopStar:
                        ob.Effects.Add(new Effect(Libraries.HeroComboSpell, 520, 16, 1600, ob) { Repeat = true, RepeatUntil = CMain.Time + p.Time });
                        break;
                    case Spell.SoulEaterSwamp:
                        ob.Effects.Add(new Effect(Libraries.HeroComboSpell, 820, 10, 1200, ob) { Repeat = true, RepeatUntil = CMain.Time + p.Time });
                        break;
                    case Spell.HandOfGod:
                        ob.Effects.Add(new Effect(Libraries.HeroComboSpell, 1243, 8, 1200, ob) { Repeat = true, RepeatUntil = CMain.Time + p.Time });
                        break;
                    case Spell.SoulReaper:
                        ob.Effects.Add(new Effect(Libraries.HeroComboSpell, 887, 6, 600, ob) { Repeat = true, RepeatUntil = CMain.Time + p.Time });
                        break;
                }

            }
        }

        private void ResizeStorage(S.ResizeStorage p)
        {
            User.AddedStorage = true;

            Array.Resize(ref Storage, p.Size);
            StorageDialog.RefreshStorage2();
        }

        private void SetHumUp(S.SetHumUp p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;

                PlayerObject pob = ob as PlayerObject;

                pob.HumUp = p.HumUp;
                pob.SetLibraries();
            }
        }

        private void SetAutoPot(S.SetAutoPot p)
        {
            Hero.AllowAutoPot = p.AutoPot;
            HeroInventoryDialog.AutoPotUpdate();
        }

        private void SetHero(S.SetHero p)
        {
            User.HasHero = p.setHero;
            MainDialog.SetBaseImage();
        }

        private void SetHeroSpawned(S.SetHeroSpawned p)
        {
            if (User == null) return;
            User.HeroState = p.setHeroSpawned;

            if (User.HeroState == HeroState.Spawned)
                GameScene.Scene.HeroBeltDialog.setVisible(true);
            else
                GameScene.Scene.HeroBeltDialog.setVisible(false);

            if (User.HeroState == HeroState.Unspawned)
            {
                GameScene.Scene.HeroCharacterDialog.Hide();
                GameScene.Scene.HeroInventoryDialog.Hide();
            }
        }


        private void SetHeroLocked(S.SetHeroLocked p)
        {
            if (User == null) return;
            if (!User.HasHero || Hero == null) return;

            Hero.isLocked = p.isLocked;

        }

        private void MailCost(S.MailCost p)
        {
            if(GameScene.Scene.MailComposeParcelDialog.Visible)
            {
                if (p.Cost > 0)
                    SoundManager.PlaySound(SoundList.Gold);

                Scene.MailComposeParcelDialog.ParcelCostLabel.Text = p.Cost.ToString();
            }
        }

        public void AddQuestItem(UserItem item,bool hero)
        {
            Redraw();

            if (item.Info.StackSize > 1) //Stackable
            {
                if (!hero)
                {
                    for (int i = 0; i < User.QuestInventory.Length; i++)
                    {
                        UserItem temp = User.QuestInventory[i];
                        if (temp == null || item.Info != temp.Info || temp.Count >= temp.Info.StackSize) continue;

                        if (item.Count + temp.Count <= temp.Info.StackSize)
                        {
                            temp.Count += item.Count;
                            return;
                        }
                        item.Count -= temp.Info.StackSize - temp.Count;
                        temp.Count = temp.Info.StackSize;
                    }
                }
                else if (GameScene.Hero != null)
                {  for (int i = 0; i < GameScene.Hero.QuestInventory.Length; i++)
                {
                    UserItem temp = GameScene.Hero.QuestInventory[i];
                    if (temp == null || item.Info != temp.Info || temp.Count >= temp.Info.StackSize) continue;

                    if (item.Count + temp.Count <= temp.Info.StackSize)
                    {
                        temp.Count += item.Count;
                        return;
                    }
                    item.Count -= temp.Info.StackSize - temp.Count;
                    temp.Count = temp.Info.StackSize;
                }
                }
            }

            if (!hero)
            {

                for (int i = 0; i < User.QuestInventory.Length; i++)
                {
                    if (User.QuestInventory[i] != null) continue;
                    User.QuestInventory[i] = item;
                    return;
                }

            }
            else if (GameScene.Hero != null)
            {

                for (int i = 0; i < GameScene.Hero.QuestInventory.Length; i++)
                {
                    if (GameScene.Hero.QuestInventory[i] != null) continue;
                    GameScene.Hero.QuestInventory[i] = item;
                    return;
                }
            }
        }

        private void RequestReincarnation()
        {
            if (CMain.Time > User.DeadTime && User.CurrentAction == MirAction.Dead)
            {
                MirMessageBox messageBox = new MirMessageBox("Would you like to be revived?", MirMessageBoxButtons.YesNo);

                messageBox.YesButton.Click += (o, e) => Network.Enqueue(new C.AcceptReincarnation());

                messageBox.Show();
            }
        }

        private void NewIntelligentCreature(S.NewIntelligentCreature p)
        {
            User.IntelligentCreatures.Add(p.Creature);

            MirInputBox inputBox = new MirInputBox("Please give your creature a name.");
            inputBox.InputTextBox.Text = GameScene.User.IntelligentCreatures[User.IntelligentCreatures.Count-1].CustomName;
            inputBox.OKButton.Click += (o1, e1) =>
            {
                if (IntelligentCreatureDialog.Visible) IntelligentCreatureDialog.Update();//refresh changes
                GameScene.User.IntelligentCreatures[User.IntelligentCreatures.Count - 1].CustomName = inputBox.InputTextBox.Text;
                Network.Enqueue(new C.UpdateIntelligentCreature { Creature = GameScene.User.IntelligentCreatures[User.IntelligentCreatures.Count - 1] });
                inputBox.Dispose();
            };
            inputBox.Show();
        }

        private void UpdateIntelligentCreatureList(S.UpdateIntelligentCreatureList p)
        {
            User.CreatureSummoned = p.CreatureSummoned;
            User.SummonedCreatureType = p.SummonedCreatureType;
            User.PearlCount = p.PearlCount;
            if (p.CreatureList.Count != User.IntelligentCreatures.Count)
            {
                User.IntelligentCreatures.Clear();
                for (int i = 0; i < p.CreatureList.Count; i++)
                    User.IntelligentCreatures.Add(p.CreatureList[i]);

                for (int i = 0; i < IntelligentCreatureDialog.CreatureButtons.Length; i++)
                    IntelligentCreatureDialog.CreatureButtons[i].Clear();

                IntelligentCreatureDialog.Hide();
            }
            else
            {
                for (int i = 0; i < p.CreatureList.Count; i++)
                    User.IntelligentCreatures[i] = p.CreatureList[i];
                if (IntelligentCreatureDialog.Visible) IntelligentCreatureDialog.Update();
            }
        }

        private void IntelligentCreatureEnableRename(S.IntelligentCreatureEnableRename p)
        {
            IntelligentCreatureDialog.CreatureRenameButton.Visible = true;
            if (IntelligentCreatureDialog.Visible) IntelligentCreatureDialog.Update();
        }

        private void IntelligentCreaturePickup(S.IntelligentCreaturePickup p)
        {
            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != p.ObjectID) continue;

                MonsterObject monOb = (MonsterObject)ob;

                if (monOb != null) monOb.PlayPickupSound();
            }
        }

        private void NPCPearlGoods(S.NPCPearlGoods p)
        {
            for (int i = 0; i < p.List.Count; i++)
            {
                p.List[i].Info = GetInfo(p.List[i].ItemIndex);
            }

            NPCRate = p.Rate;
            if (!NPCDialog.Visible) return;
            NPCGoodsDialog.usePearls = true;
            NPCGoodsDialog.NewGoods(p.List);
            NPCGoodsDialog.Show();
        }

        private void FriendUpdate(S.FriendUpdate p)
        {
            GameScene.Scene.FriendDialog.Friends = p.Friends;

            if (GameScene.Scene.FriendDialog.Visible)
            {
                GameScene.Scene.FriendDialog.Update(false);
            }
        }

        private void LoverUpdate(S.LoverUpdate p)
        {
            GameScene.Scene.RelationshipDialog.LoverName = p.Name;
            GameScene.Scene.RelationshipDialog.Date = p.Date;
            GameScene.Scene.RelationshipDialog.MapName = p.MapName;
            GameScene.Scene.RelationshipDialog.MarriedDays = p.MarriedDays;
            GameScene.Scene.RelationshipDialog.UpdateInterface();
        }

        private void MentorUpdate(S.MentorUpdate p)
        {
            GameScene.Scene.MentorDialog.MentorName = p.Name;
            GameScene.Scene.MentorDialog.MentorLevel = p.Level;
            GameScene.Scene.MentorDialog.MentorOnline = p.Online;
            GameScene.Scene.MentorDialog.MenteeEXP = p.MenteeEXP;

            GameScene.Scene.MentorDialog.UpdateInterface();
        }

        private void GameShopUpdate(S.GameShopInfo p)
        {
            p.Item.Stock = p.StockLevel;

            var itm = GameShopInfoList.FirstOrDefault(x => x.GIndex == p.Item.GIndex);

            if (itm != null)
                itm.UpdateItem(p.Item);
            else
                GameShopInfoList.Add(p.Item);

            if (p.Item.Date > DateTime.Now.AddDays(-7)) GameShopDialog.New.Visible = true;
        }

        private void HeroStashActivate(S.HeroStashActivate p)
        {
            HeroStashDialog.ActiveHero = p.Name;
            User.HeroState = HeroState.Unspawned;
            GameScene.Scene.HeroStashDialog.RefreshGUI();
        }

        private void HeroStashInfo(S.HeroStashInfo p)
        {
            for (int i = 0; i < GameScene.Scene.HeroStashDialog.Icons.Length; i++)
            {
                var icon = GameScene.Scene.HeroStashDialog.Icons[i];

                if (icon.Name.Text == p.Name)
                {
                    // to do
                    icon.Hint = $"Name: {p.Name}\nClass: {p.Class.ToString()}\nGender: {p.Gender.ToString()}\nLevel: {p.Level.ToString()}";
                    break;
                }

                if (icon.Name.Text != string.Empty) continue;
                icon.Location = new Point(103 + (i % 4 * 60), 65 + (i / 4 * 42));
                icon.Name.Text = p.Name;
                icon.Index = 1770+ ((byte)p.Gender * 5) + (byte)p.Class;
                icon.Hint = $"Name: {p.Name}\nClass: {p.Class.ToString()}\nGender: {p.Gender.ToString()}\nLevel: {p.Level.ToString()}";
                if (p.Active)
                    HeroStashDialog.ActiveHero = p.Name;

                GameScene.Scene.HeroStashDialog.RefreshGUI();
                break;
            }
        }

        private void GameShopStock(S.GameShopStock p)
        {
            for (int i = 0; i < GameShopInfoList.Count; i++)
            {
                if (GameShopInfoList[i].GIndex == p.GIndex)
                    {
                    if (p.StockLevel == 0) GameShopInfoList.Remove(GameShopInfoList[i]);
                    else GameShopInfoList[i].Stock = p.StockLevel;

                    if (GameShopDialog.Visible) GameShopDialog.UpdateShop();
                    }
            }
        }
        public void AddItem(UserItem item)
        {
            Redraw();

            if (item.Info.StackSize > 1) //Stackable
            {
                for (int i = 0; i < User.Inventory.Length; i++)
                {
                    UserItem temp = User.Inventory[i];
                    if (temp == null || item.Info != temp.Info || temp.Count >= temp.Info.StackSize) continue;

                    if (item.Count + temp.Count <= temp.Info.StackSize)
                    {
                        temp.Count += item.Count;
                        return;
                    }
                    item.Count -= temp.Info.StackSize - temp.Count;
                    temp.Count = temp.Info.StackSize;
                }
            }

            if (item.Info.Type == ItemType.Potion || item.Info.Type == ItemType.Scroll || (item.Info.Type == ItemType.Script && item.Info.Effect == 1))
            {
                for (int i = 0; i < User.BeltIdx - 2; i++)
                {
                    if (User.Inventory[i] != null) continue;
                    User.Inventory[i] = item;
                    return;
                }
            }
            else if (item.Info.Type == ItemType.Amulet)
            {
                for (int i = 4; i < User.BeltIdx; i++)
                {
                    if (User.Inventory[i] != null) continue;
                    User.Inventory[i] = item;
                    return;
                }
            }
            else
            {
                for (int i = User.BeltIdx; i < User.Inventory.Length; i++)
                {
                    if (User.Inventory[i] != null) continue;
                    User.Inventory[i] = item;
                    return;
                }
            }

            for (int i = 0; i < User.Inventory.Length; i++)
            {
                if (User.Inventory[i] != null) continue;
                User.Inventory[i] = item;
                return;
            }
        }


        public void HeroAddItem(UserItem item)
        {
            Redraw();

            if (item.Info.StackSize > 1) //Stackable
            {
                for (int i = 0; i < Hero.Inventory.Length; i++)
                {
                    UserItem temp = Hero.Inventory[i];
                    if (temp == null || item.Info != temp.Info || temp.Count >= temp.Info.StackSize) continue;

                    if (item.Count + temp.Count <= temp.Info.StackSize)
                    {
                        temp.Count += item.Count;
                        return;
                    }
                    item.Count -= temp.Info.StackSize - temp.Count;
                    temp.Count = temp.Info.StackSize;
                }
            }

            if (item.Info.Type == ItemType.Potion || item.Info.Type == ItemType.Scroll || (item.Info.Type == ItemType.Script && item.Info.Effect == 1))
            {
                for (int i = 0; i < 2; i++)
                {
                    if (Hero.Inventory[i] != null) continue;
                    Hero.Inventory[i] = item;
                    return;
                }
            }     
            else
            {
                for (int i = 2; i < Hero.Inventory.Length; i++)
                {
                    if (Hero.Inventory[i] != null) continue;
                    Hero.Inventory[i] = item;
                    return;
                }
            }

            for (int i = 0; i < Hero.Inventory.Length; i++)
            {
                if (Hero.Inventory[i] != null) continue;
                Hero.Inventory[i] = item;
                return;
            }
        }


        public static void Bind(UserItem item)
        {
            for (int i = 0; i < ItemInfoList.Count; i++)
            {
                if (ItemInfoList[i].Index != item.ItemIndex) continue;

                item.Info = ItemInfoList[i];

                item.SetSlotSize();

                for (int s = 0; s < item.Slots.Length; s++)
                {
                    if (item.Slots[s] == null) continue;

                    Bind(item.Slots[s]);
                }

                return;
            }
        }

        public static void BindQuest(ClientQuestProgress quest)
        {
            for (int i = 0; i < QuestInfoList.Count; i++)
            {
                if (QuestInfoList[i].Index != quest.Id) continue;

                quest.QuestInfo = QuestInfoList[i];

                return;
            }
        }

        public Color GradeNameColor(ItemGrade grade)
        {
            switch (grade)
            {
                case ItemGrade.Common:
                    return Color.Yellow;
                case ItemGrade.Rare:
                    return Color.DeepSkyBlue;
                case ItemGrade.Legendary:
                    return Color.DarkOrange;
                case ItemGrade.Mythical:
                    return Color.Plum;
                case ItemGrade.Quest:
                    return Color.LightGreen;
                default:
                    return Color.White;
            }
        }

        public void CreateMonsterLabel(MapObject monster)
        {
            if (monster == null || monster.Race != ObjectType.Monster || !CMain.Alt)
            {
                DisposeMonsterLabel();

                return;
            }

            if (MonsterLabel != null && !MonsterLabel.IsDisposed) return;

            MonsterLabel = new MirControl
            {
                BackColour = Color.FromArgb(255, 50, 50, 50),
                Border = true,
                BorderColour = Color.LightYellow,
                DrawControlTexture = true,
                NotControl = true,
                Parent = this,
                Opacity = 0.7F,
                Visible = false,
            };

            //Name Info Label
            MirControl[] outlines = new MirControl[6];
            outlines[0] = MonsterInfoLabel(monster);
            outlines[1] = MonsterDefenseLabel(monster);
            outlines[2] = MonsterAttackLabel(monster);
            outlines[3] = MonsterGeneralLabel(monster);
            outlines[4] = MonsterOtherLabel(monster);
            outlines[5] = MonsterPoisonBuffLebels(monster);

            foreach (var outline in outlines)
            {
                if (outline != null)
                {
                    outline.Size = new Size(MonsterLabel.Size.Width, outline.Size.Height);
                }
            }

        }

        private MirControl MonsterPoisonBuffLebels(MapObject m)
        {
            var mob = (MonsterObject)m;
            if (mob == null)
                return null;
            if (GetObjectPoisons(mob) == null || _effectList == null)
                return null;
            int pCount = 0;
            int bCount = 0;

            MirLabel poisonLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Gray,
                Location = new Point(4, MonsterLabel.DisplayRectangle.Bottom + 4),
                OutLine = true,
                Parent = MonsterLabel,
            };
            //MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, poisonLabel.DisplayRectangle.Right + 4),
              //                          Math.Max(MonsterLabel.Size.Height, poisonLabel.DisplayRectangle.Bottom + 4));
            for (int i = 0; i < _effectList.Count; i++)
            {
                int index = -1;
                //int y = 24;
                bool wasB = false;
                switch (_effectList[i].EffectType)
                {
                    case 0:
                        pCount++;
                        wasB = false;
                        //y = 48;
                        switch (_effectList[i].PType)
                        {

                            case PoisonType.Green:
                                index = 197;
                                break;
                            case PoisonType.Red:
                                index = 199;
                                break;
                            case PoisonType.Burning:
                                index = 194;
                                break;
                            case PoisonType.DelayedExplosion:
                                index = 195;
                                break;
                            case PoisonType.Bleeding:
                                index = 193;
                                break;
                            case PoisonType.Frozen:
                                index = 196;
                                break;
                            case PoisonType.Paralysis:
                            case PoisonType.LRParalysis:
                                index = 198;
                                break;
                            case PoisonType.Slow:
                                index = 200;
                                break;
                            case PoisonType.Stun:
                                index = 201;
                                break;
                            case PoisonType.Trap:
                                index = 202;
                                break;
                        }
                        break;
                    case 1:
                        index = BuffImage(_effectList[i].BType);
                        //y = 24;
                        bCount++;
                        wasB = true;
                        break;
                }
                if (index != -1)
                {
                    int x = wasB ? bCount : pCount;
                    if (x - 1 != -1)
                        x -= 1;
                    
                    MirImageControl tmp = new MirImageControl()
                    {
                        Index = index,
                        Library = wasB ? Libraries.BuffIcon : Libraries.EdensEliteInter,
                        Parent = poisonLabel,
                        Location = new Point(4 + (24 * x), 4 + pCount > 0 && bCount > 0 ? 28 : 0),
                        NotControl = true,
                    };
                    Poisons.Add(tmp);
                }

            }
            if (pCount == 0 &&
                bCount == 0)
                return null;
            if (pCount > bCount && pCount > 0 && bCount > 0)
                poisonLabel.Size = new Size(4 + (24 * pCount), 52);
            else if (pCount > bCount && pCount > 0 && bCount <= 0)
                poisonLabel.Size = new Size(4 + (24 * pCount), 24);
            else if (bCount > pCount && bCount > 0 && pCount > 0)
                poisonLabel.Size = new Size(4 + (24 * bCount), 52);
            else if (bCount > pCount && bCount > 0 && pCount <= 0)
                poisonLabel.Size = new Size(4 + (24 * pCount), 24);
            else if (bCount == pCount)
                poisonLabel.Size = new Size(4 + (24 * pCount), 52);
            MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, poisonLabel.DisplayRectangle.Right + 4),
                                        Math.Max(MonsterLabel.Size.Height, MonsterLabel.Size.Height + poisonLabel.Size.Height));
            
            MirControl outLine = new MirControl
            {
                BackColour = Color.FromArgb(255, 50, 50, 50),
                Border = true,
                BorderColour = Color.LightYellow,
                NotControl = true,
                Parent = MonsterLabel,
                Opacity = 0.4F,
                Location = new Point(0, 0)
            };
            outLine.Size = MonsterLabel.Size;
            return outLine;
        }

        #region Monster Stat Labels
        private MirControl MonsterOtherLabel(MapObject m)
        {
            var monster = ((MonsterObject)m);

            if (monster.IsBoss)
            {
                MirLabel IsBossLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Gray,
                    Location = new Point(4, MonsterLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = MonsterLabel,
                    Text = "-Is Boss",
                };


                MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, IsBossLabel.DisplayRectangle.Right + 4),
                    Math.Max(MonsterLabel.Size.Height, IsBossLabel.DisplayRectangle.Bottom + 4));
            }

            if (monster.IsPet)
            {
                MirLabel IsPetLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Gray,
                    Location = new Point(4, MonsterLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = MonsterLabel,
                    Text = "-Is Pet",
                };


                MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, IsPetLabel.DisplayRectangle.Right + 4),
                    Math.Max(MonsterLabel.Size.Height, IsPetLabel.DisplayRectangle.Bottom + 4));
            }

            if (monster.IsTamable)
            {

                MirLabel IsTamableLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Gray,
                    Location = new Point(4, MonsterLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = MonsterLabel,
                    Text = "-Is Tamable"
                };


                MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, IsTamableLabel.DisplayRectangle.Right + 4),
                    Math.Max(MonsterLabel.Size.Height, IsTamableLabel.DisplayRectangle.Bottom + 4));
            }

            if (monster.IsPushable)
            {

                MirLabel IsPushableLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Gray,
                    Location = new Point(4, MonsterLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = MonsterLabel,
                    Text = "-Is Pushable"
                };


                MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, IsPushableLabel.DisplayRectangle.Right + 4),
                    Math.Max(MonsterLabel.Size.Height, IsPushableLabel.DisplayRectangle.Bottom + 4));
            }

            if (monster.IsUndead)
            {

                MirLabel mirLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Gray,
                    Location = new Point(4, MonsterLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = MonsterLabel,
                    Text = "-Is Undead"
                };


                MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, mirLabel.DisplayRectangle.Right + 4),
                    Math.Max(MonsterLabel.Size.Height, mirLabel.DisplayRectangle.Bottom + 4));
            }



            #region OUTLINE
            MirControl outLine = new MirControl
            {
                BackColour = Color.FromArgb(255, 50, 50, 50),
                Border = true,
                BorderColour = Color.LightYellow,
                NotControl = true,
                Parent = MonsterLabel,
                Opacity = 0.4F,
                Location = new Point(0, 0)
            };
            outLine.Size = MonsterLabel.Size;
            #endregion

            return outLine;
        }
        #endregion

        #region Buff Checks
        public List<MirImageControl> Poisons = new List<MirImageControl>();
        public List<EE_Mob_Active_Effects> _effectList = new List<EE_Mob_Active_Effects>();
        private List<EE_Mob_Active_Effects> GetObjectPoisons(MonsterObject mob)
        {
            if (mob == null)
                return null;
            _effectList.Clear();
            _effectList = new List<EE_Mob_Active_Effects>();
            Poisons.Clear();
            #region Poison Checks
            if (mob.Poison.HasFlag(PoisonType.None))
            {

            }
            if (mob.Poison.HasFlag(PoisonType.Green))
            {
                EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                {
                    EffectType = 0,
                    PType = PoisonType.Green
                };
                _effectList.Add(tmp);
            }
            if (mob.Poison.HasFlag(PoisonType.Red))
            {
                EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                {
                    EffectType = 0,
                    PType = PoisonType.Red
                };
                _effectList.Add(tmp);
            }
            if (mob.Poison.HasFlag(PoisonType.Slow))
            {
                EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                {
                    EffectType = 0,
                    PType = PoisonType.Slow
                };
                _effectList.Add(tmp);
            }
            if (mob.Poison.HasFlag(PoisonType.Frozen))
            {
                EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                {
                    EffectType = 0,
                    PType = PoisonType.Frozen
                };
                _effectList.Add(tmp);
            }
            if (mob.Poison.HasFlag(PoisonType.Stun))
            {
                EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                {
                    EffectType = 0,
                    PType = PoisonType.Stun
                };
                _effectList.Add(tmp);
            }
            if (mob.Poison.HasFlag(PoisonType.Paralysis))
            {
                EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                {
                    EffectType = 0,
                    PType = PoisonType.Paralysis
                };
                _effectList.Add(tmp);
            }
            if (mob.Poison.HasFlag(PoisonType.DelayedExplosion))
            {
                EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                {
                    EffectType = 0,
                    PType = PoisonType.DelayedExplosion
                };
                _effectList.Add(tmp);
            }
            if (mob.Poison.HasFlag(PoisonType.Bleeding))
            {
                EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                {
                    EffectType = 0,
                    PType = PoisonType.Bleeding
                };
                _effectList.Add(tmp);
            }
            if (mob.Poison.HasFlag(PoisonType.LRParalysis))
            {
                EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                {
                    EffectType = 0,
                    PType = PoisonType.LRParalysis
                };
                _effectList.Add(tmp);
            }
            if (mob.Poison.HasFlag(PoisonType.Trap))
            {
                EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                {
                    EffectType = 0,
                    PType = PoisonType.Trap
                };
                _effectList.Add(tmp);
            }
            if (mob.Poison.HasFlag(PoisonType.Burning))
            {
                EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                {
                    EffectType = 0,
                    PType = PoisonType.Burning
                };
                _effectList.Add(tmp);
            }
            #endregion

            BuffType tmpB = mob.Buffs.Where(a => a == BuffType.None).FirstOrDefault();
            if (tmpB == BuffType.None)
            {

            }
            else
            {
                tmpB = mob.Buffs.Where(a => a == BuffType.TemporalFlux).FirstOrDefault();
                if (tmpB == BuffType.TemporalFlux)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.TemporalFlux
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.ShadowStepFlux).FirstOrDefault();
                if (tmpB == BuffType.ShadowStepFlux)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.ShadowStepFlux
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Hiding).FirstOrDefault();
                if (tmpB == BuffType.Hiding)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Hiding
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Cloak).FirstOrDefault();
                if (tmpB == BuffType.Cloak)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Cloak
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Haste).FirstOrDefault();
                if (tmpB == BuffType.Haste)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Haste
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.SwiftFeet).FirstOrDefault();
                if (tmpB == BuffType.SwiftFeet)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.SwiftFeet
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Fury).FirstOrDefault();
                if (tmpB == BuffType.Fury)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Fury
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.SoulShield).FirstOrDefault();
                if (tmpB == BuffType.SoulShield)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.SoulShield
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.BlessedArmour).FirstOrDefault();
                if (tmpB == BuffType.BlessedArmour)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.BlessedArmour
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.LightBody).FirstOrDefault();
                if (tmpB == BuffType.LightBody)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.LightBody
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.UltimateEnhancer).FirstOrDefault();
                if (tmpB == BuffType.UltimateEnhancer)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.UltimateEnhancer
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.UltimateEnhancerQuest).FirstOrDefault();
                if (tmpB == BuffType.UltimateEnhancerQuest)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.UltimateEnhancerQuest
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.ProtectionField).FirstOrDefault();
                if (tmpB == BuffType.ProtectionField)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.ProtectionField
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Rage).FirstOrDefault();
                if (tmpB == BuffType.Rage)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Rage
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Curse).FirstOrDefault();
                if (tmpB == BuffType.Curse)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Curse
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.MoonLight).FirstOrDefault();
                if (tmpB == BuffType.MoonLight)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.MoonLight
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.DarkBody).FirstOrDefault();
                if (tmpB == BuffType.DarkBody)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.DarkBody
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Concentration).FirstOrDefault();
                if (tmpB == BuffType.Concentration)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Concentration
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.VampireShot).FirstOrDefault();
                if (tmpB == BuffType.VampireShot)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.VampireShot
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.PoisonShot).FirstOrDefault();
                if (tmpB == BuffType.PoisonShot)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.PoisonShot
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.CounterAttack).FirstOrDefault();
                if (tmpB == BuffType.CounterAttack)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.CounterAttack
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.CounterAttack).FirstOrDefault();
                if (tmpB == BuffType.CounterAttack)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.CounterAttack
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.MentalState).FirstOrDefault();
                if (tmpB == BuffType.MentalState)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.MentalState
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.EnergyShield).FirstOrDefault();
                if (tmpB == BuffType.EnergyShield)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.EnergyShield
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.MagicBooster).FirstOrDefault();
                if (tmpB == BuffType.MagicBooster)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.MagicBooster
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.PetEnhancer).FirstOrDefault();
                if (tmpB == BuffType.PetEnhancer)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.PetEnhancer
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.ImmortalSkin).FirstOrDefault();
                if (tmpB == BuffType.ImmortalSkin)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.ImmortalSkin
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.MagicShield).FirstOrDefault();
                if (tmpB == BuffType.MagicShield)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.MagicShield
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.HumUp).FirstOrDefault();//
                if (tmpB == BuffType.HumUp)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.HumUp
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Global).FirstOrDefault();
                if (tmpB == BuffType.Global)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Global
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.FastMove).FirstOrDefault();
                if (tmpB == BuffType.FastMove)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.FastMove
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.GameMaster).FirstOrDefault();
                if (tmpB == BuffType.GameMaster)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.GameMaster
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.General).FirstOrDefault();
                if (tmpB == BuffType.General)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.General
                    };
                    _effectList.Add(tmp);
                }

                tmpB = mob.Buffs.Where(a => a == BuffType.Exp).FirstOrDefault();
                if (tmpB == BuffType.Exp)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Exp
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.ExpQuest).FirstOrDefault();
                if (tmpB == BuffType.ExpQuest)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.ExpQuest
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Drop).FirstOrDefault();
                if (tmpB == BuffType.Drop)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Drop
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.DropQuest).FirstOrDefault();
                if (tmpB == BuffType.DropQuest)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.DropQuest
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Gold).FirstOrDefault();
                if (tmpB == BuffType.Gold)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Gold
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.BagWeight).FirstOrDefault();
                if (tmpB == BuffType.BagWeight)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.BagWeight
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Transform).FirstOrDefault();
                if (tmpB == BuffType.Transform)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Transform
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.RelationshipEXP).FirstOrDefault();
                if (tmpB == BuffType.RelationshipEXP)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.RelationshipEXP
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Mentee).FirstOrDefault();
                if (tmpB == BuffType.Mentee)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Mentee
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Mentor).FirstOrDefault();
                if (tmpB == BuffType.Mentor)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Mentor
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Guild).FirstOrDefault();
                if (tmpB == BuffType.Guild)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Guild
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Prison).FirstOrDefault();
                if (tmpB == BuffType.Prison)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Prison
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Rested).FirstOrDefault();
                if (tmpB == BuffType.Rested)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Rested
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Impact).FirstOrDefault();
                if (tmpB == BuffType.Impact)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Impact
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Magic).FirstOrDefault();
                if (tmpB == BuffType.Magic)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Magic
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Taoist).FirstOrDefault();
                if (tmpB == BuffType.Taoist)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Taoist
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Storm).FirstOrDefault();
                if (tmpB == BuffType.Storm)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Storm
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.StormQuest).FirstOrDefault();
                if (tmpB == BuffType.StormQuest)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.StormQuest
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.StormQuest).FirstOrDefault();
                if (tmpB == BuffType.StormQuest)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.StormQuest
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Accuracy).FirstOrDefault();
                if (tmpB == BuffType.Accuracy)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Accuracy
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.AccuracyQuest).FirstOrDefault();
                if (tmpB == BuffType.AccuracyQuest)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.AccuracyQuest
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Agility).FirstOrDefault();
                if (tmpB == BuffType.Agility)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Agility
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.HealthAid).FirstOrDefault();
                if (tmpB == BuffType.HealthAid)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.HealthAid
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.ManaAid).FirstOrDefault();
                if (tmpB == BuffType.ManaAid)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.ManaAid
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Defence).FirstOrDefault();
                if (tmpB == BuffType.Defence)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Defence
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.MagicDefence).FirstOrDefault();
                if (tmpB == BuffType.MagicDefence)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.MagicDefence
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.WonderDrug).FirstOrDefault();
                if (tmpB == BuffType.WonderDrug)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.WonderDrug
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Knapsack).FirstOrDefault();
                if (tmpB == BuffType.Knapsack)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Knapsack
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.VIP).FirstOrDefault();
                if (tmpB == BuffType.VIP)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.VIP
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.HeroBuff).FirstOrDefault();
                if (tmpB == BuffType.HeroBuff)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.HeroBuff
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.MoonMist).FirstOrDefault();
                if (tmpB == BuffType.MoonMist)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.MoonMist
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.NewbieGuild).FirstOrDefault();
                if (tmpB == BuffType.NewbieGuild)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.NewbieGuild
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Group).FirstOrDefault();
                if (tmpB == BuffType.Group)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Group
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.PinPoint).FirstOrDefault();
                if (tmpB == BuffType.PinPoint)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.PinPoint
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Evasive).FirstOrDefault();
                if (tmpB == BuffType.Evasive)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Evasive
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.Enrage).FirstOrDefault();
                if (tmpB == BuffType.Enrage)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.Enrage
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.IronWall).FirstOrDefault();
                if (tmpB == BuffType.IronWall)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.IronWall
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.SpeedyMagician).FirstOrDefault();
                if (tmpB == BuffType.SpeedyMagician)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.SpeedyMagician
                    };
                    _effectList.Add(tmp);
                }
                tmpB = mob.Buffs.Where(a => a == BuffType.HeroEnergyShield).FirstOrDefault();
                if (tmpB == BuffType.HeroEnergyShield)
                {
                    EE_Mob_Active_Effects tmp = new EE_Mob_Active_Effects
                    {
                        EffectType = 1,
                        BType = BuffType.HeroEnergyShield
                    };
                    _effectList.Add(tmp);
                }
            }
            return _effectList;
        }
        #endregion

        #region Monster Label
        private MirControl MonsterAttackLabel(MapObject m)
        {
            var monster = ((MonsterObject)m);

            if (monster.MinDC > 0 || monster.MaxDC > 0)
            {

                MirLabel PhysicalAttack = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Crimson,
                    Location = new Point(4, MonsterLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = MonsterLabel,
                    Text = "Physical Attack: " + monster.MinDC + "-" + monster.MaxDC,
                };


                MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, PhysicalAttack.DisplayRectangle.Right + 4),
                    Math.Max(MonsterLabel.Size.Height, PhysicalAttack.DisplayRectangle.Bottom + 4));
            }

            if (monster.MinMC > 0 || monster.MaxMC > 0)
            {
                MirLabel MagicalDefense = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.RoyalBlue,
                    Location = new Point(4, MonsterLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = MonsterLabel,
                    Text = "Magical Attack: " + monster.MinMC + "-" + monster.MaxMC,
                };


                MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, MagicalDefense.DisplayRectangle.Right + 4),
                    Math.Max(MonsterLabel.Size.Height, MagicalDefense.DisplayRectangle.Bottom + 4));
            }


            if (monster.MinSC > 0 || monster.MaxSC > 0)
            {
                MirLabel MagicalDefense = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.LimeGreen,
                    Location = new Point(4, MonsterLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = MonsterLabel,
                    Text = "Spiritual Attack: " + monster.MinSC + "-" + monster.MaxSC,
                };


                MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, MagicalDefense.DisplayRectangle.Right + 4),
                    Math.Max(MonsterLabel.Size.Height, MagicalDefense.DisplayRectangle.Bottom + 4));
            }

            #region OUTLINE
            MirControl outLine = new MirControl
            {
                BackColour = Color.FromArgb(255, 50, 50, 50),
                Border = true,
                BorderColour = Color.LightYellow,
                NotControl = true,
                Parent = MonsterLabel,
                Opacity = 0.4F,
                Location = new Point(0, 0)
            };
            outLine.Size = MonsterLabel.Size;
            #endregion

            return outLine;
        }

        private MirControl MonsterGeneralLabel(MapObject m)
        {
            var monster = ((MonsterObject)m);

         
            MirLabel AgilityLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Gold,
                Location = new Point(4, MonsterLabel.DisplayRectangle.Bottom),
                OutLine = true,
                Parent = MonsterLabel,
                Text = "Agility: " + monster.Agil,
            };


            MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, AgilityLabel.DisplayRectangle.Right + 4),
                Math.Max(MonsterLabel.Size.Height, AgilityLabel.DisplayRectangle.Bottom + 4));

            MirLabel AccuracyLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Gold,
                Location = new Point(4, MonsterLabel.DisplayRectangle.Bottom),
                OutLine = true,
                Parent = MonsterLabel,
                Text = "Accuracy: " + monster.Acc,
            };


            MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, AccuracyLabel.DisplayRectangle.Right + 4),
                Math.Max(MonsterLabel.Size.Height, AccuracyLabel.DisplayRectangle.Bottom + 4));

            MirLabel AttackSpeedLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Gold,
                Location = new Point(4, MonsterLabel.DisplayRectangle.Bottom),
                OutLine = true,
                Parent = MonsterLabel,
                Text = "Attack Per Second: " + string.Format("{0:0.00}", monster.AttkSpeed / 1000.0),
            };


            MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, AttackSpeedLabel.DisplayRectangle.Right + 4),
                Math.Max(MonsterLabel.Size.Height, AttackSpeedLabel.DisplayRectangle.Bottom + 4));

            MirLabel MoveSpeedLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Gold,
                Location = new Point(4, MonsterLabel.DisplayRectangle.Bottom),
                OutLine = true,
                Parent = MonsterLabel,
                Text = "Move Per Second: " + string.Format("{0:0.00}",monster.MoveSpeed / 1000.0),
            };


            MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, MoveSpeedLabel.DisplayRectangle.Right + 4),
                Math.Max(MonsterLabel.Size.Height, MoveSpeedLabel.DisplayRectangle.Bottom + 4));



            #region OUTLINE
            MirControl outLine = new MirControl
            {
                BackColour = Color.FromArgb(255, 50, 50, 50),
                Border = true,
                BorderColour = Color.LightYellow,
                NotControl = true,
                Parent = MonsterLabel,
                Opacity = 0.4F,
                Location = new Point(0, 0)
            };
            outLine.Size = MonsterLabel.Size;
            #endregion

            return outLine;
        }

        private MirControl MonsterDefenseLabel(MapObject m)
        {
            var monster = ((MonsterObject)m);

            if (monster.MinAC > 0 || monster.MaxAC > 0)
            {

                MirLabel PhysicalDefense = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Orange,
                    Location = new Point(4, MonsterLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = MonsterLabel,
                    Text = "Physical Defense: " + monster.MinAC + "-" + monster.MaxAC,
                };


                MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, PhysicalDefense.DisplayRectangle.Right + 4),
                    Math.Max(MonsterLabel.Size.Height, PhysicalDefense.DisplayRectangle.Bottom + 4));
            }

            if (monster.MinMAC > 0 || monster.MaxMAC > 0)
            {
                MirLabel MagicalDefense = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Orange,
                    Location = new Point(4, MonsterLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = MonsterLabel,
                    Text = "Magical Defense: " + monster.MinMAC + "-" + monster.MaxMAC,
                };


                MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, MagicalDefense.DisplayRectangle.Right + 4),
                    Math.Max(MonsterLabel.Size.Height, MagicalDefense.DisplayRectangle.Bottom + 4));
            }


            #region OUTLINE
            MirControl outLine = new MirControl
            {
                BackColour = Color.FromArgb(255, 50, 50, 50),
                Border = true,
                BorderColour = Color.LightYellow,
                NotControl = true,
                Parent = MonsterLabel,
                Opacity = 0.4F,
                Location = new Point(0, 0)
            };
            outLine.Size = MonsterLabel.Size;
            #endregion

            return outLine;
        }

        private MirControl MonsterInfoLabel(MapObject m)
        {
            var monster = ((MonsterObject)m);

            MirLabel nameLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.White,
                Location = new Point(4, 4),
                OutLine = true,
                Parent = MonsterLabel,
                Text = monster.Name + (monster.Level > 0 ? " - Level " + monster.Level : ""),
                Font = new Font(Settings.FontName, 8F, FontStyle.Bold),
            };


            MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, nameLabel.DisplayRectangle.Right + 4),
                Math.Max(MonsterLabel.Size.Height, nameLabel.DisplayRectangle.Bottom));

            MirLabel mirLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.White,
                Location = new Point(4, MonsterLabel.DisplayRectangle.Bottom),
                OutLine = true,
                Parent = MonsterLabel,
                Text = $"Experience: {monster.Experience} - ({String.Format("{0:0.00}", ((monster.Experience / (double)User.MaxExperience) * 100))}%)",
                Font = new Font(Settings.FontName, 8F, FontStyle.Bold),
            };


            MonsterLabel.Size = new Size(Math.Max(MonsterLabel.Size.Width, mirLabel.DisplayRectangle.Right + 4),
                Math.Max(MonsterLabel.Size.Height, mirLabel.DisplayRectangle.Bottom));

            #region OUTLINE
            MirControl outLine = new MirControl
            {
                BackColour = Color.FromArgb(255, 50, 50, 50),
                Border = true,
                BorderColour = Color.LightYellow,
                NotControl = true,
                Parent = MonsterLabel,
                Opacity = 0.4F,
                Location = new Point(0, 0)
            };
            outLine.Size = MonsterLabel.Size;
            #endregion

            return outLine;
        }

        private void DisposeMonsterLabel()
        {

            if (MonsterLabel != null && !MonsterLabel.IsDisposed)
                MonsterLabel.Dispose();
            MonsterLabel = null;

        }

        public void DisposeItemLabel()
        {
            if (ItemLabel != null && !ItemLabel.IsDisposed)
                ItemLabel.Dispose();
            ItemLabel = null;
        }
        public void DisposeMailLabel()
        {
            if (MailLabel != null && !MailLabel.IsDisposed)
                MailLabel.Dispose();
            MailLabel = null;
        }
        public void DisposeMemoLabel()
        {
            if (MemoLabel != null && !MemoLabel.IsDisposed)
                MemoLabel.Dispose();
            MemoLabel = null;
        }
        public void DisposeGuildBuffLabel()
        {
            if (GuildBuffLabel != null && !GuildBuffLabel.IsDisposed)
                GuildBuffLabel.Dispose();
            GuildBuffLabel = null;
        }
        #endregion

        public MirControl NameInfoLabel(UserItem item, bool Inspect = false, bool hero = false)
        {
            ushort level = Inspect ? InspectDialog.Level : MapObject.User.Level;
            MirClass job = Inspect ? InspectDialog.Class : MapObject.User.Class;
            HoverItem = item;
            if (hero)
            {
                level = GameScene.Hero.Level;
                job = GameScene.Hero.Class;
            }

            ItemInfo realItem = Functions.GetRealItem(item.Info, level, job, ItemInfoList);

            MirLabel nameLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = GradeNameColor(HoverItem.Info.Grade),
                Location = new Point(4, 4),
                OutLine = true,
                Parent = ItemLabel,
                Text = HoverItem.Info.Grade != ItemGrade.None ? HoverItem.Info.FriendlyName + "\n" + HoverItem.Info.Grade.ToString() : 
                (HoverItem.Info.Type == ItemType.Pets && HoverItem.Info.Shape == 26 && HoverItem.Info.Effect != 7) ? "WonderDrug" : HoverItem.Info.FriendlyName,
            };

            if (HoverItem.RefineAdded > 0)
            nameLabel.Text = "(*)" + nameLabel.Text;


            ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, nameLabel.DisplayRectangle.Right + 4),
                Math.Max(ItemLabel.Size.Height, nameLabel.DisplayRectangle.Bottom));

            string text = "";
            bool dura0 = false;
            if (HoverItem.Info.StackSize > 1)
            {
                text += string.Format(" Count {0}", HoverItem.Count);
            }

            if (HoverItem.Info.Durability > 0)
            {
                switch (HoverItem.Info.Type)
                {
                    case ItemType.Amulet:
                        text += string.Format(" Usage {0}/{1}", HoverItem.CurrentDura, HoverItem.MaxDura);
                        break;
                    case ItemType.Ore:
                        text += string.Format(" Purity {0}", Math.Round(HoverItem.CurrentDura / 1000M));
                        break;
                    case ItemType.Meat:
                        text += string.Format(" Quality {0}", Math.Round(HoverItem.CurrentDura / 1000M));
                        break;
                    case ItemType.Mount:
                        text += string.Format(" Loyalty {0} / {1}", HoverItem.CurrentDura, HoverItem.MaxDura);
                        break;
                    case ItemType.Food:
                        text += string.Format(" Nutrition {0}", HoverItem.CurrentDura);
                        break;
                    case ItemType.Gem:
                        break;
                    case ItemType.Potion:
                        break;
                    case ItemType.Transform:
                        break;
                    case ItemType.Pets:
                        if (HoverItem.Info.Shape == 26)//WonderDrug
                        {
                            string strTime = Functions.PrintTimeSpanFromSeconds((HoverItem.CurrentDura * 60), false);
                            text += "\n" + string.Format(" Duration {0}", strTime);
                        }
                        break;
                    default:
                        text += 
                            (
                            HoverItem.Info.Type == ItemType.Scroll && 
                            HoverItem.Info.Shape == 20) ? 
                            "\nRelease stage: " + (HoverItem.Info.RequiredAmount + 1) + "\nSuccess chance: " + HoverItem.Info.Durability + "%" :
                            HoverItem.Info.Type != ItemType.RuneStone ? 
                            string.Format(" Durability {0}/{1}", Math.Round(HoverItem.CurrentDura / 1000M ),
                                                   Math.Round(HoverItem.MaxDura / 1000M) ) : "";
                        if (HoverItem.CurrentDura <= ushort.MinValue)
                            dura0 = true;
                        break;
                }
            }

            String WedRingName = "";
            if (HoverItem.WeddingRing == -1)
            {
                if (HoverItem.Info.Type != ItemType.RuneStone)
                    WedRingName = HoverItem.Info.Type.ToString() + "\n" + "Weight " + HoverItem.Weight + text;
                else
                    WedRingName = HoverItem.Info.Type.ToString() + text;
            }
            else
            {
                WedRingName = "Wedding Ring" +
                "\n" + "Weight " + HoverItem.Weight + text;
            }

            MirLabel etcLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = dura0 ? Color.Red : GradeNameColor(HoverItem.Info.Grade),
                Location = new Point(4, nameLabel.DisplayRectangle.Bottom),
                OutLine = true,
                Parent = ItemLabel,
                Text = WedRingName
            };

            ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, etcLabel.DisplayRectangle.Right + 4),
                Math.Max(ItemLabel.Size.Height, etcLabel.DisplayRectangle.Bottom + 4));

            #region OUTLINE
            MirControl outLine = new MirControl
            {
                BackColour = Color.FromArgb(255, 50, 50, 50),
                Border = true,
                BorderColour = Color.Gray,
                NotControl = true,
                Parent = ItemLabel,
                Opacity = 0.4F,
                Location = new Point(0, 0)
            };
            outLine.Size = ItemLabel.Size;
            #endregion

            return outLine;
        }
        public MirControl AttackInfoLabel(UserItem item, bool Inspect = false, bool hero = false)
        {
            ushort level = Inspect ? InspectDialog.Level : MapObject.User.Level;
            MirClass job = Inspect ? InspectDialog.Class : MapObject.User.Class;
            HoverItem = item;

            if (hero)
            {
                level = GameScene.Hero.Level;
                job = GameScene.Hero.Class;
            }

            ItemInfo realItem = Functions.GetRealItem(item.Info, level, job, ItemInfoList);

            ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

            bool fishingItem = false;

            switch (HoverItem.Info.Type)
            {
                case ItemType.Hook:
                case ItemType.Float:
                case ItemType.Bait:
                case ItemType.Finder:
                case ItemType.Reel:
                    fishingItem = true;
                    break;
                case ItemType.Weapon:
                    if (HoverItem.Info.Shape == 49 || HoverItem.Info.Shape == 50)
                        fishingItem = true;
                    break;
                case ItemType.Pets:
                    if (HoverItem.Info.Shape == 26) return null;
                    break;
                default:
                    fishingItem = false;
                    break;
            }

            int count = 0;
            int minValue = 0;
            int maxValue = 0;
            int addValue = 0;
            string text = "";

            #region Dura gem
            minValue = realItem.Durability;

            if (minValue > 0 &&  realItem.Type == ItemType.Gem)
            {
                count++;
                text = string.Format("Adds {0}Durability", minValue / 1000);
                MirLabel DuraLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, DuraLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, DuraLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region DC
            minValue = realItem.MinDC;
            maxValue = realItem.MaxDC;

            
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.DC : 0;
            if (realItem.ClassBased &&
                realItem.AllowLvlSys &&
                HoverItem.LvlSystem > 0)
            {

            }
            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                if (HoverItem.Info.Type != ItemType.Gem)
                    text = string.Format(addValue > 0 ? "DC : {0}-{1} (+{2})" : "DC : {0}-{1}", minValue, maxValue + addValue, addValue);
                else
                    text = string.Format("Adds {0}DC",minValue + maxValue + addValue);
                MirLabel DCLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, DCLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, DCLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region MC

            minValue = realItem.MinMC;
            maxValue = realItem.MaxMC;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.MC : 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                if (HoverItem.Info.Type != ItemType.Gem)
                    text = string.Format(addValue > 0 ? "MC : {0}-{1} (+{2})" : "MC : {0}-{1}", minValue, maxValue + addValue, addValue);
                else
                    text = string.Format("Adds {0}MC", minValue + maxValue + addValue);
                MirLabel MCLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, MCLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, MCLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region SC

            minValue = realItem.MinSC;
            maxValue = realItem.MaxSC;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.SC : 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                if (HoverItem.Info.Type != ItemType.Gem)
                    text = string.Format(addValue > 0 ? "SC + {0}-{1} (+{2})" : "SC : {0}-{1}", minValue, maxValue + addValue, addValue);
                else
                    text = string.Format("Adds {0}SC", minValue + maxValue + addValue);
                MirLabel SCLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, SCLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, SCLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region LUCK / SUCCESS

            minValue = realItem.Luck;
            maxValue = 0;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.Luck : 0;

            if (minValue > 0 || addValue != 0) // Ice
            {
                count++;

                if(realItem.Type == ItemType.Pets && realItem.Shape == 28)
                {
                    text = string.Format("BagWeight + {0}% ", minValue + addValue);
                }
                else if (realItem.Type == ItemType.Potion && realItem.Shape == 4)
                {
                    text = string.Format("Exp + {0}% ", minValue + addValue);
                }
                else if (realItem.Type == ItemType.Potion && realItem.Shape == 5)
                {
                    text = string.Format("Drop Rate + {0}% ", minValue + addValue);
                }
                else
                {
                    text = string.Format(minValue + addValue > 0 ? "Luck + {0}" : "Curse + {0}", Math.Abs(minValue + addValue)); // Ice
                }

                MirLabel LUCKLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, LUCKLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, LUCKLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region ACC

            minValue = realItem.Accuracy;
            maxValue = 0;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.Accuracy : 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                if (HoverItem.Info.Type != ItemType.Gem)
                    text = string.Format(addValue > 0 ? "Accuracy: : {0} (+{1})" : "Accuracy: : {0}", minValue + addValue, addValue);
                else
                    text = string.Format("Adds {0}Accuracy", minValue + maxValue + addValue);
                MirLabel ACCLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    //Text = string.Format("Accuracy + {0}", minValue + addValue)
                    Text = text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, ACCLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, ACCLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region HOLY

            minValue = realItem.Holy;
            maxValue = 0;
            addValue = 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                MirLabel HOLYLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    //Text = string.Format("Holy + {0}", minValue + addValue)
                    Text = string.Format(addValue > 0 ? "Holy: + {0} (+{1})" : "Holy: + {0}", minValue + addValue, addValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, HOLYLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, HOLYLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region ASPEED

            minValue = realItem.AttackSpeed;
            maxValue = 0;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.AttackSpeed : 0;

            if (minValue != 0 || maxValue != 0 || addValue != 0)
            {
                string plus = (addValue + minValue < 0) ? "" : "+";

                count++;
                if (HoverItem.Info.Type != ItemType.Gem)
                {
                    string negative = "+";
                    if (addValue < 0) negative = "";
                    text = string.Format(addValue != 0 ? "A.Speed: " + plus + "{0} ({2}{1})" : "A.Speed: " + plus + "{0}", minValue + addValue, addValue, negative);
                    //text = string.Format(addValue > 0 ? "A.Speed: + {0} (+{1})" : "A.Speed: + {0}", minValue + addValue, addValue);
                }
                else
                    text = string.Format("Adds {0}A.Speed", minValue + maxValue + addValue);
                MirLabel ASPEEDLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    //Text = string.Format("A.Speed + {0}", minValue + addValue)
                    Text = text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, ASPEEDLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, ASPEEDLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region FREEZING

            minValue = realItem.Freezing;
            maxValue = 0;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.Freezing : 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                if (HoverItem.Info.Type != ItemType.Gem)
                    text = string.Format(addValue > 0 ? "Freezing: + {0} (+{1})" : "Freezing: + {0}", minValue + addValue, addValue);
                else
                    text = string.Format("Adds {0}Freezing", minValue + maxValue + addValue);
                MirLabel FREEZINGLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    //Text = string.Format("Freezing + {0}", minValue + addValue)
                    Text = text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, FREEZINGLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, FREEZINGLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region POISON

            minValue = realItem.PoisonAttack;
            maxValue = 0;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.PoisonAttack : 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                if (HoverItem.Info.Type != ItemType.Gem)
                    text = string.Format(addValue > 0 ? "Poison: + {0} (+{1})" : "Poison: + {0}", minValue + addValue, addValue);
                else
                    text = string.Format("Adds {0}Poison", minValue + maxValue + addValue);
                MirLabel POISONLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    //Text = string.Format("Poison + {0}", minValue + addValue)
                    Text = text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, POISONLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, POISONLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region CRITICALRATE / FLEXIBILITY

            minValue = realItem.CriticalRate;
            maxValue = 0;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.CriticalRate : 0;

            if ((minValue > 0 || maxValue > 0 || addValue > 0))
            {
                count++;
                if (HoverItem.Info.Type != ItemType.Gem)
                    text = string.Format(addValue > 0 ? "Critical Chance: + {0} (+{1})" : "Critical Chance: + {0}", minValue + addValue, addValue);
                else
                    text = string.Format("Adds +{0} Crit Rate", minValue + maxValue + addValue);
                MirLabel CRITICALRATELabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = text
                };

                if(fishingItem)
                {
                    CRITICALRATELabel.Text = string.Format(addValue > 0 ? "Flexibility: + {0} (+{1})" : "Flexibility: + {0}", minValue + addValue, addValue);
                }

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, CRITICALRATELabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, CRITICALRATELabel.DisplayRectangle.Bottom));
            }
            #endregion

            #region CRITICALDAMAGE

            minValue = realItem.CriticalDamage;
            maxValue = 0;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.CriticalDamage : 0;

            if ((minValue > 0 || maxValue > 0 || addValue > 0))
            {
                count++;
                if (HoverItem.Info.Type != ItemType.Gem)
                    text = string.Format(addValue > 0 ? "Critical Damge: + {0} (+{1})" : "Critical Damge: + {0}", minValue + addValue, addValue);
                else
                    text = string.Format("Adds +{0} Crit Damage", minValue + maxValue + addValue);
                MirLabel CRITICALDAMAGELabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, CRITICALDAMAGELabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, CRITICALDAMAGELabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region Reflect

            minValue = realItem.Reflect;
            maxValue = 0;
            addValue = 0;

            if ((minValue > 0 || maxValue > 0 || addValue > 0) && (realItem.Type != ItemType.Gem))
            {
                count++;
                MirLabel ReflectLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Reflect chance: {0}", minValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, ReflectLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, ReflectLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region Hpdrain

            minValue = realItem.HpDrainRate;
            maxValue = 0;
            addValue = 0;

            if ((minValue > 0 || maxValue > 0 || addValue > 0) && (realItem.Type != ItemType.Gem))
            {
                count++;
                MirLabel HPdrainLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("HP Drain Rate: {0}%", minValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, HPdrainLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, HPdrainLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region Gem | Orb Socket
            minValue = realItem.MinSocket;

            if ((minValue > 0) && (realItem.Type == ItemType.Gem))
            {
                count++;
                MirLabel SocketLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Adds {0} Socket{1} to an item", minValue, minValue > 1 ? "s" : "")
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, SocketLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, SocketLabel.DisplayRectangle.Bottom));
            }
            #endregion

            if (count > 0)
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

                #region OUTLINE
                MirControl outLine = new MirControl
                {
                    BackColour = Color.FromArgb(255, 50, 50, 50),
                    Border = true,
                    BorderColour = Color.Gray,
                    NotControl = true,
                    Parent = ItemLabel,
                    Opacity = 0.4F,
                    Location = new Point(0, 0)
                };
                outLine.Size = ItemLabel.Size;
                #endregion

                return outLine;
            }
            else
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height - 4);
            }
            return null;
        }
        public MirControl DefenceInfoLabel(UserItem item, bool Inspect = false, bool hero = false)
        {
            ushort level = Inspect ? InspectDialog.Level : MapObject.User.Level;
            MirClass job = Inspect ? InspectDialog.Class : MapObject.User.Class;
            HoverItem = item;

            if (hero)
            {
                level = GameScene.Hero.Level;
                job = GameScene.Hero.Class;
            }

            ItemInfo realItem = Functions.GetRealItem(item.Info, level, job, ItemInfoList);

            ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

            bool fishingItem = false;

            switch (HoverItem.Info.Type)
            {
                case ItemType.Hook:
                case ItemType.Float:
                case ItemType.Bait:
                case ItemType.Finder:
                case ItemType.Reel:
                    fishingItem = true;
                    break;
                case ItemType.Weapon:
                    if (HoverItem.Info.Shape == 49 || HoverItem.Info.Shape == 50)
                        fishingItem = true;
                    break;
                case ItemType.Pets:
                    if (HoverItem.Info.Shape == 26) return null;
                    break;
                default:
                    fishingItem = false;
                    break;
            }

            int count = 0;
            int minValue = 0;
            int maxValue = 0;
            int addValue = 0;

            string text = "";
            #region AC

            minValue = realItem.MinAC;
            maxValue = realItem.MaxAC;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.AC : 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                if (HoverItem.Info.Type == ItemType.Scroll && HoverItem.Info.Shape == 20)
                    text = string.Format("Lose chance: {0}% , Level lose: {1}", minValue,maxValue);
                else if (HoverItem.Info.Type != ItemType.Gem)
                    text = string.Format(addValue > 0 ? "AC : {0}-{1} (+{2})" : "AC : {0}-{1}", minValue, maxValue + addValue, addValue);
                else
                    text = string.Format("Adds {0} AC", minValue + maxValue + addValue);
                MirLabel ACLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = text
                };

                if (fishingItem)
                {
                    if (HoverItem.Info.Type == ItemType.Float)
                    {
                        ACLabel.Text = string.Format("Nibble Chance + " + (addValue > 0 ? "{0}~{1}% (+{2})" : "{0}~{1}%"), minValue, maxValue + addValue);
                    }
                    else if (HoverItem.Info.Type == ItemType.Finder)
                    {
                        ACLabel.Text = string.Format("Finder Increase + " + (addValue > 0 ? "{0}~{1}% (+{2})" : "{0}~{1}%"), minValue, maxValue + addValue);
                    }
                    else
                    {
                        ACLabel.Text = string.Format("Success Chance + " + (addValue > 0 ? "{0}% (+{1})" : "{0}%"), maxValue, maxValue + addValue);
                    }
                }

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, ACLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, ACLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region MAC

            minValue = realItem.MinMAC;
            maxValue = realItem.MaxMAC;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.MAC : 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                if (HoverItem.Info.Type != ItemType.Gem)
                    text = string.Format(addValue > 0 ? "MAC : {0}-{1} (+{2})" : "MAC : {0}-{1}", minValue, maxValue + addValue, addValue);
                else
                    text = string.Format("Adds {0} MAC", minValue + maxValue + addValue);
                MirLabel MACLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = text
                };

                if (fishingItem)
                {
                    MACLabel.Text = string.Format("AutoReel Chance + {0}%", maxValue + addValue);
                }

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, MACLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, MACLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region MAXHP

            minValue = realItem.HP;
            maxValue = 0;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.HP : 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                MirLabel MAXHPLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.Red,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = realItem.Type == ItemType.Potion ? 
                    string.Format(addValue > 0 ? "HP + {0} Recovery (+{1})" : "HP + {0} Recovery", minValue + addValue, addValue)
                    : string.Format(addValue > 0 ? "Max HP + {0} (+{1})" : "Max HP + {0}", minValue + addValue, addValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, MAXHPLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, MAXHPLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region MAXMP

            minValue = realItem.MP;
            maxValue = 0;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.MP : 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                MirLabel MAXMPLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.Blue,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = realItem.Type == ItemType.Potion ? 
                    string.Format(addValue > 0 ? "MP + {0} Recovery (+{1})" : "MP + {0} Recovery", minValue + addValue, addValue)
                    : string.Format(addValue > 0 ? "Max MP + {0} (+{1})" : "Max MP + {0}", minValue + addValue, addValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, MAXMPLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, MAXMPLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region MAXHPRATE

            minValue = realItem.HPrate;
            maxValue = 0;
            addValue = 0;

            if (minValue != 0 || maxValue != 0 || addValue != 0)
            {
                count++;
                MirLabel MAXHPRATELabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = realItem.Type == ItemType.Potion ? (minValue + addValue > 0 ? Color.Turquoise : Color.PaleVioletRed) : addValue > 0 ? Color.Cyan : Color.Red,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = realItem.Type == ItemType.Potion && realItem.Shape != 3 ? string.Format("HP Tick {0}%", minValue + addValue) : string.Format("Max HP + {0}%", minValue + addValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, MAXHPRATELabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, MAXHPRATELabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region MAXMPRATE

            minValue = realItem.MPrate;
            maxValue = 0;
            addValue = 0;

            if (minValue != 0 || maxValue != 0 || addValue != 0)
            {
                count++;
                MirLabel MAXMPRATELabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = realItem.Type == ItemType.Potion ? (minValue + addValue > 0 ? Color.Turquoise : Color.PaleVioletRed) : addValue > 0 ? Color.Cyan : Color.Blue,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = realItem.Type == ItemType.Potion && realItem.Shape != 3 ? string.Format("MP Tick {0}%", minValue + addValue) : string.Format("Max MP + {0}%", minValue + addValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, MAXMPRATELabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, MAXMPRATELabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region HP/MP Regenerate
            /*
if (realItem.Type == ItemType.Potion && realItem.Shape == 0)
{
    count++;
    var regenTick = 5 + (level / 10);
    var HPregenTick = (regenTick * (realItem.HPrate + 100)) / 100;
    var MPregenTick = (regenTick * (realItem.MPrate + 100)) / 100;
    MirLabel RegenTick = new MirLabel
    {
        AutoSize = true,
        ForeColour = Color.Yellow,
        Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
        OutLine = true,
        Parent = ItemLabel,
        Text = string.Format("HP Regenerate : {0} | MP Regenerate : {1}", HPregenTick, MPregenTick),
    };

    ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, RegenTick.DisplayRectangle.Right + 4),
        Math.Max(ItemLabel.Size.Height, RegenTick.DisplayRectangle.Bottom));
}
            */
            #endregion

            #region MAXACRATE

            minValue = realItem.MaxAcRate;
            maxValue = 0;
            addValue = 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                MirLabel MAXACRATE = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Max AC + {0}%", minValue + addValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, MAXACRATE.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, MAXACRATE.DisplayRectangle.Bottom));
            }

            #endregion

            #region MAXMACRATE

            minValue = realItem.MaxMacRate;
            maxValue = 0;
            addValue = 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                MirLabel MAXMACRATELabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Max MAC + {0}%", minValue + addValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, MAXMACRATELabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, MAXMACRATELabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region HEALTH_RECOVERY

            minValue = realItem.HealthRecovery;
            maxValue = 0;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.HealthRecovery : 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                MirLabel HEALTH_RECOVERYLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.Red,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    //Text = string.Format("HealthRecovery + {0}", minValue + addValue)
                    Text = string.Format(addValue > 0 ? "Health Recovery : {0} (+{1})" : "Health Recovery : {0}", minValue + addValue, addValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, HEALTH_RECOVERYLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, HEALTH_RECOVERYLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region MANA_RECOVERY

            minValue = realItem.SpellRecovery;
            maxValue = 0;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.ManaRecovery : 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                MirLabel MANA_RECOVERYLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.Blue,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    //Text = string.Format("ManaRecovery + {0}", minValue + addValue)
                    Text = string.Format(addValue > 0 ? "Mana Recovery : {0} (+{1})" : "Mana Recovery : {0}", minValue + addValue, addValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, MANA_RECOVERYLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, MANA_RECOVERYLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region POISON_RECOVERY

            minValue = realItem.PoisonRecovery;
            maxValue = 0;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.PoisonRecovery : 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                MirLabel POISON_RECOVERYabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    //Text = string.Format("Poison Recovery + {0}", minValue + addValue)
                    Text = string.Format(addValue > 0 ? "Poison Recovery : {0} (+{1})" : "Poison Recovery : {0}", minValue + addValue, addValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, POISON_RECOVERYabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, POISON_RECOVERYabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region AGILITY

            minValue = realItem.Agility;
            maxValue = 0;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.Agility : 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                MirLabel AGILITYLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    //Text = string.Format("Agility + {0}", minValue + addValue)
                    Text = string.Format(addValue > 0 ? "Agility : {0} (+{1})" : "Agility : {0}", minValue + addValue, addValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, AGILITYLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, AGILITYLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region STRONG

            minValue = realItem.Strong;
            maxValue = 0;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.Strong : 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                MirLabel STRONGLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    //Text = string.Format("Strong + {0}", minValue + addValue)
                    Text = string.Format(addValue > 0 ? "Strong - {0} (+{1})" : "Strong - {0}", minValue + addValue, addValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, STRONGLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, STRONGLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region POISON_RESIST

            minValue = realItem.PoisonResist;
            maxValue = 0;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.PoisonResist : 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                if (HoverItem.Info.Type != ItemType.Gem)
                    text = string.Format(addValue > 0 ? "Poison Resist - {0} (+{1})" : "Poison Resist - {0}", minValue + addValue, addValue);
                else
                    text = string.Format("Adds {0} Poison Resist", minValue + maxValue + addValue);
                MirLabel POISON_RESISTLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    //Text = string.Format("Poison Resist + {0}", minValue + addValue)
                    Text = text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, POISON_RESISTLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, POISON_RESISTLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region MAGIC_RESIST

            minValue = realItem.MagicResist;
            maxValue = 0;
            addValue = (!HoverItem.Info.NeedIdentify || HoverItem.Identified) ? HoverItem.MagicResist : 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                if (HoverItem.Info.Type != ItemType.Gem)
                    text = string.Format(addValue > 0 ? "Magic Resist : {0} (+{1})" : "Magic Resist : {0}", minValue + addValue, addValue);
                else
                    text = string.Format("Adds {0} Magic Resist", minValue + maxValue + addValue);
                MirLabel MAGIC_RESISTLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    //Text = string.Format("Magic Resist + {0}", minValue + addValue)
                    Text = text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, MAGIC_RESISTLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, MAGIC_RESISTLabel.DisplayRectangle.Bottom));
            }

            #endregion

            if (count > 0)
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);
                
                #region OUTLINE
                MirControl outLine = new MirControl
                {
                    BackColour = Color.FromArgb(255, 50, 50, 50),
                    Border = true,
                    BorderColour = Color.Gray,
                    NotControl = true,
                    Parent = ItemLabel,
                    Opacity = 0.4F,
                    Location = new Point(0, 0)
                };
                outLine.Size = ItemLabel.Size;
                #endregion

                return outLine;
            }
            else
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height - 4);
            }
            return null;
        }
        public MirControl WeightInfoLabel(UserItem item, bool Inspect = false, bool hero = false)
        {
            ushort level = Inspect ? InspectDialog.Level : MapObject.User.Level;
            MirClass job = Inspect ? InspectDialog.Class : MapObject.User.Class;
            HoverItem = item;
            if (hero)
            {
                level = GameScene.Hero.Level;
                job = GameScene.Hero.Class;
            }

            ItemInfo realItem = Functions.GetRealItem(item.Info, level, job, ItemInfoList);

            ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);
            
            int count = 0;
            int minValue = 0;
            int maxValue = 0;
            int addValue = 0;
            
            #region HANDWEIGHT

            minValue = realItem.HandWeight;
            maxValue = 0;
            addValue = 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                MirLabel HANDWEIGHTLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format(addValue > 0 ? "Hand Weight + {0} (+{1})" : "Hand Weight + {0}", minValue + addValue, addValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, HANDWEIGHTLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, HANDWEIGHTLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region WEARWEIGHT

            minValue = realItem.WearWeight;
            maxValue = 0;
            addValue = 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                MirLabel WEARWEIGHTLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    //Text = string.Format("Wear Weight + {0}", minValue + addValue)
                    Text = string.Format(addValue > 0 ? "Wear Weight + {0} (+{1})" : "Wear Weight + {0}", minValue + addValue, addValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, WEARWEIGHTLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, WEARWEIGHTLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region BAGWEIGHT

            minValue = realItem.BagWeight;
            maxValue = 0;
            addValue = 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                MirLabel BAGWEIGHTLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    //Text = string.Format("Bag Weight + {0}", minValue + addValue)
                    Text = string.Format(addValue > 0 ? "Bag Weight + {0} (+{1})" : "Bag Weight + {0}", minValue + addValue, addValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, BAGWEIGHTLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, BAGWEIGHTLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region FASTRUN
            minValue = realItem.CanFastRun==true?1:0;
            maxValue = 0;
            addValue = 0;

            if (minValue > 0 || maxValue > 0 || addValue > 0)
            {
                count++;
                MirLabel BAGWEIGHTLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Instant Run")
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, BAGWEIGHTLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, BAGWEIGHTLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region TIME & RANGE
            minValue = 0;
            maxValue = 0;
            addValue = 0;

            if (HoverItem.Info.Type == ItemType.Potion && HoverItem.Info.Durability > 0)
            {
                count++;
                MirLabel TNRLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("{0}", 
                    HoverItem.Info.Shape == 1 ? 
                        string.Format("Cool-Down : {0} seconds",HoverItem.Info.Durability) : 
                        string.Format("Time : {0}", Functions.PrintTimeSpanFromSeconds(HoverItem.Info.Durability * 60)))
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, TNRLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, TNRLabel.DisplayRectangle.Bottom));
            }

            if (HoverItem.Info.Type == ItemType.Transform && HoverItem.Info.Durability > 0)
            {
                count++;
                MirLabel TNRLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = addValue > 0 ? Color.Cyan : Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Time : {0}", Functions.PrintTimeSpanFromSeconds(HoverItem.Info.Durability, false))
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, TNRLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, TNRLabel.DisplayRectangle.Bottom));
            }

            #endregion

            if (count > 0)
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

                #region OUTLINE
                MirControl outLine = new MirControl
                {
                    BackColour = Color.FromArgb(255, 50, 50, 50),
                    Border = true,
                    BorderColour = Color.Gray,
                    NotControl = true,
                    Parent = ItemLabel,
                    Opacity = 0.4F,
                    Location = new Point(0, 0)
                };
                outLine.Size = ItemLabel.Size;
                #endregion

                return outLine;
            }
            else
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height - 4);
            }
            return null;
        }
        public MirControl AwakeInfoLabel(UserItem item, bool Inspect = false, bool hero = false)
        {
            ushort level = Inspect ? InspectDialog.Level : MapObject.User.Level;
            MirClass job = Inspect ? InspectDialog.Class : MapObject.User.Class;

            if (hero)
            {
                level = GameScene.Hero.Level;
                job = GameScene.Hero.Class;
            }

            HoverItem = item;
            ItemInfo realItem = Functions.GetRealItem(item.Info, level, job, ItemInfoList);

            ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

            int count = 0;

            #region AWAKENAME
            if (HoverItem.Awake.AwakeLevel> 0)
            {
                count++;
                MirLabel AWAKENAMELabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = GradeNameColor(HoverItem.Info.Grade),
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("{0} Awakening({1})", HoverItem.Awake.type.ToString(), HoverItem.Awake.AwakeLevel)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, AWAKENAMELabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, AWAKENAMELabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region AWAKE_TOTAL_VALUE
            if (HoverItem.Awake.AwakeValue> 0)
            {
                count++;
                MirLabel AWAKE_TOTAL_VALUELabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format(realItem.Type != ItemType.Armour ? "{0} + {1}~{2}" : "MAX {0} + {1}", HoverItem.Awake.type.ToString(), HoverItem.Awake.AwakeValue, HoverItem.Awake.AwakeValue)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, AWAKE_TOTAL_VALUELabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, AWAKE_TOTAL_VALUELabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region AWAKE_LEVEL_VALUE
            if (HoverItem.Awake.AwakeLevel> 0)
            {
                count++;
                for (int i = 0; i < HoverItem.Awake.AwakeLevel; i++)
                {
                    MirLabel AWAKE_LEVEL_VALUELabel = new MirLabel
                    {
                        AutoSize = true,
                        ForeColour = Color.White,
                        Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                        OutLine = true,
                        Parent = ItemLabel,
                        Text = string.Format(realItem.Type != ItemType.Armour ? "Level {0} : {1} + {2}~{3}" : "Level {0} : MAX {1} + {2}~{3}", i + 1, HoverItem.Awake.type.ToString(), HoverItem.Awake.GetAwakeLevelValue(i), HoverItem.Awake.GetAwakeLevelValue(i))
                    };

                    ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, AWAKE_LEVEL_VALUELabel.DisplayRectangle.Right + 4),
                        Math.Max(ItemLabel.Size.Height, AWAKE_LEVEL_VALUELabel.DisplayRectangle.Bottom));
                }
            }

            #endregion

            if (count > 0)
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

                #region OUTLINE
                MirControl outLine = new MirControl
                {
                    BackColour = Color.FromArgb(255, 50, 50, 50),
                    Border = true,
                    BorderColour = Color.Gray,
                    NotControl = true,
                    Parent = ItemLabel,
                    Opacity = 0.4F,
                    Location = new Point(0, 0)
                };
                outLine.Size = ItemLabel.Size;
                #endregion

                return outLine;
            }
            else
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height - 4);
            }
            return null;
        }
        public MirControl NeedInfoLabel(UserItem item, bool Inspect = false,bool hero = false, bool shopSell = false)
        {
            ushort level = Inspect ? InspectDialog.Level : MapObject.User.Level;
            MirClass job = Inspect ? InspectDialog.Class : MapObject.User.Class;
            HoverItem = item;

            if (hero)
            {
                level = GameScene.Hero.Level;
                job = GameScene.Hero.Class;
            }

            ItemInfo realItem = Functions.GetRealItem(item.Info, level, job, ItemInfoList);

            ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

            int count = 0;

            #region LEVEL
            if (realItem.RequiredAmount > 0)
            {
                count++;
                string text;
                Color colour = GradeNameColor(HoverItem.Info.Grade); //Color.White;

                switch (realItem.RequiredType)
                {
                    case RequiredType.Level:
                        text = string.Format("Required Level : {0}", realItem.RequiredAmount);
                        if (MapObject.User.Level < realItem.RequiredAmount)
                            colour = Color.Red;
                        break;
                    case RequiredType.AC:
                        text = string.Format("Required AC : {0}", realItem.RequiredAmount);
                        if (MapObject.User.MaxAC < realItem.RequiredAmount)
                            colour = Color.Red;
                        break;
                    case RequiredType.MAC:
                        text = string.Format("Required MAC : {0}", realItem.RequiredAmount);
                        if (MapObject.User.MaxMAC < realItem.RequiredAmount)
                            colour = Color.Red;
                        break;
                    case RequiredType.DC:
                        text = string.Format("Required DC : {0}", realItem.RequiredAmount);
                        if (MapObject.User.MaxDC < realItem.RequiredAmount)
                            colour = Color.Red;
                        break;
                    case RequiredType.MC:
                        text = string.Format("Required MC : {0}", realItem.RequiredAmount);
                        if (MapObject.User.MaxMC < realItem.RequiredAmount)
                            colour = Color.Red;
                        break;
                    case RequiredType.SC:
                        text = string.Format("Required SC : {0}", realItem.RequiredAmount);
                        if (MapObject.User.MaxSC < realItem.RequiredAmount)
                            colour = Color.Red;
                        break;
                    default:
                        text = "Unknown Type Required";
                        break;
                }

                if (GameScene.Hero != null && colour == Color.Red)
                {

                    switch (realItem.RequiredType)
                    {
                        case RequiredType.Level:
                            text = string.Format("Required Level : {0}", realItem.RequiredAmount);
                            if (Hero.Level >= realItem.RequiredAmount)
                                colour = Color.Turquoise;
                            break;
                        case RequiredType.AC:
                            text = string.Format("Required AC : {0}", realItem.RequiredAmount);
                            if (Hero.MaxAC >= realItem.RequiredAmount)
                                colour = Color.Turquoise;
                            break;
                        case RequiredType.MAC:
                            text = string.Format("Required MAC : {0}", realItem.RequiredAmount);
                            if (Hero.MaxMAC >= realItem.RequiredAmount)
                                colour = Color.Turquoise;
                            break;
                        case RequiredType.DC:
                            text = string.Format("Required DC : {0}", realItem.RequiredAmount);
                            if (Hero.MaxDC >= realItem.RequiredAmount)
                                colour = Color.Turquoise;
                            break;
                        case RequiredType.MC:
                            text = string.Format("Required MC : {0}", realItem.RequiredAmount);
                            if (Hero.MaxMC >= realItem.RequiredAmount)
                                colour = Color.Turquoise;
                            break;
                        case RequiredType.SC:
                            text = string.Format("Required SC : {0}", realItem.RequiredAmount);
                            if (Hero.MaxSC >= realItem.RequiredAmount)
                                colour = Color.Turquoise;
                            break;
                        default:
                            text = "Unknown Type Required";
                            break;
                    }
                }

                MirLabel LEVELLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = colour,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, LEVELLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, LEVELLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region CLASS
            if (realItem.RequiredClass != RequiredClass.None)
            {
                count++;
                Color colour = GradeNameColor(HoverItem.Info.Grade); //Color.White;

                switch (MapObject.User.Class)
                {
                    case MirClass.Warrior:
                        if (!realItem.RequiredClass.HasFlag(RequiredClass.Warrior))
                            colour = Color.Red;
                        break;
                    case MirClass.Wizard:
                        if (!realItem.RequiredClass.HasFlag(RequiredClass.Wizard))
                            colour = Color.Red;
                        break;
                    case MirClass.Taoist:
                        if (!realItem.RequiredClass.HasFlag(RequiredClass.Taoist))
                            colour = Color.Red;
                        break;
                    case MirClass.Assassin:
                        if (!realItem.RequiredClass.HasFlag(RequiredClass.Assassin))
                            colour = Color.Red;
                        break;
                    case MirClass.Archer:
                        if (!realItem.RequiredClass.HasFlag(RequiredClass.Archer))
                            colour = Color.Red;
                        break;
                }

                if (GameScene.Hero != null && colour == Color.Red)
                {
                    switch (Hero.Class)
                    {
                        case MirClass.Warrior:
                            if (realItem.RequiredClass.HasFlag(RequiredClass.Warrior))
                                colour = Color.Turquoise;
                            break;
                        case MirClass.Wizard:
                            if (realItem.RequiredClass.HasFlag(RequiredClass.Wizard))
                                colour = Color.Turquoise;
                            break;
                        case MirClass.Taoist:
                            if (realItem.RequiredClass.HasFlag(RequiredClass.Taoist))
                                colour = Color.Turquoise;
                            break;
                        case MirClass.Assassin:
                            if (realItem.RequiredClass.HasFlag(RequiredClass.Assassin))
                                colour = Color.Turquoise;
                            break;
                        case MirClass.Archer:
                            if (realItem.RequiredClass.HasFlag(RequiredClass.Archer))
                                colour = Color.Turquoise;
                            break;
                    }
                }
                MirLabel CLASSLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = colour,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Class Required : {0}", realItem.RequiredClass)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, CLASSLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, CLASSLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region GENDER
            if (realItem.RequiredGender != RequiredGender.None)
            {
                count++;
                Color colour = GradeNameColor(HoverItem.Info.Grade); //Color.White;

                switch (MapObject.User.Gender)
                {
                    case MirGender.Male:
                        if (!realItem.RequiredGender.HasFlag(RequiredGender.Male))
                            colour = Color.Red;
                        break;
                    case MirGender.Female:
                        if (!realItem.RequiredGender.HasFlag(RequiredGender.Female))
                            colour = Color.Red;
                        break;
                }

                if (GameScene.Hero != null && colour == Color.Red)
                {
                    switch (Hero.Gender)
                    {
                        case MirGender.Male:
                            if (!realItem.RequiredGender.HasFlag(RequiredGender.Male))
                                colour = Color.Turquoise;
                            break;
                        case MirGender.Female:
                            if (!realItem.RequiredGender.HasFlag(RequiredGender.Female))
                                colour = Color.Turquoise;
                            break;
                    }
                }
                MirLabel CLASSLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = colour,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Gender Required : {0}", realItem.RequiredGender)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, CLASSLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, CLASSLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region Buying - Selling Price
            if (shopSell && realItem.Price > 0)
            {
                count++;
                string text;
                Color colour = Color.Yellow;

                text = string.Format("Buying Price : {0}", realItem.Price);
                if (Gold < realItem.Price)
                    colour = Color.Red;
                else
                    colour = Color.Cyan;

                MirLabel COSTLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = colour,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, COSTLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, COSTLabel.DisplayRectangle.Bottom));

            }
            else
            {
                count++;
                string text;
                Color colour = Color.Yellow;

                text = string.Format("Selling Price : {0}", realItem.Price / 2);

                MirLabel COSTLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = colour,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, COSTLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, COSTLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region Repair Price
            if (item.RepairPrice() > 0)
            {
               
                count++;
                string text;
                Color colour = Color.Yellow;

                text = string.Format("Repair Cost : {0}", item.RepairPrice());
                if (Gold < item.RepairPrice())
                    colour = Color.Red;

                MirLabel COSTLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = colour,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, COSTLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, COSTLabel.DisplayRectangle.Bottom));

            }

            #endregion

            #region HERO/PLAYER
            if (realItem.WearType != WearType.All)
            {
                count++;
                Color colour = Color.LightGoldenrodYellow;


                MirLabel WearLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = colour,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = $"{realItem.WearType.ToString()} Only!",
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, WearLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, WearLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region Scroll
            if (realItem.Type == ItemType.Scroll)
            {
                count++;
                Color colour = Color.SkyBlue;


                MirLabel TeleportLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = colour,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Empty,
                };

                switch (realItem.Shape)
                {
                    case 26:
                        TeleportLabel.Text = $"Teleports to {item.GTInvite} Territory";
                        break;

                }

                if (TeleportLabel.Text != string.Empty)
                {
                    ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, TeleportLabel.DisplayRectangle.Right + 4),
                        Math.Max(ItemLabel.Size.Height, TeleportLabel.DisplayRectangle.Bottom));
                }
            }

            #endregion

            #region HUMUP
            if (realItem.HumUpBased)
            {
                count++;
                Color colour = Color.Red;

                if (MapObject.User.HumUp)
                    colour = Color.White;

                MirLabel HUMUPLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = colour,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("HumUp required!")
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, HUMUPLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, HUMUPLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region UPDATETO
            if (realItem.UpdateTo > 0 && realItem.Type != ItemType.Gem)
            {
                count++;
                Color colour = Color.White;

    
                MirLabel UpdateToLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = colour,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = "Update to: " + realItem.UpdateToName,
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, UpdateToLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, UpdateToLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region ITEMLEVEL
            if (realItem.AllowLvlSys && realItem.LvlSysExp.Length > item.LvlSystem)
            {
                count++;
                Color colour =realItem.LvlSysExp[item.LvlSystem] == 0 ?  Color.Red : item.LvlSystem == realItem.LvlSysExp.Length - 1 ? Color.Red : Color.Yellow;


                MirLabel ItemLevelLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = colour,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = "Item Level: " + (colour == Color.Red ? "MAX" : item.LvlSystem.ToString()),
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, ItemLevelLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, ItemLevelLabel.DisplayRectangle.Bottom));

                MirLabel ItemExpLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = colour,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = "Item Experience: " + (colour == Color.Red ? "MAX" : ((float)item.LvlSysExpGained / item.Info.LvlSysExp[item.LvlSystem] * 100).ToString() + "%"),
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, ItemExpLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, ItemExpLabel.DisplayRectangle.Bottom));
            }

            #endregion

                if (count > 0)
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

                #region OUTLINE
                MirControl outLine = new MirControl
                {
                    BackColour = Color.FromArgb(255, 50, 50, 50),
                    Border = true,
                    BorderColour = Color.Gray,
                    NotControl = true,
                    Parent = ItemLabel,
                    Opacity = 0.4F,
                    Location = new Point(0, 0)
                };
                outLine.Size = ItemLabel.Size;
                #endregion

                return outLine;
            }
            else
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height - 4);
            }
            return null;
        }
        public MirControl BindInfoLabel(UserItem item, bool Inspect = false, bool hero = false)
        {
            ushort level = Inspect ? InspectDialog.Level : MapObject.User.Level;
            MirClass job = Inspect ? InspectDialog.Class : MapObject.User.Class;

            if (hero)
            {
                level = GameScene.Hero.Level;
                job = GameScene.Hero.Class;
            }

            HoverItem = item;
            ItemInfo realItem = Functions.GetRealItem(item.Info, level, job, ItemInfoList);

            ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

            int count = 0;

            #region DONT_DEATH_DROP

            if (HoverItem.Info.Bind != BindMode.none && HoverItem.Info.Bind.HasFlag(BindMode.DontDeathdrop))
            {
                count++;
                MirLabel DONT_DEATH_DROPLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Can't drop on death")
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, DONT_DEATH_DROPLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, DONT_DEATH_DROPLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region DONT_DROP

            if (HoverItem.Info.Bind != BindMode.none && HoverItem.Info.Bind.HasFlag(BindMode.DontDrop))
            {
                count++;
                MirLabel DONT_DROPLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Can't drop")
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, DONT_DROPLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, DONT_DROPLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region DONT_UPGRADE

            if (HoverItem.Info.Bind != BindMode.none && HoverItem.Info.Bind.HasFlag(BindMode.DontUpgrade))
            {
                count++;
                MirLabel DONT_UPGRADELabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Can't upgrade")
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, DONT_UPGRADELabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, DONT_UPGRADELabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region DONT_SELL

            if (HoverItem.Info.Bind != BindMode.none && HoverItem.Info.Bind.HasFlag(BindMode.DontSell))
            {
                count++;
                MirLabel DONT_SELLLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Can't sell")
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, DONT_SELLLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, DONT_SELLLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region DONT_TRADE

            if (HoverItem.Info.Bind != BindMode.none && HoverItem.Info.Bind.HasFlag(BindMode.DontTrade))
            {
                count++;
                MirLabel DONT_TRADELabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Can't trade")
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, DONT_TRADELabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, DONT_TRADELabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region DONT_STORE

            if (HoverItem.Info.Bind != BindMode.none && HoverItem.Info.Bind.HasFlag(BindMode.DontStore))
            {
                count++;
                MirLabel DONT_STORELabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Can't store")
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, DONT_STORELabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, DONT_STORELabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region DONT_REPAIR

            if (HoverItem.Info.Bind != BindMode.none && HoverItem.Info.Bind.HasFlag(BindMode.DontRepair))
            {
                count++;
                MirLabel DONT_REPAIRLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Can't repair")
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, DONT_REPAIRLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, DONT_REPAIRLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region DONT_SPECIALREPAIR

            if (HoverItem.Info.Bind != BindMode.none && HoverItem.Info.Bind.HasFlag(BindMode.NoSRepair))
            {
                count++;
                MirLabel DONT_REPAIRLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Can't special repair")
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, DONT_REPAIRLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, DONT_REPAIRLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region BREAK_ON_DEATH

            if (HoverItem.Info.Bind != BindMode.none && HoverItem.Info.Bind.HasFlag(BindMode.BreakOnDeath))
            {
                count++;
                MirLabel DONT_REPAIRLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Breaks on death")
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, DONT_REPAIRLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, DONT_REPAIRLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region DONT_DESTROY_ON_DROP

            if (HoverItem.Info.Bind != BindMode.none && HoverItem.Info.Bind.HasFlag(BindMode.DestroyOnDrop))
            {
                count++;
                MirLabel DONT_DODLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Destroyed when dropped")
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, DONT_DODLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, DONT_DODLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region NoWeddingRing

            if (HoverItem.Info.Bind != BindMode.none && HoverItem.Info.Bind.HasFlag(BindMode.NoWeddingRing))
            {
                count++;
                MirLabel No_WedLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Cannot be a weddingring")
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, No_WedLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, No_WedLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region BIND_ON_EQUIP

            if ((HoverItem.Info.Bind.HasFlag(BindMode.BindOnEquip)) & HoverItem.SoulBoundId == -1)
            {
                count++;
                MirLabel BOELabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Soulbinds on equip")
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, BOELabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, BOELabel.DisplayRectangle.Bottom));
            }
            else if (HoverItem.SoulBoundId != -1)
            {
                count++;
                MirLabel BOELabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = "Soulbound to: " + GetUserName((uint)HoverItem.SoulBoundId)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, BOELabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, BOELabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region CURSED

            if ((!HoverItem.Info.NeedIdentify || HoverItem.Identified) && HoverItem.Cursed)
            {
                count++;
                MirLabel CURSEDLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Cursed")
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, CURSEDLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, CURSEDLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region Gems

            if (HoverItem.Info.Type == ItemType.Gem)
            {
                #region UseOn text
                count++;
                string Text = "";
                if (HoverItem.Info.Unique == SpecialItemMode.None)
                {
                    Text = "Cannot be used on any item.";
                }
                else
                {
                    Text = "Can be used on: ";
                }
                MirLabel GemUseOn = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = Text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, GemUseOn.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, GemUseOn.DisplayRectangle.Bottom));
                #endregion
                #region Weapon text
                count++;
                if (HoverItem.Info.Unique.HasFlag(SpecialItemMode.Paralize))
                {
                    MirLabel GemWeapon = new MirLabel
                    {
                        AutoSize = true,
                        ForeColour = Color.White,
                        Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                        OutLine = true,
                        Parent = ItemLabel,
                        Text = "-Weapon"
                    };

                    ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, GemWeapon.DisplayRectangle.Right + 4),
                        Math.Max(ItemLabel.Size.Height, GemWeapon.DisplayRectangle.Bottom));
                }
                #endregion
                #region Armour text
                count++;
                if (HoverItem.Info.Unique.HasFlag(SpecialItemMode.Teleport))
                {
                    MirLabel GemArmour = new MirLabel
                    {
                        AutoSize = true,
                        ForeColour = Color.White,
                        Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                        OutLine = true,
                        Parent = ItemLabel,
                        Text = "-Armour"
                    };

                    ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, GemArmour.DisplayRectangle.Right + 4),
                        Math.Max(ItemLabel.Size.Height, GemArmour.DisplayRectangle.Bottom));
                }
                #endregion
                #region Helmet text
                count++;
                if (HoverItem.Info.Unique.HasFlag(SpecialItemMode.Clearring))
                {
                    MirLabel Gemhelmet = new MirLabel
                    {
                        AutoSize = true,
                        ForeColour = Color.White,
                        Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                        OutLine = true,
                        Parent = ItemLabel,
                        Text = "-Helmet"
                    };

                    ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, Gemhelmet.DisplayRectangle.Right + 4),
                        Math.Max(ItemLabel.Size.Height, Gemhelmet.DisplayRectangle.Bottom));
                }
                #endregion
                #region Necklace text
                count++;
                if (HoverItem.Info.Unique.HasFlag(SpecialItemMode.Protection))
                {
                    MirLabel Gemnecklace = new MirLabel
                    {
                        AutoSize = true,
                        ForeColour = Color.White,
                        Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                        OutLine = true,
                        Parent = ItemLabel,
                        Text = "-Necklace"
                    };

                    ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, Gemnecklace.DisplayRectangle.Right + 4),
                        Math.Max(ItemLabel.Size.Height, Gemnecklace.DisplayRectangle.Bottom));
                }
                #endregion
                #region Bracelet text
                count++;
                if (HoverItem.Info.Unique.HasFlag(SpecialItemMode.Revival))
                {
                    MirLabel GemBracelet = new MirLabel
                    {
                        AutoSize = true,
                        ForeColour = Color.White,
                        Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                        OutLine = true,
                        Parent = ItemLabel,
                        Text = "-Bracelet"
                    };

                    ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, GemBracelet.DisplayRectangle.Right + 4),
                        Math.Max(ItemLabel.Size.Height, GemBracelet.DisplayRectangle.Bottom));
                }
                #endregion
                #region Ring text
                count++;
                if (HoverItem.Info.Unique.HasFlag(SpecialItemMode.Muscle))
                {
                    MirLabel GemRing = new MirLabel
                    {
                        AutoSize = true,
                        ForeColour = Color.White,
                        Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                        OutLine = true,
                        Parent = ItemLabel,
                        Text = "-Ring"
                    };

                    ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, GemRing.DisplayRectangle.Right + 4),
                        Math.Max(ItemLabel.Size.Height, GemRing.DisplayRectangle.Bottom));
                }
                #endregion
                #region Amulet text
                count++;
                if (HoverItem.Info.Unique.HasFlag(SpecialItemMode.Flame))
                {
                    MirLabel Gemamulet = new MirLabel
                    {
                        AutoSize = true,
                        ForeColour = Color.White,
                        Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                        OutLine = true,
                        Parent = ItemLabel,
                        Text = "-Amulet"
                    };

                    ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, Gemamulet.DisplayRectangle.Right + 4),
                        Math.Max(ItemLabel.Size.Height, Gemamulet.DisplayRectangle.Bottom));
                }
                #endregion
                #region Belt text
                count++;
                if (HoverItem.Info.Unique.HasFlag(SpecialItemMode.Healing))
                {
                    MirLabel Gembelt = new MirLabel
                    {
                        AutoSize = true,
                        ForeColour = Color.White,
                        Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                        OutLine = true,
                        Parent = ItemLabel,
                        Text = "-Belt"
                    };

                    ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, Gembelt.DisplayRectangle.Right + 4),
                        Math.Max(ItemLabel.Size.Height, Gembelt.DisplayRectangle.Bottom));
                }
                #endregion
                #region Boots text
                count++;
                if (HoverItem.Info.Unique.HasFlag(SpecialItemMode.Probe))
                {
                    MirLabel Gemboots = new MirLabel
                    {
                        AutoSize = true,
                        ForeColour = Color.White,
                        Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                        OutLine = true,
                        Parent = ItemLabel,
                        Text = "-Boots"
                    };

                    ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, Gemboots.DisplayRectangle.Right + 4),
                        Math.Max(ItemLabel.Size.Height, Gemboots.DisplayRectangle.Bottom));
                }
                #endregion
                #region Stone text
                count++;
                if (HoverItem.Info.Unique.HasFlag(SpecialItemMode.Skill))
                {
                    MirLabel Gemstone = new MirLabel
                    {
                        AutoSize = true,
                        ForeColour = Color.White,
                        Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                        OutLine = true,
                        Parent = ItemLabel,
                        Text = "-Stone"
                    };

                    ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, Gemstone.DisplayRectangle.Right + 4),
                        Math.Max(ItemLabel.Size.Height, Gemstone.DisplayRectangle.Bottom));
                }
                #endregion
                #region Torch text
                count++;
                if (HoverItem.Info.Unique.HasFlag(SpecialItemMode.NoDuraLoss))
                {
                    MirLabel Gemtorch = new MirLabel
                    {
                        AutoSize = true,
                        ForeColour = Color.White,
                        Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                        OutLine = true,
                        Parent = ItemLabel,
                        Text = "-Candle"
                    };

                    ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, Gemtorch.DisplayRectangle.Right + 4),
                        Math.Max(ItemLabel.Size.Height, Gemtorch.DisplayRectangle.Bottom));
                }
                #endregion
            }

            #endregion

            #region CANTAWAKEN

            if ((HoverItem.Info.CanAwakening != true) && (HoverItem.Info.Type != ItemType.Gem))
            {
                count++;
                MirLabel CANTAWAKENINGLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Can't awaken")
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, CANTAWAKENINGLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, CANTAWAKENINGLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region EXPIRE

            if (HoverItem.ExpireInfo != null)
            {
                double remainingSeconds = (HoverItem.ExpireInfo.ExpiryDate - DateTime.Now).TotalSeconds;

                count++;
                MirLabel EXPIRELabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = remainingSeconds > 0 ? string.Format("Expires in {0}", Functions.PrintTimeSpanFromSeconds(remainingSeconds)) : "Expired"
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, EXPIRELabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, EXPIRELabel.DisplayRectangle.Bottom));
            }

            #endregion

            if (count > 0)
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

                #region OUTLINE
                MirControl outLine = new MirControl
                {
                    BackColour = Color.FromArgb(255, 50, 50, 50),
                    Border = true,
                    BorderColour = Color.Gray,
                    NotControl = true,
                    Parent = ItemLabel,
                    Opacity = 0.4F,
                    Location = new Point(0, 0)
                };
                outLine.Size = ItemLabel.Size;
                #endregion

                return outLine;
            }
            else
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height - 4);
            }
            return null;
        }
        public MirControl OverlapInfoLabel(UserItem item, bool Inspect = false, bool hero = false)
        {
            ushort level = Inspect ? InspectDialog.Level : MapObject.User.Level;
            MirClass job = Inspect ? InspectDialog.Class : MapObject.User.Class;
            HoverItem = item;

            if (hero)
            {
                level = GameScene.Hero.Level;
                job = GameScene.Hero.Class;
            }

            ItemInfo realItem = Functions.GetRealItem(item.Info, level, job, ItemInfoList);

            ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

            int count = 0;


            #region GEM

            if (realItem.Type == ItemType.Gem)
            {
                string text = "";

                switch (realItem.Shape)
                {
                    case 1:
                        text = "Hold CTRL and left click to repair weapons.";
                        break;
                    case 2:
                        text = "Hold CTRL and left click to repair armour\nand accessory items.";
                        break;
                    case 3:
                    case 4:
                        text = "Hold CTRL and left click to combine with an item.";
                        break;
                }
                count++;
                MirLabel GEMLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = text
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, GEMLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, GEMLabel.DisplayRectangle.Bottom));
            }

            #endregion

            #region SPLITUP

            if (realItem.StackSize > 1 && realItem.Type != ItemType.Gem)
            {
                count++;
                MirLabel SPLITUPLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.White,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = string.Format("Max Combine Count : {0}\nShift + Left click to split the stack", realItem.StackSize)
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, SPLITUPLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, SPLITUPLabel.DisplayRectangle.Bottom));
            }

            #endregion

            if (count > 0)
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

                #region OUTLINE
                MirControl outLine = new MirControl
                {
                    BackColour = Color.FromArgb(255, 50, 50, 50),
                    Border = true,
                    BorderColour = Color.Gray,
                    NotControl = true,
                    Parent = ItemLabel,
                    Opacity = 0.4F,
                    Location = new Point(0, 0)
                };
                outLine.Size = ItemLabel.Size;
                #endregion

                return outLine;
            }
            else
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height - 4);
            }
            return null;
        }

        public MirControl ShieldInfoLabel(UserItem item, bool Inspect = false , bool hero = false)
        {
            ushort level = Inspect ? InspectDialog.Level : MapObject.User.Level;
            MirClass job = Inspect ? InspectDialog.Class : MapObject.User.Class;

            if (hero)
            {
                level = GameScene.Hero.Level;
                job = GameScene.Hero.Class;
            }

            HoverItem = item;
            ItemInfo realItem = Functions.GetRealItem(item.Info, level, job, ItemInfoList);
            ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);
            if (realItem.Type == ItemType.Shield)
            {
                string text = string.Format("Current Level : {0}\nEXP : {1}/{2}", HoverItem.ShieldLevel, HoverItem.ShieldExp, HoverItem.NeedShieldExp);
                if (text.Length > 0)
                {
                    MirLabel GEMLabel = new MirLabel
                    {
                        AutoSize = true,
                        ForeColour = Color.White,
                        Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                        OutLine = true,
                        Parent = ItemLabel,
                        Text = text
                    };

                    ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, GEMLabel.DisplayRectangle.Right + 4),
                        Math.Max(ItemLabel.Size.Height, GEMLabel.DisplayRectangle.Bottom));

                    ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

                    #region OUTLINE
                    MirControl outLine = new MirControl
                    {
                        BackColour = Color.FromArgb(255, 50, 50, 50),
                        Border = true,
                        BorderColour = Color.Gray,
                        NotControl = true,
                        Parent = ItemLabel,
                        Opacity = 0.4F,
                        Location = new Point(0, 0)
                    };
                    outLine.Size = ItemLabel.Size;
                    return outLine;
                }
                else
                {
                    ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height - 4);
                    return null;
                }
                #endregion
            }
            else
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height - 4);
                return null;
            }
        }

        public MirControl StoryInfoLabel(UserItem item, bool Inspect = false, bool hero = false)
        {
            ushort level = Inspect ? InspectDialog.Level : MapObject.User.Level;
            MirClass job = Inspect ? InspectDialog.Class : MapObject.User.Class;

            if (hero)
            {
                level = GameScene.Hero.Level;
                job = GameScene.Hero.Class;
            }

            HoverItem = item;
            ItemInfo realItem = Functions.GetRealItem(item.Info, level, job, ItemInfoList);

            ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

            int count = 0;

            #region TOOLTIP

            if (realItem.Type == ItemType.Pets && realItem.Shape == 26)//Dynamic wonderDrug
            {
                string strTime = Functions.PrintTimeSpanFromSeconds((HoverItem.CurrentDura * 60), false);
                switch ((int)realItem.Effect)
                {
                    case 0://exp low/med/high
                        HoverItem.Info.ToolTip = string.Format("Increase experience gained by {0}% for {1}.", HoverItem.Luck + realItem.Luck, strTime);
                        break;
                    case 1://drop low/med/high
                        HoverItem.Info.ToolTip = string.Format("Increase droprate by {0}% for {1}.", HoverItem.Luck + realItem.Luck, strTime);
                        break;
                    case 2://hp low/med/high
                        HoverItem.Info.ToolTip = string.Format("Increase MaxHP +{0} for {1}.", HoverItem.HP + realItem.HP, strTime);
                        break;
                    case 3://mp low/med/high
                        HoverItem.Info.ToolTip = string.Format("Increase MaxMP +{0} for {1}.", HoverItem.MP + realItem.MP, strTime);
                        break;
                    case 4://ac low/med/high
                        HoverItem.Info.ToolTip = string.Format("Increase AC {0}-{0} for {1}.", HoverItem.AC + realItem.MaxAC, strTime);
                        break;
                    case 5://amc low/med/high
                        HoverItem.Info.ToolTip = string.Format("Increase AMC {0}-{0} for {1}.", HoverItem.MAC + realItem.MaxAC, strTime);
                        break;
                    case 6://speed low/med/high
                        HoverItem.Info.ToolTip = string.Format("Increase AttackSpeed by {0} for {1}.", HoverItem.AttackSpeed + realItem.AttackSpeed, strTime);
                        break;
                }
            }

            if (realItem.Type == ItemType.Scroll && realItem.Shape == 7)//Credit Scroll
            {
                HoverItem.Info.ToolTip = string.Format("Adds {0} Credits to your Account.", HoverItem.Info.Price);
            }

            if (!string.IsNullOrEmpty(HoverItem.Info.ToolTip))
            {
                count++;
                MirLabel TOOLTIPLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Goldenrod,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = HoverItem.Info.ToolTip
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, TOOLTIPLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, TOOLTIPLabel.DisplayRectangle.Bottom));
            }

            #endregion
     
            if (count > 0)
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

                #region OUTLINE
                MirControl outLine = new MirControl
                {
                    BackColour = Color.FromArgb(255, 50, 50, 50),
                    Border = true,
                    BorderColour = Color.Gray,
                    NotControl = true,
                    Parent = ItemLabel,
                    Opacity = 0.4F,
                    Location = new Point(0, 0)
                };
                outLine.Size = ItemLabel.Size;
                #endregion

                return outLine;
            }
            else
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height - 4);
            }
            return null;
        }

        #region SocketLabel
        public MirControl SocketInfoLabel(UserItem item, bool Inspect = false, bool hero = false)
        {
            ushort level = Inspect ? InspectDialog.Level : MapObject.User.Level;
            MirClass job = Inspect ? InspectDialog.Class : MapObject.User.Class;
            Hover_Item = item;
            Hover_Item = item;
            ItemInfo realItem = Functions.GetRealItem(item.Info, level, job, Item_Info_List);
            if (realItem.Type != ItemType.Gem)
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);
                int scount = 0;
                string temp = "";
                //  Item has sockets but nothing in them, draw all empty sockets.            
                if (item.SocketCount > 0 && item.Sockets.Count == 0)
                {
                    temp = string.Format("({0}) Sockets", item.SocketCount);
                    MirLabel socketLabel = new MirLabel
                    {
                        AutoSize = true,
                        ForeColour = GradeNameColor(Hover_Item.Info.Grade),
                        Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                        OutLine = true,
                        Parent = ItemLabel,
                        Text = temp
                    };
                    ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, socketLabel.DisplayRectangle.Right + 4),
                        Math.Max(ItemLabel.Size.Height, socketLabel.DisplayRectangle.Bottom + 4));
                    for (int i = 1; i < item.SocketCount + 1; i++)
                    {
                        MirImageControl imgTest = new MirImageControl
                        {
                            Parent = ItemLabel,
                            Library = Libraries.EdensEliteInter,
                            Index = 2,
                            Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                            DrawImage = true,
                            NotControl = true,
                        };

                        ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, imgTest.DisplayRectangle.Right + 4),
                            Math.Max(ItemLabel.Size.Height, imgTest.DisplayRectangle.Bottom + 4));
                    }
                    ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);
                    scount++;
                }
                //  Item has sockets and has items socketed.
                else if (item.SocketCount > 0 && item.Sockets.Count > 0)
                {
                    temp = string.Format("({0}) Sockets", item.SocketCount);
                    MirLabel socketLabel = new MirLabel
                    {
                        AutoSize = true,
                        ForeColour = GradeNameColor(Hover_Item.Info.Grade),
                        Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                        OutLine = true,
                        Parent = ItemLabel,
                        Text = temp
                    };
                    ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, socketLabel.DisplayRectangle.Right + 4),
                        Math.Max(ItemLabel.Size.Height, socketLabel.DisplayRectangle.Bottom + 4));
                    bool item0 = false;
                    string item0Name = "";
                    int item0Index = 2;
                    bool item1 = false;
                    string item1Name = "";
                    int item1Index = 2;
                    bool item2 = false;
                    string item2Name = "";
                    int item2Index = 2;
                    for (int i = 0; i < item.Sockets.Count; i++)
                    {
                        if (item.Sockets[i].Slot == 1)
                        {
                            item0Name = item.Sockets[i].SocketItemName;
                            item0 = true;
                            switch (item.Sockets[i].SocketItemType)
                            {
                                case SocketType.DamageIncreasePvE:
                                case SocketType.DamageIncreasePvP:
                                case SocketType.DamageReductionPvE:
                                case SocketType.DamageReductionPvP:
                                    item0Index = 3;
                                    break;
                                case SocketType.DestructionBonus:
                                case SocketType.MagicBonus:
                                case SocketType.SpiritBonus:
                                    item0Index = 5;
                                    break;
                                case SocketType.Enrage:
                                case SocketType.PinPoint:
                                case SocketType.IronWall:
                                case SocketType.Evasive:
                                case SocketType.SpeedyMagician:
                                    item0Index = 4;
                                    break;
                                case SocketType.MeleeDamageBonus:
                                case SocketType.MagicDamageBonus:
                                case SocketType.SpiritualBonus:
                                    item0Index = 6;
                                    break;
                                default:
                                    item0Index = 2;
                                    break;
                            }
                        }
                        if (item.Sockets[i].Slot == 2)
                        {
                            item1Name = item.Sockets[i].SocketItemName;
                            item1 = true;
                            switch (item.Sockets[i].SocketItemType)
                            {
                                case SocketType.DamageIncreasePvE:
                                case SocketType.DamageIncreasePvP:
                                case SocketType.DamageReductionPvE:
                                case SocketType.DamageReductionPvP:
                                    item1Index = 3;
                                    break;
                                case SocketType.DestructionBonus:
                                case SocketType.MagicBonus:
                                case SocketType.SpiritBonus:
                                    item1Index = 5;
                                    break;
                                case SocketType.Enrage:
                                case SocketType.PinPoint:
                                case SocketType.IronWall:
                                case SocketType.Evasive:
                                case SocketType.SpeedyMagician:
                                    item1Index = 4;
                                    break;
                                case SocketType.MeleeDamageBonus:
                                case SocketType.MagicDamageBonus:
                                case SocketType.SpiritualBonus:
                                    item1Index = 6;
                                    break;
                                default:
                                    item1Index = 2;
                                    break;
                            }
                        }
                        if (item.Sockets[i].Slot == 3)
                        {
                            item2Name = item.Sockets[i].SocketItemName;
                            item2 = true;
                            switch (item.Sockets[i].SocketItemType)
                            {
                                case SocketType.DamageIncreasePvE:
                                case SocketType.DamageIncreasePvP:
                                case SocketType.DamageReductionPvE:
                                case SocketType.DamageReductionPvP:
                                    item2Index = 3;
                                    break;
                                case SocketType.DestructionBonus:
                                case SocketType.MagicBonus:
                                case SocketType.SpiritBonus:
                                    item2Index = 5;
                                    break;
                                case SocketType.Enrage:
                                case SocketType.PinPoint:
                                case SocketType.IronWall:
                                case SocketType.Evasive:
                                case SocketType.SpeedyMagician:
                                    item2Index = 4;
                                    break;
                                case SocketType.MeleeDamageBonus:
                                case SocketType.MagicDamageBonus:
                                case SocketType.SpiritualBonus:
                                    item2Index = 6;
                                    break;
                                default:
                                    item2Index = 2;
                                    break;
                            }
                        }
                    }

                    if (item0)
                    {
                        MirImageControl imgTest = new MirImageControl
                        {
                            Parent = ItemLabel,
                            Library = Libraries.EdensEliteInter,
                            Index = item0Index,
                            Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                            DrawImage = true,
                            NotControl = true,
                        };

                        ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, imgTest.DisplayRectangle.Right + 4),
                            Math.Max(ItemLabel.Size.Height, imgTest.DisplayRectangle.Bottom + 4));

                        temp = string.Format("{0}", item0Name);
                        MirLabel runeLabel = new MirLabel
                        {
                            AutoSize = true,
                            ForeColour = GradeNameColor(Hover_Item.Info.Grade),
                            Location = new Point(imgTest.Location.X + imgTest.Size.Width + 4, imgTest.Location.Y),
                            OutLine = true,
                            Parent = ItemLabel,
                            Text = temp
                        };
                        ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, runeLabel.DisplayRectangle.Right + 4),
                            Math.Max(ItemLabel.Size.Height, runeLabel.DisplayRectangle.Bottom + 4));
                    }
                    else
                    {
                        MirImageControl imgTest = new MirImageControl
                        {
                            Parent = ItemLabel,
                            Library = Libraries.EdensEliteInter,
                            Index = item0Index,
                            Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                            DrawImage = true,
                            NotControl = true,
                        };

                        ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, imgTest.DisplayRectangle.Right + 4),
                            Math.Max(ItemLabel.Size.Height, imgTest.DisplayRectangle.Bottom + 4));
                    }
                    if (item1 && item.SocketCount >= 2)
                    {
                        MirImageControl imgTest = new MirImageControl
                        {
                            Parent = ItemLabel,
                            Library = Libraries.EdensEliteInter,
                            Index = item1Index,
                            Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                            DrawImage = true,
                            NotControl = true,
                        };

                        ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, imgTest.DisplayRectangle.Right + 4),
                            Math.Max(ItemLabel.Size.Height, imgTest.DisplayRectangle.Bottom + 4));

                        temp = string.Format("{0}", item1Name);
                        MirLabel runeLabel = new MirLabel
                        {
                            AutoSize = true,
                            ForeColour = GradeNameColor(Hover_Item.Info.Grade),
                            Location = new Point(imgTest.Location.X + imgTest.Size.Width + 4, imgTest.Location.Y),
                            OutLine = true,
                            Parent = ItemLabel,
                            Text = temp
                        };
                        ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, runeLabel.DisplayRectangle.Right + 4),
                            Math.Max(ItemLabel.Size.Height, runeLabel.DisplayRectangle.Bottom + 4));
                    }
                    else
                    {
                        if (item.SocketCount >= 2)
                        {
                            MirImageControl imgTest = new MirImageControl
                            {
                                Parent = ItemLabel,
                                Library = Libraries.EdensEliteInter,
                                Index = item1Index,
                                Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                                DrawImage = true,
                                NotControl = true,
                            };

                            ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, imgTest.DisplayRectangle.Right + 4),
                                Math.Max(ItemLabel.Size.Height, imgTest.DisplayRectangle.Bottom + 4));
                        }
                    }

                    if (item2 && item.SocketCount >= 3)
                    {
                        MirImageControl imgTest = new MirImageControl
                        {
                            Parent = ItemLabel,
                            Library = Libraries.EdensEliteInter,
                            Index = item2Index,
                            Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                            DrawImage = true,
                            NotControl = true,
                        };

                        ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, imgTest.DisplayRectangle.Right + 4),
                            Math.Max(ItemLabel.Size.Height, imgTest.DisplayRectangle.Bottom + 4));

                        temp = string.Format("{0}", item2Name);
                        MirLabel runeLabel = new MirLabel
                        {
                            AutoSize = true,
                            ForeColour = GradeNameColor(Hover_Item.Info.Grade),
                            Location = new Point(imgTest.Location.X + imgTest.Size.Width + 4, imgTest.Location.Y),
                            OutLine = true,
                            Parent = ItemLabel,
                            Text = temp
                        };
                        ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, runeLabel.DisplayRectangle.Right + 4),
                            Math.Max(ItemLabel.Size.Height, runeLabel.DisplayRectangle.Bottom + 4));
                    }
                    else
                    {
                        if (item.SocketCount >= 3)
                        {
                            MirImageControl imgTest = new MirImageControl
                            {
                                Parent = ItemLabel,
                                Library = Libraries.EdensEliteInter,
                                Index = item2Index,
                                Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                                DrawImage = true,
                                NotControl = true,
                            };

                            ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, imgTest.DisplayRectangle.Right + 4),
                                Math.Max(ItemLabel.Size.Height, imgTest.DisplayRectangle.Bottom + 4));
                        }
                    }

                    scount++;
                }

                if (scount > 0)
                {
                    MirControl outLine = new MirControl
                    {
                        BackColour = Color.FromArgb(255, 50, 50, 50),
                        Border = true,
                        BorderColour = Color.Gray,
                        NotControl = true,
                        Parent = ItemLabel,
                        Opacity = 0.4F,
                        Location = new Point(0, 0)
                    };
                    outLine.Size = ItemLabel.Size;
                    return outLine;
                }
                else
                {
                    ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height - 4);
                }
            }
            else
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height - 4);
            }
            return null;
        }
#endregion

        public void CreateItemLabel(UserItem item, bool Inspect = false, bool hero = false, bool shopSell = false)
        {
            if (item == null)
            {
                DisposeItemLabel();
                HoverItem = null;
                return;
            }

            if (item == HoverItem && 
                ItemLabel != null && 
                !ItemLabel.IsDisposed) return;
            ushort level = Inspect ? InspectDialog.Level : MapObject.User.Level;
            MirClass job = Inspect ? InspectDialog.Class : MapObject.User.Class;

            if (hero)
            {
                level = GameScene.Hero.Level;
                job = GameScene.Hero.Class;
            }
            HoverItem = item;
            ItemInfo realItem = Functions.GetRealItem(item.Info, level, job, ItemInfoList);

            ItemLabel = new MirControl
            {
                BackColour = Color.FromArgb(255, 50, 50, 50),
                Border = true,
                BorderColour = Color.Gray,
                DrawControlTexture = true,
                NotControl = true,
                Parent = this,
                Opacity = 0.7F,
              //  Visible = false
            };

            //Name Info Label
            MirControl[] outlines = new MirControl[12];
            outlines[0] = NameInfoLabel(item, Inspect,hero);
            //Attribute Info1 Label - Attack Info OR Rune stats
            ItemInfo tmp = GetInfo(item.ItemIndex);
            if (tmp != null && 
                tmp.Type != ItemType.RuneStone)
                outlines[1] = AttackInfoLabel(item, Inspect,hero);
            else if (tmp != null &&
                tmp.Type == ItemType.RuneStone)
                outlines[1] = RuneStatLavel(item, Inspect, hero);
            //Attribute Info2 Label - Defence Info
            if (tmp != null &&
                tmp.Type != ItemType.RuneStone)
                outlines[2] = DefenceInfoLabel(item, Inspect,hero);
            //Attribute Info3 Label - Weight Info
            if (tmp != null &&
                tmp.Type != ItemType.RuneStone)
                outlines[3] = WeightInfoLabel(item, Inspect, hero);
            //Awake Info Label
            if (tmp != null &&
                tmp.Type != ItemType.RuneStone)
                outlines[4] = AwakeInfoLabel(item, Inspect, hero);
            //need Info Label
            if (tmp != null &&
                tmp.Type != ItemType.RuneStone)
                outlines[5] = NeedInfoLabel(item, Inspect, hero, shopSell);
            //Bind Info Label
            outlines[6] = BindInfoLabel(item, Inspect, hero);
            //Overlap Info Label
            outlines[7] = OverlapInfoLabel(item, Inspect, hero);
            //Story Label
            outlines[8] = StoryInfoLabel(item, Inspect, hero);

            outlines[9] = ShieldInfoLabel(item, Inspect, hero);
            outlines[10] = SocketInfoLabel(item, Inspect, hero);
            outlines[11] = ElementItemLabel(item, Inspect, hero);

            foreach (var outline in outlines)
            {
                if (outline != null)
                {
                    outline.Size = new Size(ItemLabel.Size.Width, outline.Size.Height);
                }
            }

            //ItemLabel.Visible = true;
        }

        #region Rune Stone Labels
        public MirControl RuneStatLavel(UserItem item, bool Inspect = false, bool hero = false)
        {

            ushort level = Inspect ? InspectDialog.Level : MapObject.User.Level;
            MirClass job = Inspect ? InspectDialog.Class : MapObject.User.Class;
            HoverItem = item;

            if (hero)
            {
                level = GameScene.Hero.Level;
                job = GameScene.Hero.Class;
            }

            ItemInfo realItem = Functions.GetRealItem(item.Info, level, job, ItemInfoList);

            ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);
            string statDescription = string.Format("");
            int count = 0;
            //  Passive & New stat runes
            if (realItem.Type == ItemType.RuneStone &&
                realItem.Shape >= 0 &&
                realItem.Shape < 255)
            {
                switch((SocketType)realItem.Shape)
                {
                    case SocketType.DamageReductionPvE:
                        statDescription = string.Format("PvE Damage Reduction {0}%", realItem.MaxMAC);
                        break;
                    case SocketType.DamageReductionPvP:
                        statDescription = string.Format("PvP Damage Reduction {0}%", realItem.MaxAC);
                        break;
                    case SocketType.DamageIncreasePvE:
                        statDescription = string.Format("PvE Damage Increase {0}%", realItem.MinAC);
                        break;
                    case SocketType.DamageIncreasePvP:
                        statDescription = string.Format("PvE Damage Increase {0}%", realItem.MinMAC);
                        break;
                    case SocketType.MeleeDamageBonus:
                        statDescription = string.Format("Melee Damage Bonus {0}%", realItem.MaxAcRate);
                        break;
                    case SocketType.MagicDamageBonus:
                        statDescription = string.Format("Magic Damage Bonus {0}%", realItem.MaxMacRate);
                        break;
                    case SocketType.SpiritualBonus:
                        statDescription = string.Format("Spiritual Damage Bonus {0}%", realItem.Holy);
                        break;
                    //  Flat bonus
                    case SocketType.HealthBonus:
                        statDescription = string.Format("HP + {0}", realItem.HP);
                        break;
                    case SocketType.ManaBonus:
                        statDescription = string.Format("MP + {0}", realItem.MP);
                        break;
                    //  % Based
                    case SocketType.HealthRegenBonus:
                        statDescription = string.Format("HP Recovery + {0}", realItem.HPrate);
                        break;
                    case SocketType.ManaRegenBonus:
                        statDescription = string.Format("MP Recovery + {0}", realItem.MPrate);
                        break;
                    //  Flat bonus
                    case SocketType.DestructionBonus:
                        statDescription = string.Format("DC + {0}-{1}", realItem.MinDC, realItem.MaxDC);
                        break;
                    case SocketType.MagicBonus:
                        statDescription = string.Format("MC + {0}-{1}", realItem.MinMC, realItem.MaxMC);
                        break;
                    case SocketType.SpiritBonus:
                        statDescription = string.Format("SC + {0}-{1}", realItem.MinSC, realItem.MaxSC);
                        break;
                    //  Small Accuracy Boost
                    case SocketType.PinPoint:
                        statDescription = string.Format("Chance to trigger : {0}%\r\nAccuracy + {1}\r\nMax DC + {2}\r\nCrit Rate + {3}\r\nCrit Damage + {4}\r\nDuration : {5}\r\nCool-Down : {6}",
                            realItem.Weight, realItem.Accuracy, realItem.MaxDC, realItem.CriticalRate, realItem.CriticalDamage, realItem.Durability, realItem.RequiredAmount);
                        break;
                    //  Small Agility Boost
                    case SocketType.Evasive:
                        statDescription = string.Format("Chance to trigger : {0}%\r\nMax AC + {1}\r\nMax AMC + {2}\r\nAgility + {3}\r\nDuration : {4}\r\nCool-Down : {5}",
                            realItem.Weight, realItem.MaxAC, realItem.MaxMAC, realItem.Agility, realItem.Durability, realItem.RequiredAmount);
                        break;
                    //  Small Attack Speed & Crit bonus.
                    case SocketType.Enrage:
                        statDescription = string.Format("Chance to trigger : {0}%\r\nA.Speed + {1}\r\nMax DC + {2}\r\nCrit Rate + {3}\r\nCrit Damage + {4}\r\nDuration : {5}\r\nCool-Down : {6}",
                            realItem.Weight, realItem.AttackSpeed, realItem.MaxDC, realItem.CriticalRate, realItem.CriticalDamage, realItem.Durability, realItem.RequiredAmount);
                        break;
                    //  Decent Defense.
                    case SocketType.IronWall:
                        statDescription = string.Format("Chance to trigger : {0}%\r\nAC + {1}-{2}\r\nMAC + {1}-{2}\r\nHP + {3}\r\nAgility + {4}\r\nDuration : {5}\r\nCool-Down : {6}",
                            realItem.Weight, realItem.MinAC, realItem.MaxAC, realItem.HP, realItem.Agility, realItem.Durability, realItem.RequiredAmount);
                        break;
                    //  Speedy magician
                    case SocketType.SpeedyMagician:
                        statDescription = string.Format("Chance to trigger : {0}%\r\nCool-Down reduction : {1}%\r\nDuration : {2}\r\nCool-Down : {3}",
                            realItem.Weight, realItem.MinMC, realItem.Durability, realItem.RequiredAmount);
                        break;
                }
                if (statDescription.Length > 0)
                    count = 1;
            }
            //  Stat type rune
            else
            {
                if (realItem.MinAC > 0 ||
                    realItem.MaxAC > 0)
                {
                    statDescription += realItem.MinAC > 0 ? string.Format("AC {0}", realItem.MinAC) : "0";
                    statDescription += realItem.MaxAC > 0 ? string.Format(" - {0}", realItem.MaxAC) : "0";
                }
                if (realItem.MinMAC > 0 ||
                    realItem.MaxMAC > 0)
                {
                    if (statDescription.Length > 0)
                        statDescription += "\r\n";
                    statDescription += realItem.MinMAC > 0 ? string.Format("MAC {0}", realItem.MinMAC) : "0";
                    statDescription += realItem.MaxMAC > 0 ? string.Format(" - {0}", realItem.MaxMAC) : "0";
                }
                if (realItem.MinDC > 0 ||
                    realItem.MaxDC > 0)
                {
                    if (statDescription.Length > 0)
                        statDescription += "\r\n";
                    statDescription += realItem.MinDC > 0 ? string.Format("DC {0}", realItem.MinDC) : "0";
                    statDescription += realItem.MaxDC > 0 ? string.Format(" - {0}", realItem.MaxDC) : "0";
                }
                if (realItem.MinMC > 0 ||
                    realItem.MaxMC > 0)
                {
                    if (statDescription.Length > 0)
                        statDescription += "\r\n";
                    statDescription += realItem.MinMC > 0 ? string.Format("MC {0}", realItem.MinMC) : "0";
                    statDescription += realItem.MaxMC > 0 ? string.Format(" - {0}", realItem.MaxMC) : "0";
                }
                if (realItem.MinSC > 0 ||
                    realItem.MaxSC > 0)
                {
                    if (statDescription.Length > 0)
                        statDescription += "\r\n";
                    statDescription += realItem.MinSC > 0 ? string.Format("SC {0}", realItem.MinSC) : "0";
                    statDescription += realItem.MaxSC > 0 ? string.Format(" - {0}", realItem.MaxSC) : "0";
                }
                if (realItem.Accuracy > 0)
                {
                    if (statDescription.Length > 0)
                        statDescription += "\r\n";
                    statDescription += realItem.Accuracy > 0 ? string.Format("Accuracy {0}", realItem.Accuracy) : "";
                }
                if (realItem.Agility > 0)
                {
                    if (statDescription.Length > 0)
                        statDescription += "\r\n";
                    statDescription += realItem.Agility > 0 ? string.Format("Agility {0}", realItem.Agility) : "";
                }
                if (realItem.HP > 0)
                {
                    if (statDescription.Length > 0)
                        statDescription += "\r\n";
                    statDescription += realItem.HP > 0 ? string.Format("HP {0}", realItem.HP) : "";
                }
                if (realItem.MP > 0)
                {
                    if (statDescription.Length > 0)
                        statDescription += "\r\n";
                    statDescription += realItem.MP > 0 ? string.Format("MP {0}", realItem.MP) : "";
                }
                if (realItem.HPrate > 0)
                {
                    if (statDescription.Length > 0)
                        statDescription += "\r\n";
                    statDescription += realItem.HPrate > 0 ? string.Format("HP Recovery {0}", realItem.HPrate) : "";
                }
                if (realItem.MPrate > 0)
                {
                    if (statDescription.Length > 0)
                        statDescription += "\r\n";
                    statDescription += realItem.MPrate > 0 ? string.Format("MP Recovery {0}", realItem.MPrate) : "";
                }
                if (statDescription.Length > 0)
                    count = 1;
            }

            if (count > 0)
            {
                MirLabel RuneDescription = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Khaki,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = statDescription
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, RuneDescription.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, RuneDescription.DisplayRectangle.Bottom));

                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

                #region OUTLINE
                MirControl outLine = new MirControl
                {
                    BackColour = Color.FromArgb(255, 50, 50, 50),
                    Border = true,
                    BorderColour = Color.Gray,
                    NotControl = true,
                    Parent = ItemLabel,
                    Opacity = 0.4F,
                    Location = new Point(0, 0)
                };
                outLine.Size = ItemLabel.Size;
                #endregion

                return outLine;
            }
            else
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height - 4);
            }
            return null;
        }
        #endregion

        public MirControl ElementItemLabel(UserItem item, bool Inspect = false, bool hero = false)
        {
            ushort level = Inspect ? InspectDialog.Level : MapObject.User.Level;
            MirClass job = Inspect ? InspectDialog.Class : MapObject.User.Class;
            HoverItem = item;
            ItemInfo realItem = Functions.GetRealItem(item.Info, level, job, ItemInfoList);

            ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

            int count = 0;

            #region TOOLTIP
            string temp = "";
            if (realItem.PositiveElement != ElementPos.None && realItem.PositiveElementAmount > 0)
            {
                if (HoverItem.ElementPositive != ElementPos.None)
                {
                    if ((byte)HoverItem.ElementPositive == (byte)realItem.PositiveElement)
                        temp += string.Format("[Positive Element(s)]\r\n{0} + {1}.\r\n", realItem.PositiveElement.ToString(), realItem.PositiveElementAmount + HoverItem.PositiveElementAdded);
                    else
                        temp += string.Format("[Positive Element(s)]\r\n{0} + {1}\r\n{2} + {3}\r\n", realItem.PositiveElement.ToString(), realItem.PositiveElementAmount, HoverItem.ElementPositive.ToString(), HoverItem.PositiveElementAdded);
                }
                else
                    temp += string.Format("[Positive Element(s)]\r\n{0} + {1}\r\n", realItem.PositiveElement.ToString(), realItem.PositiveElementAmount);
                count++;
            }
            else if (HoverItem.ElementPositive != ElementPos.None && HoverItem.PositiveElementAdded > 0)
            {
                temp += string.Format("[Positive Element(s)]\r\n{0} + {1}\r\n", HoverItem.ElementPositive.ToString(), HoverItem.PositiveElementAdded);
                count++;
            }

            if (realItem.NegativeElement != ElementNeg.None && realItem.NegativeElementAmount > 0)
            {
                if (HoverItem.ElementNegative != ElementNeg.None)
                {
                    if ((byte)HoverItem.ElementNegative == (byte)realItem.NegativeElement)
                        temp += string.Format("[Negative Element(s)]\r\n{0} + {1}.", realItem.NegativeElement.ToString(), realItem.NegativeElementAmount + HoverItem.NegativeElementAdded);
                    else
                        temp += string.Format("[Negative Element(s)]\r\n{0} + {1}\r\n{2} + {3}", realItem.NegativeElement.ToString(), realItem.NegativeElementAmount, HoverItem.ElementNegative.ToString(), HoverItem.NegativeElementAdded);
                }
                else
                    temp += string.Format("[Negative Element(s)]\r\n{0} + {1}", realItem.NegativeElement.ToString(), realItem.NegativeElementAmount);
                count++;
            }
            else if (HoverItem.ElementNegative != ElementNeg.None && HoverItem.NegativeElementAdded > 0)
            {
                temp += string.Format("[Negative Element(s)]\r\n{0} + {1}", HoverItem.ElementNegative.ToString(), HoverItem.NegativeElementAdded);
                count++;
            }
            #endregion



            if (count > 0)
            {
                MirLabel TOOLTIPLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Khaki,
                    Location = new Point(4, ItemLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = ItemLabel,
                    Text = temp
                };

                ItemLabel.Size = new Size(Math.Max(ItemLabel.Size.Width, TOOLTIPLabel.DisplayRectangle.Right + 4),
                    Math.Max(ItemLabel.Size.Height, TOOLTIPLabel.DisplayRectangle.Bottom));

                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height + 4);

                #region OUTLINE
                MirControl outLine = new MirControl
                {
                    BackColour = Color.FromArgb(255, 50, 50, 50),
                    Border = true,
                    BorderColour = Color.Gray,
                    NotControl = true,
                    Parent = ItemLabel,
                    Opacity = 0.4F,
                    Location = new Point(0, 0)
                };
                outLine.Size = ItemLabel.Size;
                #endregion

                return outLine;
            }
            else
            {
                ItemLabel.Size = new Size(ItemLabel.Size.Width, ItemLabel.Size.Height - 4);
            }
            return null;
        }

        public void CreateMailLabel(ClientMail mail)
        {
            if (mail == null)
            {
                DisposeMailLabel();
                return;
            }

            if (MailLabel != null && !MailLabel.IsDisposed) return;

            MailLabel = new MirControl
            {
                BackColour = Color.FromArgb(255, 50, 50, 50),
                Border = true,
                BorderColour = Color.Gray,
                DrawControlTexture = true,
                NotControl = true,
                Parent = this,
                Opacity = 0.7F
            };

            MirLabel nameLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Yellow,
                Location = new Point(4, 4),
                OutLine = true,
                Parent = MailLabel,
                Text = mail.SenderName
            };

            MailLabel.Size = new Size(Math.Max(MailLabel.Size.Width, nameLabel.DisplayRectangle.Right + 4),
                Math.Max(MailLabel.Size.Height, nameLabel.DisplayRectangle.Bottom));

            MirLabel dateLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.White,
                Location = new Point(4, MailLabel.DisplayRectangle.Bottom),
                OutLine = true,
                Parent = MailLabel,
                Text = "Date Sent : " + mail.DateSent.ToString("dd/MM/yy H:mm:ss")
            };

            MailLabel.Size = new Size(Math.Max(MailLabel.Size.Width, dateLabel.DisplayRectangle.Right + 4),
                Math.Max(MailLabel.Size.Height, dateLabel.DisplayRectangle.Bottom));

            if (mail.Gold > 0)
            {
                MirLabel goldLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.White,
                    Location = new Point(4, MailLabel.DisplayRectangle.Bottom),
                    OutLine = true,
                    Parent = MailLabel,
                    Text = "Gold: " + mail.Gold
                };

                MailLabel.Size = new Size(Math.Max(MailLabel.Size.Width, goldLabel.DisplayRectangle.Right + 4),
                Math.Max(MailLabel.Size.Height, goldLabel.DisplayRectangle.Bottom));
            }

            MirLabel openedLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Red,
                Location = new Point(4, MailLabel.DisplayRectangle.Bottom),
                OutLine = true,
                Parent = MailLabel,
                Text = mail.Opened ? "[Old]" : "[New]"
            };

            MailLabel.Size = new Size(Math.Max(MailLabel.Size.Width, openedLabel.DisplayRectangle.Right + 4),
            Math.Max(MailLabel.Size.Height, openedLabel.DisplayRectangle.Bottom));
        }
        public void CreateMemoLabel(ClientFriend friend)
        {
            if (friend == null)
            {
                DisposeMemoLabel();
                return;
            }

            if (MemoLabel != null && !MemoLabel.IsDisposed) return;

            MemoLabel = new MirControl
            {
                BackColour = Color.FromArgb(255, 50, 50, 50),
                Border = true,
                BorderColour = Color.Gray,
                DrawControlTexture = true,
                NotControl = true,
                Parent = this,
                Opacity = 0.7F
            };

            MirLabel memoLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.White,
                Location = new Point(4, 4),
                OutLine = true,
                Parent = MemoLabel,
                Text = Functions.StringOverLines(friend.Memo, 5, 20)
            };

            MemoLabel.Size = new Size(Math.Max(MemoLabel.Size.Width, memoLabel.DisplayRectangle.Right + 4),
                Math.Max(MemoLabel.Size.Height, memoLabel.DisplayRectangle.Bottom + 4));
        }

        public static ItemInfo GetInfo(int index)
        {
            for (int i = 0; i < ItemInfoList.Count; i++)
            {
                ItemInfo info = ItemInfoList[i];
                if (info.Index != index) continue;
                return info;
            }

            return null;
        }

        public string GetUserName(uint id)
        {
            for (int i = 0; i < UserIdList.Count; i++)
            {
                UserId who = UserIdList[i];
                if (id == who.Id)
                    return who.UserName;
            }
            Network.Enqueue(new C.RequestUserName { UserID = id });
            UserIdList.Add(new UserId() { Id = id, UserName = "Unknown" });
            return "";
        }

        public class OutPutMessage
        {
            public string Message;
            public long ExpireTime;
            public OutputMessageType Type;
        }

        public void HeroRanks(S.HeroRanking p)
        {
            
            HeroRankingDialog.RankList = p.list;
            HeroRankingDialog.UpdateInterface();            
        }

        public void Rankings(S.Rankings p)
        {
            RankingDialog.RecieveRanks(p.Listings, p.RankType, p.MyRank);
        }

        public void Opendoor(S.Opendoor p)
        {
            MapControl.OpenDoor(p.DoorIndex, p.Close);
        }

        #region Disposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Scene = null;
                User = null;

                MoveTime = 0;
                AttackTime = 0;
                NextRunTime = 0;
                LastRunTime = 0;
                CanMove = false;
                CanRun = false;

                MapControl = null;
                MainDialog = null;
                ChatDialog = null;
                ChatControl = null;
                InventoryDialog = null;
                CharacterDialog = null;
                StorageDialog = null;
                BeltDialog = null;
                MiniMapDialog = null;
                InspectDialog = null;
                OptionDialog = null;
                MenuDialog = null;
                NPCDialog = null;
                QuestDetailDialog = null;
                QuestListDialog = null;
                QuestLogDialog = null;
                QuestTrackingDialog = null;
                GameShopDialog = null;
                MentorDialog = null;
                HeroDialog = null;
                HeroInventoryDialog = null;

                RelationshipDialog = null;
                CharacterDuraPanel = null;
                //DuraStatusPanel = null;
                HeroCharacterDuraPanel = null;

                HoverItem = null;
                SelectedCell = null;
                PickedUpGold = false;

                UseItemTime = 0;
                PickUpTime = 0;
                InspectTime = 0;

                DisposeItemLabel();

                AMode = 0;
                PMode = 0;
                Lights = 0;

                NPCTime = 0;
                NPCID = 0;
                DefaultNPCID = 0;

                for (int i = 0; i < OutputLines.Length; i++)
                    if (OutputLines[i] != null && OutputLines[i].IsDisposed)
                        OutputLines[i].Dispose();

                OutputMessages.Clear();
                OutputMessages = null;
            }

            base.Dispose(disposing);
        }

        #endregion

    }

    public sealed class MapControl : MirControl
    {
        public static List<MapEventClientSide> MapEvents = new List<MapEventClientSide>();
        public static List<MapQuestLocationClient> QuestLocations = new List<MapQuestLocationClient>();
        public static UserObject User
        {
            get { return MapObject.User; }
            set { MapObject.User = value; }
        }

        public static List<MapObject> Objects = new List<MapObject>();

        public const int CellWidth = 48;
        public const int CellHeight = 32;

        public static int OffSetX;
        public static int OffSetY;

        public static int ViewRangeX;
        public static int ViewRangeY;



        public static Point MapLocation
        {
            get { return GameScene.User == null ? Point.Empty : new Point(MouseLocation.X / CellWidth - OffSetX, MouseLocation.Y / CellHeight - OffSetY).Add(GameScene.User.CurrentLocation); }
        }

        public static MouseButtons MapButtons;
        public static Point MouseLocation;
        public static long InputDelay;
        public static long NextAction;

        public CellInfo[,] M2CellInfo;
        public List<Door> Doors = new List<Door>();
        public int Width, Height;

        public string FileName = String.Empty;
        public string Title = String.Empty;
        public ushort MiniMap, BigMap, Music, SetMusic;
        public LightSetting Lights;
        public bool Lightning, Fire;
        public byte MapDarkLight;
        public long LightningTime, FireTime;

        public bool FloorValid, LightsValid;

        private Texture _floorTexture, _lightTexture;
        private Surface _floorSurface, _lightSurface;

        public long OutputDelay;

        private static bool _awakeningAction;
        public static bool AwakeningAction
        {
            get { return _awakeningAction; }
            set
            {
                if (_awakeningAction == value) return;
                _awakeningAction = value; 
            }
        }

        private static bool _autoRun;
        public static bool AutoRun
        {
            get { return _autoRun; }
            set
            {
                if (_autoRun == value) return;
                _autoRun = value;
                if (GameScene.Scene != null)
                    GameScene.Scene.ChatDialog.ReceiveChat(value ? "[AutoRun: On]" : "[AutoRun: Off]", ChatType.Hint);
            }

        }
        public static bool AutoHit;

        public int AnimationCount;
        
        public static List<Effect> Effects = new List<Effect>();

        public MapControl()
        {
            MapButtons = MouseButtons.None;

            OffSetX = Settings.ScreenWidth / 2 / CellWidth;
            OffSetY = Settings.ScreenHeight / 2 / CellHeight - 1;

            ViewRangeX = OffSetX + 4;
            ViewRangeY = OffSetY + 4;

            Size = new Size(Settings.ScreenWidth, Settings.ScreenHeight);
            DrawControlTexture = true;
            BackColour = Color.Black;

            MouseDown += OnMouseDown;
            MouseMove += (o, e) => MouseLocation = e.Location;
            Click += OnMouseClick;
        }

        public void LoadMap()
        {
            GameScene.Scene.NPCDialog.Hide();
            Objects.Clear();
            Effects.Clear();
            Doors.Clear();

            if (User != null)
                Objects.Add(User);



            MapObject.MouseObject = null;
            MapObject.TargetObject = null;
            MapObject.MagicObject = null;
            MapReader Map = new MapReader(FileName);
            M2CellInfo = Map.MapCells;
            Width = Map.Width;
            Height = Map.Height;

            try
            {
                if (SetMusic != Music)
                {
                    SoundManager.Device.Dispose();
                    SoundManager.Create();
                    SoundManager.PlayMusic(Music, true);
                }
            }
            catch (Exception)
            {
                // Do nothing. index was not valid.
            }

            SetMusic = Music;
            SoundList.Music = Music;
        }

        public void Process()
        {
            Processdoors();
            User.Process();

            for (int i = Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = Objects[i];
                if (ob == User) continue;
                //  if (ob.ActionFeed.Count > 0 || ob.Effects.Count > 0 || GameScene.CanMove || CMain.Time >= ob.NextMotion)
                ob.Process();
            }

            for (int i = Effects.Count - 1; i >= 0; i--)
                Effects[i].Process();

            if (MapObject.TargetObject != null && MapObject.TargetObject is MonsterObject && MapObject.TargetObject.AI == 64)
                MapObject.TargetObject = null;
            if (MapObject.MagicObject != null && MapObject.MagicObject is MonsterObject && MapObject.MagicObject.AI == 64)
                MapObject.MagicObject = null;

            CheckInput();


            MapObject bestmouseobject = null;
            for (int y = MapLocation.Y + 2; y >= MapLocation.Y - 2; y--)
            {
                if (y >= Height) continue;
                if (y < 0) break;
                for (int x = MapLocation.X + 2; x >= MapLocation.X - 2; x--)
                {
                    if (x >= Width) continue;
                    if (x < 0) break;
                    CellInfo cell = M2CellInfo[x, y];
                    if (cell.CellObjects == null) continue;

                    for (int i = cell.CellObjects.Count - 1; i >= 0; i--)
                    {
                        MapObject ob = cell.CellObjects[i];
                        if (ob == MapObject.User || !ob.MouseOver(CMain.MPoint)) continue;

                        if (MapObject.MouseObject != ob)
                        {
                            if (ob.Dead)
                            {
                                if (!Settings.TargetDead && GameScene.TargetDeadTime <= CMain.Time) continue;

                                bestmouseobject = ob;
                                //continue;
                            }
                            MapObject.MouseObject = ob;
                            Redraw();
                        }
                        if (bestmouseobject != null && MapObject.MouseObject == null)
                        {
                            MapObject.MouseObject = bestmouseobject;
                            Redraw();
                        }
                        return;
                    }
                }
            }


            if (MapObject.MouseObject != null)
            {
                MapObject.MouseObject = null;
                Redraw();
            }
            
            
        }

        public static MapObject GetObject(uint targetID)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                MapObject ob = Objects[i];
                if (ob.ObjectID != targetID) continue;
                return ob;
            }
            return null;
        }

        public override void Draw()
        {
            //Do nothing.
        }

        protected override void CreateTexture()
        {
            if (User == null) return;

            if (!FloorValid)
                DrawFloor();


            if (ControlTexture != null && !ControlTexture.Disposed && Size != TextureSize)
                ControlTexture.Dispose();

            if (ControlTexture == null || ControlTexture.Disposed)
            {
                DXManager.ControlList.Add(this);
                ControlTexture = new Texture(DXManager.Device, Size.Width, Size.Height, 1, Usage.RenderTarget, Format.A8R8G8B8, Pool.Default);
                ControlTexture.Disposing += ControlTexture_Disposing;
                TextureSize = Size;
            }

            Surface oldSurface = DXManager.CurrentSurface;
            Surface surface = ControlTexture.GetSurfaceLevel(0);
            if (User.Dead)
                DXManager.SetGrayscale(true);
            else
                DXManager.SetGrayscale(false);
            DXManager.SetSurface(surface);
            DXManager.Device.Clear(ClearFlags.Target, BackColour, 0, 0);

            DrawBackground();

            if (FloorValid)
                DXManager.Sprite.Draw2D(_floorTexture, Point.Empty, 0F, Point.Empty, Color.White);

            DrawObjects();

            //Render Death, 

            LightSetting setting = Lights == LightSetting.Normal ? GameScene.Scene.Lights : Lights;
            if (setting != LightSetting.Day)
                DrawLights(setting);

            if (Settings.DropView || GameScene.DropViewTime > CMain.Time)
            {
                for (int i = 0; i < Objects.Count; i++)
                {
                    if (Objects[i] is PlayerObject)
                    {
                        PlayerObject _ob = (PlayerObject)Objects[i];
                        if (_ob.Effects != null &&
                            _ob.Effects.Count > 0)
                        {
                            bool hasBuff = false;
                            if (_ob.Buffs != null && _ob.Buffs.Count >= 1)
                                for (int x = 0; x < _ob.Buffs.Count; x++)
                                    if (_ob.Buffs[x] == BuffType.EnergyShield)
                                        hasBuff = true;

                            if (!hasBuff)
                                if (_ob.Effects != null && _ob.Effects.Count >= 1)
                                    for (int x = 0; x < _ob.Effects.Count; x++)
                                        if (_ob.Effects[x] is BuffEffect bE)
                                            if (bE.BuffType == BuffType.EnergyShield)
                                                bE.Repeat = false;
                        }
                    }
                    ItemObject ob = Objects[i] as ItemObject;
                    if (ob == null) continue;

                    if (!ob.MouseOver(MouseLocation))
                        ob.DrawName();
                }
            }

            if (Settings.BossHPBar)
            {
                if (MapObject.MouseObject != null &&
                    MapObject.MouseObject is MonsterObject tmpM)
                {
                    if (tmpM.IsBoss &&
                        !tmpM.IsSub &&
                        !tmpM.IsPet)
                        tmpM.DrawBossHealthBar();
                }
                /*
                for (int i = 0; i < Objects.Count; i++)
                {
                    if (Objects[i].Race == ObjectType.Monster)
                    {
                        MonsterObject tmp = (MonsterObject)Objects[i];
                        //  It is a Boss and it isn't a Pet
                        if (tmp.IsBoss &&
                            !tmp.IsSub &&
                            !tmp.IsPet)
                            Objects[i].DrawBossHealthBar();
                    }
                }
                */
            }


            GameScene.Scene.BossDialog.Hide();

            //  Target isn't null & not an Item
            if (MapObject.MouseObject != null && 
                !(MapObject.MouseObject is ItemObject))
            {
                
                //  Draw any other Objects Label other than Item
                MapObject.MouseObject.DrawName();

              /* if (MapObject.MouseObject is MonsterObject && CMain.Ctrl)
                {
                    GameScene.Scene.BossDialog.UpdateMob((MapObject.MouseObject));
                    GameScene.Scene.BossDialog.Show();
                }*/

            }

            GameScene.Scene.CreateMonsterLabel(MapObject.MouseObject);

            int offSet = 0;
            if (MapObject.MouseObject != null &&
                MapObject.MouseObject is ItemObject tmpI)
            {
                if (MapObject.MouseObject.CurrentLocation.X >= 0 &&
                    MapObject.MouseObject.CurrentLocation.X <= Height &&
                    MapObject.MouseObject.CurrentLocation.Y >= 0 &&
                    MapObject.MouseObject.CurrentLocation.Y <= Width)
                {
                    CellInfo tmp = M2CellInfo[MapObject.MouseObject.CurrentLocation.X, MapObject.MouseObject.CurrentLocation.Y];
                    if (tmp != null &&
                        tmp.CellObjects != null)
                    {
                        for (int i = 0; i < tmp.CellObjects.Count; i++)
                        {
                            if (tmp.CellObjects[i] is ItemObject tIobj)
                            {
                                tIobj.DrawName(offSet);
                                offSet -= tIobj.NameLabel.Size.Height + (tIobj.NameLabel.Border ? 1 : 0);
                            }
                        }
                    }
                }                
            }
            /*
            for (int i = 0; i < Objects.Count; i++)
            {
                //  Draw the Item Object's label
                ItemObject ob = Objects[i] as ItemObject;
                //  It's not an ITem Object so discontinue
                if (ob == null) continue;
                //  The Mouse is NOT over the object so discontinue
                if (!ob.MouseOver(MouseLocation)) continue;
                //  Draw the Label with the offset
                ob.DrawName(offSet);
                //  Increase the OffSet after each Label Drawn
                offSet -= ob.NameLabel.Size.Height + (ob.NameLabel.Border ? 1 : 0);
            }
            */
            //  The location of the mouse is valid
            if (MapObject.User.MouseOver(MouseLocation))
                //  Draw the Objects Label
                MapObject.User.DrawName();




            DXManager.SetSurface(oldSurface);
            surface.Dispose();
            TextureValid = true;
        }

        protected internal override void DrawControl()
        {
            if (!DrawControlTexture)
                return;

            if (!TextureValid)
                CreateTexture();

            if (ControlTexture == null || ControlTexture.Disposed)
                return;

            float oldOpacity = DXManager.Opacity;

            DXManager.SetOpacity(Opacity);
            DXManager.Sprite.Draw2D(ControlTexture, Point.Empty, 0F, Point.Empty, Color.White);
            DXManager.SetOpacity(oldOpacity);

            CleanTime = CMain.Time + Settings.CleanDelay;
        }

        private void DrawFloor()
        {
            if (_floorTexture == null || _floorTexture.Disposed)
            {
                _floorTexture = new Texture(DXManager.Device, Settings.ScreenWidth, Settings.ScreenHeight, 1, Usage.RenderTarget, Format.A8R8G8B8, Pool.Default);
                _floorTexture.Disposing += FloorTexture_Disposing;
                _floorSurface = _floorTexture.GetSurfaceLevel(0);
            }


            Surface oldSurface = DXManager.CurrentSurface;

            DXManager.SetSurface(_floorSurface);
            DXManager.Device.Clear(ClearFlags.Target, Color.Empty, 0, 0); //Color.Black

            int index;
            int drawY, drawX;

            for (int y = User.Movement.Y - ViewRangeY; y <= User.Movement.Y + ViewRangeY; y++)
            {
                if (y <= 0 || y % 2 == 1) continue;
                if (y >= Height) break;
                drawY = (y - User.Movement.Y + OffSetY) * CellHeight + User.OffSetMove.Y; //Moving OffSet

                for (int x = User.Movement.X - ViewRangeX; x <= User.Movement.X + ViewRangeX; x++)
                {
                    if (x <= 0 || x % 2 == 1) continue;
                    if (x >= Width) break;
                    drawX = (x - User.Movement.X + OffSetX) * CellWidth - OffSetX + User.OffSetMove.X; //Moving OffSet
                    if ((M2CellInfo[x, y].BackImage == 0) || (M2CellInfo[x, y].BackIndex == -1)) continue;
                    index = (M2CellInfo[x, y].BackImage & 0x1FFFF) - 1;
                    Libraries.MapLibs[M2CellInfo[x, y].BackIndex].Draw(index, drawX, drawY);
                }
            }

            for (int y = User.Movement.Y - ViewRangeY; y <= User.Movement.Y + ViewRangeY + 5; y++)
            {
                if (y <= 0) continue;
                if (y >= Height) break;
                drawY = (y - User.Movement.Y + OffSetY) * CellHeight + User.OffSetMove.Y; //Moving OffSet

                for (int x = User.Movement.X - ViewRangeX; x <= User.Movement.X + ViewRangeX; x++)
                {
                    if (x < 0) continue;
                    if (x >= Width) break;
                    drawX = (x - User.Movement.X + OffSetX) * CellWidth - OffSetX + User.OffSetMove.X; //Moving OffSet

                    index = M2CellInfo[x, y].MiddleImage - 1;

                    if ((index < 0) || (M2CellInfo[x, y].MiddleIndex == -1)) continue;
                    if (M2CellInfo[x, y].MiddleIndex > 199)
                    {//mir3 mid layer is same level as front layer not real middle + it cant draw index -1 so 2 birds in one stone :p
                        Size s = Libraries.MapLibs[M2CellInfo[x, y].MiddleIndex].GetSize(index);

                        if (s.Width != CellWidth || s.Height != CellHeight) continue;
                    }
                    Libraries.MapLibs[M2CellInfo[x, y].MiddleIndex].Draw(index, drawX, drawY);
                }
            }
            for (int y = User.Movement.Y - ViewRangeY; y <= User.Movement.Y + ViewRangeY + 5; y++)
            {
                if (y <= 0) continue;
                if (y >= Height) break;
                drawY = (y - User.Movement.Y + OffSetY) * CellHeight + User.OffSetMove.Y; //Moving OffSet

                for (int x = User.Movement.X - ViewRangeX; x <= User.Movement.X + ViewRangeX; x++)
                {
                    if (x < 0) continue;
                    if (x >= Width) break;
                    drawX = (x - User.Movement.X + OffSetX) * CellWidth - OffSetX + User.OffSetMove.X; //Moving OffSet

                    index = (M2CellInfo[x, y].FrontImage & 0x7FFF) - 1;
                    if (index == -1) continue;
                    int fileIndex = M2CellInfo[x, y].FrontIndex;
                    if (fileIndex == -1) continue;
                    Size s = Libraries.MapLibs[fileIndex].GetSize(index);
                    if (fileIndex == 200) continue; //fixes random bad spots on old school 4.map
                    if (M2CellInfo[x, y].DoorIndex > 0)
                    {
                        Door DoorInfo = GetDoor(M2CellInfo[x, y].DoorIndex);
                        if (DoorInfo == null)
                        {
                            DoorInfo = new Door() { index = M2CellInfo[x, y].DoorIndex, DoorState = 0, ImageIndex = 0, LastTick = CMain.Time };
                            Doors.Add(DoorInfo);
                        }
                        else
                        {
                            if (DoorInfo.DoorState != 0)
                            {
                                index += (DoorInfo.ImageIndex + 1) * M2CellInfo[x, y].DoorOffset;//'bad' code if you want to use animation but it's gonna depend on the animation > has to be custom designed for the animtion
                            }
                        }
                    }

                    if (index < 0 || ((s.Width != CellWidth || s.Height != CellHeight) && ((s.Width != CellWidth * 2) || (s.Height != CellHeight * 2)))) continue;
                    Libraries.MapLibs[fileIndex].Draw(index, drawX, drawY);
                }
            }

            DXManager.SetSurface(oldSurface);

            FloorValid = true;
        }

        private void DrawBackground()
        {
            string cleanFilename = FileName.Replace(Settings.MapPath, "");

            if(cleanFilename.StartsWith("ID1") || cleanFilename.StartsWith("ID2"))
            {
                Libraries.Background.Draw(10, 0, 0); //mountains
            }
            else if(cleanFilename.StartsWith("ID3_013"))
            {
                Libraries.Background.Draw(22, 0, 0); //desert
            }
            else if (cleanFilename.StartsWith("ID3_015"))
            {
                Libraries.Background.Draw(23, 0, 0); //greatwall
            }
            else if (cleanFilename.StartsWith("ID3_023") || cleanFilename.StartsWith("ID3_025"))
            {
                Libraries.Background.Draw(21, 0, 0); //village entrance
            }
        }

        private void DrawObjects()
        {
            for (int y = User.Movement.Y - ViewRangeY; y <= User.Movement.Y + ViewRangeY + 25; y++)
            {
                if (y <= 0) continue;
                if (y >= Height) break;
                for (int x = User.Movement.X - ViewRangeX; x <= User.Movement.X + ViewRangeX; x++)
                {
                    if (x < 0) continue;
                    if (x >= Width) break;
                    M2CellInfo[x, y].DrawDeadObjects();
                }
            }

            for (int y = User.Movement.Y - ViewRangeY; y <= User.Movement.Y + ViewRangeY + 25; y++)
            {
                if (y <= 0) continue;
                if (y >= Height) break;
                int drawY = (y - User.Movement.Y + OffSetY + 1) * CellHeight + User.OffSetMove.Y;

                for (int x = User.Movement.X - ViewRangeX; x <= User.Movement.X + ViewRangeX; x++)
                {
                    if (x < 0) continue;
                    if (x >= Width) break;
                    int drawX = (x - User.Movement.X + OffSetX) * CellWidth - OffSetX + User.OffSetMove.X;
                    int index;
                    byte animation;
                    bool blend;
                    Size s;
                    #region Draw shanda's tile animation layer
                    index = M2CellInfo[x, y].TileAnimationImage;
                    animation = M2CellInfo[x, y].TileAnimationFrames;
                    if ((index > 0) & (animation > 0))
                    {
                        index--;
                        int animationoffset = M2CellInfo[x, y].TileAnimationOffset ^ 0x2000;
                        index += animationoffset * (AnimationCount % animation);
                        Libraries.MapLibs[190].DrawUp(index, drawX, drawY);
                    }

                    #endregion

                    #region Draw mir3 middle layer
                    if ((M2CellInfo[x, y].MiddleIndex > 199) && (M2CellInfo[x, y].MiddleIndex != -1))
                    {
                        index = M2CellInfo[x, y].MiddleImage - 1;
                        if (index > 0)
                        {
                            animation = M2CellInfo[x, y].MiddleAnimationFrame;
                            blend = false;
                            if ((animation > 0) && (animation < 255))
                            {
                                if ((animation & 0x0f) > 0)
                                {
                                    blend = true;
                                    animation &= 0x0f;
                                }
                                if (animation > 0)
                                {
                                    byte animationTick = M2CellInfo[x, y].MiddleAnimationTick;
                                    index += (AnimationCount % (animation + (animation * animationTick))) / (1 + animationTick);

                                    if (blend && (animation == 10 || animation == 8)) //diamond mines, abyss blends
                                    {
                                        Libraries.MapLibs[M2CellInfo[x, y].MiddleIndex].DrawUpBlend(index, new Point(drawX, drawY));
                                    }
                                    else
                                    {
                                        Libraries.MapLibs[M2CellInfo[x, y].MiddleIndex].DrawUp(index, drawX, drawY);
                                    }
                                }
                            }
                            s = Libraries.MapLibs[M2CellInfo[x, y].MiddleIndex].GetSize(index);
                            if ((s.Width != CellWidth || s.Height != CellHeight) && (s.Width != (CellWidth * 2) || s.Height != (CellHeight * 2)) && !blend)
                            {
                                Libraries.MapLibs[M2CellInfo[x, y].MiddleIndex].DrawUp(index, drawX, drawY);
                            }
                        }
                    }
                    #endregion

                    #region Draw front layer
                    index = (M2CellInfo[x, y].FrontImage & 0x7FFF) - 1;

                    if (index < 0) continue;

                    int fileIndex = M2CellInfo[x, y].FrontIndex;
                    if (fileIndex == -1) continue;
                    animation = M2CellInfo[x, y].FrontAnimationFrame;

                    if ((animation & 0x80) > 0)
                    {
                        blend = true;
                        animation &= 0x7F;
                    }
                    else
                        blend = false;

                    
                    if (animation > 0)
                    {
                        byte animationTick = M2CellInfo[x, y].FrontAnimationTick;
                        index += (AnimationCount % (animation + (animation * animationTick))) / (1 + animationTick);
                    }

                    
                    if (M2CellInfo[x, y].DoorIndex > 0)
                    {
                        Door DoorInfo = GetDoor(M2CellInfo[x, y].DoorIndex);
                        if (DoorInfo == null)
                        {
                            DoorInfo = new Door() { index = M2CellInfo[x, y].DoorIndex, DoorState = 0, ImageIndex = 0, LastTick = CMain.Time };
                            Doors.Add(DoorInfo);
                        }
                        else
                        {
                            if (DoorInfo.DoorState != 0)
                            {
                                index += (DoorInfo.ImageIndex + 1) * M2CellInfo[x, y].DoorOffset;//'bad' code if you want to use animation but it's gonna depend on the animation > has to be custom designed for the animtion
                            }
                        }
                    }

                    s = Libraries.MapLibs[fileIndex].GetSize(index);
                    if (s.Width == CellWidth && s.Height == CellHeight && animation == 0) continue;
                    if ((s.Width == CellWidth * 2) && (s.Height == CellHeight * 2) && (animation == 0)) continue;

                    if (blend)
                    {
                        if ((fileIndex > 99) & (fileIndex < 199))
                            Libraries.MapLibs[fileIndex].DrawBlend(index, new Point(drawX, drawY - (3 * CellHeight)), Color.White, true);
                        else
                            Libraries.MapLibs[fileIndex].DrawBlend(index, new Point(drawX, drawY - s.Height), Color.White, (index >= 2723 && index <= 2732));
                    }
                    else
                        Libraries.MapLibs[fileIndex].Draw(index, drawX, drawY - s.Height);
                    #endregion
                }

                for (int x = User.Movement.X - ViewRangeX; x <= User.Movement.X + ViewRangeX; x++)
                {
                    if (x < 0) continue;
                    if (x >= Width) break;
                    M2CellInfo[x, y].DrawObjects();
                }
            }

            DXManager.Sprite.Flush();
            float oldOpacity = DXManager.Opacity;
            DXManager.SetOpacity(0.4F);

            //MapObject.User.DrawMount();

            MapObject.User.DrawBody();

            if ((MapObject.User.Direction == MirDirection.Up) ||
                (MapObject.User.Direction == MirDirection.UpLeft) ||
                (MapObject.User.Direction == MirDirection.UpRight) ||
                (MapObject.User.Direction == MirDirection.Right) ||
                (MapObject.User.Direction == MirDirection.Left))
            {
                MapObject.User.DrawHead();
                if (MapObject.User.Helmet > 0)
                    MapObject.User.DrawHat();//wrong order :P
                MapObject.User.DrawWings();
                
            }
            else
            {
                MapObject.User.DrawWings();
                MapObject.User.DrawHead();
                if (MapObject.User.Helmet > 0)
                    MapObject.User.DrawHat();
            }

            DXManager.SetOpacity(oldOpacity);

            if (MapObject.MouseObject != null && !MapObject.MouseObject.Dead && MapObject.MouseObject != MapObject.TargetObject && MapObject.MouseObject.Blend) //Far
                MapObject.MouseObject.DrawBlend();

            if (MapObject.TargetObject != null)
                MapObject.TargetObject.DrawBlend();

            for (int i = 0; i < Objects.Count; i++)
            {
                if (Settings.Effect)
                    Objects[i].DrawEffects(true);

                //  Draw the names of Objects other than Items (I.E Name View)
                if (Settings.NameView && 
                    (Objects[i] is PlayerObject || Objects[i] is NPCObject) && 
                    !Objects[i].Dead)
                    Objects[i].DrawName();
                if (Settings.MonsterName &&
                    (Objects[i] is MonsterObject) &&
                    !Objects[i].Dead)
                    Objects[i].DrawName();



                //  Draw the Clients Chat lines
                Objects[i].DrawChat();
                //  Check if the Object is a Monster
                if (Objects[i].Race == ObjectType.Monster)
                {   //  Temporary Object
                    MonsterObject temp = (MonsterObject)Objects[i];
                    //  Check the Oject isn't a boss
                    if (!temp.IsBoss)
                        //  Draw the normal health bar
                        Objects[i].DrawHealth();
                }
                //  Object isn't a Monster
                else
                    Objects[i].DrawHealth();
                Objects[i].DrawPoison();

                Objects[i].DrawDamages();
            }


            if (!Settings.Effect) return;

            for (int i = Effects.Count - 1; i >= 0; i--)
                Effects[i].Draw();
        }

        private void DrawLights(LightSetting setting)
        {
            if (DXManager.Lights == null || DXManager.Lights.Count == 0) return;

            if (_lightTexture == null || _lightTexture.Disposed)
            {
                _lightTexture = new Texture(DXManager.Device, Settings.ScreenWidth, Settings.ScreenHeight, 1, Usage.RenderTarget, Format.A8R8G8B8, Pool.Default);
                _lightTexture.Disposing += FloorTexture_Disposing;
                _lightSurface = _lightTexture.GetSurfaceLevel(0);
            }

            Surface oldSurface = DXManager.CurrentSurface;
            DXManager.SetSurface(_lightSurface);

            #region Night Lights
            Color Darkness = Color.Black;
            switch (MapDarkLight)
            {
                case 1:
                    Darkness = Color.FromArgb(255, 20, 20, 20);
                    break;
                case 2:
                    Darkness = Color.LightSlateGray;
                    break;
                case 3:
                    Darkness = Color.SkyBlue;
                    break;
                case 4:
                    Darkness = Color.Goldenrod;
                    break;
                default:
                    Darkness = Color.Black;
                    break;
            }

            DXManager.Device.Clear(ClearFlags.Target, setting == LightSetting.Night ? Darkness : Color.FromArgb(255, 50, 50, 50), 0, 0);

            #endregion

            int light;
            Point p;
            DXManager.SetBlend(true);
            DXManager.Device.RenderState.SourceBlend = Blend.SourceAlpha;

            #region Object Lights (Player/Mob/NPC)
            for (int i = 0; i < Objects.Count; i++)
            {
                MapObject ob = Objects[i];
                if ((ob.Light > 0 || (ob.Race == ObjectType.Monster && ((MonsterObject)ob).LightEffect > 0)) && (!ob.Dead || ob == MapObject.User || ob.Race == ObjectType.Spell))
                {

                    light = ob.Light;
                    //  Light Range
                    int LightRange = light % 40; // this changes light range (note)
                    if (LightRange >= DXManager.Lights.Count)
                        LightRange = DXManager.Lights.Count - 1;

                    p = ob.DrawLocation;

                    Color lightColour = ob.LightColour;

                    if (ob.Race == ObjectType.Player)
                    {
                        switch (light / 15)
                        {
                            case 0://no light source
                                lightColour = Color.FromArgb(255, 60, 60, 60);
                                break;
                            case 1:
                                lightColour = Color.FromArgb(255, 120, 120, 120);
                                break;
                            case 2://Candle
                                lightColour = Color.FromArgb(255, 180, 180, 180);
                                break;
                            case 3://Torch
                                lightColour = Color.FromArgb(255, 240, 240, 240);
                                break;
                            default://Peddler Torch
                                lightColour = Color.FromArgb(255, 255, 255, 255);
                                break;
                        }
                    }
                    else if (ob.Race == ObjectType.Merchant)
                    {
                        lightColour = Color.FromArgb(255, 120, 120, 120);
                    }

                    if (DXManager.Lights[LightRange] != null && !DXManager.Lights[LightRange].Disposed)
                    {
                        if (ob.Race == ObjectType.Monster && Settings.MobLightEffect)
                        {
                            var mon = (MonsterObject)ob;
                            int frame = 0;
                            switch (mon.LightEffect)
                            {
                                case 1:

                                    frame = Math.Min(Math.Min(LightRange, 7 ) + Math.Abs(5 - GameScene.Scene.LightEffectFrame), DXManager.Lights.Count - 1);

                                    p.Offset(-(DXManager.LightSizes[frame].X / 2) - (CellWidth / 2) + 10, -(DXManager.LightSizes[frame].Y / 2) - (CellHeight / 2) - 5);
                                    DXManager.Sprite.Draw2D(DXManager.Lights[frame], PointF.Empty, 0, p, lightColour);
                                    break;

                                case 2:

                                    frame = Math.Abs((5 - GameScene.Scene.LightEffectFrame) * 20);

                                    p.Offset(-(DXManager.LightSizes[LightRange].X / 2) - (CellWidth / 2) + 10, -(DXManager.LightSizes[LightRange].Y / 2) - (CellHeight / 2) - 5);
                                    DXManager.Sprite.Draw2D(DXManager.Lights[LightRange], PointF.Empty, 0, p, Color.FromArgb(255, 180, 200 - frame, 150));
                                    break;

                                case 3:

                                    frame = LightRange;

                                    p.Offset(-(CellWidth / 2) + 40, -(CellHeight / 2) + 20);

                                    DXManager.Sprite.Draw2D(DXManager.RectangleLights[frame], new PointF(DXManager.LightSizeRectangle[frame + 1].X / 2f, DXManager.LightSizeRectangle[frame + 1].Y / 2f), GameScene.Scene.LightEffectRotation / 10f, p, lightColour);

                                    break;

                                case 4:

                                    frame = LightRange;

                                    p.Offset(-(CellWidth / 2) + 40, -(CellHeight / 2) + 20);

                                    DXManager.Sprite.Draw2D(DXManager.RectangleLights[frame], new PointF(DXManager.LightSizeRectangle[frame + 1].X / 2f, DXManager.LightSizeRectangle[frame + 1].Y / 2f), GameScene.Scene.LightEffectRotation / 10f, p, lightColour);

                                    DXManager.Sprite.Draw2D(DXManager.RectangleLights[frame], new PointF(DXManager.LightSizeRectangle[frame + 1].X / 2f, DXManager.LightSizeRectangle[frame + 1].Y / 2f), 90 + GameScene.Scene.LightEffectRotation / 10f, p, Color.PaleGreen);

                                    break;


                                case 0:
                                default:
                                    p.Offset(-(DXManager.LightSizes[LightRange].X / 2) - (CellWidth / 2) + 10, -(DXManager.LightSizes[LightRange].Y / 2) - (CellHeight / 2) - 5);
                                    DXManager.Sprite.Draw2D(DXManager.Lights[LightRange], PointF.Empty, 0, p, lightColour);
                                    break;
                            }

                        }
                        else

                        {
                            p.Offset(-(DXManager.LightSizes[LightRange].X / 2) - (CellWidth / 2), -(DXManager.LightSizes[LightRange].Y / 2) - (CellHeight / 2) - 5);
                            DXManager.Sprite.Draw2D(DXManager.Lights[LightRange], PointF.Empty, 0, p, lightColour);
                        }
                    }

                }
                #region Object Effect Lights
                if (!Settings.Effect) continue;
                for (int e = 0; e < ob.Effects.Count; e++)
                {
                    Effect effect = ob.Effects[e];
                    if (!effect.Blend || CMain.Time < effect.Start || (!(effect is Missile) && effect.Light < ob.Light)) continue;

                    light = effect.Light;

                    p = effect.DrawLocation;

                    if (DXManager.Lights[light] != null && !DXManager.Lights[light].Disposed)
                    {

                        p.Offset(-(DXManager.LightSizes[light].X / 2) - (CellWidth / 2), -(DXManager.LightSizes[light].Y / 2) - (CellHeight / 2) - 5);
                        DXManager.Sprite.Draw2D(DXManager.Lights[light], PointF.Empty, 0, p, effect.LightColour);
                    }

                }
                #endregion
            }
            #endregion

            #region Map Effect Lights
            if (Settings.Effect)
            {
                for (int e = 0; e < Effects.Count; e++)
                {
                    Effect effect = Effects[e];
                    if (!effect.Blend || CMain.Time < effect.Start) continue;

                    light = effect.Light;
                    if (light == 0) continue;

                    p = effect.DrawLocation;

                    if (DXManager.Lights[light] != null && !DXManager.Lights[light].Disposed)
                    {
                        p.Offset(-(DXManager.LightSizes[light].X / 2) - (CellWidth / 2), -(DXManager.LightSizes[light].Y / 2) - (CellHeight / 2) - 5);
                        DXManager.Sprite.Draw2D(DXManager.Lights[light], PointF.Empty, 0, p, Color.White);
                    }
                }
            }
            #endregion

            #region Map Lights
            for (int y = MapObject.User.Movement.Y - ViewRangeY - 24; y <= MapObject.User.Movement.Y + ViewRangeY + 24; y++)
            {
                if (y < 0) continue;
                if (y >= Height) break;
                for (int x = MapObject.User.Movement.X - ViewRangeX - 24; x < MapObject.User.Movement.X + ViewRangeX + 24; x++)
                {
                    if (x < 0) continue;
                    if (x >= Width) break;
                    int imageIndex = (M2CellInfo[x, y].FrontImage & 0x7FFF) - 1;
                    //if (M2CellInfo[x, y].Light <= 0 || M2CellInfo[x, y].Light >= 10) continue;
                    if (M2CellInfo[x, y].Light == 0) continue;

                    Color lightIntensity;

                    light = (M2CellInfo[x, y].Light % 10) * 3;

                    switch (M2CellInfo[x, y].Light / 10)
                    {
                        case 1:
                            lightIntensity = Color.FromArgb(255, 255, 255, 255);
                            break;
                        case 2:
                            lightIntensity = Color.FromArgb(255, 120, 180, 255);
                            break;
                        case 3:
                            lightIntensity = Color.FromArgb(255, 255, 180, 120);
                            break;
                        case 4:
                            lightIntensity = Color.FromArgb(255, 22, 160, 5);
                            break;
                        default:
                            lightIntensity = Color.FromArgb(255, 255, 255, 255);
                            break;
                    }

                    int fileIndex = M2CellInfo[x, y].FrontIndex;

                    p = new Point(
                        (x + OffSetX - MapObject.User.Movement.X) * CellWidth + MapObject.User.OffSetMove.X,
                        (y + OffSetY - MapObject.User.Movement.Y) * CellHeight + MapObject.User.OffSetMove.Y + 32);


                    if (M2CellInfo[x, y].FrontAnimationFrame > 0)
                        p.Offset(Libraries.MapLibs[fileIndex].GetOffSet(imageIndex));

                    if (light >= DXManager.Lights.Count)
                        light = DXManager.Lights.Count - 1;

                    if (DXManager.Lights[light] != null && !DXManager.Lights[light].Disposed)
                    {
                        p.Offset(-(DXManager.LightSizes[light].X / 2) - (CellWidth / 2) + 10, -(DXManager.LightSizes[light].Y / 2) - (CellHeight / 2) - 5);
                        DXManager.Sprite.Draw2D(DXManager.Lights[light], PointF.Empty, 0, p, lightIntensity);
                    }
                }
            }
            #endregion

            DXManager.SetBlend(false);
            DXManager.SetSurface(oldSurface);

            DXManager.Device.RenderState.SourceBlend = Blend.DestinationColor;
            DXManager.Device.RenderState.DestinationBlend = Blend.BothInvSourceAlpha;

            DXManager.Sprite.Draw2D(_lightTexture, PointF.Empty, 0, PointF.Empty, Color.White);
            DXManager.Sprite.End();
            DXManager.Sprite.Begin(SpriteFlags.AlphaBlend);
        }

        private static void OnMouseClick(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;
            if (me == null) return;

            if (AwakeningAction == true) return;
            switch (me.Button)
            {
                case MouseButtons.Left:
                    {
                        AutoRun = false;
                        if (MapObject.MouseObject == null) return;
                        NPCObject npc = MapObject.MouseObject as NPCObject;
                        if (npc != null)
                        {
                            GameScene.Scene.NPCDialog.Hide();

                            if (CMain.Time <= GameScene.NPCTime && npc.ObjectID == GameScene.NPCID) return;

                            GameScene.NPCTime = CMain.Time + 5000;
                            GameScene.NPCID = npc.ObjectID;
                            Network.Enqueue(new C.CallNPC { ObjectID = npc.ObjectID, Key = "[@Main]" });
                        }

                        MonsterObject mon = MapObject.MouseObject as MonsterObject;
                        if (mon != null && mon.AI == 201)
                        {
                            GameScene.Scene.NPCDialog.Hide();

                            if (CMain.Time <= GameScene.NPCTime) return;

                            GameScene.NPCTime = CMain.Time + 5000;

                            Network.Enqueue(new C.TalkMonsterNPC { ObjectID = mon.ObjectID });
                        }
                    }
                    break;
                case MouseButtons.Right:
                    {
                        AutoRun = false;
                        uint id;
                        if (MapObject.MouseObject == null) return;
                        if (MapObject.MouseObject.Race == ObjectType.Player)
                        {
                            PlayerObject player = MapObject.MouseObject as PlayerObject;
                            if (player == null || player == User || !CMain.Ctrl) return;
                            if (CMain.Time <= GameScene.InspectTime && player.ObjectID == InspectDialog.InspectID) return;

                            id = player.ObjectID;
                        }
                        else if (MapObject.MouseObject.Race == ObjectType.Hero)
                        {
                            if (CMain.Time <= GameScene.InspectTime && MapObject.MouseObject.ObjectID == InspectDialog.InspectID) return;
                            id = MapObject.MouseObject.ObjectID;
                        }
                        else return;

                            GameScene.InspectTime = CMain.Time + 500;
                        InspectDialog.InspectID = id;
                        Network.Enqueue(new C.Inspect { ObjectID = id });
                    }
                    break;
                case MouseButtons.Middle:
                    if (CMain.Ctrl && GameScene.User.HeroState == HeroState.Spawned)
                        Network.Enqueue(new C.HeroRunTo { GoTo = MapLocation });
                    else
                        AutoRun = !AutoRun;
                    break;
            }
        }

        private static void OnMouseDown(object sender, MouseEventArgs e)
        {
            MapButtons |= e.Button;
            GameScene.CanRun = false;

            if (AwakeningAction == true) return;

            if (e.Button != MouseButtons.Left) return;

            if (GameScene.SelectedCell != null)
            {
                if (GameScene.SelectedCell.GridType != MirGridType.Inventory)
                {
                    GameScene.SelectedCell = null;
                    return;
                }

                MirItemCell cell = GameScene.SelectedCell;
                if (cell.Item.Info.Bind.HasFlag(BindMode.DontDrop))
                {
                    MirMessageBox messageBox = new MirMessageBox(string.Format("You cannot drop {0}", cell.Item.Name), MirMessageBoxButtons.OK);
                    messageBox.Show();
                    GameScene.SelectedCell = null;
                    return;
                }
                if (cell.Item.Count == 1)
                {
                    MirMessageBox messageBox = new MirMessageBox(string.Format("Are you sure you want to drop {0}?", cell.Item.FriendlyName), MirMessageBoxButtons.YesNo);

                    messageBox.YesButton.Click += (o, a) =>
                    {
                        Network.Enqueue(new C.DropItem { UniqueID = cell.Item.UniqueID, Count = 1 });

                        cell.Locked = true;
                    };
                    messageBox.Show();
                }
                else if (cell.Item.Count == 0)
                {
                    MirMessageBox msgBox = new MirMessageBox(string.Format("Do you wish to destroy {0}?", cell.Item.FriendlyName), MirMessageBoxButtons.YesNo);
                    msgBox.YesButton.Click += (o, a) =>
                    {
                        Network.Enqueue(new C.DropItem { UniqueID = cell.Item.UniqueID, Count = 0 });
                        cell.Locked = false;
                    };
                    msgBox.Show();
                }
                else
                {                                                               //  here
                    MirAmountBox amountBox = new MirAmountBox("Drop Amount:", cell.Item.Info.Image, cell.Item.Count);

                    amountBox.OKButton.Click += (o, a) =>
                    {
                        if (amountBox.Amount <= 0) return;
                        Network.Enqueue(new C.DropItem
                        {
                            UniqueID = cell.Item.UniqueID,
                            Count = amountBox.Amount
                        });

                        cell.Locked = true;
                    };

                    amountBox.Show();
                }
                GameScene.SelectedCell = null;

                return;
            }
            if (GameScene.PickedUpGold)
            {
                MirAmountBox amountBox = new MirAmountBox("Drop Amount:", 116, GameScene.Gold);

                amountBox.OKButton.Click += (o, a) =>
                {
                    if (amountBox.Amount > 0)
                    {
                        Network.Enqueue(new C.DropGold { Amount = amountBox.Amount });
                    }
                };

                amountBox.Show();
                GameScene.PickedUpGold = false;
            }



            if (MapObject.MouseObject != null && !MapObject.MouseObject.Dead && !(MapObject.MouseObject is ItemObject) &&
                !(MapObject.MouseObject is NPCObject) && !(MapObject.MouseObject is MonsterObject && MapObject.MouseObject.AI == 64)
                 && !(MapObject.MouseObject is MonsterObject && MapObject.MouseObject.AI == 70))
            {
                MapObject.TargetObject = MapObject.MouseObject;
                if (MapObject.MouseObject is MonsterObject && MapObject.MouseObject.AI != 6)
                    MapObject.MagicObject = MapObject.TargetObject;
            }
            else
                MapObject.TargetObject = null;
        }

        /// <summary>
        /// Walking logic target check
        /// </summary>
        /// <param name="location">The players location</param>
        /// <param name="dir">The direction to check</param>
        /// <returns></returns>
        public bool SpotHasTartget(Point location, MirDirection dir)
        {
            if (location.X < 0 ||
                location.X > Width ||
                location.Y < 0 ||
                location.Y > Height)
                return false;
            location = Functions.PointMove(location, dir, 1);
            bool hasValidTarget = false;
            if (M2CellInfo[location.X, location.Y].CellObjects != null &&
                M2CellInfo[location.X, location.Y].CellObjects.Count > 0)
            {
                for (int i = 0; i < M2CellInfo[location.X, location.Y].CellObjects.Count; i++)
                {
                    MapObject ob = M2CellInfo[location.X, location.Y].CellObjects[i];
                    if (ob.Dead)
                        continue;
                    if (ob.Race == ObjectType.Monster ||
                        ob.Race == ObjectType.Player)
                        hasValidTarget = true;
                }
            }
            return hasValidTarget;
        }

        private void CheckInput()
        {
            if (AwakeningAction == true) return;

            if ((MouseControl == this) && (MapButtons != MouseButtons.None)) AutoHit = false;//mouse actions stop mining even when frozen!
            if (!CanRideAttack()) AutoHit = false;

            if (CMain.Time < InputDelay || User.Poison.HasFlag(PoisonType.Paralysis) || User.Poison.HasFlag(PoisonType.LRParalysis) || User.Poison.HasFlag(PoisonType.Frozen) || User.Fishing) return;

            if (User.NextMagic != null && !User.RidingMount)
            {
                UseMagic(User.NextMagic);
                return;
            }

            if ((!User.FastChannel && CMain.Time < User.BlizzardStopTime) || CMain.Time < User.ReincarnationStopTime || CMain.Time < User.comboSpell.Delay) return;

            if (MapObject.TargetObject != null && !MapObject.TargetObject.Dead)
            {
                if (((MapObject.TargetObject.Name.EndsWith(")") || MapObject.TargetObject is PlayerObject) && CMain.Shift) ||
                    (!MapObject.TargetObject.Name.EndsWith(")") && MapObject.TargetObject is MonsterObject))
                {

                    GameScene.LogTime = CMain.Time + Globals.LogDelay;

                    if (User.Class == MirClass.Archer && User.HasClassWeapon && !User.RidingMount && !User.Fishing)//ArcherTest - non aggressive targets (player / pets)
                    {
                        if (Functions.InRange(MapObject.TargetObject.CurrentLocation, User.CurrentLocation, Globals.MaxAttackRange))
                        {
                            if (CMain.Time > GameScene.AttackTime)
                            {
                                User.QueuedAction = new QueuedAction { Action = MirAction.AttackRange1, Direction = Functions.DirectionFromPoint(User.CurrentLocation, MapObject.TargetObject.CurrentLocation), Location = User.CurrentLocation, Params = new List<object>() };
                                User.QueuedAction.Params.Add(MapObject.TargetObject != null ? MapObject.TargetObject.ObjectID : (uint)0);
                                User.QueuedAction.Params.Add(MapObject.TargetObject.CurrentLocation);

                                // MapObject.TargetObject = null; //stop constant attack when close up
                            }
                        }
                        else
                        {
                            if (CMain.Time >= OutputDelay)
                            {
                                OutputDelay = CMain.Time + 1000;
                                GameScene.Scene.OutputMessage("Target is too far.");
                            }
                        }
                        //  return;
                    }

                    else if (Functions.InRange(MapObject.TargetObject.CurrentLocation, User.CurrentLocation, 1))
                    {
                        if (CMain.Time > GameScene.AttackTime && CanRideAttack())
                        {
                            User.QueuedAction = new QueuedAction { Action = MirAction.Attack1, Direction = Functions.DirectionFromPoint(User.CurrentLocation, MapObject.TargetObject.CurrentLocation), Location = User.CurrentLocation };
                            return;
                        }
                    }
                }
            }
            if (AutoHit && !User.RidingMount)
            {
                if (CMain.Time > GameScene.AttackTime)
                {
                    User.QueuedAction = new QueuedAction { Action = MirAction.Mine, Direction = User.Direction, Location = User.CurrentLocation };
                    return;
                }
            }


            MirDirection direction;
            if (MouseControl == this || MouseControl == GameScene.Scene.MainDialog)
            {
                direction = MouseDirection();
                if (AutoRun)
                {
                    if (GameScene.CanRun && CanRun(direction) && CMain.Time > GameScene.NextRunTime && User.HP >= 10 && (!User.Sneaking || (User.Sneaking && User.Sprint))) //slow remove
                    {
                        int distance = User.RidingMount || User.Sprint && !User.Sneaking ? 3 : 2;
                        bool fail = false;
                        for (int i = 1; i <= distance; i++)
                        {
                            if (!CheckDoorOpen(Functions.PointMove(User.CurrentLocation, direction, i)))
                                fail = true;
                        }
                        if (!fail)
                        {
                            if ((CanWalk(direction)))
                            {
                                User.QueuedAction = new QueuedAction { Action = MirAction.Running, Direction = direction, Location = Functions.PointMove(User.CurrentLocation, direction, distance) };
                                return;
                            }
                            #region Enhanced Running
                            if ((CanWalk(Functions.NextDir(direction))))
                            {
                                direction = Functions.NextDir(direction);
                                User.QueuedAction = new QueuedAction { Action = MirAction.Running, Direction = direction, Location = Functions.PointMove(User.CurrentLocation, direction, distance) };
                                return;
                            }
                            if ((CanWalk(Functions.PreviousDir(direction))))
                            {
                                direction = Functions.PreviousDir(direction);
                                User.QueuedAction = new QueuedAction { Action = MirAction.Running, Direction = direction, Location = Functions.PointMove(User.CurrentLocation, direction, distance) };
                                return;
                            }
                            #endregion
                        }
                    }
                    if (!SpotHasTartget(User.CurrentLocation, direction) && (CanWalk(direction)) && (CheckDoorOpen(Functions.PointMove(User.CurrentLocation, direction, 1))))
                    {
                        User.QueuedAction = new QueuedAction { Action = MirAction.Walking, Direction = direction, Location = Functions.PointMove(User.CurrentLocation, direction, 1) };
                        return;
                    }
                    #region Advanced Walking
                    else if (!SpotHasTartget(User.CurrentLocation, Functions.NextDir(direction)) && (CanWalk(Functions.NextDir(direction)) && (CheckDoorOpen(Functions.PointMove(User.CurrentLocation, Functions.NextDir(direction), 1)))))
                    {
                        direction = Functions.NextDir(direction);
                        User.QueuedAction = new QueuedAction { Action = MirAction.Walking, Direction = direction, Location = Functions.PointMove(User.CurrentLocation, direction, 1) };
                        return;
                    }
                    else if (!SpotHasTartget(User.CurrentLocation, Functions.PreviousDir(direction)) && (CanWalk(Functions.PreviousDir(direction)) && (CheckDoorOpen(Functions.PointMove(User.CurrentLocation, Functions.PreviousDir(direction), 1)))))
                    {
                        direction = Functions.PreviousDir(direction);
                        User.QueuedAction = new QueuedAction { Action = MirAction.Walking, Direction = direction, Location = Functions.PointMove(User.CurrentLocation, direction, 1) };
                        return;
                    }
                    #endregion
                    if (direction != User.Direction)
                    {
                        User.QueuedAction = new QueuedAction { Action = MirAction.Standing, Direction = direction, Location = User.CurrentLocation };
                        return;
                    }
                    return;
                }

                switch (MapButtons)
                {
                    case MouseButtons.Left:
                        if (MapObject.MouseObject is NPCObject || (MapObject.MouseObject is PlayerObject && MapObject.MouseObject != User)) break;
                        if (MapObject.MouseObject is MonsterObject && MapObject.MouseObject.AI == 70) break;

                        if (CMain.Alt && !User.RidingMount)
                        {
                            User.QueuedAction = new QueuedAction { Action = MirAction.Harvest, Direction = direction, Location = User.CurrentLocation };
                            return;
                        }
                        if (CMain.Shift)
                        {
                            if (CMain.Time > GameScene.AttackTime && CanRideAttack()) //ArcherTest - shift click
                            {
                                MapObject target = null;
                                if (MapObject.MouseObject is MonsterObject || MapObject.MouseObject is PlayerObject) target = MapObject.MouseObject;

                                if (User.Class == MirClass.Archer && User.HasClassWeapon && !User.RidingMount)
                                {
                                    if (target != null)
                                    {
                                        if (!Functions.InRange(MapObject.MouseObject.CurrentLocation, User.CurrentLocation, Globals.MaxAttackRange))
                                        {
                                            if (CMain.Time >= OutputDelay)
                                            {
                                                OutputDelay = CMain.Time + 1000;
                                                GameScene.Scene.OutputMessage("Target is too far.");
                                            }
                                            return;
                                        }
                                    }

                                    User.QueuedAction = new QueuedAction { Action = MirAction.AttackRange1, Direction = MouseDirection(), Location = User.CurrentLocation, Params = new List<object>() };
                                    User.QueuedAction.Params.Add(target != null ? target.ObjectID : (uint)0);
                                    User.QueuedAction.Params.Add(Functions.PointMove(User.CurrentLocation, MouseDirection(), 9));
                                    return;
                                }

                                //stops double slash from being used without empty hand or assassin weapon (otherwise bugs on second swing)
                                if (GameScene.DoubleSlash && (!User.HasClassWeapon && User.Weapon > -1)) return;

                                User.QueuedAction = new QueuedAction { Action = MirAction.Attack1, Direction = direction, Location = User.CurrentLocation };
                            }
                            return;
                        }

                        if (MapObject.MouseObject is MonsterObject && User.Class == MirClass.Archer && MapObject.TargetObject != null && !MapObject.TargetObject.Dead && User.HasClassWeapon && !User.RidingMount) //ArcherTest - range attack
                        {
                            if (Functions.InRange(MapObject.MouseObject.CurrentLocation, User.CurrentLocation, Globals.MaxAttackRange))
                            {
                                if (CMain.Time > GameScene.AttackTime)
                                {
                                    User.QueuedAction = new QueuedAction { Action = MirAction.AttackRange1, Direction = direction, Location = User.CurrentLocation, Params = new List<object>() };
                                    User.QueuedAction.Params.Add(MapObject.TargetObject.ObjectID);
                                    User.QueuedAction.Params.Add(MapObject.TargetObject.CurrentLocation);
                                }
                            }
                            else
                            {
                                if (CMain.Time >= OutputDelay)
                                {
                                    OutputDelay = CMain.Time + 1000;
                                    GameScene.Scene.OutputMessage("Target is too far.");
                                }
                            }
                            return;
                        }

                        if (MapLocation == User.CurrentLocation)
                        {
                            if (CMain.Time > GameScene.PickUpTime)
                            {
                                GameScene.PickUpTime = CMain.Time + 200;
                                Network.Enqueue(new C.PickUp());
                            }
                            return;
                        }

                        //mine
                        if (!ValidPoint(Functions.PointMove(User.CurrentLocation, direction, 1)))
                        {
                            if ((MapObject.User.Equipment[(int)EquipmentSlot.Weapon] != null) && (MapObject.User.Equipment[(int)EquipmentSlot.Weapon].Info.CanMine))
                            {
                                if (direction != User.Direction)
                                {
                                    User.QueuedAction = new QueuedAction { Action = MirAction.Standing, Direction = direction, Location = User.CurrentLocation };
                                    return;
                                }
                                AutoHit = true;
                                return;
                            }
                        }
                        if (!SpotHasTartget(User.CurrentLocation, direction) && (CanWalk(direction)) && (CheckDoorOpen(Functions.PointMove(User.CurrentLocation, direction, 1))))
                        {

                            User.QueuedAction = new QueuedAction { Action = MirAction.Walking, Direction = direction, Location = Functions.PointMove(User.CurrentLocation, direction, 1) };
                            return;
                        }
                        #region Advanced Walking
                        else if (!SpotHasTartget(User.CurrentLocation, Functions.NextDir(direction)) && (CanWalk(Functions.NextDir(direction)) && (CheckDoorOpen(Functions.PointMove(User.CurrentLocation, Functions.NextDir(direction), 1)))))
                        {
                            direction = Functions.NextDir(direction);
                            User.QueuedAction = new QueuedAction { Action = MirAction.Walking, Direction = direction, Location = Functions.PointMove(User.CurrentLocation, direction, 1) };
                            return;
                        }
                        else if (!SpotHasTartget(User.CurrentLocation, Functions.PreviousDir(direction)) && (CanWalk(Functions.PreviousDir(direction)) && (CheckDoorOpen(Functions.PointMove(User.CurrentLocation, Functions.PreviousDir(direction), 1)))))
                        {
                            direction = Functions.PreviousDir(direction);
                            User.QueuedAction = new QueuedAction { Action = MirAction.Walking, Direction = direction, Location = Functions.PointMove(User.CurrentLocation, direction, 1) };
                            return;
                        }
                        #endregion
                        if (direction != User.Direction)
                        {
                            User.QueuedAction = new QueuedAction { Action = MirAction.Standing, Direction = direction, Location = User.CurrentLocation };
                            return;
                        }

                        if (CanFish(direction))
                        {
                            User.FishingTime = CMain.Time;
                            Network.Enqueue(new C.FishingCast { CastOut = true });
                            return;
                        }

                        break;
                    case MouseButtons.Right:
                        if (MapObject.MouseObject is PlayerObject && MapObject.MouseObject != User && CMain.Ctrl) break;

                        if (Functions.InRange(MapLocation, User.CurrentLocation, 2))
                        {
                            if (direction != User.Direction)
                            {
                                User.QueuedAction = new QueuedAction { Action = MirAction.Standing, Direction = direction, Location = User.CurrentLocation };
                            }
                            return;
                        }

                        GameScene.CanRun = User.FastRun ? true : GameScene.CanRun;

                        if (GameScene.CanRun && CanRun(direction) && CMain.Time > GameScene.NextRunTime && User.HP >= 10 && (!User.Sneaking || (User.Sneaking && User.Sprint))) //slow removed
                        {
                            int distance = User.RidingMount || User.Sprint && !User.Sneaking ? 3 : 2;
                            bool fail = false;
                            for (int i = 0; i <= distance; i++)
                            {
                                if (!CheckDoorOpen(Functions.PointMove(User.CurrentLocation, direction, i)))
                                    fail = true;
                            }
                            if (!fail)
                            {
                                if ((CanWalk(direction)))
                                {
                                    User.QueuedAction = new QueuedAction { Action = MirAction.Running, Direction = direction, Location = Functions.PointMove(User.CurrentLocation, direction, distance) };
                                    return;
                                }
                                #region Enhanced Running
                                else if ((CanWalk(Functions.NextDir(direction))))
                                {
                                    direction = Functions.NextDir(direction);
                                    User.QueuedAction = new QueuedAction { Action = MirAction.Running, Direction = direction, Location = Functions.PointMove(User.CurrentLocation, direction, distance) };
                                    return;
                                }
                                if ((CanWalk(Functions.PreviousDir(direction))))
                                {
                                    direction = Functions.PreviousDir(direction);
                                    User.QueuedAction = new QueuedAction { Action = MirAction.Running, Direction = direction, Location = Functions.PointMove(User.CurrentLocation, direction, distance) };
                                    return;
                                }
                                #endregion
                            }
                        }
                        if ((CanWalk(direction)) && (CheckDoorOpen(Functions.PointMove(User.CurrentLocation, direction, 1))))
                        {
                            User.QueuedAction = new QueuedAction { Action = MirAction.Walking, Direction = direction, Location = Functions.PointMove(User.CurrentLocation, direction, 1) };
                            return;
                        }
                        #region Advanced Walking
                        else if ((CanWalk(Functions.NextDir(direction)) && (CheckDoorOpen(Functions.PointMove(User.CurrentLocation, Functions.NextDir(direction), 1)))))
                        {
                            direction = Functions.NextDir(direction);
                            User.QueuedAction = new QueuedAction { Action = MirAction.Walking, Direction = direction, Location = Functions.PointMove(User.CurrentLocation, direction, 1) };
                            return;
                        }
                        else if ((CanWalk(Functions.PreviousDir(direction)) && (CheckDoorOpen(Functions.PointMove(User.CurrentLocation, Functions.PreviousDir(direction), 1)))))
                        {
                            direction = Functions.PreviousDir(direction);
                            User.QueuedAction = new QueuedAction { Action = MirAction.Walking, Direction = direction, Location = Functions.PointMove(User.CurrentLocation, direction, 1) };
                            return;
                        }
                        #endregion
                        if (direction != User.Direction)
                        {
                            User.QueuedAction = new QueuedAction { Action = MirAction.Standing, Direction = direction, Location = User.CurrentLocation };
                            return;
                        }
                        break;
                }
            }

            if (MapObject.TargetObject == null || MapObject.TargetObject.Dead) return;
            if (((!MapObject.TargetObject.Name.EndsWith(")") && !(MapObject.TargetObject is PlayerObject)) || !CMain.Shift) &&
                (MapObject.TargetObject.Name.EndsWith(")") || !(MapObject.TargetObject is MonsterObject))) return;
            if (Functions.InRange(MapObject.TargetObject.CurrentLocation, User.CurrentLocation, 1)) return;
            if (User.Class == MirClass.Archer && User.HasClassWeapon && (MapObject.TargetObject is MonsterObject || MapObject.TargetObject is PlayerObject)) return; //ArcherTest - stop walking
            direction = Functions.DirectionFromPoint(User.CurrentLocation, MapObject.TargetObject.CurrentLocation);

            if (!CanWalk(direction)) return;

            User.QueuedAction = new QueuedAction { Action = MirAction.Walking, Direction = direction, Location = Functions.PointMove(User.CurrentLocation, direction, 1) };
        }

        private void UseMagic(ClientMagic magic)
        {
            if (CMain.Time < GameScene.SpellTime || User.Poison.HasFlag(PoisonType.Stun))
            {
                User.ClearMagic();
                return;
            }

            long delay = magic.Delay;
            //GameScene.Scene.ChatDialog.ReceiveChat(string.Format("[c]Delay before {0}", delay), ChatType.System);
            long origDelay = delay;
            int tmp = 0;
            for (int i = 0; i < GameScene.Scene.Buffs.Count; i++)
                if (GameScene.Scene.Buffs[i].Type == BuffType.SpeedyMagician &&
                    tmp == 0)
                    tmp = GameScene.Scene.Buffs[i].Values[0];
            if (tmp > 0)
            {
                long reduceTime = (origDelay * tmp) / 100;
                origDelay = origDelay - reduceTime;
                //ReceiveChat(string.Format("Delay after {0}", delay), ChatType.System);
                delay = origDelay;
                //GameScene.Scene.ChatDialog.ReceiveChat(string.Format("[c]Delay after {0}", delay), ChatType.System);
            }

            if ((CMain.Time <= magic.CastTime + delay) && magic.CastTime > 0)
            {
                if (CMain.Time >= OutputDelay)
                {
                    OutputDelay = CMain.Time + 1000;
                    GameScene.Scene.OutputMessage(string.Format("You cannot cast {0} for another {1} seconds.", magic.Spell.ToString(), ((magic.CastTime + delay) - CMain.Time - 1) / 1000 + 1));
                }

                User.ClearMagic();
                return;
            }
            
            SkillCoolDownInfo info = new SkillCoolDownInfo
            {
                SpellType = magic.Spell,
                CoolDown = CMain.Time + magic.Delay,
                Delay = magic.Delay,
                //CastTime = magic.CastTime,
                IconIndex = magic.Icon * 2,
                //DrawnIndexes = 0,
                //NextDrawIndex = 1260
            };
            
            GameScene.Scene.SkillCoolDownDialog.AddSkillCoolDown(info);

            int cost = magic.Level * magic.LevelCost + magic.BaseCost;

            if (magic.Spell == Spell.Teleport || magic.Spell == Spell.Blink || magic.Spell == Spell.StormEscape)
            {
                for (int i = 0; i < GameScene.Scene.Buffs.Count; i++)
                {
                    if (GameScene.Scene.Buffs[i].Type != BuffType.TemporalFlux) continue;
                    cost += (int)(User.MaxMP * 0.3F);
                }
            }

            if (cost > MapObject.User.MP)
            {
                if (CMain.Time >= OutputDelay)
                {
                    OutputDelay = CMain.Time + 1000;
                    GameScene.Scene.OutputMessage("Not Enough Mana to cast.");
                }
                User.ClearMagic();
                return;
            }

            //bool isTargetSpell = true;

            MapObject target = null;

            //Targeting
            switch (magic.Spell)
            {
                case Spell.FireBall:
                case Spell.GreatFireBall:
                case Spell.ElectricShock:
                case Spell.Poisoning:
                case Spell.ThunderBolt:
                case Spell.FlameDisruptor:
                case Spell.SoulFireBall:
                case Spell.HeadShot:
                case Spell.TurnUndead:
                case Spell.FrostCrunch:
                case Spell.Vampirism:
                case Spell.Revelation:
                case Spell.Entrapment:
                case Spell.Hallucination:
                case Spell.DarkBody:
                case Spell.ChopChopStar:
                case Spell.SoulEaterSwamp:
                case Spell.HandOfGod:
                case Spell.Plague:
                    if (User.NextMagicObject != null)
                    {
                        if (!User.NextMagicObject.Dead && User.NextMagicObject.Race != ObjectType.Item && User.NextMagicObject.Race != ObjectType.Merchant)
                            target = User.NextMagicObject;
                    }

                    if (target == null) target = MapObject.MagicObject;

                    if (target != null && (target.Race == ObjectType.Monster || target.Race == ObjectType.Hero)) MapObject.MagicObject = target;
                    break;
                case Spell.StraightShot:
                case Spell.DoubleShot:
                case Spell.ElementalShot:
                case Spell.DelayedExplosion:
                case Spell.BindingShot:
                case Spell.VampireShot:
                case Spell.PoisonShot:
                case Spell.CrippleShot:
                case Spell.NapalmShot:
                case Spell.SummonVampire:
                case Spell.SummonToad:
                case Spell.SummonSnakes:
                    if (!User.HasClassWeapon)
                    {
                        GameScene.Scene.OutputMessage("You must be wearing a bow to perform this skill.");
                        User.ClearMagic();
                        return;
                    }
                    if (User.NextMagicObject != null)
                    {
                        if (!User.NextMagicObject.Dead && User.NextMagicObject.Race != ObjectType.Item && User.NextMagicObject.Race != ObjectType.Merchant)
                            target = User.NextMagicObject;
                    }

                    if (target == null) target = MapObject.MagicObject;

                    if (target != null && target.Race == ObjectType.Monster) MapObject.MagicObject = target;

                    //if(magic.Spell == Spell.ElementalShot)
                    //{
                    //    isTargetSpell = User.HasElements;
                    //}

                    //switch(magic.Spell)
                    //{
                    //    case Spell.SummonVampire:
                    //    case Spell.SummonToad:
                    //    case Spell.SummonSnakes:
                    //        isTargetSpell = false;
                    //        break;
                    //}

                    break;
                case Spell.Purification:
                case Spell.Healing:
                case Spell.UltimateEnhancer:                
                case Spell.PetEnhancer:
                    if (User.NextMagicObject != null)
                    {
                        if (!User.NextMagicObject.Dead && 
                            User.NextMagicObject.Race != ObjectType.Item &&     //  Target isn't an Item                            
                            User.NextMagicObject.Race != ObjectType.Merchant)   //  Target isn't a Merchant (NPC)
                            target = User.NextMagicObject;                      //  If none of the above object types, set the target
                    }

                    //If the target wasn't assigned above, we force it to use the User (Client AKA Self)
                    if (target == null) target = User;

                    break;
                case Spell.EnergyShield:
                    //  Target
                    if (User.NextMagicObject != null)
                    {
                        if (!User.NextMagicObject.Dead && User.NextMagicObject.Race != ObjectType.Item && User.NextMagicObject.Race != ObjectType.Merchant)
                            if (User.NextMagicObject.Race == ObjectType.Player ||//Player
                                User.NextMagicObject.Race == ObjectType.Hero)
                                target = User.NextMagicObject;
                    }
                    //  No Target (cast on self)
                    if (target == null) target = User;
                    
                    break;
                case Spell.FireBang:
                case Spell.MassHiding:
                case Spell.FireWall:
                case Spell.HealingCircle:
                case Spell.TrapHexagon:
                    if (User.NextMagicObject != null)
                    {
                        if (!User.NextMagicObject.Dead && User.NextMagicObject.Race != ObjectType.Item && User.NextMagicObject.Race != ObjectType.Merchant)
                            target = User.NextMagicObject;
                    }
                    break;
                case Spell.PoisonCloud:
                    if (User.NextMagicObject != null)
                    {
                        if (!User.NextMagicObject.Dead && User.NextMagicObject.Race != ObjectType.Item && User.NextMagicObject.Race != ObjectType.Merchant)
                            target = User.NextMagicObject;
                    }
                    break;
                case Spell.Blizzard:
                case Spell.MeteorStrike:
                case Spell.FrozenRains:
                case Spell.LavaKing:
                    if (User.NextMagicObject != null)
                    {
                        if (!User.NextMagicObject.Dead && User.NextMagicObject.Race != ObjectType.Item && User.NextMagicObject.Race != ObjectType.Merchant)
                            target = User.NextMagicObject;
                    }
                    break;
                case Spell.Reincarnation:
                    if (User.NextMagicObject != null)
                    {
                        if (User.NextMagicObject.Dead && User.NextMagicObject.Race == ObjectType.Player)
                            target = User.NextMagicObject;
                    }
                    break;
                case Spell.Trap:
                    if (User.NextMagicObject != null)
                    {
                        if (!User.NextMagicObject.Dead && User.NextMagicObject.Race != ObjectType.Item && User.NextMagicObject.Race != ObjectType.Merchant)
                            target = User.NextMagicObject;
                    }
                    break;
                case Spell.FlashDash:
                    if (User.GetMagic(Spell.FlashDash).Level <= 1 && User.IsDashAttack() == false)
                    {
                        User.ClearMagic();
                        return;
                    }
                    //isTargetSpell = false;
                    break;
                default:
                    //isTargetSpell = false;
                        break;
            }

            MirDirection dir = (target == null || target == User) ? User.NextMagicDirection : Functions.DirectionFromPoint(User.CurrentLocation, target.CurrentLocation);

            Point location = target != null ? target.CurrentLocation : User.NextMagicLocation;

            if (magic.Spell == Spell.FlashDash)
                dir = User.Direction;

            if ((magic.Range != 0) && (!Functions.InRange(User.CurrentLocation, location, magic.Range)))
            {
                if (CMain.Time >= OutputDelay)
                {
                    OutputDelay = CMain.Time + 1000;
                    GameScene.Scene.OutputMessage("Target is too far.");
                }
                User.ClearMagic();
                return;
            }

            GameScene.LogTime = CMain.Time + Globals.LogDelay;

            User.QueuedAction = new QueuedAction { Action = MirAction.Spell, Direction = dir, Location = User.CurrentLocation, Params = new List<object>() };
            User.QueuedAction.Params.Add(magic.Spell);
            User.QueuedAction.Params.Add(target != null ? target.ObjectID : 0);
            User.QueuedAction.Params.Add(location);
            User.QueuedAction.Params.Add(magic.Level);
        }

        public static MirDirection MouseDirection(float ratio = 45F) //22.5 = 16
        {
            Point p = new Point(MouseLocation.X / CellWidth, MouseLocation.Y / CellHeight);
            if (Functions.InRange(new Point(OffSetX, OffSetY), p, 2))
                return Functions.DirectionFromPoint(new Point(OffSetX, OffSetY), p);

            PointF c = new PointF(OffSetX * CellWidth + CellWidth / 2F, OffSetY * CellHeight + CellHeight / 2F);
            PointF a = new PointF(c.X, 0);
            PointF b = MouseLocation;
            float bc = (float)Distance(c, b);
            float ac = bc;
            b.Y -= c.Y;
            c.Y += bc;
            b.Y += bc;
            float ab = (float)Distance(b, a);
            double x = (ac * ac + bc * bc - ab * ab) / (2 * ac * bc);
            double angle = Math.Acos(x);

            angle *= 180 / Math.PI;

            if (MouseLocation.X < c.X) angle = 360 - angle;
            angle += ratio / 2;
            if (angle > 360) angle -= 360;

            return (MirDirection)(angle / ratio);
        }

        public static int Direction16(Point source, Point destination)
        {
            PointF c = new PointF(source.X, source.Y);
            PointF a = new PointF(c.X, 0);
            PointF b = new PointF(destination.X, destination.Y);
            float bc = (float)Distance(c, b);
            float ac = bc;
            b.Y -= c.Y;
            c.Y += bc;
            b.Y += bc;
            float ab = (float)Distance(b, a);
            double x = (ac * ac + bc * bc - ab * ab) / (2 * ac * bc);
            double angle = Math.Acos(x);

            angle *= 180 / Math.PI;

            if (destination.X < c.X) angle = 360 - angle;
            angle += 11.25F;
            if (angle > 360) angle -= 360;

            return (int)(angle / 22.5F);
        }

        public static double Distance(PointF p1, PointF p2)
        {
            double x = p2.X - p1.X;
            double y = p2.Y - p1.Y;
            return Math.Sqrt(x * x + y * y);
        }

        private bool EmptyCell(Point p)
        {
            if ((M2CellInfo[p.X, p.Y].BackImage & 0x20000000) != 0 || (M2CellInfo[p.X, p.Y].FrontImage & 0x8000) != 0) // + (M2CellInfo[P.X, P.Y].FrontImage & 0x7FFF) != 0)
                return false;

            for (int i = 0; i < Objects.Count; i++)
            {
                MapObject ob = Objects[i];

                if (ob.CurrentLocation == p && ob.Blocking)
                    return false;
            }

            return true;
        }

        private bool CanWalk(MirDirection dir)
        {
            return EmptyCell(Functions.PointMove(User.CurrentLocation, dir, 1)) && !User.InTrapRock;
        }

        private bool CheckDoorOpen(Point p)
        {
            if (M2CellInfo[p.X, p.Y].DoorIndex == 0) return true;
            Door DoorInfo = GetDoor(M2CellInfo[p.X, p.Y].DoorIndex);
            if (DoorInfo == null) return false;//if the door doesnt excist then it isnt even being shown on screen (and cant be open lol)
            if ((DoorInfo.DoorState == 0) || (DoorInfo.DoorState == 3))
            {
                Network.Enqueue(new C.Opendoor() { DoorIndex = DoorInfo.index });
                return false;
            }
            if ((DoorInfo.DoorState == 2) && (DoorInfo.LastTick + 4000 > CMain.Time))
            {
                Network.Enqueue(new C.Opendoor() { DoorIndex = DoorInfo.index });
            }
            return true;
        }


        private bool CanRun(MirDirection dir)
        {
            if (User.InTrapRock) return false;
            if (User.CurrentBagWeight > User.MaxBagWeight) return false;

            if (CanWalk(dir) && EmptyCell(Functions.PointMove(User.CurrentLocation, dir, 2)))
            {
                if (User.RidingMount || User.Sprint && !User.Sneaking)
                {
                    return EmptyCell(Functions.PointMove(User.CurrentLocation, dir, 3));
                }

                return true;
            }

            return false;
        }

        private bool CanRideAttack()
        {
            if (GameScene.User.RidingMount)
            {
                UserItem item = GameScene.User.Equipment[(int)EquipmentSlot.Mount];
                if (item == null || item.Slots.Length < 4 || item.Slots[(int)MountSlot.Bells] == null) return false;
            }

            return true;
        }

        public bool CanFish(MirDirection dir)
        {
            if (!GameScene.User.HasFishingRod || GameScene.User.FishingTime + 1000 > CMain.Time) return false;
            if (GameScene.User.CurrentAction != MirAction.Standing) return false;
            if (GameScene.User.Direction != dir) return false;
            if (GameScene.User.TransformType >= 6 && GameScene.User.TransformType <= 9) return false;

            Point point = Functions.PointMove(User.CurrentLocation, dir, 3);

            if (!M2CellInfo[point.X, point.Y].FishingCell) return false;

            return true;
        }

        public bool CanFly(Point target)
        {
            Point location = User.CurrentLocation;
            while (location != target)
            {
                MirDirection dir = Functions.DirectionFromPoint(location, target);

                location = Functions.PointMove(location, dir, 1);

                if (location.X < 0 || location.Y < 0 || location.X >= GameScene.Scene.MapControl.Width || location.Y >= GameScene.Scene.MapControl.Height) return false;

                if (!GameScene.Scene.MapControl.ValidPoint(location)) return false;
            }

            return true;
        }


        public bool ValidPoint(Point p)
        {
            //GameScene.Scene.ChatDialog.ReceiveChat(string.Format("cell: {0}", (M2CellInfo[p.X, p.Y].BackImage & 0x20000000)), ChatType.Hint);
            return (M2CellInfo[p.X, p.Y].BackImage & 0x20000000) == 0;
        }
        public bool HasTarget(Point p, bool checkIfPlayer = false)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                MapObject ob = Objects[i];

                if (ob.CurrentLocation == p && ob.Race == ObjectType.Monster && !checkIfPlayer && !ob.Dead)
                    return true;
                if (checkIfPlayer)
                    if (ob.CurrentLocation == p && (ob.Race == ObjectType.Monster || ob.Race == ObjectType.Player) && !ob.Dead)
                         return true;
            }
            return false;
        }
        public bool CanHalfMoon(Point p, MirDirection d)
        {
            d = Functions.PreviousDir(d);
            for (int i = 0; i < 4; i++)
            {
                if (HasTarget(Functions.PointMove(p, d, 1))) return true;
                d = Functions.NextDir(d);
            }
            return false;
        }
        public bool CanCrossHalfMoon(Point p)
        {
            MirDirection dir = MirDirection.Up;
            for (int i = 0; i < 8; i++)
            {
                if (HasTarget(Functions.PointMove(p, dir, 1))) return true;
                dir = Functions.NextDir(dir);
            }
            return false;
        }

        public bool CanCrescentSlash(Point p, MirDirection d)
        {
            Point[] loc = new Point[3]; //0 = left 1 = center 2 = right
            loc[0] = Functions.PointMove(p, Functions.PreviousDir(d), 1);
            loc[1] = Functions.PointMove(p, d, 1);
            loc[2] = Functions.PointMove(p, Functions.NextDir(d), 1);
            /*
            MirDirection tDir = Functions.PreviousDir(d);
            tDir = Functions.PreviousDir(tDir);

            //  Left
            loc[0] = Functions.PointMove(p, Functions.PreviousDir(d), 1);
            loc[1] = Functions.PointMove(loc[0], tDir, 1);
            loc[5] = Functions.PointMove(loc[0], d, 1);



            tDir = Functions.NextDir(d);
            tDir = Functions.NextDir(tDir);
            //  Front
            loc[2] = Functions.PointMove(p, d, 1);
            loc[6] = Functions.PointMove(p, d, 2);
            //  Right
            loc[3] = Functions.PointMove(p, Functions.NextDir(d), 1);
            loc[4] = Functions.PointMove(loc[3], tDir, 1);
            loc[7] = Functions.PointMove(loc[3], d, 1);
            */
            for (int i = 0; i < 3; i++)
            {
                if (HasTarget(loc[i])) return true;
                //dir = Functions.NextDir(dir);
            }
            return false;
        }

        private void FloorTexture_Disposing(object sender, EventArgs e)
        {
            FloorValid = false;
            _floorTexture = null;

            if (_floorSurface != null && !_floorSurface.Disposed)
                _floorSurface.Dispose();
            _floorSurface = null;
        }
        #region Disposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Objects.Clear();

                MapButtons = 0;
                MouseLocation = Point.Empty;
                InputDelay = 0;
                NextAction = 0;

                M2CellInfo = null;
                Width = 0;
                Height = 0;

                FileName = String.Empty;
                Title = String.Empty;
                MiniMap = 0;
                BigMap = 0;
                Lights = 0;
                FloorValid = false;
                LightsValid = false;
                MapDarkLight = 0;
                Music = 0;

                if (_floorSurface != null && !_floorSurface.Disposed)
                    _floorSurface.Dispose();


                if (_lightSurface != null && !_lightSurface.Disposed)
                    _lightSurface.Dispose();

                AnimationCount = 0;
                Effects.Clear();
            }

            base.Dispose(disposing);
        }

        #endregion



        public void RemoveObject(MapObject ob)
        {
            M2CellInfo[ob.MapLocation.X, ob.MapLocation.Y].RemoveObject(ob);
        }
        public void AddObject(MapObject ob)
        {
            M2CellInfo[ob.MapLocation.X, ob.MapLocation.Y].AddObject(ob);
        }
        public MapObject FindObject(uint ObjectID, int x, int y)
        {
            return M2CellInfo[x, y].FindObject(ObjectID);
        }
        public void SortObject(MapObject ob)
        {
            M2CellInfo[ob.MapLocation.X, ob.MapLocation.Y].Sort();
        }

        public Door GetDoor(byte Index)
        {
            for (int i = 0; i < Doors.Count; i++)
            {
                if (Doors[i].index == Index)
                    return Doors[i];
            }
            return null;
        }
        public void Processdoors()
        {
            for (int i = 0; i < Doors.Count; i++)
            {
                if ((Doors[i].DoorState == 1) || (Doors[i].DoorState == 3))
                {
                    if (Doors[i].LastTick + 50 < CMain.Time)
                    {
                        Doors[i].LastTick = CMain.Time;
                        Doors[i].ImageIndex++;
                        if (Doors[i].ImageIndex == 1)//change the 1 if you want to actualy animate doors opening/closing
                        {
                            Doors[i].ImageIndex = 0;
                            Doors[i].DoorState = (byte)(++Doors[i].DoorState % 4);
                        }
                        FloorValid = false;
                    }
                }
                if (Doors[i].DoorState == 2)
                {
                    if (Doors[i].LastTick + 5000 < CMain.Time)
                    {
                        Doors[i].LastTick = CMain.Time;
                        Doors[i].DoorState = 3;
                        FloorValid = false;
                    }
                }
            }
        }
        public void OpenDoor(byte Index, bool Closed)
        {
            Door Info = GetDoor(Index);
            if (Info == null) return;
            Info.DoorState = (byte)(Closed? 3: Info.DoorState == 2? 2: 1);
            Info.ImageIndex = 0;
            Info.LastTick = CMain.Time;
        }
    }

    public class Buff
    {
        public BuffType Type;
        public string Caster;
        public bool Visible;
        public uint ObjectID;
        public long Expire;
        public int[] Values;
        public bool Infinite;
        public bool Hero;
        public bool Paused;
        public override string ToString()
        {
            string text = string.Empty; 
            switch (Type)
            {
                //magic
                case BuffType.TemporalFlux:
                    text = string.Format("Temporal Flux\nIncreases cost of next Teleport by: {0} MP.\n", (int)(MapObject.User.MaxMP * 0.3F));
                    break;
                case BuffType.ShadowStepFlux:
                    text = string.Format("Temporal Relocation\nIncreases cost of next use by : {0} MP\n", Values[0]);
                    break;
                case BuffType.Hiding:
                    text = "Hiding\nInvisible to many monsters.\n";
                    break;
                case BuffType.Cloak:
                    text = "Cloak\nInvisible to many monsters.\n";
                    break;
                case BuffType.MoonMist:
                    text = "MoonMist\nInvisible to many Monsters.\nFirst hit before Hiding effect disappears will\ncause more damage.";
                    break;
                case BuffType.Haste:
                    text = string.Format("Haste\nIncreases Attack Speed by: {0}.\n", Values[0]);
                    break;
                case BuffType.SwiftFeet:
                    text = string.Format("Swift Feet\nIncreases Move Speed by: {0}.\n", Values[0]);
                    break;
                case BuffType.FastMove:
                    text = string.Format("Swift Feet\nRun while channeling spells\n", Values[0]);
                    break;
                case BuffType.Fury:
                    text = string.Format("Fury\nIncreases Attack Speed by: {0}.\n", Values[0]);
                    break;
                case BuffType.LightBody:
                    text = string.Format("Light Body\nIncreases Agility by: {0}.\n", Values[0]);
                    break;
                case BuffType.SoulShield:
                    text = string.Format("Soul Shield\nIncreases MAC by: 0-{0}.\n", Values[0]);
                    break;
                case BuffType.BlessedArmour:
                    text = string.Format("Blessed Armour\nIncreases AC by: 0-{0}.\n", Values[0]);
                    break;
                case BuffType.ProtectionField:
                    text = string.Format("Protection Field\nIncreases AC by: 0-{0}.\n", Values[0]);
                    break;
                case BuffType.Rage:
                    text = string.Format("Rage\nIncreases DC by: 0-{0}.\n", Values[0]);
                    break;
                case BuffType.ImmortalSkin:
                    text = string.Format("ImmortalSkin\nIncreases AC by: {0}-{1}.\nIncreases MAC by: {2}-{3}\nDecreases MaxHP by: {4}%.\n", Values[0], Values[1], Values[2], Values[3], Values[4]);
                    break;
                case BuffType.CounterAttack:
                    text = string.Format("Counter Attack\nIncreases AC/MAC by: {0}-{1}.\n", Values[0], Values[0]);
                    break;
                case BuffType.UltimateEnhancer:
                case BuffType.UltimateEnhancerQuest:

                    if (GameScene.Hero != null && Hero)
                    {
                        if (GameScene.Hero.Class == MirClass.Wizard || GameScene.Hero.Class == MirClass.Archer)
                        {
                            text = string.Format("Ultimate Enhancer\nIncreases MC by: 0-{0}.\n", Values[0]);
                        }
                        else if (GameScene.Hero.Class == MirClass.Taoist)
                        {
                            text = string.Format("Ultimate Enhancer\nIncreases SC by: 0-{0}.\n", Values[0]);
                        }
                        else
                        {
                            text = string.Format("Ultimate Enhancer\nIncreases DC by: 0-{0}.\n", Values[0]);
                        }
                    }
                    else
                    {
                        if (GameScene.User.Class == MirClass.Wizard || GameScene.User.Class == MirClass.Archer)
                        {
                            text = string.Format("Ultimate Enhancer\nIncreases MC by: 0-{0}.\n", Values[0]);
                        }
                        else if (GameScene.User.Class == MirClass.Taoist)
                        {
                            text = string.Format("Ultimate Enhancer\nIncreases SC by: 0-{0}.\n", Values[0]);
                        }
                        else
                        {
                            text = string.Format("Ultimate Enhancer\nIncreases DC by: 0-{0}.\n", Values[0]);
                        }
                    }
                    break;
                case BuffType.CombinedBuff:
                    {
                        if (GameScene.Hero != null && Hero)
                        {
                            if (GameScene.Hero.Class == MirClass.Wizard || GameScene.Hero.Class == MirClass.Archer)
                            {
                                text = string.Format("Holy Shield\nIncreases MC by: 0-{0}.\n", Values[0]);
                            }
                            else if (GameScene.Hero.Class == MirClass.Taoist)
                            {
                                text = string.Format("Holy Shield\nIncreases SC by: 0-{0}.\n", Values[0]);
                            }
                            else
                            {
                                text = string.Format("Holy Shield\nIncreases DC by: 0-{0}.\n", Values[0]);
                            }
                        }
                        else
                        {
                            if (GameScene.User.Class == MirClass.Wizard || GameScene.User.Class == MirClass.Archer)
                            {
                                text = string.Format("Holy Shield\nIncreases MC by: 0-{0}.\n", Values[0]);
                            }
                            else if (GameScene.User.Class == MirClass.Taoist)
                            {
                                text = string.Format("Holy Shield\nIncreases SC by: 0-{0}.\n", Values[0]);
                            }
                            else
                            {
                                text = string.Format("Holy Shield\nIncreases DC by: 0-{0}.\n", Values[0]);
                            }
                        }
                        text += string.Format("AC Increased by: 0-{0}.\nMAC Increased by: 0-{1}.\n", Values[1], Values[2]);
                    }
                    break;
                case BuffType.EnergyShield:
                    text = string.Format("Energy Shield\n{0}% chance to gain {1} HP when attacked\n", Math.Round((1 / (decimal)Values[0]) * 100), Values[1]);
                    break;
                case BuffType.HeroEnergyShield:
                    text = string.Format("[Hero]Energy Shield\nHero has a {0}% chance to gain {1} HP when attacked\n", Math.Round((1 / (decimal)Values[0]) * 100), Values[1]);
                    break;
                case BuffType.Curse:
                    text = string.Format("Cursed\nDecreases DC/MC/SC/ASpeed by: {0}%.\n", Values[0]);
                    break;
                case BuffType.MoonLight:
                    text = "Moon Light\nInvisible to players and many\nmonsters when at a distance.\n";
                    break;
                case BuffType.DarkBody:
                    text = "Dark Body\nInvisible to many monsters and able to move.\n";
                    break;
                case BuffType.VampireShot:
                    text = string.Format("Vampire Shot\nGives you a vampiric ability\nthat can be released with\ncertain skills.\n", Values[0]);
                    break;
                case BuffType.VIP:
                    text = "VIP [" + MapObject.User.VIP + "]\n";            
                    text += "-Experience bonus: " + Values[0] + "%";
                    break;
                case BuffType.NewbieGuild:
                    text = "NewbieGuild\n";
                    text += "-Experience bonus: " + Values[0] + "%";
                    break;
                case BuffType.HeroBuff:
                    text = "Hero Buff\n";

                    if (GameScene.Hero.Class == MirClass.Warrior)
                    {
                        if (Values[0] > 0)
                            text += "-MaxHp bonus: " + Values[0] + "\n";
                        if (Values[1] > 0)
                            text += "-MaxMp bonus: " + Values[1] + "\n";
                    }

                    if (GameScene.Hero.Class == MirClass.Wizard)
                    {
                        if (Values[2] > 0)
                            text += "-ExpRate bonus: " + Values[2] + "%\n";
                        if (Values[3] > 0)
                            text += "-DropRate bonus: " + Values[3] + "%\n";
                    }

                    if (GameScene.Hero.Class == MirClass.Taoist)
                    {
                        if (Values[4] > 0)
                            text += "-HPRegen bonus: " + Values[4] + "%\n";
                        if (Values[5] > 0)
                            text += "-MpRegen bonus: " + Values[5] + "%\n";
                    }

                    if (GameScene.Hero.Class == MirClass.Assassin)
                    {
                        if (Values[6] > 0)
                            text += "-Agility bonus: " + Values[6] + "\n";
                        if (Values[7] > 0)
                            text += "-Accuracy bonus: " + Values[7] + "\n";
                    }
                    break;
                case BuffType.SwBuff:
                    text = "SW Buff\n";
                    if (Values[0] > 0)
                        text += string.Format("EXP rate {0}%\n", Values[0]);
                    if (Values[1] > 0)
                        text += string.Format("Drop rate {0}%\n", Values[1]);
                    if (Values[2] > 0 && Values[3] > 0)
                        text += string.Format("AC + {0}-{1}\n", Values[2], Values[3]);
                    else if (Values[3] > 0)
                        text += string.Format("AC + 0-{0}\n", Values[3]);
                    if (Values[4] > 0 && Values[5] > 0)
                        text += string.Format("MAC + {0}-{1}\n", Values[4], Values[5]);
                    else if (Values[5] > 0)
                        text += string.Format("MAC + 0-{0}\n", Values[5]);
                    if (Values[6] > 0 && Values[7] > 0)
                        text += string.Format("DC + {0}-{1}\n", Values[6], Values[7]);
                    else if (Values[7] > 0)
                        text += string.Format("DC + 0-{0}\n", Values[7]);
                    if (Values[8] > 0 && Values[9] > 0)
                        text += string.Format("MC + {0}-{1}\n", Values[8], Values[9]);
                    else if (Values[9] > 0)
                        text += string.Format("MC + 0-{0}\n", Values[9]);
                    if (Values[10] > 0 && Values[11] > 0)
                        text += string.Format("SC + {0}-{1}\n", Values[10], Values[11]);
                    else if (Values[11] > 0)
                        text += string.Format("SC + 0-{0}\n", Values[11]);
                    if (Values[12] > 0)
                        text += string.Format("Accuracy + {0}\n", Values[12]);
                    if (Values[13] > 0)
                        text += string.Format("Agility + {0}\n", Values[13]);
                    if (Values[14] > 0)
                        text += string.Format("HP + {0}\n", Values[14]);
                    if (Values[15] > 0)
                        text += string.Format("MP + {0}\n", Values[15]);
                    if (Values[16] > 0)
                        text += string.Format("Speed + {0}\n", Values[16]);
                    break;
                case BuffType.HumUp:
                    text = "EliteBuff\n";
                    
                    if (GameScene.User.Class == MirClass.Warrior)
                    {
                            text += "-MaxHp: 220\n";
                            text += "-MaxMp: 130\n";
                            text += "-HealthRecovery: 10\n";
                            text += "-SpellRecovery: 10\n";
                            text += "-MaxBagWeight: 80\n";
                    }

                    if (GameScene.User.Class == MirClass.Wizard)
                    {
                            text += "-MaxHp: 140\n";
                            text += "-MaxMp: 210\n";
                            text += "-HealthRecovery: 10\n";
                            text += "-SpellRecovery: 10\n";
                            text += "-MaxBagWeight: 80\n";
                    }

                    if (GameScene.User.Class == MirClass.Taoist)
                    {
                            text += "-MaxHp: 170\n";
                            text += "-MaxMp: 180\n";
                            text += "-HealthRecovery: 10\n";
                            text += "-SpellRecovery: 10\n";
                            text += "-MaxBagWeight: 80\n";
                    }

                    if (GameScene.User.Class == MirClass.Assassin)
                    {
                            text += "-MaxHp: 160\n";
                            text += "-MaxMp: 200\n";
                            text += "-HealthRecovery: 10\n";
                            text += "-SpellRecovery: 10\n";
                            text += "-MaxBagWeight: 80\n";
                    }
                    break;
                case BuffType.PoisonShot:
                    text = string.Format("Poison Shot\nGives you a poison ability\nthat can be released with\ncertain skills.\n", Values[0]);
                    break;
                case BuffType.Concentration:
                    text = "Concentrating\nIncreases chance on element extraction.\n";
                    break;
                case BuffType.MentalState:
                    switch (Values[0])
                    {
                        case 0:
                            text = string.Format("Agressive (Full damage)\nCan't shoot over walls.\n", Values[0]);
                            break;
                        case 1:
                            text = string.Format("Trick shot (Minimal damage)\nCan shoot over walls.\n", Values[0]);
                            break;
                        case 2:
                            text = string.Format("Group Mode (Medium damage)\nDon't steal agro.\n", Values[0]);
                            break;
                    }
                    break;
                case BuffType.Global:
                    text = string.Format("Global\n");
                    if (Values[0] > 0)
                    text += string.Format("-Experience buff: {0}%\n", Values[0]);
                    if (Values[1] > 0)
                        text += string.Format("-Drop buff: {0}%\n", Values[1]);
                    if (Values[2] > 0)
                        text += string.Format("-Gold buff: {0}%\n", Values[2]);
                    break;
                case BuffType.Group:
                    text = string.Format("Group System(Stage {0}\n",Values[1]);
                    text += string.Format("-Experience Buff: {0}%\n", Values[0]);
                    break;
                case BuffType.MagicBooster:
                    text = string.Format("Magic Booster\nIncreases MC by: {0}-{0}.\nIncreases consumption by {1}%.\n", Values[0], Values[1]);
                    break;
                case BuffType.MagicShield:
                    text = string.Format("Magic Shield\nReduces damage by {0}%.\n", Hero ? Values[0] : (Values[0] + 2) * 10);
                    break;

                //special
                case BuffType.GameMaster:
                    GMOptions options = (GMOptions)Values[0];
                    text = "GameMaster\n";

                    if (options.HasFlag(GMOptions.GameMaster)) text += "-Invisible\n";
                    if (options.HasFlag(GMOptions.Superman)) text += "-Superman\n";
                    if (options.HasFlag(GMOptions.Observer)) text += "-Observer\n";
                    break;
                case BuffType.General:
                    text = string.Format("Mirian Advantage\nExpRate increased by {0}%\n", Values[0]);

                    if (Values.Length > 1)
                        text += string.Format("DropRate increased by {0}%\n", Values[1]);
                    if (Values.Length > 2)
                        text += string.Format("GoldRate increased by {0}%\n", Values[2]);
                    break;
                case BuffType.Exp:
                case BuffType.ExpQuest:
                    text = string.Format("Exp Rate\nIncreased by {0}%\n", Values[0]);
                    break;
                case BuffType.Gold:
                    text = string.Format("Gold Rate\nIncreased by {0}%\n", Values[0]);
                    break;
                case BuffType.Drop:
                case BuffType.DropQuest:
                    text = string.Format("Drop Rate\nIncreased by {0}%\n", Values[0]);
                    break;
                case BuffType.BagWeight:
                    text = string.Format("Bag Weight\nIncreases BagWeight by: {0}.\n", Values[0]);
                    break;
                case BuffType.Transform:
                    text = string.Format("Transform\nDisguises your appearance.\n");
                    break;
                case BuffType.RelationshipEXP:
                    text = string.Format("Love is in the Air\nExpRate increased by: {0}%.\n", Values[0]);
                    break;
                case BuffType.Mentee:
                    text = string.Format("In Training\nLearn skill points twice as quick.\nExpRate increased by: {0}%.\n", Values[0]);
                    break;
                case BuffType.Mentor:
                    text = string.Format("Mentorship Empowerment\nDamage to monsters increased by {0}%.\n", Values[0]);
                    break;
                case BuffType.Guild:
                    text = string.Format("Guild Charge\n");
                    text += GameScene.Scene.GuildDialog.ActiveStats;
                    break;
                case BuffType.Rested:
                    text = string.Format("Rested\nIncreases Exp Rate by {0}%\n", Values[0]);
                    break;

                //stats
                case BuffType.Impact:
                    text = string.Format("Impact\nIncreases DC by: 0-{0}.\n", Values[0]);
                    break;
                case BuffType.Magic:
                    text = string.Format("Magic\nIncreases MC by: 0-{0}.\n", Values[0]);
                    break;
                case BuffType.Taoist:
                    text = string.Format("Taoist\nIncreases SC by: 0-{0}.\n", Values[0]);
                    break;
                case BuffType.Storm:
                case BuffType.StormQuest:
                    text = string.Format("Storm\nIncreases A.Speed by: {0}.\n", Values[0]);
                    break;
                case BuffType.Accuracy:
                case BuffType.AccuracyQuest:
                    text = string.Format("Accuracy\nIncreases Accuracy by: {0}.\n", Values[0]);
                    break;
                case BuffType.Agility:
                    text = string.Format("Agility\nIncreases Agility by: {0}.\n", Values[0]);
                    break;
                case BuffType.HealthAid:
                    text = string.Format("Health Aid\nIncreases HP by: {0}.\n", Values[0]);
                    break;
                case BuffType.ManaAid:
                    text = string.Format("Mana Aid\nIncreases MP by: {0}.\n", Values[0]);
                    break;
                case BuffType.Defence:
                    text = string.Format("Defence\nIncreases Max AC by: {0}-{0}.\n", Values[0]);
                    break;
                case BuffType.MagicDefence:
                    text = string.Format("Magic Defence\nIncreases Max MAC by: {0}-{0}.\n", Values[0]);
                    break;
                case BuffType.WonderDrug:
                    text = string.Format("Wonder Drug\n");
                    switch (Values[0])
                    {
                        case 0:
                            text += string.Format("Increases Exp Rate by {0}%\n", Values[1]);
                            break;
                        case 1:
                            text += string.Format("Increases Drop Rate by {0}%\n", Values[1]);
                            break;
                        case 2:
                            text += string.Format("Increases HP by: {0}.\n", Values[1]);
                            break;
                        case 3:
                            text += string.Format("Increases MP by: {0}.\n", Values[1]);
                            break;
                        case 4:
                            text += string.Format("Increases Max AC by: {0}-{0}.\n", Values[1]);
                            break;
                        case 5:
                            text += string.Format("Increases Max MAC by: {0}-{0}.\n", Values[1]);
                            break;
                        case 6:
                            text += string.Format("Increases A.Speed by: {0}.\n", Values[1]);
                            break;
                    }
                    break;
                case BuffType.Knapsack:
                    text = string.Format("Knapsack\nIncreases BagWeight by: {0}.\n", Values[0]);
                    break;
                case BuffType.PinPoint:
                    text = string.Format("PinPoint\nIncreases Accuracy by: {0}.\nMax DC by: {1}\nCrit Rate by: {2}\nCrit Damage by: {3}", Values[0], Values[1], Values[2], Values[3]);
                    break;
                case BuffType.Enrage:
                    text = string.Format("Enrage\nIncreases Attack Speed by: {0}.\nMax DC by: {1}\nCrit Rate by: {2}\nCrit Damage by: {3}", Values[0], Values[1], Values[2], Values[3]);
                    break;
                case BuffType.IronWall:
                    text = string.Format("IronWall\nIncreases AC by: {0}-{1}.\nMAC by: {0}-{1}\nHP by: {2}\nAgility by: {3}", Values[0], Values[1], Values[2], Values[3]);
                    break;
                case BuffType.Evasive:
                    text = string.Format("Evasive\nIncreases AC by: 0-{0}.\nMAC by: 0-{0}\nHP by: {1}\nAgility by: {2}", Values[0], Values[1], Values[2]);
                    break;
                case BuffType.SpeedyMagician:
                    text = string.Format("Speedy Magician\nReduces Cool-downs by {0}%", Values[0]);
                    break;
                case BuffType.PetEnhancer:
                    text = string.Format("Pet Enhancer\nIncrease Pet stats as follows\nDC + {0}\nAC + {1}", Values[0], Values[1]);
                    break;
                case BuffType.MobDebuff:
                    {
                        text = string.Format("Debuff");
                        if (Values.Length > 0 && Values[0] > 0)
                            text += string.Format("\nDecrease HP by: {0}", Values[0]);
                        if (Values.Length > 1 && Values[1] > 0)
                            text += string.Format("\nDecrease MP by: {0}", Values[1]);
                        switch(GameScene.User.Class)
                        {
                            case MirClass.Warrior:
                            case MirClass.Assassin:
                                if (Values.Length > 2 && Values[2] > 0)
                                    text += string.Format("\nDecrease DC by: {0}-{0}", Values[2]);
                                break;
                            case MirClass.Wizard:
                                if (Values.Length > 3 && Values[3] > 0)
                                    text += string.Format("\nDecrease MC by: {0}-{0}", Values[3]);
                                break;
                            case MirClass.Taoist:
                                if (Values.Length > 3 && Values[2] > 0)
                                    text += string.Format("\nDecrease DC by: {0}-{0}", Values[2]);
                                if (Values.Length > 4 && Values[4] > 0)
                                    text += string.Format("\nDecrease SC by: {0}-{0}", Values[4]);
                                break;
                            case MirClass.Archer:
                                break;
                        }
                        if (Values.Length > 5 && Values[5] > 0)
                            text += string.Format("\nDecrease AC by: {0}-{0}", Values[5]);
                        if (Values.Length > 6 && Values[6] > 0)
                            text += string.Format("\nDecrease MAC by: {0}-{0}", Values[6]);
                        if (Values.Length > 7 && Values[7] > 0)
                            text += string.Format("\nDecrease Crit Damage by: {0}", Values[7]);
                        if (Values.Length > 8 && Values[8] > 0)
                            text += string.Format("\nDecrease Reflect by: {0}", Values[8]);
                        if (Values.Length > 9 && Values[9] > 0)
                            text += string.Format("\nDecrease HP Drain by: {0}", Values[9]);
                        text += "\n";
                    }
                    break;
                case BuffType.LoverRecall:
                    {
                        text = string.Format("Lover Recall\nYou must wait another {0}", Functions.PrintTimeSpanFromMilliSeconds(Math.Round((Expire - CMain.Time) / 1000D)));
                    }
                    break;
            }
            if (Type != BuffType.LoverRecall)
            {
                if (!Paused)
                    text += string.Format("Expire: {0}", Infinite ? "Never" : Functions.PrintTimeSpanFromSeconds(Math.Round((Expire - CMain.Time) / 1000D)));
                else
                    text += string.Format("Paused.");

                if (Caster.Length > 0) text += string.Format("\nCaster: {0}", Caster);
                if (Hero)
                    text += "\n[Hero Buff]";
            }
            return text;
        }

    }
}

