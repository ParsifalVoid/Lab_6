using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BMPReader
{

    internal class Program
    {
        struct RGB
        {
            public byte r;
            public byte g;
            public byte b;
        }
        private static void Main(string[] args)
        {
            List<RGB> list = new List<RGB>();
            using (BinaryReader reader = new BinaryReader(File.Open("E:\\С#\\Лаба 2\\24bmp.bmp", FileMode.Open)))
            {
/* 	0 	2 	Символы 'BM' (код 4D42h) 
	2 	4 	Размер файла в байтах 
	6 	2 	0 (Резервное поле) 
	8 	2 	0 (Резервное поле) 
    10 	4 	Смещение, с которого начинается само изображение (растр). 
*/
                Char start1 = reader.ReadChars(1)[0];
                Char start2 = reader.ReadChars(1)[0];
                Int32 size = reader.ReadInt32();
                Int16 field1 = reader.ReadInt16();
                Int16 field2 = reader.ReadInt16();
                Int32 offset = reader.ReadInt32();

/* 14  4   Размер заголовка BITMAP(в байтах) равно 40
   18  4   Ширина изображения в пикселях
   22  4   Высота изображения в пикселях
   26  2   Число плоскостей, должно быть 1
*/
                Int32 hederBMPSize = reader.ReadInt32();
                Int32 imgWidth = reader.ReadInt32();
                Int32 imgHeight = reader.ReadInt32();
                Int16 pCount = reader.ReadInt16();

/* 	28 	2 	Бит/пиксел 
    30 	4 	Тип сжатия 
*/
                Int16 bitPerInch = reader.ReadInt16();
                Int32 zipType = reader.ReadInt32();

/* 	34 	4 	0 или размер сжатого изображения в байтах. 
	38 	4 	Горизонтальное разрешение, пиксел/м 
	42 	4 	Вертикальное разрешение, пиксел/м 
	46 	4 	Количество используемых цветов 
	50 	4 	Количество "важных" цветов. 
*/

                Int32 zipImageSize = reader.ReadInt32();
                Int32 hResolution = reader.ReadInt32();
                Int32 vResolution = reader.ReadInt32();
                Int32 colorCount = reader.ReadInt32();
                Int32 importantColorCount = reader.ReadInt32();

                Console.WriteLine($"Символы 'BM' (код 4D42h): {start1}{start2}");
                Console.WriteLine($"Размер файла в байтах: {size}");
                Console.WriteLine($"Резервное поле 1: {field1}");
                Console.WriteLine($"Резервное поле 2: {field2}");
                Console.WriteLine($"Смещение: {offset}");

                Console.WriteLine($"Размер заголовка BITMAP: {hederBMPSize}");
                Console.WriteLine($"Ширина изображения в пикселях: {imgWidth}");
                Console.WriteLine($"Высота изображения в пикселях: {imgHeight}");
                Console.WriteLine($"Число плоскостей: {pCount}");
                Console.WriteLine($"Бит/пиксел: {bitPerInch}");

/*1 = monochrome palette. Кол-во цветов = 2  
  4 = 4bit palletized. Кол-во цветов = 16  
  8 = 8bit palletized. Кол-во цветов = 256  
  16 = 16bit RGB. Кол-во ветов = 65536  
  24 = 24bit RGB. Кол-во ветов = 16M 
*/

                switch (bitPerInch)
                {
                    case 1: Console.WriteLine($"{bitPerInch} => monochrome palette. Кол-во цветов = 2"); break;

                    case 4: Console.WriteLine($"{bitPerInch} => 4bit palletized. Кол-во цветов = 16");break;

                    case 8: Console.WriteLine($"{bitPerInch} => 8bit palletized. Кол-во цветов = 256"); break;

                    case 16: Console.WriteLine($"{bitPerInch} => 16bit RGB. Кол-во цветов = 65536");break;

                    case 24: Console.WriteLine($"{bitPerInch} => 24bit RGB. Кол-во цветов = 16M"); break;
                }
                Console.WriteLine($"Бит/пиксел: {zipType}");

/*0	= BI_RGB  (без сжатия)  
  1	= BI_RLE8 (8 bit RLE сжатие)  
  2	= BI_RLE4 (4 bit RLE сжатие)  
*/
                switch(zipType)
                {
                    case 0: Console.WriteLine("BI_RGB  (без сжатия)"); break;

                    case 1: Console.WriteLine("BI_RLE8 (8 bit RLE сжатие)"); break;

                    case 2: Console.WriteLine("BI_RLE4 (4 bit RLE сжатие)"); break;

                }

                Console.WriteLine($"Pазмер сжатого изображения в байтах: {zipImageSize}");
                Console.WriteLine($"Горизонтальное разрешение, пиксел/м: {hResolution}");
                Console.WriteLine($"Вертикальное разрешение, пиксел/м: {vResolution}");
                Console.WriteLine($"Количество используемых цветов: {colorCount}");
                Console.WriteLine($"Количество \"важных\" цветов: {importantColorCount}");

                
                Console.ReadLine();
            }
            
        }
    }
}
