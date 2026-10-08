# RSA Math Foundation — RSA didático em C#

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-console-239120?style=flat&logo=csharp&logoColor=white)

Console que cifra e decifra um texto com RSA para mostrar a conta, não para proteger dado. Os primos são fixos e pequenos (`p = 61`, `q = 53`), o expoente público é `e = 17`, e cada caractere vira um bloco. Não há padding (OAEP ou PKCS#1), geração de primo, nem tamanho de chave de produção. Não use isso como biblioteca criptográfica.

| Passo | Conta no `Program.cs` |
|---|---|
| Módulo | `n = p * q` |
| Totiente | `phi = (p - 1) * (q - 1)` |
| Chave privada | `ModInverse` (Euclides estendido) acha `d` tal que `e * d ≡ 1 (mod phi)` |
| Cifrar | `BigInteger.ModPow(m, e, n)` com `m` = código do caractere |
| Decifrar | `BigInteger.ModPow(c, d, n)` e converte de volta para `char` |

Se `e` não for coprimo com `phi`, `ModInverse` lança `ArgumentException`.

## Stack

- .NET 10 (`net10.0`), console (`OutputType` Exe)
- `System.Numerics.BigInteger` para a exponenciação modular
- Nullable e implicit usings ligados no csproj. Não há `LangVersion` fixada.

## Estrutura

```
rsa-math-foundation/
├── DiscreteMath.RSA.csproj
└── Program.cs
```

Tudo está em `Program.cs`: geração das chaves, leitura da linha no console, cifra, decifra e `ModInverse`. Não há testes.

## Como rodar

Pré-requisito: [.NET 10 SDK](https://dotnet.microsoft.com/download).

```bash
git clone https://github.com/gabrielteramae/rsa-math-foundation.git
cd rsa-math-foundation
dotnet run
```

O programa imprime `p`, `q`, `n`, `phi` e os pares `(e, n)` e `(d, n)`. Em seguida pede uma mensagem. Entrada vazia encerra sem cifrar.

---

© 2026 Gabriel Teramae Chan
