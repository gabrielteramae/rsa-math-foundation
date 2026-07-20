# rsa-math-foundation

![.NET 8.0](https://img.shields.io/badge/.NET-8.0-blueviolet)
![C#](https://img.shields.io/badge/C%23-12.0-blue)
![Cryptography](https://img.shields.io/badge/Criptografia-RSA-orange)

Uma implementação didática e puramente matemática do algoritmo de criptografia assíncrona **RSA** desenvolvida em C#. O projeto demonstra a aplicação prática de conceitos de Álgebra Linear e Matemática Discreta, como números primos, aritmética modular, a Função Totiente de Euler e o Algoritmo de Euclides Estendido.

## 🧠 Como Funciona (A Matemática por Trás)

O algoritmo RSA baseia-se na assimetria matemática de que é fácil multiplicar dois números primos grandes, mas extremamente difícil fatorar o resultado de volta nos dois primos originais.

1. **Escolha dos Primos:** Escolhe-se dois números primos secretos, p e q.
2. **Módulo (n):** Calcula-se n = p \* q. Esse número é público e define o limite dos nossos blocos numéricos.
3. **Totiente de Euler (phi):** Calcula-se phi(n) = (p - 1) \* (q - 1). Ele representa a quantidade de números coprimos a n.
4. **Chave Pública (e):** Escolhe-se um expoente e que seja coprimo com phi(n), ou seja, MDC(e, phi(n)) = 1.
5. **Chave Privada (d):** Calcula-se o inverso multiplicativo modular de e mod(phi(n)), de forma que:
   (e \* d) = 1 mod(phi(n))

### Fórmulas de Operação

- **Cifrar:** C = M^e mod n
- **Decifrar:** M = C^d mod n

---

## 💻 Código da Aplicação (`Program.cs`)

O código abaixo utiliza o tipo `BigInteger` do namespace `System.Numerics` para realizar exponenciações modulares gigantescas de forma eficiente sem estourar a memória do sistema.

```csharp
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        // 1. Escolha de dois números primos didáticos
        BigInteger p = 61;
        BigInteger q = 53;

        // 2. Calcular o módulo n
        BigInteger n = p * q;

        // 3. Calcular a Função Totiente de Euler (phi)
        BigInteger phi = (p - 1) * (q - 1);

        // 4. Escolher o expoente público 'e' (coprimo com phi)
        BigInteger e = 17;

        // 5. Calcular a chave privada 'd' (Inverso multiplicativo modular)
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

        // --- ENCRIPTAÇÃO ---
        List<BigInteger> mensagemCifrada = new List<BigInteger>();
        foreach (char caractere in mensagemOriginal)
        {
            BigInteger m = (int)caractere;
            BigInteger c = BigInteger.ModPow(m, e, n); // (m^e) % n
            mensagemCifrada.Add(c);
        }

        Console.WriteLine("\n-> Mensagem Cifrada (Blocos numéricos):");
        Console.WriteLine(string.Join(" ", mensagemCifrada));

        // --- DESENCRIPTAÇÃO ---
        StringBuilder mensagemDecriptada = new StringBuilder();
        foreach (BigInteger c in mensagemCifrada)
        {
            BigInteger mDecriptado = BigInteger.ModPow(c, d, n); // (c^d) % n
            mensagemDecriptada.Append((char)mDecriptado);
        }

        Console.WriteLine("\n----------------------------------------------");
        Console.WriteLine($"-> Mensagem Decriptada Final: {mensagemDecriptada}");
        Console.WriteLine("----------------------------------------------");
    }

    // Algoritmo de Euclides Estendido para encontrar o Inverso Modular
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
```

🚀 Como Executar Localmente
Pré-requisitos
.NET 8 SDK instalado na sua máquina.

Passos para Execução
Clone este repositório:

Bash
git clone [https://github.com/gabrielteramae/rsa-math-foundation.git](https://github.com/gabrielteramae/rsa-math-foundation.git)
cd rsa-math-foundation
Execute o projeto diretamente pelo terminal:

Bash
dotnet run
