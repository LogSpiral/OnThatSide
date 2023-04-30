using Microsoft.Xna.Framework;
using SubworldLibrary;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;
using Terraria.ID;
using Terraria.IO;
using ImmersiveProjector.Tiles;
using ImmersiveProjector.DataStructure;
using ImmersiveProjector;
using System;
using System.Reflection;
using Terraria.ModLoader.IO;
using Newtonsoft.Json;
using Terraria.GameContent;
using Terraria.Audio;
using Microsoft.Xna.Framework.Audio;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace OnThatSide
{
    public static class Utils
    {
        public static Vector2 Multiply(this Vector2 v1, Vector2 v2) => v1 * v2;
        public static Vector2 Multiply(this Vector2 v1, float x = 1, float y = 1) => v1 * new Vector2(x, y);
        public static float Multiply(this float v1, float v2) => v1 * v2;
    }
    public class OnThatSideWorld : Subworld
    {
        public override WorldGenConfiguration Config => base.Config;
        public override int Height => 1200;
        public override string Name => "OnThatSideWorld";//\"在那一边\"
        public override bool NormalUpdates => true;
        public override bool NoPlayerSaving => false;
        public override bool ShouldSave => false;
        public void BuildAnimWorld(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "我抄";
            var genRand = WorldGen.genRand;
            var height = Height / 4;

            for (int n = 0; n < Width; n++)
            {
                var track = Main.tile[n, height];
                //Minecart.PlaceTrack(track,1);
                track.TileType = TileID.MinecartTrack;
                track.HasTile = true;
                WorldGen.SquareTileFrame(n, height);
                //WorldUtils.TileFrame(n,height);

                for (int k = height + 1; k < Height / 2; k++)
                {
                    var tile = Main.tile[n, k];
                    tile.TileType = TileID.SnowBlock;
                    tile.HasTile = true;
                }
            }

            int projCenX = Width / 2;
            int projCenY = Height / 8;
            var gold = Main.tile[projCenX, projCenY - 1];
            var projector = Main.tile[projCenX, projCenY];
            gold.TileType = TileID.GoldBrick;
            gold.HasTile = true;
            gold = Main.tile[projCenX, projCenY + 1];
            gold.TileType = TileID.GoldBrick;
            gold.HasTile = true;

            Main.spawnTileY = height;
        }
        public override List<GenPass> Tasks => new List<GenPass>
        {
            new PassLegacy("OnThatSideWorld",BuildAnimWorld)
        };
        public override int Width => 2100;
        public override void OnEnter()
        {
            Main.time = Main.dayLength / 2;
            OnThatSidePlayer.enterSet = true;
            base.OnEnter();
        }
    }
    public class AnimationProjectile : ModProjectile
    {
        /// <summary>
        /// 持续时长,211秒//现修改为212秒
        /// </summary>
        public static int MaxTime => 12720;
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
        public void Part1(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;

            if (timer < 0) return;
            int partMaxtime = 2340;
            newTimer = timer - partMaxtime;
            if (timer > partMaxtime) return;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            Vector2 center = new Vector2(64758, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = new Vector2(0, -224);
            OnThatSidePlayer.zoomTarget = 1.5f;
            #endregion

            #region 绘制内容
            Victor.direction = -1;
            Victor.sleeping.isSleeping = true;
            //Victor.GetModPlayer<OnThatSidePlayer>().drawMouth = true;
            Victor.PlayerFrame();
            Victor.GetModPlayer<OnThatSidePlayer>().FastVisualSet(MathHelper.PiOver2, MathHelper.Pi / 3, new Vector2(-2, -14), MathHelper.Pi / 3, new Vector2(-6, -10), null, new Vector2(-12, 0));
            Victor.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, -MathHelper.Pi / 3);
            Victor.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 6);

            Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-33, 2), MathHelper.PiOver2, new Vector2(10, 21));
            //bool flag = true;
            //if (flag)
            //    Main.EntitySpriteDraw(TextureAssets.MagicPixel.Value, new Vector2(960 - 18, 560 - 85) - OnThatSidePlayer.screenOffsetor, new Rectangle(0, 0, 1, 1), Color.DarkRed, 0, new Vector2(.5f), new Vector2(2, 4), 0, 0);
            Anton.sitting.isSitting = true;
            Anton.PlayerFrame();
            Anton.direction = -1;
            float angleOffset = MathF.Cos(timer / 60f * MathHelper.Pi) * MathHelper.Pi / 6;
            float angleOffset2 = MathF.Cos(timer / 45f * MathHelper.Pi) * MathHelper.Pi / 12;
            float angleOffset3 = MathF.Cos(timer / 75f * MathHelper.Pi) * MathHelper.Pi / 24;

            Anton.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.ThreeQuarters, MathHelper.PiOver2 + angleOffset + angleOffset2 + angleOffset3);
            Anton.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.PiOver2 - MathHelper.Pi / 6 - angleOffset3);

            Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(33, 2), 0, new Vector2(10, 21));
            #endregion


        }
        public void Part2(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 1020;
            newTimer = timer - partMaxtime;
            if (timer > partMaxtime) return;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            Vector2 center = new Vector2(64758, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = new Vector2(0, -224);
            OnThatSidePlayer.zoomTarget = 1.5f;
            #endregion

            #region 绘制内容
            Victor.direction = -1;
            Victor.sleeping.isSleeping = true;
            //Victor.GetModPlayer<OnThatSidePlayer>().drawMouth = true;
            Victor.PlayerFrame();
            Victor.GetModPlayer<OnThatSidePlayer>().FastVisualSet(MathHelper.PiOver2, MathHelper.Pi / 3, new Vector2(-2, -14), MathHelper.Pi / 3, new Vector2(-6, -10), null, new Vector2(-12, 0));
            Victor.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, -MathHelper.Pi / 3);
            Victor.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 6);

            Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-33, 2), MathHelper.PiOver2, new Vector2(10, 21));
            //bool flag = true;
            //if (flag)
            //    Main.EntitySpriteDraw(TextureAssets.MagicPixel.Value, new Vector2(960 - 18, 560 - 85) - OnThatSidePlayer.screenOffsetor, new Rectangle(0, 0, 1, 1), Color.DarkRed, 0, new Vector2(.5f), new Vector2(2, 4), 0, 0);
            if (timer < 30)
            {
                Anton.compositeFrontArm.stretch = Player.CompositeArmStretchAmount.Full;
                Anton.compositeFrontArm.rotation = MathHelper.Lerp(Anton.compositeFrontArm.rotation, -MathHelper.Pi / 6, 0.2f);
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + Vector2.SmoothStep(new Vector2(33, 2), new Vector2(32, 0), timer / 30f), 0, new Vector2(10, 21));
                //Main.NewText((timer, "0"));
                //Anton.compositeBackArm.rotation = MathHelper.Lerp(Anton.compositeBackArm.rotation, fac * (1 - fac) * 4 * MathHelper.Pi / 6, 0.2f);
            }
            else if (timer < 60)
            {
                Anton.sitting.isSitting = false;
                Anton.direction = 1;
                Anton.PlayerFrame();
                Anton.compositeFrontArm.stretch = Player.CompositeArmStretchAmount.Full;
                Anton.compositeFrontArm.rotation = MathHelper.Lerp(Anton.compositeFrontArm.rotation, 0, 0.2f);
                Anton.compositeBackArm.rotation = MathHelper.Lerp(Anton.compositeBackArm.rotation, 0, 0.2f);
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(32, 0), 0, new Vector2(10, 21));
                //Main.NewText((timer, "1"));

            }
            else if (timer < 180)
            {
                float fac = (timer - 60) / 120f;
                float smoothFac = MathHelper.SmoothStep(0, 1, fac);
                //float legFac = MathHelper.SmoothStep(0, 1, (timer - 150) / 30f);
                //float Fac2 = MathHelper.SmoothStep(0, 1, (timer - 120) / 30f);
                //if (Fac2 == 1) Fac2 += legFac;
                //Fac2 *= .5f;
                float fullRot;
                float armFac;
                if (timer >= 120)
                {
                    armFac = 1 - MathHelper.SmoothStep(0, 1, (timer - 120) / 60f);
                    Anton.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, -MathHelper.Lerp(MathHelper.Pi / 6, -MathHelper.Pi / 6, armFac));
                    Anton.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, -MathHelper.Lerp(-MathHelper.Pi / 3, 0, armFac));
                    fullRot = -MathHelper.PiOver2;
                    Anton.GetModPlayer<OnThatSidePlayer>().FastVisualSet(fullRot,
                        MathHelper.Pi / 3 * 2, Vector2.Lerp(new Vector2(6, -16), new Vector2(6, -10), armFac),
                        -fullRot, Vector2.Lerp(new Vector2(-2, -14), new Vector2(-2, -12), armFac),
                        null, new Vector2(-12, 0));
                    //Main.NewText("2");
                    //Main.NewText((timer, "2.5"));

                }
                else
                {
                    armFac = 1 - MathHelper.SmoothStep(0, 1, (timer - 60) / 60f);
                    //Anton.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, MathHelper.Lerp(-MathHelper.Pi / 6, 0, armFac));
                    //Anton.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, 0);
                    fullRot = MathHelper.Lerp(-MathHelper.PiOver2, 0, armFac);

                    Anton.GetModPlayer<OnThatSidePlayer>().FastVisualSet(fullRot,
                        MathHelper.Lerp(MathHelper.Pi / 3 * 2, 0, armFac), Vector2.Lerp(new Vector2(6, -10), default, armFac),
                        -fullRot, Vector2.Lerp(new Vector2(-2, -12), default, armFac),
                        null, Vector2.Lerp(new Vector2(-12, 0), default, armFac * armFac * armFac * armFac));
                    //Main.NewText("2.5");
                    //Main.NewText((timer, "2"));

                }
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + Vector2.Lerp(new Vector2(33, 2), new Vector2(32, 0), smoothFac), fullRot, new Vector2(10, 21));

            }
            else if (timer < 210)
            {
                float fac = (timer - 180) / 30f;
                fac = 1 - fac;
                float smoothFac = MathHelper.SmoothStep(0, 1, fac);
                Anton.GetModPlayer<OnThatSidePlayer>().FastVisualSet(-MathHelper.PiOver2,
                    MathHelper.Lerp(MathHelper.Pi / 3, MathHelper.Pi / 3 * 2, smoothFac), Vector2.Lerp(new Vector2(-2, -14), new Vector2(6, -16), smoothFac),
                    MathHelper.Lerp(MathHelper.Pi / 3, MathHelper.Pi / 2, smoothFac), Vector2.Lerp(new Vector2(-6, -10), new Vector2(-2, -14), smoothFac),
                    null, new Vector2(-12, 0));
                Anton.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, -MathHelper.Pi / 6);
                Anton.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 3);
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(33, 2), -MathHelper.PiOver2, new Vector2(10, 21));
                //Main.NewText((timer, "3"));

            }
            else
            {
                Anton.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, -MathHelper.Pi / 6);
                Anton.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 3);
                Anton.direction = 1;
                Anton.GetModPlayer<OnThatSidePlayer>().FastVisualSet(-MathHelper.PiOver2, MathHelper.Pi / 3, new Vector2(-2, -14), MathHelper.Pi / 3, new Vector2(-6, -10), null, new Vector2(-12, 0));
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(33, 2), -MathHelper.PiOver2, new Vector2(10, 21));
                //Main.NewText((timer, "4"));

            }

            #endregion
        }
        public void Part3(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 1080;
            newTimer = timer - partMaxtime;
            if (timer > partMaxtime) return;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            Vector2 center = new Vector2(64758, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = new Vector2(0, MathHelper.SmoothStep(-224, -128, factor));
            OnThatSidePlayer.zoomTarget = MathHelper.SmoothStep(1.5f, 3, factor);
            #endregion

            #region 绘制内容
            Victor.direction = -1;
            Victor.sleeping.isSleeping = true;
            //Victor.GetModPlayer<OnThatSidePlayer>().drawMouth = true;
            Victor.PlayerFrame();
            Victor.GetModPlayer<OnThatSidePlayer>().FastVisualSet(MathHelper.PiOver2, MathHelper.Pi / 3, new Vector2(-2, -14), MathHelper.Pi / 3, new Vector2(-6, -10), null, new Vector2(-12, 0));
            Victor.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, -MathHelper.Pi / 3);
            Victor.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 6);

            Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-33, 2), MathHelper.PiOver2, new Vector2(10, 21));
            //bool flag = true;
            //if (flag)
            //    Main.EntitySpriteDraw(TextureAssets.MagicPixel.Value, new Vector2(960 - 18, 560 - 85) - OnThatSidePlayer.screenOffsetor, new Rectangle(0, 0, 1, 1), Color.DarkRed, 0, new Vector2(.5f), new Vector2(2, 4), 0, 0);
            Anton.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, -MathHelper.Pi / 6);
            Anton.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 3);
            Anton.direction = 1;
            Anton.GetModPlayer<OnThatSidePlayer>().FastVisualSet(-MathHelper.PiOver2, MathHelper.Pi / 3, new Vector2(-2, -14), MathHelper.Pi / 3, new Vector2(-6, -10), null, new Vector2(-12, 0));
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(33, 2), -MathHelper.PiOver2, new Vector2(10, 21));
            #endregion
        }
        public void Part4(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 1080;
            newTimer = timer - partMaxtime;
            if (timer > partMaxtime) return;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            Vector2 center = new Vector2(64758, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = new Vector2(factor < .33f ? MathHelper.SmoothStep(0, -32f, 3 * factor) : MathHelper.SmoothStep(-32f, 32f, (factor - 0.33f) / 0.67f), MathHelper.SmoothStep(-96, -256 / 6f, 6 * factor) - 32);
            OnThatSidePlayer.zoomTarget = MathHelper.SmoothStep(3, 6, 4 * factor);
            #endregion

            #region 绘制内容
            if (timer < 40)
            {
                //Main.NewText("0");
                Victor.direction = -1;
                Victor.sleeping.isSleeping = true;
                //Victor.GetModPlayer<OnThatSidePlayer>().drawMouth = true;
                Victor.PlayerFrame();
                Victor.GetModPlayer<OnThatSidePlayer>().FastVisualSet(MathHelper.PiOver2, MathHelper.Pi / 3, new Vector2(-2, -14), MathHelper.Pi / 3, new Vector2(-6, -10), null, new Vector2(-12, 0));
                Victor.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, -MathHelper.Pi / 3);
                Victor.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 6);
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-33, 2), MathHelper.PiOver2, new Vector2(10, 21));

            }
            else if (timer < 120)
            {
                float fac = (timer - 40) / 80f;

                //Victor.GetModPlayer<OnThatSidePlayer>().FastVisualDefault();
                //MathHelper.Pi / 3, new Vector2(-2, -14), MathHelper.Pi / 3, new Vector2(-6, -10), null, new Vector2(-12, 0)
                float smoothFac = MathHelper.SmoothStep(0, 1, fac);
                Victor.GetModPlayer<OnThatSidePlayer>().FastVisualSet(MathHelper.PiOver2,
                    MathHelper.Lerp(MathHelper.Pi / 3, MathHelper.Pi / 3 * 2, smoothFac), Vector2.Lerp(new Vector2(-2, -14), new Vector2(6, -16), smoothFac),
                    MathHelper.Lerp(MathHelper.Pi / 3, MathHelper.Pi / 2, smoothFac), Vector2.Lerp(new Vector2(-6, -10), new Vector2(-2, -14), smoothFac),
                    null, new Vector2(-12, 0));
                //Victor.compositeFrontArm.enabled = true;
                //Victor.compositeBackArm.enabled = true;
                //Victor.compositeFrontArm.stretch = Player.CompositeArmStretchAmount.Full;
                //Victor.compositeFrontArm.rotation = MathHelper.Lerp(Victor.compositeFrontArm.rotation, -fac * (1 - fac) * 4 * MathHelper.Pi / 3, 0.05f);
                //Victor.compositeBackArm.rotation = MathHelper.Lerp(Victor.compositeBackArm.rotation, -fac * (1 - fac) * 4 * MathHelper.Pi / 6, 0.05f);
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-33, 2), MathHelper.PiOver2, new Vector2(10, 21));
                //Main.NewText("1");
            }
            else if (timer < 180)
            {
                float fac = (timer - 120) / 60f;
                float smoothFac = MathHelper.SmoothStep(0, 1, fac);
                //float legFac = MathHelper.SmoothStep(0, 1, (timer - 150) / 30f);
                //float Fac2 = MathHelper.SmoothStep(0, 1, (timer - 120) / 30f);
                //if (Fac2 == 1) Fac2 += legFac;
                //Fac2 *= .5f;
                float fullRot;
                float armFac;
                if (timer < 150)
                {
                    armFac = MathHelper.SmoothStep(0, 1, (timer - 120) / 30f);
                    Victor.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, MathHelper.Lerp(MathHelper.Pi / 6, -MathHelper.Pi / 6, armFac));
                    Victor.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.Lerp(-MathHelper.Pi / 3, 0, armFac));
                    fullRot = MathHelper.PiOver2;
                    Victor.GetModPlayer<OnThatSidePlayer>().FastVisualSet(fullRot,
                        MathHelper.Pi / 3 * 2, Vector2.Lerp(new Vector2(6, -16), new Vector2(6, -10), armFac),
                        fullRot, Vector2.Lerp(new Vector2(-2, -14), new Vector2(-2, -12), armFac),
                        null, new Vector2(-12, 0));
                    //Main.NewText("2");

                }
                else
                {
                    armFac = MathHelper.SmoothStep(0, 1, (timer - 150) / 30f);
                    Victor.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, MathHelper.Lerp(-MathHelper.Pi / 6, 0, armFac));
                    Victor.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, 0);
                    fullRot = MathHelper.Lerp(MathHelper.PiOver2, 0, armFac);

                    Victor.GetModPlayer<OnThatSidePlayer>().FastVisualSet(fullRot,
                        MathHelper.Lerp(MathHelper.Pi / 3 * 2, 0, armFac), Vector2.Lerp(new Vector2(6, -10), default, armFac),
                        fullRot, Vector2.Lerp(new Vector2(-2, -12), default, armFac),
                        null, Vector2.Lerp(new Vector2(-12, 0), default, armFac * armFac * armFac * armFac));
                    //Main.NewText("2.5");

                }
                //Victor.compositeFrontArm.enabled = true;
                //Victor.compositeBackArm.enabled = true;
                //Victor.compositeFrontArm.stretch = Player.CompositeArmStretchAmount.Full;
                //Victor.compositeFrontArm.rotation = MathHelper.Lerp(Victor.compositeFrontArm.rotation, -fac * (1 - fac) * 4 * MathHelper.Pi / 3, 0.05f);
                //Victor.compositeBackArm.rotation = MathHelper.Lerp(Victor.compositeBackArm.rotation, -fac * (1 - fac) * 4 * MathHelper.Pi / 6, 0.05f);
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + Vector2.Lerp(new Vector2(-33, 2), new Vector2(-32, 0), smoothFac), fullRot, new Vector2(10, 21));

            }
            else if (timer < 300)
            {
                Victor.GetModPlayer<OnThatSidePlayer>().FastVisualDefault();
                Victor.SetCompositeArmFront(false, 0, 0);
                Victor.SetCompositeArmBack(false, 0, 0);
                //Victor.velocity = new Vector2(-32 / 120f, 0);
                Victor.velocity = new Vector2(MathHelper.SmoothStep(-32, -64, (timer - 181f) / 120f) - MathHelper.SmoothStep(-32, -64, (timer - 180f) / 120f), 0);

                Victor.PlayerFrame();
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(MathHelper.SmoothStep(-32, -64, (timer - 180f) / 120f), 0), 0, new Vector2(10, 21));
                //Main.NewText("3");

            }
            else
            {
                Victor.velocity = default;
                Victor.PlayerFrame();
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-64, 0), 0, new Vector2(10, 21));

            }

            if (timer < 360)
            {
                Anton.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, -MathHelper.Pi / 6);
                Anton.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 3);
                Anton.direction = 1;
                Anton.GetModPlayer<OnThatSidePlayer>().FastVisualSet(-MathHelper.PiOver2, MathHelper.Pi / 3, new Vector2(-2, -14), MathHelper.Pi / 3, new Vector2(-6, -10), null, new Vector2(-12, 0));
            }
            else if (timer < 480)
            {
                if (timer < 420)
                {
                    Anton.compositeFrontArm.rotation = MathHelper.Lerp(Anton.compositeFrontArm.rotation, MathHelper.Pi / 3, 0.025f);
                    Anton.compositeBackArm.rotation = MathHelper.Lerp(Anton.compositeBackArm.rotation, MathHelper.Pi / 2, 0.025f);
                }
                else
                {
                    Anton.compositeFrontArm.rotation = MathHelper.Lerp(Anton.compositeFrontArm.rotation, MathHelper.Pi / 6 * 5f, 0.025f);
                    Anton.compositeBackArm.rotation = MathHelper.Lerp(Anton.compositeBackArm.rotation, MathHelper.Pi / 6 * 5f, 0.025f);
                }
                Anton.direction = 1;
                var fac = MathHelper.SmoothStep(0, 1, (timer - 360) / 120f);
                Anton.GetModPlayer<OnThatSidePlayer>().FastVisualSet(-MathHelper.PiOver2, MathHelper.Pi / 3 * (1 - fac), new Vector2(-2, -14) * (1 - fac), MathHelper.Pi / 3 * (1 - fac), new Vector2(-6, -10) * (1 - fac), null, new Vector2(-12, 0) * (1 - fac));
            }
            else
            {
                Anton.GetModPlayer<OnThatSidePlayer>().FastVisualDefault();
                Anton.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 6 * 5f);
                Anton.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 6 * 5f);
            }
            Anton.direction = 1;
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(33, 2), -MathHelper.PiOver2, new Vector2(10, 21));

            #endregion
        }
        public void Part5(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 600;
            newTimer = timer - partMaxtime;
            if (timer > partMaxtime) return;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            Vector2 center = new Vector2(64758, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = Vector2.SmoothStep(new Vector2(32, -256 / 6f - 32), new Vector2(0, -224f), 4 * factor);
            OnThatSidePlayer.zoomTarget = MathHelper.SmoothStep(6, 2, 4 * factor);
            #endregion

            #region 绘制内容
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-64, 0), 0, new Vector2(10, 21));
            Anton.GetModPlayer<OnThatSidePlayer>().FastVisualDefault();
            Anton.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 6 * 5f);
            Anton.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 6 * 5f);
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(33, 2), -MathHelper.PiOver2, new Vector2(10, 21));

            #endregion
        }
        public void Part6(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 540;
            newTimer = timer - partMaxtime;
            if (timer > partMaxtime) return;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            Vector2 center = new Vector2(64758, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = Vector2.SmoothStep(new Vector2(0, -224f), new Vector2(-128f, -352f), 4 * factor);
            OnThatSidePlayer.zoomTarget = MathHelper.SmoothStep(2, 1.414f, 4 * factor);
            #endregion

            #region 绘制内容
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-64, 0), 0, new Vector2(10, 21));
            Anton.GetModPlayer<OnThatSidePlayer>().FastVisualDefault();
            Anton.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 6 * 5f);
            Anton.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 6 * 5f);
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(33, 2), -MathHelper.PiOver2, new Vector2(10, 21));

            #endregion
        }
        public void Part7(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 780;
            newTimer = timer - partMaxtime;
            if (timer > partMaxtime) return;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            Vector2 center = new Vector2(64758, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = Vector2.SmoothStep(new Vector2(-128f, -352f), new Vector2(640f, -464f), 4 * factor);
            OnThatSidePlayer.zoomTarget = MathHelper.SmoothStep(1.414f, 1f, 4 * factor);
            #endregion

            #region 绘制内容
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-64, 0), 0, new Vector2(10, 21));
            Anton.GetModPlayer<OnThatSidePlayer>().FastVisualDefault();
            Anton.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 6 * 5f);
            Anton.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 6 * 5f);
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(33, 2), -MathHelper.PiOver2, new Vector2(10, 21));

            #endregion
        }
        public void Part8(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 2820;
            newTimer = timer - partMaxtime;
            if (timer > partMaxtime) return;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            Vector2 center = new Vector2(64758, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = Vector2.SmoothStep(new Vector2(640f, -464f), new Vector2(0f, -96f), 20 * factor);
            OnThatSidePlayer.zoomTarget = MathHelper.SmoothStep(1f, 6f, 5 * factor);
            #endregion

            #region 绘制内容
            int offCen = 590;
            if (timer < offCen)
            {
                Victor.direction = -1;
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-64, 0), 0, new Vector2(10, 21));
            }
            else if (timer < offCen + 60)
            {
                float fac = (timer - offCen) / 60f;
                Victor.velocity = new Vector2(MathHelper.SmoothStep(-64, -16, fac) - MathHelper.SmoothStep(-64, -16, (timer - offCen - 1) / 60f), 0);
                Victor.PlayerFrame();
                Victor.direction = 1;
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(MathHelper.SmoothStep(-64, -16, fac), 0), 0, new Vector2(10, 21));

            }
            else
            {
                Victor.velocity = default;
                Victor.PlayerFrame();
                Victor.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, -MathHelper.Pi / 3 * 2 * MathHelper.SmoothStep(0, 1, (timer - offCen - 60) / 30f));
                if (timer - offCen == 90)
                    Victor.sitting.isSitting = true;
                Victor.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 6 * MathHelper.SmoothStep(0, 1, (timer - offCen - 90) / 30f));

                Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-16, 0), 0, new Vector2(10, 21));
            }
            Anton.GetModPlayer<OnThatSidePlayer>().FastVisualDefault();
            Anton.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 6 * 5f);
            Anton.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 6 * 5f);
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(33, 2), -MathHelper.PiOver2, new Vector2(10, 21));
            #endregion
        }
        public void Part9(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 2460;
            newTimer = timer - partMaxtime;
            if (timer > partMaxtime) return;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            Vector2 center = new Vector2(64758, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = Vector2.SmoothStep(new Vector2(0, -96f), new Vector2(0f, -128f), 20 * factor);
            OnThatSidePlayer.zoomTarget = MathHelper.SmoothStep(6f, 3f, 20 * factor);
            #endregion

            #region 绘制内容
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-16, 0), 0, new Vector2(10, 21));

            Anton.GetModPlayer<OnThatSidePlayer>().FastVisualDefault();
            Anton.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 6 * 5f);
            Anton.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 6 * 5f);
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(33, 2), -MathHelper.PiOver2, new Vector2(10, 21));

            #endregion
        }
        public override void SetDefaults()
        {
            ProjectileID.Sets.DrawScreenCheckFluff[Type] = 144000;
            Projectile.timeLeft = MaxTime;
            Anton = new Player();
            Anton.hair = 19;
            Anton.hairColor = new Color(86, 68, 17);
            Anton.eyeColor = new Color(105, 90, 75);
            Anton.pantsColor = new Color(59, 76, 60);
            Anton.shirtColor = new Color(93, 130, 95);
            Anton.shoeColor = new Color(160, 105, 60);
            Anton.skinColor = new Color(255, 125, 90);
            Anton.underShirtColor = new Color(71, 94, 71);
            Anton.PlayerFrame();
            Victor = new Player();
            Victor.CopyVisuals(Anton);
            Victor.hair = 1;
            Victor.PlayerFrame();
        }
        Player Anton;
        Player Victor;
        public override string Texture => "Terraria/Images/Item_1";
        public override bool PreDraw(ref Color lightColor)
        {
            //if (Main.mouseLeft) Main.NewText(Timer);
            var player = Main.player[Projectile.owner];
            var factor = Timer / (float)MaxTime;
            //Projectile.Center = player.Center = new Vector2(MathHelper.Lerp(6520, 124680, factor * factor), 6587);
            Vector2 center = new Vector2(64758, 2539 - 22);
            if (Timer == 1)
            {
                Main.hideUI = true;
                //Main.NewText("开始！");
            }
            int timer = Timer;
            int _timer = timer;

            Part1(timer, out timer);
            Part2(timer, out timer);
            Part3(timer, out timer);
            Part4(timer, out timer);
            Part5(timer, out timer);
            Part6(timer, out timer);
            Part7(timer, out timer);
            Part8(timer, out timer);
            Part9(timer, out _);
            //for (int n = -7; n < 8; n++)
            //{
            //    Vector2 wheelCen = new Vector2(960 + 17 * 16 * n, 520) - OnThatSidePlayer.screenOffsetor;
            //    Color wheelColor = new Color(51, 51, 51);
            //    float wheelRot = factor * factor * MathHelper.TwoPi * 480;
            //    Main.EntitySpriteDraw(TextureAssets.Item[3811].Value, wheelCen + new Vector2(-96, 0), null, wheelColor, wheelRot, new Vector2(11), 1.5f, 0, 0);
            //    Main.EntitySpriteDraw(TextureAssets.Item[3811].Value, wheelCen + new Vector2(-64, 0), null, wheelColor, wheelRot, new Vector2(11), 1.5f, 0, 0);
            //    Main.EntitySpriteDraw(TextureAssets.Item[3811].Value, wheelCen + new Vector2(64, 0), null, wheelColor, wheelRot, new Vector2(11), 1.5f, 0, 0);
            //    Main.EntitySpriteDraw(TextureAssets.Item[3811].Value, wheelCen + new Vector2(96, 0), null, wheelColor, wheelRot, new Vector2(11), 1.5f, 0, 0);

            //}
            //for (int n = -7; n < 8; n++)
            //{
            //    Vector2 wheelCen = new Vector2(4057.5f * 16 - 8 + 17 * 16 * n, 160 * 16 - 8 + 24);
            //    Color wheelColor = Lighting.GetColor((wheelCen + new Vector2(0, -32)).ToTileCoordinates(), new Color(153, 153, 153));
            //    wheelCen -= Main.screenPosition;
            //    float wheelRot = factor * factor * MathHelper.TwoPi * 480;
            //    Main.EntitySpriteDraw(TextureAssets.Item[3811].Value, wheelCen + new Vector2(-96, 0), null, wheelColor, wheelRot, new Vector2(11), 1.5f, 0, 0);
            //    Main.EntitySpriteDraw(TextureAssets.Item[3811].Value, wheelCen + new Vector2(-64, 0), null, wheelColor, wheelRot, new Vector2(11), 1.5f, 0, 0);
            //    Main.EntitySpriteDraw(TextureAssets.Item[3811].Value, wheelCen + new Vector2(64, 0), null, wheelColor, wheelRot, new Vector2(11), 1.5f, 0, 0);
            //    Main.EntitySpriteDraw(TextureAssets.Item[3811].Value, wheelCen + new Vector2(96, 0), null, wheelColor, wheelRot, new Vector2(11), 1.5f, 0, 0);

            //}

            for (int n = -7; n <= 8; n++)
            {
                //Vector2 wheelCen = new Vector2(960 + 17 * 16 * n, 520) - OnThatSidePlayer.screenOffsetor;
                Vector2 wheelCen = new Vector2(4092.5f * 16 - 8 + 22 * 16 * n, 162 * 16 - 8 + 24);
                Color wheelColor = Lighting.GetColor((wheelCen + new Vector2(0, -32)).ToTileCoordinates(), new Color(153, 153, 153));
                wheelCen -= Main.screenPosition;
                float wheelRot = factor * factor * MathHelper.TwoPi * 480;
                var wheel = TextureAssets.Item[3811].Value;
                Main.EntitySpriteDraw(wheel, wheelCen + new Vector2(-96, 0), null, wheelColor, wheelRot, new Vector2(11), 2f, 0, 0);
                Main.EntitySpriteDraw(wheel, wheelCen + new Vector2(-48, 0), null, wheelColor, wheelRot, new Vector2(11), 2f, 0, 0);
                Main.EntitySpriteDraw(wheel, wheelCen + new Vector2(48, 0), null, wheelColor, wheelRot, new Vector2(11), 2f, 0, 0);
                Main.EntitySpriteDraw(wheel, wheelCen + new Vector2(96, 0), null, wheelColor, wheelRot, new Vector2(11), 2f, 0, 0);
                Main.EntitySpriteDraw(TextureAssets.Item[281].Value, wheelCen + wheelRot.ToRotationVector2() * 8, null, wheelColor, -0.05f * MathF.Sin(wheelRot), new Vector2(19, 4), new Vector2(6f, 1.25f), 0, 0);
                //Main.EntitySpriteDraw(TextureAssets.Item[ItemID.StonePlatform].Value, wheelCen, null, wheelColor, MathHelper.PiOver4, new Vector2(11), 3f, 0, 0);


            }
            return false;
        }
        public override void AI()
        {
            //Main.moonType = 1;
            var player = Main.player[Projectile.owner];
            var factor = Timer / (float)MaxTime;
            Projectile.Center = player.Center = new Vector2(MathHelper.Lerp(6520, 124680, factor * factor), 6587);
            //if (new int[] { 0, 2320, 3340, 4420, 5500, 6100, 6640, 7420, 10240 }.Contains(Timer))
            //{
            //    Main.NewText((Main.time.ToString("0"), Main.dayTime));
            //    OnThatSide.showPosition = true;
            //}
            //Main.NewText(Main.ColorOfTheSkies);
            //Projectile.hide = false;
            //if (!(Main.mouseLeft ^ Main.mouseLeftRelease)) Main.NewText((Main.mouseLeft ? "张口" : "闭口") + Timer, Main.mouseLeft ? Color.Red : Color.Blue);
            int[] victorMouthKey = new int[]
            {
                189,221,
                289,361,
                416,462,
                530,595,

                753,779,
                797,816,
                838,844,
                854,862,
                875,899,
                914,936,
                956,975,
                993,1046,
                1073,1093,
                1110,1127,
                1147,1165,
                1188,1204,
                1225,1232,
                1242,1249,
                1262,1291,
                1301,1346,

                8131,8197,
                8272,8379,
                8410,8441,
                8500,8583,
                8653,8722,
                8756,8868,
                8906,8952,
                9011,9046,
                9099,9141,
                9196,9213,
                9219,9226,
                9236,9260,
                9292,9378,
                9395,9481,
                9532,9558,
                9565,9580,
                9586,9593,
                9599,9621,
                9642,9666,
                9678,9702,
                9735,9755,
                9752,9809,
                9950,9976
            };
            int[] sharedMouthKey = new int[]
            {

                1377,1393,
                1413,1430,
                1453,1458,
                1469,1476,
                1488,1515,
                1532,1552,
                1573,1596,
                1609,1655,
                1679,1700,
                1716,1729,
                1755,1761,
                1769,1777,
                1791,1797,
                1804,1837,
                1844,1868,
                1881,1965,
                1996,2015,
                2030,2088,
                2108,2162,
                2190,2213,
                2227,2259,
                2292,2302,
                2310,2318,
                2332,2385,
                2400,2408,
                2415,2435,
                2460,2477,
                2496,2575,
                2606,2626,
                2642,2695,
                2720,2765,
                2783,2790,
                2799,2827,
                2837,2891,
                2911,2932,
                2950,2971,
                2995,3004,
                3014,3022,
                3036,3058,
                3072,3091,
                3110,3180,
                3230,3255,
                3276,3291,
                3326,3335,
                3349,3354,
                3340,3366,
                3378,3403,
                3415,3437,
                3447,3591,
                3520,3542,
                3555,3576,
                3588,3612,
                3624,3649,
                3660,3667,
                3673,3679,
                3688,3712,
                3723,3764,
                3794,3816,
                3825,3830,
                3835,3854,
                3860,3883,
                3889,3897,
                3900,3921,
                3927,3952,
                3961,3983,
                3992,4037,
                4061,4084,
                4096,4102,
                4126,4135,
                4151,4165,
                4169,4183,
                4201,4214,
                4230,4281,
                4341,4362,
                4443,4486,
                4515,4536,
                4547,4583,
                4617,4640,
                4650,4696,
                4716,4723,
                4727,4733,
                4736,4764,
                4772,4794,
                4805,4855,
                4907,4926,
                4938,4990,
                5009,5057,
                5066,5075,
                5081,5107,
                5117,5163,
                5183,5221,
                5228,5262,
                5269,5277,
                5283,5292,
                5301,5328,
                5340,5358,
                5370,5446,
                5631,5652,
                5667,5693,
                5707,5714,
                5723,5729,
                5740,5773,
                5780,5802,
                5811,5836,
                5845,5898,
                5917,5940,
                5951,5972,
                5985,6017,
                6024,6052,
                6060,6068,
                6074,6081,
                6090,6122,
                6128,6182,
                6198,6225,
                6233,6254,
                6268,6276,
                6283,6290,
                6298,6328,
                6337,6358,
                6369,6396,
                6405,6454,
                6477,6497,
                6515,6526,
                6547,6556,
                6567,6574,
                6586,6608,
                6620,6634,
                6652,6741,
                6761,6781,
                6797,6853,
                6870,6923,
                6944,6968,
                6978,7034,
                7047,7072,
                7081,7136,
                7143,7152,
                7159,7188,
                7195,7217,
                7225,7317,
                7330,7356,
                7366,7426,
                //7436,7487,
                //7495,7503,
                //7511,7538,
                //7543,7581,
                //7652,7669,
                //7684,7690,
                10497,10508,
                10523,10530,
                10542,10548,
                10557,10580,
                10595,10614,
                10630,10648,
                10642,10711,
                10733,10753,
                10767,10784,
                10804,10817,
                10838,10858,
                10875,10884,
                10892,10899,
                10909,10938,
                10950,11000,
                11020,11041,
                11055,11083,
                11094,11105,
                11112,11120,
                11127,11158,
                11168,11194,
                11202,11225,
                11234,11283,
                11303,11324,
                11339,11365,
                11378,11387,
                11394,11403,
                11411,11439,
                11449,11468,
                11480,11562,
                11581,11615,
                11623,11679,
                11699,11747,
                11770,11802,
                11809,11864,
                11879,11903,
                11913,11966,
                11976,11986,
                11992,12022,
                12034,12058,
                12068,12141,
                12168,12196,
                12211,12262,
                12279,12233,
                12340,12349,
                12359,12386,
                12394,12444,
                12463,12484,
                12503,12526,
                12538,12548,
                12553,12561,
                12575,12598,
                12609,12623,
                12639,12684,
            };
            int[] antonMouthKey = new int[]
            {
                7654,7659,
                7686,7701,
                7779,7855,
                7898,7941,
                7967,8020,

                10018,10058,
                10126,10257
            };
            if (victorMouthKey.Contains(Timer))
            {
                Victor.GetModPlayer<OnThatSidePlayer>().drawMouth ^= true;
            }
            if (antonMouthKey.Contains(Timer))
            {
                Anton.GetModPlayer<OnThatSidePlayer>().drawMouth ^= true;
            }
            if (sharedMouthKey.Contains(Timer))
            {
                Victor.GetModPlayer<OnThatSidePlayer>().drawMouth ^= true;
                Anton.GetModPlayer<OnThatSidePlayer>().drawMouth ^= true;
            }
            base.AI();
        }
        public override void Kill(int timeLeft)
        {
            OnThatSidePlayer.zoomTarget = 1f;
            OnThatSidePlayer.screenOffsetor = default;
            base.Kill(timeLeft);
        }
    }
    public class DrawInterfaceProjectile : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_1";
        public Player drawPlayer;
        public override void SetDefaults()
        {
            drawPlayer = new Player();
            drawPlayer.hair = 19;
            drawPlayer.hairColor = new Color(86, 68, 17);
            drawPlayer.eyeColor = new Color(105, 90, 75);
            drawPlayer.pantsColor = new Color(59, 76, 60);
            drawPlayer.shirtColor = new Color(93, 130, 95);
            drawPlayer.shoeColor = new Color(160, 105, 60);
            drawPlayer.skinColor = new Color(255, 125, 90);
            drawPlayer.underShirtColor = new Color(71, 94, 71);
            drawPlayer.PlayerFrame();
            Projectile.hide = true;
            base.SetDefaults();
        }
        public override void AI()
        {
            Projectile.Center = Main.LocalPlayer.Center;
            Projectile.timeLeft = 2;
            base.AI();
        }
        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 center = new Vector2(64758, 2539 - 22);
            var mplr = drawPlayer.GetModPlayer<OnThatSidePlayer>();
            drawPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.Pi / 3);
            drawPlayer.direction = 1;
            mplr.drawMouth = true;
            mplr.FastVisualSet(-MathHelper.PiOver2, MathHelper.Pi / 3, new Vector2(4, -14), MathHelper.Pi / 3, new Vector2(0, -10), null, new Vector2(-6, 0));
            Main.PlayerRenderer.DrawPlayer(Main.Camera, drawPlayer, center + new Vector2(33, 2), -MathHelper.PiOver2, new Vector2(10, 21));

            //mplr.headRotationBuffer = -MathHelper.Pi / 3;
            //mplr.bodyRotationBuffer = -MathHelper.Pi / 3;
            //mplr.headOffsetBuffer = new Vector2(-4, -14).RotatedBy(-MathHelper.PiOver2);

            //mplr.bodyOffsetBuffer = new Vector2(0, -10).RotatedBy(-MathHelper.PiOver2);
            //mplr.legOffsetBuffer = new Vector2(6, 0).RotatedBy(-MathHelper.PiOver2);

            drawPlayer.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, -MathHelper.Pi / 3);
            drawPlayer.direction = -1;
            mplr.FastVisualSet(MathHelper.PiOver2, MathHelper.Pi / 3, new Vector2(4, -14), MathHelper.Pi / 3, new Vector2(0, -10), null, new Vector2(-6, 0));
            Main.PlayerRenderer.DrawPlayer(Main.Camera, drawPlayer, center + new Vector2(-33, 2), MathHelper.PiOver2, new Vector2(10, 21));

            return false;
        }
        public override void PostDraw(Color lightColor)
        {
            base.PostDraw(lightColor);
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            overPlayers.Add(index);
            base.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
        }
    }
    public class OnThatSideBiome : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<AnimationProjectile>()] > 0;
        }
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Music/YoungPeoplesMarch" + (OnThatSide.UseRedArmyChoir ? "_TheRedArmyChoir" : ""));
    }
    /// <summary>
    /// 
    /// 有讨厌的没法在头和头盔之间的罢格
    /// </summary>
    public class MouthLayer : PlayerDrawLayer
    {
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            var player = drawInfo.drawPlayer;
            if (player.GetModPlayer<OnThatSidePlayer>().drawMouth)
            {
                var drawinfo = drawInfo;
                Vector2 faceHeadOffsetFromHelmet = drawinfo.drawPlayer.GetFaceHeadOffsetFromHelmet();
                Vector2 position = new Vector2((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2)), (int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f)) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + faceHeadOffsetFromHelmet;
                //
                //Main.NewText(drawInfo.drawPlayer.bodyFrame);
                int offsetY = drawInfo.drawPlayer.bodyFrame.Y switch
                {
                    896 or 784 or 448 or 392 or 504 or 840 => -2,
                    _ => 0
                };
                Vector3 headColor = drawInfo.colorHead.ToVector3();//new Vector2(player.direction * 4, 5 + player.gfxOffY + offsetY)
                drawInfo.DrawDataCache.Add(new DrawData(TextureAssets.MagicPixel.Value, position, new Rectangle(0, 0, 1, 1), new Color(217 / 255f * headColor.X, 48 / 255f * headColor.Y, 54 / 255f * headColor.Z), drawInfo.drawPlayer.headRotation, new Vector2(.5f) + new Vector2(-1 * player.direction, -2.25f - offsetY * .5f), new Vector2(4, 2), 0, 0));


                //DrawData drawData = new DrawData(TextureAssets.MagicPixel.Value, position, new Rectangle(0,0,1,1), Color.Purple, drawinfo.drawPlayer.headRotation, new Vector2(.5f), 1f, drawinfo.playerEffect, 0);
                //drawInfo.DrawDataCache.Add(drawData);
                //DrawData drawData2 = new DrawData(TextureAssets.MagicPixel.Value, position + new Vector2(player.direction * 4, 5 + player.gfxOffY + offsetY), new Rectangle(0, 0, 1, 1), Color.Cyan, drawinfo.drawPlayer.headRotation, new Vector2(.5f), 1f, drawinfo.playerEffect, 0);
                //drawInfo.DrawDataCache.Add(drawData2);
            }
        }
        public override Position GetDefaultPosition()
        {
            //Main.NewText("哼哼啊啊啊啊");
            return new AfterParent(PlayerDrawLayers.Head);
        }
    }
    public class OnThatSidePlayer : ModPlayer
    {

        //public static void BuildSingleRoom(Point leftDown)
        //{

        //}
        //public static void BuildTrain(Point leftDown)
        //{
        //    for (int n = 0; n < 11; n++)
        //    {
        //        BuildSingleRoom(new Point(leftDown.X + n * 17, leftDown.Y));
        //    }
        //}
        public static bool enterSet;
        public static Vector2 screenOffsetor;
        public static float zoomTarget = 1f;
        public bool drawMouth;
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
        }
        public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
            base.HideDrawLayers(drawInfo);
        }
        /// <summary>
        /// 快速设置各部件相关参数
        /// </summary>
        /// <param name="fullRotation">你传入draw函数那边的旋转量，用于计算其它值</param>
        /// <param name="headRotation">头旋转</param>
        /// <param name="headOffset">头偏移</param>
        /// <param name="bodyRotation">身旋转</param>
        /// <param name="bodyOffset">身偏移</param>
        /// <param name="legRotation">腿旋转</param>
        /// <param name="legOffset">腿偏移</param>
        public void FastVisualSet(float fullRotation = 0, float? headRotation = null, Vector2? headOffset = null, float? bodyRotation = null, Vector2? bodyOffset = null, float? legRotation = null, Vector2? legOffset = null)
        {
            headRotationBuffer = headRotation?.Multiply(Player.direction);
            bodyRotationBuffer = bodyRotation?.Multiply(Player.direction);
            legRotationBuffer = legRotation?.Multiply(Player.direction);
            headOffsetBuffer = (headOffset?.Multiply(Player.direction))?.RotatedBy(-fullRotation);
            bodyOffsetBuffer = (bodyOffset?.Multiply(Player.direction))?.RotatedBy(-fullRotation);
            legOffsetBuffer = (legOffset?.Multiply(Player.direction))?.RotatedBy(-fullRotation);
        }
        public void FastVisualDefault() => FastVisualSet(default, 0, default(Vector2), 0, default(Vector2), 0, default(Vector2));
        public float? headRotationBuffer;
        public float? bodyRotationBuffer;
        public float? legRotationBuffer;
        public Vector2? headOffsetBuffer;
        public Vector2? bodyOffsetBuffer;
        public Vector2? legOffsetBuffer;
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (headRotationBuffer.HasValue)
            {
                drawInfo.drawPlayer.headRotation = headRotationBuffer.Value;
                headRotationBuffer = null;
            }
            if (bodyRotationBuffer.HasValue)
            {
                drawInfo.drawPlayer.bodyRotation = bodyRotationBuffer.Value;
                bodyRotationBuffer = null;
            }
            if (legRotationBuffer.HasValue)
            {
                drawInfo.drawPlayer.legRotation = legRotationBuffer.Value;
                legRotationBuffer = null;
            }
            if (headOffsetBuffer.HasValue)
            {
                drawInfo.drawPlayer.headPosition = headOffsetBuffer.Value;
                headOffsetBuffer = null;
            }
            if (bodyOffsetBuffer.HasValue)
            {
                drawInfo.drawPlayer.bodyPosition = bodyOffsetBuffer.Value;
                bodyOffsetBuffer = null;
            }
            if (legOffsetBuffer.HasValue)
            {
                drawInfo.drawPlayer.legPosition = legOffsetBuffer.Value;
                legOffsetBuffer = null;
            }
            drawInfo.hidesBottomSkin = true;
            //Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, 0, 1920, 1017), Color.White);//new Rectangle(0, 800, 100, 217)
            //drawInfo.rotation = Main.GlobalTimeWrappedHourly * Main.GlobalTimeWrappedHourly;
            //drawInfo.rotationOrigin = (drawInfo.Center - drawInfo.Position);
            //Main.NewText(drawInfo.rotationOrigin);
            //Main.NewText(drawInfo.headVect);


            //SpriteBatch spriteBatch = Main.spriteBatch;

            //spriteBatch.Draw(TextureAssets.MagicPixel.Value, Terraria.Utils.CenteredRectangle(new Vector2(Main.maxTilesX * 8 + 8, 216 * 16 + 8) - Main.screenPosition
            //    , new Vector2(Main.maxTilesX * 16, 8)), Color.Red * .5f);
            //for (int n = -7; n <= 8; n++)
            //{
            //    //Vector2 wheelCen = new Vector2(960 + 17 * 16 * n, 520) - OnThatSidePlayer.screenOffsetor;
            //    Vector2 wheelCen = new Vector2(4092.5f * 16 - 8 + 22 * 16 * n, 162 * 16 - 8 + 24) - Main.screenPosition;
            //    Color wheelColor = new Color(51, 51, 51);
            //    float wheelRot = Main.GlobalTimeWrappedHourly / 1000f * MathHelper.TwoPi * 480;
            //    var wheel = TextureAssets.Item[3811].Value;
            //    Main.EntitySpriteDraw(wheel, wheelCen + new Vector2(-96, 0), null, wheelColor, wheelRot, new Vector2(11), 2f, 0, 0);
            //    Main.EntitySpriteDraw(wheel, wheelCen + new Vector2(-48, 0), null, wheelColor, wheelRot, new Vector2(11), 2f, 0, 0);
            //    Main.EntitySpriteDraw(wheel, wheelCen + new Vector2(48, 0), null, wheelColor, wheelRot, new Vector2(11), 2f, 0, 0);
            //    Main.EntitySpriteDraw(wheel, wheelCen + new Vector2(96, 0), null, wheelColor, wheelRot, new Vector2(11), 2f, 0, 0);
            //    Main.EntitySpriteDraw(TextureAssets.Item[281].Value, wheelCen + wheelRot.ToRotationVector2() * 8, null, wheelColor, -0.05f * MathF.Sin(wheelRot), new Vector2(19, 4), new Vector2(6f, 1.25f), 0, 0);
            //    //Main.EntitySpriteDraw(TextureAssets.Item[ItemID.StonePlatform].Value, wheelCen, null, wheelColor, MathHelper.PiOver4, new Vector2(11), 3f, 0, 0);


            //}
            base.ModifyDrawInfo(ref drawInfo);
        }
        public override void ModifyDrawLayerOrdering(IDictionary<PlayerDrawLayer, PlayerDrawLayer.Position> positions)
        {
            //foreach (var layer in positions.Keys) 
            //{
            //    Main.NewText(layer.Name);
            //}
            //Main.NewText("更新");
        }
        //public ProjectorData cloneData;
        public override void ResetEffects()
        {
            drawMouth = false;
            //Main.NewText((Main.mouseLeft, Main.mouseLeftRelease));
            bool _flag = true;
            foreach (var proj in Main.projectile)
            {
                if (proj.active && proj.type == ModContent.ProjectileType<DrawInterfaceProjectile>()) { _flag = false; break; }
            }
            if (_flag)
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, default, ModContent.ProjectileType<DrawInterfaceProjectile>(), 0, 0, Player.whoAmI);
            //if (Main.mouseLeft)
            //{
            //    var point = Main.MouseWorld.ToTileCoordinates();
            //Main.NewText((Main.tile[point], point));
            //for (int n = 0; n < Main.maxTilesX; n++)
            //{
            //    var tile = Main.tile[new Point(n, 216)];
            //    tile.HasTile = false;
            //    tile.WallType = 0;
            //}
            //if (TileEntity.ByPosition.TryGetValue(new Point16(point), out TileEntity entity) && entity is ProjectorTileEntity projEntity)
            //{
            //    //if (cloneData != null)
            //    //{
            //    //    Main.NewText("已经克隆一份了");

            //    //}
            //    //else
            //    //{

            //    //}
            //    cloneData = projEntity.data;

            //}
            //}
            //if (Main.mouseRight) 
            //{
            //    var point = Main.MouseWorld.ToTileCoordinates();
            //    if (TileEntity.ByPosition.TryGetValue(new Point16(point), out TileEntity entity) && entity is ProjectorTileEntity projEntity)
            //    {
            //        if (cloneData != null)
            //        {
            //            projEntity.data.CopyDataFrom(cloneData);
            //        }
            //        else 
            //        {
            //            Main.NewText("请先用左键克隆一份数据");
            //        }
            //    }
            //}
            //if (Main.mouseLeft) 
            //{
            //    var type = typeof(Terraria.ModLoader.PlayerDrawLayerLoader);
            //    var method = type?.GetMethod("ResizeArrays", BindingFlags.NonPublic | BindingFlags.Static);
            //    method?.Invoke(null, null);
            //}
            if (OnThatSide.projectorList == null)
            {
                var types = typeof(MiniProjectorTile).Assembly.GetTypes();
                Type projectorSystemType = null;
                foreach (var type in types)
                {
                    if (type.Name == "ProjectorSystem")
                    {
                        projectorSystemType = type;
                        break;
                    }

                }
                if (projectorSystemType != null)
                {
                    PropertyInfo fieldInfo = null;
                    var fieldInfos = projectorSystemType.GetProperties();
                    foreach (var fieldinfo in fieldInfos)
                    {
                        if (fieldinfo.Name == "ListInWorld")
                        {
                            fieldInfo = fieldinfo;
                            break;
                        }
                    }
                    if (fieldInfo != null)
                    {
                        dynamic value = fieldInfo.GetValue(null);
                        OnThatSide.projectorList = value;
                        if (value != null) Main.NewText(value.Count);
                        else Main.NewText("valueNull了");
                    }
                    else
                    {
                        Main.NewText("fieldInfoNull了");
                    }
                    //Main.NewText($"当前投影数量为{((dynamic)projectorSystemType.GetField("ListInWorld", BindingFlags.Static | BindingFlags.Public).GetValue(null)).Count}");

                }
            }
            else
            {
                //if ((int)Main.time % 60 == 0)
                //    Main.NewText($"当前投影数量为{OnThatSide.projectorList.Count}");
            }
            if (enterSet)
            {
                int projCenX = 1050;
                int projCenY = 150;
                Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), Player.Center, default, ModContent.ProjectileType<AnimationProjectile>(), 0, 0, Player.whoAmI);
                WorldGen.PlaceTile(projCenX, projCenY, ModContent.TileType<MiniProjectorTile>());
                ModContent.GetInstance<ProjectorTileEntity>().Hook_AfterPlacement(projCenX, projCenY, ModContent.TileType<MiniProjectorTile>(), 0, 0, 0);
                if (TileEntity.ByPosition.TryGetValue(new Point16(projCenX, projCenY), out TileEntity entity) && entity is ProjectorTileEntity projEntity)
                {
                    //if (OnThatSide.projectorList == null) Main.NewText("列表为null");
                    //else
                    //{
                    //    Main.NewText($"添加之前数量{OnThatSide.projectorList.Count}");
                    //    //OnThatSide.projectorList.Add(projEntity.projectorInstance);
                    //    //Main.NewText($"添加之后数量{OnThatSide.projectorList.Count}");
                    //}
                    var data = projEntity.data;
                    data.turnedOn = 1;
                    data.SetDefault();
                    //var reader = new StreamReader(Mod.GetFileStream("ProjectionConfig.json"));
                    //JsonConvert.PopulateObject(reader.ReadToEnd(), data);
                    //File.WriteAllText(Path.Combine(ModLoader.ModPath, "OnThatSide", "ProjectionJson.json"), data.ToJson());
                    //var stream = Mod.GetFileStream("Train.qolstruct");
                    //var tag = TagIO.FromStream(stream);
                    //GenerateCore_NonConsume.GenerateFromTag(tag, new Point(projCenX, projCenY - 10));
                    //stream.Close();

                }
                else
                {
                    //ImmersiveProjector.Items.MiniProjector
                    //ProjectorTileEntity projEntity1 = new ProjectorTileEntity();
                    //var data = projEntity1.data;
                    //data.SetDefault();
                    //TileEntity.ByPosition.Add(new Point16(projCenX, projCenY), projEntity1);
                }
                enterSet = false;
            }
            //if (Player.controlUseItem)
            //{
            //Point16 mousePosTile = Main.MouseWorld.ToTileCoordinates16();
            //if (TileEntity.ByPosition.TryGetValue(mousePosTile, out TileEntity value) && value is ProjectorTileEntity projector)
            //{
            //    File.WriteAllText(Path.Combine(ModLoader.ModPath, "OnThatSide", "ProjectionJson.json"), projector.data.ToJson());
            //    Main.NewText("保存完毕!");
            //    Main.NewText(mousePosTile.ToVector2() * 16);
            //}
            //else 
            //{
            //    Main.NewText("请对准投影");
            //}
            //}
            //Main.NewText(Player.HeldItem.type);
            //Main.NewText((Player.pantsColor, Player.shirtColor, Player.shoeColor, Player.skinColor, Player.underShirtColor));
            var flag = false;
            foreach (var proj in Main.projectile)
            {
                if (proj.type == ModContent.ProjectileType<AnimationProjectile>())
                {
                    flag = true; break;
                }
            }
            if (flag)
            {
                foreach (var proj in Main.projectile) { if (proj.type == ProjectileID.FallingStarSpawner && proj.active) proj.Kill(); }
                foreach (var item in Main.item) { if (item.type == ItemID.FallenStar && item.active) item.TurnToAir(); }
            }
            //screenOffsetor = default;
            //zoomTarget = 1f;
            //drawMouth = true;
            base.ResetEffects();
        }
        public override void PostUpdate()
        {
            base.PostUpdate();
        }
        public override void ModifyScreenPosition()
        {
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<AnimationProjectile>()] > 0)
            {
                Main.screenPosition += screenOffsetor;
                Main.GameZoomTarget = zoomTarget;
            }

            base.ModifyScreenPosition();
        }
    }
    public class OnThatSide : Mod
    {
        public static dynamic projectorList;
        public static int StartOffset => 0;//0 2320 3340 4420 5500 6100 6640 7420 10240
        public override void Load()
        {
            On.Terraria.Audio.MP3AudioTrack.ReadAheadPutAChunkIntoTheBuffer += MP3AudioTrack_ReadAheadPutAChunkIntoTheBuffer;
            base.Load();
        }
        public static bool UseRedArmyChoir => false;
        public static bool showPosition;
        public static long? positionBuffer;
        private void MP3AudioTrack_ReadAheadPutAChunkIntoTheBuffer(On.Terraria.Audio.MP3AudioTrack.orig_ReadAheadPutAChunkIntoTheBuffer orig, MP3AudioTrack self)
        {
            orig.Invoke(self);
            var fieldInfo = typeof(MP3AudioTrack).GetField("_mp3Stream", BindingFlags.NonPublic | BindingFlags.Instance);
            var fieldInfo2 = typeof(MP3AudioTrack).GetField("_bufferToSubmit", BindingFlags.NonPublic | BindingFlags.Instance);
            var fieldInfo3 = typeof(MP3AudioTrack).GetField("_soundEffectInstance", BindingFlags.NonPublic | BindingFlags.Instance);

            var mp3str = (XPT.Core.Audio.MP3Sharp.MP3Stream)fieldInfo.GetValue(self);
            long position = mp3str.Position;
            if (positionBuffer != null)
            {
                mp3str.Position = positionBuffer.Value;
                positionBuffer = null;
            }
            if (showPosition)
            {
                Main.NewText($"Mp3Length更新前:{position}");
                showPosition = false;
            }

            {
                byte[] bufferToSubmit = (byte[])fieldInfo2.GetValue(self);
                int count = mp3str.Read(bufferToSubmit, 0, bufferToSubmit.Length);
                //Main.NewText("读取了" + count);
                if (count < 1)
                {
                    self.Stop(AudioStopOptions.Immediate);
                }
                else
                {
                    //byte[] newbuffer = new byte[bufferToSubmit.Length];
                    //int offsetor = (int)(ModTime / 10) % 32;
                    //for (int n = 0; n < bufferToSubmit.Length; n++)
                    //{
                    //    newbuffer[n] = bufferToSubmit[n];
                    //}
                    //musicBuffer = newbuffer;
                    ((DynamicSoundEffectInstance)fieldInfo3.GetValue(self)).SubmitBuffer(bufferToSubmit);
                }
            }
            //Main.NewText($"Mp3Length更新后:{mp3str.Position}");
            //Main.NewText($"Mp3Length差值:{mp3str.Position - position}");
        }
    }
    public class OnThatSideSystem : ModSystem
    {
        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            base.PostDrawInterface(spriteBatch);
        }
    }
    public class Disk : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("一张记录着过去的黑胶唱片");
            Tooltip.SetDefault("上面写着「歌唱动荡的青春 苏联 红旗歌舞团」\n左键点击观看代码动画 《在那一边》(片段)");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.value = -1;
            Item.useTime = Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Purple;
        }
        public override bool? UseItem(Player player)
        {
            if (player.itemAnimation == 1)
            {
                //SubworldSystem.Enter<OnThatSideWorld>();
                var animType = ModContent.ProjectileType<AnimationProjectile>();
                bool flag = false;
                Projectile anim = null;
                foreach (var proj in Main.projectile)
                {
                    if (proj.type == animType && proj.active)
                    {
                        proj.timeLeft = 12720 - OnThatSide.StartOffset;
                        anim = proj;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    player.Center = new Vector2(6520, 6587);
                    anim = Projectile.NewProjectileDirect(player.GetSource_ItemUse(Item), player.Center, default, animType, 0, 0, player.whoAmI);
                    anim.timeLeft = 12720 - OnThatSide.StartOffset;
                }
                OnThatSide.positionBuffer = OnThatSide.UseRedArmyChoir ?
                OnThatSide.StartOffset switch
                {
                    0 => 519,
                    2320 => 622860,
                    3340 => 894533,
                    4420 => 1182925,
                    5500 => 1469645,
                    6100 => 1630141,
                    6640 => 1774337,
                    7420 => 1982481,
                    10240 => 2734390,
                    _ => null
                } :
                OnThatSide.StartOffset switch
                {
                    0 => 1009,
                    2320 => 1551409,
                    3340 => 2230129,
                    4420 => 2950129,
                    5500 => 3671089,
                    6100 => 4070449,
                    6640 => 4428529,
                    7420 => 4950769,
                    10240 => 6835249,
                    _ => null
                };
                Main.dayTime = OnThatSide.StartOffset switch
                {
                    7420 or 10240 => false,
                    _ => true
                };
                Main.time = OnThatSide.StartOffset switch
                {
                    0 => 47250,
                    2320 => 49570,
                    3340 => 50590,
                    4420 => 51670,
                    5500 => 52750,
                    6100 => 53350,
                    6640 => 53890,
                    7420 => 669,
                    10240 => 3429,
                    _ => 47250
                };
                //TODO: 张口切换
                //if (anim.ModProjectile is AnimationProjectile animation) 
                //{
                //    animation.
                //}
                //Main.dayTime = true;
                //Main.time = Main.dayLength * 0.875f;
            }
            //Main.NewText(player.itemAnimation);
            return base.UseItem(player);
        }
    }
}