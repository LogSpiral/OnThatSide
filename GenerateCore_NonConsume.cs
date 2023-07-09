using ImproveGame.Common.ConstructCore;
using ImproveGame.Common.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;
using Terraria.UI;
using Terraria.UI.Gamepad;
//using static OnThatSide.MyUtils;
namespace OnThatSide
{
    //public class MyUtils//来自qol
    //{
    //    public static int TileFrameToPlaceStyle(int type, int frameX, int frameY)
    //    {
    //        switch (type)
    //        {
    //            case TileID.Torches: // tnnd火把搞特殊
    //                return frameY / 22;
    //            case TileID.GolfCupFlag:
    //            case TileID.LogicGateLamp:
    //            case TileID.MetalBars:
    //            case TileID.Bottles: // 横向排列 一格间隙 单排
    //                return frameX / 18;
    //            case TileID.LunarMonolith:
    //            case TileID.PottedCrystalPlants:
    //            case TileID.PotsSuspended:
    //            case TileID.FishingCrate:
    //            case TileID.AlphabetStatues:
    //            case TileID.Painting2X3:
    //            case TileID.WaterFountain:
    //            case TileID.GrandfatherClocks:
    //            case TileID.Bowls:
    //            case TileID.CookingPots:
    //            case TileID.Tombstones:
    //            case TileID.Containers:
    //            case TileID.Containers2:
    //            case TileID.FakeContainers:
    //            case TileID.FakeContainers2:
    //            case TileID.WorkBenches:
    //            case TileID.Anvils:
    //            case TileID.MythrilAnvil: // 横向排列 两格间隙 单排
    //                return frameX / 36;
    //            case TileID.MasterTrophyBase:
    //            case TileID.PottedLavaPlants:
    //            case TileID.TeleportationPylon:
    //            case TileID.GemSaplings:
    //            case TileID.Tables2:
    //            case TileID.Campfire:
    //            case TileID.Cannon:
    //            case TileID.AdamantiteForge: // 横向排列 三格间隙 单排
    //                return frameX / 54;
    //            case TileID.PicnicTable: // 横向排列 四格间隙 单排
    //                return frameX / 72;
    //            case TileID.WeightedPressurePlate:
    //            case TileID.LogicSensor:
    //            case TileID.LogicGate:
    //            case TileID.Traps:
    //            case TileID.PressurePlates:
    //            case TileID.Candles:
    //            case TileID.Platforms: // 竖向排列 一格间隙 单排
    //                return frameY / 18;
    //            case TileID.Painting3X2:
    //            case TileID.Firework:
    //            case TileID.Sinks:
    //            case TileID.Candelabras:
    //            case TileID.Bathtubs:
    //            case TileID.Beds:
    //            case TileID.HangingLanterns:
    //            case TileID.Toilets:
    //            case TileID.Chairs: // 竖向排列 两格间隙 单排
    //                return frameY / 36;
    //            // 双排特例
    //            case TileID.Statues: // 横向排列 两格间隙 双排 特殊的转向排列...
    //                if (frameY is 0 or 162)
    //                    return frameX / 36;
    //                else if (frameY is 54 or 162 + 54)
    //                    return frameX / 36 + 55;
    //                return 0;
    //            case TileID.Bookcases:
    //            case TileID.Benches:
    //            case TileID.Dressers:
    //            case TileID.Pianos: // 横向排列 三格间隙 双排
    //                if (frameY is 0)
    //                    return frameX / 54;
    //                else
    //                    return frameX / 54 + 36;
    //            case TileID.Tables:
    //                if (frameY is 0)
    //                    return frameX / 54;
    //                else
    //                    return frameX / 54 + 35;
    //            case TileID.MusicBoxes: // 竖向排列 两格间隙 双排
    //                if (frameX is 0 or 18 * 2)
    //                    return frameY / 54;
    //                else if (frameX is 18 * 4 or 18 * 6)
    //                    return frameY / 54 + 56;
    //                return 0;
    //            case TileID.Chandeliers: // 竖向排列 三格间隙 双排
    //                if (frameX is 0 or 18 * 3)
    //                    return frameY / 54;
    //                else if (frameX is 18 * 6 or 18 * 9)
    //                    return frameY / 54 + 37;
    //                return 0;
    //            case TileID.OpenDoor: // OpenDoor实际上没用，因为没有物品放置开着的门
    //                if (frameX is 0 or 18 * 2)
    //                    return frameY / 54;
    //                else if (frameX is 18 * 4 or 18 * 6)
    //                    return frameY / 54 + 36;
    //                return 0;
    //            case TileID.ClosedDoor:
    //                if (frameX is 0 or 18 or 18 * 2)
    //                    return frameY / 54;
    //                else if (frameX is 18 * 3 or 18 * 4 or 18 * 5)
    //                    return frameY / 54 + 36;
    //                return 0;
    //            case TileID.Lamps:
    //                if (frameX is 0 or 18 * 2)
    //                    return frameY / 54;
    //                else if (frameX is 18 * 4 or 18 * 6)
    //                    return frameY / 54 + 37;
    //                return 0;
    //            case TileID.Painting6X4: // 竖向排列 四格间隙 双排
    //                if (frameX is 0)
    //                    return frameY / 72;
    //                else if (frameX is 18 * 6)
    //                    return frameY / 72 + 27;
    //                return 0;
    //            // 三排!?!?!?!?
    //            case TileID.Banners: // 横向 一格间隔
    //                if (frameY is 0)
    //                    return frameX / 18;
    //                else if (frameY is 18 * 3)
    //                    return frameX / 18 + 111;
    //                else if (frameY is 18 * 6)
    //                    return frameX / 18 + 222;
    //                return 0;
    //            case TileID.Painting3X3: // 横向 三格间隔
    //                if (frameY is 0)
    //                    return frameX / 54;
    //                else if (frameY is 18 * 3)
    //                    return frameX / 54 + 36;
    //                else
    //                    return frameX / 54 + 72;
    //        }
    //        return 0;
    //    }
    //    public static int GetItemTile(int itemType)
    //    {
    //        if (MaterialCore.ItemToTile.TryGetValue(itemType, out int tileType))
    //        {
    //            return tileType;
    //        }
    //        return -1;
    //    }

    //    public static int GetTileItem(int tileType, int tileFrameX, int tileFrameY)
    //    {
    //        int getItemTileType = tileType; // 用于找到物品的type（主要是给门用），到时候placeStyle是用tileType
    //        if (getItemTileType is TileID.OpenDoor)
    //        {
    //            getItemTileType = TileID.ClosedDoor;
    //        }
    //        if (MaterialCore.TileToItem.TryGetValue(getItemTileType, out List<int> itemTypes))
    //        {
    //            return itemTypes.FirstOrDefault(i => MaterialCore.ItemToPlaceStyle[i] == TileFrameToPlaceStyle(tileType, tileFrameX, tileFrameY) || i >= Main.maxItemTypes, -1);
    //        }
    //        return -1;
    //    }

    //    public static int GetItemWall(int itemType)
    //    {
    //        if (MaterialCore.ItemToWall.TryGetValue(itemType, out int wallType))
    //        {
    //            return MaterialCore.ItemToWall[wallType];
    //        }
    //        return -1;
    //    }

    //    public static int GetWallItem(int wallType)
    //    {
    //        //if (!MaterialCore.FinishSetup)
    //        //{
    //        //    return -1;
    //        //}

    //        // 合 并 同 类 项 https://terraria.wiki.gg/zh/wiki/%E5%A2%99_ID
    //        if (wallType is WallID.DirtUnsafe or WallID.DirtUnsafe1 or WallID.DirtUnsafe2 or WallID.DirtUnsafe3 or WallID.DirtUnsafe4)
    //            wallType = WallID.Dirt;
    //        if (wallType is WallID.HellstoneBrickUnsafe)
    //            wallType = WallID.HellstoneBrick;
    //        if (wallType is WallID.ObsidianBrickUnsafe)
    //            wallType = WallID.ObsidianBrick;
    //        if (wallType is WallID.MudUnsafe)
    //            wallType = WallID.MudWallEcho;
    //        if (wallType is WallID.SpiderUnsafe)
    //            wallType = WallID.SpiderEcho;
    //        if (wallType is WallID.ObsidianBackUnsafe)
    //            wallType = WallID.ObsidianBackEcho;
    //        if (wallType is WallID.MushroomUnsafe)
    //            wallType = WallID.Mushroom;
    //        if (wallType is WallID.HiveUnsafe)
    //            wallType = WallID.Hive;
    //        if (wallType is WallID.LihzahrdBrickUnsafe)
    //            wallType = WallID.LihzahrdBrick;
    //        // 大理石与花岗岩
    //        if (wallType is WallID.MarbleUnsafe)
    //            wallType = WallID.Marble;
    //        if (wallType is WallID.GraniteUnsafe)
    //            wallType = WallID.Granite;
    //        // 普通石墙系列
    //        if (wallType is WallID.EbonstoneUnsafe) // 黑檀石墙
    //            wallType = WallID.EbonstoneEcho;
    //        if (wallType is WallID.CrimstoneUnsafe) // 猩红石墙
    //            wallType = WallID.CrimstoneEcho;
    //        if (wallType is WallID.PearlstoneBrickUnsafe) // 珍珠石墙
    //            wallType = WallID.PearlstoneEcho;
    //        // 草墙 丛林墙 花墙
    //        if (wallType is >= 63 and <= 65)
    //            wallType = wallType - 63 + 66;
    //        if (wallType is WallID.HallowedGrassUnsafe) // 神圣草墙
    //            wallType = WallID.HallowedGrassEcho;
    //        if (wallType is WallID.CrimsonGrassUnsafe) // 猩红草墙
    //            wallType = WallID.CrimsonGrassEcho;
    //        if (wallType is WallID.CorruptGrassUnsafe) // 腐化草墙
    //            wallType = WallID.CorruptGrassEcho;
    //        // https://terraria.wiki.gg/zh/wiki/%E5%AE%9D%E7%9F%B3%E5%A2%99 宝石墙
    //        if (wallType is >= 48 and <= 53)
    //            wallType = wallType - 48 + 250;
    //        // https://terraria.wiki.gg/zh/wiki/%E5%9C%B0%E7%89%A2%E7%A0%96%E5%A2%99 地牢砖墙
    //        if (wallType is >= 7 and <= 9)
    //            wallType = wallType - 7 + 17;
    //        if (wallType is >= 94 and <= 99)
    //            wallType = wallType - 94 + 100;
    //        // https://terraria.wiki.gg/zh/wiki/%E6%B2%99%E5%B2%A9%E5%A2%99 沙岩墙
    //        // https://terraria.wiki.gg/zh/wiki/%E8%85%90%E5%8C%96%E5%A2%99 腐化墙
    //        // https://terraria.wiki.gg/zh/wiki/%E7%8C%A9%E7%BA%A2%E5%A2%99 猩红墙
    //        // https://terraria.wiki.gg/zh/wiki/%E5%9C%9F%E5%A2%99%EF%BC%88%E5%A4%A9%E7%84%B6%EF%BC%89 斑驳的土墙
    //        // https://terraria.wiki.gg/zh/wiki/%E7%A5%9E%E5%9C%A3%E5%A2%99 神圣墙
    //        // https://terraria.wiki.gg/zh/wiki/%E4%B8%9B%E6%9E%97%E5%A2%99%EF%BC%88%E5%A4%A9%E7%84%B6%EF%BC%89 特殊丛林墙
    //        // https://terraria.wiki.gg/zh/wiki/%E7%86%94%E5%B2%A9%E5%A2%99 熔岩墙
    //        // https://terraria.wiki.gg/zh/wiki/%E6%B4%9E%E5%A3%81 洞壁 (一部分)
    //        // https://terraria.wiki.gg/zh/wiki/%E7%A1%AC%E5%8C%96%E6%B2%99%E5%A2%99 硬化沙墙
    //        // https://terraria.wiki.gg/zh/wiki/%E6%B2%99%E6%BC%A0%E5%8C%96%E7%9F%B3%E5%A2%99 沙漠化石墙
    //        if (wallType is >= 187 and <= 223)
    //            wallType = wallType - 188 + 275;
    //        // https://terraria.wiki.gg/zh/wiki/%E6%B4%9E%E5%A3%81 洞壁
    //        if (wallType is >= 54 and <= 59)
    //            wallType = wallType - 54 + 256;
    //        if (wallType is WallID.Cave7Unsafe)
    //            wallType = WallID.Cave7Echo;
    //        if (wallType is WallID.Cave8Unsafe)
    //            wallType = WallID.Cave8Echo;
    //        if (wallType is >= 170 and <= 171)
    //            wallType = wallType - 170 + 270;
    //        if (MaterialCore.WallToItem.TryGetValue(wallType, out var itemType))
    //            return itemType[0]; // 可以多对一，但我懒得写了
    //        return -1;
    //    }

    //    /// <summary>
    //    /// 获取多格物块的左上角位置
    //    /// </summary>
    //    /// <param name="i">物块的 X 坐标</param>
    //    /// <param name="j">物块的 Y 坐标</param>
    //    public static Point16 GetTileOrigin(int i, int j)
    //    {
    //        Tile tile = Framing.GetTileSafely(i, j);
    //        TileObjectData tileData = TileObjectData.GetTileData(tile.TileType, 0);
    //        if (tileData == null)
    //        {
    //            return Point16.NegativeOne;
    //        }
    //        int frameX = tile.TileFrameX;
    //        int frameY = tile.TileFrameY;
    //        int subX = frameX % tileData.CoordinateFullWidth;
    //        int subY = frameY % tileData.CoordinateFullHeight;

    //        Point16 coord = new(i, j);
    //        Point16 frame = new(subX / 18, subY / 18);

    //        return coord - frame;
    //    }

    //    /// <inheritdoc cref="TryGetTileEntityAs{T}(Point, out T)"/>
    //    public static bool TryGetTileEntityAs<T>(Point16 position, out T entity) where T : TileEntity
    //    {
    //        return TryGetTileEntityAs(position.X, position.Y, out entity);
    //    }

    //    /// <inheritdoc cref="TryGetTileEntityAs{T}(int, int, out T)"/>
    //    /// <param name="position">物块的坐标</param>
    //    public static bool TryGetTileEntityAs<T>(Point position, out T entity) where T : TileEntity
    //    {
    //        return TryGetTileEntityAs(position.X, position.Y, out entity);
    //    }

    //    /// <summary>
    //    /// 通过 <seealso cref="GetTileOrigin(int, int)"/> 快速获取处于 (<paramref name="i"/>, <paramref name="j"/>) 的 <see cref="TileEntity"/> 实例.
    //    /// </summary>
    //    /// <typeparam name="T">实例类型，应为 <see cref="TileEntity"/></typeparam>
    //    /// <param name="i">物块的 X 坐标</param>
    //    /// <param name="j">物块的 Y 坐标</param>
    //    /// <param name="entity">找到的 <typeparamref name="T"/> 实例，如果没有则是null</param>
    //    /// <returns>返回 <see langword="true"/> 如果找到了 <typeparamref name="T"/> 实例，返回 <see langword="false"/> 如果没有实例或者该实体并非为 <typeparamref name="T"/></returns>
    //    public static bool TryGetTileEntityAs<T>(int i, int j, out T entity) where T : TileEntity
    //    {
    //        Point16 origin = GetTileOrigin(i, j);

    //        if (TileEntity.ByPosition.TryGetValue(origin, out TileEntity existing) && existing is T existingAsT)
    //        {
    //            entity = existingAsT;
    //            return true;
    //        }

    //        entity = null;
    //        return false;
    //    }

    //    /// <summary>
    //    /// 快捷开关箱子
    //    /// </summary>
    //    /// <param name="player">玩家实例</param>
    //    /// <param name="chestId">箱子ID（对于便携储存是-2/-3/-4/-5，对于其他箱子是在<see cref="Main.chest"/>的索引）</param>
    //    public static void ToggleChest(ref Player player, int chestId, int x = -1, int y = -1, SoundStyle? sound = null)
    //    {
    //        if (player.chest == chestId)
    //        {
    //            player.chest = -1;
    //            SoundEngine.PlaySound(sound ?? SoundID.MenuClose);
    //        }
    //        else
    //        {
    //            x = x == -1 ? player.Center.ToTileCoordinates().X : x;
    //            y = y == -1 ? player.Center.ToTileCoordinates().Y : y;
    //            // 以后版本TML会加的东西，只不过现在stable还没有，现在就先放在这里吧
    //            //player.OpenChest(x, y, chestID);
    //            player.chest = chestId;
    //            for (int i = 0; i < 40; i++)
    //            {
    //                ItemSlot.SetGlow(i, -1f, chest: true);
    //            }

    //            player.chestX = x;
    //            player.chestY = y;
    //            player.SetTalkNPC(-1);
    //            Main.SetNPCShopIndex(0);

    //            UILinkPointNavigator.ForceMovementCooldown(120);
    //            if (PlayerInput.GrappleAndInteractAreShared)
    //                PlayerInput.Triggers.JustPressed.Grapple = false;

    //            SoundEngine.PlaySound(sound ?? SoundID.MenuOpen);
    //        }
    //        Main.playerInventory = true;
    //        Recipe.FindRecipes();
    //    }

    //    /// <summary>
    //    /// 尝试破坏物块，需要有镐子，并且挖的动。
    //    /// </summary>
    //    /// <param name="x"></param>
    //    /// <param name="y"></param>
    //    /// <param name="player"></param>
    //    /// <returns></returns>
    //    public static bool TryKillTile(int x, int y, Player player)
    //    {
    //        Tile tile = Main.tile[x, y];
    //        if (tile.HasTile && !Main.tileHammer[Main.tile[x, y].TileType])
    //        {
    //            if (player.HasEnoughPickPowerToHurtTile(x, y))
    //            {
    //                if (tile.TileType is 2 or 477 or 492 or 23 or 60 or 70 or 109 or 199 || Main.tileMoss[tile.TileType] || TileID.Sets.tileMossBrick[tile.TileType])
    //                {
    //                    player.PickTile(x, y, 10000);
    //                }
    //                player.PickTile(x, y, 10000);
    //            }
    //        }
    //        return !Main.tile[x, y].HasTile;
    //    }

    //    public static bool CanDestroyTileAnyCases(int x, int y, Player player)
    //    {
    //        Tile tile = Main.tile[x, y];
    //        if (!tile.HasTile)
    //            return true;
    //        if (!Main.tileHammer[tile.TileType])
    //        {
    //            return player.HasEnoughPickPowerToHurtTile(x, y) && WorldGen.CanKillTile(x, y);
    //        }
    //        return false;
    //    }

    //    /// <summary>
    //    /// 你猜干嘛用的，bongbong！！！
    //    /// </summary>
    //    /// <param name="position"></param>
    //    /// <param name="width"></param>
    //    /// <param name="height"></param>
    //    public static void BongBong(Vector2 position, int width, int height)
    //    {
    //        if (Main.rand.NextBool(6))
    //        {
    //            Gore.NewGore(null, position + new Vector2(Main.rand.Next(16), Main.rand.Next(16)), Vector2.Zero, Main.rand.Next(61, 64));
    //        }
    //        if (Main.rand.NextBool(2))
    //        {
    //            int index = Dust.NewDust(position, width, height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
    //            Main.dust[index].velocity *= 1.4f;
    //        }
    //        if (Main.rand.NextBool(3))
    //        {
    //            int index = Dust.NewDust(position, width, height, DustID.Torch, 0f, 0f, 100, default, 2.5f);
    //            Main.dust[index].noGravity = true;
    //            Main.dust[index].velocity *= 5f;
    //            index = Dust.NewDust(position, width, height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
    //            Main.dust[index].velocity *= 3f;
    //        }
    //    }

    //    /// <summary>
    //    /// 放物块，但是有魔杖特判
    //    /// </summary>
    //    public static bool TryPlaceTile(int i, int j, Item item, Player player, bool mute = false, bool forced = false)
    //    {
    //        int targetTile = item.createTile;
    //        var tileObjectData = TileObjectData.GetTileData(targetTile, 0);
    //        if (tileObjectData is null || tileObjectData.CoordinateFullWidth <= 22 && tileObjectData.CoordinateFullHeight <= 22)
    //        {
    //            if (Main.tile[i, j].HasTile)
    //                return false;
    //        }
    //        else
    //        {
    //            var origin = new Point16(i, j) - tileObjectData.Origin;
    //            for (int m = 0; m < tileObjectData.CoordinateFullWidth / 16; m++)
    //            {
    //                for (int n = 0; n < tileObjectData.CoordinateFullHeight / 16; n++)
    //                {
    //                    if (Main.tile[origin.ToPoint() + new Point(m, n)].HasTile)
    //                        return false;
    //                }
    //            }
    //        }
    //        bool placed = WorldGen.PlaceTile(i, j, targetTile, mute, forced, player.whoAmI, item.placeStyle);
    //        return placed;
    //    }

    //    public enum CheckType
    //    {
    //        TypeAndStyle,
    //        Type
    //    }
    //    /// <summary>
    //    /// 判断物块是否相同
    //    /// </summary>
    //    public static bool SameTile(int i, int j, int Type, int Style, CheckType tileSort = CheckType.TypeAndStyle)
    //    {
    //        if (tileSort == CheckType.TypeAndStyle)
    //        {
    //            if (Main.tile[i, j].TileType == Type && Main.tile[i, j].TileFrameY == Style * 18)
    //            {
    //                return true;
    //            }
    //        }
    //        else if (tileSort == CheckType.Type)
    //        {
    //            if (Main.tile[i, j].TileType == Type)
    //            {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }
    //}
    //public class GenerateCore_NonConsume
    //{
    //    private static int _taskProcessed;
    //    public static dynamic GenerateRunner
    //    {
    //        get
    //        {
    //            if (generateRunner == null)
    //            {
    //                var fieldInfo = typeof(CoroutineSystem).GetField("GenerateRunner", BindingFlags.NonPublic | BindingFlags.Static);
    //                generateRunner = fieldInfo?.GetValue(null);
    //            }
    //            return generateRunner;
    //        }
    //    }
    //    static dynamic generateRunner;
    //    public static void GenerateFromTag(TagCompound tag, Point position)
    //    {
    //        GenerateRunner.Run(Generate(tag, position));
    //    }

    //    public static IEnumerator Generate(TagCompound tag, Point position)
    //    {
    //        var structure = (QoLStructure)typeof(QoLStructure).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, new Type[] { tag.GetType() }).Invoke(new object[] { tag });

    //        if (structure.StructureDatas is null || structure.StructureDatas.Count is 0)
    //        {
    //            // 此处应有Logger.Warn
    //            yield break;
    //        }

    //        // 添加Origin偏移
    //        position.X -= structure.OriginX;
    //        position.Y -= structure.OriginY;

    //        _taskProcessed = 0;
    //        var currentTask = GenerateRunner.Run(KillTiles(structure, position));
    //        while (currentTask.IsRunning)
    //            yield return null;
    //        currentTask = GenerateRunner.Run(GenerateSingleTiles(structure, position));
    //        while (currentTask.IsRunning)
    //            yield return null;
    //        currentTask = GenerateRunner.Run(GenerateWalls(structure, position));
    //        while (currentTask.IsRunning)
    //            yield return null;
    //        currentTask = GenerateRunner.Run(GenerateMultiTiles(structure, position));
    //        while (currentTask.IsRunning)
    //            yield return null;
    //        currentTask = GenerateRunner.Run(GenerateOutSet(structure, position));
    //        while (currentTask.IsRunning)
    //            yield return null;
    //        GenerateRunner.Run(SquareTiles(structure, position));
    //    }

    //    public static IEnumerator KillTiles(QoLStructure structure, Point position)
    //    {
    //        if (false)
    //        {
    //            yield break;
    //        }

    //        int width = structure.Width;
    //        int height = structure.Height;
    //        for (int x = 0; x <= width; x++)
    //        {
    //            for (int y = 0; y <= height; y++)
    //            {
    //                var placePosition = position + new Point(x, y);
    //                Tile tile = Main.tile[placePosition.X, placePosition.Y];
    //                if (tile.HasTile && TryKillTile(placePosition.X, placePosition.Y, Main.LocalPlayer))
    //                {
    //                    _taskProcessed++;
    //                }
    //                if (tile.WallType > 0)
    //                {
    //                    WorldGen.KillWall(placePosition.X, placePosition.Y);
    //                    _taskProcessed++;
    //                }
    //                if (_taskProcessed >= 12)
    //                {
    //                    _taskProcessed = 0;
    //                    yield return null;
    //                }
    //            }
    //        }
    //    }

    //    public static IEnumerator GenerateSingleTiles(QoLStructure structure, Point position)
    //    {
    //        int width = structure.Width;
    //        int height = structure.Height;
    //        for (int x = 0; x <= width; x++)
    //        {
    //            for (int y = height; y >= 0; y--)
    //            {
    //                int index = y + x * (height + 1);
    //                var placePosition = position + new Point(x, y);
    //                TileDefinition tileData = structure.StructureDatas[index];
    //                int tileType = structure.ParseTileType(tileData); // 实际上的Type
    //                int tileItemFindType = tileType; // 用于找到合适物品的Type
    //                if (tileItemFindType is not -1 && TileID.Sets.Grass[tileItemFindType])
    //                {
    //                    tileItemFindType = TileID.Dirt;
    //                }
    //                int tileItemType = GetTileItem(tileItemFindType, tileData.TileFrameX, tileData.TileFrameY);
    //                if (tileItemType == -1 || Main.tile[placePosition].HasTile)
    //                {
    //                    continue;
    //                }

    //                var tileObjectData = TileObjectData.GetTileData(tileType, 0);
    //                if (tileObjectData is not null && (tileObjectData.CoordinateFullWidth > 22 ||
    //                                                   tileObjectData.CoordinateFullHeight > 22))
    //                {
    //                    continue;
    //                }
    //                // 挖掉重来！
    //                if (TileID.Sets.Grass[tileType])
    //                {
    //                    Main.tile[placePosition].ResetToType((ushort)tileType);
    //                    WorldGen.TileFrame(placePosition.X, position.Y, true, false);
    //                }
    //                if (WorldGen.CanPoundTile(placePosition.X, placePosition.Y))
    //                {
    //                    if (tileData.BlockType is BlockType.HalfBlock)
    //                    {
    //                        WorldGen.SlopeTile(placePosition.X, placePosition.Y, 0);
    //                        WorldGen.PoundTile(placePosition.X, placePosition.Y);
    //                    }
    //                    else if (tileData.BlockType is not BlockType.Solid)
    //                    {
    //                        WorldGen.SlopeTile(placePosition.X, placePosition.Y, (int)tileData.BlockType - 1);
    //                    }
    //                }
    //                var tile = Main.tile[placePosition];
    //                tile.TileColor = tileData.TileColor;
    //                if (tileData.ExtraDatas[2] && !tile.IsActuated)
    //                {
    //                    Wiring.ActuateForced(placePosition.X, placePosition.Y);
    //                }
    //                _taskProcessed++;
    //                if (_taskProcessed >= 6)
    //                {
    //                    _taskProcessed = 0;
    //                    yield return null;
    //                }
    //            }
    //        }
    //    }

    //    public static IEnumerator GenerateWalls(QoLStructure structure, Point position)
    //    {
    //        int width = structure.Width;
    //        int height = structure.Height;
    //        for (int x = 0; x <= width; x++)
    //        {
    //            for (int y = 0; y <= height; y++)
    //            {
    //                int index = y + x * (height + 1);
    //                var placePosition = position + new Point(x, y);
    //                TileDefinition tileData = structure.StructureDatas[index];
    //                int wallItemType = GetWallItem(structure.ParseWallType(tileData));
    //                if (wallItemType == -1 || Main.tile[placePosition].WallType != 0)
    //                {
    //                    continue;
    //                }
    //                var tile = Main.tile[placePosition];
    //                tile.WallColor = tileData.WallColor;
    //                _taskProcessed++;
    //                if (_taskProcessed >= 10)
    //                {
    //                    _taskProcessed = 0;
    //                    yield return null;
    //                }
    //            }
    //        }
    //    }

    //    private static bool TryPlaceWall(Item item, int x, int y)
    //    {
    //        if (Main.tile[x, y].WallType != 0)
    //        {
    //            return false;
    //        }

    //        WorldGen.PlaceWall(x, y, item.createWall);
    //        return true;
    //    }

    //    public static IEnumerator GenerateMultiTiles(QoLStructure structure, Point position)
    //    {
    //        int width = structure.Width;
    //        int height = structure.Height;
    //        for (int x = 0; x <= width; x++)
    //        {
    //            for (int y = 0; y <= height; y++)
    //            {
    //                var placePosition = position + new Point(x, y);
    //                int index = y + x * (height + 1);
    //                TileDefinition tileData = structure.StructureDatas[index];
    //                int tileType = structure.ParseTileType(tileData);
    //                if (tileType is -1)
    //                    continue;
    //                var tileObjectData = TileObjectData.GetTileData(tileType, 0);
    //                if (tileObjectData is null || tileObjectData.CoordinateFullWidth <= 22 && tileObjectData.CoordinateFullHeight <= 22)
    //                {
    //                    continue;
    //                }

    //                // 转换为帧坐标
    //                int subX = tileData.TileFrameX / tileObjectData.CoordinateFullWidth * tileObjectData.CoordinateFullWidth;
    //                int subY = tileData.TileFrameY / tileObjectData.CoordinateFullHeight * tileObjectData.CoordinateFullHeight;
    //                int tileItemType = GetTileItem(tileType, subX, subY);
    //                if (tileItemType == -1)
    //                {
    //                    continue;
    //                }

    //                subX = tileData.TileFrameX % tileObjectData.CoordinateFullWidth;
    //                subY = tileData.TileFrameY % tileObjectData.CoordinateFullHeight;
    //                Point16 frame = new(subX / 18, subY / 18);
    //                var origin = tileObjectData.Origin.ToPoint();
    //                if (tileType is TileID.OpenDoor && tileData.TileFrameX / tileObjectData.CoordinateFullWidth % 2 == 1) // 开启的门实际上OriginX为0，这里对于向左开的要重新定位到他的轴心
    //                {
    //                    origin.X = 1;
    //                }

    //                if (frame.X != origin.X || frame.Y != origin.Y)
    //                {
    //                    continue;
    //                }
    //                // 什么怪玩意，还要我特判
    //                if (tileType is TileID.Banners)
    //                {
    //                    int placeStyle = MaterialCore.ItemToPlaceStyle[tileItemType];
    //                    short frameX = (short)(placeStyle % 111 * 18);
    //                    short frameY = (short)(placeStyle / 111 * 54);
    //                    for (int j = 0; j <= 2; j++)
    //                    {
    //                        var tilePosition = placePosition;
    //                        tilePosition.Y += j;

    //                        var tile = Main.tile[tilePosition];
    //                        var stateData = tile.Get<TileWallWireStateData>(); // 只想替换掉TileType，其他数据先存一存
    //                        tile.ResetToType((ushort)tileType);
    //                        tile.Get<TileWallWireStateData>() = stateData; // 还原其他数据
    //                        tile.HasTile = true;
    //                        tile.TileFrameX = frameX;
    //                        tile.TileFrameY = (short)(frameY + j * 18);
    //                    }
    //                }
    //                yield return null;
    //            }
    //        }
    //    }

    //    public static IEnumerator GenerateOutSet(QoLStructure structure, Point position)
    //    {
    //        int width = structure.Width;
    //        int height = structure.Height;
    //        for (int x = 0; x <= width; x++)
    //        {
    //            for (int y = 0; y <= height; y++)
    //            {
    //                int index = y + x * (height + 1);
    //                TileDefinition tileData = structure.StructureDatas[index];

    //                var placePosition = position + new Point(x, y);
    //                var tile = Main.tile[placePosition];
    //                if (tileData.ExtraDatas[2] && !tile.IsActuated)
    //                {
    //                    Wiring.ActuateForced(placePosition.X, placePosition.Y);
    //                }
    //                if (tileData.RedWire && !tile.RedWire)
    //                {
    //                    WorldGen.PlaceWire(placePosition.X, placePosition.Y);
    //                }
    //                if (tileData.BlueWire && !tile.BlueWire)
    //                {
    //                    WorldGen.PlaceWire2(placePosition.X, placePosition.Y);
    //                }
    //                if (tileData.GreenWire && !tile.GreenWire)
    //                {
    //                    WorldGen.PlaceWire3(placePosition.X, placePosition.Y);
    //                }
    //                if (tileData.YellowWire && !tile.YellowWire)
    //                {
    //                    WorldGen.PlaceWire4(placePosition.X, placePosition.Y);
    //                }
    //                if (tileData.HasActuator && !tile.HasActuator) // 促动器
    //                {
    //                    WorldGen.PlaceActuator(placePosition.X, placePosition.Y);
    //                }

    //                tile.WallColor = tileData.WallColor;
    //                tile.TileColor = tileData.TileColor;

    //                _taskProcessed++;
    //                if (_taskProcessed >= 50)
    //                {
    //                    _taskProcessed = 0;
    //                    yield return null;
    //                }
    //            }
    //        }
    //    }

    //    public static IEnumerator SquareTiles(QoLStructure structure, Point position)
    //    {
    //        int width = structure.Width;
    //        int height = structure.Height;
    //        for (int x = 0; x <= width; x++)
    //        {
    //            for (int y = 0; y <= height; y++)
    //            {
    //                var placePosition = position + new Point(x, y);
    //                if (Main.tile[placePosition].HasTile)
    //                {
    //                    WorldGen.TileFrame(placePosition.X, position.Y, true, false);
    //                }
    //                if (Main.tile[placePosition].WallType > 0)
    //                {
    //                    Framing.WallFrame(placePosition.X, position.Y, true);
    //                }
    //                _taskProcessed++;
    //                if (_taskProcessed >= 50)
    //                {
    //                    _taskProcessed = 0;
    //                    yield return null;
    //                }
    //            }
    //        }
    //        if (Main.netMode is NetmodeID.MultiplayerClient)
    //        {
    //            NetMessage.SendTileSquare(Main.myPlayer, position.X - 1, position.Y - 1, width + 2, height + 2);
    //        }
    //    }
    //}
}
