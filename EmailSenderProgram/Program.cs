using EmailSenderBLL;
using EmailSenderDomain;
using System;
using System.Collections.Generic;

namespace EmailSenderProgram
{
	public class Program
	{

		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public static IEmailSendBLLManager objIEmailSendBLLManager;
		public static   void Main(string[] args)
		{
            try
            {

				//Call the method that do the work for me, I.E. sending the mails
				Console.WriteLine("Send Welcomemail");
				log.Info("Send Welcomemail");

				objIEmailSendBLLManager = new EmailSendBLLManager();
				List<RespondList> _List = new List<RespondList>();
				string Msg = "";
				_List.AddRange(objIEmailSendBLLManager.DoEmailWork1(ref Msg));

				if (DateTime.Now.DayOfWeek.Equals(DayOfWeek.Monday))
				{
					Console.WriteLine("Send Comebackmail");
					log.Info("Send Comebackmail");
					_List.AddRange(objIEmailSendBLLManager.DoEmailWork2(ref Msg));

				}
				log.Info("Starting...................................." + DateTime.Now.ToString());
				foreach (RespondList R in _List)
				{

					log.Info(R.Email + " " + R.ErrorMessage + " " + R.IsSend.ToString() +" " + DateTime.Now.ToString());


				}
				log.Info("Ending...................................." + DateTime.Now.ToString());
				//Console.ReadKey();

			}
			catch (Exception ex)
            {

				log.Error(ex.Message+ DateTime.Now.ToString());
            }

		}

		
	}

	}