using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace OnThatSide
{
    public class SoilderItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("战士");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle aswell as setting a few values that are common across all placeable items
            Item.DefaultToPlaceableTile(ModContent.TileType<Soilder>());

            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Purple;
        }
    }
    public class Soilder : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(120, 85, 60), Language.GetText("MapObject.Trophy"));
            DustType = 7;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<SoilderItem>());
        }
    }

    public class Soilder2Item : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("战士");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle aswell as setting a few values that are common across all placeable items
            Item.DefaultToPlaceableTile(ModContent.TileType<Soilder2>());

            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Purple;
        }
    }
    public class Soilder2 : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(120, 85, 60), Language.GetText("MapObject.Trophy"));
            DustType = 7;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<Soilder2Item>());
        }
    }

    public class Soilder3Item : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("战士");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle aswell as setting a few values that are common across all placeable items
            Item.DefaultToPlaceableTile(ModContent.TileType<Soilder3>());

            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Purple;
        }
    }
    public class Soilder3 : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true;

            TileObjectData newTile = TileObjectData.newTile;
            newTile.Width = 4;
            newTile.Height = 5;
            newTile.Origin = new Point16(2, 3);
            //newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, newTile.Width, 0);
            newTile.UsesCustomCanPlace = true;
            newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16 };
            newTile.CoordinateWidth = 16;
            newTile.CoordinatePadding = 2;
            newTile.DrawYOffset = 2;
            newTile.LavaDeath = true;
            newTile.AnchorWall = true;

            //TileObjectData.newTile.CopyFrom(newTile);
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(120, 85, 60), Language.GetText("MapObject.Trophy"));
            DustType = 7;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<Soilder3Item>());
        }
    }
    public class Soilder4Item : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("战士");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle aswell as setting a few values that are common across all placeable items
            Item.DefaultToPlaceableTile(ModContent.TileType<Soilder4>());

            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Purple;
        }
    }
    public class Soilder4 : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(120, 85, 60), Language.GetText("MapObject.Trophy"));
            DustType = 7;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<Soilder4Item>());
        }
    }
    public class LeaderItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("列宁同志");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle aswell as setting a few values that are common across all placeable items
            Item.DefaultToPlaceableTile(ModContent.TileType<Leader>());

            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Purple;
        }
    }
    public class Leader : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(120, 85, 60), Language.GetText("MapObject.Trophy"));
            DustType = 7;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            base.PostDraw(i, j, spriteBatch);
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            return base.PreDraw(i, j, spriteBatch);
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<LeaderItem>());
        }
    }
    public class Leader2Item : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("列宁同志_大号");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle aswell as setting a few values that are common across all placeable items
            Item.DefaultToPlaceableTile(ModContent.TileType<Leader2>());

            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Purple;
        }
    }
    public class Leader2 : ModTile
    {
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            //var gd = Main.graphics.GraphicsDevice;
            //var blend = gd.BlendState;
            //var sampler = gd.SamplerStates[0];
            //var depth = gd.DepthStencilState;
            //var rasterizer = gd.RasterizerState;
            //var matrix = (Matrix)spriteBatch.GetType().GetField("transformMatrix",BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            //spriteBatch.End();
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, depth, rasterizer, null, Matrix.Identity);
            //var scaler = new Vector2(Main.screenWidth * .5f, Main.screenHeight) / new Vector2(960, 1080);
            //var tile = Main.tile[i, j];
            //if (!(tile.TileFrameX == 54 && tile.TileFrameY == 72)) return;

            //Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            //if (Main.drawToScreen)
            //{
            //    zero = Vector2.Zero;
            //}
            //spriteBatch.Draw(ModContent.Request<Texture2D>("OnThatSide/Lenin").Value, Terraria.Utils.CenteredRectangle(new Vector2(i - 1f, j - 1.5f) * 16 - Main.screenPosition + zero + new Vector2(0, 2), new Vector2(64, 80)), Color.White);
            //Main.NewText(matrix);
            //spriteBatch.End();
            //spriteBatch.Begin(SpriteSortMode.Deferred, blend, sampler, depth, rasterizer, null, matrix);
            //var gd = Main.graphics.GraphicsDevice;
            //Main.NewText(gd.GetRenderTargets()[0].RenderTarget.GetHashCode() + "|||" + Main.instance.tile2Target.GetHashCode());
            base.PostDraw(i, j, spriteBatch);
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            return base.PreDraw(i, j, spriteBatch);
        }
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true;

            TileObjectData newTile = TileObjectData.newTile;
            newTile.Width = 4;
            newTile.Height = 5;
            newTile.Origin = new Point16(2, 3);
            //newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, newTile.Width, 0);
            newTile.UsesCustomCanPlace = true;
            newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16 };
            newTile.CoordinateWidth = 16;
            newTile.CoordinatePadding = 2;
            newTile.DrawYOffset = 2;
            newTile.LavaDeath = true;
            newTile.AnchorWall = true;

            //TileObjectData.newTile.CopyFrom(newTile);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(120, 85, 60), Language.GetText("MapObject.Trophy"));
            DustType = 7;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<Leader2Item>());
        }
    }
}
