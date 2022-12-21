using EmailSenderDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSenderBLL
{
 public  interface IEmailSendBLLManager
   
    {
         List<Customer> ListCustomers();
         List<Order> ListOrders();
         List<RespondList> DoEmailWork1(ref string OutputMessage);
         List<RespondList> DoEmailWork2(ref string OutputMessage);
    }
}
