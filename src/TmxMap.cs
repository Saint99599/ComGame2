using System.Collections.Generic;

namespace Project1.src
{
    public class TmxMap
    {
        private string v;

        public TmxMap(string v)
        {
            this.v = v;
        }

        public int Width { get; }
        public int Height { get; }
        public int TileWidth { get; }
        public int TileHeight { get; }
        public List<TmxLayer> TileLayers { get; }
        public object Tilesets { get; internal set; }

        // Other properties and methods

        public class TmxLayer
        {
            public int[] Tiles { get; }
            // Other properties and methods
        }

        // Other classes and methods
    }
}