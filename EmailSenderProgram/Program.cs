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


		/*
		internal class Program
		{
			/// <summary>
			/// This application is run everyday
			/// </summary>
			/// <param name="args"></param>
			private static void Main(string[] args)
			{
				//Call the method that do the work for me, I.E. sending the mails
				Console.WriteLine("Send Welcomemail");
				bool success = DoEmailWork();

	#if DEBUG
				//Debug mode, always send Comeback mail
				Console.WriteLine("Send Comebackmail");
				success = DoEmailWork2("EYEPAXComebackToUs");
	#else
				//Every Sunday run Comeback mail
				if (DateTime.Now.DayOfWeek.Equals(DayOfWeek.Monday))
				{
					Console.WriteLine("Send Comebackmail");
					success = DoEmailWork2("EYEPAXComebackToUs");
				}
	#endif

				//Check if the sending went OK
				if (success == true)
				{
					Console.WriteLine("All mails are sent, I hope...");
				}
				//Check if the sending was not going well...
				if (success == false)
				{
					Console.WriteLine("Oops, something went wrong when sending mail (I think...)");
				}
				Console.ReadKey();
			}

			/// <summary>
			/// Send Welcome mail
			/// </summary>
			/// <returns></returns>
			public static bool DoEmailWork()
			{
				try
				{
				//List all customers
				List<Customer> e = DataLayer.ListCustomers();

					//loop through list of new customers
					for (int i = 0; i < e.Count; i++)
					{
						//If the customer is newly registered, one day back in time
						if (e[i].CreatedDateTime > DateTime.Now.AddDays(-1))
						{
							//Create a new MailMessage
							System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
							//Add customer to reciever list
							m.To.Add(e[i].Email);
							//Add subject
							m.Subject = "Welcome as a new customer at EYEPAX!";
							//Send mail from info@eyepax.com
							m.From = new System.Net.Mail.MailAddress("info@eyepax.com");
							//Add body to mail
							m.Body = "Hi " + e[i].Email +
									 "<br>We would like to welcome you as customer on our site!<br><br>Best Regards,<br>EYEPAX Team";
	#if DEBUG
							//Don't send mails in debug mode, just write the emails in console
							Console.WriteLine("Send mail to:" + e[i].Email);
	#else
		//Create a SmtpClient to our smtphost: yoursmtphost
						System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("yoursmtphost");
						//Send mail
						smtp.Send(m);
	#endif
						}
					}
					//All mails are sent! Success!
					return true;
				}
				catch (Exception)
				{
					//Something went wrong :(
					return false;
				}
			}

			/// <summary>
			/// Send Customer ComebackMail
			/// </summary>
			/// <param name="v"></param>
			/// <returns></returns>
			private static bool DoEmailWork2(string v)
			{
				try
				{
					//List all customers 
					List<Customer> e = DataLayer.ListCustomers();
					//List all orders
					List<Order> f = DataLayer.ListOrders();

					//loop through list of customers
					foreach (Customer c in e)
					{
						// We send mail if customer hasn't put an order
						bool Send = true;
						//loop through list of orders to see if customer don't exist in that list
						foreach (Order o in f)
						{
							// Email exists in order list
							if (c.Email == o.CustomerEmail)
							{
								//We don't send email to that customer
								Send = false;
							}
						}

						//Send if customer hasn't put order
						if (Send == true)
						{
							//Create a new MailMessage
							System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
							//Add customer to reciever list
							m.To.Add(c.Email);
							//Add subject
							m.Subject = "We miss you as a customer";
							//Send mail from info@eyepax.com
							m.From = new System.Net.Mail.MailAddress("infor@eyepax.com");
							//Add body to mail
							m.Body = "Hi " + c.Email +
									 "<br>We miss you as a customer. Our shop is filled with nice products. Here is a voucher that gives you 50 kr to shop for." +
									 "<br>Voucher: " + v +
									 "<br><br>Best Regards,<br>EYEPAX Team";
	#if DEBUG
							//Don't send mails in debug mode, just write the emails in console
							Console.WriteLine("Send mail to:" + c.Email);
	#else
		//Create a SmtpClient to our smtphost: yoursmtphost
						System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("yoursmtphost");
						//Send mail
						smtp.Send(m);
	#endif
						}
					}
					//All mails are sent! Success!
					return true;
				}
				catch (Exception)
				{
					//Something went wrong :(
					return false;
				}
			}
		}*/
	}