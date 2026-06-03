using System;
using System.Collections.Generic;

namespace BuscaTexto {
    class BuscaKMP {
        static int[] next = new int[1000];

        // Função de pré-processamento que cria a tabela de falha (array 'next')
        // A tabela de falha indica quantos caracteres podem ser ignorados ao ocorrer um erro (mismatch)
        public static void InitNext(String p) {
            int i = 0, j = -1, m = p.Length;
            next[0] = -1;
            while (i < m) {
                while (j >= 0 && p[i] != p[j])
                    j = next[j];
                i++;
                j++;
                next[i] = j;
            }
        }

        public static List<int> KMPSearch(String p, String t) {
            List<int> posicoes = new List<int>();
            int i = 0, j = 0, m = p.Length, n = t.Length;
            if (m == 0 || n < m) return posicoes;

            // Inicializa a tabela do padrão antes da busca
            InitNext(p);
            
            while (i < n) {
                // Se houver mismatch, avança o índice do padrão usando a tabela precalculada
                while (j >= 0 && t[i] != p[j]) {
                    j = next[j];
                }
                i++;
                j++;
                // Se j chegou ao final da string de busca, uma ocorrência inteira foi encontrada
                if (j == m) {
                    posicoes.Add(i - m); // Salva o índice inicial onde o padrão foi achado
                    j = next[j]; // Continua a busca a partir de onde parou para achar múltiplas ocorrências
                }
            }
            return posicoes;
        }
    }
}
