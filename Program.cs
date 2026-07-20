using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        BigInteger p = 61;
        BigInteger q = 53;
        BigInteger n = p * q;
        BigInteger phi = (p - 1) * (q - 1);
        BigInteger e = 17;
        BigInteger d = ModInverse(e, phi);

        Console.WriteLine("==============================================");
        Console.WriteLine("         CONFIGURAÇÃO DAS CHAVES RSA          ");
        Console.WriteLine("==============================================");
        Console.WriteLine($"Prime (p): {p}");
        Console.WriteLine($"Prime (q): {q}");
        Console.WriteLine($"Módulo (n = p * q): {n}");
        Console.WriteLine($"Totiente (phi): {phi}");
        Console.WriteLine($"Chave Pública (e, n): ({e}, {n})");
        Console.WriteLine($"Chave Privada (d, n): ({d}, {n})");
        Console.WriteLine("==============================================\n");

        Console.Write("Digite uma mensagem para encriptar: ");
        string mensagemOriginal = Console.ReadLine();

        if (string.IsNullOrEmpty(mensagemOriginal)) return;

        // Encriptação
        List<BigInteger> mensagemCifrada = new List<BigInteger>();
        foreach (char caractere in mensagemOriginal)
        {
            BigInteger m = (int)caractere;
            BigInteger c = BigInteger.ModPow(m, e, n);
            mensagemCifrada.Add(c);
        }

        Console.WriteLine("\n-> Mensagem Cifrada (Blocos numéricos):");
        Console.WriteLine(string.Join(" ", mensagemCifrada));

        // Decriptação
        StringBuilder mensagemDecriptada = new StringBuilder();
        foreach (BigInteger c in mensagemCifrada)
        {
            BigInteger mDecriptado = BigInteger.ModPow(c, d, n);
            mensagemDecriptada.Append((char)mDecriptado);
        }

        Console.WriteLine("\n----------------------------------------------");
        Console.WriteLine($"-> Mensagem Decriptada Final: {mensagemDecriptada}");
        Console.WriteLine("----------------------------------------------");
    }

    static BigInteger ModInverse(BigInteger e, BigInteger phi)
    {
        BigInteger t = 0, newt = 1;
        BigInteger r = phi, newr = e;

        while (newr != 0)
        {
            BigInteger quotient = r / newr;
            BigInteger tempT = t;
            t = newt;
            newt = tempT - quotient * newt;
            BigInteger tempR = r;
            r = newr;
            newr = tempR - quotient * newr;
        }

        if (r > 1) throw new ArgumentException("O valor de 'e' não é coprimo com phi.");
        if (t < 0) t = t + phi;

        return t;
    }
}