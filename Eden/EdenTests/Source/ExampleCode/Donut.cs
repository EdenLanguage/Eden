namespace EdenTests.Source.ExampleCode
{
    class Donut
    {
        const int scale = 1;
        const int Width = scale * 80;
        const int Height = scale * 40;
        const int Size = Width * Height;

        static void Run()
        {
            float A = 0, B = 0;
            char[] buffer = new char[Size];
            float[] zBuffer = new float[Size];

            int R1 = 3;
            int R2 = 1;
            int K1 = 4;
            int K2 = Width * K1 * 3 / (8 * (R1 + R2));

            while (true)
            {
                Array.Fill(buffer, ' ');
                Array.Fill(zBuffer, 0);

                for (float theta = 0; theta < 2 * MathF.PI; theta += 0.07f)
                {
                    for (float phi = 0; phi < 2 * MathF.PI; phi += 0.02f)
                    {
                        float cosA = MathF.Cos(A), sinA = MathF.Sin(A);
                        float cosB = MathF.Cos(B), sinB = MathF.Sin(B);
                        float cosTheta = MathF.Cos(theta), sinTheta = MathF.Sin(theta);
                        float cosPhi = MathF.Cos(phi), sinPhi = MathF.Sin(phi);

                        float circleX = R1 + R2 * cosTheta;
                        float circleY = R2 * sinTheta;

                        float x = circleX * (cosB * cosPhi + sinA * sinB * sinPhi) - circleY * cosA * sinB;
                        float y = circleX * (sinB * cosPhi - sinA * cosB * sinPhi) + circleY * cosA * cosB;
                        float z = 5 + cosA * circleX * sinPhi + circleY * sinA;
                        float ooz = 1 / z;

                        int xp = (int)(Width / 2 + K2 * ooz * x);
                        int yp = (int)(Height / 2 - K2 / 2 * ooz * y);

                        int idx = xp + yp * Width;
                        float luminance = cosPhi * cosTheta * sinB - cosA * cosTheta * sinPhi - sinA * sinTheta + cosB * (cosA * sinTheta - cosTheta * sinA * sinPhi);

                        if (idx >= 0 && idx < Size && ooz > zBuffer[idx])
                        {
                            zBuffer[idx] = ooz;
                            int lumIdx = (int)(luminance * 8);
                            lumIdx = Math.Clamp(lumIdx, 0, 11);
                            buffer[idx] = ".,-~:;=!*#$@"[lumIdx];
                        }
                    }
                }

                Console.SetCursorPosition(0, 0);
                for (int i = 0; i < Size; i++)
                {
                    if (i % Width == 0) Console.WriteLine();
                    Console.Write(buffer[i]);
                }

                A += 0.04f;
                B += 0.08f;
            }
        }
    }
}