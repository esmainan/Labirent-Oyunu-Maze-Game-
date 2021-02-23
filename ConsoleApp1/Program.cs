using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            char[,] maze = new char[10, 10];
            int i,j ;
            //alt satıra random 1 atama
            for( i=0;i<10;i++)
            {
                for ( j = 0; j < 10; j++)
                    maze[i, j] = '0';
            }

            int[] tempString = new int[3];
            int counter = 0, value = 0;

            while(counter<3)
            {
                Random mix = new Random();
                value = (int)mix.Next(0, 10);
                
                if(Array.IndexOf(tempString,value)<0)
                {
                    tempString[counter] = value;
                    counter++;
                }
            }

            maze[9, tempString[0]] = '1';
            maze[9, tempString[1]] = '1';
            maze[9, tempString[2]] = '1';
            //ilk satırdaki 1 lerin konumunu bir dizide tutuyorum
            int[] permt = new int[3];
            permt[0] = tempString[0];
            permt[1] = tempString[1];
            permt[2] = tempString[2];

            //ilk satırdaki 1 lerin y eksenindeki konumunu ayrıca başka bir dizide aritmetik tutuyorum yolları numaralandırmak için
            int[] locationY = new int[3];
            int temporary;

            locationY[0] = tempString[0];
            locationY[1] = tempString[1];
            locationY[2] = tempString[2];

            for (i=0;i<3;i++)
            {
                for(j=i+1;j<3;j++)
                {
                    if(locationY[j]<locationY[i])
                    {
                        temporary = locationY[i];
                        locationY[i] = locationY[j];
                        locationY[j] = temporary;
                    }
                }
            }
            //birlerin üstüne çıkma
            Random mix2= new Random();
            int[] rightLeft = new int[]{ 1, -1,0 };
            int index;

            for (i = 8; i >= 0; i--)
            {
                //ilk 1 için
                maze[i, tempString[0]] = '1';
                index = (int)mix2.Next(0, 3);
                tempString[0] = tempString[0] + rightLeft[index];
                if (tempString[0] > 9)
                    tempString[0] = 9;
                if (tempString[0] < 0)
                    tempString[0] = 0;
                maze[i, tempString[0]] = '1';

                //ikinci 1 için
                maze[i, tempString[1]] = '1';
                index = (int)mix2.Next(0, 3);
                tempString[1] = tempString[1] + rightLeft[index];
                if (tempString[1] > 9)
                    tempString[1] = 9;
                if (tempString[1] < 0)
                    tempString[1] = 0;
                maze[i, tempString[1]] = '1';

                //üçüncü 1 için
                maze[i, tempString[2]] = '1';
                index = (int)mix2.Next(0, 3);
                tempString[2] = tempString[2] + rightLeft[index];
                if (tempString[2] > 9)
                    tempString[2] = 9;
                if (tempString[2] < 0)
                    tempString[2] = 0;
                maze[i, tempString[2]] = '1';
            }
            //bombaların konumu
            bool control = false;
            int bombOneX = 0, bombOneY = 0, bombTwoX = 0, bombTwoY = 0;
            while (!control)
            {
                bombOneX = (int)mix2.Next(1, 9);
                bombOneY = (int)mix2.Next(1, 9);
                bombTwoX = (int)mix2.Next(1, 9);
                bombTwoY = (int)mix2.Next(1, 9);

                if ((maze[bombOneX,bombOneY] == '1') && (maze[bombTwoX, bombTwoY] == '1'))
                    control = true;
                if (bombOneX == bombTwoX && bombTwoY == bombOneY)
                    control = false;
            }

            //labirenti ve yolların numarasını yazdırma

            void mazePrinter()
            {
                for (i = 0; i < 10; i++)
                {
                    for (j = 0; j < 10; j++)
                        Console.Write("{0}", maze[i, j]);
                    Console.WriteLine();
                }
                for (i = 0; i < locationY[0]; i++)
                    Console.Write(" ");
                Console.Write("1");
                for (i = locationY[0] + 1; i < locationY[1]; i++)
                    Console.Write(" ");
                Console.Write("2");
                for (i = locationY[1] + 1; i < locationY[2]; i++)
                    Console.Write(" ");
                Console.Write("3");
                Console.WriteLine("");
            }
            mazePrinter();

            //kullanıcıdan başlangıç değeri alma
            Console.Write("Lütfen bir yol seçiniz");
            int choice = 0;
            bool choiceControl = false;
            while(!choiceControl)
            {
                choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 1 || choice == 2 || choice == 3)
                {
                    maze[9, locationY[choice - 1]] = 'K';
                    choiceControl = true;
                }
                else
                {
                    Console.Clear();
                    mazePrinter();
                    Console.Write("Geçerli bir yol seçiniz lütfen");
                }
            }
            
            //k nin koordinatlarını tutma
            int x=9, y=locationY[choice-1];

            //labirenti tekrar yazdırma
            Console.Clear();
            mazePrinter();

            //girilen tuşlar ve skor
            ConsoleKeyInfo key;
            bool kontrol = false;
            int score = 0;

            //hareket döngüsü
            while (kontrol==false)
            {
                key = Console.ReadKey();
                //g girildiğinde bombanın gözüküp gizlenmesi
                if (key.Key == ConsoleKey.G)
                {
                    if (maze[bombOneX, bombOneY] == '2' && maze[bombTwoX, bombTwoY] == '2')
                    {
                        maze[bombOneX, bombOneY] = '1';
                        maze[bombTwoX, bombTwoY] = '1';
                        Console.Clear();
                        mazePrinter();
                    }
                    else
                    {
                        maze[bombOneX, bombOneY] = '2';
                        maze[bombTwoX, bombTwoY] = '2';
                        Console.Clear();
                        mazePrinter();
                    }
                }
                //w ile ileri gitme
                else if(key.Key==ConsoleKey.W)
                {                    
                    if (maze[x-1,y]=='1')
                    {

                        if ((x - 1) == 0)
                        {
                            maze[x, y] = '1';
                            x = x - 1;
                            maze[x, y] = 'K';
                            Console.Clear();
                            mazePrinter();
                            score++;
                            Console.WriteLine("TEBRİKLER OYUNU KAZANDINIZ!! SKORUNUZ->" + score);
                            kontrol = true;
                        }
                        else if((x-1)==bombOneX && y==bombOneY)
                        {
                            maze[x, y] = '1';
                            x = x - 1;
                            maze[x, y] = 'K';
                            Console.Clear();
                            mazePrinter();
                            Console.WriteLine("Bombaya bastınız. Oyun bitti.. Skorunuz->" + score);
                            kontrol = true;
                        }
                        else if((x-1)==bombTwoX && y==bombTwoY)
                        {
                            maze[x, y] = '1';
                            x = x - 1;
                            maze[x, y] = 'K';
                            Console.Clear();
                            mazePrinter(); 
                            Console.WriteLine("Bombaya bastınız. Oyun bitti.. Skorunuz->" + score);
                            kontrol = true;
                        }
                        else 
                        {
                            maze[x, y] = '1';
                            x = x - 1;
                            maze[x, y] = 'K';
                            Console.Clear();
                            mazePrinter();
                            score++;
                            Console.WriteLine("Yeni skorunuz->" + score);
                        }
                    }
                    else if(maze[x-1,y]=='0')
                    {
                        Console.Clear();
                        mazePrinter();
                        score--;
                        Console.WriteLine("Duvara geldiniz!! Yeni skorunuz->" + score);
                    }
                    else if(maze[x-1,y]=='2')
                    {
                        maze[x, y] = '1';
                        x = x - 1;
                        maze[x, y] = 'K';
                        Console.Clear();
                        mazePrinter(); 
                        Console.WriteLine("Bombaya bastınız. Oyun bitti.. Skorunuz->" + score);
                        kontrol = true;
                    }
                }
                //s ile geri gitme
                else if(key.Key==ConsoleKey.S)
                {
                    if ((x + 1) > 9)
                    {
                        maze[x, y] = '1';
                        Console.Clear();
                        mazePrinter();
                        Console.Write("Tekrar yol seçiniz->");
                        choiceControl = false;
                        while (!choiceControl)
                        {
                            choice = Convert.ToInt32(Console.ReadLine());
                            if (choice == 1 || choice == 2 || choice == 3)
                            {
                                maze[9, locationY[choice - 1]] = 'K';
                                choiceControl = true;
                            }
                            else
                            {
                                Console.Clear();
                                mazePrinter();
                                Console.Write("Geçerli bir yol seçiniz lütfen");
                            }
                        }
                        x = 9;
                        y = locationY[choice - 1];
                        Console.Clear();
                        mazePrinter();
                        score = 0;
                    }
                    else if (maze[x + 1, y] == '1')
                    {
                        if ((x + 1) == bombOneX && y == bombOneY)
                        {
                            maze[x, y] = '1';
                            x = x + 1;
                            maze[x, y] = 'K';
                            Console.Clear();
                            mazePrinter(); 
                            Console.WriteLine("Bombaya bastınız. Oyun bitti.. Skorunuz->" + score);
                            kontrol = true;
                        }
                        else if ((x + 1) == bombTwoX && y == bombTwoY)
                        {
                            maze[x, y] = '1';
                            x = x + 1;
                            maze[x, y] = 'K';
                            Console.Clear();
                            mazePrinter(); 
                            Console.WriteLine("Bombaya bastınız. Oyun bitti.. Skorunuz->" + score);
                            kontrol = true;
                        }
                        else
                        {
                            maze[x, y] = '1';
                            x = x + 1;
                            maze[x, y] = 'K';
                            Console.Clear();
                            mazePrinter();
                            score++;
                            Console.WriteLine("Yeni skorunuz->" + score);
                        }
                    }
                    else if(maze[x+1,y]=='0')
                    {
                        Console.Clear();
                        mazePrinter();
                        score--;
                        Console.WriteLine("Duvara geldiniz!! Yeni skorunuz->" + score);
                    }                    
                    else if(maze[x+1,y]=='2')
                    {
                        maze[x, y] = '1';
                        x = x + 1;
                        maze[x, y] = 'K';
                        Console.Clear();
                        mazePrinter(); 
                        Console.WriteLine("Bombaya bastınız. Oyun bitti.. Skorunuz->" + score);
                        kontrol = true;
                    }
                }
                //d ile sağa ilerleme
                else if(key.Key==ConsoleKey.D)
                {
                    if((y+1)>9)
                    {
                        Console.Clear();
                        mazePrinter();
                        Console.WriteLine("Yeni skorunuz->" + score);
                    }
                    else if (maze[x, y+1] == '1')
                    {
                        if (x == bombOneX && (y+1) == bombOneY)
                        {
                            maze[x, y] = '1';
                            y = y + 1;
                            maze[x, y] = 'K';
                            Console.Clear();
                            mazePrinter(); 
                            Console.WriteLine("Bombaya bastınız. Oyun bitti.. Skorunuz->" + score);
                            kontrol = true;
                        }
                        else if (x == bombTwoX && (y+1) == bombTwoY)
                        {
                            maze[x, y] = '1';
                            y = y + 1;
                            maze[x, y] = 'K';
                            Console.Clear();
                            mazePrinter(); 
                            Console.WriteLine("Bombaya bastınız. Oyun bitti.. Skorunuz->" + score);
                            kontrol = true;
                        }
                        else
                        {
                            maze[x, y] = '1';
                            y = y + 1;
                            maze[x, y] = 'K';
                            Console.Clear();
                            mazePrinter();
                            score++;
                            Console.WriteLine("Yeni skorunuz->" + score);
                        }
                    }
                    else if (maze[x, y+1] == '0')
                    {
                        Console.Clear();
                        mazePrinter();
                        score--;
                        Console.WriteLine("Duvara geldiniz!! Yeni skorunuz->" + score);
                    }
                    else if (maze[x, y+1] == '2')
                    {
                        maze[x, y] = '1';
                        y = y + 1;
                        maze[x, y] = 'K';
                        Console.Clear();
                        mazePrinter(); 
                        Console.WriteLine("Bombaya bastınız. Oyun bitti.. Skorunuz->" + score);
                        kontrol = true;
                    }
                }
                //a ile sola ilerleme
                else if (key.Key == ConsoleKey.A)
                {
                    if ((y - 1) < 0)
                    {
                        Console.Clear();
                        mazePrinter();
                        Console.WriteLine("Yeni skorunuz->" + score);
                    }
                    else if (maze[x, y - 1] == '1')
                    {
                        if (x == bombOneX && (y - 1) == bombOneY)
                        {
                            maze[x, y] = '1';
                            y = y - 1;
                            maze[x, y] = 'K';
                            Console.Clear();
                            mazePrinter(); 
                            Console.WriteLine("Bombaya bastınız. Oyun bitti.. Skorunuz->" + score);
                            kontrol = true;
                        }
                        else if (x == bombTwoX && (y - 1) == bombTwoY)
                        {
                            maze[x, y] = '1';
                            y = y - 1;
                            maze[x, y] = 'K';
                            Console.Clear();
                            mazePrinter();
                            Console.WriteLine("Bombaya bastınız. Oyun bitti.. Skorunuz->" + score);
                            kontrol = true;
                        }
                        else
                        {
                            maze[x, y] = '1';
                            y = y - 1;
                            maze[x, y] = 'K';
                            Console.Clear();
                            mazePrinter();
                            score++;
                            Console.WriteLine("Yeni skorunuz->" + score);
                        }
                    }
                    else if (maze[x, y - 1] == '0')
                    {
                        Console.Clear();
                        mazePrinter();
                        score--;
                        Console.WriteLine("Duvara geldiniz!! Yeni skorunuz->" + score);
                    }
                    else if (maze[x, y - 1] == '2')
                    {
                        maze[x, y] = '1';
                        y = y - 1;
                        maze[x, y] = 'K';
                        Console.Clear();
                        mazePrinter();
                        Console.WriteLine("Bombaya bastınız. Oyun bitti.. Skorunuz->" + score);
                        kontrol = true;
                    }
                }
                //geçersiz tuş girişi
                else
                {
                    Console.Clear();
                    mazePrinter();
                    Console.WriteLine("Yeni skorunuz->" + score);
                }
            }
    
            Console.ReadLine();
        }
    }
}
