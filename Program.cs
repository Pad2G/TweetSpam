using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using System.Diagnostics;

namespace tweetspam
{
    class Program
    {
        static string oauth_key = ""/* YOUR OAUTH */;
        static string oauth_sec = ""/* YOUR OUATH_SECRET */;
        static string troll1_tok = "";
        static string troll1_sec = "";  // ......
       
        static int max;
        static string abort;
        static string mess;
        static OAuthAccessToken access;
        static TwitterService service = new TwitterService(oauth_key, oauth_sec);
        static OAuthRequestToken requestToken = service.GetRequestToken();
        class spam
        {
            int cont1 = 0, cont2 = 0;
            string msg;
            public void setMsg(string message)
            {
                msg = message;
            }
           public void startSpamming1()
            {
                service.AuthenticateWith( troll1_tok, troll1_sec);
               while (true)
                {
                    service.SendTweet(new SendTweetOptions() { Status = msg + cont1 });
                    Console.WriteLine(2);
                     cont1++;
                }
            }
           public void startSpamming2()
            {
                service.AuthenticateWith(troll2_tok, troll2_sec);
                while (true)
                {
                    service.SendTweet(new SendTweetOptions() { Status = msg + cont2 });
                    Console.WriteLine(1);
                     cont2++;
              
                }
            }
        static void Main(string[] args)
        {
            /* USE THIS TO GET TOKENS
              Uri uri = service.GetAuthorizationUri(requestToken);
              Process.Start(uri.ToString());
              string pin = Console.ReadLine();
              access = service.GetAccessToken(requestToken, pin);
              System.IO.StreamWriter write = new System.IO.StreamWriter(@"C:\Users\pad2g\Desktop\twitt\tokens.txt");
              write.WriteLine(access.Token + "\n");
              write.WriteLine(access.TokenSecret);
              write.Close();
               
              Console.WriteLine(access.Token);
              Console.WriteLine(access.TokenSecret);*/
            Console.WriteLine("Authenticating...");
            spam spammer = new spam();
            do
            {
                Console.WriteLine("How many accounts? (2 maximum)");
                max = Convert.ToInt32(Console.ReadLine());
            } while (max > 2 || max <= 0);

            Console.WriteLine("Insert Spam Message");
            mess = Console.ReadLine();
            spammer.setMsg(mess);
            System.Threading.Thread firThread;
            firThread = new System.Threading.Thread(spammer.startSpamming1);
            System.Threading.Thread secThread;
            secThread = new System.Threading.Thread(spammer.startSpamming2);
            Console.WriteLine("Spamming...   Type \" Exit \" to Stop");
            if (max == 1) 
                firThread.Start();
            else {
                firThread.Start();
                secThread.Start();
            }
            abort = Console.ReadLine();
            if (abort == "exit")
            {
                Console.WriteLine("Exiting...");
                firThread.Abort();
                if (max==2)
                    secThread.Abort();
            }
        }

        
        }
    }
}
