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

namespace OnThatSide
{
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
        public void Part1(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 2340;
            newTimer = timer - partMaxtime;
            if (timer > partMaxtime) return;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            Vector2 center = new Vector2(64368, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = new Vector2(0, -192);
            OnThatSidePlayer.zoomTarget = 1.5f;
            #endregion

            #region 绘制内容
            Victor.direction = -1;
            Victor.sleeping.isSleeping = true;
            //Victor.GetModPlayer<OnThatSidePlayer>().drawMouth = true;
            Victor.PlayerFrame();
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-32, 0), MathHelper.PiOver2, new Vector2(10, 14));
            //bool flag = true;
            //if (flag)
            //    Main.EntitySpriteDraw(TextureAssets.MagicPixel.Value, new Vector2(960 - 18, 560 - 85) - OnThatSidePlayer.screenOffsetor, new Rectangle(0, 0, 1, 1), Color.DarkRed, 0, new Vector2(.5f), new Vector2(2, 4), 0, 0);
            Anton.sitting.isSitting = true;
            Anton.PlayerFrame();
            float angleOffset = MathF.Cos(timer / 60f * MathHelper.Pi) * MathHelper.Pi / 6;
            float angleOffset2 = MathF.Cos(timer / 45f * MathHelper.Pi) * MathHelper.Pi / 12;

            Anton.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Quarter, -MathHelper.PiOver2 + angleOffset + angleOffset2);
            Anton.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, -MathHelper.PiOver2 + MathHelper.Pi / 6);

            Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(32, 0), 0, new Vector2(10, 14));
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
            Vector2 center = new Vector2(64368, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = new Vector2(0, -192);
            OnThatSidePlayer.zoomTarget = 1.5f;
            #endregion

            #region 绘制内容
            Victor.direction = -1;
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-32, 0), MathHelper.PiOver2, new Vector2(10, 14));
            //bool flag = true;
            //if (flag)
            //    Main.EntitySpriteDraw(TextureAssets.MagicPixel.Value, new Vector2(960 - 18, 560 - 85) - OnThatSidePlayer.screenOffsetor, new Rectangle(0, 0, 1, 1), Color.DarkRed, 0, new Vector2(.5f), new Vector2(2, 4), 0, 0);

            Anton.sitting.isSitting = false;
            Anton.PlayerFrame();
            if (timer < 120)
            {
                float fac = timer / 120f;
                Anton.compositeFrontArm.stretch = Player.CompositeArmStretchAmount.Full;
                Anton.compositeFrontArm.rotation = MathHelper.Lerp(Anton.compositeFrontArm.rotation, fac * (1 - fac) * 4 * MathHelper.Pi / 3, 0.05f);
                Anton.compositeBackArm.rotation = MathHelper.Lerp(Anton.compositeBackArm.rotation, fac * (1 - fac) * 4 * MathHelper.Pi / 6, 0.05f);
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + Vector2.SmoothStep(new Vector2(32, 0), new Vector2(12, 7), fac), MathHelper.SmoothStep(0, -MathHelper.PiOver2, fac), new Vector2(10, 14));

            }
            else
            {
                Anton.SetCompositeArmFront(false, 0, 0);
                Anton.SetCompositeArmBack(false, 0, 0);

                Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(12, 7), -MathHelper.PiOver2, new Vector2(10, 14));

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
            Vector2 center = new Vector2(64368, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = new Vector2(0, MathHelper.SmoothStep(-192, -96, factor));
            OnThatSidePlayer.zoomTarget = MathHelper.SmoothStep(1.5f, 3, factor);
            #endregion

            #region 绘制内容
            Victor.direction = -1;
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-32, 0), MathHelper.PiOver2, new Vector2(10, 14));
            //bool flag = true;
            //if (flag)
            //    Main.EntitySpriteDraw(TextureAssets.MagicPixel.Value, new Vector2(960 - 18, 560 - 85) - OnThatSidePlayer.screenOffsetor, new Rectangle(0, 0, 1, 1), Color.DarkRed, 0, new Vector2(.5f), new Vector2(2, 4), 0, 0);
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(12, 7), -MathHelper.PiOver2, new Vector2(10, 14));
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
            Vector2 center = new Vector2(64368, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = new Vector2(factor < .33f ? MathHelper.SmoothStep(0, -32f, 3 * factor) : MathHelper.SmoothStep(-32f, 32f, (factor - 0.33f) / 0.67f), MathHelper.SmoothStep(-96, -256 / 6f, 6 * factor));
            OnThatSidePlayer.zoomTarget = MathHelper.SmoothStep(3, 6, 4 * factor);
            #endregion

            #region 绘制内容
            if (timer < 120)
            {
                float fac = timer / 120f;
                Victor.compositeFrontArm.enabled = true;
                Victor.compositeBackArm.enabled = true;
                Victor.compositeFrontArm.stretch = Player.CompositeArmStretchAmount.Full;
                Victor.compositeFrontArm.rotation = MathHelper.Lerp(Victor.compositeFrontArm.rotation, -fac * (1 - fac) * 4 * MathHelper.Pi / 3, 0.05f);
                Victor.compositeBackArm.rotation = MathHelper.Lerp(Victor.compositeBackArm.rotation, -fac * (1 - fac) * 4 * MathHelper.Pi / 6, 0.05f);
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-32, 0), MathHelper.SmoothStep(MathHelper.PiOver2, 0, fac), new Vector2(10, 14));

            }
            else if (timer < 240)
            {
                Victor.SetCompositeArmFront(false, 0, 0);
                Victor.SetCompositeArmBack(false, 0, 0);
                Victor.velocity = new Vector2(-32 / 120f, 0);
                Victor.PlayerFrame();
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(MathHelper.SmoothStep(-32, -64, (timer - 120f) / 120f), 0), 0, new Vector2(10, 14));

            }
            else
            {
                Victor.velocity = default;
                Victor.PlayerFrame();
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-64, 0), 0, new Vector2(10, 14));

            }
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(12, 7), -MathHelper.PiOver2, new Vector2(10, 14));

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
            Vector2 center = new Vector2(64368, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = Vector2.SmoothStep(new Vector2(32, -256 / 6f), new Vector2(0, -192f), 4 * factor);
            OnThatSidePlayer.zoomTarget = MathHelper.SmoothStep(6, 2, 4 * factor);
            #endregion

            #region 绘制内容
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-64, 0), 0, new Vector2(10, 14));
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(12, 7), -MathHelper.PiOver2, new Vector2(10, 14));

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
            Vector2 center = new Vector2(64368, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = Vector2.SmoothStep(new Vector2(0, -192f), new Vector2(-128f, -320f), 4 * factor);
            OnThatSidePlayer.zoomTarget = MathHelper.SmoothStep(2, 1.414f, 4 * factor);
            #endregion

            #region 绘制内容
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-64, 0), 0, new Vector2(10, 14));
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(12, 7), -MathHelper.PiOver2, new Vector2(10, 14));

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
            Vector2 center = new Vector2(64368, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = Vector2.SmoothStep(new Vector2(-128f, -320f), new Vector2(640f, -432f), 4 * factor);
            OnThatSidePlayer.zoomTarget = MathHelper.SmoothStep(1.414f, 1f, 4 * factor);
            #endregion

            #region 绘制内容
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-64, 0), 0, new Vector2(10, 14));
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(12, 7), -MathHelper.PiOver2, new Vector2(10, 14));

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
            Vector2 center = new Vector2(64368, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = Vector2.SmoothStep(new Vector2(640f, -432f), new Vector2(0f, -64f), 20 * factor);
            OnThatSidePlayer.zoomTarget = MathHelper.SmoothStep(1f, 6f, 5 * factor);
            #endregion

            #region 绘制内容
            if (timer < 270) 
            {
                Victor.direction = -1;
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-64, 0), 0, new Vector2(10, 14));
            }
            else if (timer < 330)
            {
                float fac = (timer - 270) / 60f;
                Victor.velocity = new Vector2(64 / 120f, 0);
                Victor.PlayerFrame();
                Victor.direction = 1;
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(MathHelper.SmoothStep(-64, -16, fac), 0), 0, new Vector2(10, 14));

            }
            else
            {
                Victor.velocity = default;
                Victor.PlayerFrame();
                Victor.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, -MathHelper.Pi / 3 * 2 * MathHelper.SmoothStep(0, 1, (timer - 120f) / 30f));
                Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-16, 0), 0, new Vector2(10, 14));
            }
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(12, 7), -MathHelper.PiOver2, new Vector2(10, 14));

            #endregion
        }
        public void Part9(int timer, out int newTimer)
        {
            #region 参数声明与赋值
            newTimer = -1;
            if (timer < 0) return;
            int partMaxtime = 2400;
            newTimer = timer - partMaxtime;
            if (timer > partMaxtime) return;
            float factor = MathHelper.Clamp(timer / (float)partMaxtime, 0, 1);
            Vector2 center = new Vector2(64368, 2539 - 22);
            #endregion

            #region 画面修改
            OnThatSidePlayer.screenOffsetor = Vector2.SmoothStep(new Vector2(0, -64f), new Vector2(0f, -96f), 20 * factor);
            OnThatSidePlayer.zoomTarget = MathHelper.SmoothStep(6f, 3f, 20 * factor);
            #endregion

            #region 绘制内容
            Main.PlayerRenderer.DrawPlayer(Main.Camera, Victor, center + new Vector2(-16, 0), 0, new Vector2(10, 14));

            Main.PlayerRenderer.DrawPlayer(Main.Camera, Anton, center + new Vector2(12, 7), -MathHelper.PiOver2, new Vector2(10, 14));

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
            Vector2 center = new Vector2(64368, 2539 - 22);
            if (Timer == 1)
            {
                Main.hideUI = true;
                //Main.NewText("开始！");
            }
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
            for (int n = -7; n < 8; n++)
            {
                Vector2 wheelCen = new Vector2(960 + 17 * 16 * n, 520) - OnThatSidePlayer.screenOffsetor;
                Color wheelColor = new Color(51, 51, 51);
                float wheelRot = factor * factor * MathHelper.TwoPi * 480;
                Main.EntitySpriteDraw(TextureAssets.Item[3811].Value, wheelCen + new Vector2(-96, 0), null, wheelColor, wheelRot, new Vector2(11), 1.5f, 0, 0);
                Main.EntitySpriteDraw(TextureAssets.Item[3811].Value, wheelCen + new Vector2(-64, 0), null, wheelColor, wheelRot, new Vector2(11), 1.5f, 0, 0);
                Main.EntitySpriteDraw(TextureAssets.Item[3811].Value, wheelCen + new Vector2(64, 0), null, wheelColor, wheelRot, new Vector2(11), 1.5f, 0, 0);
                Main.EntitySpriteDraw(TextureAssets.Item[3811].Value, wheelCen + new Vector2(96, 0), null, wheelColor, wheelRot, new Vector2(11), 1.5f, 0, 0);

            }

            return false;
        }
        public override void AI()
        {
            var player = Main.player[Projectile.owner];
            var factor = Timer / (float)MaxTime;
            Projectile.Center = player.Center = new Vector2(MathHelper.Lerp(6520, 124680, factor * factor), 6587);
            base.AI();
        }
        public override void Kill(int timeLeft)
        {
            OnThatSidePlayer.zoomTarget = 1f;
            OnThatSidePlayer.screenOffsetor = default;
            base.Kill(timeLeft);
        }
    }
    public class OnThatSideBiome : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<AnimationProjectile>()] > 0;
        }
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Music/OnThatSideSong");
    }
    public class MouthLayer : PlayerDrawLayer
    {
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            var player = drawInfo.drawPlayer;
            if (player.GetModPlayer<OnThatSidePlayer>().drawMouth)
            {
                drawInfo.DrawDataCache.Add(new DrawData(TextureAssets.MagicPixel.Value, player.Center - Main.screenPosition + new Vector2(player.direction * 3, -4), new Rectangle(0, 0, 1, 1), new Color(217, 48, 54), 0, new Vector2(.5f), new Vector2(4, 2), 0, 0));
            }
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);
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
        public static float zoomTarget;
        public bool drawMouth;
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
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<AnimationProjectile>()] > 0)
                foreach (var proj in Main.projectile) { if (proj.type == ProjectileID.FallingStarSpawner && proj.active) proj.Kill(); }
            //screenOffsetor = default;
            //zoomTarget = 1f;
            base.ResetEffects();
        }
        public override void PostUpdate()
        {
            base.PostUpdate();
        }
        public override void ModifyScreenPosition()
        {
            Main.screenPosition += screenOffsetor;
            Main.GameZoomTarget = zoomTarget;
            base.ModifyScreenPosition();
        }
    }
    public class OnThatSide : Mod
    {
        public static dynamic projectorList;
        public static int StartOffset => 0;// 2320 3340 4420 5500 6100 6640 7420
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
                foreach (var proj in Main.projectile)
                {
                    if (proj.type == animType && proj.active)
                    {
                        proj.timeLeft = 12660 - OnThatSide.StartOffset;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    player.Center = new Vector2(6520, 6587);
                    Projectile.NewProjectileDirect(player.GetSource_ItemUse(Item), player.Center, default, animType, 0, 0, player.whoAmI).timeLeft = 12660 - OnThatSide.StartOffset;
                }
                Main.dayTime = true;
                Main.time = Main.dayLength * 0.875f;
            }
            //Main.NewText(player.itemAnimation);
            return base.UseItem(player);
        }
    }
}