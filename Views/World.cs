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

using System.IO;

namespace Eragonia_Demo_Day_One
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class World : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Mikey

        public enum gameState { StartUp, TitleScreen, Artifacts, PlayGame, Pause, Options, Config, QuitGame, ReallyQuit, LostGame, 
        WinGame, ShowInfo, Tutorial, Stats, Cutscene, Bossfight, DeathScene }
        public enum transistionState { Default, Transistioning };
        public gameState gS;
        public transistionState transistion;
        public MouseState oldM;
        public Rectangle[] buttonSprites;
        public Boolean motorizerAlive = false;
        public gameState prevGS;
        int time;

        Background bG;

        TipsAndTricks titleTips;

        public Title titleObj;
        public Options optObj;
        List<Button> optButton;
        Quit quitObj;
        List<Button> quitButtons;
        List<Button> artifactButtons = new List<Button>();
        public List<EnemySuper> totalEnemies = new List<EnemySuper>();
        public List<EnemySuper> enemies = new List<EnemySuper>();
        public List<EnemySuper> activeEnemies = new List<EnemySuper>();
        public List<Button> pauseButtons;
        public Pause pauseObj;

        public WaveLoader levelLoad = new WaveLoader();


        Loss losCond;

        public int breachedEnemiesAllowed = 0;
        public int bEAretry = 0;
        public Boolean lawnMowerAvaliable = false;

        public double waveMoneyMultiplier = 1;

        //Vivek
        public SelectionBar mainBar;
        public int highestWave = 1;
        public KeyboardState kb;
        Rectangle artifactRectangle;
        public int currentWave = 1;
        public Texture2D dummyTexture;
        public Tile[,] tileMap;
        Decoration[,] decoMap;
        Random rand = new Random();
        public ResourceBar bar;
        public Boolean canShowRange = true;
        ResourceTower temp2;
        Texture2D[] tempTextures;
        public List<AttackSuper> attackTowers = new List<AttackSuper>();
        public List<ResourceTower> resourceTowers = new List<ResourceTower>();
        List<ArtifactItem> artifacts = new List<ArtifactItem>();
        public Texture2D lossTex;
        public MusicManager music = new MusicManager();
        public SFXManager sfx = new SFXManager();
        public int completedTut = 0;
        public Boolean isPreparingForBoss = false;
        public Boss boss = new Boss();
        public Color bossOverlay = new Color(50, 30, 10, 0);
        SpriteFont font;
        public WaveButton waveButton = new WaveButton();
        public ScreenTransistioner transistioner = new ScreenTransistioner();
        Texture2D artifactTexture;
        Texture2D artifactTitle;
        Rectangle artTitlePos;
        public int prestige = 15000;
        Vector2 prestigePos;
        public double damageMultiplier = 1;
        public Boolean applyCrowbar = false;
        int enemyTimer;
        public int towerID = 0;
        public int shownRangeID = -1;
        public int prestigeMult = 1;
        public Boolean shouldRefundUpgrade = false;
        public Boolean upgrade1Unlocked = false;
        public Boolean upgrade2Unlocked = false;
        public double healthDivider = 1;
        public Boolean[] hasPlacedResourceTower = new Boolean[4];
        public Infobox shownInfoBox;
        public UpgradeButton shownUpgrade;
        public int rescoresUpgrade = 0;
        public int wmm = 0;
        public int bea = 0;
        public int dm = 0;
        public int uu = 0;
        public int hd = 0;
        public TierUpButton shownTier;
        public Boolean isTierTransistioning = false;
        public Rectangle tierTransPos;
        public Color tierTransCol = new Color(0, 0, 0, 255);
        public int tierTransHue = 0;
        public int era = 0;
        public Texture2D tierTransTex;
        public TowerLoader loader = new TowerLoader();
        public LifeHeart heart = new LifeHeart();
        public Slider volumeSlider = new Slider();
        public Slider sfxSlider = new Slider();
        public float maxVolume = 1;
        public float maxSFX = 1;
        public EnemyInfo shownInfo;
        public EnemyInfoManager infoManager = new EnemyInfoManager();
        public DialogueBox dialogue = new DialogueBox();
        public BossDialogue bossDialogue = new BossDialogue();
        public Boolean isFirstTimePlaying = true;
        public StatsManager stats = new StatsManager();
        public int totalDamage = 0;
        public int[] towerDamages = new int[5];
        public int gamePrestige = 0;
        public int[] resourcesGained = new int[4];
        public KeyboardState oldKb = Keyboard.GetState();
        public CheatManager cheat;
        public Boolean isCheating = false;
        public Texture2D[] additionalTileTextures = new Texture2D[6];
        public CutsceneManager cutscene = new CutsceneManager();
        public int bossTimer = -1;
        public Healthbar healthBar = new Healthbar();
        public ParticleEffects particles;
        int radius = 0;
        int lavaTimer = 0;
        public Boolean shouldReplaceLava = false;
        public Color winColor;
        public Texture2D winTexture;
        public BossPieces pieces;
        public int offsetX = 640;
        public int offsetY = 480;
        public Camera camera = new Camera();
        public CameraManager cameraManager = new CameraManager();
        public Warning warning = new Warning();
        //Paulo
        Texture2D[] bGt;
        Background cloud;
        Background bGcloud;
        Background birds;
        int bGtime;

        public Controls ctrl;
        List<Button> configButton;
        public Config configObj;
        

        public PauseInfoBox pauseinfobox;
        int cdTime;
        bool cooldown;
        public List<PauseButton> pauseButton;
        public WinScreen winCond;
        
        public World()
        {
            graphics = new GraphicsDeviceManager(this);
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

            //Mikey

            oldM = Mouse.GetState();

            gS = gameState.StartUp;
            

            buttonSprites = new Rectangle[] { new Rectangle(0, 0, 1000, 400), new Rectangle(1024, 0, 1000, 400), new Rectangle(0, 400, 1000, 400), new Rectangle(1024, 400, 1000, 400) };

            graphics.PreferredBackBufferHeight = 960;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ApplyChanges();

            time = 360;
            levelLoad.world = this;

            //Vivek

            tierTransPos = new Rectangle(-1280 + offsetX - 640, 0, 1280, 960);
            tileMap = new Tile[15, 40];
            decoMap = new Decoration[15, 40];
            for (int i = 0; i < 4; i++) {
                hasPlacedResourceTower[i] = false;
            }
            loader.world = this;
            // GraphicsDevice.BlendState = BlendState.Additive;

            tempTextures = new Texture2D[5];
            artifactRectangle = new Rectangle(0, 0, 1280, 960);

            //Pau pau
            bGt = new Texture2D[8];
            bGtime = 0;

            cooldown = false;
            cdTime = 0;

            ctrl = new Controls();
            kb = Keyboard.GetState();


            // this.Window.AllowUserResizing = true;

            IsMouseVisible = true;

            base.Initialize();
        }

        public void SpawnBoss() {
            boss = new Boss();
            boss.world = this;
            boss.Initialize();
            activeEnemies.Add(boss);
        }
        public Tile getTile(int mouseX, int mouseY)
        {
            int i = mouseX / 64;
            int j = mouseY / 64;

            if (i < tileMap.GetLength(1) && j < tileMap.GetLength(0) && i >= 0 && j >= 0) {

                return tileMap[j, i];
            }
            Tile temp = new Tile();
            temp.isPlaceable = false;
            return temp;
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Mikey
            dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { Color.White });

            titleTips = new TipsAndTricks(Color.White, this.Content.Load<SpriteFont>("TitleScreen/Tips/Font/TipsFont"));
            loadText(@"Content/TitleScreen/Tips/greatAdvice.txt");
            titleTips.changeTip();

            bG = new Background(this.Content.Load<Texture2D>("TitleScreen/Background/background2"), new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), new Rectangle(0, 0, 1960, 1470), Color.White);

            Button[] tB = new Button[4];
            for (int i = 0; i < 4; i++)
            {
                tB[i] = new Button(this.Content.Load<Texture2D>("TitleScreen/Buttons/ButtonsTry"), new Rectangle(480, 250 + (i * 150), 320, 100),
                 buttonSprites, Color.White, this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont"), true);
                tB[i].world = this;
            }

            titleObj = new Title(this.Content.Load<Texture2D>("TitleScreen/Title/TitleImage"), new Rectangle(290, 20, 800, 300), new Rectangle(0, 0, 2048, 782), Color.White, tB);
            titleObj.titleButtons[0].text = "Start";
            titleObj.titleButtons[1].text = "Resume";
            titleObj.titleButtons[2].text = "Options";
            titleObj.titleButtons[3].text = "Quit";
            titleObj.titleButtons[0].painNum = 345;
            titleObj.titleButtons[1].painNum = 290;
            titleObj.titleButtons[2].painNum = 225;
            titleObj.titleButtons[3].painNum = 405;

            Button[] qB = new Button[2];
            for (int i = 0; i < 2; i++)
            {
                qB[i] = new Button(this.Content.Load<Texture2D>("TitleScreen/Buttons/ButtonsTry"), new Rectangle(300 + (i * 400), 500, 320, 100), 
                buttonSprites, Color.White, this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont"), true);
                qB[i].world = this;
            }
            quitObj = new Quit(this.Content.Load<Texture2D>("TitleScreen/Quit/AYSYWTQ"), new Rectangle(150, 200, 960, 540), new Rectangle(0, 0, 960, 540), Color.White, qB);
            quitObj.quitButtons[0].text = "Quit";
            quitObj.quitButtons[1].text = "Cancel";
            quitObj.quitButtons[0].painNum = 223;
            quitObj.quitButtons[1].painNum = 510;

            Button[] oB = new Button[2];
            for (int i = 0; i < 2; i++)
            {
                oB[i] = new Button(this.Content.Load<Texture2D>("TitleScreen/Buttons/ButtonsTry"), new Rectangle(300 + (i * 400), 650, 320, 100), buttonSprites, 
                Color.White, this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont"), true);
                oB[i].world = this;
            }
            optObj = new Options(this.Content.Load<Texture2D>("TitleScreen/Options/Keyboard"), new Rectangle(150, 200, 936, 501), new Rectangle(0, 0, 624, 334), Color.White, oB);
            optObj.optButtons[0].text = "Config";
            optObj.optButtons[0].painNum = 100;
            optObj.optButtons[1].text = "Return";
            optObj.optButtons[1].painNum = 500;

            levelLoad.loadEnemies(@"Content/Levels/Levels.txt", totalEnemies);

            for (int i = 0; i < totalEnemies.Count; i++)
            {
                totalEnemies[i].healthSprite = this.Content.Load<Texture2D>("Enemy/EnemyHealthBar");
                totalEnemies[i].healthBar = new Rectangle(totalEnemies[i].position.X, totalEnemies[i].position.Y, totalEnemies[i].position.Width, 10);
                totalEnemies[i].dummyTexture = dummyTexture;
                totalEnemies[i].calculateOrigin();
            }

            losCond = new Loss(oldM, this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont"), new Button[] { new Button(this.Content.Load<Texture2D>("TitleScreen/Buttons/ButtonsTry"), new Rectangle(100, 800, 320, 100),
            buttonSprites, Color.White, this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont"), true),
            new Button(this.Content.Load<Texture2D>("TitleScreen/Buttons/ButtonsTry"), new Rectangle(860, 800, 320, 100), buttonSprites,
            Color.White, this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont"), true),
            new Button(this.Content.Load<Texture2D>("TitleScreen/Buttons/ButtonsTry"), new Rectangle(480, 800, 320, 100), buttonSprites,
            Color.White, this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont"), true) });
            losCond.lossButtons[0].text = "Retry";
            losCond.lossButtons[0].painNum = -32;
            losCond.lossButtons[0].world = this;
            losCond.lossButtons[1].text = "Quit";
            losCond.lossButtons[1].painNum = 785;
            losCond.lossButtons[1].world = this;
            losCond.lossButtons[2].text = "Stats";
            losCond.lossButtons[2].painNum = 344;
            losCond.lossButtons[2].world = this;


            Button[] pB = new Button[2];
            for (int i = 0; i < 2; i++)
            {
                pB[i] = new Button(this.Content.Load<Texture2D>("TitleScreen/Buttons/ButtonsTry"), new Rectangle(275 + (i * 400), 510, 320, 100), buttonSprites,
                Color.White, this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont"), true);
                pB[i].world = this;
            }
            pauseObj = new Pause(this.Content.Load<Texture2D>("TitleScreen/Options/Keyboard"), new Rectangle(150, 200, 936, 501), new Rectangle(0, 0, 624, 334), Color.White, pB);
            pauseObj.pauseButtons[0].text = "Return";
            pauseObj.pauseButtons[0].painNum = 83;
            pauseObj.pauseButtons[1].text = "Quit";
            pauseObj.pauseButtons[1].painNum = 600;


            //Vivek
            font = this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont");


            mainBar = new SelectionBar();
            mainBar.texture = this.Content.Load<Texture2D>("GUI/main_bar");
            mainBar.position = new Rectangle(256, 796, 1200, 164);
            mainBar.world = this;
            mainBar.resources = bar;
            mainBar.attackTowerTextures.Add(this.Content.Load<Texture2D>("GUI/Thumbnails/clubberThumb"));
            mainBar.attackTowerTextures.Add(this.Content.Load<Texture2D>("GUI/Thumbnails/bowerThumb"));
            mainBar.attackTowerTextures.Add(this.Content.Load<Texture2D>("GUI/Thumbnails/knifeThumb"));
            mainBar.attackTowerTextures.Add(this.Content.Load<Texture2D>("GUI/Thumbnails/torcherThumb"));
            mainBar.attackTowerTextures.Add(this.Content.Load<Texture2D>("GUI/Thumbnails/catapultThumb"));
            mainBar.attackTowerTextures.Add(this.Content.Load<Texture2D>("GUI/Thumbnails/teleThumb"));
            mainBar.resourceTowerTextures.Add(this.Content.Load<Texture2D>("GUI/Thumbnails/stonerThumb"));
            mainBar.resourceTowerTextures.Add(this.Content.Load<Texture2D>("GUI/Thumbnails/minerThumb"));
            mainBar.resourceTowerTextures.Add(this.Content.Load<Texture2D>("GUI/Thumbnails/prospectorThumb"));
            mainBar.dummyTexture = dummyTexture;
            mainBar.infoTexture = this.Content.Load<Texture2D>("GUI/infobox");
            mainBar.font = this.Content.Load<SpriteFont>("Other/Font2");
            tierTransTex = this.Content.Load<Texture2D>("GUI/tierTransistioner");

            mainBar.tierFont = this.Content.Load<SpriteFont>("Other/Font1");
            mainBar.tierUpTex = this.Content.Load<Texture2D>("GUI/tierUpButton");
            mainBar.Initialize();
            mainBar.buttons.texture = this.Content.Load<Texture2D>("GUI/barButtons");
            mainBar.buttons.position = new Rectangle(700, 796, 140, 26);
            tempTextures[1] = this.Content.Load<Texture2D>("GUI/pause");
            tempTextures[2] = this.Content.Load<Texture2D>("GUI/fast");

            waveButton.position = new Rectangle(1092, 0, 188, 128);
            waveButton.texture = this.Content.Load<Texture2D>("GUI/waveButton");
            waveButton.world = this;
            transistioner.texture = dummyTexture;

            
            artifactTexture = this.Content.Load<Texture2D>("GUI/artifact");
            tempTextures[4] = this.Content.Load<Texture2D>("Towers/torcher");
            bar = new ResourceBar(this.Content.Load<SpriteFont>("Other/Font1"));
            bar.texture = this.Content.Load<Texture2D>("GUI/resource_bar");
            bar.position = new Rectangle(0, 690, 192, 270);
            bar.updateResources(0, 200);
            bar.updateResources(1, 80);
            //bar.updateResources(2, 5000);
            //bar.updateResources(3, 5000);
            mainBar.resources = bar;
            loadTiles(@"Content/Other/map.txt");
            loadDecos(@"Content/Other/decorations.txt");


            artifactButtons.Add(new Button(this.Content.Load<Texture2D>("TitleScreen/Buttons/ButtonsTry"), new Rectangle(480, 760, 320, 100), buttonSprites, Color.White, this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont"), true));
            artifactButtons[0].text = "BEGIN";
            artifactButtons[0].world = this;
            artifactButtons[0].painNum = 345;
            optObj.font = font;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    loadArtifactItem(new Rectangle(i * 350 + 168, j * 200 + 300, 244, 168), i + j * 3);
                }
            }
            artifactTitle = this.Content.Load<Texture2D>("GUI/artifactTitle");
            artTitlePos = new Rectangle(80, -30, 1120, 240);
            Vector2 temp = font.MeasureString("Your Prestige: 1500");
            prestigePos = new Vector2(640 - temp.X / 2, 200);
            music.world = this;
            music.LoadSongs();
            
            sfx.world = this;
            sfx.Initialize();
            heart.world = this;
            heart.Initialize();


            volumeSlider.world = this;
            volumeSlider.barPos = new Rectangle(400, 200, 700, 80);
            volumeSlider.Initialize();
            
            sfxSlider.world = this;
            sfxSlider.barPos = new Rectangle(400, 400, 700, 80);
            sfxSlider.Initialize();
            sfxSlider.index = 1;

            infoManager.world = this;
            infoManager.Initialize();

            dialogue.world = this;
            dialogue.Initialize();

            bossDialogue.world = this;
            bossDialogue.Initialize();

            stats.world = this;
            stats.Initialize();
            stats.SetText(); //change
            stats.backButton = new Button(this.Content.Load<Texture2D>("TitleScreen/Buttons/ButtonsTry"), new Rectangle(480, 700, 320, 100), buttonSprites, Color.White, this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont"), true);
            stats.backButton.text = "Back";
            stats.backButton.painNum = 404;
            stats.backButton.world = this;
            lossTex = this.Content.Load<Texture2D>("Conditions/LossImage");

            cheat = new CheatManager();
            cheat.world = this;

            additionalTileTextures[0] = this.Content.Load<Texture2D>("Tiles/moatLava");
            additionalTileTextures[1] = this.Content.Load<Texture2D>("Tiles/stone");
            additionalTileTextures[2] = this.Content.Load<Texture2D>("Tiles/stoneCorner");
            additionalTileTextures[3] = this.Content.Load<Texture2D>("Tiles/pathBoss");
            additionalTileTextures[4] = this.Content.Load<Texture2D>("Tiles/lava");
            additionalTileTextures[5] = this.Content.Load<Texture2D>("Tiles/lava_top");

            healthBar.world = this;
            healthBar.Initialize();



            cutscene.world = this;
            cutscene.Initialize();
            particles = new ParticleEffects();
            particles.world = this;
            particles.Initialize();


            //SpawnBoss();

            //particles.isSpawningParticles = true;
            //shouldReplaceLava = true;

            winTexture = this.Content.Load<Texture2D>("Conditions/WinImage");

            camera.world = this;
            camera.viewport = graphics.GraphicsDevice.Viewport;
            cameraManager.world = this;

            warning.world = this;
            warning.Initialize();
            //warning.shouldDraw = true;
        
            //Paulo
            Button[] cB = new Button[6];
            
            for (int i = 0; i < 6; i++)
            {
                if (i == 3)
                {
                    cB[i] = new Button(this.Content.Load<Texture2D>("TitleScreen/Buttons/ButtonsTry"), new Rectangle(700, 70 + (i * 100), 320, 75),
                    buttonSprites,
                    Color.White, this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont"), true);
                }
                else
                {
                    cB[i] = new Button(this.Content.Load<Texture2D>("TitleScreen/Buttons/Config Button-1.png"), new Rectangle(700, 70 + (i * 100), 320, 75), new Rectangle[] { new Rectangle(0, 0, 400, 120), new Rectangle(0, 0, 400, 120), new Rectangle(0, 0, 400, 120) }, Color.White, this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/configFont"), true);
                }
            }
            configObj = new Config(new Rectangle(150, 200, 936, 501), new Rectangle(0, 0, 624, 334), Color.White, cB);
            configObj.configButtons[0].text = ctrl.controls["Pause"].ToString();
            configObj.configButtons[0].painNum = 968;
            configObj.configButtons[1].text = ctrl.controls["Cancel Tower"].ToString();
            configObj.configButtons[1].painNum = 968;
            configObj.configButtons[2].text = ctrl.controls["Place Tower"].ToString();
            configObj.configButtons[2].painNum = 968;
            configObj.configButtons[3].rec = new Rectangle(480, 630, 320, 100);
            configObj.configButtons[3].world = this;
            configObj.configButtons[3].text = "Return";
            configObj.configButtons[3].forceClickSound = true;
            configObj.Center("Return", 3);
           

            configObj.configButtons[4].text = ctrl.controls["Move Left"].ToString();
            configObj.configButtons[4].rec.Y -= 100;
            configObj.configButtons[5].text = ctrl.controls["Move Right"].ToString();
            configObj.configButtons[5].rec.Y -= 100;

            for (int i = 0; i < 8; i++)
            {
                bGt[i] = this.Content.Load<Texture2D>("TitleScreen/Background/bg" + i);
            }
            birds = new Background(this.Content.Load<Texture2D>("TitleScreen/Background/bird boiz"), new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), new Rectangle(0, 0, 1960, 1470), Color.White);
            bG = new Background(bGt[0], new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), new Rectangle(0, 0, 1960, 1470), Color.White);
            cloud = new Background(this.Content.Load<Texture2D>("TitleScreen/Background/cloudy boi v2"), new Rectangle(bG.rec.Width, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), new Rectangle(0, 0, 1960, 1470), Color.White);
            bGcloud = new Background(this.Content.Load<Texture2D>("TitleScreen/Background/cloudy boi v2"), new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), new Rectangle(0, 0, 1960, 1470), Color.White);

            pauseButton = new List<PauseButton>();
            for (int i = 0; i < 2; i++)
            {
                pauseButton.Add(new PauseButton(this.Content.Load<Texture2D>("GUI/pause"), new Rectangle(24, 24, 64, 64), Color.White));
            }
            pauseinfobox = new PauseInfoBox(this.Content.Load<Texture2D>("GUI/pause info box"), new Rectangle(240, 240, 800, 300), "GAME PAUSED", this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont"));
            pauseinfobox.painNum = -10;

            winCond = new WinScreen(oldM, this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont"), new Button[] { new Button(this.Content.Load<Texture2D>("TitleScreen/Buttons/ButtonsTry"), new Rectangle(100, 800, 320, 100),
            buttonSprites, Color.White, this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont"), true),
            new Button(this.Content.Load<Texture2D>("TitleScreen/Buttons/ButtonsTry"), new Rectangle(860, 800, 320, 100), buttonSprites,
            Color.White, this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont"), true),
            new Button(this.Content.Load<Texture2D>("TitleScreen/Buttons/ButtonsTry"), new Rectangle(480, 800, 320, 100), buttonSprites,
            Color.White, this.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont"), true) });
            winCond.winButtons[0].text = "Retry";
            winCond.winButtons[0].painNum = -32;
            winCond.winButtons[0].world = this;
            winCond.winButtons[1].text = "Quit";
            winCond.winButtons[1].painNum = 785;
            winCond.winButtons[1].world = this;
            winCond.winButtons[2].text = "Stats";
            winCond.winButtons[2].painNum = 344;
            winCond.winButtons[2].world = this;
            winCond.world = this;
            winCond.Initialize();
            music.PlaySong(0);
            gameWillAppear(@"Content/saveData.txt");
            // TODO: use this.Content to load your game content here
        }
        public void gameWillAppear(String fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                try
                {
                    int i = 0;
                    while (!reader.EndOfStream)
                    {
                        String[] temp = reader.ReadLine().Split(' ');
                        String s = temp[0];
                        String s1 = temp[1];

                       
                        if (s.Equals("tut"))
                        {
                            int t = int.Parse(s1);
                            if (t == 1)
                            {
                                isFirstTimePlaying = false;
                            }
                            else
                            {
                                isFirstTimePlaying = true;
                            }
                            
                        }
                        if (s.Equals("pause"))
                        {
                            Keys tempKey = (Keys)Enum.Parse(typeof(Keys), s1);
                            if (tempKey == Keys.OemComma)
                            {
                                configObj.configButtons[0].text = ",";
                            }
                            else if (tempKey == Keys.OemPeriod)
                            {
                                configObj.configButtons[0].text = ".";
                            }
                            else
                            {
                                configObj.configButtons[0].text = s1;
                            }
                            ctrl.controls["Pause"] = tempKey;
                            
                        }
                        if (s.Equals("cancel"))
                        {
                            Keys tempKey = (Keys)Enum.Parse(typeof(Keys), s1);
                            if (tempKey == Keys.OemComma)
                            {
                                configObj.configButtons[1].text = ",";
                            }
                            else if (tempKey == Keys.OemPeriod)
                            {
                                configObj.configButtons[1].text = ".";
                            }
                            else
                            {
                                configObj.configButtons[1].text = s1;
                            }
                            ctrl.controls["Cancel Tower"] = tempKey;

                        }
                        if (s.Equals("place"))
                        {
                            Keys tempKey = (Keys)Enum.Parse(typeof(Keys), s1);
                            if (tempKey == Keys.OemComma)
                            {
                                configObj.configButtons[2].text = ",";
                            }
                            else if (tempKey == Keys.OemPeriod)
                            {
                                configObj.configButtons[2].text = ".";
                            }
                            else
                            {
                                configObj.configButtons[2].text = s1;
                            }
                            ctrl.controls["Place Tower"] = tempKey;

                        }

                        if (s.Equals("left"))
                        {
                            Keys tempKey = (Keys)Enum.Parse(typeof(Keys), s1);
                            if (tempKey == Keys.OemComma)
                            {
                                configObj.configButtons[4].text = ",";
                            }
                            else if (tempKey == Keys.OemPeriod)
                            {
                                configObj.configButtons[4].text = ".";
                            }
                            else
                            {
                                configObj.configButtons[4].text = s1;
                            }
                            ctrl.controls["Move Left"] = tempKey;
                        }
                        if (s.Equals("right"))
                        {
                            Keys tempKey = (Keys)Enum.Parse(typeof(Keys), s1);
                            if (tempKey == Keys.OemComma)
                            {
                                configObj.configButtons[5].text = ",";
                            }
                            else if (tempKey == Keys.OemPeriod)
                            {
                                configObj.configButtons[5].text = ".";
                            }
                            else
                            {
                                configObj.configButtons[5].text = s1;
                            }
                            ctrl.controls["Move Right"] = tempKey;
                        }
                        if (s.Equals("wave"))
                        {
                            int x = int.Parse(s1);
                            highestWave = x;
                        }
                        i++;
                    }

                }
                catch
                {
                    Console.WriteLine("???");
                }
            }
        }
        public void loadInGame(String fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                try
                {
                    int i = 0;
                    while (!reader.EndOfStream)
                    {
                        String[] temp = reader.ReadLine().Split(' ');
                        String s = temp[0];
                        String s1 = temp[1];

                        if (s.Equals("bEA"))
                        {
                            int be = int.Parse(s1);
                            for(int b = 0; b < be; b++)
                            {
                                artifacts[1].incrementArtifact();
                            }
                        }
                        if (s.Equals("wMM"))
                        {
                            int wm = int.Parse(s1);
                            for (int b = 0; b < wm; b++)
                            {
                                artifacts[5].incrementArtifact();
                            }
                        } 
                        if (s.Equals("rU"))
                        {
                            int r = int.Parse(s1);
                            for (int b = 0; b < r; b++)
                            {
                                artifacts[3].incrementArtifact();
                            }
                        }
                        if (s.Equals("dM"))
                        {
                            int d = int.Parse(s1);
                            for (int b = 0; b < d; b++)
                            {
                                artifacts[0].incrementArtifact();
                            }
                        }
                        if (s.Equals("uu"))
                        {
                            int u = int.Parse(s1);
                            for (int b = 0; b < u; b++)
                            {
                                artifacts[2].incrementArtifact();
                            }
                        }
                        if (s.Equals("hD"))
                        {
                            int h = int.Parse(s1);
                            for (int b = 0; b < h; b++)
                            {
                                artifacts[4].incrementArtifact();
                            }
                        }
                    
                        i++;
                    }

                }
                catch
                {
                    Console.WriteLine("?");
                }
            }
        }

        public void baseLoad(String fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                try
                {
                    writer.WriteLine("");
                }
                catch
                {
                    
                }
            }
        }

        public void save(String fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                try
                {
                    writer.WriteLine("pP " + prestige);
                    writer.WriteLine("dM " + dm);
                    writer.WriteLine("bEA " + bea);
                    writer.WriteLine("wMM " + wmm); 
                    writer.WriteLine("uu " + uu);
                    writer.WriteLine("rU " + rescoresUpgrade);
                    writer.WriteLine("hD " + hd);
                    writer.WriteLine("tut " + completedTut);
                    writer.WriteLine("pause " + ctrl.controls["Pause"].ToString());
                    writer.WriteLine("cancel " + ctrl.controls["Cancel Tower"].ToString());
                    writer.WriteLine("place " + ctrl.controls["Place Tower"].ToString());
                    writer.WriteLine("left " + ctrl.controls["Move Left"].ToString());
                    writer.WriteLine("right " + ctrl.controls["Move Right"].ToString());
                    writer.WriteLine("wave" + highestWave);
                    writer.Dispose();
                    //Console.WriteLine("HIII");
                }
                catch
                {
                   
                }
            }
        }

        public void Cheat() {
            bar.resources[0] = 9000;
            bar.resources[1] = 6000;
            bar.resources[2] = 3000;
            bar.resources[3] = 2000;
            currentWave = 39;
            isCheating = true;
            levelLoad.enemiesPerWave = new List<int>();
            levelLoad.spawnT = new List<int>();
            for (int i = totalEnemies.Count - 1; i >= 0; i--)
            {
                totalEnemies.RemoveAt(i);
            }

            for (int i = activeEnemies.Count - 1; i >= 0; i--)
            {
                activeEnemies.RemoveAt(i);
            }

            levelLoad.loadEnemies(@"Content/Levels/cheatLevel.txt", totalEnemies);
            for (int i = 0; i < totalEnemies.Count; i++)
            {
                totalEnemies[i].healthSprite = this.Content.Load<Texture2D>("Enemy/EnemyHealthBar");
                totalEnemies[i].healthBar = new Rectangle(totalEnemies[i].position.X, totalEnemies[i].position.Y, totalEnemies[i].position.Width, 10);
                totalEnemies[i].dummyTexture = dummyTexture;
                totalEnemies[i].calculateOrigin();
            }
        }
        public void Cheat2()
        {
            isPreparingForBoss = true;
            waveButton.permaDisabled = true;
            bar.resources[0] += 200;
            bar.resources[1] += 200;
           
        }
        public void loadEnemy()
        {
            activeEnemies.Add(enemies[0]);
            enemies[0].calculateOrigin();
            enemies.Remove(enemies[0]);  
        }

        public void loadArtifactItem(Rectangle pos, int index)
        {
            ArtifactItem temp = new ArtifactItem();
            temp.texture = artifactTexture;
            temp.position = pos;
            temp.artifactID = index;
            temp.world = this;
            temp.font = this.Content.Load<SpriteFont>("Other/Font2");
            String[] texts = new String[3];
            if (index == 0)
            {
                texts[0] = "Increase tower damage. 50 Prestige";
                texts[1] = "Further increase tower damage. 250 Prestige";
                texts[2] = "The first attack on an enemy deals extra damage. 1K Prestige";
                temp.prestigeCosts[0] = 50;
                temp.prestigeCosts[1] = 250;
                temp.prestigeCosts[2] = 1000;
            }
            else if(index == 1) 
            {
                texts[0] = "Unlock level 1 tower upgrades. 50 Prestige";
                texts[1] = "Unlock level 2 tower upgrades. 250 Prestige";
                texts[2] = "Partially refund upgrade costs upon tiering up. 600 Prestige";
                temp.prestigeCosts[0] = 50;
                temp.prestigeCosts[1] = 250;
                temp.prestigeCosts[2] = 600;
            }
            else if(index == 2)
            {
                texts[0] = "Starting health of enemies is lower. 50 Prestige";
                texts[1] = "Starting health of enemies further decreases. 150 Prestige";
                texts[2] = "Starting health of enemies is even lower. 350 Prestige";
                temp.prestigeCosts[0] = 50;
                temp.prestigeCosts[1] = 150;
                temp.prestigeCosts[2] = 350;
            }
            else if (index == 3)
            {
                texts[0] = "Allows one extra enemy to breach the gates. 100 Prestige";
                texts[1] = "Allows one more enemy to breach the gates. 300 Prestige";
                texts[2] = "Kill every enemy on screen upon losing a life. 500 Prestige";
                temp.prestigeCosts[0] = 100;
                temp.prestigeCosts[1] = 300;
                temp.prestigeCosts[2] = 500;
            }
            else if (index == 4)
            {
                texts[0] = "Starting resourses increase. 50 Prestige";
                texts[1] = "Starting resourses increase further. 250 Prestige";
                texts[2] = "Start the game with a free, weaker digger. 1.5K Prestige";
                temp.prestigeCosts[0] = 50;
                temp.prestigeCosts[1] = 250;
                temp.prestigeCosts[2] = 1500;
            }
            else if (index == 5)
            {
                texts[0] = "Earn resourses after wave completion. 50 Prestige";
                texts[1] = "Wave end resourses are increased. 250 Prestige";
                texts[2] = "Wave end benefits are further increased. 1K Prestige";
                temp.prestigeCosts[0] = 50;
                temp.prestigeCosts[1] = 250;
                temp.prestigeCosts[2] = 1000;
            }
            else
            {
                texts[0] = "placeholder";
                texts[1] = "placeholder";
                texts[2] = "placeholder";
                temp.prestigeCosts[0] = 0;
                temp.prestigeCosts[1] = 0;
                temp.prestigeCosts[2] = 0;

            }
            temp.barTexture = dummyTexture;
            temp.setTexts(texts);
            temp.setSources();
            artifacts.Add(temp);
        }

        public void callTierTransistion()
        {
            isTierTransistioning = true;
            tierTransPos.X = -1280 + offsetX - 640;
        }

        public void loadTiles(String fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                try
                {
                    int i = 0;
                    while (!reader.EndOfStream)
                    {

                        String s = reader.ReadLine();
                        Char[] chars = s.ToCharArray();

                        for (int j = 0; j < chars.Length; j++)
                        {

                            Tile temp = new Tile();
                            if (chars[j] == 'g')
                            {

                                temp.color = Color.White;
                                temp.isPath = false;
                                temp.isPlaceable = true;
                                temp.texture = this.Content.Load<Texture2D>("Tiles/grass");
                                int a = rand.Next(0, 100);
                                temp.setSource(a);
                            }
                            else if (chars[j] == 'p')
                            {
                                temp.color = Color.White;
                                temp.isPath = true;
                                temp.texture = this.Content.Load<Texture2D>("Tiles/path");
                                int a = rand.Next(5, 100);
                                temp.setSource(a);
                            }
                            else if (chars[j] == 'v')
                            {
                                temp.color = Color.White;
                                temp.isPath = true;
                                temp.texture = this.Content.Load<Texture2D>("Tiles/path");
                                int a = rand.Next(5, 100);
                                temp.setSource(a);
                                temp.rotation = 270;
                                
                            }
                            else if (chars[j] == '1')
                            {
                                temp.color = Color.White;
                                temp.isPath = true;
                                temp.texture = this.Content.Load<Texture2D>("Tiles/corner");
                                temp.source = new Rectangle(0, 0, 1024, 1024);
                                temp.rotation = 0;
                                temp.isCorner = true;
                            }
                            else if (chars[j] == '2')
                            {
                                temp.color = Color.White;
                                temp.isPath = true;
                                temp.texture = this.Content.Load<Texture2D>("Tiles/corner");
                                temp.source = new Rectangle(0, 0, 1024, 1024);
                                temp.rotation = 90;
                                temp.isCorner = true;
                                //temp.flip = SpriteEffects.FlipHorizontally;

                            }
                            else if (chars[j] == '3')
                            {
                                temp.color = Color.White;
                                temp.isPath = true;
                                temp.texture = this.Content.Load<Texture2D>("Tiles/corner");
                                temp.source = new Rectangle(0, 0, 1024, 1024);
                                temp.rotation = 270;
                                temp.isCorner = true;

                            }
                            else if (chars[j] == '4')
                            {
                                temp.color = Color.White;
                                temp.isPath = true;
                                temp.texture = this.Content.Load<Texture2D>("Tiles/corner");
                                temp.source = new Rectangle(0, 0, 1024, 1024);
                                temp.rotation = 180;
                                temp.isCorner = true;
                                //temp.flip = SpriteEffects.FlipHorizontally;

                            }
                            else if (chars[j] == 'w')
                            {
                                temp.color = Color.White;
                                temp.isPath = false;
                                temp.texture = this.Content.Load<Texture2D>("Tiles/water");
                                temp.source = new Rectangle(0, 0, 1024, 1024);
                                temp.isWater = true;
                                temp.rotation = 0;
                            }
                            else if (chars[j] == 't')
                            {
                                temp.color = Color.White;
                                temp.isPath = false;
                                temp.texture = this.Content.Load<Texture2D>("Tiles/water_top");
                                temp.source = new Rectangle(0, 0, 1024, 1024);
                                temp.isWater = true;
                                temp.isWaterTop = true;
                                temp.rotation = 0;
                            }
                            else if (chars[j] == 'm') {
                                temp.color = Color.White;
                                temp.isPath = true;
                                temp.texture = this.Content.Load<Texture2D>("Tiles/moat");
                                temp.source = new Rectangle(0, 0, 1024, 1024);
                                temp.rotation = 0;
                                temp.isMoat = true;
                            }
                            temp.position = new Rectangle(j * 64 + 32, i * 64 + 32, 64, 64);
                            temp.dummyTexture = dummyTexture;
                            temp.world = this;
                            tileMap[i, j] = temp;
                        }
                        i++;

                    }


                }
                catch
                {

                }
            }
        }

        public void loadDecos(String fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                try
                {
                    int i = 0;
                    while (!reader.EndOfStream)
                    {

                        String s = reader.ReadLine();
                        Char[] chars = s.ToCharArray();

                        for (int j = 0; j < chars.Length; j++)
                        {

                            Decoration temp = new Decoration();
                            if (chars[j] == 'l')
                            {
                                temp.source = new Rectangle(0, 0, 512, 512);
                            }
                            else if (chars[j] == 't')
                            {
                                temp.source = new Rectangle(512, 0, 512, 512);
                            }
                            else if (chars[j] == 's')
                            {
                                temp.source = new Rectangle(0, 512, 512, 512);
                            }
                            if (chars[j] != '.')
                            {
                                tileMap[i, j].hasDecoration = true;
                                tileMap[i, j].decoration = temp;
                            }
                            temp.position = new Rectangle(j * 64 + 32, i * 64 + 32, 64, 64);
                            temp.texture = this.Content.Load<Texture2D>("Tiles/decorations");

                            decoMap[i, j] = temp;
                        }
                        i++;

                    }


                }
                catch
                {
                    Console.WriteLine("ERROR");
                }
            }
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        /// 

        public void loadText(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while(!reader.EndOfStream){
                        string line = reader.ReadLine();
                        titleTips.text.Add(line);
                        
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Hey it broke fix -->" + e);
            }
        }
       
        public void retry()
        {
            gS = gameState.TitleScreen;
            cameraManager.resetOffsets();
            particles.isSpawningParticles = false;
            particles.particles = new List<Particle>();

            //Vivek
            cheat.currentInputs = new List<Keys>();
            cheat.hasCheated = false;
            music.PlaySong(0);
            isCheating = false;
            loadTiles(@"Content/Other/map.txt");
            loadDecos(@"Content/Other/decorations.txt");
           
            bar.resources[0] = 200;
            bar.resources[1] = 80;
            bar.resources[2] = 0;
            bar.resources[3] = 0;
            currentWave = 1;
            infoManager.currentIndex = 0;
            shownUpgrade = null;
            shownInfoBox = null;
            bossDialogue.currentTextIndex = 0;
            breachedEnemiesAllowed = bEAretry;
            mainBar.era = 0;
            waveButton.permaDisabled = false;
            resourcesGained = new int[4];
            totalDamage = 0;
            waveButton.current = WaveButton.Selected.Default;
            prestige += gamePrestige;
            gamePrestige = 0;
            for (int i = totalEnemies.Count - 1; i >= 0; i--)
            {
                totalEnemies.RemoveAt(i);
            }

            for (int i = activeEnemies.Count - 1; i >= 0; i--)
            {
                activeEnemies.RemoveAt(i);
            }
            for (int i = resourceTowers.Count - 1; i >= 0; i--)
            {
                resourceTowers.RemoveAt(i);
            }
            for (int i = attackTowers.Count - 1; i >= 0; i--)
            {
                attackTowers.RemoveAt(i);
            }
            heart.currentIndex = 0;
            towerDamages = new int[5];
            losCond.lossButtons[0].col.A = 0;
            losCond.lossButtons[1].col.A = 0;
            losCond.lossButtons[2].col.A = 0;
            losCond.color.A = 0;
            levelLoad.loadEnemies(@"Content/Levels/Levels.txt", totalEnemies);
            for (int i = 0; i < totalEnemies.Count; i++)
            {
                totalEnemies[i].healthSprite = this.Content.Load<Texture2D>("Enemy/EnemyHealthBar");
                totalEnemies[i].healthBar = new Rectangle(totalEnemies[i].position.X, totalEnemies[i].position.Y, totalEnemies[i].position.Width, 10);
                totalEnemies[i].dummyTexture = dummyTexture;
                totalEnemies[i].calculateOrigin();
            }
            hasPlacedResourceTower[0] = false;
            hasPlacedResourceTower[1] = false;
            hasPlacedResourceTower[2] = false;
            shownUpgrade = null;
            shownTier = null;

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void spendPrestige(int points)
        {
            prestige -= points;
            String s = "Your Prestige: " + prestige;
            Vector2 temp = font.MeasureString(s);
            prestigePos.X = 640 - temp.X / 2;
        }
        public void applyArtifact(int index, int level)
        {
            if (index == 0)
            {
                if (level == 1)
                {
                    damageMultiplier = 1.1;
                    dm = 1;
                }
                if (level == 2)
                {
                    damageMultiplier = 1.2;
                    dm = 2;
                }
                if (level == 3)
                {
                    applyCrowbar = true;
                    dm = 3;
                }
            }
            if(index == 1) {
                if (level == 1)
                {
                    upgrade1Unlocked = true;
                    uu = 1;
                }
                if (level == 2)
                {
                    upgrade2Unlocked = true;
                    uu = 2;
                }
                if (level == 3)
                {
                    shouldRefundUpgrade = true;
                    uu = 3;
                }
            }
            if (index == 2)
            {
                if (level == 1)
                {
                    healthDivider = .97;
                    hd = 1;
                }
                if (level == 2)
                {
                    healthDivider = .93;
                    hd = 2;
                }
                if (level == 3)
                {
                    healthDivider = .90;
                    hd = 3;
                }
            }
            if (index == 3)
            {
                if (level == 1)
                {
                    breachedEnemiesAllowed = 1;
                    bEAretry = 1;
                    heart.currentIndex = 0;
                    bea = 1;
                }
                if (level == 2)
                {
                    breachedEnemiesAllowed = 2;
                    heart.currentIndex = 0;
                    bEAretry = 2;
                    bea = 2;
                }
                if (level == 3)
                {
                    lawnMowerAvaliable = true;
                    heart.currentIndex = 0;
                    bea = 3;
                }
                heart.showHeart = true;
            }
            if (index == 4)
            {
                if (level == 1)
                {
                    bar.updateResources(0, 50);
                    bar.updateResources(1, 20);
                    rescoresUpgrade = 1;
                }
                if (level == 2)
                {
                    bar.updateResources(0, 50);
                    bar.updateResources(1, 20);
                    rescoresUpgrade = 2;
                }
                if (level == 3)
                {
                    loader.placeTower(10, 1152, 640);
                    
                    rescoresUpgrade = 3;
                }
            }
            if (index == 5)
            {
                if (level == 1)
                {
                    waveMoneyMultiplier = 1.1;
                    wmm = 1;
                }
                if (level == 2)
                {
                    waveMoneyMultiplier = 1.3;
                    wmm = 2;
                }
                if (level == 3)
                {
                    waveMoneyMultiplier = 1.5;
                    wmm = 3;
                }
            }
            if(level == 1) {
                sfx.PlaySoundWithStop("artifact1");
            }
            if (level == 2)
            {
                sfx.PlaySoundWithStop("artifact2");
            }
            if (level == 3)
            {
                sfx.PlaySoundWithStop("artifact3");
            }


        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                save(@"Content/saveData.txt");
                this.Exit();
            }

             MouseState mouse = Mouse.GetState();
            kb = Keyboard.GetState();

            losCond.m = mouse;
            losCond.world = this;
            winCond.m = mouse;
            bossTimer--;
            titleObj.mouse = mouse;
            titleObj.world = this;
            optObj.mouse = mouse;
            optObj.world = this;
            quitObj.mouse = mouse;
            quitObj.world = this;
            configObj.mouse = mouse;
            configObj.world = this;
            pauseObj.mouse = mouse;
            pauseObj.world = this;

            time--;
            music.Update();
            transistioner.Update();
             

            if(transistion == transistionState.Transistioning) {
                transistioner.Update();
                if(transistioner.color.A >= 250 && transistioner.turnBlack) {
                    transistioner.turnBlack = false;
                    gS = transistioner.state;
                    transistioner.turnWhite = true;
                }
                if(transistioner.color.A <= 5 && transistioner.turnWhite) {
                    transistion = transistionState.Default;
                    transistioner.color.A = 0;
                    transistioner.turnWhite = false;
                }
                if(transistioner.color.A >= 240 && transistioner.flashWhite) {
                    transistioner.flashWhite = false;
                    shouldReplaceLava = true;
                    transistioner.flashBlack = true;
                    SpawnBoss();
                    particles.isSpawningParticles = true;
                    gS = transistioner.state;
                }
                if(transistioner.color.A == 0 && transistioner.flashBlack) {
                    transistioner.flashBlack = false;
                    transistion = transistionState.Default;
                }
            }

            if (isFirstTimePlaying)
            {
                completedTut = 0;
            }
            else
            {
                completedTut = 1;
            }
            
            if(transistion == transistionState.Default) 
            {
                if(bossTimer == 0) {
                    transistioner.flashWhite = true;
                    bossTimer--;
                    
                    transistioner.state = gameState.Bossfight;
                    music.TransistionSong(3, true);
                    transistion = transistionState.Transistioning;
                }
                if(bossTimer == 120) {
                    sfx.PlaySound("thunder");
                }
                //Mikey
                if (gS == gameState.StartUp)
                {
                    titleObj.DoStart();
                }
                if (gS == gameState.TitleScreen)
                {
                    titleObj.Update();
                }

                if (gS == gameState.Options)
                {
                    optObj.Update();
                    configObj.Update();
                    volumeSlider.isClicking(mouse);
                    sfxSlider.isClicking(mouse);
                    
                }
                if (gS == gameState.Pause)
                {
                    pauseObj.Update();
                }

                if (gS == gameState.Config)
                {
                    configObj.Update();
                    configObj.configButtons[3].isOverChoice(mouse.X, mouse.Y, mouse, oldM);
                    
                    if (ctrl.pressed == true)
                    {
                        ctrl.Update(kb, oldKb);
                       
                    }
                }

                if (gS == gameState.QuitGame)
                {
                    quitObj.Update();
                }
                if ((gS == gameState.TitleScreen || gS == gameState.Options || gS == gameState.QuitGame || gS == gameState.ReallyQuit) && time == 0)
                {
                    titleTips.changeTip();
                    if (time == 0)
                        time = 360;

                }

                if (gS == gameState.Artifacts)
                {
                    artifactButtons[0].isOverChoice(mouse.X, mouse.Y, mouse, oldM);
                    artifactButtons[0].whatButton();
                    if (artifactButtons[0].bp == Button.buttonPress.pressed)
                    {
                        if (!isFirstTimePlaying)
                        {
                            transistioner.state = gameState.PlayGame;
                        }
                        else {
                            isFirstTimePlaying = false;
                            transistioner.state = gameState.Tutorial;
                        }
                        transistioner.turnBlack = true;
                        music.TransistionSong(1, true);
                        transistion = transistionState.Transistioning;
                    }
                    for (int i = 0; i < artifacts.Count; i++)
                    {
                        artifacts[i].isClicking(mouse);
                    }
                }


                if (gS == gameState.PlayGame || gS == gameState.Bossfight)
                {
                    particles.Update();



                    enemyTimer--;
                    if (activeEnemies.Count == 0 && waveButton.current == WaveButton.Selected.Disabled && enemies.Count == 0 && !waveButton.permaDisabled) {
                        if(currentWave == 42) {
                            gS = gameState.WinGame;
                            stats.isLoss = false;
                        }
                        
                        if (waveMoneyMultiplier != 1)
                        {
                            bar.updateResources(0, 25 * waveMoneyMultiplier);
                        }
                        for (int i = 0; i < resourceTowers.Count; i++)
                        {
                            resourceTowers[i].currentIndex = 0;
                        }
                        waveButton.current = WaveButton.Selected.Default;
                    }
                    if (isPreparingForBoss)
                    {

                        waveButton.current = WaveButton.Selected.Disabled;
                        bossDialogue.IsClicking(mouse);
                        
                    }

                    if (enemyTimer <= 0 && enemies.Count != 0)
                    {
                        if (!isPreparingForBoss)
                        {
                            loadEnemy();
                        }
                        if (isCheating)
                        {
                            enemyTimer = 350;
                            infoManager.showInfo(14 - enemies.Count);
                        }
                        else
                        {
                            enemyTimer = levelLoad.spawnT[currentWave - 2];
                        }
                    }
                    motorizerAlive = false;
                    for (int i = 0; i < activeEnemies.Count; i++)
                    {

                       
                        activeEnemies[i].Update();
                        if (motorizerAlive)
                        {
                            double pixMoveGoFast = 130;

                            if (activeEnemies[i].pixelsMoved >= pixMoveGoFast && !activeEnemies[i].shouldMotor && !activeEnemies[i].isSped)
                            {
                                activeEnemies[i].speed++;
                                activeEnemies[i].isSped = true;
                            }
                        }

                        activeEnemies[i].pathing();
                        if (activeEnemies[i].markForDeletion) {
                            bar.updateResources(0, activeEnemies[i].goldWorth);
                            resourcesGained[0] += activeEnemies[i].goldWorth;
                            activeEnemies.Remove(activeEnemies[i]);
                        }
                    }

                    cheat.Update(kb, oldKb);
                }

                if (gS == gameState.LostGame)
                {
                    losCond.Update();
                    stats.isLoss = true;
                    if (losCond.color.A < 250)
                    {
                        losCond.raiseColor();
                    }
                    else
                    {
                        for (int i = 0; i < losCond.lossButtons.Count(); i++)
                        {
                            if (losCond.lossButtons[i].col.A < 250)
                                losCond.lossButtons[i].raiseColor();
                        }
                    }
                    
                }
                if (gS == gameState.WinGame)
                {
                    stats.isLoss = false;
                    
                    if(winColor.A >= 1) {
                        winColor.A--;
                        return;
                    }
                    winCond.Update();
                    if (winCond.color.A < 250)
                    {
                        winCond.raiseColor();
                    }
                    else if(winCond.winButtons[0].col.A < 250)
                    {
                        for (int i = 0; i < losCond.lossButtons.Count(); i++)
                        {
                            if (winCond.winButtons[i].col.A < 250)
                                winCond.winButtons[i].raiseColor();
                        }
                    }

                }

                //Vivek
                if (gS == gameState.ShowInfo) {
                    shownInfo.isClicking(mouse);
                    if(shownInfo.button.bp == Button.buttonPress.pressed) {
                        gS = gameState.PlayGame;
                    }
                }
                if(gS == gameState.Tutorial) {
                    dialogue.IsClicking(mouse);
                }
                if(gS == gameState.Tutorial || gS == gameState.PlayGame || gS == gameState.Bossfight) {
                    mainBar.isClicking(mouse, kb);
                    if (loader.spawner != null)
                    {
                        loader.spawner.Update(mouse);
                    }
                    for (int i = 0; i < tileMap.GetLength(0); i++)
                    {
                        for (int j = 0; j < tileMap.GetLength(1); j++)
                        {
                            //Console.WriteLine(i + " " + j);
                            tileMap[i, j].isBeingHovered(mouse);
                        }
                    }
                }
                if (gS == gameState.PlayGame || gS == gameState.Bossfight || gS == gameState.Tutorial || gS == gameState.ShowInfo || gS == gameState.Pause) {
                    cameraManager.IsClicking(mouse, kb);
                   
                    camera.Update(new Vector2(offsetX, offsetY));
                    artifactRectangle.X = offsetX - 640;
                }
                else
                {
                    offsetX = 640;
                    artifactRectangle.X = 0;
                }
                if (gS == gameState.PlayGame || gS == gameState.Bossfight)
                {
                    if(warning.shouldDraw) {
                        warning.Update();
                    }
                    
                    for (int i = 0; i < attackTowers.Count; i++)
                    {
                        attackTowers[i].Update();
                        attackTowers[i].isBeingClicked(mouse);

                    }
                    for (int i = 0; i < resourceTowers.Count; i++)
                    {
                        resourceTowers[i].isClicking(mouse);
                        if (waveButton.current == WaveButton.Selected.Disabled) {

                            resourceTowers[i].UpdateResources();
                            resourceTowers[i].Update();
                        }

                    }

                    if (waveButton.current != WaveButton.Selected.Disabled)
                    {
                        waveButton.isOverChoice(mouse.X, mouse.Y, mouse);
                    }
                    if (waveButton.current == WaveButton.Selected.Pressed)
                    {
                        loadNextWave(currentWave);
                        
                        currentWave++;
                        if(currentWave > highestWave)
                        {
                            highestWave = currentWave;
                        }
                        if (currentWave == 43) //change
                        {
                            isPreparingForBoss = true;
                            waveButton.permaDisabled = true;
                        }
                        waveButton.current = WaveButton.Selected.Disabled;
                    }
                    
                    Rectangle r = new Rectangle(mouse.X, mouse.Y, 1, 1);



                    Vector2 b = new Vector2(mouse.X, mouse.Y);






                    warning.shouldDraw = false;
                    for (int i = 0; i < activeEnemies.Count; i++)
                    {
                        if( activeEnemies[i].position.X >= 2200 && offsetX <= 1800 && activeEnemies[i].canEndGame) {
                            warning.shouldDraw = true;
                        }
                        if (activeEnemies[i].position.X >= 2560 && activeEnemies[i].canEndGame)
                        {
                            if (lawnMowerAvaliable)
                            {
                                Order66();
                                tierTransCol.R = 155;
                                breachedEnemiesAllowed -= 1;
                                if(breachedEnemiesAllowed == 1) {
                                    heart.currentIndex = 1;
                                }
                                if(breachedEnemiesAllowed == 0) {
                                    heart.currentIndex = 2;
                                }
                                if(breachedEnemiesAllowed == -1) {
                                    gS = gameState.LostGame;
                                    sfx.PlaySound("loss");
                                    stats.SetText();
                                    MediaPlayer.Stop();
                                }
                                callTierTransistion();
                                break;
                            }
                            else if (breachedEnemiesAllowed > 0)
                            {
                                if (breachedEnemiesAllowed == 1)
                                {
                                    activeEnemies[i].health = 0;
                                    breachedEnemiesAllowed--;
                                    heart.currentIndex = 2;
                                    break;
                                }
                                if (breachedEnemiesAllowed == 2)
                                {
                                    activeEnemies[i].health = 0;
                                    breachedEnemiesAllowed--;
                                    heart.currentIndex = 1;
                                    break;
                                }
                            }
                            else
                            {
                                gS = gameState.LostGame;
                                sfx.PlaySound("loss");
                                stats.SetText();
                                MediaPlayer.Stop();
                            }
                        }
                    }


                    pauseButton[0].isOverChoice(mouse.X, mouse.Y, mouse, oldM);
                    pauseButton[0].whatButton();
                    if ((pauseButton[0].bp == PauseButton.buttonPress.pressed || kb.IsKeyDown(ctrl.controls["Pause"])) && cooldown == false)
                    {
                        cooldown = true;
                        prevGS = gS;
                        gS = gameState.Pause;
                       
                        save(@"Content/saveData.txt");
                    }
                }
                
                if(gS == gameState.Stats)
                {
                    stats.Update(mouse);
                }
                if(gS == gameState.Cutscene) {
                    bossDialogue.IsClicking(mouse);
                    cutscene.Update();
                }
                if (gS == gameState.TitleScreen || gS == gameState.Options || gS == gameState.Config || gS == gameState.QuitGame || gS == gameState.ReallyQuit)
                {
                    bGtime++;

                    //bird boiz
                    if (birds.rec.X >= graphics.GraphicsDevice.Viewport.Width)
                    {
                        birds.rec.X = -graphics.GraphicsDevice.Viewport.Width / 4;
                        birds.rec.Y = rand.Next(-100, 100);
                    }
                    else
                        birds.rec.X++;
                    //cloud men
                    if (bGtime % 3 == 0)
                    {
                        cloud.rec.X--;
                        bGcloud.rec.X--;
                    }
                    if (cloud.rec.X == 0)
                        bGcloud.rec.X = cloud.rec.Width;
                    else if (bGcloud.rec.X == 0)
                        cloud.rec.X = bGcloud.rec.Width;

                    //background movement time
                    if (bGtime == 0)
                        bG.tex = bGt[0];
                    else if (bGtime == 8)
                        bG.tex = bGt[1];
                    else if (bGtime == 16)
                        bG.tex = bGt[2];
                    else if (bGtime == 24)
                        bG.tex = bGt[3];
                    else if (bGtime == 32)
                        bG.tex = bGt[4];
                    else if (bGtime == 40)
                        bG.tex = bGt[5];
                    else if (bGtime == 48)
                        bG.tex = bGt[6];
                    else if (bGtime == 56)
                        bG.tex = bGt[7];
                    else if (bGtime == 64)
                    {
                        bGtime = 0;
                        bG.tex = bGt[0];
                    }

                }

                if (cooldown == true)
                {
                    cdTime++;
                    if (cdTime >= 10)
                    {
                        cdTime = 0;
                        cooldown = false;
                    }

                }
                if (gS == gameState.Pause)
                {
                    pauseButton[1].isOverChoice(mouse.X, mouse.Y, mouse, oldM);
                    pauseButton[1].whatButton();
                    if ((pauseButton[1].bp == PauseButton.buttonPress.pressed || kb.IsKeyDown(ctrl.controls["Pause"])) && cooldown == false)
                    {
                        cooldown = true;
                        gS = prevGS;
                    }
                }
                if(gS == gameState.Bossfight) {
                    if(shouldReplaceLava) {
                        replaceLava(100, 478);
                    }
                    if(boss.isDead) {
                        gS = gameState.DeathScene;
                        sfx.PlaySound("break", 0.1f);
                        pieces = new BossPieces();
                        pieces.world = this;
                        pieces.rightPos = boss.position;
                        pieces.leftPos = boss.position;
                        pieces.Initialize();
                    }
                    healthBar.Update();
                }
                if(gS == gameState.DeathScene) {
                    pieces.Update();
                    if(pieces.shouldTransistion) {
                        gS = gameState.WinGame;
                        winColor = Color.Black;
                    }
                }
                if (isTierTransistioning)
                {


                    if (tierTransPos.X != offsetX - 640)
                    {
                        tierTransPos.X += 80;
                    }
                    else
                    {
                        tierTransHue++;
                    }
                    if(tierTransHue == 10) {
                        
                    }
                    if (tierTransHue > 20)
                    {
                        tierTransPos.X += 80;
                    }

                    if (tierTransPos.X > 1280 + offsetX)
                    {
                        tierTransPos.X = -1280 + offsetX - 640;
                        isTierTransistioning = false;
                        tierTransHue = 0;
                    }

                }
                
                camera.Update(new Vector2(offsetX, offsetY));
                oldM = mouse;
                oldKb = kb;
        }
            base.Update(gameTime);
        }
        public void replaceLava(int bossX, int bossY) {
            if(lavaTimer < 15) {
                lavaTimer++;
                //return;
            }
            if(bossOverlay.A < 120) {
                bossOverlay.A += 1;
            }
            radius += 10;

            if(radius > 2500) {
                shouldReplaceLava = false;
            }
            for (int i = 0; i < tileMap.GetLength(0); i++)
            {
                for (int j = 0; j < tileMap.GetLength(1); j++)
                {
                    Tile temp = tileMap[i, j];
                    Vector2 tilePos = new Vector2(temp.position.X, temp.position.Y);
                    Vector2 bossPos = new Vector2(bossX, bossY);
                    Vector2 difference = Vector2.Subtract(tilePos, bossPos);
                    double d = difference.X * difference.X;
                    d += difference.Y * difference.Y;
                    d = Math.Sqrt(d);
                    if(radius < d || temp.hasGoneUp) {
                        continue;
                    }
                    if (temp.isWaterTop)
                    {
                        temp.secondTexture = additionalTileTextures[5];
                    }
                    else if (temp.isWater)
                    {

                        temp.secondTexture = additionalTileTextures[4];
                    }
                    else if(temp.isCorner) {
                        temp.secondTexture = additionalTileTextures[2];
                    }
                    else if(temp.isMoat) {
                        temp.secondTexture = additionalTileTextures[0];
                    }
                    else if(temp.isPath) {
                        temp.secondTexture = additionalTileTextures[3];
                    }
                    else {
                        temp.secondTexture = additionalTileTextures[1];
                    }
                    temp.hasDecoration = false;
                    temp.goUp = true;
                    
                }
            }
        }
        /*
        public void replaceTiles(int era) {
            if(era == 1) {
                for(int i = 0; i < tileMap.GetLength(0); i++) {
                    for(int j = 0; j < tileMap.GetLength(1); j++) {
                        Tile temp = tileMap[i, j];
                        if(!temp.isPath) {
                            continue;
                        }
                        if(temp.isCorner) {
                            temp.texture = additionalTileTextures[1];
                        }
                        else {
                            temp.texture = additionalTileTextures[0];
                        }
                    }
                }
            }
            if(era == 2) {
                for (int i = 0; i < tileMap.GetLength(0); i++)
                {
                    for (int j = 0; j < tileMap.GetLength(1); j++)
                    {
                        Tile temp = tileMap[i, j];
                        if (!temp.isPath)
                        {
                            continue;
                        }
                        if (temp.isCorner)
                        {
                            temp.texture = additionalTileTextures[3];
                        }
                        else
                        {
                            temp.texture = additionalTileTextures[2];
                        }
                    }
                }
            }
        }
        */
        public void loadNextWave(int wave) {
            if (isCheating)
            {
                //Console.WriteLine(totalEnemies.Count);
                for (int i = 0; i < levelLoad.enemiesPerWave[0]; i++)
                {
                    if(totalEnemies.Count == 0) {
                        break;
                    }
                    EnemySuper e = totalEnemies[0];
                    e.setHealth();
                    enemies.Add(e);
                    enemyTimer = 30;
                    totalEnemies.Remove(e);

                }
                return;

            }
            if (wave >= levelLoad.enemiesPerWave.Count) {

                return;
                //Win condition?
            }
            

            for (int i = 0; i < levelLoad.enemiesPerWave[wave]; i++ ) {
                EnemySuper e = totalEnemies[0];
                e.setHealth();
                enemies.Add(e);
                enemyTimer = 30;
                totalEnemies.Remove(e);
                
            }

            infoManager.checkInfo(wave);
        }

        public void Order66()
        {
            for (int i = activeEnemies.Count - 1; i >= 0; i--)
            {
                activeEnemies[i].health = 0;
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.PeachPuff);
             
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, camera.transformation);

            if (gS == gameState.TitleScreen || gS == gameState.Options || gS == gameState.Config || gS == gameState.QuitGame || gS == gameState.ReallyQuit)
            {
                //paulo
                cloud.Draw(gameTime, spriteBatch);
                bGcloud.Draw(gameTime, spriteBatch);
                bG.Draw(gameTime, spriteBatch);
                birds.Draw(gameTime, spriteBatch);
                titleTips.Draw(gameTime, spriteBatch);
            }

            if (gS == gameState.Config)
            {
                // controlBox.Draw(gameTime, spriteBatch);
                configObj.Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(font, "Pause Game", new Vector2(180, 90), Color.White);
                spriteBatch.DrawString(font, "Cancel Tower", new Vector2(180, 190), Color.White);
                spriteBatch.DrawString(font, "Place Tower", new Vector2(180, 290), Color.White);
                spriteBatch.DrawString(font, "Move Left", new Vector2(180, 390), Color.White);
                spriteBatch.DrawString(font, "Move Right", new Vector2(180, 490), Color.White);
                //new Rectangle(140,70,1000,500)
                //every 50
            }

            //Mikey
            if (gS == gameState.StartUp)
            {
                spriteBatch.Draw(dummyTexture, artifactRectangle, Color.Black);
                titleObj.Draw(gameTime, spriteBatch);
            }
            if (gS == gameState.TitleScreen)
            {
                titleObj.Draw(gameTime, spriteBatch);
            }
            if (gS == gameState.Options)
            {
                optObj.Draw(gameTime, spriteBatch);
                volumeSlider.Draw(gameTime, spriteBatch);
                sfxSlider.Draw(gameTime, spriteBatch);
            }
            if (gS == gameState.QuitGame)
            {
                quitObj.Draw(gameTime, spriteBatch);
            }

            if (gS == gameState.Artifacts)
            {
                spriteBatch.Draw(dummyTexture, artifactRectangle, Color.DarkSalmon);
                artifactButtons[0].Draw(gameTime, spriteBatch);

                for (int i = 0; i < artifacts.Count; i++)
                {
                    artifacts[i].Draw(gameTime, spriteBatch);

                }
                spriteBatch.Draw(artifactTitle, artTitlePos, Color.White);
                spriteBatch.DrawString(font, "Your Prestige: " + prestige, prestigePos, Color.LightGray);
            }



            if (gS == gameState.LostGame)
            {
                spriteBatch.Draw(lossTex, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);
                    losCond.Draw(gameTime, spriteBatch);
                

            }

            if (gS == gameState.WinGame)
            {
                spriteBatch.Draw(winTexture, artifactRectangle, Color.White);
                winCond.Draw(gameTime, spriteBatch);
                spriteBatch.Draw(dummyTexture, artifactRectangle, winColor);

            }
            //Vivek

            if (gS == gameState.PlayGame || gS == gameState.Pause || gS == gameState.ShowInfo || gS == gameState.Tutorial || gS == gameState.Bossfight)
            {
                
                for (int i = 0; i < tileMap.GetLength(0); i++)
                {
                    for (int j = 0; j < tileMap.GetLength(1); j++)
                    {
                        tileMap[i, j].Draw(gameTime, spriteBatch);

                    }
                }

                for (int i = 0; i < attackTowers.Count; i++) {
                    attackTowers[i].Draw(gameTime, spriteBatch);
                }
                for (int i = 0; i < resourceTowers.Count; i++)
                {
                    resourceTowers[i].Draw(gameTime, spriteBatch);
                }
                if (gS == gameState.Bossfight)
                {
                   
                    spriteBatch.Draw(dummyTexture, artifactRectangle, bossOverlay);


                }
                for (int i = 0; i < activeEnemies.Count; i++)
                {
                    activeEnemies[i].Draw(gameTime, spriteBatch);
                }
                if(gS == gameState.Bossfight && boss.isDying) {
                    boss.Update();
                    boss.Draw(gameTime, spriteBatch);
                }
                particles.Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(font, "" + (currentWave-1), new Vector2(540 + offsetX, 140), Color.White);
                waveButton.Draw(gameTime, spriteBatch);
                bar.Draw(gameTime, spriteBatch);
                
                if(loader.spawner != null)
                {
                    loader.spawner.Draw(gameTime, spriteBatch);
                }
                if(shownInfoBox != null)
                {
                    shownInfoBox.Draw(gameTime, spriteBatch);
                }
                if(shownUpgrade != null)
                {
                    shownUpgrade.Draw(gameTime, spriteBatch);
                }
                if (shownTier != null)
                {
                    shownTier.Draw(gameTime, spriteBatch);
                }
                heart.Draw(gameTime, spriteBatch);
                if (isTierTransistioning)
                {
                    spriteBatch.Draw(dummyTexture, tierTransPos, tierTransCol);
                }
                if (gS == gameState.PlayGame || gS == gameState.Bossfight)
                {
                    pauseButton[0].Draw(gameTime, spriteBatch);
                    warning.Draw(gameTime, spriteBatch);
                }
               
                mainBar.Draw(gameTime, spriteBatch);
                if(isPreparingForBoss) {
                    bossDialogue.Draw(gameTime, spriteBatch);
                }
                
                if (gS == gameState.Pause)
                {
                    spriteBatch.Draw(dummyTexture, artifactRectangle, new Color(0, 0, 0, 100));
                    pauseButton[1].Draw(gameTime, spriteBatch);
                    pauseinfobox.Draw(gameTime, spriteBatch);
                    pauseObj.Draw(gameTime, spriteBatch);
                }
                if(gS == gameState.ShowInfo) {
                    spriteBatch.Draw(dummyTexture, artifactRectangle, new Color(0, 0, 0, 100));
                    shownInfo.Draw(gameTime, spriteBatch);
                }
                if(gS == gameState.Tutorial) {
                    //spriteBatch.Draw(dummyTexture, artifactRectangle, new Color(0, 0, 0, 100));
                    dialogue.Draw(gameTime, spriteBatch);
                }
                if(gS == gameState.Bossfight) {
                    healthBar.Draw(gameTime, spriteBatch);
                }
            }
            if(gS == gameState.Cutscene) {
                
                cutscene.Draw(gameTime, spriteBatch);
                bossDialogue.Draw(gameTime, spriteBatch);
            }
            if(gS == gameState.DeathScene) {
                spriteBatch.Draw(dummyTexture, artifactRectangle, Color.Black);
                pieces.Draw(gameTime, spriteBatch);
               
            }
            if (gS == gameState.Stats)
            {
               
              
                if (stats.isLoss)
                {
                    spriteBatch.Draw(lossTex, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);
                }
                else {
                    spriteBatch.Draw(winTexture, artifactRectangle, Color.White);
                }
                stats.Draw(gameTime, spriteBatch);
            }
            transistioner.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
