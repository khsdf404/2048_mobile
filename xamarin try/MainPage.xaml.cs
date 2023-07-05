using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;

namespace _2048
{
    public partial class MainPage : ContentPage
    {
        private static double width;
        private static double height;
        String GetColor(int caseValue) {
            string[] colors = {
            "CCC2B5", // emptyCase
            "EEE4DA", // Case_2
            "EDE0C8", // Case_4 
            "F2B179", // Case_8
            "F59563", // Case_16
            "F67C5F", // Case_32
            "F65E3B", // Case_64
            "EDCE73", // Case_128
            "E9CE61", // Case_256
            "EBCA51", // Case_512
            "EEC63F", // Case_1024
            "E9C42D" // Case_2048
            };

            return colors[(int)Math.Log(caseValue, 2)];
        }





        public MainPage()
        {
            InitializeComponent();

            docWrapp.SizeChanged += new EventHandler(PageLoaded);
        }
        private void PageLoaded(object sender, EventArgs e)
        {
            width = docWrapp.Width;
            height = docWrapp.Height;
            mainGrid.WidthRequest = width;
            mainGrid.HeightRequest = height;


            Console.WriteLine("ABOBA");

            SetStartMenu();
            ShowStartMenu();
            SetGameField();
            HideGameField();
        }



        #region StartMenu

        void SetStartMenu() {
            double constMT = height * 0.01 * (100.0 - 10.0 - 25.0 - 25.0) / 2.5;
            menuWrapp.Margin = new Thickness(0, constMT, 0, 0);

            AbsoluteLayout textPlayWrapp = CreateTextPlay();
            AbsoluteLayout logoWrapp = CreateLogoWrapp();
            AbsoluteLayout playWrapp = CreatePlayWrapp();
            AbsoluteLayout functionalBtnsWrapp = CreateFunctionalBtnsWrapp();

            menuWrapp.Children.Add(textPlayWrapp);
            menuWrapp.Children.Add(logoWrapp);
            menuWrapp.Children.Add(playWrapp);
            menuWrapp.Children.Add(functionalBtnsWrapp);
        }
        void ShowStartMenu()
        {
            menuWrapp.TranslationX = 0;
        }
        void HideStartMenu()
        {
            menuWrapp.TranslationX = 2000;
            swipeAllowed = false;
        }

        AbsoluteLayout CreateTextPlay()
        {
            AbsoluteLayout textPlayWrapp = new AbsoluteLayout();
            textPlayWrapp.WidthRequest = width;
            textPlayWrapp.HeightRequest = height * (13.0 / 100.0);

            Label textPlay = new Label();
            textPlay.FontSize = 66;
            textPlay.Text = "Play the";
            textPlay.TextColor = Color.Black;
            textPlay.WidthRequest = width;
            textPlay.HorizontalTextAlignment = TextAlignment.Center;
            textPlay.FontFamily = "NerisSemiBold.otf#NerisSemiBold";

            textPlayWrapp.Children.Add(textPlay);
            return textPlayWrapp;

        }

        AbsoluteLayout CreateLogoWrapp()
        {
            double topMargin = height * (10.0 / 100.0);
            double logoHeight = height * (25.0 / 100.0);
            AbsoluteLayout logoWrapp = new AbsoluteLayout();
            logoWrapp.Margin = new Thickness(0, topMargin, 0, 0);
            logoWrapp.WidthRequest = width;
            logoWrapp.HeightRequest = logoHeight;


            // flying squares 
            string[] textArray = { "2", "0", "4", "8" };
            int[] marginsArray = { -4, -3, 0, 0 };
            int[] edgesArray = { 5, -5, 5, -5 };
            int[] rotationsArray = { -7, 4, -1, 1 };
            int[] colorsInt = { 2, 1024, 16, 8 };
            int flySpeed = 1700;
            for (int i = 0; i < 4; i++)
            {
                AbsoluteLayout square = new AbsoluteLayout();
                double squareSize = 5.6;
                square.HeightRequest = width / squareSize;
                square.WidthRequest = width / squareSize;
                double marginLeft = (width / 21.0) + (i * width / squareSize) + (20 * i);
                double marginTop = marginsArray[i] + (logoHeight - width / squareSize) / 2;
                square.Margin = new Thickness(marginLeft, marginTop, 0, 0);


                Button label = new Button();
                label.FontSize = 58;
                label.HeightRequest = 1.2 * width / squareSize;
                label.WidthRequest = 1.2 * width / squareSize;
                label.Margin = new Thickness(1, 0, 0, 0);
                label.Text = textArray[i];
                label.FontFamily = "NerisLightItalic.otf#NerisLightItalic";
                label.TextColor = Color.Black;
                label.BackgroundColor = Color.Transparent;
                label.Rotation = rotationsArray[i];

                Frame border = new Frame();
                border.HeightRequest = 1 + (width / squareSize) * 0.7;
                border.WidthRequest = 1 + (width / squareSize) * 0.7;
                border.BorderColor = Color.Black;
                border.BackgroundColor = Color.FromHex(GetColor(colorsInt[i]));
                border.CornerRadius = 4;
                border.Rotation = rotationsArray[i];


                square.Children.Add(border);
                square.Children.Add(label);
                logoWrapp.Children.Add(square);

                FlyingSquares(square, marginsArray[i], edgesArray[i], 2300, true); ;

            }

            return logoWrapp;
        }
        async void FlyingSquares(AbsoluteLayout square, int current, int edge, int duration, bool needToWait)
        {
            int delta = edge / Math.Abs(edge);
            if (needToWait)
                await Task.Delay(Math.Abs(current + edge * delta) * 300);

            if (current < edge)
            {
                current += delta;
                square.TranslateTo(0, edge, (uint)duration);
                await Task.Delay((int)Math.Floor((double)duration * 1.09));
                FlyingSquares(square, current, -1 * edge, duration, false);
            }
            if (current > edge)
            {
                current += delta;
                square.TranslateTo(0, edge, (uint)duration);
                await Task.Delay((int)Math.Floor((double)duration * 1.09));
                FlyingSquares(square, current, -1 * edge, duration, false);
            }
        }

        AbsoluteLayout CreatePlayWrapp()
        {
            double topMargin = height * ((10.0 + 25.0) / 100.0);
            double logoHeight = height * (25.0 / 100.0);
            AbsoluteLayout playWrapp = new AbsoluteLayout();
            playWrapp.Margin = new Thickness(0, topMargin + 1 * logoHeight / 8, 0, 0);
            playWrapp.WidthRequest = width;

            AbsoluteLayout playBtnWrapp = CreatePlayBtnWrapp();
            playWrapp.Children.Add(playBtnWrapp);
            CreatePlayIco(playBtnWrapp);


            return playWrapp;
        }
        AbsoluteLayout CreatePlayBtnWrapp()
        {
            double btnSize = (width / 3.5);
            AbsoluteLayout playBtnWrapp = new AbsoluteLayout();
            playBtnWrapp.HeightRequest = btnSize;
            playBtnWrapp.WidthRequest = btnSize;
            playBtnWrapp.Margin = new Thickness((width - btnSize) / 2, 40, (width - btnSize) / 2, 40);

            return playBtnWrapp;
        }
        void CreatePlayIco(AbsoluteLayout playBtnWrapp)
        {
            double scaling = 1.3;
            Image playTriangle = new Image();
            playTriangle.Source = ImageSource.FromFile("playTriangle.png");
            playTriangle.ScaleX = scaling;
            playTriangle.ScaleY = scaling;
            Image playCircle = new Image();
            playCircle.Source = ImageSource.FromFile("playCircle.png");
            playCircle.ScaleX = scaling;
            playCircle.ScaleY = scaling;
            Image playCircleBig = new Image();
            playCircleBig.Source = ImageSource.FromFile("playCircle.png");
            playCircleBig.ScaleX = scaling + 0.2;
            playCircleBig.ScaleY = scaling + 0.2;
            playCircleBig.Rotation = -20;


            playBtnWrapp.Children.Add(playTriangle);
            playBtnWrapp.Children.Add(playCircle);
            playBtnWrapp.Children.Add(playCircleBig);
            RotateAnim(playCircle, playCircleBig);


            TapGestureRecognizer TapGesture = new TapGestureRecognizer();
            TapGesture.Tapped += (s, e) => {
                HideStartMenu();
                ShowGameField();
            };
            playBtnWrapp.GestureRecognizers.Add(TapGesture);
        }
        async void RotateAnim(Image playCircle, Image playCircleBig)
        {

            playCircle.RotateTo(playCircle.Rotation + 180, 5000);
            playCircleBig.RotateTo(playCircleBig.Rotation - 90, 10000);
            await Task.Delay(5000);
            playCircle.RotateTo(playCircle.Rotation + 180, 5000);
            await Task.Delay(5000);
            RotateAnim(playCircle, playCircleBig);
        }


        AbsoluteLayout CreateFunctionalBtnsWrapp()
        {
            double topMargin = height * ((10.0 + 25.0 + 25.0 + 6.0) / 100.0);
            AbsoluteLayout functionalBtnsWrapp = new AbsoluteLayout();
            functionalBtnsWrapp.WidthRequest = width;
            functionalBtnsWrapp.BackgroundColor = Color.Transparent;
            functionalBtnsWrapp.Margin = new Thickness(0, constMT + topMargin, 0, 0);


            Frame hightScoresFrame = new Frame
            {
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(width * 0.11, 0, 0, 0),
                Padding = new Thickness(0, 0, 0, 0),
                HorizontalOptions = LayoutOptions.Center,
                Rotation = Rotation - 10,
                ScaleX = 1.5,
                ScaleY = 1.25,
                Content = new Image
                {
                    Source = ImageSource.FromFile("winner.png")
                }
            };
            Frame settingsFrame = new Frame
            {
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(width * (1.0 - 0.11) - 80, 0, 0, 0),
                Padding = new Thickness(0, 0, 0, 0),
                HorizontalOptions = LayoutOptions.Center,
                Rotation = Rotation + 100,
                ScaleX = 1.1,
                ScaleY = 1.1,
                Content = new Image
                {
                    Source = ImageSource.FromFile("settings.png")
                }
            };


            functionalBtnsWrapp.Children.Add(hightScoresFrame);
            functionalBtnsWrapp.Children.Add(settingsFrame);
            return functionalBtnsWrapp;
        }

        #endregion


        #region GameField

        #region variables
        static int gameSize;
        double gameCaseSize;
        double fieldWidth;
        double fieldHeight;
        double constMT;
        double constLT;

        int[] fieldArray = new int[0];
        int[][] pastFields = new int[0][];
        Label[] elements = new Label[0];
        AbsoluteLayout[] Rows = new AbsoluteLayout[gameSize];
        AbsoluteLayout[] AnimLayouts = new AbsoluteLayout[64];
        Label[] AnimElements = new Label[64];

        int score;
        int[] pastScores = new int[0];
        int scoreHolder;

        bool swipeAllowed;
        bool wereChanges;
        int linesProcessed;

        int animAmount;
        uint animSpeed;
        double speedMultiply;

        bool isGameGoing;
        Frame loseWrapp;
        #endregion

        void SetGameField() {
            ResetGameValues();
            Array.Resize(ref elements, gameSize * gameSize);
            Array.Resize(ref fieldArray, gameSize * gameSize);
            Array.Resize(ref Rows, gameSize);


            GameGrid();
            NavGrid();
            SwipeGuesture();
            LoseGrid();
            TrySetNewElem();
        }
        void ShowGameField() {
            gameWrapp.TranslationX = 0;
            swipeAllowed = true;
            ResetGameField(false);
        }
        void HideGameField() {
            gameWrapp.TranslationX = 2000;
            swipeAllowed = false;
            isGameGoing = false;
        }


        void ResetGameValues() {
            gameSize = 4;
            gameCaseSize = (((width - 2 * 10) - 2 * 10) - (gameSize - 1) * 5) / gameSize;
            fieldWidth = width - 2 * 10;
            fieldHeight = gameCaseSize * gameSize + 2 * 10 + (gameSize - 1) * 5;
            constMT = 65.0 * (height - (width - (gameSize - 1) * 5)) / 100.0;
            constLT = 10;

            score = 0;
            scoreHolder = 0;

            swipeAllowed = true;
            wereChanges = true;
            linesProcessed = 0;

            animAmount = 0;
            animSpeed = 80;
            speedMultiply = 1 - (10 / (double)animSpeed);

            isGameGoing = true;
        }



        // Grid of gameWrapp
        void GameGrid() {
            gameWrapp.Margin = new Thickness(0, 0, 0, 0);

            AbsoluteLayout fieldWrapp = new AbsoluteLayout();
            fieldWrapp.WidthRequest = fieldWidth;
            fieldWrapp.HeightRequest = fieldHeight;
            fieldWrapp.Margin = new Thickness(constLT, constMT, 0, 0);

            Frame borderRadius = new Frame();
            borderRadius.WidthRequest = fieldWidth;
            borderRadius.HeightRequest = fieldHeight;
            borderRadius.Margin = new Thickness(0, 0, 0, 0);
            borderRadius.Padding = new Thickness(0, 0, 0, 0);
            borderRadius.BackgroundColor = Color.FromHex("#AFA095");
            borderRadius.CornerRadius = (int)(fieldHeight * 0.013);

            fieldWrapp.Children.Add(borderRadius);
            gameWrapp.Children.Add(fieldWrapp);

            // fill fieldWrapp
            for (int i = 0; i < gameSize; i++)
            {
                AbsoluteLayout gameRow = new AbsoluteLayout();
                Rows[i] = gameRow;
                gameRow.WidthRequest = width;
                gameRow.Margin = new Thickness(0, constLT + 5 * i + gameCaseSize * i, 0, 0);

                for (int k = 0; k < gameSize; k++)
                {
                    Label gameCase = new Label();
                    gameCase.WidthRequest = gameCaseSize;
                    gameCase.HeightRequest = gameCaseSize;
                    gameCase.TextColor = Color.Black;
                    gameCase.FontSize = 40;
                    gameCase.FontFamily = "NerisLightItalic.otf#NerisLightItalic";
                    gameCase.HorizontalTextAlignment = TextAlignment.Center;
                    gameCase.VerticalTextAlignment = TextAlignment.Center;
                    gameCase.Text = " ";
                    gameCase.BackgroundColor = Color.FromHex("CCC2B5");
                    gameCase.Margin = new Thickness(constLT + 5 * k + gameCaseSize * k, 0, 0, 0);
                    elements[i * gameSize + k] = gameCase;

                    gameRow.Children.Add(gameCase);


                    ////////////////////
                    ////////////////////
                    ////////////////////
                    ////////////////////
                    AbsoluteLayout animLayout = new AbsoluteLayout();
                    animLayout.TranslationX = 2000; // hiding
                    AnimLayouts[i * gameSize + k] = animLayout;
                    Label animCase = new Label();
                    AnimElements[i * gameSize + k] = animCase;
                    animLayout.Children.Add(animCase);
                    animGrid.Children.Add(animLayout);
                    animCase.TextColor = Color.Black;
                    animCase.FontFamily = "NerisLightItalic.otf#NerisLightItalic";
                    animCase.HorizontalTextAlignment = TextAlignment.Center;
                    animCase.VerticalTextAlignment = TextAlignment.Center;
                    animCase.WidthRequest = gameCaseSize;
                    animCase.HeightRequest = gameCaseSize;
                    animLayout.WidthRequest = gameCaseSize;
                    animLayout.HeightRequest = gameCaseSize;

                }
                fieldWrapp.Children.Add(gameRow);
            }

        }
        void NavGrid()
        {
            AbsoluteLayout navWrapp = new AbsoluteLayout();
            navWrapp.WidthRequest = fieldWidth;
            double navHeight = 40 + 40 + 10 + 10 + 5;
            navWrapp.HeightRequest = navHeight;
            navWrapp.Margin = new Thickness(constLT, constMT - constLT - navHeight, 0, 0);

            Frame borderRadius = new Frame
            {
                WidthRequest = fieldWidth,
                HeightRequest = navHeight,
                Margin = new Thickness(0, 0, 0, 0),
                Padding = new Thickness(0, 0, 0, 0),
                BackgroundColor = Color.FromHex("#AFA095"),
                CornerRadius = 4,
                IsEnabled = false
            };


            Frame scoreFrame = new Frame
            {
                HeightRequest = 40,
                BackgroundColor = Color.FromHex(GetColor(2)),
                CornerRadius = 4,
                Margin = new Thickness(10, 55, 0, 0),
                Padding = new Thickness(5, 0, 5, 0),
                HorizontalOptions = LayoutOptions.Center,
                Content = new Label
                {
                    Text = Convert.ToString(score),
                    FontSize = 20,
                    TextColor = Color.Black,
                    VerticalTextAlignment = TextAlignment.Center
                }
            };
            scoreFrame.Content.PropertyChanged += (s, e) => {
                ScoreUpdate((Label)s);
            };
            Frame hightScoreFrame = new Frame
            {
                HeightRequest = 40,
                BackgroundColor = Color.FromHex(GetColor(2)),
                CornerRadius = 4,
                Margin = new Thickness(10 + 110 + 10, 10, 0, 0),
                Padding = new Thickness(5, 0, 5, 0),
                HorizontalOptions = LayoutOptions.Center,
                Content = new Label
                {
                    Text = "Hight Score: 62320",
                    TextColor = Color.Black,
                    FontSize = 20,
                    VerticalTextAlignment = TextAlignment.Center
                }
            };



            Label timeLabel = new Label();
            timeLabel.Text = TimerValue(0, timeLabel);
            timeLabel.TextColor = Color.Black;
            timeLabel.FontSize = 20;
            timeLabel.VerticalTextAlignment = TextAlignment.Center;
            Frame timerFrame = new Frame
            {
                HeightRequest = 40,
                BackgroundColor = Color.FromHex(GetColor(2)),
                CornerRadius = 4,
                Margin = new Thickness(10, 10, 0, 0),
                Padding = new Thickness(5, 0, 5, 0),
                HorizontalOptions = LayoutOptions.Center,
                Content = timeLabel
            };



            Frame homeFrame = new Frame
            {
                HeightRequest = 40,
                WidthRequest = 40,
                BackgroundColor = Color.FromHex("#555555"),
                CornerRadius = 4,
                Margin = new Thickness(fieldWidth - 50, 10, 0, 0),
                Padding = new Thickness(0, 0, 0, 0),
                HorizontalOptions = LayoutOptions.Center,
                Content = new Image
                {
                    Source = ImageSource.FromFile("house.png"),
                    ScaleX = 1,
                    ScaleY = 1
                }
            };
            TapGestureRecognizer homeTap = new TapGestureRecognizer();
            homeTap.Tapped += (s, e) => {
                ShowStartMenu();
                HideGameField();
            };
            homeFrame.GestureRecognizers.Add(homeTap);

            Frame stepBackFrame = new Frame
            {
                HeightRequest = 40,
                WidthRequest = 40,
                BackgroundColor = Color.FromHex("#555555"),
                CornerRadius = 4,
                Margin = new Thickness(fieldWidth - 10 - 80 - 5, 55, 0, 0),
                Padding = new Thickness(0, 0, 0, 0),
                HorizontalOptions = LayoutOptions.Center,
                Content = new Image
                {
                    Source = ImageSource.FromFile("left.png"),
                    ScaleX = 0.7,
                    ScaleY = 0.7
                }
            };
            TapGestureRecognizer stepBackTap = new TapGestureRecognizer();
            stepBackTap.Tapped += (s, e) => {
                SetPreviousField();
            };
            stepBackFrame.GestureRecognizers.Add(stepBackTap);


            Frame resetGameFrame = new Frame
            {
                HeightRequest = 40,
                WidthRequest = 40,
                BackgroundColor = Color.FromHex("#555555"),
                CornerRadius = 4,
                Margin = new Thickness(fieldWidth - 10 - 40, 55, 0, 0),
                Padding = new Thickness(0, 0, 0, 0),
                HorizontalOptions = LayoutOptions.Center,
                Content = new Image
                {
                    Source = ImageSource.FromFile("sync.png"),
                    ScaleX = 0.7,
                    ScaleY = 0.7
                }
            };
            TapGestureRecognizer resetGameTap = new TapGestureRecognizer();
            resetGameTap.Tapped += (s, e) => {
                ResetGameField(true);
            };
            resetGameFrame.GestureRecognizers.Add(resetGameTap);



            navWrapp.Children.Add(borderRadius);
            navWrapp.Children.Add(scoreFrame);
            // navWrapp.Children.Add(hightScoreFrame);
            navWrapp.Children.Add(timerFrame);
            navWrapp.Children.Add(homeFrame);
            navWrapp.Children.Add(stepBackFrame);
            navWrapp.Children.Add(resetGameFrame);


            gameWrapp.Children.Add(navWrapp);
        }
        void SwipeGuesture() {
            SwipeView swipeCopy = swipeWrapp;
            swipeCopy.WidthRequest = 999999;
            swipeCopy.HeightRequest = (height - constMT);
            swipeCopy.Margin = new Thickness(0, constMT, 0, 0);
            swipeCopy.BackgroundColor = Color.Transparent;
            int swipeDuration = 0;
            swipeCopy.SwipeEnded += (s, e) => {
                if (swipeAllowed && swipeDuration > 3)
                {
                    if (e.SwipeDirection == SwipeDirection.Right)
                    {
                        gameMove(0);
                    }
                    if (e.SwipeDirection == SwipeDirection.Left)
                    {
                        gameMove(1);
                    }
                    if (e.SwipeDirection == SwipeDirection.Up)
                    {
                        gameMove(2);
                    }
                    if (e.SwipeDirection == SwipeDirection.Down)
                    {
                        gameMove(3);
                    }
                }
                swipeDuration = 0;
                swipeCopy.Close();
            };
            swipeCopy.SwipeChanging += (s, e) => {
                swipeDuration++;
            };
            gameWrapp.Children.RemoveAt(0);
            gameWrapp.Children.Add(swipeCopy);
        }
        void LoseGrid()
        {
            loseWrapp = new Frame
            {
                HeightRequest = fieldHeight,
                WidthRequest = fieldWidth,
                Margin = new Thickness(10, constMT, 0, 0),
                Padding = 0,
                BackgroundColor = Color.FromHex("#BB222222"),
                CornerRadius = (int)(fieldHeight * 0.013),
                HorizontalOptions = LayoutOptions.Center,
                TranslationX = 2000,
                Content = new Label
                {
                    FontSize = 50,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = Color.White,
                    Text = "You've lost... :("
                }
            };
            gameWrapp.Children.Add(loseWrapp);
        }


        // NavBar functions
        void ScoreUpdate(Label scoreLabel)
        {
            string scoreText = "Score: ";
            scoreText += Convert.ToString(score);

            Device.StartTimer(TimeSpan.FromMilliseconds(1), () => {
                ScoreUpdate(scoreLabel);
                return false;
            });
            scoreLabel.Text = scoreText;
        }
        string TimerValue(int currentTime, Label timeLabel)
        {
            currentTime++;
            string timeText = "";
            int hours = (currentTime - (currentTime % 3600)) / 3600;
            int min = (currentTime - (currentTime % 60)) / 60;
            int sec = currentTime - hours * 36 - min * 60;
            if (hours > 0)
            {
                timeText += hours < 10 ? "0" + Convert.ToString(hours) : Convert.ToString(hours);
                timeText += ":";
            }
            timeText += min < 10 ? "0" + Convert.ToString(min) : Convert.ToString(min);
            timeText += ":";
            timeText += sec < 10 ? "0" + Convert.ToString(sec) : Convert.ToString(sec);

            bool passed = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(1), () => {
                if (!passed || !isGameGoing)
                {
                    currentTime = isGameGoing ? currentTime : 0;
                    if (isLose())
                    {
                        currentTime = currentTime - 1;
                    }
                    TimerValue(currentTime, timeLabel);
                    passed = false;
                }
                return passed;
            });
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                passed = false;
                return false;
            });
            timeLabel.Text = timeText;
            return timeText;
        }
        async void SetPreviousField()
        {
            if (pastFields.Length > 1 && swipeAllowed)
            {
                swipeAllowed = false;
                isGameGoing = true;
                HideLose();
                Array.Resize(ref pastFields, pastFields.Length - 1);
                Array.Resize(ref pastScores, pastScores.Length - 1);
                for (int i = 0; i < gameSize * gameSize; i++)
                {
                    fieldArray[i] = pastFields[pastFields.Length - 1][i];
                    if (fieldArray[i] > 512)
                    {
                        elements[i].FontSize = 36;
                    }
                    else
                    {
                        elements[i].FontSize = 40;
                    }
                    elements[i].Text = fieldArray[i] == 0 ? " " : Convert.ToString(fieldArray[i]);
                    elements[i].BackgroundColor = Color.FromHex(GetColor(fieldArray[i] == 0 ? 1 : fieldArray[i]));
                }
                score = pastScores[pastScores.Length - 1];
                await Task.Delay(70);
                swipeAllowed = true;
            }
        }
        async void ResetGameField(bool needToWait)
        {
            if (swipeAllowed)
            {
                isGameGoing = false;
                HideLose();
                if (needToWait)
                    await Task.Delay(10);
                ResetGameValues();
                for (int i = 0; i < gameSize * gameSize; i++)
                {
                    fieldArray[i] = 0;
                    elements[i].Text = " ";
                    elements[i].FontSize = 40;
                    elements[i].BackgroundColor = Color.FromHex(GetColor(1));
                }
                Array.Resize(ref pastFields, 0);
                TrySetNewElem();
            }
        }

        // Move&swipe event 
        void gameMove(int direction) {
            animAmount = 0;
            swipeAllowed = false;
            wereChanges = false;
            linesProcessed = 0;
            scoreHolder = 0;

            for (int k = 0; k < gameSize; k++)
                CreateLineArray(direction > 1 ? 1 : 0, (direction == 1 || direction == 2) ? 1 : 0, k);
        }

        void CreateLineArray(int isVertical, int isReverse, int lineNumber)
        {
            int[] lineArray = new int[gameSize];
            for (int t = 0; t < gameSize; t++)
            {
                int vertIndex = isVertical * (t * gameSize + lineNumber);
                int horizIndex = Math.Abs(isVertical - 1) * (lineNumber * gameSize + t);
                lineArray[t] = fieldArray[vertIndex + horizIndex];
            }
            ZeroAside(lineArray, isVertical, isReverse, lineNumber, true);
        }
        int[] ZeroAside(int[] lineArray, int isVertical, int isReverse, int lineNumber, bool firstCalling)
        {
            int elemIndex = isReverse == 1 ? 0 : (gameSize - 1);
            int dCounter = isReverse == 1 ? 1 : -1;
            int forLimit = isReverse == 1 ? gameSize : -1;
            int maxDuration = 1;
            for (int i = isReverse == 1 ? 0 : (gameSize - 1); ((-1 * dCounter) * i > (-1 * dCounter) * forLimit); i += dCounter)
            {
                if (lineArray[i] != 0)
                {
                    if (elemIndex == i)
                    {
                        elemIndex += dCounter;
                        continue;
                    }
                    lineArray[elemIndex] = lineArray[i];
                    lineArray[i] = 0;

                    animAmount++;
                    int animDuration = Math.Abs(i - elemIndex) * (int)animSpeed;
                    maxDuration = maxDuration < animDuration ? animDuration : maxDuration;
                    Animate(isVertical, isReverse, lineNumber, i, elemIndex, 0);
                    elemIndex += dCounter;
                }
            }
            if (firstCalling)
                FinishLines(lineArray, isVertical, isReverse, lineNumber, maxDuration);
            return lineArray;
        }

        async void FinishLines(int[] lineArray, int isVertical, int isReverse, int lineNumber, int delay)
        {
            int addAmount = 0;
            for (int i = 0; i < lineArray.Length - 1; i++)
            {
                if (lineArray[i] == lineArray[i + 1] && lineArray[i] != 0)
                {
                    addAmount++;
                    i++;
                }
            }


            // нужно сделать универсальный ревёрс 
            await Task.Delay((int)((double)delay * speedMultiply));
            if (isReverse == 1)
            {
                for (int h = 0; h < gameSize - 1; h++)
                {
                    int thisIndex, nextIndex;
                    thisIndex = h;
                    nextIndex = thisIndex + 1;
                    if (lineArray[thisIndex] == lineArray[nextIndex] && lineArray[thisIndex] != 0)
                    {
                        wereChanges = true;
                        Animate(isVertical, isReverse, lineNumber, nextIndex, thisIndex, 1);
                        animAmount++;
                        scoreHolder += lineArray[thisIndex] * 2;
                        lineArray[thisIndex] *= 2;
                        lineArray[nextIndex] = 0;
                        lineArray = ZeroAside(lineArray, isVertical, isReverse, lineNumber, false);
                        await Task.Delay((int)animSpeed + 1);
                    }
                }
            }
            else
            {
                for (int h = gameSize - 1; h > 0; h--)
                {
                    int thisIndex, previousIndex;
                    thisIndex = h;
                    previousIndex = thisIndex - 1;
                    if (lineArray[thisIndex] == lineArray[previousIndex] && lineArray[thisIndex] != 0)
                    {
                        wereChanges = true;
                        Animate(isVertical, isReverse, lineNumber, previousIndex, thisIndex, 1);
                        animAmount++;
                        scoreHolder += lineArray[thisIndex] * 2;
                        lineArray[thisIndex] *= 2;
                        lineArray[previousIndex] = 0;
                        lineArray = ZeroAside(lineArray, isVertical, isReverse, lineNumber, false);
                        await Task.Delay((int)animSpeed + 1);
                    }
                }
            }

            if (addAmount > 0)
                await Task.Delay(1 + (int)animSpeed * addAmount);
            if (delay > 1)  // means was Animations =>
                wereChanges = true;


            SetCase(lineArray, isVertical, lineNumber);

            if (lineNumber == gameSize - 1)
            {
                while (animAmount > 0 || linesProcessed != gameSize)
                    await Task.Delay(1);
                TrySetNewElem();
            }
        }

        void SetCase(int[] lineArray, int isVertical, int lineNumber)
        {
            for (int t = 0; t < gameSize; t++)
            {
                int vertIndex = isVertical * (t * 4 + lineNumber);
                int horizIndex = (1 - isVertical) * (lineNumber * 4 + t);
                fieldArray[vertIndex + horizIndex] = lineArray[t];
                if (lineArray[t] != 0)
                {
                    if (lineArray[t] > 512)
                    {
                        elements[vertIndex + horizIndex].FontSize = 36;
                    }
                    elements[vertIndex + horizIndex].Text = lineArray[t].ToString();
                    elements[vertIndex + horizIndex].BackgroundColor = Color.FromHex(GetColor(lineArray[t]));
                }
                else
                {
                    elements[vertIndex + horizIndex].Text = " ";
                    elements[vertIndex + horizIndex].BackgroundColor = Color.FromHex(GetColor(1));
                }
            }
            linesProcessed++;
        }
        int TrySetNewElem()
        {
            int[] freeCases = new int[0];
            for (int i = 0; i < fieldArray.Length; i++)
            {
                if (fieldArray[i] == 0)
                {
                    Array.Resize(ref freeCases, freeCases.Length + 1);
                    freeCases[freeCases.Length - 1] = i;
                }
            }

            if (!wereChanges)
            {
                swipeAllowed = true;
                return -1;
            }


            int tempRandom = new Random().Next(0, freeCases.Length - 1);
            int randomValue = new Random().Next(0, 100);
            int valueToSet = randomValue > 95 ? 4 : 2;
            fieldArray[freeCases[tempRandom]] = valueToSet;
            elements[freeCases[tempRandom]].Text = Convert.ToString(valueToSet);
            elements[freeCases[tempRandom]].BackgroundColor = Color.FromHex(GetColor(valueToSet));


            appearenceAnimation(freeCases[tempRandom]);

            score += scoreHolder;

            Array.Resize(ref pastFields, pastFields.Length + 1);
            Array.Resize(ref pastFields[pastFields.Length - 1], gameSize * gameSize);
            for (int i = 0; i < gameSize * gameSize; i++)
            {
                pastFields[pastFields.Length - 1][i] = fieldArray[i];
            }
            Array.Resize(ref pastScores, pastScores.Length + 1);
            pastScores[pastScores.Length - 1] = score;



            if (freeCases.Length == 1)
            {
                Lose();
                return -1;
            }




            return freeCases[tempRandom];
        }

        // Animations
        async void Animate(int isVertical, int isReverse, int lineNumber, int i, int elemIndex, int add)
        {
            int currIndex = isVertical == 1 ? lineNumber + i * gameSize : i + lineNumber * gameSize;
            int nextIndex = isVertical == 1 ? lineNumber + elemIndex * gameSize : elemIndex + lineNumber * gameSize;
            ConfigureAnimElem(elements[currIndex], currIndex);

            uint correctedSpeed = isVertical == 1 ? (uint)Math.Abs(currIndex - nextIndex) * animSpeed / 4 : (uint)Math.Abs(currIndex - nextIndex) * animSpeed;
            int valueHolder = fieldArray[currIndex] + add * fieldArray[currIndex];
            elements[currIndex].BackgroundColor = Color.FromHex(GetColor(1));
            MoveFromTo(elements[currIndex], currIndex, elements[nextIndex], nextIndex, currIndex, correctedSpeed);
            elements[currIndex].Text = " "; fieldArray[currIndex] = 0;

            await Task.Delay((int)((double)correctedSpeed * (speedMultiply - 0.0125)));
            fieldArray[nextIndex] = valueHolder;
            elements[nextIndex].Text = valueHolder.ToString();
            elements[nextIndex].BackgroundColor = Color.FromHex(GetColor(valueHolder));
            AnimLayouts[currIndex].IsVisible = false;
            animAmount--;
            if (add == 1) {
                animAmount++;
                elements[nextIndex].ScaleXTo(1.1, 60);
                elements[nextIndex].ScaleYTo(1.1, 60);
                elements[nextIndex].FontSize = elements[nextIndex].FontSize + 5;
                await Task.Delay(30);
                elements[nextIndex].ScaleXTo(1, 60);
                elements[nextIndex].ScaleYTo(1, 60);
                elements[nextIndex].FontSize = elements[nextIndex].FontSize - 5;
                animAmount--;
            }
        }
        void ConfigureAnimElem(Label mainElem, int index)
        {
            AnimElements[index].Text = mainElem.Text;
            AnimElements[index].FontSize = mainElem.FontSize;
            AnimElements[index].BackgroundColor = mainElem.BackgroundColor;

            AnimLayouts[index].TranslationX = constLT + mainElem.Bounds.X;
            AnimLayouts[index].TranslationY = constMT + Rows[(index - (index % gameSize)) / gameSize].Bounds.Y;
        }
        void MoveFromTo(Label mainElem, double k, Label nextElem, double n, int layoutIndex, uint correctedSpeed)
        {
            double x = constLT + mainElem.Bounds.X + (nextElem.Bounds.X - mainElem.Bounds.X);
            double y = constMT + Rows[((int)n - ((int)n % gameSize)) / gameSize].Bounds.Y;
            AnimLayouts[layoutIndex].IsVisible = true;
            AnimLayouts[layoutIndex].TranslateTo(x, y, correctedSpeed);
            //await Task.Delay((int)(correctedSpeed*1.1));

        }
        async void appearenceAnimation(int index)
        {
            elements[index].ScaleTo(0.1, animSpeed);
            await Task.Delay(1);
            elements[index].ScaleTo(1, (uint)(animSpeed * 1.5));
            swipeAllowed = true;
        }


        // Lose event
        bool isLose()
        {
            bool isLose = true;
            for (int i = 0; i < gameSize; i++)
            {
                for (int j = 0; j < gameSize - 1; j++)
                {
                    if (fieldArray[i * gameSize + j] == fieldArray[i * gameSize + j + 1])
                        isLose = false;
                    if (fieldArray[j * gameSize + i] == fieldArray[(j + 1) * gameSize + i])
                        isLose = false;
                }
            }
            return isLose;
        }
        void Lose()
        {
            if (isLose())
            {
                ShowLose();
                isGameGoing = false;
            }
        }

        void ShowLose()
        {
            loseWrapp.TranslationX = 0;
        }
        void HideLose()
        {
            loseWrapp.TranslationX = 2000;
        }

        #endregion

    }
}
