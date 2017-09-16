using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WITS_CORE
{
    public class ecuacionesRadar
    {/*
        public double  ecuacionAV(double diametro, double diametrodelasBoquillas, int numerodeBoquillas)
        {
            double ecuacionAV2 = 0;
            double ecuacionAV = Math.Pow(diametro, 2);
            ecuacionAV = ecuacionAV * 0.15;
            ecuacionAV2 = Math.Pow(diametrodelasBoquillas, 2);
            ecuacionAV2 = ecuacionAV2 - numerodeBoquillas;
            double ecuacionResultanteAV = ecuacionAV / ecuacionAV2;
            return ecuacionResultanteAV;
        }

        public double ecuacionM(double anguloSimetriaAxial, double longitudPotencialdelNucleo, double diametrodelasBoquillas, double distanciadeBoquillasaldonfodelhueco)
        {
            double angulo = (anguloSimetriaAxial / 2);
            double radianes = angulo * (Math.PI / 180);
            double  ecuacionM1 = Math.Tan(radianes);
            ecuacionM1 = ecuacionM1 * (2 * longitudPotencialdelNucleo);
            ecuacionM1 = ecuacionM1 + diametrodelasBoquillas;
            double ecuacionM2 = Math.Tan(radianes);
            ecuacionM2 = ecuacionM2 * distanciadeBoquillasaldonfodelhueco;
            ecuacionM2 = ecuacionM2 + diametrodelasBoquillas;
            double ecuacionResultanteM = ecuacionM1 / ecuacionM2;

            return ecuacionResultanteM;
        }

        public double ecuacionN(double ecuacionResultanteAV, double k, double ecuacionResultanteM)
        {
            double  ecuacionN = Math.Pow(ecuacionResultanteAV, -k);
            ecuacionN = 1 - ecuacionN;
            ecuacionN = (ecuacionN / (Math.Pow(ecuacionResultanteM, 2)));

            return ecuacionN;
        }

        public double ecuacionFJ(double densidaddelodo, double wits0130, double velocidadsalidaBoquillas)
        {
            double ecuacionFJ = 0.000516 * densidaddelodo;
            ecuacionFJ = ecuacionFJ * wits0130;
            ecuacionFJ = ecuacionFJ * velocidadsalidaBoquillas;
            return ecuacionFJ;
        }

        public double ecuacionWOBe(double wits0117, double ecuacionFJ, double ecuacionN)
        {
           double  ecuacionWOBe = ecuacionN * ecuacionFJ;
            ecuacionWOBe = wits0117 - ecuacionWOBe;
            return ecuacionWOBe;
        }
        
        public double ecuacionMSE(double HMSE, double ecuacionN, double caidadePresionsobrelaBroca, double wits0130)
        {
            double HMSE2 = 1154 * ecuacionN;
            HMSE2 = HMSE2 * caidadePresionsobrelaBroca;
            HMSE2 = HMSE2 * wits0130;

            double resultado = HMSE - HMSE2;
            return resultado;

        }
*/
        //TODO: Hace falta la ecuaciontb
        public List <double> ecuacionHMSE(double torqueMax, double limitemaximopresiondiferencialMotor, double ecuacionN, double caidadePresionsobrelaBroca,
                               double wits0130, double velocidadRotacionMotor, double wits0120, double areadelasBoquillas, double wits0113,
                               double torqueAplicadoEnLaBroca, double wits0119, double wits0171, double wits0117)
        {
            double MSETotal = 0;
            double HMSETotal = 0;
            List<double> resultado = new List<double>();
            if (velocidadRotacionMotor.ToString() == "0")
                 {
                double MSE1 = wits0117 / areadelasBoquillas;
                double MSE2 = ((Math.PI) * 120);
                double MSE3 = MSE2 * wits0120;
                double MSE4 = MSE3 * wits0119;
                double MSE5 = areadelasBoquillas * wits0113;
                double MSE6 = MSE4 / MSE5;
                MSETotal = MSE1 + MSE6;
                double HMSE1 = areadelasBoquillas * wits0113;
                double HMSE2 = 1154 * ecuacionN;
                double HMSE3 = caidadePresionsobrelaBroca * HMSE2;
                double HMSE4 = HMSE3 * wits0130;
                double HMSE5 = HMSE4 / HMSE1;
                HMSETotal = MSETotal + HMSE5;
                resultado.Add(MSETotal);
                resultado.Add(HMSETotal);
            }
            else
            {
                double MSE1 = wits0117 / areadelasBoquillas;
                double MSE2 = velocidadRotacionMotor * wits0130;
                double MSE3 = MSE2 + wits0120;
                double MSE4 = torqueMax / limitemaximopresiondiferencialMotor;
                double MSE5 = MSE4 * wits0171;
                double MSE6 = MSE3 * MSE5;
                double MSE7 = ((Math.PI) * 120);
                double MSE8 = MSE7 * MSE6;
                double MSE9 = areadelasBoquillas * wits0113;
                double MSE10 = MSE8 / MSE9;
                MSETotal = MSE10 + MSE1;
                double HMSE1 = areadelasBoquillas * wits0113;
                double HMSE2 = 1154 * ecuacionN;
                double HMSE3 = caidadePresionsobrelaBroca * HMSE2;
                double HMSE4 = HMSE3 * wits0130;
                double HMSE5 = HMSE4 / HMSE1;
                HMSETotal = MSETotal + HMSE5;
                resultado.Add(MSETotal);
                resultado.Add(HMSETotal);

            }
            return resultado;
        }

    }
}
