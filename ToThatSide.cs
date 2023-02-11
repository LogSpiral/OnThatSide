using SubworldLibrary;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace ToThatSide
{
    public class ToThatSideWorld : Subworld
    {
        public override WorldGenConfiguration Config => base.Config;
        public override int Height => throw new System.NotImplementedException();
        public override string Name => base.Name;
        public override bool NormalUpdates => base.NormalUpdates;
        public override bool NoPlayerSaving => base.NoPlayerSaving;
        public override bool ShouldSave => base.ShouldSave;
        public override List<GenPass> Tasks => throw new System.NotImplementedException();
        public override int Width => throw new System.NotImplementedException();
    }
    public class ToThatSide : Mod
    {
    }
}