using System;
using System.Collections.Generic;

namespace Maths.Regression
{
    public class Potencia : Regressao
    {
        public Potencia() : base("Potencia", "POTENCIA")
        {
        }

        public override Double Calcular(Double x)
        {
            return new double();
        }

        public void CalcularParemetros(List<Coordenadas> ListPonto)
        {
            throw new NotImplementedException();
        }

        public override void CriarRegressao()
        {
            throw new NotImplementedException();
        }        

        public override string Formula()
        {
            throw new NotImplementedException();
        }
    }
}