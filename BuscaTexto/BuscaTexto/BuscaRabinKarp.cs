using System;
using System.Collections.Generic;

namespace BuscaTexto {
    class BuscaRabinKarp {
        // Constantes matemáticas para a função de Hash (número primo grande e base do alfabeto)
        const long q = 10014521L;
        const int d = 128;

        public static List<int> RKSearch(String p, String t) {
            List<int> posicoes = new List<int>();
            int m = p.Length;
            int n = t.Length;
            // Se o texto for menor que o padrão, não há como haver correspondência
            if (n < m || m == 0) return posicoes;

            long dm = 1, h1 = 0, h2 = 0;
            // Pré-calcula d^(m-1) módulo q para ser usado na remoção do dígito mais significativo
            for (int i = 1; i < m; i++)
                dm = (d * dm) % q;
            
            // Calcula o valor do hash inicial para a string padrão e para a primeira janela do texto
            for (int i = 0; i < m; i++) {
                h1 = (h1 * d + p[i]) % q;
                h2 = (h2 * d + t[i]) % q;
            }

            // Desliza a janela sobre o texto
            for (int i = 0; i <= n - m; i++) {
                // Se os hashes da janela do texto e do padrão forem iguais, pode haver um "match"
                if (h1 == h2) {
                    int j;
                    // Verifica caractere por caractere para evitar falsos positivos devido à colisão de Hash
                    for (j = 0; j < m; j++) {
                        if (t[i + j] != p[j]) break;
                    }
                    // Se j chegou a m, os caracteres são idênticos, adiciona a posição
                    if (j == m) posicoes.Add(i);
                }
                
                // Calcula o Hash da próxima janela (removendo o primeiro caractere e adicionando o próximo)
                if (i < n - m) {
                    h2 = (h2 + d * q - t[i] * dm) % q;
                    h2 = (h2 * d + t[i + m]) % q;
                }
            }
            return posicoes;
        }
    }
}
