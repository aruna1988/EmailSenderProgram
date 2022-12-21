using EmailSenderDAL;
using EmailSenderDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static EmailSenderDomain.RespondList;

namespace EmailSenderBLL
{

    public class EmailSendBLLManager : IEmailSendBLLManager
    {
        public EmailDAL objEmailDAL;

        public EmailSendBLLManager()
        {
            objEmailDAL = new EmailDAL();

        }
        public List<Customer> ListCustomers()
        {
            try
            {
                return objEmailDAL.ListCustomers();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<Order> ListOrders()
        {


            try
            {
                return objEmailDAL.ListOrders();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<RespondList> SedEmail(List<Customer> e, Email objEmail, ref string OutputMessage)
        {
			List<RespondList> _List = new List<RespondList>();
			//bool IsSucess = true;
			try
			{
				//loop through list of new customers
				for (int i = 0; i < e.Count; i++)
				{
					RespondList obj = new RespondList();

					try
					{
						//If the customer is newly registered, one day back in time
						if (e[i].CreatedDateTime > DateTime.Now.AddDays(-1))
						{
							//Create a new MailMessage
							System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
							//Add customer to reciever list
							m.To.Add(e[i].Email);
							//Add subject
							m.Subject = objEmail.Subject;//  "Welcome as a new customer at EYEPAX!";
							//Send mail from info@eyepax.com
							m.From = new System.Net.Mail.MailAddress(objEmail.FromEmail);
							//Add body to mail
							m.Body = "Hi " + e[i].Email +  objEmail.Body; 
#if DEBUG
							
							//Console.WriteLine("Send mail to:" + e[i].Email);
							OutputMessage = " Send mail to:" + e[i].Email;



#else
						//Create a SmtpClient to our smtphost: yoursmtphost
						System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("yoursmtphost");
						//Send mail
						smtp.Send(m);

						obj.Email = e[i].Email;
						obj.ErrorMessage ="";
						obj.IsSend = true;
						_List.Add(obj);
#endif
						}
					}
					catch (Exception ex1)
                    {
						obj.Email = e[i].Email;
						obj.ErrorMessage = ex1.Message;
						obj.IsSend = false;
						_List.Add(obj);
					//	return false;
					}
				}
				//All mails are sent! Success!
			//	return true;
			}
			catch (Exception ex )
			{
				throw ex;
			}
			return _List;
		}

		public List<RespondList> DoEmailWork1(ref string OutputMessage)
		{
			List<RespondList> _List = new List<RespondList>();

			try
			{
				Email objEmail = new Email();
				objEmail.Subject = "Welcome as a new customer at ABC!"; 
				objEmail.Body =		
									 "<br>We would like to welcome you as customer on our site!<br><br>Best Regards,<br>ABC Team"; 
				objEmail.FromEmail = "info@abc.com";


				List<Customer> e = objEmailDAL.ListCustomers();		
				_List = SedEmail(e, objEmail, ref OutputMessage);
			}
			catch (Exception ex)
			{
				throw ex;

			}
			return _List;
		}
			
		public List<RespondList> DoEmailWork2(ref string OutputMessage)
        {
			List<RespondList> _List = new List<RespondList>();

			try
            {
				Email objEmail = new Email();
				objEmail.Subject = "We miss you as a customer";
				objEmail.Body = 
					 "<br>We miss you as a customer. Our shop is filled with nice products. Here is a voucher that gives you 50 kr to shop for." +
					
						 "<br><br>Best Regards,<br>ABC Team";
				objEmail.FromEmail = "info@abc.com";


				List<Customer> e = objEmailDAL.ListCustomers();
				List<Order> f = objEmailDAL.ListOrders();
				List<Customer> result = e.Where(X => f.All(p2 => p2.CustomerEmail != X.Email)).ToList();
				_List= SedEmail(result, objEmail, ref OutputMessage); 
			}
			catch(Exception ex)
            {
				throw ex;

            }
			return _List;

		}

	


}
}
