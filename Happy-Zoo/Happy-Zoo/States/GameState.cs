using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Happy_Zoo.Controls;
using MonoGame.Extended;

namespace Happy_Zoo.States
{
    public class GameState : State
    {
        #region Fields
        Rectangle clickable;
        Texture2D grass;
        Texture2D elephant1, flamingo1, giraffe1, gorilla1, lion1, kangaroo1, panda1;
        Texture2D elephant2, flamingo2, giraffe2, gorilla2, lion2, kangaroo2, panda2;
        Texture2D elephant3, flamingo3, giraffe3, gorilla3, lion3, kangaroo3, panda3;

        #region animal in housing sprites

        Texture2D elep0, elep1, elep2, elep3;
        Texture2D leeuw0, leeuw1, leeuw2, leeuw3, leeuw4;
        Texture2D flam0, flam1, flam2, flam3, flam4, flam5, flam6;
        Texture2D gira0, gira1, gira2, gira3, gira4;
        Texture2D gori0, gori1, gori2, gori3, gori4;
        Texture2D pand0, pand1, pand2, pand3, pand4;
        Texture2D kang0, kang1, kang2, kang3, kang4, kang5, kang6;

        #endregion

        Texture2D info1, toilet1, eat1;
        SpriteFont font;
        Texture2D rand;
        Texture2D dierenKnop;
        Texture2D faciliteitenKnop;
        Texture2D verblijvenKnop;
        Texture2D geldKnop;
        Camera2D camera;
        Park park;
        Dictionary<string, Texture2D> texturesLevel1;
        Dictionary<string, Texture2D> texturesLevel2;
        Dictionary<string, Texture2D> texturesLevel3;
        Dictionary<string, Texture2D> animalInHousing;
        Dictionary<Vector2, string> buildingPos;
        Dictionary<string, Rectangle> clickableItems;
        bool isReleased, finished;
        private MouseState _currentMouse;
        private MouseState _previousMouse;

        Texture2D buttonText;
        Texture2D box;
        Texture2D box2;
        private List<Component> _components;
        private List<Component> _components2;
        private List<Component> _components3;
        private List<Component> _components4;
        private List<Component> _components5;
        bool animal = false;
        bool animalHouse = false;
        bool facility = false;
        bool animalHouseClicked = false;
        bool moneyMenu = false;
        float entrenceFeeTemp;
        String itemName;

        Color browncolour = new Color(102, 71, 54);
        private int screenWidth = 1024;
        private int screenHeight = 768;
        private int screenCenter = 1024 / 2;

        MouseState ms;
        KeyboardState kbs;

        private float previousMouseWheelValue, currentMouseWheelValue;
        #endregion

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            // Set each texture to a variable
            #region Constructor fields
            isReleased = true;
            finished = true;
            texturesLevel1 = new Dictionary<string, Texture2D>();
            texturesLevel2 = new Dictionary<string, Texture2D>();
            texturesLevel3 = new Dictionary<string, Texture2D>();
            animalInHousing = new Dictionary<string, Texture2D>();
            buildingPos = new Dictionary<Vector2, string>();
            clickableItems = new Dictionary<string, Rectangle>();
            font = _content.Load<SpriteFont>("Fonts/default");
            grass = _content.Load<Texture2D>("Images/GrassTile");
            font = _content.Load<SpriteFont>("Fonts/bloklettersklein");
            #region sprites

            info1 = _content.Load<Texture2D>("Images/faciliteiten/Info1");
            toilet1 = _content.Load<Texture2D>("Images/faciliteiten/ToiletBuilding");
            eat1 = _content.Load<Texture2D>("Images/faciliteiten/eetendrankKraam");
            camera = new Camera2D(graphicsDevice);
            rand = _content.Load<Texture2D>("Images/rand");
            dierenKnop = _content.Load<Texture2D>("Images/dierenKnop");
            faciliteitenKnop = _content.Load<Texture2D>("Images/faciliteitenKnop");
            verblijvenKnop = _content.Load<Texture2D>("Images/verblijvenKnop");
            geldKnop = _content.Load<Texture2D>("Controls/knopgeld");
            buttonText = _content.Load<Texture2D>("Controls/giraffeText");
            box = _content.Load<Texture2D>("Images/UI/popoutmenu");
            box2 = _content.Load<Texture2D>("Images/UI/popupmenu");

            #endregion
            itemName = String.Empty;
            #endregion
            #region Level 1 buildings

            elephant1 = _content.Load<Texture2D>("Images/kooien/olifantLevel1");
            flamingo1 = _content.Load<Texture2D>("Images/kooien/flamingoLevel1");
            giraffe1 = _content.Load<Texture2D>("Images/kooien/girafLevel1");
            gorilla1 = _content.Load<Texture2D>("Images/kooien/gorillaLevel1");
            lion1 = _content.Load<Texture2D>("Images/kooien/leeuwLevel1");
            kangaroo1 = _content.Load<Texture2D>("Images/kooien/kangaroeLevel1");
            panda1 = _content.Load<Texture2D>("Images/kooien/PandaLevel1");

            #endregion
            #region Level 2 buildings

            elephant2 = _content.Load<Texture2D>("Images/kooien/olifantLevel2");
            flamingo2 = _content.Load<Texture2D>("Images/kooien/flamingoLevel2");
            giraffe2 = _content.Load<Texture2D>("Images/kooien/girafLevel2");
            gorilla2 = _content.Load<Texture2D>("Images/kooien/gorillaLevel2");
            lion2 = _content.Load<Texture2D>("Images/kooien/leeuwLevel2");
            kangaroo2 = _content.Load<Texture2D>("Images/kooien/kangaroeLevel2");
            panda2 = _content.Load<Texture2D>("Images/kooien/PandaLevel2");

            #endregion
            #region Level 3 buildings

            elephant3 = _content.Load<Texture2D>("Images/kooien/olifantLevel3");
            flamingo3 = _content.Load<Texture2D>("Images/kooien/flamingoLevel3");
            giraffe3 = _content.Load<Texture2D>("Images/kooien/girafLevel3");
            gorilla3 = _content.Load<Texture2D>("Images/kooien/gorillaLevel3");
            lion3 = _content.Load<Texture2D>("Images/kooien/leeuwLevel3");
            kangaroo3 = _content.Load<Texture2D>("Images/kooien/kangaroeLevel3");
            panda3 = _content.Load<Texture2D>("Images/kooien/PandaLevel3");

            #endregion
            #region animal in housings
            elep0 = _content.Load<Texture2D>("Images/kooien/olifantLevel1");
            elep1 = _content.Load<Texture2D>("Images/dieren_in_kooien/olifant1");
            elep2 = _content.Load<Texture2D>("Images/dieren_in_kooien/olifant2");
            elep3 = _content.Load<Texture2D>("Images/dieren_in_kooien/olifant3");
            leeuw0 = _content.Load<Texture2D>("Images/kooien/olifantLevel1");
            leeuw1 = _content.Load<Texture2D>("Images/dieren_in_kooien/leeuw1");
            leeuw2 = _content.Load<Texture2D>("Images/dieren_in_kooien/leeuw2");
            leeuw3 = _content.Load<Texture2D>("Images/dieren_in_kooien/leeuw3");
            leeuw4 = _content.Load<Texture2D>("Images/dieren_in_kooien/leeuw4");
            kang0 = _content.Load<Texture2D>("Images/kooien/olifantLevel1");
            kang1 = _content.Load<Texture2D>("Images/dieren_in_kooien/kangoeroe1");
            kang2 = _content.Load<Texture2D>("Images/dieren_in_kooien/kangoeroe2");
            kang3 = _content.Load<Texture2D>("Images/dieren_in_kooien/kangoeroe3");
            kang4 = _content.Load<Texture2D>("Images/dieren_in_kooien/kangoeroe4");
            kang5 = _content.Load<Texture2D>("Images/dieren_in_kooien/kangoeroe5");
            kang6 = _content.Load<Texture2D>("Images/dieren_in_kooien/kangoeroe6");
            pand0 = _content.Load<Texture2D>("Images/kooien/olifantLevel1");
            pand1 = _content.Load<Texture2D>("Images/dieren_in_kooien/panda1");
            pand2 = _content.Load<Texture2D>("Images/dieren_in_kooien/panda2");
            pand3 = _content.Load<Texture2D>("Images/dieren_in_kooien/panda3");
            gira0 = _content.Load<Texture2D>("Images/kooien/girafLevel1");
            gira1 = _content.Load<Texture2D>("Images/dieren_in_kooien/giraffe1");
            gira2 = _content.Load<Texture2D>("Images/dieren_in_kooien/giraffe2");
            gira3 = _content.Load<Texture2D>("Images/dieren_in_kooien/giraffe3");
            gira4 = _content.Load<Texture2D>("Images/dieren_in_kooien/giraffe4");
            flam0 = _content.Load<Texture2D>("Images/kooien/flamingoLevel1");
            flam1 = _content.Load<Texture2D>("Images/dieren_in_kooien/flamingo1");
            flam2 = _content.Load<Texture2D>("Images/dieren_in_kooien/flamingo2");
            flam3 = _content.Load<Texture2D>("Images/dieren_in_kooien/flamingo3");
            flam4 = _content.Load<Texture2D>("Images/dieren_in_kooien/flamingo4");
            flam5 = _content.Load<Texture2D>("Images/dieren_in_kooien/flamingo5");
            flam6 = _content.Load<Texture2D>("Images/dieren_in_kooien/flamingo6");
            gori0 = _content.Load<Texture2D>("Images/kooien/gorillaLevel1");
            gori1 = _content.Load<Texture2D>("Images/dieren_in_kooien/gorilla1");
            gori2 = _content.Load<Texture2D>("Images/dieren_in_kooien/gorilla2");
            gori3 = _content.Load<Texture2D>("Images/dieren_in_kooien/gorilla3");
            gori4 = _content.Load<Texture2D>("Images/dieren_in_kooien/gorilla4");
            #endregion

            // add building textures to a list for comparison
            #region Textures Level 1
            texturesLevel1.Add("Elephant", elephant1);
            texturesLevel1.Add("Flamingo", flamingo1);
            texturesLevel1.Add("Giraffe", giraffe1);
            texturesLevel1.Add("Gorilla", gorilla1);
            texturesLevel1.Add("Kangaroo", kangaroo1);
            texturesLevel1.Add("Panda", panda1);
            texturesLevel1.Add("Lion", lion1);
            texturesLevel1.Add("ToiletBuilding", toilet1);
            texturesLevel1.Add("InformationBuilding", info1);
            texturesLevel1.Add("EatAndDrinkBooth", eat1);
            #endregion

            #region Textures Level 2
            texturesLevel2.Add("Elephant", elephant2);
            texturesLevel2.Add("Flamingo", flamingo2);
            texturesLevel2.Add("Giraffe", giraffe2);
            texturesLevel2.Add("Gorilla", gorilla2);
            texturesLevel2.Add("Kangaroo", kangaroo2);
            texturesLevel2.Add("Panda", panda2);
            texturesLevel2.Add("Lion", lion2);
            #endregion

            #region Textures Level 3
            texturesLevel3.Add("Elephant", elephant3);
            texturesLevel3.Add("Flamingo", flamingo3);
            texturesLevel3.Add("Giraffe", giraffe3);
            texturesLevel3.Add("Gorilla", gorilla3);
            texturesLevel3.Add("Kangaroo", kangaroo3);
            texturesLevel3.Add("Panda", panda3);
            texturesLevel3.Add("Lion", lion3);
            #endregion

            #region animals+level sprites

            animalInHousing.Add("elep0", elep0);
            animalInHousing.Add("elep1",elep1);
            animalInHousing.Add("elep2",elep2);
            animalInHousing.Add("elep3",elep3);
            animalInHousing.Add("leeuw0",leeuw0);
            animalInHousing.Add("leeuw1",leeuw1);
            animalInHousing.Add("leeuw2",leeuw2);
            animalInHousing.Add("leeuw3",leeuw3);
            animalInHousing.Add("leeuw4",leeuw4);
            animalInHousing.Add("kang0",kang0);
            animalInHousing.Add("kang1",kang1);
            animalInHousing.Add("kang2",kang2);
            animalInHousing.Add("kang3",kang3);
            animalInHousing.Add("kang4",kang4);
            animalInHousing.Add("kang5",kang5);
            animalInHousing.Add("kang6",kang6);
            animalInHousing.Add("pand0",pand0);
            animalInHousing.Add("pand1",pand1);
            animalInHousing.Add("pand2",pand2);
            animalInHousing.Add("pand3",pand3);
            animalInHousing.Add("gira0",gira0);
            animalInHousing.Add("gira1",gira1);
            animalInHousing.Add("gira2",gira2);
            animalInHousing.Add("gira3",gira3);
            animalInHousing.Add("gira4",gira4);
            animalInHousing.Add("flam0",flam0);
            animalInHousing.Add("flam1",flam1);
            animalInHousing.Add("flam2",flam2);
            animalInHousing.Add("flam3",flam3);
            animalInHousing.Add("flam4",flam4);
            animalInHousing.Add("flam5",flam5);
            animalInHousing.Add("flam6",flam6);
            animalInHousing.Add("gori0",gori0);
            animalInHousing.Add("gori1",gori1);
            animalInHousing.Add("gori2",gori2);
            animalInHousing.Add("gori3",gori3);
            animalInHousing.Add("gori4",gori4);

            #endregion

            park = new Park();

            // Create the buttons used in the game
            #region Creating buttons
            var animalButton = new ButtonExp(dierenKnop, font)
            {
                Rectangle = new Rectangle(18, 250, 75, 75)
            };

            animalButton.Click += animalButton_Click;

            var facilityButton = new ButtonExp(faciliteitenKnop, font)
            {
                Rectangle = new Rectangle(18, 400, 75, 75)
            };

            facilityButton.Click += facilityButton_Click;

            var animalHouseButton = new ButtonExp(verblijvenKnop, font)
            {
                Rectangle = new Rectangle(18, 550, 75, 75)
            };

            animalHouseButton.Click += animalHouseButton_Click;

            var moneyButton = new ButtonExp(geldKnop, font)
            {
                Rectangle = new Rectangle(18, 680, 75, 75)
            };

            moneyButton.Click += moneyButton_Click;

            _components = new List<Component>()
            {
                animalButton,
                facilityButton,
                animalHouseButton,
                moneyButton
            };

            var buttonX = 195;
            var buttonY = 180;

            var Elephant = new ButtonExp(buttonText, font)
            {
                Text = "Elephant",
                Rectangle = new Rectangle(buttonX, buttonY, 110, 75)
            };

            Elephant.Click += Elephant_Click;

            var Giraffe = new ButtonExp(buttonText, font)
            {
                Text = "Giraffe",
                Rectangle = new Rectangle(buttonX, buttonY + 70, 110, 75)
            };

            Giraffe.Click += Giraffe_Click;

            var Gorilla = new ButtonExp(buttonText, font)
            {
                Text = "Gorilla",
                Rectangle = new Rectangle(buttonX, buttonY + 140, 110, 75)
            };

            Gorilla.Click += Gorilla_Click;

            var Kangaroo = new ButtonExp(buttonText, font)
            {
                Text = "Kangaroo",
                Rectangle = new Rectangle(buttonX, buttonY + 210, 110, 75)
            };

            Kangaroo.Click += Kangaroo_Click;

            var Flamingo = new ButtonExp(buttonText, font)
            {
                Text = "Flamingo",
                Rectangle = new Rectangle(buttonX, buttonY + 280, 110, 75)
            };

            Flamingo.Click += Flamingo_Click;

            var Panda = new ButtonExp(buttonText, font)
            {
                Text = "Panda",
                Rectangle = new Rectangle(buttonX, buttonY + 350, 110, 75)
            };

            Panda.Click += Panda_Click;

            var Lion = new ButtonExp(buttonText, font)
            {
                Text = "Lion",
                Rectangle = new Rectangle(buttonX, buttonY + 420, 110, 75)
            };

            Lion.Click += Lion_Click;

            _components2 = new List<Component>()
            {
                Elephant,
                Giraffe,
                Gorilla,
                Kangaroo,
                Flamingo,
                Panda,
                Lion
            };

            var ToiletBuilding = new ButtonExp(buttonText, font)
            {
                Text = "Toilet",
                Rectangle = new Rectangle(buttonX, buttonY, 110, 75)
            };

            var InformationBuilding = new ButtonExp(buttonText, font)
            {
                Text = "Info",
                Rectangle = new Rectangle(buttonX, buttonY + 140, 110, 75)
            };

            var EatAndDrinkBooth = new ButtonExp(buttonText, font)
            {
                Text = "Food",
                Rectangle = new Rectangle(buttonX, buttonY + 70, 110, 75)
            };

            ToiletBuilding.Click += ToiletBuilding_Click;
            InformationBuilding.Click += InformationBuilding_Click;
            EatAndDrinkBooth.Click += EatAndDrinkBooth_Click;

            _components3 = new List<Component>()
            {
                ToiletBuilding,
                InformationBuilding,
                EatAndDrinkBooth
            };

            var upgrade = new ButtonExp(buttonText, font)
            {
                Text = "Upgrade",
                Rectangle = new Rectangle(450, 400, 130, 75)
            };

            upgrade.Click += upgrade_Click;

            var cancel = new ButtonExp(buttonText, font)
            {
                Text = "Cancel",
                Rectangle = new Rectangle(450, 470, 130, 75)
            };

            cancel.Click += cancel_Click;

            _components4 = new List<Component>()
            {
                upgrade,
                cancel
            };

            var plus = new ButtonExp(buttonText, font)
            {
                Text = "+",
                Rectangle = new Rectangle(549, 280, 75, 75)
            };

            plus.Click += plus_Click;

            var min = new ButtonExp(buttonText, font)
            {
                Text = "-",
                Rectangle = new Rectangle(400, 280, 75, 75)
            };

            min.Click += min_Click;

            var confirm = new ButtonExp(buttonText, font)
            {
                Text = "Confirm",
                Rectangle = new Rectangle(450, 470, 130, 75)
            };

            confirm.Click += confirm_Click;

            _components5 = new List<Component>()
            {
                plus,
                min,
                confirm
            };


            #endregion
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Local variables for map generation
            int TileWidth = 0;
            int TileHeight = 0;
            int TileX = 0;
            int TileY = 0;
            int offsetX = 200;
            int offsetY = -500;
            int difference = TileHeight - TileWidth;
            int newOffset = TileWidth / 3;

            // Draw the background
            #region Background
            spriteBatch.Begin(transformMatrix: camera.GetViewMatrix());
            // y axis
            for (int i = 0; i < 300; i++)
            {
                // x axis
                for (int j = 300; j > 0; j--)
                {
                    TileWidth = grass.Width;
                    TileHeight = grass.Height;

                    TileX = (j * TileWidth / 3) + (i * TileWidth / 2) / 2 + newOffset;
                    TileY = (i * TileHeight / 2) - (j * TileHeight / 4);

                    spriteBatch.Draw(grass, new Rectangle((TileX - TileY) + offsetX, (TileY + TileX) + offsetY, TileWidth, TileHeight), Color.White);
                }
                newOffset = 0 - (TileWidth * i) / 3;
            }
            spriteBatch.End();
            #endregion

            // Draw the main elements
            #region Main
            spriteBatch.Begin(transformMatrix: camera.GetViewMatrix());
            ms = Mouse.GetState();
            kbs = Keyboard.GetState();

            Matrix inverseTransform = Matrix.Invert(camera.GetViewMatrix());
            Vector2 mouseInWorld = Vector2.Transform(new Vector2(ms.X, ms.Y), inverseTransform);

            if (itemName != null)
                DrawBuildings(spriteBatch);


            spriteBatch.End();
            #endregion

            // Draw the UI
            #region UI
            spriteBatch.Begin();

            var boxWidth = 650;     // width of pop-up screen
            var boxHeight = 600;    // height of pop-up screen
            float stringX = screenCenter; // X starting point of the strings
            float stringY = 240;        // Y starting point of the strings

            spriteBatch.Draw(rand, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);

            if (animal == true || animalHouse == true || facility == true)
            {
                var textX = 315;
                var textY = 210;

                //animal and animalHouse menu
                spriteBatch.Draw(box, new Rectangle(70, 100, 450, 700), Color.White);
                if (animal == true || animalHouse == true)
                {
                    foreach (var component in _components2)
                    {
                        component.Draw(gameTime, spriteBatch);
                    }

                    if (animal == true)
                    {
                        spriteBatch.DrawString(font, "$300", new Vector2(textX, textY), browncolour);
                        spriteBatch.DrawString(font, "$200", new Vector2(textX, textY + 70), browncolour);
                        spriteBatch.DrawString(font, "$250", new Vector2(textX, textY + 140), browncolour);
                        spriteBatch.DrawString(font, "$150", new Vector2(textX, textY + 210), browncolour);
                        spriteBatch.DrawString(font, "$75", new Vector2(textX, textY + 280), browncolour);
                        spriteBatch.DrawString(font, "$1000", new Vector2(textX, textY + 350), browncolour);
                        spriteBatch.DrawString(font, "$200", new Vector2(textX, textY + 420), browncolour);
                    }

                    if (animalHouse == true)
                    {
                        spriteBatch.DrawString(font, "$1000", new Vector2(textX, textY), browncolour);
                        spriteBatch.DrawString(font, "$1200", new Vector2(textX, textY + 70), browncolour);
                        spriteBatch.DrawString(font, "$1500", new Vector2(textX, textY + 140), browncolour);
                        spriteBatch.DrawString(font, "$1200", new Vector2(textX, textY + 210), browncolour);
                        spriteBatch.DrawString(font, "$1000", new Vector2(textX, textY + 280), browncolour);
                        spriteBatch.DrawString(font, "$3000", new Vector2(textX, textY + 350), browncolour);
                        spriteBatch.DrawString(font, "$1000", new Vector2(textX, textY + 420), browncolour);
                    }
                }

                //facility menu
                if (facility == true)
                {
                    foreach (var component in _components3)
                    {
                        component.Draw(gameTime, spriteBatch);
                    }
                    spriteBatch.DrawString(font, "$750", new Vector2(textX, textY), browncolour);
                    spriteBatch.DrawString(font, "$2000", new Vector2(textX, textY + 140), browncolour);
                    spriteBatch.DrawString(font, "$1500", new Vector2(textX, textY + 70), browncolour);
                }
            }



            if (animalHouseClicked)
            {


                // draw the pop-up screen
                spriteBatch.Draw(box2, new Rectangle(screenCenter - (boxWidth / 2), (768 / 2) - (boxHeight / 2), boxWidth, boxHeight), Color.White);

                // draw the buttons
                foreach (var component in _components4)
                {
                    component.Draw(gameTime, spriteBatch);
                }

                Animalhouse currentAnimalHouse = park.selectAnimalHouse(itemName);
                currentAnimalHouse.setPriceUpgrade();

                // specify the text in the pop-up screen
                string a = "This animal house contains ";
                string b = currentAnimalHouse.getCurrentAnimals() + " animal(s) and is level " + currentAnimalHouse.getLevel();
                string c = "Do you want to upgrade";
                string d = "this building for " + currentAnimalHouse.getPrice() + " coins?";



                // draw the text
                spriteBatch.DrawString(font, a, new Vector2(stringX - (font.MeasureString(a).X / 2), stringY), new Color(102, 71, 54));
                spriteBatch.DrawString(font, b, new Vector2(stringX - (font.MeasureString(b).X / 2), stringY + 25), new Color(102, 71, 54));
                spriteBatch.DrawString(font, c, new Vector2(stringX - (font.MeasureString(c).X / 2), stringY + 75), new Color(102, 71, 54));
                spriteBatch.DrawString(font, d, new Vector2(stringX - (font.MeasureString(d).X / 2), stringY + 100), new Color(102, 71, 54));
            }

            if (moneyMenu == true)
            {
                //draw the pop-up screen
                spriteBatch.Draw(box2, new Rectangle(screenCenter - (boxWidth / 2), (768 / 2) - (boxHeight / 2), boxWidth, boxHeight), Color.White);

                string a = "Set the entrance fee.";
                string b = entrenceFeeTemp.ToString();

                // draw the text
                spriteBatch.DrawString(font, a, new Vector2(stringX - (font.MeasureString(a).X / 2), stringY), new Color(102, 71, 54));
                spriteBatch.DrawString(font, b, new Vector2(stringX - (font.MeasureString(b).X / 2), stringY + 75), new Color(102, 71, 54));

                //draw the buttons
                foreach (var component in _components5)
                {
                    component.Draw(gameTime, spriteBatch);
                }
            }

            //draw left buttons
            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            //draw top text
            Vector2 textcoord3 = new Vector2(300, 18);
            spriteBatch.DrawString(font, park.getDate(), textcoord3, Color.White);
            Vector2 textcoord1 = new Vector2(575, 18);
            spriteBatch.DrawString(font, "$ " + park.getCoins(), textcoord1, Color.White);
            Vector2 textcoord2 = new Vector2(840, 18);
            spriteBatch.DrawString(font, "Visitors: " + park.getVisitorCount(), textcoord2, Color.White);


            spriteBatch.End();
            #endregion
        }

        public void DrawBuildings(SpriteBatch spriteBatch)
        {
            // loop through each building purchased
            foreach (var building in buildingPos)
            {
                Animalhouse currentAnimalHouse = park.selectAnimalHouse(building.Value);
                if (currentAnimalHouse.getLevel() == 1)
                {
                    // Loop through textures in texturelist
                    foreach (var item in texturesLevel1)
                    {
                        // Compare values for the right texture
                        if (building.Value == item.Key)
                        {
                            spriteBatch.Draw(item.Value, new Rectangle((int)building.Key.X, (int)building.Key.Y, item.Value.Width / 4, item.Value.Height / 4), Color.White);
                            //clickableItems.Add(item.Key, new Rectangle((int)building.Key.X, (int)building.Key.Y, item.Value.Width / 4, item.Value.Height / 4));
                            clickable = new Rectangle((int)building.Key.X, (int)building.Key.Y, item.Value.Width / 4, item.Value.Height / 4);
                        }
                    }
                }
                else if (currentAnimalHouse.getLevel() == 2)
                {
                    // Loop through textures in texturelist
                    foreach (var item in texturesLevel2)
                    {
                        // Compare values for the right texture
                        if (building.Value == item.Key)
                        {
                            spriteBatch.Draw(item.Value, new Rectangle((int)building.Key.X, (int)building.Key.Y, item.Value.Width / 4, item.Value.Height / 4), Color.White);
                            //clickableItems.Add(item.Key, new Rectangle((int)building.Key.X, (int)building.Key.Y, item.Value.Width / 4, item.Value.Height / 4));
                            clickable = new Rectangle((int)building.Key.X, (int)building.Key.Y, item.Value.Width / 4, item.Value.Height / 4);
                        }
                    }
                }
                else if (currentAnimalHouse.getLevel() == 3)
                {
                    // Loop through textures in texturelist
                    foreach (var item in texturesLevel3)
                    {
                        // Compare values for the right texture
                        if (building.Value == item.Key)
                        {
                            spriteBatch.Draw(item.Value, new Rectangle((int)building.Key.X, (int)building.Key.Y, item.Value.Width / 4, item.Value.Height / 4), Color.White);
                            //clickableItems.Add(item.Key, new Rectangle((int)building.Key.X, (int)building.Key.Y, item.Value.Width / 4, item.Value.Height / 4));
                            clickable = new Rectangle((int)building.Key.X, (int)building.Key.Y, item.Value.Width / 4, item.Value.Height / 4);
                        }
                    }
                }
            }
        }

        private void DrawBuildingsToAnimals(SpriteBatch spriteBatch) {

            // loop through each building purchased
            foreach (var building in buildingPos)
            {
                Animalhouse currentAnimalHouse = park.selectAnimalHouse(building.Value);
                int currentAnimalHouseAnimals = currentAnimalHouse.getCurrentAnimals();

                // Loop through textures in texturelist
                foreach (var item in texturesLevel1)
                {
                    // Compare values for the right texture
                    if (building.Value == item.Key)
                    {
                        spriteBatch.Draw(item.Value, new Rectangle((int)building.Key.X, (int)building.Key.Y, item.Value.Width / 4, item.Value.Height / 4), Color.White);
                        //clickableItems.Add(item.Key, new Rectangle((int)building.Key.X, (int)building.Key.Y, item.Value.Width / 4, item.Value.Height / 4));
                        clickable = new Rectangle((int)building.Key.X, (int)building.Key.Y, item.Value.Width / 4, item.Value.Height / 4);
                    }
                }
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public void DetectClick()
        {
            if (ms.LeftButton == ButtonState.Pressed && isReleased)
            {
                isReleased = false;
                // We now know the left mouse button is down and it wasn't down last frame
                // so we've detected a click
                // Now find the position 
                Point mousePos = new Point(ms.X, ms.Y);
                if (clickable.Contains(mousePos))
                {
                    animalHouseClicked = true;
                }
            }
            isReleased = true;
        }

        public override void Update(GameTime gameTime)
        {
            DetectClick();
            ms = Mouse.GetState();
            kbs = Keyboard.GetState();
            // get current viewmatrix
            Matrix inverseTransform = Matrix.Invert(camera.GetViewMatrix());
            // Inverse mouse input for right place in world
            Vector2 mouseInWorld = Vector2.Transform(new Vector2(ms.X, ms.Y), inverseTransform);
            // After building is purchased, place it with E
            if (!finished)
            {
                if (kbs.IsKeyDown(Keys.E) && isReleased)
                {
                    isReleased = false;
                    buildingPos.Add(new Vector2(mouseInWorld.X - 100, mouseInWorld.Y - 125), itemName);
                    finished = true;

                }
                isReleased = true;
            }

            Vector2 moveVelocity = Vector2.Zero;

            foreach (var component in _components)
            {
                component.Update(gameTime);
            }

            foreach (var component in _components2)
            {
                component.Update(gameTime);
            }

            foreach (var component in _components3)
            {
                component.Update(gameTime);
            }

            foreach (var component in _components4)
            {
                component.Update(gameTime);
            }

            foreach (var component in _components5)
            {
                component.Update(gameTime);
            }
            #region camera Control 
            if (kbs.IsKeyDown(Keys.W))
            {
                moveVelocity += new Vector2(0, -1);
            }
            if (kbs.IsKeyDown(Keys.S))
            {
                moveVelocity += new Vector2(0, 1);
            }
            if (kbs.IsKeyDown(Keys.A))
            {
                moveVelocity += new Vector2(-1, 0);
            }
            if (kbs.IsKeyDown(Keys.D))
            {
                moveVelocity += new Vector2(1, 0);
            }
            camera.Move(moveVelocity * 5);

            previousMouseWheelValue = currentMouseWheelValue;
            currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;

            if (currentMouseWheelValue > previousMouseWheelValue)
            {
                camera.ZoomIn(.08f);
            }
            if (currentMouseWheelValue < previousMouseWheelValue)
            {
                camera.ZoomOut(.08f);
            }
            #endregion
        }

        #region onClick methods

        // Click the animal house button
        private void animalHouseButton_Click(object sender, EventArgs e)
        {
            if (animalHouse == false)
            {
                animal = false;
                facility = false;
                animalHouse = true;
            }
            else if (animalHouse == true)
            {
                animalHouse = false;
            }
        }
        // Click the animal button
        private void animalButton_Click(object sender, EventArgs eh)
        {
            if (animal == false)
            {
                animalHouse = false;
                facility = false;
                animal = true;
            }
            else if (animal == true)
            {
                animal = false;
            }

        }
        // Click the facility button
        private void facilityButton_Click(object sender, EventArgs e)
        {
            if (facility == false)
            {
                animal = false;
                animalHouse = false;
                facility = true;
            }
            else
            {
                facility = false;
            }
        }
        // Click the money button
        private void moneyButton_Click(object sender, EventArgs e)
        {
            if (moneyMenu == false)
            {
                animal = false;
                animalHouse = false;
                facility = false;
                moneyMenu = true;

                entrenceFeeTemp = park.getEntranceFee();
            }
            else
            {
                moneyMenu = false;
            }
        }
        //click the plus to increase the temp entrencefee
        private void plus_Click(object sender, EventArgs e)
        {
            if (entrenceFeeTemp > -1 && entrenceFeeTemp < 20)
            {
                entrenceFeeTemp += 1;
            }
        }
        //click the min to decrease the temp entrencefee
        private void min_Click(object sender, EventArgs e)
        {
            if (entrenceFeeTemp > 0 && entrenceFeeTemp < 21)
            {
                entrenceFeeTemp -= 1;
            }
        }
        //click confirm to set the temp entrencefee to the real entrencefee
        private void confirm_Click(object sender, EventArgs e)
        {
            park.setEntranceFee(entrenceFeeTemp);
            moneyMenu = false;
        }


        // Click Elephant to buy either animalhouse or animal
        private void Elephant_Click(object sender, EventArgs e)
        {
            buyAnimalEct("Elephant", AnimalType.Elephant);
        }
        // Click Giraffe to buy either animalhouse or animal
        private void Giraffe_Click(object sender, EventArgs e)
        {
            buyAnimalEct("Giraffe", AnimalType.Giraffe);
        }
        // Click Gorilla to buy either animalhouse or animal
        private void Gorilla_Click(object sender, EventArgs e)
        {
            buyAnimalEct("Gorilla", AnimalType.Gorilla);
        }
        // Click Kangaroo to buy either animalhouse or animal
        private void Kangaroo_Click(object sender, EventArgs e)
        {
            buyAnimalEct("Kangaroo", AnimalType.Kangaroo);
        }
        // Click Flamingo to buy either animalhouse or animal
        private void Flamingo_Click(object sender, EventArgs e)
        {
            buyAnimalEct("Flamingo", AnimalType.Flamingo);
        }
        // Click Panda to buy either animalhouse or animal
        private void Panda_Click(object sender, EventArgs e)
        {
            buyAnimalEct("Panda", AnimalType.Panda);
        }
        // Click Lion to buy either animalhouse or animal
        private void Lion_Click(object sender, EventArgs e)
        {
            buyAnimalEct("Lion", AnimalType.Lion);
        }

        //Buy animal or animalhouse depending on the menu
        private void buyAnimalEct(string name, AnimalType animaltype)
        {
            if (animal == true && animalHouse == false)
            {
                park.addAnimal(name);
            }
            else if (animalHouse == true && animal == false)
            {
                finished = false;
                park.addAnimalHouse(park.createAnimalhouse(animaltype, name));
                itemName = name;
            }
            else
            {
                new Exception("something is wrong with the animal and animalhouse booleans");
            }
        }

        // Buy facility
        private void buyFacility(FacilityType facilityType, string name)
        {
            if (facility && !animal && !animalHouse)
            {
                finished = false;
                park.addFacilities(park.createFacility(facilityType, name));
                itemName = name;
            }
            else
            {
                new Exception("Something is wrong with the facility boolean");
            }
        }

        // Click ToiletBuilding to buy facility
        private void ToiletBuilding_Click(object sender, EventArgs e)
        {
            buyFacility(FacilityType.ToiletBuilding, "ToiletBuilding");
        }

        // Click InformationBuilding to buy facility
        private void InformationBuilding_Click(object sender, EventArgs e)
        {
            buyFacility(FacilityType.InformationBuilding, "InformationBuilding");
        }
        // Click EatAndDrinkBooth to buy facility
        private void EatAndDrinkBooth_Click(object sender, EventArgs e)
        {
            buyFacility(FacilityType.EatAndDrinkBooth, "EatAndDrinkBooth");
        }

        // Cancel button in upgrade menu
        private void cancel_Click(object sender, EventArgs e)
        {
            animalHouseClicked = false;
        }

        // Upgrade the facility
        private void upgrade_Click(object sender, EventArgs e)
        {
            park.upgradeAnimalhouse(itemName);
        }

        #endregion

    }
}
