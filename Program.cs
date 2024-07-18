using static System.Net.Mime.MediaTypeNames;

namespace ConsoleProject1
/**************************************************************************
 * 콘솔 프로젝트 1
 * 최연승
 * 2번) 2차원 배열과 Console ReadKey, 혹은 ReadLine을 통해 캐릭터 이동 구현하기.
 * 
 * 쿠쿠세이튼 2관 세토 숨바꼭질
 * 플레이어게 랜덤한 문양이 주어짐 ♠ ♥ ♣ ◆
 * 맵에 랜덤한 위치에 등장하는 같은 문양을 먹어야함
 * 맵에 방해꾼인 세토가 등장함 "＠" 닿으면 리셋
 * 3번 먹어으면 탈출구가 나옴
 * 그리고 탈출구로 탈출 하면 끝
 * 
 **************************************************************************/


{
    internal class Program
    {
        public static int RandomNumX(int min, int max)
        {
            int randNum1;
            Random rand = new Random();
            randNum1 = rand.Next(min, max);
            // 짝수에 벽이 있으니 홀수는 피하기
            while (randNum1 % 2 != 0)
            {
                randNum1 = rand.Next(min, max);
            }
            return randNum1;
        }

        public static int RandomNumY(int min, int max)
        {
            int randNum2;
            Random rand = new Random();
            randNum2 = rand.Next(min, max);
            // 짝수에 벽이 있으니 홀수는 피하기
            while (randNum2 % 2 == 0)
            {
                randNum2 = rand.Next(min, max);
            }
            return randNum2;
        }
        // 랜덤수 만들기 테스트
        public static int RandomNumber(int a, int b)
        {
            Random r = new Random();
            int result = r.Next(a, b);

            return result;
        }



        public struct GameData
        {
            public bool running;
            public bool[,] map;
            public ConsoleKey inputKey;
            public Point playerLocation;
            public Point goalLocation;
            public Point spadeLocation;
            public Point heartLocation;
            public Point cloverLocation;
            public Point diamondLocation;
            public Point setoLocationV;
            public Point setoLocationH;

            public Point setoLocation;

            public Point setoV1;
            public Point setoV2;
            public Point setoV3;
            public Point setoV4;
            public Point setoV5;
            public Point setoV6;
            public Point setoV7;
            public Point setoV8;
            public Point setoV9;

        }

        public struct Point
        {
            public int x;
            public int y;
            // 세토꺼
            // 위에서 아래로
            public int setoV1x;
            public int setoV1y;
            public int setoV2x;
            public int setoV2y;
            public int setoV3x;
            public int setoV3y;
            public int setoV4x;
            public int setoV4y;
            public int setoV5x;
            public int setoV5y;
            public int setoV6x;
            public int setoV6y;
            public int setoV7x;
            public int setoV7y;
            public int setoV8x;
            public int setoV8y;
            public int setoV9x;
            public int setoV9y;


            public int setoVx;
            public int setoVy;


            // 왼쪽에서 오른쪽
            public int setoHx;
            public int setoHy;

        }

        static GameData data;

        // 글자 색깔 꾸미기: 글씨하나 밖에 안되고 아직 잘 안됨
        /* 수정/보강 필요
        void ColorLetter(string letters, char c)
        {
            var outcome = letters.IndexOf(c);
            Console.Write(letters.Substring(0, outcome));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(letters[outcome]);
            Console.ResetColor();
            Console.WriteLine(letters.Substring(outcome + 1));
        }
        */
        static void Main(string[] args)
        {

            Start();
            while (data.running) // data.running 데이터 러닝
            {
                Render();
                Input();
                Update();
            }
            Collision(); // 수정해야됨
            End();
        }

        static void Start()
        {
            // 게임 시작 
            //커서 반짝이 안보이기
            Console.CursorVisible = false;

            data = new GameData();

            data.running = true;
            data.map = new bool[,]
            {   //  0      1      2      3      4      5      6       7     8       9     10     11     12     13     14     15    16      17     18
 /*  1 */     { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false }, 
 /*  2 */     { false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false },
 /*  3 */     { false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false },
 /*  4 */     { false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false },
 /*  5 */     { false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false },
 /*  6 */     { false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false },
 /*  7 */     { false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false },
 /*  8 */     { false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false },
 /*  9 */     { false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false },
 /* 10 */     { false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false },
 /* 11 */     { false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false },
 /* 12 */     { false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false },
 /* 13 */     { false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false,  true, false },
 /* 14 */     { false,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true,  true, false },
 /* 15 */     { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            };

            #region 좌표
            // 랜덤 스폰위치
            // x-axis
            int xMin = 2;
            int xMax = 17;
            // y-axis
            int yMin = 2;
            int yMax = 13;
            // 각자 랜덤 좌표
            int playerX = RandomNumX(xMin, xMax);
            int playerY = RandomNumY(yMin, yMax);
            int goalX = RandomNumX(xMin, xMax);
            int goalY = RandomNumY(yMin, yMax);
            int spadeX = RandomNumX(xMin, xMax);
            int spadeY = RandomNumY(yMin, yMax);
            int heartX = RandomNumX(xMin, xMax);
            int heartY = RandomNumY(yMin, yMax);
            int cloverX = RandomNumX(xMin, xMax);
            int cloverY = RandomNumY(yMin, yMax);
            int diamondX = RandomNumX(xMin, xMax);
            int diamondY = RandomNumY(yMin, yMax);
            #endregion

            // 어떻게 랜덤 문양이 정해지게 할 것인가???

            /*************************************************************
             * 방법 1. 겹치기
             * 프레임마다 지우고 그리고를 반복하는것을 이용해서
             * 플레이어에게 특정 문양이 주어지고 그걸 먹어야 하는것을
             * 특정문양 아래에 Goal을 겹쳐서 먹으면 상호작용하게 하는 방법 
             **************************************************************/
            /*************************************************************
             * 방법 2. 
             * 프레임마다 지우고 그리고를 반복하는것을 이용해서
             * 플레이어에게 특정 문양이 주어지고 그걸 먹어야 하는것을
             * 특정문양 아래에 Goal을 겹쳐서 먹으면 상호작용하게 하는 방법 
             **************************************************************/

            #region 세토array 작업중
            data.setoLocation = new Point() {x = 3, y = 3};
            int[] setoArray = new int[9] { 2, 4, 6, 8, 10, 12, 14, 16, 18 };
            for (int i = 0; i < setoArray.Length; i++)
                //   {
                //       Console.Write(setoArray[i]);
                //   }

                //int[] setoArray = {xSeto, ySeto};
                #endregion

            data.playerLocation = new Point() { x = 9, y = 7 }; // 플레이어는 그냥 가운데
            // 문양들 스폰위치
            data.spadeLocation = new Point() { x = spadeX, y = spadeY };  // 스페
            data.heartLocation = new Point() { x = heartX, y = heartY };  // 하트 
            data.cloverLocation = new Point() { x = cloverX, y = cloverY };  // 클로버
            data.diamondLocation = new Point() { x = diamondX, y = diamondY };  // 다이아

            // 세토 나오는 자리랑 나오는 타이밍, 나오는 갯수 조정해야됨
            data.setoLocationV = new Point() { setoVx = 1, setoVy = 1 };  // 세로이동 세로
            data.setoLocationH = new Point() { setoHx = 1, setoHy = 1 };  // 가로이동 버전
            //세토 대량생산
            data.setoV1 = new Point() { setoV1x = 1, setoV1y = 1 };
            data.setoV2 = new Point() { setoV2x = 3, setoV2y = 1 };
            data.setoV3 = new Point() { setoV3x = 5, setoV3y = 1 };
            data.setoV4 = new Point() { setoV4x = 7, setoV4y = 1 };
            data.setoV5 = new Point() { setoV5x = 9, setoV5y = 1 };
            data.setoV6 = new Point() { setoV6x = 11, setoV6y = 1 };
            data.setoV7 = new Point() { setoV7x = 13, setoV7y = 1 };
            data.setoV8 = new Point() { setoV8x = 15, setoV8y = 1 };
            data.setoV9 = new Point() { setoV9x = 17, setoV9y = 1 };



            // 문양 세번 다 먹어야 나옴 랜덤위치
            data.goalLocation = new Point() { x = goalX, y = goalY };


            /* 색깔 어떻게 좀 삐까빤딱하게 하려했찌만 일단 보류
            string shapes = " ♠ ♥ ♣ ◆ ♠ ♥ ♣ ◆ ♠ ♥ ♣ ◆ ♠ ♥ ♣ ◆ ";
            var heart = shapes.IndexOf('♥');
            Console.Write(shapes.Substring(0, heart));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(shapes[heart]);
            Console.ResetColor();
            Console.WriteLine(shapes.Substring(heart + 1));
            Console.WriteLine(shapes);
            */
            // 타이틀 화면
            Console.Clear();
            Console.WriteLine($"┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓"); // 타이틀화면 문양바꾸기
            Console.WriteLine($"┃ ♠ ♥ ♣ ◆ ♠ ♥ ♣ ◆ ♠ ♥ ♣ ◆ ♠ ♥ ♣ ◆ ┃");
            Console.WriteLine($"┃                                 ┃");
            Console.WriteLine($"┃      로스트아크 쿠크세이튼      ┃"); //│ ┃ 『』「」┌ ┐└ ┘─ ┏ ┓ ┛ ┗ ━ 
            Console.WriteLine($"┃           2관문 미로            ┃");
            Console.WriteLine($"┃                                 ┃");
            Console.WriteLine($"┃ ◆ ♣ ♥ ♠ ◆ ♣ ♥ ♠ ◆ ♣ ♥ ♠ ◆ ♣ ♥ ♠ ┃");
            Console.WriteLine($"┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
            Console.WriteLine();
            Console.WriteLine("    계속하려면 아무키나 누르세요    ");
            Console.ReadKey();

        }

        static void End()
        {
            Console.Clear();
            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓"); // 클리어 문구도 바꾸기
            Console.WriteLine("┃       누구 맘대로 끝을 내!?      ┃");
            Console.WriteLine("┃         무효야! 전부 무효!       ┃");
            Console.WriteLine("┃     진짜 시작은 지금부터라고!    ┃");
            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
            Console.WriteLine();
        }

        static void Collision()
        {
            Console.Clear();
            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓"); 
            Console.WriteLine("┃            쿠크루삥뽕 !          ┃");
            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
            Console.WriteLine();
        }


        static void Render()
        {
            Console.Clear();
            // 패턴
            PrintMap();
            

            // 세토가 위에 있어야 지나갈때 문양도 안덮어짐.
            //PrintSetoVertical();
            PrintSetoHorizontal();
            // 세토 대량생산
            PrintSeto1();
            PrintSeto2();
            PrintSeto3();
            PrintSeto4();
            PrintSeto5();
            PrintSeto6();
            PrintSeto7();
            PrintSeto8();
            PrintSeto9();
            // 문양들 추가해야됨.
            PrintSpade();
            PrintHeart();
            PrintClover();
            PrintDiamond();

            PrintPlayer();  // 배치 순서에 따라서 겹쳐지고 안겹쳐지고가 됨
            PrintGoal();
        }

        static void Input()
        {
            data.inputKey = Console.ReadKey(true).Key;
        }

        static void Update()
        {
            SetoVplayer();
            SetoHplayer();
            
            // 세토 뭉태기
            SetoMoveV1();
            SetoMoveV2();
            SetoMoveV3();
            SetoMoveV4();
            SetoMoveV5();
            SetoMoveV6();
            SetoMoveV7();
            SetoMoveV8();
            SetoMoveV9();


            SetoMoveH();

            // 몇단계가면 시작하게 하거나 해야됨 둘중하나만
            // 몇단계가면 시작하게 하거나 해야됨 하나씩 되게
            // 아니면 그냥 둘이 번갈아가면서?? 반복적으로?
            // 가로세토하면 세로세토 하고??
            Move();

            TouchSpade();
            TouchHeart();
            TouchClover();
            TouchDiamond();

            CheckGameClear();
        }

        static void PrintMap()
        {
            for (int y = 0; y < data.map.GetLength(0); y++)
            {
                for (int x = 0; x < data.map.GetLength(1); x++)
                {
                    if (data.map[y, x])
                    {
                        Console.Write(" ");  //
                    }
                    else
                    {
                        Console.Write("▒");  //맵 벽타일 바꾸기 ■ ▒
                    }
                }
                Console.WriteLine();
            }
        }

        static void PrintPlayer()
        {
            Console.SetCursorPosition(data.playerLocation.x, data.playerLocation.y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("P");  // 후보 P ● ο Δ ♀ 
            Console.ResetColor();
        }

        static void PrintGoal()
        {
            Console.SetCursorPosition(data.goalLocation.x, data.goalLocation.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("G");
            Console.ResetColor();
        }

        // 세토
        // 한 줄 다채워서 한쪽 끝에서 나와서 반대쪽까지 전진
        //
        // 플레이어 부딫이면 플레이어 레벨 리셋
        // 플레이어 이동 -> 세토 이동
      //  static void PrintSetoVertical()  //세토 세로 버전 | 본체 제외 분신술 이슈로 폐기.
      //  {
      //      Console.SetCursorPosition(data.setoLocationV.setoVx, data.setoLocationV.setoVy);
      //      Console.ForegroundColor = ConsoleColor.DarkMagenta;
      //      //Console.Write("●");
      //      for (int i = 1; i < 18; i += 2)
      //      {
      //          Console.SetCursorPosition(i, data.setoLocationV.setoVy);
      //          Console.Write("●");
      //      }
      //      Console.ResetColor();
      //  }
        #region 대량세토(세로)
        static void PrintSeto1() //// 새로 위치 전부 다 갖고있는 변수로 만들어야함
        {
            Console.SetCursorPosition(data.setoV1.setoV1x , data.setoV1.setoV1y);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;    
            Console.Write("○");  
            Console.ResetColor();
        }
        static void PrintSeto2() //// 새로 위치 전부 다 갖고있는 변수로 만들어야함
        {
            Console.SetCursorPosition(data.setoV2.setoV2x, data.setoV2.setoV2y);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("○");
            Console.ResetColor();
        }
        static void PrintSeto3() //// 새로 위치 전부 다 갖고있는 변수로 만들어야함
        {
            Console.SetCursorPosition(data.setoV3.setoV3x, data.setoV3.setoV3y);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("○");
            Console.ResetColor();
        }
        static void PrintSeto4() //// 새로 위치 전부 다 갖고있는 변수로 만들어야함
        {
            Console.SetCursorPosition(data.setoV4.setoV4x, data.setoV4.setoV4y);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("○");
            Console.ResetColor();
        }
        static void PrintSeto5() //// 새로 위치 전부 다 갖고있는 변수로 만들어야함
        {
            Console.SetCursorPosition(data.setoV5.setoV5x, data.setoV5.setoV5y);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("○");
            Console.ResetColor();
        }
        static void PrintSeto6() //// 새로 위치 전부 다 갖고있는 변수로 만들어야함
        {
            Console.SetCursorPosition(data.setoV6.setoV6x, data.setoV6.setoV6y);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("○");
            Console.ResetColor();
        }
        static void PrintSeto7() //// 새로 위치 전부 다 갖고있는 변수로 만들어야함
        {
            Console.SetCursorPosition(data.setoV7.setoV7x, data.setoV7.setoV7y);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("○");
            Console.ResetColor();
        }
        static void PrintSeto8() //// 새로 위치 전부 다 갖고있는 변수로 만들어야함
        {
            Console.SetCursorPosition(data.setoV8.setoV8x, data.setoV8.setoV8y);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("○");
            Console.ResetColor();
        }
        static void PrintSeto9() //// 새로 위치 전부 다 갖고있는 변수로 만들어야함
        {
            Console.SetCursorPosition(data.setoV9.setoV9x, data.setoV9.setoV9y);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("○");
            Console.ResetColor();
        }

        #endregion
        #region 스페버전
        /* 세토사이에 스페이스 삽입해서 공간띄우던방식
      //  for (int i = 0; i < 17; i++)
      //  {
      //      if (i % 2 != 0)
      //      { Console.Write(" "); }  //빈칸을 그리는 작업x 해야 , 커서위치를 늘리는 방식으로.
      //      // sETcURSORpOSITION
      //      else
      //      {
      //          Console.Write("●");
      //      }
      //  }   //이게 스페이스바하니까 가려짐...
        */
        #endregion
        static void PrintSetoHorizontal()  //세토 가로 이동 버전  , 위치를 전부다 갖고 있어야함
        {
            Console.SetCursorPosition(data.setoLocationH.setoHx, data.setoLocationH.setoHy);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            for (int i = 0; i < 13; i++)
            {
                Console.SetCursorPosition(data.setoLocationH.setoHx, data.setoLocationH.setoHy + i); //세로 이동방식으로 프린트하면 계속 벽에 꼬라박혀서 다른방식으로...
                if (i % 2 != 0)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                }
                else
                { Console.Write($"●"); }

            }   //캐릭터 사이즈 확인 잘해야됨 벽을 가려버림
            Console.ResetColor();
        }

        // 모양들 print
        /// <summary>
        ///  
        /// </summary>
        #region
        static void PrintSpade()
        {
            Console.SetCursorPosition(data.spadeLocation.x, data.spadeLocation.y);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("♠");  // ♠ ♥ ♣ ◆
            Console.ResetColor();
        }
        static void PrintHeart()
        {
            Console.SetCursorPosition(data.heartLocation.x, data.heartLocation.y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("♥");  // ♠ ♥ ♣ ◆
            Console.ResetColor();
        }
        static void PrintClover()
        {
            Console.SetCursorPosition(data.cloverLocation.x, data.cloverLocation.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("♣");  // ♠ ♥ ♣ ◆ 
            Console.ResetColor();
        }
        static void PrintDiamond()
        {
            Console.SetCursorPosition(data.diamondLocation.x, data.diamondLocation.y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("◆");  // ♠ ♥ ♣ ◆ 
            Console.ResetColor();
        }
        #endregion

        static void Move()  // 플레이어 움직임.
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;
            }
        }


        // 세토 움직임
        // 플레이어가 아무키나 입력하면
        // 정해진 방향으로 이동하도록

        //뭉태기
        #region
        static void SetoMoveV1()
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    SetoVerticalMove1();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    SetoVerticalMove1();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    SetoVerticalMove1();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    SetoVerticalMove1();
                    break;
            }
        }
        static void SetoMoveV2()
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    SetoVerticalMove2();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    SetoVerticalMove2();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    SetoVerticalMove2();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    SetoVerticalMove2();
                    break;
            }
        }
        static void SetoMoveV3()
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    SetoVerticalMove3();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    SetoVerticalMove3();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    SetoVerticalMove3();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    SetoVerticalMove3();
                    break;
            }
        }
        static void SetoMoveV4()
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    SetoVerticalMove4();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    SetoVerticalMove4();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    SetoVerticalMove4();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    SetoVerticalMove4();
                    break;
            }
        }
        static void SetoMoveV5()
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    SetoVerticalMove5();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    SetoVerticalMove5();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    SetoVerticalMove5();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    SetoVerticalMove5();
                    break;
            }
        }
        static void SetoMoveV6()
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    SetoVerticalMove6();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    SetoVerticalMove6();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    SetoVerticalMove6();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    SetoVerticalMove6();
                    break;
            }
        }
        static void SetoMoveV7()
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    SetoVerticalMove7();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    SetoVerticalMove7();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    SetoVerticalMove7();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    SetoVerticalMove7();
                    break;
            }
        }
        static void SetoMoveV8()
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    SetoVerticalMove8();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    SetoVerticalMove8();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    SetoVerticalMove8();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    SetoVerticalMove8();
                    break;
            }
        }
        static void SetoMoveV9()
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    SetoVerticalMove9();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    SetoVerticalMove9();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    SetoVerticalMove9();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    SetoVerticalMove9();
                    break;
            }
        }

        #endregion
        static void SetoMoveH()
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    SetoHorizontalMove();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    SetoHorizontalMove();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    SetoHorizontalMove();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    SetoHorizontalMove();
                    break;
            }
        }





        // 3단계가 되게끔 해야됨
        // 문양 먹으면 클리어 되게끔 추가
        // 어떤 방향으로 갈지 정해야 함
        // 1. 문양 네개를 하나씩 먹으면서 terminate 하고 나머지 하나씩먹으면서 다음단계
        // 2. 기존 처럼 문양 정해지면 그문양 3번 먹고 탈출

        // 세토랑 부딪히면
        // 세로 세토
        static void SetoVplayer() // 세로 세토 Payer 충돌
        {
            if (data.playerLocation.x == data.setoLocationV.setoV1x &&
                data.playerLocation.y == data.setoLocationV.setoV1y)
            {
                data.running = false;
            }
            else if(data.playerLocation.x == data.setoV1.setoV1x &&
                data.playerLocation.y == data.setoV1.setoV1y)
            {
                data.running = false;
            }
            else if (data.playerLocation.x == data.setoV2.setoV2x &&
                data.playerLocation.y == data.setoV2.setoV2y)
            {
                data.running = false;
            }
            else if (data.playerLocation.x == data.setoV3.setoV3x &&
                data.playerLocation.y == data.setoV3.setoV3y)
            {
                data.running = false;
            }
            else if (data.playerLocation.x == data.setoV4.setoV4x &&
                data.playerLocation.y == data.setoV4.setoV4y)
            {
                data.running = false;
            }
            else if (data.playerLocation.x == data.setoV5.setoV5x &&
                 data.playerLocation.y == data.setoV5.setoV5y)
            {
                data.running = false;
            }
            else if (data.playerLocation.x == data.setoV6.setoV6x &&
                data.playerLocation.y == data.setoV6.setoV6y)
            {
                data.running = false;
            }
            else if (data.playerLocation.x == data.setoV7.setoV7x &&
                data.playerLocation.y == data.setoV7.setoV7y)
            {
                data.running = false;
            }
            else if (data.playerLocation.x == data.setoV8.setoV8x &&
                data.playerLocation.y == data.setoV8.setoV8y)
            {
                data.running = false;
            }
            else if (data.playerLocation.x == data.setoV9.setoV9x &&
                data.playerLocation.y == data.setoV9.setoV9y)
            {
                data.running = false;
            }
        }

        // 가로 세토
        static void SetoHplayer() // 가로 세토 Player 충돌
        {
            if (data.playerLocation.x == data.setoLocationH.setoHx &&
                data.playerLocation.y == data.setoLocationH.setoHy)
            {
                data.running = false;
            }
        }


        static void CheckGameClear() // Player && Goal 의 접촉
        {
            if (data.playerLocation.x == data.goalLocation.x &&
                data.playerLocation.y == data.goalLocation.y)
            {
                data.running = false;
            }
        }


        // Next Level 
        static void TouchSpade() // Player && spade 문양 의 접촉
        {
            if (data.playerLocation.x == data.spadeLocation.x &&
                data.playerLocation.y == data.spadeLocation.y)
            {
                data.running = false;
            }
        }

        static void TouchHeart() // Player && heart 문양 의 접촉
        {
            if (data.playerLocation.x == data.heartLocation.x &&
                data.playerLocation.y == data.heartLocation.y)
            {
                data.running = false;
            }
        }

        static void TouchClover() // Player && clover 문양 의 접촉
        {
            if (data.playerLocation.x == data.cloverLocation.x &&
                data.playerLocation.y == data.cloverLocation.y)
            {
                data.running = false;
            }
        }

        static void TouchDiamond() // Player && diamond 문양 의 접촉
        {
            if (data.playerLocation.x == data.diamondLocation.x &&
                data.playerLocation.y == data.diamondLocation.y)
            {
                data.running = false;
            }
        }



        static void SetoVerticalMove1()
        {
            Point next = new Point() { setoV1x = data.setoV1.setoV1x, setoV1y = data.setoV1.setoV1y + 1 };
            if (data.map[next.setoV1y, next.setoV1x])
            {
                data.setoV1 = next;
            }
        }
        static void SetoVerticalMove2()
        {
            Point next = new Point() { setoV2x = data.setoV2.setoV2x, setoV2y = data.setoV2.setoV2y + 1 };
            if (data.map[next.setoV2y, next.setoV2x])
            {
                data.setoV2 = next;
            }
        }
        static void SetoVerticalMove3()
        {
            Point next = new Point() { setoV3x = data.setoV3.setoV3x, setoV3y = data.setoV3.setoV3y + 1 };
            if (data.map[next.setoV3y, next.setoV3x])
            {
                data.setoV3 = next;
            }
        }
        static void SetoVerticalMove4()
        {
            Point next = new Point() { setoV4x = data.setoV4.setoV4x, setoV4y = data.setoV4.setoV4y + 1 };
            if (data.map[next.setoV4y, next.setoV4x])
            {
                data.setoV4 = next;
            }
        }
        static void SetoVerticalMove5()
        {
            Point next = new Point() { setoV5x = data.setoV5.setoV5x, setoV5y = data.setoV5.setoV5y + 1 };
            if (data.map[next.setoV5y, next.setoV5x])
            {
                data.setoV5 = next;
            }
        }
        static void SetoVerticalMove6()
        {
            Point next = new Point() { setoV6x = data.setoV6.setoV6x, setoV6y = data.setoV6.setoV6y + 1 };
            if (data.map[next.setoV6y, next.setoV6x])
            {
                data.setoV6 = next;
            }
        }
        static void SetoVerticalMove7()
        {

            Point next = new Point() { setoV7x = data.setoV7.setoV7x, setoV7y = data.setoV7.setoV7y + 1 };
            if (data.map[next.setoV7y, next.setoV7x])
            {
                data.setoV7 = next;
            }
        }
        static void SetoVerticalMove8()
        {
            Point next = new Point() { setoV8x = data.setoV8.setoV8x, setoV8y = data.setoV8.setoV8y + 1 };
            if (data.map[next.setoV8y, next.setoV8x])
            {
                data.setoV8 = next;
            }
        }
        static void SetoVerticalMove9()
        {
            Point next = new Point() { setoV9x = data.setoV9.setoV9x, setoV9y = data.setoV9.setoV9y + 1 };
            if (data.map[next.setoV9y, next.setoV9x])
            {
                data.setoV9 = next;
            }
        }


        static void SetoHorizontalMove()
        {
            Point next = new Point() { setoHx = data.setoLocationH.setoHx + 1, setoHy = data.setoLocationH.setoHy };
            if (data.map[next.setoHy, next.setoHx])
            {
                data.setoLocationH = next;
            }
        }




        #region P움직임
        static void MoveUp()
        {
            Point next = new Point() { x = data.playerLocation.x, y = data.playerLocation.y - 1 };
            if (data.map[next.y, next.x])
            {
                data.playerLocation = next;
            }
        }

        static void MoveDown()
        {
            Point next = new Point() { x = data.playerLocation.x, y = data.playerLocation.y + 1 };
            if (data.map[next.y, next.x])
            {
                data.playerLocation = next;
            }
        }

        static void MoveLeft()
        {
            Point next = new Point() { x = data.playerLocation.x - 1, y = data.playerLocation.y };
            if (data.map[next.y, next.x])
            {
                data.playerLocation = next;
            }
        }

        static void MoveRight()
        {
            Point next = new Point() { x = data.playerLocation.x + 1, y = data.playerLocation.y };
            if (data.map[next.y, next.x])
            {
                data.playerLocation = next;
            }
        }
        #endregion

    }
}
