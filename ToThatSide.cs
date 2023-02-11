using Microsoft.Xna.Framework;
using SubworldLibrary;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace ToThatSide
{
    public class ToThatSideWorld : Subworld
    {
        public override WorldGenConfiguration Config => base.Config;
        public override int Height => 1200;
        public override string Name => "\"在那一边\"";
        public override bool NormalUpdates => true;
        public override bool NoPlayerSaving => true;
        public override bool ShouldSave => false;
        public override List<GenPass> Tasks => throw new System.NotImplementedException();
        public override int Width => 2100;
        public override void OnEnter()
        {
            Main.time = Main.dayLength / 2;
            base.OnEnter();
        }
    }
    public class AnimationProjectile : ModProjectile 
    {
        /// <summary>
        /// 持续时长,211秒
        /// </summary>
        public static int MaxTime => 12660;
        /// <summary>
        /// 计时器
        /// </summary>
        public int Timer 
        {
            get => MaxTime - Projectile.timeLeft;
            set => Projectile.timeLeft = MaxTime - value;
        }
        //这里是时间表————
        //第一部分 0:00-0:39
        //维克多躺在床上，安东坐在床上整针线活(?

        //第二部分 0:39-0:56
        //安东整完针线活了，稍作调整侧身躺在床上

        //第三部分 0:56-1:14
        //画面放大

        //第四部分 1:14-1:32
        //维克多起身环顾

        //第五部分 1:32-1:42
        //近处雪景

        //第六部分 1:42-1:51
        //远处雪景

        //第七部分 1:51-2:04
        //火车前进！

        //第八部分 2:04-2:51
        //画面切回，二人展开对话

        //第九部分 2:51-3:31
        //继续唱歌至结束
        public void Part1(int timer,out int newTimer) 
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 2340;
            newTimer = timer - partMaxtime;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            #endregion

            #region 绘制内容

            #endregion
        }
        public void Part2(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 1020;
            newTimer = timer - partMaxtime;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            #endregion

            #region 绘制内容

            #endregion
        }
        public void Part3(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 1080;
            newTimer = timer - partMaxtime;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            #endregion

            #region 绘制内容

            #endregion
        }
        public void Part4(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 1080;
            newTimer = timer - partMaxtime;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            #endregion

            #region 绘制内容

            #endregion
        }
        public void Part5(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 600;
            newTimer = timer - partMaxtime;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            #endregion

            #region 绘制内容

            #endregion
        }
        public void Part6(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 540;
            newTimer = timer - partMaxtime;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            #endregion

            #region 绘制内容

            #endregion
        }
        public void Part7(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 780;
            newTimer = timer - partMaxtime;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            #endregion

            #region 绘制内容

            #endregion
        }
        public void Part8(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 2820;
            newTimer = timer - partMaxtime;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            #endregion

            #region 绘制内容

            #endregion
        }
        public void Part9(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 2400;
            newTimer = timer - partMaxtime;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            #endregion

            #region 绘制内容

            #endregion
        }
        public override void SetDefaults()
        {
            Projectile.timeLeft = MaxTime;
        }
        public override string Texture => "Terraria/Images/Item_1";
        public override bool PreDraw(ref Color lightColor)
        {
            int timer = Timer;
            Part1(timer, out timer);
            Part2(timer, out timer);
            Part3(timer, out timer);
            Part4(timer, out timer);
            Part5(timer, out timer);
            Part6(timer, out timer);
            Part7(timer, out timer);
            Part8(timer, out timer);
            Part9(timer, out _);
            return false;
        }
    }
    public class ToThatSidePlayer : ModPlayer 
    {
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
        }
        public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
            base.HideDrawLayers(drawInfo);
        }
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            base.ModifyDrawInfo(ref drawInfo);
        }
        public override void ModifyDrawLayerOrdering(IDictionary<PlayerDrawLayer, PlayerDrawLayer.Position> positions)
        {
            base.ModifyDrawLayerOrdering(positions);
        }
        public override void ResetEffects()
        {
            base.ResetEffects();
        }
        public override void PostUpdate()
        {
            base.PostUpdate();
        }
    }
    public class ToThatSide : Mod
    {
    }
}