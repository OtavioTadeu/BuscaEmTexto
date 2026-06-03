using System;
using System.Collections.Generic;

namespace BuscaTexto {
    class BuscaForcaBruta {
        public static List<int> ForcaBruta(String p, String t) {
            List<int> posicoes = new List<int>();
            int i, j, aux;
            int m = p.Length;
            int n = t.Length;
            for (i = 0; i <= n - m; i++) {
                aux = i;
                for (j = 0; j < m && aux < n; j++) {
                    if (p[j] != '?' && t[aux] != p[j])
                        break;
                    aux++;
                }
                if (j == m)
                    posicoes.Add(i);
            }
            return posicoes;
        }
    }
}
