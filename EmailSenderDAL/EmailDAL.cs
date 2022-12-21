using EmailSenderDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSenderDAL
{
    public class EmailDAL
    {

		public  List<Customer> ListCustomers()
		{

			try
			{
				return new List<Customer>()
					   {
						   new Customer(){Email = "mail1@mail.com", CreatedDateTime = DateTime.Now.AddHours(-7)},
						   new Customer(){Email = "mail2@mail.com", CreatedDateTime = DateTime.Now.AddDays(-1)},
						   new Customer(){Email = "mail3@mail.com", CreatedDateTime = DateTime.Now.AddMonths(-6)},
						   new Customer(){Email = "mail4@mail.com", CreatedDateTime = DateTime.Now.AddMonths(-1)},
						   new Customer(){Email = "mail5@mail.com", CreatedDateTime = DateTime.Now.AddMonths(-2)},
						   new Customer(){Email = "mail6@mail.com", CreatedDateTime = DateTime.Now.AddDays(-5)}
					   };
			}
			catch(Exception ex)
            {
				throw ex;
            }
		
		}

		/// <summary>
		/// Mockup method for listing all orders
		/// </summary>
		public  List<Order> ListOrders()
		{
			try
			{
				return new List<Order>()
					   {
						   new Order(){CustomerEmail = "mail3@mail.com", OrderDatetime = DateTime.Now.AddMonths(-6)},
						   new Order(){CustomerEmail = "mail5@mail.com", OrderDatetime = DateTime.Now.AddMonths(-2)},
						   new Order(){CustomerEmail = "mail6@mail.com", OrderDatetime = DateTime.Now.AddDays(-2)}
					   };
			}
			catch(Exception ex)
            {
				throw ex;
            }
			
		}
	}

	
}
