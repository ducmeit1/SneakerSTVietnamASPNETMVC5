using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;

namespace SneakerSTVietnamMVC.Helpers
{
    public class HelpersSet
    {
        public static string EncryptMD5(string inputTxt)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(System.Text.Encoding.Default.GetBytes(inputTxt));
            System.Text.StringBuilder newStr = new System.Text.StringBuilder();
            foreach (byte b in data)
            {
                newStr.Append(b.ToString("x2"));
            }
            return newStr.ToString();
        }

        public static string GenerateRandomString()
        {
            System.Text.StringBuilder rdStr = new System.Text.StringBuilder();
            string libraryCharacter = "1234567890abcdefghiklmnopqwertyuizxABCDEFGHIKLMNOPQWRTZX";
            Random rd = new Random();
            for (int i = 0; i < 10; i++)
            {
                int index = rd.Next(libraryCharacter.Length);
                rdStr.Append(libraryCharacter[index]);
            }
            return rdStr.ToString();
        }

        public static bool SendEmail(string receiverEmail, string subject, string message)
        {
            if (receiverEmail != null && subject != null && message != null)
            {

                string senderEmail = "info.sneakerst@ink4link.com";
                string senderPassword = "Abc12345#$";
                string senderName = "SneakerST Vietnam";
                MailMessage m = new MailMessage(new MailAddress(senderEmail, senderName), new MailAddress(receiverEmail));
                m.Subject = subject;
                m.Body = message;
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("server188.web-hosting.com");
                smtp.Credentials = new System.Net.NetworkCredential(senderEmail, senderPassword);
                smtp.EnableSsl = true;
                smtp.Port = 25;
                try
                {
                    smtp.Send(m);
                    return true;
                }
                catch (SmtpFailedRecipientsException ex)
                {
                    for (int i = 0; i < ex.InnerExceptions.Length; i++)
                    {
                        SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                        if (status == SmtpStatusCode.MailboxBusy ||
                            status == SmtpStatusCode.MailboxUnavailable)
                        {
                            Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                            System.Threading.Thread.Sleep(5000);
                            try
                            {
                                smtp.Send(m);
                                return true;
                            } catch (Exception ex1) { Console.WriteLine(ex1.Message); }
                        }
                        else
                        {
                            Console.WriteLine("Failed to deliver message to {0}",
                                ex.InnerExceptions[i].FailedRecipient);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in RetryIfBusy(): {0}",
                            ex.ToString());
                }
            }
            return false;
        }
    }
}