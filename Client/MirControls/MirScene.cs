﻿using System.Drawing;
using System.Windows.Forms;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirScenes;
using Microsoft.DirectX.Direct3D;
using S = ServerPackets;
using C = ClientPackets;

namespace Client.MirControls
{
    public abstract class MirScene : MirControl
    {
        public static MirScene ActiveScene = new LoginScene();

        private static MouseButtons _buttons;
        private static long _lastClickTime;
        private static MirControl _clickedControl;
        //private bool _redraw;

        protected MirScene()
        {
            DrawControlTexture = true;
            BackColour = Color.Magenta;
            Size = new Size(Settings.ScreenWidth, Settings.ScreenHeight);

        }

        public override sealed Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }


        public override void Draw()
        {
            if (IsDisposed || !Visible)
                return;

            OnBeforeShown();

            DrawControl();

            if (CMain.DebugBaseLabel != null && !CMain.DebugBaseLabel.IsDisposed)
                CMain.DebugBaseLabel.Draw();

            if (CMain.HintBaseLabel != null && !CMain.HintBaseLabel.IsDisposed)
                CMain.HintBaseLabel.Draw();

            OnShown();
        }

        protected override void CreateTexture()
        {
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
            DXManager.SetSurface(surface);


            DXManager.Device.Clear(ClearFlags.Target, BackColour, 0, 0);

            BeforeDrawControl();
            DrawChildControls();
            AfterDrawControl();

            DXManager.Sprite.Flush();


            DXManager.SetSurface(oldSurface);
            TextureValid = true;
            surface.Dispose();

        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            if (!Enabled)
                return;

            if (MouseControl != null && MouseControl != this)
                MouseControl.OnMouseDown(e);
            else
                base.OnMouseDown(e);
        }
        public override void OnMouseUp(MouseEventArgs e)
        {
            if (!Enabled)
                return;
            if (MouseControl != null && MouseControl != this)
                MouseControl.OnMouseUp(e);
            else
                base.OnMouseUp(e);
        }
        public override void OnMouseMove(MouseEventArgs e)
        {
            if (!Enabled)
                return;

            if (MouseControl != null && MouseControl != this && MouseControl.Moving)
                MouseControl.OnMouseMove(e);
            else
                base.OnMouseMove(e);
        }
        public override void OnMouseWheel(MouseEventArgs e)
        {
            if (!Enabled)
                return;

            if (MouseControl != null && MouseControl != this)
                MouseControl.OnMouseWheel(e);
            else
                base.OnMouseWheel(e);
        }

        public override void OnMouseClick(MouseEventArgs e)
        {
            if (!Enabled)
                return;
            if (_buttons == e.Button)
            {
                if (_lastClickTime + SystemInformation.DoubleClickTime >= CMain.Time)
                {
                    OnMouseDoubleClick(e);
                    return;
                }
            }
            else
                _lastClickTime = 0;

            if (ActiveControl != null && ActiveControl.IsMouseOver(CMain.MPoint) && ActiveControl != this)
                ActiveControl.OnMouseClick(e);
            else
                base.OnMouseClick(e);

            _clickedControl = ActiveControl;

            _lastClickTime = CMain.Time;
            _buttons = e.Button;
        }
        public override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (!Enabled)
                return;
            _lastClickTime = 0;
            _buttons = MouseButtons.None;

            if (ActiveControl != null && ActiveControl.IsMouseOver(CMain.MPoint) && ActiveControl != this)
            {
                if (ActiveControl == _clickedControl)
                    ActiveControl.OnMouseDoubleClick(e);
                else
                    ActiveControl.OnMouseClick(e);
            }
            else
            {
                if (ActiveControl == _clickedControl)
                    base.OnMouseDoubleClick(e);
                else
                    base.OnMouseClick(e);
            }
        }

        public override void Redraw()
        {
            TextureValid = false;
        }
        
        public virtual void ProcessPacket(Packet p)
        {
            switch (p.Index)
            {
                case (short)ServerPacketIds.Disconnect: // Disconnected
                    Disconnect((S.Disconnect) p);
                    Network.Disconnect();
                    break;
                case (short)ServerPacketIds.NewItemInfo:
                    NewItemInfo((S.NewItemInfo) p);
                    break;
                case (short)ServerPacketIds.NewQuestInfo:
                    NewQuestInfo((S.NewQuestInfo)p);
                    break;
                case (short)ServerPacketIds.RefreshQuestInfo:
                    RefreshQuestInfo((S.RefreshQuestInfo)p);
                    break;
                case (short)ServerPacketIds.UpdateItemInfo:
                    UpdateItemInfo((S.UpdateItemInfo)p);
                    break;
                case (short)ServerPacketIds.RefreshStats:
                    RefreshStats((S.RefreshStats)p);
                    break;
            }
        }

        public bool ItemExists(ItemInfo info)
        {
            ItemInfo tmp = GameScene.ItemInfoList.Find(x => x.Index == info.Index);
            if (tmp == null)
                return false;
            else
                return true;
        }

        private void NewItemInfo(S.NewItemInfo info)
        {
            info.Info.UpdateToName = info.updateto;   
            if (ItemExists(info.Info))
            {
                ItemInfo tmp = GameScene.ItemInfoList.Find(x => x.Index == info.Info.Index);
                if (tmp != null)
                    tmp.UpdateItem(info.Info);
            }
            else
                GameScene.ItemInfoList.Add(info.Info);
        }

        private void RefreshStats(S.RefreshStats info)
        {
            GameScene.User.RefreshStats();
        }

        private void UpdateItemInfo(S.UpdateItemInfo info)
        {
            info.Info.UpdateToName = info.updateto;
            ItemInfo temp = GameScene.ItemInfoList.Find(x => x.Index == info.Info.Index);
            if (temp != null)
            {
                temp.UpdateItem(info.Info);
            }
            else
                GameScene.ItemInfoList.Add(info.Info);
        }

        private void NewQuestInfo(S.NewQuestInfo info)
        {
            GameScene.QuestInfoList.Add(info.Info);
        }

        private void RefreshQuestInfo(S.RefreshQuestInfo info)
        {
         GameScene.QuestInfoList.Clear();
        }

        private static void Disconnect(S.Disconnect p)
        {
            switch (p.Reason)
            {
                case 0:
                    MirMessageBox.Show("Disconnected: Server is shutting down.", true);
                    break;
                case 1:
                    MirMessageBox.Show("Disconnected: Another user logged onto your account.", true);
                    break;
                case 2:
                    MirMessageBox.Show("Disconnected: Packet Error.", true);
                    break;
                case 3:
                    MirMessageBox.Show("Disconnected: Server Crashed.", true);
                    break;
                case 4:
                    MirMessageBox.Show("Disconnected: Kicked by Admin.", true);
                    break;
                case 5:
                    MirMessageBox.Show("Disconnected: Maximum connections reached.", true);
                    break;
            }

            GameScene.LogTime = 0;
        }

        public abstract void Process();

        #region Disposable

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);

            if (!disposing) return;

            if (ActiveScene == this) ActiveScene = null;

            _buttons = 0;
            _lastClickTime = 0;
            _clickedControl = null;
        }

        #endregion
    }
}