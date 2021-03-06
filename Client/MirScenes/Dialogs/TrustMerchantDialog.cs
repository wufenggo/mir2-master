﻿using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirSounds;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C = ClientPackets;
using Client.MirObjects;

namespace Client.MirScenes.Dialogs
{
    public sealed class TrustMerchantDialog : MirImageControl
    {
        public static bool UserMode = false;

        public MirDropDownBox PaymentType;

        public bool ggPay = false;

        public uint Amount = 0, MinAmount = Globals.MinConsignment, MaxAmount = Globals.MaxConsignment;

        public static long SearchTime, MarketTime;

        public MirTextBox SearchTextBox, PriceTextBox;
        public MirButton FindButton, RefreshButton, MailButton, BuyButton, CloseButton, NextButton, BackButton;
        public MirImageControl TitleLabel;
        public MirLabel ItemLabel, PriceLabel, SellerLabel, PageLabel;
        public MirLabel DateLabel, ExpireLabel;
        public MirLabel NameLabel, TotalPriceLabel, SplitPriceLabel;

        public MirItemCell ItemCell, tempCell;
        public static UserItem SellItemSlot;
        public MirButton SellItemButton;

        public MirCheckBox ggPayCheck;
        public bool ggSell = false;

        public List<ClientAuction> Listings = new List<ClientAuction>();

        public int Page, PageCount;
        public static AuctionRow Selected;
        public AuctionRow[] Rows = new AuctionRow[10];

        public MirButton UpButton, DownButton, PositionBar;
        public MirButton MarketButton, ConsignmentButton;
        public MirImageControl MarketPage, ConsignmentPage;

        public MirImageControl FilterBox, FilterBackground;
        public MirButton ShowAllButton, WeaponButton, DraperyItemsButton, AccessoriesItemsButton, ConsumableItemsButton;
        public MirButton EnhEquipButton, BooksButton, CraftingSystemButton, PetsItemButton;
        public MirButton ShieldItemButton, RuneStoneItemButton;

        public MirLabel ShowAllLabel, WeaponLabel, DraperyItemsLabel, AccessoriesItemsLabel, ConsumableItemsLabel;
        public MirLabel EnhEquipLabel, BooksLabel, CraftingSystemLabel, PetsItemLabel;
        public MirLabel ShieldItemLabel, RuneStoneLabel;

        public MirButton ArmoursSubBtn, HelmetsSubBtn, BeltsSubBtn, BootsSubBtn, StonesSubBtn;// Drapery Items
        public MirButton NecklaceSubBtn, BraceletsSubBtn, RingsSubBtn;// Accessories Items
        public MirButton RecoveryPotionSubBtn, PowerUpSubBtn, ScrollSubBtn, ScriptSubBtn; //Consumable Items
        public MirButton GemSubBtn, OrbSubBtn, AwakeSubBtn; //Enhanced Equipment
        public MirButton WarriorSubBtn, WizardSubBtn, TaoistSubBtn, AssassinSubBtn, ArcherSubBtn; //Books
        public MirButton MaterialsSubBtn, FishSubBtn, MeatSubBtn, OreSubBtn; //Crafting System
        public MirButton NoveltyPetsSubBtn, NoveltyEquipmentSubBtn, MountsSubBtn, ReinsSubBtn, BellsSubBtn, RibbonSubBtn, MaskSubBtn; //Pets

        public MirButton ShieldSubBtn, MedalsSubBtn, RuneStoneSubBtn;

        public MirLabel ArmoursLabel, HelmetsLabel, BeltsLabel, BootsLabel, StonesLabel;// Drapery Items
        public MirLabel NecklaceLabel, BraceletsLabel, RingsLabel;// Accessories Items
        public MirLabel RecoveryPotionLabel, PowerUpLabel, ScrollLabel, ScriptLabel; //Consumable Items
        public MirLabel GemLabel, OrbLabel, AwakeLabel; //Enhanced Equipment
        public MirLabel WarriorLabel, WizardLabel, TaoistLabel, AssassinLabel, ArcherLabel; //Books
        public MirLabel MaterialsLabel, FishLabel, MeatLabel, OreLabel; //Crafting System
        public MirLabel NoveltyPetsLabel, NoveltyEquipmentLabel, MountsLabel, ReinsLabel, BellsLabel, RibbonLabel, MaskLabel; //Pets

        public MirLabel ShieldLabel, MedalsLabel, RuneLabel;

        public MirLabel totalGold;

        public TrustMerchantDialog()
        {
            Index = 786;
            Library = Libraries.CustomTitle;
            Sort = true;

            TitleLabel = new MirImageControl
            {
                Index = 24,
                Library = Libraries.CustomTitle,
                Location = new Point(174, 9),
                Parent = this
            };

            #region TrustMerchant Buttons

            MarketButton = new MirButton
            {
                Index = 789,
                PressedIndex = 788,
                Library = Libraries.CustomTitle,
                Location = new Point(4, 35),
                Parent = this,
            };
            MarketButton.Click += (o, e) =>
            {

                TMerchantDialog(0);
                if (tempCell != null)
                {
                    tempCell.Locked = false;
                    SellItemSlot = null;
                    tempCell = null;
                }
            };
            ConsignmentButton = new MirButton
            {
                Index = 791,
                PressedIndex = 790,
                Library = Libraries.CustomTitle,
                Location = new Point(98, 35),
                Parent = this,
                Visible = true,
            };
            ConsignmentButton.Click += (o, e) =>
            {
                TMerchantDialog(1);

            };

            CloseButton = new MirButton
            {
                Index = 361,
                HoverIndex = 362,
                PressedIndex = 363,
                Location = new Point(465, 6),
                Library = Libraries.CustomPrguse,
                Sound = SoundList.ButtonA,
                Parent = this,
            };
            CloseButton.Click += (o, e) => Hide();

            #region Page Buttons & Label

            BackButton = new MirButton
            {
                Index = 240,
                HoverIndex = 241,
                PressedIndex = 242,
                Library = Libraries.CustomPrguse2,
                Location = new Point(264, 416),
                Sound = SoundList.ButtonA,
                Parent = this,
            };
            BackButton.Click += (o, e) =>
            {
                if (Page <= 0) return;

                Page--;
                UpdateInterface();
            };

            NextButton = new MirButton
            {
                Index = 243,
                HoverIndex = 244,
                PressedIndex = 245,
                Library = Libraries.CustomPrguse2,
                Location = new Point(330, 416),
                Sound = SoundList.ButtonA,
                Parent = this,
            };
            NextButton.Click += (o, e) =>
            {
                if (Page >= PageCount - 1) return;
                if (Page < (Listings.Count - 1) / 10)
                {
                    Page++;
                    UpdateInterface();
                    return;
                }

                Network.Enqueue(new C.MarketPage { Page = Page + 1 });

            };

            PageLabel = new MirLabel
            {
                Size = new Size(70, 18),
                Location = new Point(270, 416),
                DrawFormat = TextFormatFlags.HorizontalCenter,
                Text = "0/0",
                NotControl = true,
                Parent = this,
            };

            #endregion

            #endregion

            MarketPage = new MirImageControl()
            {
                Index = 786,
                Library = Libraries.CustomTitle,
                Visible = false,
                Sort = true,
            };

            ConsignmentPage = new MirImageControl()
            {
                Index = 787,
                Library = Libraries.CustomTitle,
                Visible = false,
                Sort = true,
            };



            #region Filter Buttons
            ShowAllButton = new MirButton
            {
                Index = 915,
                PressedIndex = 914,
                HoverIndex = 914,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(1, 67),
                Parent = this,
            };
            ShowAllButton.Click += (o, e) => SwitchTab(0);

            ShowAllLabel = new MirLabel
            {
                Size = new Size(99, 18),
                Location = new Point(2, 1),
                Parent = ShowAllButton,
                Text = "Show All Items",
                NotControl = true,
            };

            WeaponButton = new MirButton
            {
                Index = 915,
                PressedIndex = 914,
                HoverIndex = 914,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, ShowAllButton.Location.Y + 20),
                Parent = this,
                Visible = true,
            };
            WeaponButton.Click += (o, e) => SwitchTab(1);

            WeaponLabel = new MirLabel
            {
                Size = new Size(99, 18),
                Location = new Point(2, 1),
                Parent = WeaponButton,
                Text = "Weapon Items",
                NotControl = true,
            };

            DraperyItemsButton = new MirButton
            {
                Index = 915,
                PressedIndex = 914,
                HoverIndex = 914,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, WeaponButton.Location.Y + 20),
                Parent = this,
                Visible = true,
            };

            DraperyItemsButton.Click += (o, e) => SwitchTab(2);

            DraperyItemsLabel = new MirLabel
            {
                Size = new Size(99, 18),
                Location = new Point(2, 1),
                Parent = DraperyItemsButton,
                Text = "Drapery Items",
                NotControl = true,
            };

            #region Drapery Filtering.
            ArmoursSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, DraperyItemsButton.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            ArmoursSubBtn.Click += (o, e) => SwitchTab(9);

            ArmoursLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = ArmoursSubBtn,
                Text = "Amours",
                NotControl = true,
            };

            HelmetsSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, ArmoursSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            HelmetsSubBtn.Click += (o, e) => SwitchTab(10);

            HelmetsLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = HelmetsSubBtn,
                Text = "Helmets",
                NotControl = true,
            };

            BeltsSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, HelmetsSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            BeltsSubBtn.Click += (o, e) => SwitchTab(11);

            BeltsLabel = new MirLabel
            {
                Size = new Size(99, 18),
                Location = new Point(18, 1),
                Parent = BeltsSubBtn,
                Text = "Belts",
                NotControl = true,
            };

            BootsSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, BeltsSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            BootsSubBtn.Click += (o, e) => SwitchTab(12);

            BootsLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = BootsSubBtn,
                Text = "Boots",
                NotControl = true,
            };

            StonesSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, BootsSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            StonesSubBtn.Click += (o, e) => SwitchTab(13);

            StonesLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = StonesSubBtn,
                Text = "Stones",
                NotControl = true,
            };
            #endregion

            AccessoriesItemsButton = new MirButton
            {
                Index = 915,
                PressedIndex = 914,
                HoverIndex = 914,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, DraperyItemsButton.Location.Y + 20),
                Parent = this,
                Visible = true,
            };

            AccessoriesItemsButton.Click += (o, e) => SwitchTab(3);

            AccessoriesItemsLabel = new MirLabel
            {
                Size = new Size(99, 18),
                Location = new Point(2, 1),
                Parent = AccessoriesItemsButton,
                Text = "Accessory Items",
                NotControl = true,
            };

            #region Accessories Items
            NecklaceSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, AccessoriesItemsButton.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            NecklaceSubBtn.Click += (o, e) => SwitchTab(14);

            NecklaceLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = NecklaceSubBtn,
                Text = "Necklaces",
                NotControl = true,
            };

            BraceletsSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, NecklaceSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            BraceletsSubBtn.Click += (o, e) => SwitchTab(15);

            BraceletsLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = BraceletsSubBtn,
                Text = "Bracelets",
                NotControl = true,
            };

            RingsSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, BraceletsSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            RingsSubBtn.Click += (o, e) => SwitchTab(16);

            RingsLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = RingsSubBtn,
                Text = "Rings",
                NotControl = true,
            };
            #endregion

            ConsumableItemsButton = new MirButton
            {
                Index = 915,
                PressedIndex = 914,
                HoverIndex = 914,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, AccessoriesItemsButton.Location.Y + 20),
                Parent = this,
                Visible = true,
            };

            ConsumableItemsButton.Click += (o, e) => SwitchTab(4);

            ConsumableItemsLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(2, 1),
                Parent = ConsumableItemsButton,
                Text = "Consumable Items",
                NotControl = true,
            };

            #region Consumable Items
            RecoveryPotionSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, ConsumableItemsButton.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            RecoveryPotionSubBtn.Click += (o, e) => SwitchTab(17);

            RecoveryPotionLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = RecoveryPotionSubBtn,
                Text = "Recovery Pots",
                NotControl = true,
            };

            PowerUpSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, RecoveryPotionSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            PowerUpSubBtn.Click += (o, e) => SwitchTab(18);

            PowerUpLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = PowerUpSubBtn,
                Text = "Buff Pots",
                NotControl = true,
            };

            ScrollSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, PowerUpSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            ScrollSubBtn.Click += (o, e) => SwitchTab(19);

            ScrollLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = ScrollSubBtn,
                Text = "Scrolls / Oils",
                NotControl = true,
            };

            ScriptSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, ScrollSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            ScriptSubBtn.Click += (o, e) => SwitchTab(20);

            ScriptLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = ScriptSubBtn,
                Text = "Misc Items",
                NotControl = true,
            };
            #endregion

            EnhEquipButton = new MirButton
            {
                Index = 915,
                PressedIndex = 914,
                HoverIndex = 914,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, ConsumableItemsButton.Location.Y + 20),
                Parent = this,
                Visible = true,
            };

            EnhEquipButton.Click += (o, e) => SwitchTab(5);

            EnhEquipLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(2, 1),
                Parent = EnhEquipButton,
                Text = "UpGrade Items",
                NotControl = true,
            };

            #region Enhancing Equipment
            GemSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, EnhEquipButton.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            GemSubBtn.Click += (o, e) => SwitchTab(21);

            GemLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = GemSubBtn,
                Text = "Gems",
                NotControl = true,
            };

            OrbSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, GemSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            OrbSubBtn.Click += (o, e) => SwitchTab(22);

            OrbLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = OrbSubBtn,
                Text = "Orbs",
                NotControl = true,
            };

            AwakeSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, OrbSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            AwakeSubBtn.Click += (o, e) => SwitchTab(23);

            AwakeLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = AwakeSubBtn,
                Text = "Awakening",
                NotControl = true,
            };
            #endregion

            BooksButton = new MirButton
            {
                Index = 915,
                PressedIndex = 914,
                HoverIndex = 914,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, EnhEquipButton.Location.Y + 20),
                Parent = this,
                Visible = true,
            };

            BooksButton.Click += (o, e) => SwitchTab(6);

            BooksLabel = new MirLabel
            {
                Size = new Size(99, 18),
                Location = new Point(2, 1),
                Parent = BooksButton,
                Text = "Books",
                NotControl = true,
            };

            #region Class Books
            WarriorSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, BooksButton.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            WarriorSubBtn.Click += (o, e) => SwitchTab(24);

            WarriorLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = WarriorSubBtn,
                Text = "Warrior",
                NotControl = true,
            };

            WizardSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, WarriorSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            WizardSubBtn.Click += (o, e) => SwitchTab(25);

            WizardLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = WizardSubBtn,
                Text = "Wizard",
                NotControl = true,
            };

            TaoistSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, WizardSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            TaoistSubBtn.Click += (o, e) => SwitchTab(26);

            TaoistLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = TaoistSubBtn,
                Text = "Taoist",
                NotControl = true,
            };

            AssassinSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, TaoistSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            AssassinSubBtn.Click += (o, e) => SwitchTab(27);

            AssassinLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = AssassinSubBtn,
                Text = "Assassin",
                NotControl = true,

            };

            ArcherSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, AssassinSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            ArcherSubBtn.Click += (o, e) => SwitchTab(28);

            ArcherLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = ArcherSubBtn,
                Text = "Archer",
                NotControl = true,
            };
            #endregion

            CraftingSystemButton = new MirButton
            {
                Index = 915,
                PressedIndex = 914,
                HoverIndex = 914,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, BooksButton.Location.Y + 20),
                Parent = this,
                Visible = true,
            };

            CraftingSystemButton.Click += (o, e) => SwitchTab(7);

            CraftingSystemLabel = new MirLabel
            {
                Size = new Size(99, 18),
                Location = new Point(2, 1),
                Parent = CraftingSystemButton,
                Text = "Crafting Items",
                NotControl = true,
            };

            #region Crafting System (CraftingMaterials)
            MaterialsSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, CraftingSystemButton.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            MaterialsSubBtn.Click += (o, e) => SwitchTab(29);

            MaterialsLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = MaterialsSubBtn,
                Text = "Materials",
                NotControl = true,
            };

            FishSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, MaterialsSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            FishSubBtn.Click += (o, e) => SwitchTab(30);

            FishLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = FishSubBtn,
                Text = "Fish",
                NotControl = true,
            };

            MeatSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, FishSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            MeatSubBtn.Click += (o, e) => SwitchTab(31);

            MeatLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = MeatSubBtn,
                Text = "Meat",
                NotControl = true,
            };

            OreSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, MeatSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            OreSubBtn.Click += (o, e) => SwitchTab(32);

            OreLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = OreSubBtn,
                Text = "Ore",
                NotControl = true,
            };
            #endregion

            PetsItemButton = new MirButton
            {
                Index = 915,
                PressedIndex = 914,
                HoverIndex = 914,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, CraftingSystemButton.Location.Y + 20),
                Parent = this,
                Visible = true,
            };

            PetsItemButton.Click += (o, e) => SwitchTab(8);

            PetsItemLabel = new MirLabel
            {
                Size = new Size(99, 18),
                Location = new Point(2, 1),
                Parent = PetsItemButton,
                Text = "Pet Items",
                NotControl = true,
            };

            #region Pets & Mounts
            NoveltyPetsSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, PetsItemButton.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            NoveltyPetsSubBtn.Click += (o, e) => SwitchTab(33);

            NoveltyPetsLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = NoveltyPetsSubBtn,
                Text = "Novelty Pets",
                NotControl = true,
            };

            NoveltyEquipmentSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, NoveltyPetsSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            NoveltyEquipmentSubBtn.Click += (o, e) => SwitchTab(34);

            NoveltyEquipmentLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = NoveltyEquipmentSubBtn,
                Text = "Novelty Equip",
                NotControl = true,
            };

            MountsSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, NoveltyEquipmentSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            MountsSubBtn.Click += (o, e) => SwitchTab(35);

            MountsLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = MountsSubBtn,
                Text = "Mounts",
                NotControl = true,
            };

            ReinsSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, MountsSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            ReinsSubBtn.Click += (o, e) => SwitchTab(36);

            ReinsLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = ReinsSubBtn,
                Text = "Reins",
                NotControl = true,
            };

            BellsSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, ReinsSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            BellsSubBtn.Click += (o, e) => SwitchTab(37);

            BellsLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = BellsSubBtn,
                Text = "Bells",
                NotControl = true,
            };

            RibbonSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, BellsSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            RibbonSubBtn.Click += (o, e) => SwitchTab(38);

            RibbonLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = RibbonSubBtn,
                Text = "Ribbons",
                NotControl = true,
            };

            MaskSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, RibbonSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            MaskSubBtn.Click += (o, e) => SwitchTab(39);

            MaskLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = MaskSubBtn,
                Text = "Mask",
                NotControl = true,
            };
            #endregion

            ShieldItemButton = new MirButton
            {
                Index = 915,
                PressedIndex = 914,
                HoverIndex = 914,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, PetsItemButton.Location.Y + 20),
                Parent = this,
                Visible = true,
            };

            ShieldItemButton.Click += (o, e) => SwitchTab(40);

            ShieldItemLabel = new MirLabel
            {
                Size = new Size(99, 18),
                Location = new Point(2, 1),
                Parent = ShieldItemButton,
                Text = "Shield Items",
                NotControl = true,
            };

            #region Shields & Medals
            ShieldSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, ShieldItemButton.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            ShieldSubBtn.Click += (o, e) => SwitchTab(41);

            ShieldLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = ShieldSubBtn,
                Text = "Shields",
                NotControl = true,
            };

            MedalsSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, ShieldSubBtn.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            MedalsSubBtn.Click += (o, e) => SwitchTab(42);

            MedalsLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = MedalsSubBtn,
                Text = "Medals",
                NotControl = true,
            };
            #endregion

            RuneStoneItemButton = new MirButton
            {
                Index = 915,
                PressedIndex = 914,
                HoverIndex = 914,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, ShieldItemButton.Location.Y + 20),
                Parent = this,
                Visible = true,
            };

            RuneStoneItemButton.Click += (o, e) => SwitchTab(43);

            RuneStoneLabel = new MirLabel
            {
                Size = new Size(99, 18),
                Location = new Point(2, 1),
                Parent = RuneStoneItemButton,
                Text = "Rune Stones",
                NotControl = true,
            };

            #region Rune Stones
            RuneStoneSubBtn = new MirButton
            {
                Index = 917,
                PressedIndex = 916,
                HoverIndex = 916,
                Library = Libraries.CustomPrguse2,
                Sound = SoundList.ButtonA,
                Location = new Point(12, RuneStoneItemButton.Location.Y + 20),
                Parent = this,
                Visible = false,
            };

            RuneStoneSubBtn.Click += (o, e) => SwitchTab(44);

            RuneStoneLabel = new MirLabel
            {
                Size = new Size(100, 18),
                Location = new Point(18, 1),
                Parent = RuneStoneSubBtn,
                Text = "Runes",
                NotControl = true,
            };
            #endregion
            #endregion

            #region Market Buttons

            MailButton = new MirButton
            {
                Index = 437,
                HoverIndex = 438,
                PressedIndex = 439,
                Library = Libraries.CustomPrguse,
                Location = new Point(202, 437),
                Sound = SoundList.ButtonA,
                Parent = this,
                Hint = "Send a Mail"
            };
            MailButton.Click += (o, e) =>
            {
                if (Selected == null || CMain.Time < MarketTime) return;

                string message = $"I am interested in purchasing {Selected.Listing.Item.FriendlyName} for {Selected.Listing.Price}.";

                GameScene.Scene.MailComposeLetterDialog.ComposeMail(Selected.Listing.Seller, message);
            };

            RefreshButton = new MirButton
            {
                Index = 663,
                HoverIndex = 664,
                PressedIndex = 665,
                Library = Libraries.CustomPrguse,
                Location = new Point(174, 437),
                Sound = SoundList.ButtonA,
                Parent = this,
                Hint = "Refresh Page"
            };
            RefreshButton.Click += (o, e) =>
            {
                if (CMain.Time < SearchTime)
                {
                    GameScene.Scene.ChatDialog.ReceiveChat(string.Format("You can search again after {0} seconds.", Math.Ceiling((SearchTime - CMain.Time) / 1000D)), ChatType.System);
                    return;
                }
                SearchTime = CMain.Time + Globals.SearchDelay;
                SearchTextBox.Text = string.Empty;
                Network.Enqueue(new C.MarketRefresh());
            };


            PaymentType = new MirDropDownBox()
            {
                Parent = this,
                Location = new Point(200, 40),
                Size = new Size(109, 14),
                ForeColour = Color.White,
                Visible = true,
                Enabled = true,
            };
            PaymentType.ValueChanged += (o, e) =>
            {
                PaymentType.SelectedIndex = PaymentType._WantedIndex;


                if (PaymentType._WantedIndex == 0)
                    ggPay = false;
                else
                    ggPay = true;
            };

            BuyButton = new MirButton
            {
                Index = 312,
                PressedIndex = 314,
                HoverIndex = 313,
                Library = Libraries.CustomTitle,
                Location = new Point(380, 437),
                Sound = SoundList.ButtonA,
                Parent = this,
                Hint = "Buy Item"
            };
            BuyButton.Click += (o, e) =>
            {
                if (Selected == null || CMain.Time < MarketTime) return;

                if (UserMode)
                {
                    if (Selected.Listing.Seller == "For Sale")
                    {
                        MirMessageBox box = new MirMessageBox(string.Format("{0} has not sold, Are you sure you want to get it back?", Selected.Listing.Item.FriendlyName), MirMessageBoxButtons.YesNo);
                        box.YesButton.Click += (o1, e2) =>
                        {
                            MarketTime = CMain.Time + 3000;
                            Network.Enqueue(new C.MarketGetBack { AuctionID = Selected.Listing.AuctionID });
                        };
                        box.Show();
                    }
                    else
                    {
                        MarketTime = CMain.Time + 3000;
                        Network.Enqueue(new C.MarketGetBack { AuctionID = Selected.Listing.AuctionID });
                    }

                }
                else
                {
                    MirMessageBox box = new MirMessageBox(string.Format("Are you sure you want to buy {0} for {1}?", Selected.Listing.Item.FriendlyName, ggPay ? Selected.Listing.ggPrice : Selected.Listing.Price), MirMessageBoxButtons.YesNo);
                    box.YesButton.Click += (o1, e2) =>
                    {
                        MarketTime = CMain.Time + 3000;
                        Network.Enqueue(new C.MarketBuy { AuctionID = Selected.Listing.AuctionID, MailItems = false, ggPay = ggPay });
                    };
                    box.Show();
                }
            };
            #endregion

            #region Search

            SearchTextBox = new MirTextBox
            {
                Location = new Point(335, 35),
                Size = new Size(140, 1),
                MaxLength = 20,
                Parent = this,
                CanLoseFocus = true,
            };
            SearchTextBox.TextBox.KeyPress += SearchTextBox_KeyPress;
            SearchTextBox.TextBox.KeyUp += SearchTextBox_KeyUp;
            SearchTextBox.TextBox.KeyDown += SearchTextBox_KeyDown;



            FindButton = new MirButton
            {
                Index = 480,
                HoverIndex = 481,
                PressedIndex = 482,
                Library = Libraries.CustomTitle,
                Location = new Point(124, 437),
                Sound = SoundList.ButtonA,
                Parent = this,
                Hint = "Search Market"
            };
            FindButton.Click += (o, e) =>
            {
                if (String.IsNullOrEmpty(SearchTextBox.Text)) return;
                if (CMain.Time < SearchTime)
                {
                    GameScene.Scene.ChatDialog.ReceiveChat(string.Format("You can search again after {0} seconds.", Math.Ceiling((SearchTime - CMain.Time) / 1000D)), ChatType.System);
                    return;
                }

                SearchTime = CMain.Time + Globals.SearchDelay;
                Network.Enqueue(new C.MarketSearch
                {
                    Match = SearchTextBox.Text,
                });
            };

            #endregion

            #region Gold Label
            totalGold = new MirLabel
            {
                Size = new Size(100, 20),
                DrawFormat = TextFormatFlags.RightToLeft | TextFormatFlags.Right,

                Location = new Point(6, 441),
                Parent = this,
                NotControl = true,
                Font = new Font(Settings.FontName, 8F),
            };
            #endregion

            #region ItemCell


            ItemCell = new MirItemCell
            {
                BorderColour = Color.Lime,
                GridType = MirGridType.TMPanel,
                Library = Libraries.Items,
                Parent = this,
                Location = new Point(54, 104),
                Visible = false
            };
            ItemCell.Click += (o, e) => ItemCell_Click();

            PriceTextBox = new MirTextBox
            {
                Location = new Point(20, 165),
                Size = new Size(100, 1),
                MaxLength = 20,
                Parent = this,
                CanLoseFocus = true,
                Visible = false,
            };
            PriceTextBox.TextBox.TextChanged += TextBox_TextChanged;
            PriceTextBox.TextBox.KeyPress += MirInputBox_KeyPress;

            SellItemButton = new MirButton
            {
                Index = 700,
                PressedIndex = 702,
                HoverIndex = 701,
                Library = Libraries.CustomTitle,
                Sound = SoundList.ButtonA,
                Location = new Point(47, 188),
                Parent = this,
                Visible = false,
                Enabled = false
            };
            SellItemButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.ConsignItem { UniqueID = SellItemSlot.UniqueID, Price = ggSell ? 0 : Amount, GGPrice = ggSell ? Amount : 0 });
                SellItemSlot = null;
                PriceTextBox.Text = null;
                SellItemButton.Enabled = false;
                TMerchantDialog(1);
            };

            ggPayCheck = new MirCheckBox
            {
                Location = new Point(27, 198),
                Parent = this,
                Hint = "Sell for GameGold",
                Index = 2086,
                UnTickedIndex = 2086,
                TickedIndex = 2087,
                Library = Libraries.Prguse,
                Checked = false,
            };
            ggPayCheck.Click += (e, o) =>
            {
                ggSell = ggPayCheck.Checked;
            };


            #endregion

            for (int i = 0; i < Rows.Length; i++)
            {
                Rows[i] = new AuctionRow
                {
                    Location = new Point(141, 82 + i * 33),
                    Parent = this,
                };
                Rows[i].Click += (o, e) =>
                {
                    Selected = (AuctionRow)o;
                    UpdateInterface();
                };


            }



        }

        public void UpdateInterface()
        {

            PageLabel.Text = string.Format("{0}/{1}", Page + 1, PageCount);
            totalGold.Text = GameScene.Gold.ToString("###,###,##0");

            GameScene.Scene.TrustMerchantDialog.PaymentType.Items.Clear();

            GameScene.Scene.TrustMerchantDialog.ggPay = false;
            if (Selected != null && Selected.Listing.Price > 0)
            {
                GameScene.Scene.TrustMerchantDialog.PaymentType.Items.Add("Gold");
            }
            else
            {
                GameScene.Scene.TrustMerchantDialog.ggPay = true;
            }

            if (Selected != null && Selected.Listing.ggPrice > 0)
            {
                GameScene.Scene.TrustMerchantDialog.PaymentType.Items.Add("GameGold");
            }
            GameScene.Scene.TrustMerchantDialog.PaymentType.SelectedIndex = 0;



            for (int i = 0; i < 10; i++)
                if (i + Page * 10 >= Listings.Count)
                {
                    Rows[i].Clear();
                    if (Rows[i] == Selected) Selected = null;
                }
                else
                {
                    if (Rows[i] == Selected && Selected.Listing != Listings[i + Page * 10])
                    {
                        Selected.Border = false;
                        Selected = null;
                    }

                    Rows[i].Update(Listings[i + Page * 10]);
                }

            for (int i = 0; i < Rows.Length; i++)
                Rows[i].Border = Rows[i] == Selected;
        }
        private void SearchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CMain.Time < SearchTime)
            {
                GameScene.Scene.ChatDialog.ReceiveChat(string.Format("You can search again after {0} seconds.", Math.Ceiling((SearchTime - CMain.Time) / 1000D)), ChatType.System);
                return;
            }

            switch (e.KeyChar)
            {
                case (char)Keys.Enter:
                    e.Handled = true;
                    if (string.IsNullOrEmpty(SearchTextBox.Text)) return;
                    SearchTime = CMain.Time + Globals.SearchDelay;
                    Network.Enqueue(new C.MarketSearch
                    {
                        Match = SearchTextBox.Text,
                    });
                    Program.Form.ActiveControl = null;
                    break;
                case (char)Keys.Escape:
                    e.Handled = true;
                    break;
            }
        }
        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            CMain.Shift = e.Shift;
            CMain.Alt = e.Alt;
            CMain.Ctrl = e.Control;

            switch (e.KeyCode)
            {
                case Keys.F1:
                case Keys.F2:
                case Keys.F3:
                case Keys.F4:
                case Keys.F5:
                case Keys.F6:
                case Keys.F7:
                case Keys.F8:
                case Keys.F9:
                case Keys.F10:
                case Keys.F11:
                case Keys.F12:
                case Keys.Tab:
                case Keys.Escape:
                    CMain.CMain_KeyUp(sender, e);
                    break;

            }
        }
        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            CMain.Shift = e.Shift;
            CMain.Alt = e.Alt;
            CMain.Ctrl = e.Control;

            switch (e.KeyCode)
            {
                case Keys.F1:
                case Keys.F2:
                case Keys.F3:
                case Keys.F4:
                case Keys.F5:
                case Keys.F6:
                case Keys.F7:
                case Keys.F8:
                case Keys.F9:
                case Keys.F10:
                case Keys.F11:
                case Keys.F12:
                case Keys.Tab:
                case Keys.Escape:
                    CMain.CMain_KeyDown(sender, e);
                    break;

            }
        }
        public void TMerchantDialog(byte TMDid)
        {

            MarketButton.Index = 789;
            ConsignmentButton.Index = 791;


            Setdefault();

            switch (TMDid)
            {
                case 0:
                    Index = 786;
                    MarketButton.Index = 788;
                    BuyButton.Index = 312;
                    BuyButton.HoverIndex = 313;
                    BuyButton.PressedIndex = 314;
                    SwitchTab(0);
                    Setvalues(0);
                    Network.Enqueue(new C.MarketSearch
                    {
                        Match = "",
                        Type = ItemType.Nothing,
                        Usermode = false,
                    });
                    break;
                case 1:
                    Index = 787;
                    BuyButton.Index = 312;
                    BuyButton.HoverIndex = 313;
                    BuyButton.PressedIndex = 314;
                    ConsignmentButton.Index = 790;
                    Setvalues(1);
                    Network.Enqueue(new C.MarketSearch
                    {
                        Match = "",
                        Type = ItemType.Nothing,
                        Usermode = true,
                        MinShape = 0,
                        MaxShape = ushort.MaxValue,
                    });
                    break;

            }
        }

        private void Setvalues(byte i)
        {
            switch (i)
            {
                case 0:
                    FindButton.Visible = true;
                    SellItemButton.Visible = false;
                    ShowAllButton.Visible = true;
                    WeaponButton.Visible = true;
                    DraperyItemsButton.Visible = true;
                    AccessoriesItemsButton.Visible = true;
                    ConsumableItemsButton.Visible = true;
                    EnhEquipButton.Visible = true;
                    BooksButton.Visible = true;
                    MailButton.Visible = true;
                    CraftingSystemButton.Visible = true;
                    PetsItemButton.Visible = true;
                    ShieldItemButton.Visible = true;
                    RuneStoneItemButton.Visible = true;
                    PriceTextBox.Visible = false;
                    ItemCell.Visible = false;
                    SellItemButton.Visible = false;
                    SearchTextBox.Visible = true;
                    RefreshButton.Visible = true;
                    ggPayCheck.Visible = false;
                    break;
                case 1:
                    MailButton.Visible = false;
                    ShowAllButton.Visible = false;
                    WeaponButton.Visible = false;
                    DraperyItemsButton.Visible = false;
                    AccessoriesItemsButton.Visible = false;
                    ConsumableItemsButton.Visible = false;
                    EnhEquipButton.Visible = false;
                    BooksButton.Visible = false;
                    CraftingSystemButton.Visible = false;
                    PetsItemButton.Visible = false;
                    ShieldItemButton.Visible = false;
                    RuneStoneItemButton.Visible = false;
                    PriceTextBox.Visible = true;
                    PriceTextBox.Text = null;
                    ItemCell.Visible = true;
                    SellItemButton.Visible = true;
                    SellItemButton.Enabled = false;
                    FindButton.Visible = false;
                    SearchTextBox.Visible = false;
                    RefreshButton.Visible = false;
                    ggPayCheck.Visible = true;
                    break;
            }
        }
        public void SwitchTab(byte STabid)
        {
            switch (STabid)
            {
                case 0:
                    Setdefault();
                    ShowAllButton.Index = 914;
                    SetLocations(0);
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Nothing, Usermode = false, MinShape = 0, MaxShape = ushort.MaxValue });
                    break;
                case 1:
                    Setdefault();
                    WeaponButton.Index = 914;
                    SetLocations(1);
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Weapon, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 2:
                    Setdefault();
                    DraperyItemsButton.Index = 914;
                    ArmoursSubBtn.Visible = true;
                    HelmetsSubBtn.Visible = true;
                    BeltsSubBtn.Visible = true;
                    BootsSubBtn.Visible = true;
                    StonesSubBtn.Visible = true;
                    SetLocations(2); //2
                    break;
                case 3:
                    Setdefault();
                    AccessoriesItemsButton.Index = 914;
                    NecklaceSubBtn.Visible = true;
                    BraceletsSubBtn.Visible = true;
                    RingsSubBtn.Visible = true;
                    SetLocations(3);
                    break;
                case 4:
                    Setdefault();
                    ConsumableItemsButton.Index = 914;
                    RecoveryPotionSubBtn.Visible = true;
                    PowerUpSubBtn.Visible = true;
                    ScrollSubBtn.Visible = true;
                    ScriptSubBtn.Visible = true;
                    SetLocations(4);
                    break;
                case 5:
                    Setdefault();
                    EnhEquipButton.Index = 914;
                    GemSubBtn.Visible = true;
                    OrbSubBtn.Visible = true;
                    AwakeSubBtn.Visible = true;
                    SetLocations(5);
                    break;
                case 6:
                    Setdefault();
                    BooksButton.Index = 914;
                    WarriorSubBtn.Visible = true;
                    WizardSubBtn.Visible = true;
                    TaoistSubBtn.Visible = true;
                    AssassinSubBtn.Visible = true;
                    ArcherSubBtn.Visible = true;
                    SetLocations(6);
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Book, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 7:
                    Setdefault();
                    CraftingSystemButton.Index = 914;
                    MaterialsSubBtn.Visible = true;
                    FishSubBtn.Visible = true;
                    MeatSubBtn.Visible = true;
                    OreSubBtn.Visible = true;
                    SetLocations(7);
                    break;
                case 8:
                    Setdefault();
                    PetsItemButton.Index = 914;
                    NoveltyPetsSubBtn.Visible = true;
                    NoveltyEquipmentSubBtn.Visible = true;
                    MountsSubBtn.Visible = true;
                    ReinsSubBtn.Visible = true;
                    BellsSubBtn.Visible = true;
                    RibbonSubBtn.Visible = true;
                    MaskSubBtn.Visible = true;
                    SetLocations(8);
                    break;
                case 9:
                    ArmoursSubBtn.Index = 916;
                    HelmetsSubBtn.Index = 917;
                    BeltsSubBtn.Index = 917;
                    BootsSubBtn.Index = 917;
                    StonesSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Armour, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 10:
                    ArmoursSubBtn.Index = 917;
                    HelmetsSubBtn.Index = 916;
                    BeltsSubBtn.Index = 917;
                    BootsSubBtn.Index = 917;
                    StonesSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Helmet, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 11:
                    ArmoursSubBtn.Index = 917;
                    HelmetsSubBtn.Index = 917;
                    BeltsSubBtn.Index = 916;
                    BootsSubBtn.Index = 917;
                    StonesSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Belt, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 12:
                    ArmoursSubBtn.Index = 917;
                    HelmetsSubBtn.Index = 917;
                    BeltsSubBtn.Index = 917;
                    BootsSubBtn.Index = 916;
                    StonesSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Boots, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 13:
                    ArmoursSubBtn.Index = 917;
                    HelmetsSubBtn.Index = 917;
                    BeltsSubBtn.Index = 917;
                    BootsSubBtn.Index = 917;
                    StonesSubBtn.Index = 916;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Stone, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 14:
                    NecklaceSubBtn.Index = 916;
                    BraceletsSubBtn.Index = 917;
                    RingsSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Necklace, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 15:
                    NecklaceSubBtn.Index = 917;
                    BraceletsSubBtn.Index = 916;
                    RingsSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Bracelet, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 16:
                    NecklaceSubBtn.Index = 917;
                    BraceletsSubBtn.Index = 917;
                    RingsSubBtn.Index = 916;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Ring, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 17:
                    RecoveryPotionSubBtn.Index = 916;
                    PowerUpSubBtn.Index = 917;
                    ScrollSubBtn.Index = 917;
                    ScriptSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Potion, Usermode = false, MinShape = 2, MaxShape = 2 });
                    break;
                case 18:
                    RecoveryPotionSubBtn.Index = 917;
                    PowerUpSubBtn.Index = 916;
                    ScrollSubBtn.Index = 917;
                    ScriptSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Potion, Usermode = false, MinShape = 3, MaxShape = 4 });
                    break;
                case 19:
                    RecoveryPotionSubBtn.Index = 917;
                    PowerUpSubBtn.Index = 917;
                    ScrollSubBtn.Index = 916;
                    ScriptSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Scroll, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 20:
                    RecoveryPotionSubBtn.Index = 917;
                    PowerUpSubBtn.Index = 917;
                    ScrollSubBtn.Index = 917;
                    ScriptSubBtn.Index = 916;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Script, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 21:
                    GemSubBtn.Index = 916;
                    OrbSubBtn.Index = 917;
                    AwakeSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Gem, Usermode = false, MinShape = 3, MaxShape = 3 });
                    break;
                case 22:
                    GemSubBtn.Index = 917;
                    OrbSubBtn.Index = 916;
                    AwakeSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Gem, Usermode = false, MinShape = 4, MaxShape = 4 });
                    break;
                case 23:
                    GemSubBtn.Index = 917;
                    OrbSubBtn.Index = 917;
                    AwakeSubBtn.Index = 916;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Awakening, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 24:
                    WarriorSubBtn.Index = 916;
                    WizardSubBtn.Index = 917;
                    TaoistSubBtn.Index = 917;
                    AssassinSubBtn.Index = 917;
                    ArcherSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Book, Usermode = false, MinShape = 0, MaxShape = 30, Class = RequiredClass.Warrior });
                    break;
                case 25:
                    WarriorSubBtn.Index = 917;
                    WizardSubBtn.Index = 916;
                    TaoistSubBtn.Index = 917;
                    AssassinSubBtn.Index = 917;
                    ArcherSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Book, Usermode = false, MinShape = 31, MaxShape = 60, Class = RequiredClass.Wizard });
                    break;
                case 26:
                    WarriorSubBtn.Index = 917;
                    WizardSubBtn.Index = 917;
                    TaoistSubBtn.Index = 916;
                    AssassinSubBtn.Index = 917;
                    ArcherSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Book, Usermode = false, MinShape = 61, MaxShape = 90, Class = RequiredClass.Taoist });
                    break;
                case 27:
                    WarriorSubBtn.Index = 917;
                    WizardSubBtn.Index = 917;
                    TaoistSubBtn.Index = 917;
                    AssassinSubBtn.Index = 916;
                    ArcherSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Book, Usermode = false, MinShape = 91, MaxShape = 120, Class = RequiredClass.Assassin });
                    break;
                case 28:
                    WarriorSubBtn.Index = 917;
                    WizardSubBtn.Index = 917;
                    TaoistSubBtn.Index = 917;
                    AssassinSubBtn.Index = 917;
                    ArcherSubBtn.Index = 916;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Book, Usermode = false, MinShape = 121, MaxShape = 150, Class = RequiredClass.Archer });
                    break;
                case 29:
                    MaterialsSubBtn.Index = 916;
                    FishSubBtn.Index = 917;
                    MeatSubBtn.Index = 917;
                    OreSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.CraftingMaterial, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 30:
                    MaterialsSubBtn.Index = 917;
                    FishSubBtn.Index = 916;
                    MeatSubBtn.Index = 917;
                    OreSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Fish, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 31:
                    MaterialsSubBtn.Index = 917;
                    FishSubBtn.Index = 917;
                    MeatSubBtn.Index = 916;
                    OreSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Meat, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 32:
                    MaterialsSubBtn.Index = 917;
                    FishSubBtn.Index = 917;
                    MeatSubBtn.Index = 917;
                    OreSubBtn.Index = 916;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Ore, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 33:
                    NoveltyPetsSubBtn.Index = 916;
                    NoveltyEquipmentSubBtn.Index = 917;
                    MountsSubBtn.Index = 917;
                    ReinsSubBtn.Index = 917;
                    BellsSubBtn.Index = 917;
                    RibbonSubBtn.Index = 917;
                    MaskSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Pets, Usermode = false, MinShape = 0, MaxShape = 13 });
                    break;
                case 34:
                    NoveltyPetsSubBtn.Index = 917;
                    NoveltyEquipmentSubBtn.Index = 916;
                    MountsSubBtn.Index = 917;
                    ReinsSubBtn.Index = 917;
                    BellsSubBtn.Index = 917;
                    RibbonSubBtn.Index = 917;
                    MaskSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Pets, Usermode = false, MinShape = 20, MaxShape = 28 });
                    break;
                case 35:
                    NoveltyPetsSubBtn.Index = 917;
                    NoveltyEquipmentSubBtn.Index = 917;
                    MountsSubBtn.Index = 916;
                    ReinsSubBtn.Index = 917;
                    BellsSubBtn.Index = 917;
                    RibbonSubBtn.Index = 917;
                    MaskSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Mount, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 36:
                    NoveltyPetsSubBtn.Index = 917;
                    NoveltyEquipmentSubBtn.Index = 917;
                    MountsSubBtn.Index = 917;
                    ReinsSubBtn.Index = 916;
                    BellsSubBtn.Index = 917;
                    RibbonSubBtn.Index = 917;
                    MaskSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Reins, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 37:
                    NoveltyPetsSubBtn.Index = 917;
                    NoveltyEquipmentSubBtn.Index = 917;
                    MountsSubBtn.Index = 917;
                    ReinsSubBtn.Index = 917;
                    BellsSubBtn.Index = 916;
                    RibbonSubBtn.Index = 917;
                    MaskSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Bells, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 38:
                    NoveltyPetsSubBtn.Index = 917;
                    NoveltyEquipmentSubBtn.Index = 917;
                    MountsSubBtn.Index = 917;
                    ReinsSubBtn.Index = 917;
                    BellsSubBtn.Index = 917;
                    RibbonSubBtn.Index = 916;
                    MaskSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Ribbon, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 39:
                    NoveltyPetsSubBtn.Index = 917;
                    NoveltyEquipmentSubBtn.Index = 917;
                    MountsSubBtn.Index = 917;
                    ReinsSubBtn.Index = 917;
                    BellsSubBtn.Index = 917;
                    RibbonSubBtn.Index = 917;
                    MaskSubBtn.Index = 916;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Mask, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 40:
                    Setdefault();
                    ShieldItemButton.Index = 914;
                    ShieldSubBtn.Visible = true;
                    MedalsSubBtn.Visible = true;
                    SetLocations(9);
                    break;
                case 41:
                    ShieldSubBtn.Index = 916;
                    MedalsSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Shield, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 42:
                    ShieldSubBtn.Index = 917;
                    MedalsSubBtn.Index = 916;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.Medals, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
                case 43:
                    Setdefault();
                    RuneStoneItemButton.Index = 914;
                    RuneStoneSubBtn.Visible = true;
                    SetLocations(10);
                    break;
                case 44:
                    RuneStoneSubBtn.Index = 917;
                    Network.Enqueue(new C.MarketSearch { Match = SearchTextBox.Text, Type = ItemType.RuneStone, Usermode = false, MinShape = 0, MaxShape = 400 });
                    break;
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (uint.TryParse(PriceTextBox.Text, out Amount) && ((!ggSell && Amount >= MinAmount) || (ggSell && Amount > 0)))
            {
                PriceTextBox.BorderColour = Color.Lime;


                if (Amount > MaxAmount)
                {
                    Amount = MaxAmount;
                    PriceTextBox.Text = MaxAmount.ToString();
                    PriceTextBox.TextBox.SelectionStart = PriceTextBox.Text.Length;
                    SellItemButton.Enabled = true;
                }

                if (Amount == MaxAmount)
                    PriceTextBox.BorderColour = Color.Orange;
                SellItemButton.Enabled = true;
            }
            else
            {
                PriceTextBox.BorderColour = Color.Red;
                SellItemButton.Enabled = false;
            }
        }

        private void MirInputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ItemCell_Click()
        {
            if (tempCell != null)
            {
                tempCell.Locked = false;
                SellItemSlot = null;
                tempCell = null;
            }

            if (GameScene.SelectedCell == null || GameScene.SelectedCell.GridType != MirGridType.Inventory ||
                  GameScene.SelectedCell.Item != null && GameScene.SelectedCell.Item.Info.Durability < 0)
                return;


            SellItemSlot = GameScene.SelectedCell.Item;
            tempCell = GameScene.SelectedCell;
            tempCell.Locked = true;
            GameScene.SelectedCell = null;
            PriceTextBox.SetFocus();


        }
        public void Setdefault()
        {
            ShowAllButton.Index = 915;
            WeaponButton.Index = 915;

            DraperyItemsButton.Index = 915;
            ArmoursSubBtn.Index = 917;
            ArmoursSubBtn.Visible = false;
            HelmetsSubBtn.Index = 917;
            HelmetsSubBtn.Visible = false;
            BeltsSubBtn.Index = 917;
            BeltsSubBtn.Visible = false;
            BootsSubBtn.Index = 917;
            BootsSubBtn.Visible = false;
            StonesSubBtn.Index = 917;
            StonesSubBtn.Visible = false;

            AccessoriesItemsButton.Index = 915;
            NecklaceSubBtn.Index = 917;
            NecklaceSubBtn.Visible = false;
            BraceletsSubBtn.Index = 917;
            BraceletsSubBtn.Visible = false;
            RingsSubBtn.Index = 917;
            RingsSubBtn.Visible = false;

            ConsumableItemsButton.Index = 915;
            RecoveryPotionSubBtn.Index = 917;
            RecoveryPotionSubBtn.Visible = false;
            PowerUpSubBtn.Index = 917;
            PowerUpSubBtn.Visible = false;
            ScrollSubBtn.Index = 917;
            ScrollSubBtn.Visible = false;
            ScriptSubBtn.Index = 917;
            ScriptSubBtn.Visible = false;

            EnhEquipButton.Index = 915;
            GemSubBtn.Index = 917;
            GemSubBtn.Visible = false;
            OrbSubBtn.Index = 917;
            OrbSubBtn.Visible = false;
            AwakeSubBtn.Index = 917;
            AwakeSubBtn.Visible = false;

            BooksButton.Index = 915;
            WarriorSubBtn.Index = 917;
            WarriorSubBtn.Visible = false;
            WizardSubBtn.Index = 917;
            WizardSubBtn.Visible = false;
            TaoistSubBtn.Index = 917;
            TaoistSubBtn.Visible = false;
            AssassinSubBtn.Index = 917;
            AssassinSubBtn.Visible = false;
            ArcherSubBtn.Index = 917;
            ArcherSubBtn.Visible = false;

            CraftingSystemButton.Index = 915;
            MaterialsSubBtn.Index = 917;
            MaterialsSubBtn.Visible = false;
            FishSubBtn.Index = 917;
            FishSubBtn.Visible = false;
            MeatSubBtn.Index = 917;
            MeatSubBtn.Visible = false;
            OreSubBtn.Index = 917;
            OreSubBtn.Visible = false;

            PetsItemButton.Index = 915;
            NoveltyPetsSubBtn.Index = 917;
            NoveltyPetsSubBtn.Visible = false;
            NoveltyEquipmentSubBtn.Index = 917;
            NoveltyEquipmentSubBtn.Visible = false;
            MountsSubBtn.Index = 917;
            MountsSubBtn.Visible = false;
            ReinsSubBtn.Index = 917;
            ReinsSubBtn.Visible = false;
            BellsSubBtn.Index = 917;
            BellsSubBtn.Visible = false;
            RibbonSubBtn.Index = 917;
            RibbonSubBtn.Visible = false;
            MaskSubBtn.Index = 917;
            MaskSubBtn.Visible = false;

            ShieldItemButton.Index = 915;
            ShieldSubBtn.Index = 917;
            ShieldSubBtn.Visible = false;
            MedalsSubBtn.Index = 917;
            MedalsSubBtn.Visible = false;

            RuneStoneItemButton.Index = 915;
            RuneStoneSubBtn.Index = 917;
            RuneStoneSubBtn.Visible = false;
        }

        public void SetLocations(int i)
        {
            switch (i)
            {
                case 0:
                case 1:
                case 10://8
                    {
                        ShowAllButton.Location = new Point(12, 67);
                        WeaponButton.Location = new Point(12, ShowAllButton.Location.Y + 20);
                        DraperyItemsButton.Location = new Point(12, WeaponButton.Location.Y + 20);
                        AccessoriesItemsButton.Location = new Point(12, DraperyItemsButton.Location.Y + 20);
                        ConsumableItemsButton.Location = new Point(12, AccessoriesItemsButton.Location.Y + 20);
                        EnhEquipButton.Location = new Point(12, ConsumableItemsButton.Location.Y + 20);
                        BooksButton.Location = new Point(12, EnhEquipButton.Location.Y + 20);
                        CraftingSystemButton.Location = new Point(12, BooksButton.Location.Y + 20);
                        PetsItemButton.Location = new Point(12, CraftingSystemButton.Location.Y + 20);
                        ShieldItemButton.Location = new Point(12, PetsItemButton.Location.Y + 20);
                        RuneStoneItemButton.Location = new Point(12, ShieldItemButton.Location.Y + 20);
                        break;
                    }
                case 2:
                    {
                        ShowAllButton.Location = new Point(12, 67);
                        WeaponButton.Location = new Point(12, ShowAllButton.Location.Y + 20);
                        DraperyItemsButton.Location = new Point(12, WeaponButton.Location.Y + 20);
                        AccessoriesItemsButton.Location = new Point(12, StonesSubBtn.Location.Y + 20);
                        ConsumableItemsButton.Location = new Point(12, AccessoriesItemsButton.Location.Y + 20);
                        EnhEquipButton.Location = new Point(12, ConsumableItemsButton.Location.Y + 20);
                        BooksButton.Location = new Point(12, EnhEquipButton.Location.Y + 20);
                        CraftingSystemButton.Location = new Point(12, BooksButton.Location.Y + 20);
                        PetsItemButton.Location = new Point(12, CraftingSystemButton.Location.Y + 20);
                        ShieldItemButton.Location = new Point(12, PetsItemButton.Location.Y + 20);
                        RuneStoneItemButton.Location = new Point(12, ShieldItemButton.Location.Y + 20);
                        break;
                    }
                case 3:
                    {
                        ShowAllButton.Location = new Point(12, 67);
                        WeaponButton.Location = new Point(12, ShowAllButton.Location.Y + 20);
                        DraperyItemsButton.Location = new Point(12, WeaponButton.Location.Y + 20);
                        AccessoriesItemsButton.Location = new Point(12, DraperyItemsButton.Location.Y + 20);
                        ConsumableItemsButton.Location = new Point(12, RingsSubBtn.Location.Y + 20);
                        EnhEquipButton.Location = new Point(12, ConsumableItemsButton.Location.Y + 20);
                        BooksButton.Location = new Point(12, EnhEquipButton.Location.Y + 20);
                        CraftingSystemButton.Location = new Point(12, BooksButton.Location.Y + 20);
                        PetsItemButton.Location = new Point(12, CraftingSystemButton.Location.Y + 20);
                        ShieldItemButton.Location = new Point(12, PetsItemButton.Location.Y + 20);
                        RuneStoneItemButton.Location = new Point(12, ShieldItemButton.Location.Y + 20);
                        break;
                    }
                case 4:
                    {
                        ShowAllButton.Location = new Point(12, 67);
                        WeaponButton.Location = new Point(12, ShowAllButton.Location.Y + 20);
                        DraperyItemsButton.Location = new Point(12, WeaponButton.Location.Y + 20);
                        AccessoriesItemsButton.Location = new Point(12, DraperyItemsButton.Location.Y + 20);
                        ConsumableItemsButton.Location = new Point(12, AccessoriesItemsButton.Location.Y + 20);
                        EnhEquipButton.Location = new Point(12, ScriptSubBtn.Location.Y + 20);
                        BooksButton.Location = new Point(12, EnhEquipButton.Location.Y + 20);
                        CraftingSystemButton.Location = new Point(12, BooksButton.Location.Y + 20);
                        PetsItemButton.Location = new Point(12, CraftingSystemButton.Location.Y + 20);
                        ShieldItemButton.Location = new Point(12, PetsItemButton.Location.Y + 20);
                        RuneStoneItemButton.Location = new Point(12, ShieldItemButton.Location.Y + 20);
                        break;
                    }
                case 5:
                    {
                        ShowAllButton.Location = new Point(12, 67);
                        WeaponButton.Location = new Point(12, ShowAllButton.Location.Y + 20);
                        DraperyItemsButton.Location = new Point(12, WeaponButton.Location.Y + 20);
                        AccessoriesItemsButton.Location = new Point(12, DraperyItemsButton.Location.Y + 20);
                        ConsumableItemsButton.Location = new Point(12, AccessoriesItemsButton.Location.Y + 20);
                        EnhEquipButton.Location = new Point(12, ConsumableItemsButton.Location.Y + 20);
                        BooksButton.Location = new Point(12, AwakeSubBtn.Location.Y + 20);
                        CraftingSystemButton.Location = new Point(12, BooksButton.Location.Y + 20);
                        PetsItemButton.Location = new Point(12, CraftingSystemButton.Location.Y + 20);
                        ShieldItemButton.Location = new Point(12, PetsItemButton.Location.Y + 20);
                        RuneStoneItemButton.Location = new Point(12, ShieldItemButton.Location.Y + 20);
                        break;
                    }
                case 6:
                    {
                        ShowAllButton.Location = new Point(12, 67);
                        WeaponButton.Location = new Point(12, ShowAllButton.Location.Y + 20);
                        DraperyItemsButton.Location = new Point(12, WeaponButton.Location.Y + 20);
                        AccessoriesItemsButton.Location = new Point(12, DraperyItemsButton.Location.Y + 20);
                        ConsumableItemsButton.Location = new Point(12, AccessoriesItemsButton.Location.Y + 20);
                        EnhEquipButton.Location = new Point(12, ConsumableItemsButton.Location.Y + 20);
                        BooksButton.Location = new Point(12, EnhEquipButton.Location.Y + 20);
                        CraftingSystemButton.Location = new Point(12, ArcherSubBtn.Location.Y + 20);
                        PetsItemButton.Location = new Point(12, CraftingSystemButton.Location.Y + 20);
                        ShieldItemButton.Location = new Point(12, PetsItemButton.Location.Y + 20);
                        RuneStoneItemButton.Location = new Point(12, ShieldItemButton.Location.Y + 20);
                        break;
                    }

                case 7:
                    {
                        ShowAllButton.Location = new Point(12, 67);
                        WeaponButton.Location = new Point(12, ShowAllButton.Location.Y + 20);
                        DraperyItemsButton.Location = new Point(12, WeaponButton.Location.Y + 20);
                        AccessoriesItemsButton.Location = new Point(12, DraperyItemsButton.Location.Y + 20);
                        ConsumableItemsButton.Location = new Point(12, AccessoriesItemsButton.Location.Y + 20);
                        EnhEquipButton.Location = new Point(12, ConsumableItemsButton.Location.Y + 20);
                        BooksButton.Location = new Point(12, EnhEquipButton.Location.Y + 20);
                        CraftingSystemButton.Location = new Point(12, BooksButton.Location.Y + 20);
                        PetsItemButton.Location = new Point(12, OreSubBtn.Location.Y + 20);
                        ShieldItemButton.Location = new Point(12, PetsItemButton.Location.Y + 20);
                        RuneStoneItemButton.Location = new Point(12, ShieldItemButton.Location.Y + 20);
                        break;
                    }

                case 8:
                    {
                        ShowAllButton.Location = new Point(12, 67);
                        WeaponButton.Location = new Point(12, ShowAllButton.Location.Y + 20);
                        DraperyItemsButton.Location = new Point(12, WeaponButton.Location.Y + 20);
                        AccessoriesItemsButton.Location = new Point(12, DraperyItemsButton.Location.Y + 20);
                        ConsumableItemsButton.Location = new Point(12, AccessoriesItemsButton.Location.Y + 20);
                        EnhEquipButton.Location = new Point(12, ConsumableItemsButton.Location.Y + 20);
                        BooksButton.Location = new Point(12, EnhEquipButton.Location.Y + 20);
                        CraftingSystemButton.Location = new Point(12, BooksButton.Location.Y + 20);
                        PetsItemButton.Location = new Point(12, CraftingSystemButton.Location.Y + 20);
                        ShieldItemButton.Location = new Point(12, MaskSubBtn.Location.Y + 20);
                        RuneStoneItemButton.Location = new Point(12, ShieldItemButton.Location.Y + 20);
                        break;
                    }

                case 9:
                    {
                        ShowAllButton.Location = new Point(12, 67);
                        WeaponButton.Location = new Point(12, ShowAllButton.Location.Y + 20);
                        DraperyItemsButton.Location = new Point(12, WeaponButton.Location.Y + 20);
                        AccessoriesItemsButton.Location = new Point(12, DraperyItemsButton.Location.Y + 20);
                        ConsumableItemsButton.Location = new Point(12, AccessoriesItemsButton.Location.Y + 20);
                        EnhEquipButton.Location = new Point(12, ConsumableItemsButton.Location.Y + 20);
                        BooksButton.Location = new Point(12, EnhEquipButton.Location.Y + 20);
                        CraftingSystemButton.Location = new Point(12, BooksButton.Location.Y + 20);
                        PetsItemButton.Location = new Point(12, CraftingSystemButton.Location.Y + 20);
                        ShieldItemButton.Location = new Point(12, PetsItemButton.Location.Y + 20);
                        RuneStoneItemButton.Location = new Point(12, MedalsSubBtn.Location.Y + 20);
                        break;
                    }
            }
        }

        public void Hide()
        {
            if (!Visible) return;
            Visible = false;
            Listings.Clear();
            GameScene.Scene.InventoryDialog.Location = new Point(0, 0);
            if (tempCell != null)
            {
                tempCell.Locked = false;
                SellItemSlot = null;
                tempCell = null;
            }
        }
        public void Show()
        {
            if (Visible) return;
            Visible = true;
            GameScene.Scene.InventoryDialog.Show();
            GameScene.Scene.InventoryDialog.Location = new Point(490, 0);
            TMerchantDialog(0);
            SwitchTab(0);
            UpdateInterface();
        }

        #region AuctionRow
        public sealed class AuctionRow : MirControl
        {
            public ClientAuction Listing = null;

            public MirLabel NameLabel, PriceLabel, ggLabel, SellerLabel, ExpireLabel;
            public MirImageControl IconImage, SelectedImage;
            public bool Selected = false;

            Size IconArea = new Size(36, 32);

            public AuctionRow()
            {
                Size = new Size(337, 34);
                Sound = SoundList.ButtonA;
                BorderColour = Color.FromArgb(255, 200, 100, 0);
                Location = new Point(2, 0);

                BeforeDraw += AuctionRow_BeforeDraw;

                NameLabel = new MirLabel
                {
                    AutoSize = true,
                    Size = new Size(140, 20),
                    Location = new Point(38, 7),
                    DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                    NotControl = true,
                    Parent = this,
                };

                PriceLabel = new MirLabel
                {
                    AutoSize = true,
                    Size = new Size(178, 20),
                    Location = new Point(160, 0),
                    DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                    NotControl = true,
                    Parent = this,
                };

                ggLabel = new MirLabel
                {
                    AutoSize = true,
                    Size = new Size(178, 20),
                    Location = new Point(160, 14),
                    DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                    NotControl = true,
                    Parent = this,
                    ForeColour = Color.MediumPurple,
                };

                SellerLabel = new MirLabel
                {
                    AutoSize = true,
                    Size = new Size(148, 20),
                    Location = new Point(240, 0),
                    DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                    NotControl = true,
                    Parent = this,
                };

                IconImage = new MirImageControl
                {
                    Size = new Size(36, 32),
                    Location = new Point(1, 1),
                    Parent = this,
                };

                SelectedImage = new MirImageControl
                {
                    Size = new Size(339, 34),
                    Location = new Point(0, 0),
                    Border = true,
                    BorderColour = Color.FromArgb(255, 210, 100, 0),
                    NotControl = true,
                    Visible = false,
                    Parent = this,
                };

                ExpireLabel = new MirLabel
                {
                    AutoSize = true,
                    Location = new Point(240, 14),
                    Size = new Size(110, 22),
                    DrawFormat = TextFormatFlags.Left | TextFormatFlags.VerticalCenter,
                    Parent = this,
                    NotControl = true,
                };

                UpdateInterface();
            }

            #endregion

            void AuctionRow_BeforeDraw(object sender, EventArgs e)
            {
                UpdateInterface();
            }
            public void UpdateInterface()
            {
                if (Listing == null) return;

                IconImage.Visible = true;

                if (Listing.Item.Count > 0)
                {
                    //here
                    IconImage.Index = Listing.Item.Info.Image;
                    IconImage.Library = Libraries.Items;
                }
                else
                {
                    IconImage.Index = 540;
                    IconImage.Library = Libraries.Prguse;
                }

                IconImage.Location = new Point((IconArea.Width - IconImage.Size.Width) / 2, (IconArea.Height - IconImage.Size.Height) / 2);

                ExpireLabel.Visible = Listing != null;

                if (Listing == null) return;

                ExpireLabel.Text = string.Format("{0:dd/MM/yy HH:mm:ss}", Listing.ConsignmentDate.AddDays(Globals.ConsignmentLength));
            }
            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);

                SelectedImage = null;
                IconImage = null;

                Selected = false;
            }
            public void Clear()
            {
                Visible = false;
                NameLabel.Text = string.Empty;
                PriceLabel.Text = string.Empty;
                SellerLabel.Text = string.Empty;
            }
            public void Update(ClientAuction listing)
            {
                Listing = listing;
                NameLabel.Text = Listing.Item.FriendlyName;
                PriceLabel.Text = listing.Price > 0 ? "Gold: " + Listing.Price.ToString("###,###,##0") : "Gold: -";
                ggLabel.Text = listing.ggPrice > 0 ? "GG: " + Listing.ggPrice.ToString("###,###,##0") : "GG: -";

                NameLabel.ForeColour = GameScene.Scene.GradeNameColor(Listing.Item.Info.Grade);
                if (NameLabel.ForeColour == Color.Yellow)
                    NameLabel.ForeColour = Color.White;

                if (Listing.Price > 10000000) //10 m
                    PriceLabel.ForeColour = Color.Red;
                else if (listing.Price > 1000000) //1m
                    PriceLabel.ForeColour = Color.Orange;
                else if (listing.Price > 100000) //100k
                    PriceLabel.ForeColour = Color.Green;
                else if (listing.Price > 10000) //10k
                    PriceLabel.ForeColour = Color.DeepSkyBlue;
                else
                    PriceLabel.ForeColour = Color.White;


                SellerLabel.Text = Listing.Seller;

                if (UserMode)
                {
                    switch (Listing.Seller)
                    {
                        case "Sold":
                            SellerLabel.ForeColour = Color.Gold;
                            break;
                        case "Expired":
                            SellerLabel.ForeColour = Color.Red;
                            break;
                        default:
                            SellerLabel.ForeColour = Color.White;
                            break;
                    }
                }
                Visible = true;
            }
            protected override void OnMouseEnter()
            {
                if (Listing == null) return;

                base.OnMouseEnter();
                GameScene.Scene.CreateItemLabel(Listing.Item);
            }
            protected override void OnMouseLeave()
            {
                if (Listing == null) return;

                base.OnMouseLeave();
                GameScene.Scene.DisposeItemLabel();
                GameScene.HoverItem = null;
            }
        }
    }
}