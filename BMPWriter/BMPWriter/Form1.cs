namespace BMPWriter
{
    public partial class Form1 : Form
    {
        struct RGB
        {
            public byte r;
            public byte g;
            public byte b;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            List<RGB> list = new List<RGB>();
            using (BinaryReader reader = new BinaryReader(File.Open("E:\\�#\\���� 2\\24bmp.bmp", FileMode.Open)))
            {
                    /* 	0 	2 	������� 'BM' (��� 4D42h) 
                        2 	4 	������ ����� � ������ 
                        6 	2 	0 (��������� ����) 
                        8 	2 	0 (��������� ����) 
                        10 	4 	��������, � �������� ���������� ���� ����������� (�����). 
                    */
                    Char start1 = reader.ReadChars(1)[0];
                    Char start2 = reader.ReadChars(1)[0];
                    Int32 size = reader.ReadInt32();
                    Int16 field1 = reader.ReadInt16();
                    Int16 field2 = reader.ReadInt16();
                    Int32 offset = reader.ReadInt32();

                    /* 14  4   ������ ��������� BITMAP(� ������) ����� 40
                       18  4   ������ ����������� � ��������
                       22  4   ������ ����������� � ��������
                       26  2   ����� ����������, ������ ���� 1
                    */
                    Int32 hederBMPSize = reader.ReadInt32();
                    Int32 imgWidth = reader.ReadInt32();
                    Int32 imgHeight = reader.ReadInt32();
                    Int16 pCount = reader.ReadInt16();

                    /* 	28 	2 	���/������ 
                        30 	4 	��� ������ 
                    */
                    Int16 bitPerInch = reader.ReadInt16();
                    Int32 zipType = reader.ReadInt32();

                    /* 	34 	4 	0 ��� ������ ������� ����������� � ������. 
                        38 	4 	�������������� ����������, ������/� 
                        42 	4 	������������ ����������, ������/� 
                        46 	4 	���������� ������������ ������ 
                        50 	4 	���������� "������" ������. 
                    */

                    Int32 zipImageSize = reader.ReadInt32();
                    Int32 hResolution = reader.ReadInt32();
                    Int32 vResolution = reader.ReadInt32();
                    Int32 colorCount = reader.ReadInt32();
                    Int32 importantColorCount = reader.ReadInt32();

                    Console.WriteLine($"������� 'BM' (��� 4D42h): {start1}{start2}");
                    Console.WriteLine($"������ ����� � ������: {size}");
                    Console.WriteLine($"��������� ���� 1: {field1}");
                    Console.WriteLine($"��������� ���� 2: {field2}");
                    Console.WriteLine($"��������: {offset}");

                    Console.WriteLine($"������ ��������� BITMAP: {hederBMPSize}");
                    Console.WriteLine($"������ ����������� � ��������: {imgWidth}");
                    Console.WriteLine($"������ ����������� � ��������: {imgHeight}");
                    Console.WriteLine($"����� ����������: {pCount}");
                    Console.WriteLine($"���/������: {bitPerInch}");

                    /*1 = monochrome palette. ���-�� ������ = 2  
                      4 = 4bit palletized. ���-�� ������ = 16  
                      8 = 8bit palletized. ���-�� ������ = 256  
                      16 = 16bit RGB. ���-�� ����� = 65536  
                      24 = 24bit RGB. ���-�� ����� = 16M 
                    */

                    switch (bitPerInch)
                    {
                        case 1: Console.WriteLine($"{bitPerInch} => monochrome palette. ���-�� ������ = 2"); break;

                        case 4: Console.WriteLine($"{bitPerInch} => 4bit palletized. ���-�� ������ = 16"); break;

                        case 8: Console.WriteLine($"{bitPerInch} => 8bit palletized. ���-�� ������ = 256"); break;

                        case 16: Console.WriteLine($"{bitPerInch} => 16bit RGB. ���-�� ������ = 65536"); break;

                        case 24: Console.WriteLine($"{bitPerInch} => 24bit RGB. ���-�� ������ = 16M"); break;
                    }
                    Console.WriteLine($"���/������: {zipType}");

                    /*0	= BI_RGB  (��� ������)  
                      1	= BI_RLE8 (8 bit RLE ������)  
                      2	= BI_RLE4 (4 bit RLE ������)  
                    */
                    switch (zipType)
                    {
                        case 0: Console.WriteLine("BI_RGB  (��� ������)"); break;

                        case 1: Console.WriteLine("BI_RLE8 (8 bit RLE ������)"); break;

                        case 2: Console.WriteLine("BI_RLE4 (4 bit RLE ������)"); break;

                    }

                    Console.WriteLine($"P����� ������� ����������� � ������: {zipImageSize}");
                    Console.WriteLine($"�������������� ����������, ������/�: {hResolution}");
                    Console.WriteLine($"������������ ����������, ������/�: {vResolution}");
                    Console.WriteLine($"���������� ������������ ������: {colorCount}");
                    Console.WriteLine($"���������� \"������\" ������: {importantColorCount}");

                    while (true)
                    {
                        try
                        {

                            RGB rgb = new RGB();
                            rgb.r = reader.ReadByte();
                            rgb.g = reader.ReadByte();
                            rgb.b = reader.ReadByte();

                            list.Add(rgb);

                            Console.Write($"{rgb.r} {rgb.g} {rgb.b}\n");
                        }
                        catch
                        {
                            break;
                        }
                    }
                int index = 0;
                for (int i = 0; i < imgHeight; i++)
                {
                    for (int j = 0; j < imgWidth + 1; j++)
                    {
                        Rectangle r = new Rectangle();
                        r.Size = new Size(100, 100);
                        r.X = j;
                        r.Y = i;
                        Color color = Color.FromArgb(255, 
                            list[index].r, 
                            list[index].g, 
                            list[index].b
                            );
                        SolidBrush br = new SolidBrush(color);
                        g.FillRectangle(br, r);
                        index++;
                    }
                }

            }
        }
    }
}