using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WITS_CORE
{
    public class ecuacionesRadar
    {
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

        public double ecuacionMSE(double torqueMax, double limitemaximopresiondiferencialMotor, double wits0171, double wits0119, double velocidadRotacionMotor, double wits0130, double wits0120,
                               double diametro, double wits0113, double wits0117)
        {
            double MSE1 = (torqueMax / limitemaximopresiondiferencialMotor);
            MSE1 = MSE1 * wits0171;
            MSE1 = MSE1 + wits0119;
            double MSE2 = (velocidadRotacionMotor * wits0130);
            MSE2 = MSE2 + wits0120;
            MSE2 = MSE2 * 480;
            double MSE3 = Math.Pow(diametro, 2);
            MSE3 = MSE3 * wits0113;
            double MSE4 = MSE2 * MSE1;
            MSE4 = MSE4 / MSE3;
            double MSE5 = 4 * wits0117;
            MSE5 = MSE5 / (Math.PI * (Math.Pow(diametro, 2)));
            double MSEResultante = MSE5 + MSE4;
            return MSEResultante;
            
        }

        //TODO: Hace falta la ecuaciontb
        public double ecuacionHMSE(double torqueMax, double limitemaximopresiondiferencialMotor, double ecuacionN, double caidadePresionsobrelaBroca,
                               double wits0130, double velocidadRotacionMotor, double wits0120, double areadelasBoquillas, double wits0113, double ecuacionWOBe,
                               double torqueAplicadoEnLaBroca, double wits0119, double wits0171)
        {
            double HMSE1 = torqueMax / limitemaximopresiondiferencialMotor;
            HMSE1 = HMSE1 * wits0171;
            double ecuaciontb = (torqueAplicadoEnLaBroca * wits0119)*wits0119;
            HMSE1 = HMSE1 + ecuaciontb;
            double HMSE2 = 1154 * ecuacionN;
            HMSE2 = HMSE2 * caidadePresionsobrelaBroca;
            HMSE2 = HMSE2 * wits0130;
            double HMSE3 = HMSE1 + HMSE2;
            double HMSE4 = velocidadRotacionMotor * wits0130;
            HMSE4 = HMSE4 + wits0120;
            double HMSE5 = ((Math.PI) * 120);
            HMSE5 = HMSE5 * HMSE4;
            HMSE5 = HMSE5 * HMSE3;
            double HMSE6 = areadelasBoquillas * wits0113;
            double HMSE7 = HMSE5 / HMSE6;
            double HMSE8 = ecuacionWOBe / areadelasBoquillas;
            double HMSEresultante = HMSE8 + HMSE7;

            return HMSEresultante;
        }

    }
}
