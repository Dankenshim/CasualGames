using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TileMapEx
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D _character;

        List<Texture2D> tileTextures = new List<Texture2D>();
        List<Texture2D> tileInteractableTextures = new List<Texture2D>();

        int[,] tileMap = new int[,]  
    {
        {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3},
        {3,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,3,0,0,0,0,0,0,0,0,0,3,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,3,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,3,0,0,0,3,3,3,3,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,3,0,0,0,3,3,3,3,0,0,3,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,3,0,0,0,0,0,0,3,0,0,3,3,3,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,3,3,3,3,3,3,0,3,0,0,0,0,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,0,0,0,0,0,0,0,3,0,0,0,0,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,3,3,3,3,0,0,0,0,0,0,0,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,3,3,3,3,3,3,3,3,0,0,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,3,3,3,3,0,0,0,0,0,0,0,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,0,0,0,3},
        {3,0,0,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
        {3,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},        
        {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3},

    };
        //dont need to use the hiddenmap below anymore


    //    int[,] hiddenMap = new int[,]  
    //{
    //    {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3},
    //    {3,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,1,0,0,0,0,0,0,0,0,0,2,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,3,0,0,0,0,0,0,0,0,0,3,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,3,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,3,0,0,0,3,3,3,3,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,3,0,0,0,3,3,3,3,0,0,3,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,3,0,0,0,0,0,0,3,0,0,3,3,3,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,3,3,3,3,3,3,0,3,0,0,0,0,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,0,0,0,0,0,0,0,3,0,0,0,0,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,3,3,3,3,0,0,0,0,0,0,0,3,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,3,3,3,3,3,3,3,3,0,0,3,3,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,3,3,3,3,0,0,0,0,0,0,0,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,0,0,0,3},
    //    {3,1,0,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
    //    {3,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},        
    //    {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3},

    //};
        

        int tileWidth = 64;
        int tileHeight = 64;
        Vector2 cameraPosition = new Vector2(0, 0);
        float cameraSpeed = 5;
        private Rectangle _characterRect;
        int gold = 0;
        int score = 0;

        KeyboardState oldState;
        int impassible = 1;
        public enum TileType { Dirt, Grass, Ground, Mud, Road, Rock, Wood };
        SpriteFont debug;
        TileManager _tManager;
        private byte pulseColor;
        Camera2D cam;
        private List<Tile> chestTiles;
        private List<Tile> coinTiles;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = tileWidth * 16;
            graphics.PreferredBackBufferHeight = tileWidth * 9;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _tManager = new TileManager();
            cam = new Camera2D(graphics.GraphicsDevice.Viewport);
            cam.Following = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
      
            
            //background textures
            // assumes dirt is 0 
            Texture2D dirt = Content.Load<Texture2D>("Tiles/se_free_dirt_texture");
            tileTextures.Add(dirt);
            Texture2D road = Content.Load<Texture2D>("Tiles/se_free_road_texture");
            tileTextures.Add(road);
            Texture2D ground = Content.Load<Texture2D>("Tiles/se_free_ground_texture");
            tileTextures.Add(ground);
            Texture2D wall = Content.Load<Texture2D>("Tiles/wall");
            tileTextures.Add(wall);

            //interactable textures
            Texture2D empty = Content.Load<Texture2D>("clearTile");
            tileInteractableTextures.Add(empty);          
            Texture2D chest = Content.Load<Texture2D>("chest");
            tileInteractableTextures.Add(chest);
            Texture2D coin = Content.Load<Texture2D>("coin");
            tileInteractableTextures.Add(coin);
            Texture2D wall2 = Content.Load<Texture2D>("Tiles/wall");
            tileInteractableTextures.Add(wall2);

            _character = Content.Load<Texture2D>("warrior");
            debug = Content.Load<SpriteFont>("debug");
            string[] backTileNames = { "dirt", "grass", "ground","rock" };
            string[] impassibleTiles = { "rock", "ground" };
            //string[] hiddenTileNames = { "NONE", "chest", "key","rock" };

            //_tManager.addLayer("hidden", hiddenTileNames, hiddenMap);
            //int mapWidth = _tManager.Layers[0].MapWidth;
            _tManager.addLayer("background", backTileNames, tileMap);
            _tManager.ActiveLayer = _tManager.getLayer("background");
            _tManager.ActiveLayer.makeImpassable(impassibleTiles);
             //Test against hidden layers
             //all maps are height,width oriented
            //List<Tile> adjfirst = _tManager.Layers[1].adjacentTo(_tManager.Layers[0].Tiles[0, 0]);
            //List<Tile> adjlast = _tManager.Layers[1].adjacentTo(_tManager.Layers[0].Tiles[_tManager.Layers[0].MapHeight-1, _tManager.Layers[0].MapWidth-1]);
            //List<Tile> adjtop = _tManager.Layers[1].adjacentTo(_tManager.Layers[0].Tiles[3, 0]);
            //List<Tile> adjbottom = _tManager.Layers[1].adjacentTo(_tManager.Layers[0].Tiles[_tManager.Layers[0].MapHeight-1,3]);
            //List<Tile> adjleft = _tManager.Layers[1].adjacentTo(_tManager.Layers[0].Tiles[0, 3]);
            //List<Tile> adjright = _tManager.Layers[1].adjacentTo(_tManager.Layers[0].Tiles[3,_tManager.Layers[0].MapWidth-1]);
            //List<Tile> adjcentre = _tManager.Layers[1].adjacentTo(_tManager.Layers[0].Tiles[_tManager.Layers[0].MapHeight / 2, _tManager.Layers[0].MapWidth / 2]);
            
            
            chestTiles = new List<Tile>()
            {
                new Tile() {Id = 1, Passable = true, Interactable= true, TileName = "Chest", Y = 3, X = 2 },
                new Tile() {Id = 1, Passable = true, Interactable = true, TileName = "Chest", Y = 30, X = 2 },
                new Tile() {Id = 1, Passable = true, Interactable= true, TileName = "Chest", Y = 18, X = 13 },
                new Tile() {Id = 1, Passable = true, Interactable = true, TileName = "Chest", Y = 14, X = 15 }
            };

            coinTiles = new List<Tile>()
            {
                new Tile() {Id = 2, Passable = true, Interactable = true,TileName = "Coin", Y = 0, X = 0}
            };
            
            _tManager.CurrentTile = new Tile();
            _tManager.CurrentTile.TileName = "Character";
            _tManager.CurrentTile.X = 6;
            _tManager.CurrentTile.Y = 3;
            _characterRect = new Rectangle(tileWidth * _tManager.CurrentTile.X, tileHeight * _tManager.CurrentTile.Y, tileWidth, tileHeight);
            // TODO: use this.Content to load your game content here

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
           
            // Allows the game to exit


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here


            KeyboardState keyState = Keyboard.GetState();
            

            // check movement of the character against the adjecent tiles in the current active layer
            Tile previousTile = _tManager.CurrentTile;
            
            if (keyState.IsKeyDown(Keys.W) && !oldState.IsKeyDown(Keys.W))
            //if (keyState.IsKeyDown(Keys.W))
            {
                if (_tManager.ActiveLayer.valid("above", _tManager.CurrentTile))
                    _tManager.CurrentTile =
                        _tManager.ActiveLayer.getadjacentTile("above", _tManager.CurrentTile);

            }

            if (keyState.IsKeyDown(Keys.S) && !oldState.IsKeyDown(Keys.S))
            //if (keyState.IsKeyDown(Keys.S))
            {
                if (_tManager.ActiveLayer.valid("below", _tManager.CurrentTile))
                    _tManager.CurrentTile =
                        _tManager.ActiveLayer.getadjacentTile("below", _tManager.CurrentTile);
            }

            if (keyState.IsKeyDown(Keys.A) && !oldState.IsKeyDown(Keys.A))
                //if (keyState.IsKeyDown(Keys.A))
                if (_tManager.ActiveLayer.valid("left", _tManager.CurrentTile))
                    _tManager.CurrentTile =
                        _tManager.ActiveLayer.getadjacentTile("left", _tManager.CurrentTile);

            if (keyState.IsKeyDown(Keys.D) && !oldState.IsKeyDown(Keys.D))
                //if (keyState.IsKeyDown(Keys.D))
                if (_tManager.ActiveLayer.valid("right", _tManager.CurrentTile))
                    _tManager.CurrentTile =
                        _tManager.ActiveLayer.getadjacentTile("right", _tManager.CurrentTile);

            // Calculate the new rectangle position to draw the character in

            Rectangle r = new Rectangle(_tManager.CurrentTile.X * tileWidth,
                                        _tManager.CurrentTile.Y * tileHeight, tileWidth, tileHeight);
            bool inView = GraphicsDevice.Viewport.Bounds.Contains(r);
           
            bool passable = _tManager.ActiveLayer.Tiles[_tManager.CurrentTile.Y, _tManager.CurrentTile.X].Passable;
            
            bool interactable = _tManager.ActiveLayer.Tiles[_tManager.CurrentTile.Y, _tManager.CurrentTile.X].Interactable;
            
            Vector2 PossibleCameraMove = new Vector2(_characterRect.X - GraphicsDevice.Viewport.Bounds.Width / 2,
                                                _characterRect.Y - GraphicsDevice.Viewport.Bounds.Height / 2);
            if (passable)
            {
                _characterRect = r;

            }
            else
            {
                _tManager.CurrentTile = previousTile;

            }


            if (interactable)
            {
                if (keyState.IsKeyDown(Keys.R) && !oldState.IsKeyDown(Keys.R))
                {

                    gold = gold + 20;
                }
            }
            else
            {
            }
            cam.Pos += TileDifference(previousTile, _tManager.CurrentTile);
            oldState = keyState;
            cam.Update();

            base.Update(gameTime);
        }

        Vector2 TileDifference(Tile t1, Tile t2)
        {
            Vector2 v1 = new Vector2(t1.X, t1.Y) * tileWidth;
            Vector2 v2 = new Vector2(t2.X, t2.Y) * tileWidth;
            Vector2 result = v1 - v2;
            return result;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, cam.Transform);

            TileLayer background = _tManager.getLayer("background");
            List<Tile> surroundingTiles = background.adjacentTo(_tManager.CurrentTile);
            for (int x = 0; x < background.MapWidth; x++)
                for (int y = 0; y < background.MapHeight; y++)
                {
                    int textureIndex = background.Tiles[y, x].Id;
                    Texture2D texture = tileTextures[textureIndex];
                    // Draw surrounding tiles
                    if (surroundingTiles.Contains(background.Tiles[y, x]))
                    {           
                        
                        spriteBatch.Draw(texture,
                            new Rectangle(x * tileWidth,
                          y * tileHeight,
                          tileWidth,
                          tileHeight),
                            new Color(255, 255, 0, pulseColor++));
                    }
                    else
                    {
                        spriteBatch.Draw(texture,
                            new Rectangle(x * tileWidth,
                          y * tileHeight,
                          tileWidth,
                          tileHeight),
                            Color.White);
                    }

                }
            foreach (var chestTile in chestTiles)    
            {
            spriteBatch.Draw(tileInteractableTextures[chestTile.Id],new Rectangle(chestTile.X * tileWidth,
                          chestTile.Y * tileHeight,
                          tileWidth,
                          tileHeight),
                            Color.White);
            }

            // draw the character

            spriteBatch.Draw(_character, new Rectangle(_tManager.CurrentTile.X * tileWidth,
                          _tManager.CurrentTile.Y * tileHeight,
                          tileWidth,
                          tileHeight),
                            Color.White);

            //spriteBatch.Draw(_character, _characterRect, Color.White);
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(debug, cam.Pos.ToString(), new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(debug, new Vector2(_tManager.CurrentTile.X, _tManager.CurrentTile.Y).ToString(), new Vector2(10, 30), Color.White);
            spriteBatch.DrawString(debug, "Score: "+ score, new Vector2(10, 50), Color.White);
            spriteBatch.DrawString(debug, "Gold: "+ gold, new Vector2(10, 70), Color.Gold);
            spriteBatch.DrawString(debug, "Player List ", new Vector2(910, 10), Color.White);
            spriteBatch.End();



            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
