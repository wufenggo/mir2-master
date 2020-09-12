using Client.MirScenes;

namespace Client.MirControls
{
    public class MirLabelItem : MirLabel
    {
        public UserItem Item;

        protected override void OnMouseEnter()
        {
            base.OnMouseEnter();
            GameScene.Scene.CreateItemLabel(Item);
        }
        protected override void OnMouseLeave()
        {
            base.OnMouseLeave();
            GameScene.Scene.DisposeItemLabel();
            GameScene.HoverItem = null;
        }

    }
}