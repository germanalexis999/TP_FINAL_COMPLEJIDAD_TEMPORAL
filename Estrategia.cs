
using System;
using System.Collections.Generic;
using tp1;
using tp2;

namespace tpfinal
{

	class Estrategia
	{

		public String Consulta1(ArbolBinario<DecisionData> arbol)
		{
            
            HashSet<string> predicciones = new HashSet<string>();

            void Recorrer(ArbolBinario<DecisionData> nodo)
            {
                if (nodo == null) return;

                if (nodo.esHoja())
                {
                    var hojaPred = nodo.getDatoRaiz().Predictions;
                    if (hojaPred != null)
                    {
                        foreach (var clave in hojaPred.Keys)
                        {
                            predicciones.Add(clave);
                        }
                    }
                }
                else
                {
                    Recorrer(nodo.getHijoIzquierdo());
                    Recorrer(nodo.getHijoDerecho());
                }
            }

            Recorrer(arbol);

            return "Posibles predicciones: " + string.Join(", ", predicciones);
        }


		public String Consulta2(ArbolBinario<DecisionData> arbol)
		{

			return "Implementar";
		}


		public String Consulta3(ArbolBinario<DecisionData> arbol)
		{
			string result = "Implementar";
			return result;
		}

		public ArbolBinario<DecisionData> CrearArbol(Clasificador clasificador)
		{
            if (clasificador.crearHoja())
            {
               
                var predicciones = clasificador.obtenerDatoHoja();
                var hoja = new DecisionData(predicciones);
                return new ArbolBinario<DecisionData>(hoja);
            }
            else
            {
                
                var pregunta = clasificador.obtenerPregunta();
                var nodo = new DecisionData(pregunta);

                var clasificadorIzq = clasificador.obtenerClasificadorIzquierdo();
                var clasificadorDer = clasificador.obtenerClasificadorDerecho();

                var arbol = new ArbolBinario<DecisionData>(nodo);

                arbol.agregarHijoIzquierdo(CrearArbol(clasificadorIzq));
                arbol.agregarHijoDerecho(CrearArbol(clasificadorDer));

                return arbol;
            }
        }
	}
}