using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace BlackBoxTools.WIP
{
public class EmailSender : MonoBehaviour
{
    [SerializeField]
    private string _from;
    [SerializeField]
    private string _password;
    [SerializeField]
    private string[] _to;
    [SerializeField]
    private string _subject;
    [SerializeField]
    private string _message;
    public bool _launchMailAtStart;

    public void Start() {

        if(_launchMailAtStart)
        SendMailTo(_from, _password, _to, _subject, _message);
    }
    public  void SendMailTo(string from, string password, string[] to, string subject, string message)
    {
        SendTo(from, password, to, subject, message);

    }
    public static void SendTo(string from, string password, string[] to, string subject, string message) {
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress(from);
        for (int i = 0; i < to.Length; i++)
        {
            if(!string.IsNullOrEmpty(to[i]))
                mail.To.Add(to[i]);

        }
        mail.Subject = subject;
        mail.Body = message;

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential(from, password) as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        smtpServer.Send(mail);
        Debug.Log("success");

    }
    
}}