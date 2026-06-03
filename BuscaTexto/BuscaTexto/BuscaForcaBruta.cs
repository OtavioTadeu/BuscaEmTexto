using System;
using System.Collections.Generic;

namespace BuscaTexto {
    class BuscaForcaBruta {
        public static List<int> ForcaBruta(String p, String t) {
            // Lista para armazenar o índice inicial de todas as ocorrências encontradas
            List<int> posicoes = new List<int>();
            int i, j, aux;
            int m = p.Length; // Tamanho do padrão (palavra buscada)
            int n = t.Length; // Tamanho do texto completo

            // Percorre o texto até o último ponto onde o padrão ainda caberia
            for (i = 0; i <= n - m; i++) {
                aux = i;
                // Tenta casar o padrão caractere por caractere a partir da posição 'i'
                for (j = 0; j < m && aux < n; j++) {
                    // Se o caractere do padrão for '?' ele funciona como curinga (ignora a diferença)
                    // Caso contrário, se os caracteres forem diferentes, interrompe a verificação (break)
                    if (p[j] != '?' && t[aux] != p[j])
                        break;
                    aux++;
                }
                // Se j chegou até m, significa que todos os caracteres casaram com sucesso
                if (j == m)
                    posicoes.Add(i); // Adiciona a posição inicial da ocorrência na lista
            }
            return posicoes;
        }
    }
}
