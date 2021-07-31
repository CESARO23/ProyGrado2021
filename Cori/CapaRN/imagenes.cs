using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace CapaRN
{
    public class imagenes
    {
        public string Guardar_Foto(Image foto, string ruta)
        {
            //Persona.capsfotper = CodificarFoto(ruta);
            FileStream fs;
            fs = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            //PCBFoto.Image = System.Drawing.Image.FromStream(fs);
            fs.Close();
            return CodificarFoto(ruta);
        }

        public string CodificarFoto(string sNombreArchivo)
        {
            string sBase64 = "";
            // Declaramos fs para tener acceso a la imagen residente en la maquina cliente. 
            FileStream fs = new FileStream(sNombreArchivo, FileMode.Open);

            // Declaramos un Leector Binario para accesar a los datos de la imagen pasarlos a un arreglo de bytes 
            BinaryReader br = new BinaryReader(fs);
            byte[] bytes = new byte[(int)fs.Length];

            try
            {
                br.Read(bytes, 0, bytes.Length);

                // base64 es la cadena en donde se guarda el arreglo de bytes ya convertido 
                sBase64 = Convert.ToBase64String(bytes);
                return sBase64;
            }

            catch
            {
                MessageBox.Show("Ocurrio un error al cargar la fotografa.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return null;
            }

            // Se cierran los archivos para liberar memoria. 
            finally
            {
                fs.Close();
                fs = null;
                br = null;
                bytes = null;
            }
        }

        public string DecodificarFoto(string sBase64)
        {
            // Declaramos fs para tener crear un nuevo archivo temporal en la maquina cliente. 
            // y memStream para almacenar en memoria la cadena recibida. 
            string sImagenTemporal = Application.StartupPath + "\\foto-decodificada.jpg";
            string sImagenTemporal2 = Application.StartupPath + "\\foto-decodificada2.jpg";
            BinaryWriter bw;
            FileStream fs;
            bool ban = true;
            try
            {
                fs = new FileStream(sImagenTemporal, FileMode.Create);
                bw = new BinaryWriter(fs);

            }
            catch
            {
                fs = new FileStream(sImagenTemporal2, FileMode.Create);
                bw = new BinaryWriter(fs);
                ban = false;
            }

            byte[] bytes;

            try
            {
                bytes = Convert.FromBase64String(sBase64);
                bw.Write(bytes);
                if (ban)
                {
                    return sImagenTemporal;
                }
                else
                {
                    return sImagenTemporal2;
                }
            }

            catch
            {
                MessageBox.Show("Ocurrio un error al leer la imagen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return sImagenTemporal = @"c:no-disponible.jpg";
            }

            finally
            {
                fs.Close();
                bytes = null;
                bw = null;
                sBase64 = null;
                sImagenTemporal = "";
                sImagenTemporal2 = "";
            }
        }

        public System.Drawing.Image Resize(System.Drawing.Image foto, int ancho, int alto, string ruta)
        {
            int ImgORAncho = foto.Width;
            int ImgOrAlto = foto.Height; // Obtengo las dimensiones de la foto

            int OrigX = 0;
            int OrigY = 0;
            int ResX = 0;  // Varables referencia para saber donde contar px
            int ResY = 0;

            float Porciento = 0;
            float PorcientoAncho = 0; // Porcentajes de sampleo
            float PorcientoAlto = 0;

            PorcientoAncho = ((float)ancho / (float)ImgORAncho);
            PorcientoAlto = ((float)alto / (float)ImgOrAlto); //Calculo el % que puedo resamplear

            if (PorcientoAlto < PorcientoAncho)
            {
                Porciento = PorcientoAlto;
            }
            else
            { // Para resamplear bien                 
                Porciento = PorcientoAncho;
            }


            int AnchuraFinal = (int)(ImgORAncho * Porciento);
            int AlturaFinal;  // Calculo las nuevas dimensiones                

            if (ancho > alto)
            {
                AlturaFinal = (int)(ImgOrAlto * Porciento);
            }
            else
            {
                AlturaFinal = AnchuraFinal;
            } // Para proporcionar la imagen 

            Bitmap RszIm = new Bitmap(ancho, alto, PixelFormat.Format24bppRgb);
            RszIm.SetResolution(foto.HorizontalResolution, foto.VerticalResolution);

            Graphics Gfoto = Graphics.FromImage(RszIm);
            Gfoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Gfoto.DrawImage(foto, new System.Drawing.Rectangle(ResX, ResY, AnchuraFinal, AlturaFinal), new System.Drawing.Rectangle(OrigX, OrigY, ImgORAncho, ImgOrAlto), GraphicsUnit.Pixel);
            Gfoto.Dispose();
            RszIm.Save(ruta, ImageFormat.Jpeg);
            return (RszIm);
        }

        public Image ConvertBase64StringToImage(string imageBase64String)
        {
            var imageBytes = Convert.FromBase64String(imageBase64String);
            var imageStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
            imageStream.Write(imageBytes, 0, imageBytes.Length);
            var image = Image.FromStream(imageStream, true);
            return image;
        }

        public string ConvertImageToBase64String(Image image)
        {
            var imageStream = new MemoryStream();
            image.Save(imageStream, ImageFormat.Png);
            imageStream.Position = 0;
            var imageBytes = imageStream.ToArray();
            return Convert.ToBase64String(imageBytes);
        }

    }
}
