using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
namespace BulletsGoBrrr
{
    public class TextureManager
    {
        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private ContentManager contentManager;
        public TextureManager(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }
        public Texture2D Load(string assetName)
        {
            textures[assetName] = contentManager.Load<Texture2D>(assetName);
            return textures[assetName];
        }
        public List<Texture2D> LoadAll(IEnumerable<string> assetNames)
        {
            return assetNames.Select(this.Load).ToList();
        }
        public Texture2D this[string assetName]
        {
            get => textures[assetName];
        }
    }
}
